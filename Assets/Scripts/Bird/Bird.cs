using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    protected PathFollower pathFollower;
    protected Animator birdAnimator;
    protected SpriteRenderer birdRenderer;

    private void Awake()
    {
        pathFollower = GetComponent<PathFollower>();
        birdAnimator = GetComponent<Animator>();
        birdRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        FaceMovingDirection();
    }

    protected void DisableAnimatorBools()
    {
        foreach (AnimatorControllerParameter parameter in birdAnimator.parameters)
        {
            birdAnimator.SetBool(parameter.name, false);
        }
    }

    private void FaceMovingDirection()
    {
        float moveX = pathFollower.delta.x;
        birdRenderer.flipX = moveX > 0;
    }
}
