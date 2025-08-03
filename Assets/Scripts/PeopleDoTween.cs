using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PeopleDoTween : MonoBehaviour
{
    [SerializeField] GameObject[] Sekil;
    [SerializeField] private List<EnemyType> SekildenTypeaGecisSirasi;

    
    TrackMoveHandler tr;
    [SerializeField] private GameObject popupPanel;
    public static EnemyType LastBeatenEnemy;
    void Start()
    {
        tr = FindAnyObjectByType<TrackMoveHandler>();
        foreach (GameObject sekil in Sekil)
        {
            sekil.SetActive(false);
        }
        List<GameObject> havuz = new List<GameObject>(Sekil);
        
        if(LastBeatenEnemy != 0)
        {
            havuz.RemoveAt(SekildenTypeaGecisSirasi.IndexOf(LastBeatenEnemy));
        }
        int adet = Mathf.Min(2, havuz.Count);
        var sekilList = Sekil.ToList();

        List<EnemyType> enemyTypes = new();
        for (int i = 0; i < adet; i++)
        {
            int randomIndex = Random.Range(0, havuz.Count);
            havuz[randomIndex].SetActive(true);
            var index = sekilList.IndexOf(havuz[randomIndex]);
            enemyTypes.Add(SekildenTypeaGecisSirasi[index]);
            havuz.RemoveAt(randomIndex);
        }
     
        //TODO bugra duzeltecek
        // CombatManagerTester.enemies = enemyTypes;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public IEnumerator Sirali()
    {

        List<GameObject> aktifSekiller = new List<GameObject>();
        
        foreach (GameObject sekil in Sekil)
        {
            if (sekil.activeSelf)
            {
                aktifSekiller.Add(sekil);

            }
        }

        foreach (GameObject sekil in aktifSekiller)
        {
            yield return sekil.transform.DOMove(sekil.transform.position + new Vector3(0, 4, 0), 1f).WaitForCompletion();
           
        }

        yield return new WaitForSeconds(1f);

        foreach (GameObject sekil in aktifSekiller)
        {
            Destroy(sekil);
        }

        


        // Bekle ve sahneyi yeniden y�kle
        yield return new WaitForSecondsRealtime(1f);
        RectTransform popupRect = popupPanel.GetComponent<RectTransform>();
        popupRect.anchoredPosition = new Vector2(0, -400); // ekran�n alt� gibi
        popupPanel.SetActive(true);

        // Yukar� kayd�rarak g�ster (0, 0 pozisyonuna gitsin)
        yield return popupRect.DOAnchorPosY(0, 1.5f).SetEase(Ease.OutExpo).WaitForCompletion();







    }
    
    
}
