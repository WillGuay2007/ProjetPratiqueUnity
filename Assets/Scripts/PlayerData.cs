using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Grounded State")]
    public float groundFriction = 1f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public float jumpHoldMultiplier = 0.5f;
    public int amountOfJumps = 1;

    [Header("In Air state")]
    public float coyoteTime = 0.2f;
    public float airFrictionX = 2;

    [Header("Dash state")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.25f;
    public float regainDashAtDuration = 0.20f;
    public int amountOfAirDashes = 1;
    public float dashEndYMultiplier = 0.2f;
    public float HyperdashXVelocity = 40f;
    public float HyperdashYVelocity = 10f;
    public float SuperDashXVelocity = 25f;
    public float SuperDashYVelocity = 21f;

    [Header("Check variables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

}
