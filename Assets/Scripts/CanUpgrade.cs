using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanUpgrade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform canContainer; // "Can" objesini buraya sürükle
    public Button artýButonu;
    public Button eksiButonu;
    public int SkillPoint = 5;
    private int aktifCanSayisi = 0;
    [SerializeField]TextMeshProUGUI SkorText;
    

    void Start()
    {
        artýButonu.onClick.AddListener(CanArtir);
        eksiButonu.onClick.AddListener(CanAzalt);
        GuncelleUI();

    }

    void CanArtir()
    {
        if (SkillPoint <= 0) return;

        foreach (Transform can in canContainer)
        {
            if (!can.gameObject.activeSelf)
            {
                can.gameObject.SetActive(true);
                aktifCanSayisi++;
                SkillPoint--;
                break;
            }
        }
        GuncelleUI();
    }

    void CanAzalt()
    {
        // tersten döneriz, son aktif olaný bulmak için
        for (int i = canContainer.childCount - 1; i >= 0; i--)
        {
            if (canContainer.GetChild(i).gameObject.activeSelf)
            {
                canContainer.GetChild(i).gameObject.SetActive(false);
                aktifCanSayisi--;
                SkillPoint++;
                break;
            }
        }
        GuncelleUI();
    }
    void GuncelleUI()
    {
        // Skill puaný yazýsýný güncelle
        SkorText.text = "SKILL POINTS: " + SkillPoint;

        // Artý butonu: sadece puan varsa açýk olsun
        artýButonu.interactable = SkillPoint > 0;

        // Eksi butonu: en az 1 can aktifse açýk
        eksiButonu.interactable = aktifCanSayisi > 0;
    }

}
