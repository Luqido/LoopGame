using UnityEngine;

public class InteractionX : MonoBehaviour
{
    public float interactionDistance = 3f;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Baþta görünmesin
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.parent.position); // X’in baðlý olduðu obje
        if (distance <= interactionDistance)
        {
            spriteRenderer.enabled = true;
            Color color = spriteRenderer.color;
            color.a = Mathf.Clamp01(1 - (distance / interactionDistance));
            spriteRenderer.color = color;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
