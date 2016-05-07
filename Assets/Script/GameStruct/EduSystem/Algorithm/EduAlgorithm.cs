using Assets.Script.GameStruct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.EduSystem.Algorithm
{
    public interface EduAlgorithm
    {
        int ResultType(Player player);
        int CalculateDelta(int result, Player player, Range range);
    }
}
