using System.Collections;
using UnityEngine;

public class PetBird : Bird
{
    [SerializeField] private PlayerController m_player;
    private void Start()
    {
        StartCoroutine(FollowTargetCoroutine());
    }

    private IEnumerator FollowTargetCoroutine()
    {
        DisableAnimatorBools();
        birdAnimator.SetBool("Flying", true);
        while (true)
        {
            pathFollower.MoveTo(m_player.transform);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
