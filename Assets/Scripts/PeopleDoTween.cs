using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PeopleDoTween : MonoBehaviour
{   

    void Start()
    {
        StartCoroutine(S�ral�());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator S�ral�()
    {
        yield return transform.DOMove(new Vector3(-4, 4, 0), 1).WaitForCompletion();
        Destroy(gameObject);
        

            
    }
    
    
}
