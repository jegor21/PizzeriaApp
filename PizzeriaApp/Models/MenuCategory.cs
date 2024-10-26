using SQLite;

namespace PizzeriaApp.Models;

public class MenuCategory
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; }
    public string ImagePath { get; set; }
}
