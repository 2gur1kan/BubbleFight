using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    protected override void CalculateScale()
    {
        scaleMull++;

        Hand.RotAngle = 1;

        if(attackRate > .1f) attackRate -= (scaleMull % 10 == 0) ? 0.1f : 0f;

        SetSTRText();
    }

    protected override void SetSTRText() => Hand.SetSTRText("Dagger SpeedUp: " + scaleMull.ToString());
}
