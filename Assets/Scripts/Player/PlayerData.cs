using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Player health")]
    public float playerHealth = 100f;

    [Header("Move State")]
    public float movementVelocity = 8f;

    [Header("Grounded State")]
    public float groundFriction = 90f;

    [Header("Jump State")]
    public float jumpVelocity = 16f;
    public float jumpHoldMultiplier = 0.5f;
    public int amountOfJumps = 1;

    [Header("In Air state")]
    public float coyoteTime = 0.2f;
    public float airFrictionX = 60f;

    [Header("Dash state")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.25f;
    public float regainDashAtDuration = 0.1f;
    public float diagonalDownDashYMultiplier= 0.5f;
    public float verticalDashYMultiplier = 0.75f;
    public float diagonalUpDashYMultiplier = 0.85f;
    public int maxAmountOfDashes = 1;
    public float dashEndYMultiplier = 0.7f;
    public float HyperdashXVelocity = 40f;
    public float HyperdashYVelocity = 13f;
    public float SuperDashXVelocity = 30f;
    public float SuperDashYVelocity = 21f;

    [Header("Wall Slide state")]
    public float maxWallSlideVelocity = 25f;
    public float wallSlideFriction = 21f;
    public float wallBounceXVelocity = 25f;

    [Header("Wall Climb state")]
    public float wallClimbSpeed = 5f;

    [Header("Grab + Climb state")]
    public float GrabInputDeactivationDelayAfterBounce = 0.2f;

    [Header("Climb state")]
    public float ClimbDownXBoost = 30f;
    public float ClimbDownFaceAwayXBoostMultiplier = 1.5f;

    [Header("Check variables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public float wallCheckRayLength = 0.7f;

}
