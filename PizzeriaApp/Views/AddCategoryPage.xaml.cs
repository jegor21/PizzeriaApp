using PizzeriaApp.Models;
using PizzeriaApp.Services;
using Microsoft.Maui.Storage;

namespace PizzeriaApp.Views
{
    public partial class AddCategoryPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private string _imagePath;

        public AddCategoryPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        
        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                _imagePath = result.FullPath;
                CategoryImage.Source = ImageSource.FromFile(_imagePath);
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryNameEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a category name", "OK");
                return;
            }

            var newCategory = new MenuCategory
            {
                Name = CategoryNameEntry.Text,
                ImagePath = _imagePath
            };

            await _databaseService.SaveMenuCategoryAsync(newCategory);
            await Navigation.PopAsync();
        }
    }
}
