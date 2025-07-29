using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bubble Hand;

    [SerializeField] protected int damage = 10;
    [SerializeField] protected float attackRate = .1f;

    protected int scaleMull = 0;

    protected Collider2D col;

    protected virtual void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        SetSTRText();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall")) TouchWall();

        if (collision.collider.CompareTag("Weapon")) TouchWeapon(collision);

        if (collision.collider.CompareTag("Bubble"))
        {
            Bubble bubble = collision.collider.GetComponent<Bubble>();
            if (bubble != null && bubble != Hand)
            {
                bubble.TakeDamage(damage);

                CalculateScale();
            }

            TouchBubble();
        }
    }

    #region Transition Functions

    protected virtual void TouchWall()
    {
        Hand.TurnOtherSide();
        SetWeaponAttackRate();
    }

    protected virtual void TouchWeapon(Collision2D collision)
    {
        Hand.TurnOtherSide();
    }

    protected virtual void TouchBubble()
    {
        Hand.TurnOtherSide();
        SetWeaponAttackRate();
    }

    #endregion

    #region Other Func

    public int ScaleMull { get => scaleMull; }

    protected virtual void CalculateScale()
    {
        scaleMull++;

        damage++;

        SetSTRText();
    }

    protected virtual void SetSTRText() => Hand.SetSTRText("Sword Damage: " + damage.ToString());

    protected void SetWeaponAttackRate()
    {
        col.isTrigger = true;

        Invoke("ResetColTrigger", attackRate);
    }

    protected void ResetColTrigger() => col.isTrigger = false;

    public void SaiEffect()
    {
        scaleMull--;
        if (scaleMull < 0) scaleMull = 0;

        SetSTRText();
    }

    #endregion
}
