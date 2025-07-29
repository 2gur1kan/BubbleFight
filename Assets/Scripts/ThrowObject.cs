using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] protected int damage = 5;
    [SerializeField] protected int bounce = 2;
    [SerializeField] protected float speed = 5f;

    [Header("Req :")]
    protected Rigidbody2D rb;
    protected Vector2 moveDirection;
    [HideInInspector] public Bubble bubble;

    public int Bounce { set => bounce += value; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Random.insideUnitCircle.normalized;
    }

    protected virtual void Update()
    {
        rb.velocity = moveDirection * speed;

        Vector3 gg = transform.rotation.eulerAngles;
        gg.z += 10;
        transform.rotation = Quaternion.Euler(gg);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall")) TouchWall(collision);

        if (collision.collider.CompareTag("Weapon")) TouchWeapon(collision);

        if (collision.collider.CompareTag("Bubble"))
        {
            Bubble bubble = collision.collider.GetComponent<Bubble>();
            if (bubble != null && bubble != this.bubble)
            {
                bubble.TakeDamage(damage);
            }

            TouchBubble();
        }
    }

    #region Transition Functions

    protected virtual void TouchWall(Collision2D collision)
    {
        if (bounce <= 0) Destroy(gameObject);

        Reflect(collision);
        bounce--;
    }

    protected virtual void TouchWeapon(Collision2D collision)
    {
        if (bounce <= 0) Destroy(gameObject);

        Reflect(collision);
        bounce--;
    }

    protected virtual void TouchBubble()
    {
        Destroy(gameObject);
    }

    #endregion

    public void Reflect(Collision2D collision)
    {
        Vector2 incoming = moveDirection.normalized;
        Vector2 normal = collision.contacts[0].normal.normalized;

        // Yansýtma iþlemi: R = V - 2 * (V·N) * N
        float dot = Vector2.Dot(incoming, normal);
        Vector2 reflected = incoming - 2f * dot * normal;

        moveDirection = reflected.normalized;
    }
}
