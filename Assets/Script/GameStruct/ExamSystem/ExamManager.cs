using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.GameStruct
{
    public class ExamManager
    {
        private static ExamManager instance;

        public static ExamManager GetInstance()
        {
            if (instance == null) instance = new ExamManager();
            return instance;
        }

        private ExamManager() { }

        /// <summary>
        /// 获取某一类型的所有难度题目
        /// </summary>
        private void GetQuestionsOfType(string typeName)
        {

        }

        /// <summary>
        /// 获取某一难度的所有类型题目
        /// </summary>
        private void GetQuestionsOfHard(int hardLevel)
        {

        }

        private void GetQuestions()
        {

        }
    }
}
