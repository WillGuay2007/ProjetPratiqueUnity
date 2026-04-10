using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StandingBird : MonoBehaviour
{
    [SerializeField] private float m_birdSpeed;
    private float m_minimumXForDirection = 0.45f;
    private float m_minimumYForDirection = 0.45f;
    private float m_birdDespawnTime = 5f;
    private int m_birdFacingDirection;
    private Animator m_birdAnimator;
    private SpriteRenderer m_birdRenderer;
    private Rigidbody2D m_birdRigidBody2D;
    private Coroutine activeCoroutine = null;

    private void Awake()
    {
        m_birdRenderer = GetComponent<SpriteRenderer>();
        m_birdAnimator = GetComponent<Animator>();
        m_birdRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (activeCoroutine != null || player == null) return;
        activeCoroutine = StartCoroutine(FlyAway(player.facingXDirection));
    }

    private void DisableAnimatorBools()
    {
        foreach (AnimatorControllerParameter parameter in m_birdAnimator.parameters)
        {
            m_birdAnimator.SetBool(parameter.name, false);
        }
    }

    private IEnumerator FlyAway(float playerFacingDirection)
    {
        DisableAnimatorBools();
        AudioController.Instance.PlaySound(AudioController.Sounds.BirdFlyAway);
        m_birdAnimator.SetBool("Flying", true);

        Vector2 chosenVelocity = new Vector2(
            Random.Range(m_minimumXForDirection, 1f),
            Random.Range(m_minimumYForDirection, 1f)
            );
        bool isFlyingTowardPlayer = Mathf.Sign(chosenVelocity.x) != playerFacingDirection;

        if (chosenVelocity.y < 0) chosenVelocity.y *= -1; //Je veut pas qu'il vole vers le bas.
        if (isFlyingTowardPlayer) chosenVelocity.x *= -1f; //Pour qu'il s'enfuie du joueur et pas aller vers lui 
        if (playerFacingDirection == 1) m_birdRenderer.flipX = true;

        float zAngle = Mathf.Atan(chosenVelocity.y / chosenVelocity.x) * Mathf.Rad2Deg;
        transform.Rotate(0f, 0f, zAngle);

        m_birdRigidBody2D.linearVelocity = chosenVelocity * m_birdSpeed;

        yield return new WaitForSeconds(m_birdDespawnTime);
        Destroy(gameObject);
    }
}
