using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    protected override void CalculateScale()
    {
        scaleMull++;

        Hand.Heal((scaleMull/2) + 1);

        SetSTRText();
    }

    protected override void SetSTRText() => Hand.SetSTRText("Katana Heal: " + ((scaleMull / 2) + 1).ToString());
}
