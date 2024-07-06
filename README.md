# Web Page

**Developer:** Aarón Chacón Céspedes

## Table of Contents
1. [Description](#description-of-the-project)
    - [Main Features](#main-features)
2. [Diagrams](#diagrams)
   - [Main Page Diagram](#main-page-diagram)
   - [Payment Page Diagram](#payment-page-diagram)
   - [Reports Page Diagram](#reports-page-diagram)
   - [Package Diagram](#package-diagram)
   - [Activity Diagram](#activity-diagram)
   - [WebSocket Diagram](#websocket-diagram)
   - [Project Class Diagram](#project-class-diagram)
   - [Cart Activity Diagram](#cart-activity-diagram)
   - [Payment Activity Diagram](#payment-activity-diagram)
3. [Security](#security)
   - [Authentication and Authorization](#authentication-and-authorization)
   - [JWT Configuration](#jwt-configuration)
   - [Role-based Authorization](#role-based-authorization)
4. [Cache](#cache)
   - [Product Cache](#product-cache)
5. [Product Search](#product-search)
   - [Search Logic](#search-logic)
6. [Sales Reports](#sales-reports)
   - [Reports Implementation](#reports-implementation)
7. [Demonstration](#project-demonstration)


## Description of the Project
This project is a complete web store where users can buy products, select the desired quantity, choose the shipping address and payment method, among other functionalities. Administrators have the ability to send messages or campaigns to clients, add new products, enable or disable payment methods, and review daily and weekly sales reports.

## Main Features
### Users
- **Registration and Login:** Clients can register and log into their accounts.
- **Search Products:** Users can search and browse products.
- **Shopping Cart:** Add products to the cart, select the desired quantity.
- **Make Purchases:** Complete the purchase by selecting the shipping address and payment method.

### Administrators
- **Product Management:**
  - Add new products.
  - Update existing product information.
  - Delete products.

- **Payment Method Management:**
  - Enable or disable payment methods as needed.

- **Sending Campaigns:**
  - Send promotional messages or campaigns to clients.

- **Reports:**
  - Review detailed daily sales reports.
  - Review detailed weekly sales reports.

## Requirements
- **Frontend:**
  - HTML, CSS, JavaScript
  - Recommended framework: React.js

- **Backend:**
  - Language: Node.js
  - Database: MySQL

## Diagrams
### Main Page Diagram
![Main Page Diagram](C22037/Images/pagprincipal.jpeg)

### Store
**Properties:**
- **Products:** A collection of products available in the store.
- **TaxPercentage:** The percentage of tax applied to products.
- **categoryLogic:** An instance of `CategoryLogic` that handles the logic related to product categories.

**Methods:**
- **Store(IEnumerable<Product> products, int taxPercentage):** Constructor that initializes the store with a collection of products and a tax percentage.
- **InstanceAsync():** Asynchronous method that returns an instance of `Store`.
- **GetProductsByCategoryAsync(int id):** Asynchronous method that returns an instance of `Store` filtered by category.

### CategoryLogic
**Properties:**
- **productsByCategoryId:** Dictionary mapping category identifiers to corresponding product lists.

**Methods:**
- **CategoryLogic(categories: CategorySt[], products: IEnumerable<Product>):** Constructor that initializes category logic with the given categories and products.
- **GetProductsBySearchAsync(search: String, categories: String):** Asynchronous method that returns products matching the search and provided categories.
- **GetCategoriesByIdAsync(categoryIds: IEnumerable<int>):** Asynchronous method that returns categories by their identifiers.
- **SearchProducts(products: List<Product>, search: String):** Method that searches for products in a list based on the search criteria.

### Product
**Properties:**
- **Name:** Product name.
- **ImageURL:** Product image URL.
- **Price:** Product price.
- **Description:** Product description.
- **Id:** Product identifier.
- **Category:** Product category represented by an instance of `CategorySt`.

**Methods:**
- **Product(name: String, imageURL: String, price: decimal, description: String, id: int, category: CategorySt):** Constructor that initializes the product with the provided values.
- **Clone():** Method that clones the product.

### Category
**Properties:**
- **Id:** Category identifier.
- **Name:** Category name.
- **Categories:** Subcategories represented by a collection of `CategorySt` instances.

**Methods:**
- **Category(id: int, name: String, categories: CategorySt[]):** Constructor that initializes the category with the provided values.
- **GetType(id: int):** Method that gets the category type based on its identifier.
- **SortCategories():** Method that sorts categories.

### StoreDB
**Methods:**
- **CreateMysql():** Method that creates the MySQL database for the store.
- **GetProductsAsync():** Asynchronous method that retrieves a collection of products from the database.

### StoreController
**Methods:**
- **GetStoreAsync():** Asynchronous method that returns the store.
- **GetProductsByCategoryAsync(categories: [FromQuery] string):** Asynchronous method that returns products filtered by categories.
- **SearchAsync(search: [FromQuery] string, categories: [FromQuery] string):** Asynchronous method that searches for products based on the provided search criteria and categories.


### Payment Page Diagram
![Payment Page Diagram](C22037/Images/pagpago.jpeg)

### SaleDB
**Methods:**
- **SaveAsync(sale: Sale):** Asynchronous method that saves a sale to the database.
- **GetSalesReportAsync(date: DateTime):** Asynchronous method that retrieves a sales report for a specific date.
- **GetWeeklySalesAsync(date: DateTime):** Asynchronous method that retrieves weekly sales for a specific date.
- **GetDailySalesAsync(date: DateTime):** Asynchronous method that retrieves daily sales for a specific date.

### PaymentMethod
**Properties:**
- **Type:** Enumeration representing the type of payment method.

**Methods:**
- **Find(type: Type):** Method that finds a payment method based on its type.

### Sale
**Properties:**
- **Products:** Collection of products included in the sale.
- **Address:** Shipping address.
- **Amount:** Total amount of the sale.
- **PaymentMethod:** Type of payment method used.
- **PurchaseNumber:** Purchase number.

### StoreLogic
**Properties:**
- **saleDB:** Instance of `SaleDB` to interact with the sales database.

**Methods:**
- **PurchaseAsync(cart: Cart):** Asynchronous method that processes the purchase of a cart.
- **GenerateNextPurchaseNumber():** Method that generates the next purchase number.

### CartController
**Properties:**
- **storeLogic:** Instance of `StoreLogic` to handle store logic.

**Methods:**
- **CreateCartAsync(cart: Cart):** Asynchronous method that creates a cart.

### Reports Page Diagram
![Reports Page Diagram](C22037/Images/pagreportes.jpeg)

### SaleDB
**Methods:**
- **SaveAsync(sale: Sale):** Asynchronous method that saves a sale to the database.
- **GetSalesReportAsync(date: DateTime):** Asynchronous method that retrieves a sales report for a specific date.
- **GetWeeklySalesAsync(date: DateTime):** Asynchronous method that retrieves weekly sales for a specific date.
- **GetDailySalesAsync(date: DateTime):** Asynchronous method that retrieves daily sales for a specific date.

### DailyReport
**Properties:**
- **PurchaseDate:** Purchase date.
- **PurchaseNumber:** Purchase number.
- **Total:** Total amount of the purchase.

### WeeklyReport
**Properties:**
- **Day:** Day of the week.
- **Total:** Total sales for the day.

### SaleController
**Methods:**
- **GetReportAsync(date: [FromQuery] DateTime):** Asynchronous method that retrieves a sales report based on the provided date.

## Package Diagram
![Package Diagram](C22037/Images/diagrama_paquetes.jpeg)

## WebSocket Diagram
![WebSocket Diagram](C22037/Images/ws.jpeg)

## Project Class Diagram
![Project Class Diagram](C22037/Images/DiagramaClasesProy.png)

### ApiController
**Methods:**
- **CreateCartAsync(Cart cart): Task<IActionResult>:** Asynchronous method that creates a cart.
- **GetPaymentMethodsAsync(): Task<List<PaymentMethod>>:** Asynchronous method that retrieves the list of available payment methods.

### StoreLogic
**Methods:**
- **PurchaseAsync(Cart cart): Task<Purchase>:** Asynchronous method that processes the purchase of a cart and returns an instance of `Purchase`.

### Purchase
**Properties:**
- **PurchaseNumber:** Purchase number.
- **Cart:** Instance of `Cart` containing the purchased products.
- **PaymentMethod:** Payment method used for the purchase.
- **Address:** Shipping address.

### Cart
**Properties:**
- **Items:** Dictionary mapping product identifiers to `CartItem` instances, representing the products in the cart.
- **Subtotal:** Cart subtotal.
- **Total:** Cart total.

### CartItem
**Properties:**
- **Product:** Instance of `Product` representing the product in the cart.
- **Quantity:** Quantity of the product in the cart.

### Product
**Properties:**
- **Id:** Product identifier.
- **Name:** Product name.
- **Description:** Product description.
- **Price:**

 Product price.
- **ImageURL:** Product image URL.

### PaymentMethod
**Properties:**
- **Id:** Payment method identifier.
- **Name:** Payment method name.
- **IsEnabled:** Indicates whether the payment method is enabled.

### Workflow
1. **ApiController:**
   - `CreateCartAsync` creates a new cart and returns it.
   - `GetPaymentMethodsAsync` retrieves the available payment methods.
2. **StoreLogic:**
   - `PurchaseAsync` processes the purchase of a cart, creating an instance of `Purchase` with the purchase details.
3. **Purchase:**
   - Contains the purchase details, including the purchase number, cart, payment method, and shipping address.
4. **Cart:**
   - Contains the cart items (products and their quantities) and the totals (subtotal and total).
5. **CartItem:**
   - Represents a product and its quantity in the cart.
6. **Product:**
   - Contains the product information, such as its identifier, name, description, price, and image.
7. **PaymentMethod:**
   - Contains the payment method information, such as its identifier, name, and enabled status.


## Cart Activity Diagram
![Cart Activity Diagram](C22037/Images/DiagramaActividadCart.png)

## Payment Activity Diagram
![Payment Activity Diagram](C22037/Images/DiagramaActividadPayment.png)

## Security

### Authentication and Authorization
Authentication and authorization are handled through JWT (JSON Web Tokens). The authentication endpoint is located in the `AuthController`, which generates a JWT token upon validating user credentials. This token includes roles and permissions, allowing for role-based access control to different parts of the application.

The JWT token is validated on each request through middleware configured in `Program.cs`, ensuring that only authenticated users can access certain endpoints. Additionally, the `Bearer` authorization scheme is used with `JwtBearerDefaults.AuthenticationScheme`.

### JWT Configuration
In the `Program.cs` file, the JWT authentication scheme is configured with the following options:
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5087",
            ValidAudience = "http://localhost:5087",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TheSecretKeyNeedsToBePrettyLongSoWeNeedToAddSomeCharsHere"))
        };
    });
```

### Role-based Authorization
The user's role is extracted from the JWT token and used to control access to certain parts of the application. For example, only users with the `Admin` role can access the administration interface.

## Cache

### Product Cache
To improve efficiency and reduce database requests, a caching mechanism is implemented for the products in the store. Products and categories are initially loaded from the database and stored in lists that contain the models of these classes, ensuring that database access does not affect the overall performance of the application.

## Product Search

The product search allows users to search for products by name, description, and price. It uses an optimized binary search to find matches in the product list.

### Search Logic
In `CategoryLogic.cs`, the search logic is implemented:
```csharp
public async Task<IEnumerable<Product>> GetProductsBySearchAsync(string search, string categories)
{
    List<Product> matchingProducts = new List<Product>();
    List<int> categoryIdsList = categories?.Split(',').Select(int.Parse).ToList();
    if (categoryIdsList == null || !categoryIdsList.Any())
    {
        foreach (var id in productsByCategoryId)
        {
            matchingProducts.AddRange(SearchProducts(id.Value, search));
        }
    }
    else
    {
        foreach (var categoryId in categoryIdsList)
        {
            if (productsByCategoryId.TryGetValue(categoryId, out List<Product> categoryProducts))
            {
                matchingProducts.AddRange(SearchProducts(categoryProducts, search));
            }
        }
    }
    return matchingProducts;
}
```

## Sales Reports

The application provides daily and weekly sales reports. Reports are retrieved from the database and presented in the administration interface.

### Reports Implementation
In the `SaleController`, an endpoint is implemented to retrieve sales reports:
```csharp
[HttpGet]
public async Task<IActionResult> GetReportAsync([FromQuery] DateTime date)
{
    if (date == DateTime.MinValue)
    {
        return BadRequest("Date parameter is required.");
    }
    if (date > DateTime.Now)
    {
        return BadRequest("Date cannot be in the future.");
    }
    SaleDB saleDB = new SaleDB();
    SalesReport salesReport = saleDB.GetSalesReport(date);
    return Ok(salesReport);
}
```

The way reports are generated in `SaleDB` is as follows:
```csharp
public SalesReport GetSalesReport(DateTime date)
{
    if (date == DateTime.MinValue)
        throw new ArgumentException("Date cannot be:", nameof(date));

    Task<List<WeeklyReport>> weeklySalesTask = GetWeeklySalesAsync(date);
    Task<List<DailyReport>> dailySalesTask = GetDailySalesAsync(date);
    Task.WaitAll(weeklySalesTask, dailySalesTask);
    List<WeeklyReport> weeklySales = weeklySalesTask.Result;
    List<DailyReport> dailySales = dailySalesTask.Result;

    SalesReport salesReport = new SalesReport(dailySales, weeklySales);
    return salesReport;
}
```

In the `GetWeeklySalesAsync` function, weekly sales reports are retrieved:
```csharp
public async Task<List<WeeklyReport>> GetWeeklySalesAsync(DateTime date)
{
    List<WeeklyReport> weeklySales = new List<WeeklyReport>();

    using (MySqlConnection connection = new MySqlConnection(Storage.Instance.ConnectionString))
    {
        await connection.OpenAsync();

        string selectQuery = @"
            use store;
            SELECT DAYNAME(sale.purchase_date) AS day, SUM(sale.total) AS total
            FROM sales sale 
            WHERE YEARWEEK(sale.purchase_date) = YEARWEEK(@date)
            GROUP BY DAYNAME(sale.purchase_date);";

        using (var command = new MySqlCommand(selectQuery, connection))
        {
            command.Parameters.AddWithValue("@date", date);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    string day = reader.GetString("day");
                    decimal total = reader.GetDecimal("total");
                    WeeklyReport weeklyReport = new WeeklyReport(day, total);
                    weeklySales.Add(weeklyReport);
                }
            }
        }
    }
    return weeklySales;
}
```

In the `GetDailySalesAsync` function, daily sales reports are retrieved:
```csharp
public async Task<List<DailyReport>> GetDailySalesAsync(DateTime date)
{
    List<DailyReport> dailySales = new List<DailyReport>();

    using (MySqlConnection connection = new MySqlConnection(Storage.Instance.ConnectionString))
    {
        await connection.OpenAsync();

        string selectQuery = @"
            use store;
            SELECT purchase_date, purchase_number, total
            FROM sales
            WHERE DATE(purchase_date) = DATE(@date);";

        using (var command = new MySqlCommand(selectQuery, connection))
        {
            command.Parameters.AddWithValue("@date", date);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    DateTime purchaseDate = reader.GetDateTime("purchase_date");
                    string purchaseNumber = reader.GetString("purchase_number");
                    decimal total = reader.GetDecimal("total");
                    DailyReport dailyReport = new DailyReport(purchaseDate, purchaseNumber, total);
                    dailySales.Add(dailyReport);
                }
            }
        }
    }
    return dailySales;
}
```

## Project Demonstration

[Watch the demonstration on YouTube](https://youtu.be/z8tUeHrQPFk)
