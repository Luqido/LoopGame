using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveImput;
    Rigidbody2D playerRb;
    [SerializeField] float RunSpeed = 2f;
    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private float footstepCooldown = 0.4f; // adýmlar arasý süre
    private float footstepTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }
    void OnMove(InputValue value)
    {
        moveImput = value.Get<Vector2>();
        Debug.Log(moveImput);

    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveImput.x * RunSpeed, playerRb.velocity.y);
        playerRb.velocity = playerVelocity;

        // Hareket varsa ve adým sesi zamaný geldiyse çal
        if (Mathf.Abs(playerVelocity.x) > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                if (!footstepAudio.isPlaying)
                {
                    footstepAudio.Play();
                }
                footstepTimer = footstepCooldown;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

}
