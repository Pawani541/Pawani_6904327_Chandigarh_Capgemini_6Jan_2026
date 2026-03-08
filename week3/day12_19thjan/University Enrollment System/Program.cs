using System;

class UniversityPerson   
{
    public string Name
    {
        get;
        protected set;
    }
    public int Id 
    {
        get;
        protected set; 
    }

    public UniversityPerson(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public virtual void Display()
    {
        Console.WriteLine($"{Id} - {Name}");
    }
}

class Student : UniversityPerson
{
    public string Course { get; set; }

    public Student(string name, int id, string course)
        : base(name, id)
    {
        Course = course;
    }
}

class Professor : UniversityPerson
{
    public string Subject
    {
        get;
        set;
    }

    public Professor(string name, int id, string subject)
        : base(name, id)
    {
        Subject = subject;
    }
}

class Staff : UniversityPerson
{
    public string Department { get; set; }

    public Staff(string name, int id, string dept)
        : base(name, id)
    {
        Department = dept;
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student("Aarav", 101, "B.Tech");
        Professor p = new Professor("Dr. Sharma", 201, "Maths");

        s.Display();
        p.Display();
    }
}
