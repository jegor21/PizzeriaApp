using SQLite;
using PizzeriaApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzeriaApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            InitializeDatabase();
        }

        private async Task InitializeDatabase()
        {
            await _database.CreateTableAsync<MenuCategory>();
            await _database.CreateTableAsync<Product>();
        }

        
        public Task<List<MenuCategory>> GetMenuCategoriesAsync() => _database.Table<MenuCategory>().ToListAsync();
        public Task<int> SaveMenuCategoryAsync(MenuCategory category) => category.Id != 0 ? _database.UpdateAsync(category) : _database.InsertAsync(category);
        public Task<int> DeleteMenuCategoryAsync(MenuCategory category) => _database.DeleteAsync(category);

      
        public Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return _database.Table<Product>()
                            .Where(p => p.MenuCategoryId == categoryId)
                            .ToListAsync();
        }

        public async Task<MenuCategory> GetCategoryByIdAsync(int categoryId)
        {
            
            var categories = await _database.Table<MenuCategory>()
                                             .Where(c => c.Id == categoryId)
                                             .ToListAsync();
            return categories.FirstOrDefault(); 
        }

        public Task<int> SaveProductAsync(Product product) => product.Id != 0 ? _database.UpdateAsync(product) : _database.InsertAsync(product);
        public Task<int> DeleteProductAsync(Product product) => _database.DeleteAsync(product);
    }
}
