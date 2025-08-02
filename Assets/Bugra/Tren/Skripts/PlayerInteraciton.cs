using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isInTrigger = false;
    private Transform currentTarget;

    public InteractionManager interactionManager;

    // Hangi tag'lerle etkile�ime girilece�ini buradan ayarlayabilirsin
    private readonly string[] interactableTags = { "NPC", "Door" };

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.X) && currentTarget != null)
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
