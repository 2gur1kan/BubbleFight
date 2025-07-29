using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Weapon
{
    [Header("Shuriken")]
    [SerializeField] private GameObject ThrowShuriken;
    [SerializeField] private float ThrowTimer = 5f;

    protected override void Start()
    {
        base.Start();

        Invoke("ThrowShurikenInvoke", 1f);
    }

    protected override void CalculateScale()
    {
        scaleMull++;

        SetSTRText();
    }

    protected override void SetSTRText() => Hand.SetSTRText("Shuriken Bounce: " + scaleMull.ToString());

    #region shuriken

    protected void ThrowShurikenInvoke()
    {
        GameObject gg = Instantiate(ThrowShuriken, transform.position, Quaternion.identity);

        gg.GetComponent<Collider2D>().excludeLayers = 1 << gameObject.layer;

        gg.GetComponent<ThrowObject>().Bounce = scaleMull;
        gg.GetComponent<ThrowObject>().bubble = Hand;

        Invoke("ThrowShurikenInvoke", ThrowTimer);
    }

    #endregion
}
