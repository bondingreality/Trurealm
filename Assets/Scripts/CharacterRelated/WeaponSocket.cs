using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WeaponSocket : GearSocket
{
    private float currentY;

    [SerializeField]
    private SpriteRenderer parentRenderer;

    public override void SetXandY(float x, float y)
    {
        base.SetXandY(x, y);

        if(currentY != y)
        {
            if(y == 1)//Back
            {
                spriteRenderer.sortingOrder = parentRenderer.sortingOrder - 1;
            }
            else//front
            {
                spriteRenderer.sortingOrder = parentRenderer.sortingOrder + 5;
            }
        }
    }
}
