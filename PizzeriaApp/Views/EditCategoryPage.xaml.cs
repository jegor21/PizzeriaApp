using PizzeriaApp.Models;
using PizzeriaApp.Services;
using Microsoft.Maui.Storage;

namespace PizzeriaApp.Views
{
    public partial class EditCategoryPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private MenuCategory _category;
        private string _imagePath;

        public EditCategoryPage(DatabaseService databaseService, MenuCategory category)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _category = category;

            _imagePath = _category.ImagePath; 
            LoadCategoryData();
        }

        private void LoadCategoryData()
        {
            CategoryNameEntry.Text = _category.Name;
            CategoryImage.Source = !string.IsNullOrEmpty(_category.ImagePath)
                ? ImageSource.FromFile(_category.ImagePath)
                : "placeholder.png"; 
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

            _category.Name = CategoryNameEntry.Text;
            _category.ImagePath = _imagePath;

            await _databaseService.SaveMenuCategoryAsync(_category);
            await Navigation.PopAsync();
        }
    }
}
