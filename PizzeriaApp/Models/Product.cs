using SQLite;

namespace PizzeriaApp.Models;

public class Product
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int MenuCategoryId { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; } 
}
