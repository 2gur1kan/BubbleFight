using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI PlayerInfo;
    [SerializeField] private TextMeshProUGUI EnemyInfo;

    [SerializeField] private GameObject SelectPanel; 

    public string SetPlayerInfo { set => PlayerInfo.text = value; }
    public string SetEnemyInfo { set => EnemyInfo.text = value; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        SelectPanel.SetActive(true);
    }

    public void SetWeapon(int gg) => GameManager.Instance.StartGame((WeaponEnum)gg);

    public void CloseSelectPanel() => SelectPanel.SetActive(false);

    #region Time Sleep

    private float timer;
    private bool isReleasing = false;

    public void SlowTime()
    {
        Time.timeScale = 0.01f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        timer = .5f;

        if (!isReleasing)
            StartCoroutine(TimeReleaser());
    }

    private IEnumerator TimeReleaser()
    {
        yield return null;

        isReleasing = true;

        while (timer > 0)
        {
            Time.timeScale = Mathf.Lerp(0.01f, 1f, 1f - (timer / .5f));
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            yield return new WaitForSecondsRealtime(.05f);

            timer -= .05f;
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isReleasing = false;
    }

    #endregion
}
