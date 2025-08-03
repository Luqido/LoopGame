using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    public Transform barContainer; // Image’larýn parent'ý
    public Button artýButonu;
    public Button eksiButonu;

    private int aktifSayisi = 0;

    void Start()
    {
        artýButonu.onClick.AddListener(Artir);
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

        artýButonu.interactable = artiAktif;
        eksiButonu.interactable = eksiAktif;

        // Görsel renk deðiþimi yapmak istersen:
        artýButonu.image.color = artiAktif ? Color.white : Color.red;
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
