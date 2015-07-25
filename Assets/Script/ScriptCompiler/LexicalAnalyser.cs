using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Assets.Script.ScriptCompiler
{
    class LexicalAnalyzer
    {
        enum State
        {
            INIT,
            VARIABLE,
            KEYWORD,
            LABEL,
            FUNCTION,
            VALUE
        }

        public string source;
        private State state;

        public static string[] operators = 
        {
            // "+", "-", "*", "/",
            "=", "==", "(" ,")",
        };

        public static string[] keywords = 
        {
            "IF", "THEN",
            "GOTO", "EXIT", "END"
        };

       public LexicalAnalyzer(string source)
        {
            this.source = source;
        }

        public List<List<Token>> divide()
        {
            List<List<Token>> tokens = new List<List<Token>>();
            state = State.INIT;
            string[] lines = preprocess();

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                string buffer = "";
                List<Token> tokensInLine = new List<Token>();
                state = State.INIT;
                for(int j = 0; j < line.Length; j++)
                {
                    char chr = line[j];
                    // 处理字符
                    if(isBlank(chr) || isOperator(chr) || isEOS(chr))
                    {
                        // 词结束状态，处理缓冲区
                        // 处理缓冲区
                        if (buffer.Length != 0)
                        {
                            Token token;
                            token = new Token(buffer);

                            if (isKeyWord(buffer))
                            {
                                // 缓冲区是关键词的情况
                                token.type = TokenType.KEYWORD;
                                buffer = ""; // 清空缓冲区
                                state = State.INIT;
                                tokensInLine.Add(token);
                                continue;
                            }

                            switch (state)
                            {
                                case State.VARIABLE:
                                    token.type = TokenType.VARIABLE;
                                    state = State.INIT;
                                    break;
                                case State.LABEL:
                                    token.type = TokenType.LABEL;
                                    state = State.INIT;
                                    break;
                                case State.FUNCTION:
                                    token.type = TokenType.FUNCTION;
                                    break;
                                case State.VALUE:
                                    token.type = TokenType.VALUE;
                                    state = State.INIT;
                                    break;
                                case State.INIT:
                                    token.type = TokenType.DIALOG;
                                    break;
                                default:
                                    break;
                            }
                            tokensInLine.Add(token);
                            buffer = "";

                            // 缓冲区处理完毕
                        }
                       
                        if(isOperator(chr))
                        {
                            // 处理操作符
                            Token t = new Token("" + chr);
                            tokensInLine.Add(t);
                            t.type = TokenType.OPERATOR;

                            if (state == State.FUNCTION && chr == '(')
                            {
                                state = State.VALUE;
                            }
                            else
                            {
                                state = State.INIT;
                            } 
                        }

                    }
                    else if(chr == '$')
                    {
                        state = State.VARIABLE;
                    }
                    else if (chr == '%')
                    {
                        state = State.LABEL;
                    }
                    else if (chr == '@')
                    {
                        state = State.FUNCTION;
                    }
                    else if (chr == ',')
                    {
                        state = State.VALUE;
                    }
                    else if(chr == ':')
                    {
                        Token token = new Token(buffer);
                        token.type = TokenType.NAME;
                        tokensInLine.Add(token);
                        buffer = "";
                    }
                    else
                    {
                        buffer += chr;
                    }

                }

                tokens.Add(tokensInLine);
          }

            return tokens;
        }

        /// <summary>
        /// Method<c>preprocess</c>
        /// 预处理过程，消除所有的注释
        /// </summary>
        public string[] preprocess()
        {
            string pattern = "#.*";
            Regex reg = new Regex(pattern);
            source = reg.Replace(source, "");
            return source.Split(new char[] { '\n' });
        }

        /// <summary>
        /// Method <c>isEOS</c>
        /// 判断是否为表达式结束
        /// </summary>
        /// <param name="c">被判断的字符</param>
        /// <returns>如果是表达式终结符则为true,反之false</returns>
        public bool isEOS(char c)
        {
            return c == ';';
        }

        /// <summary>
        /// Method <c>isOperator</c>
        /// 判断是否为操作符
        /// </summary>
        /// <param name="c">被判断的字符</param>
        /// <returns>如果是操作符则为true,反之false</returns>
 
        private bool isOperator(char c)
        {
            string s = "" + c;
            for(int i = 0; i < operators.Length; i++)
            {
                if (s.Equals(operators[i]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Method <c>isBlank</c>
        /// 判断是否为空白(全角，半角)
        /// </summary>
        /// <param name="c">被判断的字符</param>
        /// <returns>如果是空白则为true,反之false</returns>
        private bool isBlank(char c)
        {
            return (c == ' '  ||
                    c == '\t' ||
                    c == '\r' ||
                    c == '\n' ||
                    c == '\u3000'
                );
        }

        /// <summary>
        /// Method <c>isKeyWord</c>
        /// 判断是否为关键词(全角，半角)
        /// </summary>
        /// <param name="s">被判断的字符</param>
        /// <returns>如果是关键字则为true，反之false</returns>
        private bool isKeyWord(string s)
        {

            for (int i = 0; i < keywords.Length; i++ )
            {
                if (keywords[i].Equals(s))
                    return true;
            }

                return false;
        }


        //private bool isValidVariableChar(char c)
        //{
            
        //}

    }
}
