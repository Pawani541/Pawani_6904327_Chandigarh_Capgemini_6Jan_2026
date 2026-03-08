using System;

class BankAccount
{
    public int AccountNo 
    {
        get; 
        private set;
    }
    public string HolderName
    {
        get;
        private set;
    }
    protected double Balance;

    public BankAccount(int accNo, string name, double balance)
    {
        AccountNo = accNo;
        HolderName = name;
        Balance = balance;
    }

    public void Deposit(double amount)
    {
        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= Balance)
            Balance -= amount;
        else
            Console.WriteLine("Insufficient balance");
    }

    public virtual void Display()
    {
        Console.WriteLine($"Account: {AccountNo}, Name: {HolderName}, Balance: {Balance}");
    }
}

class SavingsAccount : BankAccount
{
    public SavingsAccount(int accNo, string name, double balance)
        : base(accNo, name, balance) { }

    public void AddInterest()
    {
        Balance += Balance * 0.04;
    }
}

class CheckingAccount : BankAccount
{
    public CheckingAccount(int accNo, string name, double balance)
        : base(accNo, name, balance) { }
}

class Program
{
    static void Main()
    {
        SavingsAccount sa = new SavingsAccount(101, "Aarav", 10000);
        sa.AddInterest();
        sa.Display();
    }
}
