using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponDBManager : MonoBehaviour
{
    public static WeaponDBManager Instance;

    public WeaponDB DB;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public GameObject getWeaponGO(WeaponEnum weapon) => DB.weaponList.Find(gg => gg.Name == weapon)?.GO;

    public WeaponEnum getRandomWeaponName()
    {
        Array values = Enum.GetValues(typeof(WeaponEnum));
        return (WeaponEnum)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}
