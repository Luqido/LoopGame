using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class PeopleDoTween : MonoBehaviour
{
    TrackMoveHandler tr;
    void Start()
    {
        tr = FindAnyObjectByType<TrackMoveHandler>();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public IEnumerator Sýralý()
    {
        
        
        yield return transform.DOMove(new Vector3(-4, 4, 0), 1).WaitForCompletion();
        Destroy(gameObject);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(0);


        

            
    }
    
    
}
