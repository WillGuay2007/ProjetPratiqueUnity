using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{

    private bool isClaimed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isClaimed) return;
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            ClaimCheckpoint();
            player.ClaimCheckpoint(transform.position);
        }
    }

    private void ClaimCheckpoint()
    {
        isClaimed = true;
        transform.Rotate(0f, 180f, 0f); 
    }

}
