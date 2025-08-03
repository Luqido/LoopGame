using UnityEngine;
using UnityEngine.UI;

public enum StatType
{
    Health,
    Attack,
    Defense,
    Luck
}

public class BarControl : MonoBehaviour
{
    public Transform barContainer; // Image�lar�n parent'�
    public Button artiButonu;
    public Button eksiButonu;
    public StatType statType;
    private int aktifSayisi = 0;

    void Start()
    {
        artiButonu.onClick.AddListener(Artir);
        eksiButonu.onClick.AddListener(Azalt);

        LoadStatLevel();
        GuncelleButonlar();
    }
    void LoadStatLevel()
    {
        int level = GetStatLevel();

        // Clamp just in case
        level = Mathf.Clamp(level, 0, barContainer.childCount);

        aktifSayisi = level;

        for (int i = 0; i < barContainer.childCount; i++)
        {
            barContainer.GetChild(i).gameObject.SetActive(i < level);
        }
    }
    int GetStatLevel()
    {
        switch (statType)
        {
            case StatType.Health:
                return PlayerLevels.Instance.healthLevel;
            case StatType.Attack:
                return PlayerLevels.Instance.attackLevel;
            case StatType.Defense:
                return PlayerLevels.Instance.defenseLevel;
            case StatType.Luck:
                return PlayerLevels.Instance.luckLevel;
            default:
                return 0;
        }
    }


    void Artir()
    {
        if (SkillManager.Instance.SkillHarca())
        {
            foreach (Transform item in barContainer)
            {
                if (!item.gameObject.activeSelf)
                {
                    item.gameObject.SetActive(true);
                    aktifSayisi++;
                    IncreaseStat();
                    break;
                }
            }
        }

        GuncelleButonlar();
    }

    void Azalt()
    {
        for (int i = barContainer.childCount - 1; i >= 0; i--)
        {
            if (barContainer.GetChild(i).gameObject.activeSelf)
            {
                barContainer.GetChild(i).gameObject.SetActive(false);
                aktifSayisi--;
                SkillManager.Instance.SkillIadeEt();
                DecreaseStat();
                break;
            }
        }

        GuncelleButonlar();
    }

    void GuncelleButonlar()
    {
        bool artiAktif = SkillManager.Instance.KalanSkill() > 0 && aktifSayisi < barContainer.childCount;
        bool eksiAktif = aktifSayisi > 0;

        artiButonu.interactable = artiAktif;
        eksiButonu.interactable = eksiAktif;

        // G�rsel renk de�i�imi yapmak istersen:
        artiButonu.image.color = artiAktif ? Color.white : Color.red;
        eksiButonu.image.color = eksiAktif ? Color.white : Color.red;
    }
    void OnEnable()
    {
        SkillManager.OnSkillChanged += GuncelleButonlar;
    }
    void OnDisable()
    {
        SkillManager.OnSkillChanged -= GuncelleButonlar;
    }
    
    void IncreaseStat()
    {
        switch (statType)
        {
            case StatType.Health:
                PlayerLevels.Instance.healthLevel++;
                break;
            case StatType.Attack:
                PlayerLevels.Instance.attackLevel++;
                break;
            case StatType.Defense:
                PlayerLevels.Instance.defenseLevel++;
                break;
            case StatType.Luck:
                PlayerLevels.Instance.luckLevel++;
                break;
        }
    }

    void DecreaseStat()
    {
        switch (statType)
        {
            case StatType.Health:
                PlayerLevels.Instance.healthLevel--;
                break;
            case StatType.Attack:
                PlayerLevels.Instance.attackLevel--;
                break;
            case StatType.Defense:
                PlayerLevels.Instance.defenseLevel--;
                break;
            case StatType.Luck:
                PlayerLevels.Instance.luckLevel--;
                break;
        }
    }


}
