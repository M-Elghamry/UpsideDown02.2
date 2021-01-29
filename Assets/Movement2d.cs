using UnityEngine;


public class Movement2d : MonoBehaviour
{
    [Header("Necessary References")]
    public Rigidbody2D PlayerRigidbody2D;
    public Transform PlayerTransform;


    [Header("Movement Parameters")]
    [Range(0f, 1000f)]
    public float MovementSpeed = 200f;

    [Range(0f, 1000f)]
    public float JumpSpeed = 25f;

    public bool EnableCrouching;

    [Range(0f, 1f), Tooltip("The number that the MovementSpeed and JumpSpeed variables are being multiplied with while crouching")]
    public float CrouchSpeedRatio = 0.5f;

    [Header("Better Jumping"), Tooltip("Adds more game-like jumping instead of a physically accurate one. Original code by \"Board To Bits Games\" on YT")]
    public bool BetterJumping;
    [Range(0f, 10f),Tooltip("the value used to determine the speed of the player while falling")]
    public float FallMultiplier = 1.5f;
    [Range(0f, 10f), Tooltip("the value used to determine the Speed for low jumps")]
    public float LowJumpMultiplier = 1f;

    private float OriginalMovementSpeed;
    private float CrouchMovementSpeed;

    private float Direction;
    private bool isMoving = false;
    private bool isJumping = false;
    private bool isCrouching = false;

    private void Awake()
    {
        CrouchMovementSpeed = MovementSpeed * CrouchSpeedRatio;

        OriginalMovementSpeed = MovementSpeed;
        if (PlayerRigidbody2D == null || PlayerTransform == null)
        {
            PlayerRigidbody2D = GetComponent<Rigidbody2D>();
            PlayerTransform = GetComponent<Transform>();
        }

        if (PlayerRigidbody2D == null || PlayerTransform == null)
            throw new MissingReferenceException("Please Add all necessary references to the Movement2d script manually. The program wasnt able to find them automatically");
    }

    private void FixedUpdate()
    {

        if(EnableCrouching)
        {
            if (isCrouching)
            {
                MovementSpeed = CrouchMovementSpeed;
            }
            else
            {
                MovementSpeed = OriginalMovementSpeed;
            }
        }

        if (isMoving)
        {
            PlayerRigidbody2D.velocity = new Vector2(Direction * MovementSpeed * Time.deltaTime, PlayerRigidbody2D.velocity.y);
        }

        if (isJumping && !isCrouching)
        {
            if (Mathf.Abs(PlayerRigidbody2D.velocity.y) < 0.01f)
            {
                PlayerRigidbody2D.AddForce(new Vector2(0, JumpSpeed * Time.deltaTime), ForceMode2D.Impulse);
            }
        }

        if (BetterJumping)
        {
            if (PlayerRigidbody2D.velocity.y < 0)
            {
                PlayerRigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            }
            else if (PlayerRigidbody2D.velocity.y > 0 && !isJumping)
            {
                PlayerRigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        isMoving = false;
        isJumping = false;
        isCrouching = false;
    }

    public void MoveHorizontally(float Direction)
    {
        this.Direction = Direction;
        isMoving = true;
    }

    public void MoveHorizontally(float DirectionX, float DirectionY)
    {
        MoveHorizontally(DirectionX);
    }

    public void Jump()
    {
        isJumping = true;
    }
    public void Crouch()
    {
        isCrouching = true;
    }
}
 