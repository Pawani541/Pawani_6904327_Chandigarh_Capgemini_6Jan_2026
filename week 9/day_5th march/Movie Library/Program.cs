using System;
using System.Collections.Generic;
using System.Linq;

class Movie
{
    public string Title;
    public string Genre;
    public int Year;

    public Movie(string t, string g, int y)
    {
        Title = t;
        Genre = g;
        Year = y;
    }
}

class MovieLibrary
{
    List<Movie> movies = new List<Movie>();

    public void AddMovie(Movie m)
    {
        movies.Add(m);
    }

    public void ShowMovies()
    {
        foreach (var m in movies)
        {
            Console.WriteLine(m.Title + " " + m.Genre + " " + m.Year);
        }
    }
}

class Program
{
    static void Main()
    {
        MovieLibrary lib = new MovieLibrary();

        Console.WriteLine("Enter number of movies:");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter Title Genre Year:");
            var a = Console.ReadLine().Split(' ');

            lib.AddMovie(new Movie(a[0], a[1], int.Parse(a[2])));
        }

        Console.WriteLine("Movie List:");
        lib.ShowMovies();
    }
}