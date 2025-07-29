using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Leth : Weapon
{
    protected override void CalculateScale()
    {
        scaleMull++;

        transform.localScale += new Vector3(.025f, .025f, 0);

        SetSTRText();
    }

    protected override void SetSTRText() => Hand.SetSTRText("Bat_Leth SizeUp: " + scaleMull.ToString());
}
