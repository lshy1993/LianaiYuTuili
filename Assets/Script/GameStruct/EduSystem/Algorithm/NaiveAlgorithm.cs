using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.GameStruct.Model;
using UnityEngine;

namespace Assets.Script.GameStruct.EduSystem.Algorithm
{
    public class NaiveAlgorithm : EduAlgorithm
    {
        Random random;
        public int CalculateDelta(int result, Player player, Range range)
        {
            return UnityEngine.Random.Range(range.GetMin(), range.GetMax());
        }

        public int ResultType(Player player)
        {

            return 0;
        }
    }
}
