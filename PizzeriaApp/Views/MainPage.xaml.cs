using PizzeriaApp.Models;
using PizzeriaApp.Services;
using System;

namespace PizzeriaApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public MainPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService)); // Null check
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MenuCollectionView.ItemsSource = await _databaseService.GetMenuCategoriesAsync();
        }

        async void OnAddCategoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCategoryPage(_databaseService));
        }

        async void OnMenuSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is MenuCategory selectedCategory)
            {
                await Navigation.PushAsync(new ProductPage(_databaseService, selectedCategory));
            }
        }
    }
}
