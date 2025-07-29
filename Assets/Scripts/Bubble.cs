using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bubble : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private bool isEnemy = false;
    [SerializeField] private WeaponEnum weapon;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health = 100;
    [SerializeField] private float rotAngle= 10f;

    private float angleMul = 1f;

    [Header("Req :")]
    [SerializeField] private Transform WeaponCenter;
    [SerializeField] private TextMeshPro HPText;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public Vector2 Velocity { get => moveDirection; set => moveDirection = value; }
    public float RotAngle { set => rotAngle += value; }
    public WeaponEnum SetWeapon { set => weapon = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Random.insideUnitCircle.normalized;

        HPText.text = health.ToString();
    }

    private void Start()
    {
        GameObject Weapon = null;

        if (isEnemy)
        {
            Weapon = WeaponDBManager.Instance.getWeaponGO(WeaponDBManager.Instance.getRandomWeaponName());
        }
        else Weapon = WeaponDBManager.Instance.getWeaponGO(weapon);

        if (Weapon == null) Destroy(gameObject);

        Vector3 weaponPos = Weapon.transform.position;

        Weapon = Instantiate(Weapon);
        Weapon.transform.parent = WeaponCenter;
        Weapon.GetComponent<Weapon>().Hand = this;

        Weapon.layer = gameObject.layer;

        Weapon.transform.localPosition = weaponPos;
    }

    private void Update()
    {
        Vector3 gg = WeaponCenter.rotation.eulerAngles;
        gg.z += rotAngle* angleMul;
        WeaponCenter.rotation = Quaternion.Euler(gg);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.ReloadScene();

            Destroy(gameObject);
        }

        HPText.text = health.ToString();

        UIManager.Instance.SlowTime();
    }

    public void Heal(int heal)
    {
        health += heal;

        HPText.text = health.ToString();
    }

    public void TurnOtherSide() =>  angleMul *= -1f;

    public void SetSTRText(string text)
    {
        if (isEnemy) UIManager.Instance.SetEnemyInfo = text;
        else UIManager.Instance.SetPlayerInfo = text;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Bubble"))
        {
            Reflect(collision);
        }
    }

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
