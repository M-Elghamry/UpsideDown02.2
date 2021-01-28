using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    private float Speed;
    private float JumpSpeed;
    private Vector2 Direction = Vector2.zero;
    private bool ShouldJump;

    public Rigidbody2D EnemyRB;

    private void FixedUpdate()
    {
        EnemyRB.velocity = new Vector2((Direction * Speed * Time.deltaTime).x, EnemyRB.velocity.y);
    }

    public void SetHorizontalMovement(Vector2 Direction, float Speed)
    {
        this.Speed = Speed;
        this.Direction = Direction;

        if(ShouldJump)
        {
            EnemyRB.AddForce(new Vector2(0, JumpSpeed * Time.deltaTime), ForceMode2D.Impulse);
            ShouldJump = false;
        }
    }

    public void Jump(float Speed)
    {
        ShouldJump = true;
        JumpSpeed = Speed;
    }
}
