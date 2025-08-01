using UnityEngine;
using Unity.Cinemachine;

public class TrainShake : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource impulseSource;
    [SerializeField] float shakeInterval = 5f;
    float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>= shakeInterval)
        {
            impulseSource.GenerateImpulse();
            timer = 0;
        }
    }
}
