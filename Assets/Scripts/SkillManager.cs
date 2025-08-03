using UnityEngine;
using TMPro;

public class SkillManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SkillManager Instance;

    public int skillPoint = 15;
    public TextMeshProUGUI skillPointText;
    public static event System.Action OnSkillChanged;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GuncelleText();
    }

    public bool SkillHarca()
    {
        if (skillPoint > 0)
        {
            skillPoint--;
            GuncelleText();
            return true;
        }

        return false;
    }

    public void SkillIadeEt()
    {
        skillPoint++;
        GuncelleText();
    }

    

    void GuncelleText()
    {
        skillPointText.text = "SKILL POINTS: " + skillPoint;

        // Barlara haber ver
        OnSkillChanged?.Invoke();
    }

    public int KalanSkill()
    {
        return skillPoint;
    }

}
