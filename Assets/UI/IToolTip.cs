using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToolTip
{
    string getToolTipMessage();
    float getOffsetX();
    Sprite getToolTipImage();
}