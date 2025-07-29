using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDB", menuName = "Create Weapon DB")]
public class WeaponDB : ScriptableObject
{
    public List<WeaponValue> weaponList;
}

[System.Serializable]
public class WeaponValue
{
    public WeaponEnum Name;
    public GameObject GO;
}