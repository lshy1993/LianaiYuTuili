using Assets.Script.GameStruct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct
{
    public interface LoadSaveInterface
    {

        void Load(GameDataSet data);
        void Save(GameDataSet data);
    }
}
