using UnityEngine;

public class PlayerDirectionController : MonoBehaviour
{
    SpriteRenderer PlayerSpriteRenderer;

    private void Awake()
    {
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int facingXDirection { get; private set; } = 1;
    public int facingYDirection { get; private set; } = 1;

    public void ShouldFlipX(int xInput)
    {
        if (xInput != 0 && xInput != facingXDirection)
        {
            FlipX();
        }
    }
    public int FlipX()
    {
        facingXDirection *= -1;
        PlayerSpriteRenderer.flipX = !PlayerSpriteRenderer.flipX;
        return facingXDirection;
    }

    public int FlipY()
    {
        facingYDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
        transform.Rotate(0f, 0f, 180f);
        return facingYDirection;
    }
}
