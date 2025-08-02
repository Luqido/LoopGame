using UnityEngine;
using UnityEngine.SceneManagement;

public class Sahnegeçişi : MonoBehaviour
{
    TrackMoveHandler tr;
    private int triggersayisi = 0;
    public bool porno;
    [SerializeField]PeopleDoTween people;
    void Start()
    {
        tr = FindAnyObjectByType<TrackMoveHandler>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggersayisi++;
        if (triggersayisi == 2)
        {
            tr.SetPause(true);

            StartCoroutine(people.Sıralı());
        }

    }
    public void NextLevel()
    {
        int currentindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentindex + 1);

    }
}
