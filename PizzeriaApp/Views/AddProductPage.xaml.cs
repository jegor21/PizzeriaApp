using PizzeriaApp.Models;
using PizzeriaApp.Services;
using Microsoft.Maui.Storage;
using System;

namespace PizzeriaApp.Views
{
    public partial class AddProductPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private string _imagePath;
        private Product _product;
        private MenuCategory _category; 

        
        public AddProductPage(DatabaseService databaseService, MenuCategory category)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _category = category; 
        }

        
        public AddProductPage(DatabaseService databaseService, MenuCategory category, Product product)
            : this(databaseService, category) 
        {
            _product = product; 
            LoadProductData();
        }

        private void LoadProductData()
        {
            if (_product != null)
            {
                ProductNameEntry.Text = _product.Name;
                ProductDescriptionEntry.Text = _product.Description;
                ProductIngredientsEntry.Text = _product.Ingredients;
                ProductPriceEntry.Text = _product.Price.ToString();
                _imagePath = _product.ImagePath;
                ProductImage.Source = ImageSource.FromFile(_imagePath);
            }
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                _imagePath = result.FullPath;
                ProductImage.Source = ImageSource.FromFile(_imagePath);
            }
        }

        
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductNameEntry.Text) ||
                string.IsNullOrWhiteSpace(ProductDescriptionEntry.Text) ||
                string.IsNullOrWhiteSpace(ProductIngredientsEntry.Text) ||
                string.IsNullOrWhiteSpace(ProductPriceEntry.Text))
            {
                await DisplayAlert("Error", "Please fill all lines", "OK");
                return;
            }

            if (_product != null) 
            {
                _product.Name = ProductNameEntry.Text;
                _product.Description = ProductDescriptionEntry.Text;
                _product.Ingredients = ProductIngredientsEntry.Text;
                _product.Price = decimal.Parse(ProductPriceEntry.Text);
                _product.ImagePath = _imagePath;

                await _databaseService.SaveProductAsync(_product); 
            }
            else 
            {
                var newProduct = new Product
                {
                    Name = ProductNameEntry.Text,
                    Description = ProductDescriptionEntry.Text,
                    Ingredients = ProductIngredientsEntry.Text,
                    Price = decimal.Parse(ProductPriceEntry.Text),
                    ImagePath = _imagePath,
                    MenuCategoryId = _category.Id 
                };

                await _databaseService.SaveProductAsync(newProduct); 
            }

            MessagingCenter.Send(this, "ProductAdded");
            await Navigation.PopAsync();
        }

    }

}
