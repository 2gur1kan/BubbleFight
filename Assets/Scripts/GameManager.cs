using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Enemy;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void StartGame(WeaponEnum gg)
    {
        UIManager.Instance.CloseSelectPanel();

        Player.SetActive(true);
        Player.GetComponent<Bubble>().SetWeapon = gg;

        Enemy.SetActive(true);

        UIManager.Instance.SlowTime();
    }

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
