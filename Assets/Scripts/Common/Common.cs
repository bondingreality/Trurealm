using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class Common
{
    public static int GetLayerMaskID(string LayerMaskName)
    {
        return 1 << LayerMask.NameToLayer(LayerMaskName);
    }
}
