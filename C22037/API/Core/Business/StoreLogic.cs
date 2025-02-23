using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;
using TodoApi.Database;
using TodoApi;

namespace TodoApi.Business
{
    public sealed class StoreLogic
    {
        private SaleDB saleDB = new SaleDB();

        public async Task<Sale> PurchaseAsync(Cart cart)
        {
            if (cart == null) throw new ArgumentNullException("Cart cannot be null.");
            if (cart.ProductIds.Count == 0) throw new ArgumentException("Cart must contain at least one product.");
            if (string.IsNullOrWhiteSpace(cart.Address)) throw new ArgumentException("Address must be provided.");

            var store = await Store.InstanceAsync();
            var products = store.Products;
            var taxPercentage = store.TaxPercentage;

            IEnumerable<Product> matchingProducts = products.Where(p => cart.ProductIds.Contains(p.Id.ToString())).ToList();

            IEnumerable<Product> shadowCopyProducts = matchingProducts.Select(p =>
            {
                var product = (Product)p.Clone();
                product.Quantity = cart.GetQuantityForProduct(p.Id.ToString());
                return product;
            }).ToList();

            string purchaseNumber = GenerateNextPurchaseNumber();
            PaymentMethod.Type paymentMethodType = cart.PaymentMethod;

            var sale = new Sale(shadowCopyProducts, cart.Address, cart.Total, paymentMethodType);
            await saleDB.SaveAsync(sale);

            return sale;
        }

        public static string GenerateNextPurchaseNumber()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomLetters = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());

            int randomNumber = random.Next(100000, 999999);

            string purchaseNumber = $"{randomLetters}-{randomNumber}";

            return purchaseNumber;
        }
    }
}