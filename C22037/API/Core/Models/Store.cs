using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Business;
using TodoApi.Database;

namespace TodoApi.Models
{
    public sealed class Store
    {
        private static Store instance;
        private static readonly object _lock = new object();
        private StoreDB storeDB;
        public List<Product> Products { get; private set; }
        public int TaxPercentage { get; private set; }
        private CategoryLogic categoryLogic;

        public Store(List<Product> products, int taxPercentage, StoreDB db)
        {
            if (products == null) throw new ArgumentNullException(nameof(products), "Products cannot be null.");
            if (!products.Any()) throw new ArgumentException("Products list cannot be empty.", nameof(products));
            if (taxPercentage < 0 || taxPercentage > 100) throw new ArgumentOutOfRangeException(nameof(taxPercentage), "Tax percentage must be between 0 and 100.");

            Products = products;
            TaxPercentage = taxPercentage;
            storeDB = db;
        }

        public static async Task<Store> InstanceAsync()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        var db = new StoreDB();
                        var categories = new Categories().GetCategories();
                        var products = StoreDB.GetProductsAsync().Result.ToList();
                        instance = new Store(products, 13, db);
                        instance.categoryLogic = new CategoryLogic(categories, products);
                    }
                }
            }
            return instance;
        }

        public async Task AddProduct(ProductAdd productAdd, int id)
        {
            if (string.IsNullOrWhiteSpace(productAdd.name))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(productAdd.name));
            if (string.IsNullOrWhiteSpace(productAdd.imageUrl))
                throw new ArgumentException("Product image URL cannot be null or empty.", nameof(productAdd.imageUrl));
            if (productAdd.price <= 0)
                throw new ArgumentException("Product price must be greater than zero.", nameof(productAdd.price));
            if (string.IsNullOrWhiteSpace(productAdd.description))
                throw new ArgumentException("Product description cannot be null or empty.", nameof(productAdd.description));
            if (id <= 0)
                throw new ArgumentException("Product ID must be greater than zero.", nameof(id));
            if (productAdd.category <= 0)
                throw new ArgumentException("Product category must be greater than zero.", nameof(productAdd.category));

            var newProduct = new Product(productAdd.name, productAdd.imageUrl, productAdd.price, productAdd.description, id, new Categories().GetType(productAdd.category), productAdd.quantity);
            Products.Add(newProduct);
        }

        public async Task AddProductAsync(ProductAdd product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (string.IsNullOrWhiteSpace(product.name))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(product.name));
            if (string.IsNullOrWhiteSpace(product.imageUrl))
                throw new ArgumentException("Product image URL cannot be null or empty.", nameof(product.imageUrl));
            if (product.price <= 0)
                throw new ArgumentException("Product price must be greater than zero.", nameof(product.price));
            if (string.IsNullOrWhiteSpace(product.description))
                throw new ArgumentException("Product description cannot be null or empty.", nameof(product.description));
            if (product.category <= 0)
                throw new ArgumentException("Product category must be greater than zero.", nameof(product.category));

            await storeDB.InsertProductAsync(product);
        }

        public async Task RemoveProductAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Product ID must be greater than zero.", nameof(id));

            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
                await storeDB.DeleteProductAsync(id);
            }
        }

        public async Task<Store> GetProductsByCategoryAsync(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Category ID must be greater than 0.");

            var productsByCategory = await categoryLogic.GetCategoriesByIdAsync(new[] { id });
            return new Store(productsByCategory.ToList(), 13, storeDB);
        }
    }
}