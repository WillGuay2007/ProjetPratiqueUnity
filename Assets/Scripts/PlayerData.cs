using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public float jumpHoldMultiplier = 0.5f;
    public int amountOfJumps = 1;

    [Header("In Air state")]
    public float coyoteTime = 0.2f;

    [Header("Check variables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

}
