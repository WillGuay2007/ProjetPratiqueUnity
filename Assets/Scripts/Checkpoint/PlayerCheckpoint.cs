using Unity.VisualScripting;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] private int m_checkpointIndex;
    public Vector2 Position => transform.position;
    public int CheckpointIndex => m_checkpointIndex;
    public bool IsClaimed { get; private set; }

    public void Claim()
    {
        if (IsClaimed)
        {
            print("Checkpoint cannot be claimed because it has already been claimed.");
            return;
        }
        IsClaimed = true;
        PlayClaimedAnimation();
    }
    private void PlayClaimedAnimation() => transform.Rotate(0f, 180f, 0f);
}
