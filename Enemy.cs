using System;

class Enemy
{
    public string Name;
    public int Health = 50;
    public float Speed = 2.0f;
    public float PositionX = 0;
    public float PositionY = 0;

    public Enemy(string name)
    {
        Name = name;
    }

    public void Update()
    {
        Console.WriteLine($"{Name} is idle...");
    }

    // when enemy gets hit
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name} took {damage} damage! Health: {Health}");
        
        if(Health <= 0)
        {
            Console.WriteLine($"{Name} died!");
        }
    }
}
