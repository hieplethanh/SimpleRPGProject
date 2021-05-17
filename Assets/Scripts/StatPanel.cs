using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    public GameObject player;
    Player playerStats;
    public int Points;
    int PointPerLevel = 3;
    public Text textDmg, textDef, textHp, textPoints;
    public Button btnDmg, btnDef, btnHp;
    public GameObject Panel;
    // Start is called before the first frame update
    void Awake()
    {
        playerStats = player.GetComponent<Player>();
        Points = 0;
        Panel.SetActive(false);

        btnDmg.onClick.AddListener(onDmbBtnClicked);
        btnDef.onClick.AddListener(onDefBtnClicked);
        btnHp.onClick.AddListener(onHPBtnclicked);

        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (Panel.activeSelf)
            UpdateStats();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Panel.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Panel.gameObject.SetActive(!Panel.gameObject.activeSelf);
        }

    }

    public void OnPlayerLevelChange()
    {
        Points += PointPerLevel;
        UpdateStats();
    }

    void UpdateStats()
    {
        if (playerStats.characterStats == null)
            { return; }
        textDmg.text = playerStats.characterStats.GetStat(BaseStat.BaseStatType.Power).BaseValue.ToString();
        textDef.text = playerStats.characterStats.GetStat(BaseStat.BaseStatType.Toughness).BaseValue.ToString();
        textHp.text = playerStats.maxHealth.ToString();
        textPoints.text = Points.ToString();

        if (Points<=0)
        {
            SetButtonsState(false);
        }
        else
        {
            SetButtonsState(true);
        }
    }

    void SetButtonsState(bool val)
    {
        btnDmg.gameObject.SetActive(val);
        btnDef.gameObject.SetActive(val);
        btnHp.gameObject.SetActive(val);
    }

    void onDmbBtnClicked()
    {
        Points -= 1;
        playerStats.characterStats.GetStat(BaseStat.BaseStatType.Power).BaseValue += 1;
        UpdateStats();
    }

    void onDefBtnClicked()
    {
        Points -= 1;
        playerStats.characterStats.GetStat(BaseStat.BaseStatType.Toughness).BaseValue += 1;
        UpdateStats();
    }

    void onHPBtnclicked()
    {
        Points -= 1;
        playerStats.maxHealth += 5;
        UpdateStats();
    }
}
