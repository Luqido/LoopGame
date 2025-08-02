using System.Collections.Generic;
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
    [SerializeField] private List<Transform> nonFlippableObjects;
    private bool isFacingRight = true;


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
        FlipCharacter();

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
    void FlipCharacter()
    {
        if (moveImput.x > 0.01f && !isFacingRight)
        {
            Flip(true);
        }
        else if (moveImput.x < -0.01f && isFacingRight)
        {
            Flip(false);
        }
    }

    void Flip(bool faceRight)
    {
        isFacingRight = faceRight;
        float newScaleX = faceRight ? 1f : -1f;

        // Karakterin yönünü çevir
        transform.localScale = new Vector3(newScaleX, 1f, 1f);

        // Dönmemesi gereken objeleri ters çevirerek sabit tut
        foreach (Transform t in nonFlippableObjects)
        {
            Vector3 scale = t.localScale;
            scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            t.localScale = scale;
        }
    }
}
