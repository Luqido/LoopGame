using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveImput;
    Rigidbody2D playerRb;
    [SerializeField] float RunSpeed = 2f;
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
        Vector2 playerVelocity = new Vector2(moveImput.x * RunSpeed, playerRb.linearVelocity.y);
        playerRb.linearVelocity = playerVelocity;

        





    }
}
