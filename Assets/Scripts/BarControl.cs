using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    public Transform barContainer; // Image�lar�n parent'�
    public Button artiButonu;
    public Button eksiButonu;

    private int aktifSayisi = 0;

    void Start()
    {
        artiButonu.onClick.AddListener(Artir);
        eksiButonu.onClick.AddListener(Azalt);

        GuncelleButonlar();
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

}
