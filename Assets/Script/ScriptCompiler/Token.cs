using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.ScriptCompiler
{
    public enum TokenType
    {
        KEYWORD,
        VARIABLE,
        OPERATOR,
        NAME,
        DIALOG,
        LABEL,
        FUNCTION,
        VALUE,
        BAD,
    }

    /// <summary>
    /// Class <c>Token</c>
    /// Token表示最小的可被Parser执行的元素，保存一系列相关信息。
    /// 由LexicalAnalyser创建。
    /// </summary>
    public class Token
    {


        public TokenType type;
        public string value;

        public Token(string value)
        {
            this.value = value;
        }
    }
}
