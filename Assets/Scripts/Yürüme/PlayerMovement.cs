using System.Collections.Generic;
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
    private bool isFacingRight = true;

    public CameraZoom cameraZoom;

    private Animator animator;
    private bool isWalking = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator component'i al�n�yor
    }

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
        Vector2 playerVelocity = new Vector2(moveImput.x * RunSpeed, playerRb.linearVelocity.y);
        playerRb.linearVelocity = playerVelocity;
        FlipCharacter();

        // Y�r�me kontrol�
        bool walkingNow = Mathf.Abs(playerVelocity.x) > 0.1f;
        if (walkingNow)
        {
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

        // isWalking ve animasyon kontrol�
        if (isWalking != walkingNow)
        {
            isWalking = walkingNow;
            animator.SetBool("isWalking", isWalking); // Animator parametresi g�ncelleniyor
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

        transform.localScale = new Vector3(newScaleX, 1f, 1f);

        foreach (Transform t in nonFlippableObjects)
        {
            Vector3 scale = t.localScale;
            scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            t.localScale = scale;
        }
    }
}
