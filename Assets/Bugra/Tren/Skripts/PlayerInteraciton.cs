using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private bool isInTrigger = false;
    public Transform currentTarget { get; private set; }

    public InteractionManager interactionManager;

    // Hangi tag'lerle etkile�ime girilece�ini buradan ayarlayabilirsin
    private readonly string[] interactableTags = { "NPC", "Door" };

    void Update()
    {
       
    }
    void OnEtkilesim(InputValue value)
    {
        if (isInTrigger  && currentTarget != null)
        {
            interactionManager.InteractWithObject(currentTarget);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string tag in interactableTags)
        {
            if (other.CompareTag(tag))
            {
                currentTarget = other.transform;
                isInTrigger = true;
                Debug.Log($"{tag} ile etkile�im alan�na girildi.");
                break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (string tag in interactableTags)
        {
            if (other.CompareTag(tag))
            {
                isInTrigger = false;
                currentTarget = null;
                Debug.Log($"{tag} etkile�im alan�ndan ��k�ld�.");
                break;
            }
        }
    }
}
