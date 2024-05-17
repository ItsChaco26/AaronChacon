using MySqlConnector;
using storeApi.DataBase;
using storeApi.Models;
using storeApi.Models.Data;

namespace storeApi
{
    public sealed class Store
    {
        private Products ProductsInstance;
        public IEnumerable<Product> Products { get; private set; }
        public int TaxPercentage { get; private set; }
        public IEnumerable<Category> Categories { get; private set; }
        private Store(IEnumerable<Product> productsList, int taxPercentage, IEnumerable<Category> category, Products productsInstance)
        {
            if (productsList == null || productsList.Count() == 0) throw new ArgumentNullException($"La lista de productos {nameof(productsList)} no puede ser nula.");
            if (taxPercentage < 1 || taxPercentage > 100) throw new ArgumentOutOfRangeException($"El porcentaje de impuestos {nameof(taxPercentage)} debe estar en el rango de 1 a 100.");
            if (category == null || category.Count() == 0) throw new ArgumentNullException($"La lista de estructuras {nameof(category)} de categorías no puede ser nula.");
            if (productsInstance == null) throw new ArgumentNullException($"La instancia de  {nameof(productsInstance)} no puede ser nula.");
            this.Products = productsList;
            this.TaxPercentage = taxPercentage;
            this.Categories = category;
            this.ProductsInstance = productsInstance;
        }
        public static async Task<Store> GetInstanceAsync()
        {
            Products products = await new Products().GetInstanceAsync();
            var productsInstance = products;
            if (productsInstance == null || productsInstance.GetAllProducts().Count() == 0) throw new ArgumentException($" La instancia {nameof(productsInstance)} no puede ser nula.");
            return new Store(productsInstance.GetAllProducts(), 13, productsInstance.GetCategory(), productsInstance);
        }
        private static readonly Lazy<Task<Store>> instance = new Lazy<Task<Store>>(GetInstanceAsync);
        public static Task<Store> Instance => instance.Value;
        public async Task<IEnumerable<Product>> getProductosCategoryID(List<int> categoryIDs)
        {
            var productsInstance = ProductsInstance;
            foreach (var categoryId in categoryIDs)
            {
                if (categoryId < 1) throw new ArgumentException($"El ID de la categoría {categoryId} no puede ser negativo o cero.");
            }
            if (productsInstance == null || productsInstance.GetAllProducts().Count() == 0) throw new ArgumentException($" La instancia {nameof(productsInstance)} no puede ser nula.");
            return productsInstance.GetProductsByCategoryIDs(categoryIDs);
        }
        public async Task<IEnumerable<Product>> getProductsCategoryAndText(string textToSearch, List<int> categoryIDs)
        {
            var productsInstance = ProductsInstance;
            if (textToSearch == null || textToSearch.Trim() == "") throw new ArgumentException($"El texto a buscar {nameof(textToSearch)} no puede ser nulo.");
            foreach (var categoryId in categoryIDs)
            {
                if (categoryId < 1) throw new ArgumentException($"El ID de la categoría {categoryId} no puede ser negativo o cero.");
            }
            if (productsInstance == null || productsInstance.GetAllProducts().Count() == 0) throw new ArgumentException($" La instancia {nameof(productsInstance)} no puede ser nula.");
            return productsInstance.SearchByTextAndCategory(textToSearch, categoryIDs);
        }
        
        public async Task<IEnumerable<Product>> getProductByText(string textToSearch)
        {
            var productsInstance = ProductsInstance;
            if (textToSearch == null || textToSearch.Trim() == "") throw new ArgumentException($"El texto a buscar {nameof(textToSearch)} no puede ser nulo.");
            if (productsInstance == null || productsInstance.GetAllProducts().Count() == 0) throw new ArgumentException($" La instancia {nameof(productsInstance)} no puede ser nula.");
            return productsInstance.SearchByText(textToSearch);
        }


    }
}