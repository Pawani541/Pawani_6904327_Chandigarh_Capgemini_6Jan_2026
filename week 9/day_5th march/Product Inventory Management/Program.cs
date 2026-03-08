using System;
using System.Collections.Generic;
using System.Linq;

public interface IProduct
{
    string Name { get; set; }
    string Category { get; set; }
    int Stock { get; set; }
    int Price { get; set; }
}

public interface IInventory
{
    void AddProduct(IProduct product);
    void RemoveProduct(IProduct product);
    int CalculateTotalValue();
    List<IProduct> GetProductsByCategory(string category);
    List<IProduct> SearchProductsByName(string name);
    List<(string, int)> GetProductsByCategoryWithCount();
    List<(string, List<IProduct>)> GetAllProductsByCategory();
}

public class Product : IProduct
{
    public string Name { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}

public class Inventory : IInventory
{
    private List<IProduct> products = new List<IProduct>();

    public void AddProduct(IProduct product)
    {
        products.Add(product);
    }

    public void RemoveProduct(IProduct product)
    {
        products.Remove(product);
    }

    public int CalculateTotalValue()
    {
        return products.Sum(p => p.Price * p.Stock);
    }

    public List<IProduct> GetProductsByCategory(string category)
    {
        return products.Where(p => p.Category == category).ToList();
    }

    public List<IProduct> SearchProductsByName(string name)
    {
        return products.Where(p => p.Name.Contains(name)).ToList();
    }

    public List<(string, int)> GetProductsByCategoryWithCount()
    {
        return products
            .GroupBy(p => p.Category)
            .Select(g => (g.Key, g.Count()))
            .ToList();
    }

    public List<(string, List<IProduct>)> GetAllProductsByCategory()
    {
        return products
            .GroupBy(p => p.Category)
            .Select(g => (g.Key, g.ToList()))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        IInventory inventory = new Inventory();

        Product p1 = new Product { Name = "Laptop", Category = "Electronics", Stock = 5, Price = 1000 };
        Product p2 = new Product { Name = "Phone", Category = "Electronics", Stock = 10, Price = 500 };
        Product p3 = new Product { Name = "Chair", Category = "Furniture", Stock = 3, Price = 200 };

        inventory.AddProduct(p1);
        inventory.AddProduct(p2);
        inventory.AddProduct(p3);

        Console.WriteLine("Total Value: " + inventory.CalculateTotalValue());

        Console.WriteLine("Electronics Products:");
        foreach (var p in inventory.GetProductsByCategory("Electronics"))
        {
            Console.WriteLine(p.Name);
        }

        Console.WriteLine("Search 'Phone':");
        foreach (var p in inventory.SearchProductsByName("Phone"))
        {
            Console.WriteLine(p.Name);
        }
    }
}