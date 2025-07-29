using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sai : Weapon
{
    protected override void TouchWeapon(Collision2D collision)
    {
        base.TouchWeapon(collision);

        scaleMull++;

        damage += scaleMull / 4;

        collision.collider.GetComponent<Weapon>().SaiEffect();

        SetSTRText();
    }

    protected override void SetSTRText() => Hand.SetSTRText("Sai Effect: " + scaleMull.ToString());
}
