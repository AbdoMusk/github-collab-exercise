using System;

class Player
{
    public int Health = 100;
    public float Speed = 5.0f;
    public float JumpForce = 10.0f;
    public float PositionX = 0;
    public float PositionY = 0;
    public bool IsGrounded = true;

    // moves the player left or right
    public void Move(float direction)
    {
        PositionX += direction * Speed;
        Console.WriteLine($"Player moved to X: {PositionX}");
    }

    // makes player jump
    public void Jump()
    {
        if(IsGrounded)
        {
            IsGrounded = false;
            PositionY += JumpForce;
            Console.WriteLine("Player jumped!");
        }
    }

    public void PlayJumpAnimation()
    {
        Console.WriteLine("*jump*");
    }

    // when player takes damage
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"Ouch! Player took {damage} damage! Health: {Health}");
        
        if(Health <= 0)
        {
            Console.WriteLine("Player died! Game over");
        }
    }
}
