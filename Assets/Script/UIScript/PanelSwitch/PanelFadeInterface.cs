using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.UIScript
{
    public delegate void UIAnimationCallback();
    interface PanelFadeInterface
    {
        void Open(UIAnimationCallback callback);
        void Open();
        void Close(UIAnimationCallback callback);
        void Close();
    }
}
