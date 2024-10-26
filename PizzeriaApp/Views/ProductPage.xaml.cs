using PizzeriaApp.Models;
using PizzeriaApp.Services;

namespace PizzeriaApp.Views
{
    public partial class ProductPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly MenuCategory _category;

        public ProductPage(DatabaseService databaseService, MenuCategory category)
        {
            InitializeComponent();
            _databaseService = databaseService;
            _category = category;
            BindingContext = new { CategoryName = category.Name };

            MessagingCenter.Subscribe<AddProductPage>(this, "ProductAdded", async (sender) =>
            {
                await LoadProducts(); 
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadProducts(); 
        }

        private async Task LoadProducts()
        {
            ProductCollectionView.ItemsSource = await _databaseService.GetProductsByCategoryAsync(_category.Id);
        }

        public async void OnProductSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Product selectedProduct)
            {
                
                await Navigation.PushAsync(new ProductDetailPage(_databaseService, selectedProduct));

                
                ProductCollectionView.SelectedItem = null;
            }
        }


        public async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProductPage(_databaseService, _category));
        }

        
        private async void OnDeleteCategoryClicked(object sender, EventArgs e)
        {
            var confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete the category and all associated products?", "Yes", "No");
            if (confirm)
            {
                await _databaseService.DeleteMenuCategoryAsync(_category);
                await Navigation.PopAsync(); 
            }
        }

        
        private async void OnEditCategoryClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new EditCategoryPage(_databaseService, _category));
        }

        
        private async void OnViewProductDetailsClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Product selectedProduct)
            {
                await Navigation.PushAsync(new ProductDetailPage(_databaseService, selectedProduct));
            }
        }

        private async void OnEditProductClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Product product)
            {
                await Navigation.PushAsync(new AddProductPage(_databaseService, _category, product));
            }
        }

        private async void OnDeleteProductClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Product product)
            {
                var confirm = await DisplayAlert("Confirmation", "Are you sure you want to remove the product?", "Yes", "No");
                if (confirm)
                {
                    await _databaseService.DeleteProductAsync(product);
                    await LoadProducts(); 
                }
            }
        }
    }
}
