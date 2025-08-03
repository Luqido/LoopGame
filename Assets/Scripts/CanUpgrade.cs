using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanUpgrade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform canContainer; // "Can" objesini buraya s�r�kle
    public Button artiButonu;
    public Button eksiButonu;
    public int SkillPoint = 5;
    private int aktifCanSayisi = 0;
    [SerializeField]TextMeshProUGUI SkorText;
    

    void Start()
    {
        artiButonu.onClick.AddListener(CanArtir);
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
        // tersten d�neriz, son aktif olan� bulmak i�in
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
        // Skill puan� yaz�s�n� g�ncelle
        SkorText.text = "SKILL POINTS: " + SkillPoint;

        // Art� butonu: sadece puan varsa a��k olsun
        artiButonu.interactable = SkillPoint > 0;

        // Eksi butonu: en az 1 can aktifse a��k
        eksiButonu.interactable = aktifCanSayisi > 0;
    }

}
