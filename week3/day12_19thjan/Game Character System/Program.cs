using System;

class GameCharacter 
{
    public string Name { get; set; }
    public int Health { get; set; }

    public virtual void Attack()
    {
        Console.WriteLine(Name + " attacks!");
    }
}

class Warrior : GameCharacter
{
    public override void Attack()
    {
        Console.WriteLine(Name + " swings a sword!");
    }
}

class Mage : GameCharacter
{
    public override void Attack()
    {
        Console.WriteLine(Name + " casts a fireball!");
    }
}

class Archer : GameCharacter
{
    public override void Attack()
    {
        Console.WriteLine(Name + " shoots an arrow!");
    }
}

class Program
{
    static void Main()
    {
        GameCharacter c1 = new Warrior { Name = "Thor", Health = 100 };
        GameCharacter c2 = new Mage { Name = "Merlin", Health = 80 };
        GameCharacter c3 = new Archer { Name = "Robin", Health = 90 };

        c1.Attack();
        c2.Attack();
        c3.Attack();
    }
}
