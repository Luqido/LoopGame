using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PeopleDoTween : MonoBehaviour
{
    [SerializeField] GameObject[] Sekil;
    TrackMoveHandler tr;
    [SerializeField] private GameObject popupPanel;
    void Start()
    {
        tr = FindAnyObjectByType<TrackMoveHandler>();
        foreach (GameObject sekil in Sekil)
        {
            sekil.SetActive(false);
        }
        List<GameObject> havuz = new List<GameObject>(Sekil);
        int adet = Mathf.Min(2, havuz.Count);

        for (int i = 0; i < adet; i++)
        {
            int randomIndex = Random.Range(0, havuz.Count);
            havuz[randomIndex].SetActive(true);
            havuz.RemoveAt(randomIndex);
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public IEnumerator Sýralý()
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

        


        // Bekle ve sahneyi yeniden yükle
        yield return new WaitForSecondsRealtime(1f);
        RectTransform popupRect = popupPanel.GetComponent<RectTransform>();
        popupRect.anchoredPosition = new Vector2(0, -400); // ekranýn altý gibi
        popupPanel.SetActive(true);

        // Yukarý kaydýrarak göster (0, 0 pozisyonuna gitsin)
        yield return popupRect.DOAnchorPosY(0, 1.5f).SetEase(Ease.OutExpo).WaitForCompletion();







    }
    
    
}
