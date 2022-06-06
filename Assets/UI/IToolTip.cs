using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGRules.UI
{
    public interface IToolTip
    {
        string getToolTipMessage();
        float getOffsetX();
        Sprite getToolTipImage();
    }
}