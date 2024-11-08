using MarketPlaceApp.Models;
using Newtonsoft.Json;

namespace MarketPlaceApp.Services
{
    public class ProductService
    {
         
        private readonly string _productsFilePath;
        public ProductService()
        {
            _productsFilePath = FindJsonFilePath(); 
        }
        private string FindJsonFilePath()
        { string currentDirectory = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(currentDirectory, "Products.json",SearchOption.AllDirectories);
            if (files.Length > 0) 
            { return files[0]; } 
            else 
            { throw new FileNotFoundException("Products.json file not found."); } }
 
        //private readonly string _productsFilePath1 = "C:\\Users\\Trainee\\Desktop\\c#\\MarketPlaceApp\\wwwroot\\js\\Products.json";

        public List<Product> GetAllProduct()
        {
            if (!File.Exists(_productsFilePath))
            {
                return new List<Product>();
            }

            var jsonData = File.ReadAllText(_productsFilePath);
            return JsonConvert.DeserializeObject<List<Product>>(jsonData) ?? new List<Product>();
        }
        
        public void AddProduct(Product newProduct)
        {
            var allProducts = GetAllProduct();
            newProduct.Id = allProducts.Any() ? allProducts.Max(products => products.Id)+1 : 1;

            allProducts.Add(newProduct);
            SaveProduct(allProducts);
        }
 
        public void UpdateProduct(Product newProduct) 
        {
            var allProducts = GetAllProduct();
            Product oldProduct = allProducts.FirstOrDefault(u => u.Id == newProduct.Id);
            if (oldProduct != null)
            {
                oldProduct.Name = newProduct.Name;
                oldProduct.Description = newProduct.Description;
                oldProduct.Rating = newProduct.Rating;
                oldProduct.price = newProduct.price; 
                oldProduct.ImageURL = newProduct.ImageURL;
                SaveProduct(allProducts);
            }
        }
        private void SaveProduct(List<Product> allProducts)
        {
            var jsonData = JsonConvert.SerializeObject(allProducts, Formatting.Indented);
            File.WriteAllText(_productsFilePath, jsonData);
        }
    }
}
