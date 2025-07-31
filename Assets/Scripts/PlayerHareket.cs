using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHareket : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float runSpeed = 5f;
    Rigidbody2D hamRigidBody;
    void Start()
    {
        hamRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        flipSprite();
    }
     void OnMove(InputValue Value)
    {
        moveInput = Value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void Run()
    {
        Vector2 isoDirection = new Vector2(moveInput.x - moveInput.y, moveInput.x + moveInput.y/2f );
        hamRigidBody.linearVelocity = isoDirection.normalized * runSpeed;
    }
     
    void flipSprite()
    {
        bool donme = Mathf.Abs(moveInput.x - moveInput.y) > Mathf.Epsilon;
        if (donme)
        {
            transform.localScale = new Vector2(-Mathf.Sign(moveInput.x - moveInput.y), 1f);
        }
        
    }

}
