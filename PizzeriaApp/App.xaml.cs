using System.IO;
using PizzeriaApp.Services;
using PizzeriaApp.Views;

namespace PizzeriaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PizzeriaApp.db");
            var databaseService = new DatabaseService(dbPath);
            MainPage = new NavigationPage(new MainPage(databaseService));
        }
    }
}
