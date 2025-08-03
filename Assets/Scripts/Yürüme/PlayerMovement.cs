using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveImput;
    Rigidbody2D playerRb;

    [SerializeField] float RunSpeed = 2f;
    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private float footstepCooldown = 0.4f;
    private float footstepTimer;

    [SerializeField] private List<Transform> nonFlippableObjects;
    private bool isFacingRight;

    public CameraZoom cameraZoom;

    private Animator animator;
    private bool isWalking = false;
    public bool canMove = true;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Başlangıç yönünü localScale'den belirle
        isFacingRight = transform.localScale.x >= 0f;
    }

    void Update()
    {
        if (!canMove)
        {
            // Hareketi sıfırla (gerekirse)
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            return;
        }
        Run();
    }

    void OnMove(InputValue value)
    {
        moveImput = value.Get<Vector2>();
        Debug.Log(moveImput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveImput.x * RunSpeed, playerRb.linearVelocity.y);
        playerRb.linearVelocity = playerVelocity;

        // Yürüme kontrolü
        bool walkingNow = Mathf.Abs(playerVelocity.x) > 0.1f;
        if (walkingNow)
        {
            FlipCharacter();

            cameraZoom.ZoomTo(8.15f);
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                if (!footstepAudio.isPlaying)
                    footstepAudio.Play();

                footstepTimer = footstepCooldown;
            }
        }
        else
        {
            footstepTimer = 0f;
            cameraZoom.ZoomTo(7f);
        }

        // Animasyon parametresi güncelleme
        if (isWalking != walkingNow)
        {
            isWalking = walkingNow;
            animator.SetBool("isWalking", isWalking);
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

        // Ana karakteri çevir
        transform.localScale = new Vector3(newScaleX, 1f, 1f);

        // Dönmemesi gereken objeleri tersleyerek sabit tut
        foreach (Transform t in nonFlippableObjects)
        {
            Vector3 scale = t.localScale;
            scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            t.localScale = scale;
        }

        Debug.Log("Flip çalıştı → " + (faceRight ? "Sağ" : "Sol"));
    }
}
