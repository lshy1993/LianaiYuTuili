using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SystemConfig
    {
        private static SystemConfig instance;
        public static SystemConfig GetInstance()
        {
            if (instance == null) instance = new SystemConfig();
            return instance;

        }


        public int volumeBGM, volumeSE, volumeVoice;
        public string currentBGM, currentSE, currentVoice;

        //public string currentScenario;
        //public int ScenarioLine;

        private SystemConfig()
        {
            init();
        }

        private void init()
        {
            
        }
    }
}
