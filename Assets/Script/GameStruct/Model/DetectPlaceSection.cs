using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.GameStruct.Model
{
    public class DetectPlaceSection
    {
        public string place;
        public string entry;
        public List<DetectInvest> invests;
        public List<DetectDialog> dialogs;
        public List<string> moves;

        public DetectPlaceSection(JsonData data)
        {
            place = (string)data["地点"];
            entry = (string)data["进入事件"];
            dialogs = new List<DetectDialog>();
            invests = new List<DetectInvest>();
            moves = new List<string>();

            if (data.Contains("调查")
                && data["调查"] != null
                && data["调查"].Count > 0)
            {
                foreach (JsonData inv in data["调查"]) invests.Add(new DetectInvest(inv));
                //invests.Add(new DetectInvest(data["调查"]));
            }

            if (data.Contains("对话")
                && data["对话"] != null
                && data["对话"].Count > 0)
            {
                foreach (JsonData dia in data["对话"]) dialogs.Add(new DetectDialog(dia));
                //dialogs.Add(new DetectDialog(data["对话"]));
            }

            if (data.Contains("移动")
                && data["移动"] != null
                && data["移动"].Count > 0)
            {
                foreach (JsonData mov in data["移动"]) moves.Add((string)mov);
                //moves.Add((string)data["移动"]);
            }
        }
    }
}
