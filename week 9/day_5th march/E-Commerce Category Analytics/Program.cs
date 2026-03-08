using System;
using System.Collections.Generic;
using System.Linq;

interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
}

interface ICategory
{
    int Id { get; set; }
    string Name { get; set; }
    List<IProduct> Products { get; set; }
    void AddProduct(IProduct product);
}

interface ICompany
{
    string GetTopCategoryNameByProductCount();
    List<IProduct> GetProductsBelongsToMultipleCategory();
    (string categoryName, decimal totalValue) GetTopCategoryBySumOfProductPrices();
    List<(ICategory category, decimal totalValue)> GetCategoriesWithSumOfTheProductPrices();
}

class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

class Category : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<IProduct> Products { get; set; } = new List<IProduct>();

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddProduct(IProduct product)
    {
        Products.Add(product);
    }
}

class Company : ICompany
{
    List<ICategory> categories = new List<ICategory>();

    public void AddCategory(ICategory category)
    {
        categories.Add(category);
    }

    public string GetTopCategoryNameByProductCount()
    {
        return categories.OrderByDescending(c => c.Products.Count).First().Name;
    }

    public List<IProduct> GetProductsBelongsToMultipleCategory()
    {
        return categories.SelectMany(c => c.Products)
            .GroupBy(p => p.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.First())
            .ToList();
    }

    public (string categoryName, decimal totalValue) GetTopCategoryBySumOfProductPrices()
    {
        var result = categories
            .Select(c => new { c.Name, Total = c.Products.Sum(p => p.Price) })
            .OrderByDescending(x => x.Total)
            .First();

        return (result.Name, result.Total);
    }

    public List<(ICategory category, decimal totalValue)> GetCategoriesWithSumOfTheProductPrices()
    {
        return categories.Select(c => (c, c.Products.Sum(p => p.Price))).ToList();
    }
}

class Program
{
    static void Main()
    {
        Company company = new Company();

        Console.WriteLine("Enter number of products:");
        int n = Convert.ToInt32(Console.ReadLine());

        List<IProduct> products = new List<IProduct>();

        Console.WriteLine("Enter product data: id name price");

        for (int i = 0; i < n; i++)
        {
            var a = Console.ReadLine().Split(' ');
            products.Add(new Product(
                Convert.ToInt32(a[0]),
                a[1],
                Convert.ToDecimal(a[2])));
        }

        Console.WriteLine("Enter number of categories:");
        int m = Convert.ToInt32(Console.ReadLine());

        List<ICategory> categories = new List<ICategory>();

        Console.WriteLine("Enter category data: id name");

        for (int i = 0; i < m; i++)
        {
            var a = Console.ReadLine().Split(' ');
            categories.Add(new Category(
                Convert.ToInt32(a[0]),
                a[1]));
        }

        Console.WriteLine("Enter number of product-category relations:");
        int p = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter relation: categoryId productId");

        for (int i = 0; i < p; i++)
        {
            var a = Console.ReadLine().Split(' ');

            var c = categories.FirstOrDefault(x => x.Id == Convert.ToInt32(a[0]));
            var pr = products.FirstOrDefault(x => x.Id == Convert.ToInt32(a[1]));

            if (c != null && pr != null)
                c.AddProduct(pr);
        }

        foreach (var c in categories)
            company.AddCategory(c);

        Console.WriteLine("Top Category: " + company.GetTopCategoryNameByProductCount());

        Console.WriteLine("Products in multiple categories:");
        foreach (var p1 in company.GetProductsBelongsToMultipleCategory())
            Console.WriteLine(p1.Name);

        var top = company.GetTopCategoryBySumOfProductPrices();
        Console.WriteLine("Most Valuable Category: " + top.categoryName + " " + top.totalValue);

        Console.WriteLine("Category Total Prices:");
        foreach (var item in company.GetCategoriesWithSumOfTheProductPrices())
            Console.WriteLine(item.category.Name + " " + item.totalValue);
    }
}