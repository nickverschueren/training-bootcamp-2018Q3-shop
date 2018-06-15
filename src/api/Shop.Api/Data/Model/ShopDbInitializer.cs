using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Api.Data.Model
{
    public class ShopDbInitializer
    {
        private readonly ShopDbContext _shopDbContext;

        public ShopDbInitializer(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task Initialize()
        {
            if (await _shopDbContext.Database.EnsureCreatedAsync())
            {
                await SeedProducts();
            }
        }

        private async Task SeedProducts()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Title = "Chai",
                    BasePrice = 18.00M,
                    Price = 18.00M,
                    Description = "Chai (10 boxes x 20 bags)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 39, Reserved = 0}
                },
                new Product
                {
                    Title = "Chang",
                    BasePrice = 19.00M,
                    Price = 19.00M,
                    Description = "Chang (24 - 12 oz bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 17, Reserved = 0}
                },
                new Product
                {
                    Title = "Aniseed Syrup",
                    BasePrice = 10.00M,
                    Price = 10.00M,
                    Description = "Aniseed Syrup (12 - 550 ml bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 13, Reserved = 0}
                },
                new Product
                {
                    Title = "Chef Anton's Cajun Seasoning",
                    BasePrice = 22.00M,
                    Price = 22.00M,
                    Description = "Chef Anton's Cajun Seasoning (48 - 6 oz jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 53, Reserved = 0}
                },
                new Product
                {
                    Title = "Chef Anton's Gumbo Mix",
                    BasePrice = 21.35M,
                    Price = 21.35M,
                    Description = "Chef Anton's Gumbo Mix (36 boxes)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 0, Reserved = 0}
                },
                new Product
                {
                    Title = "Grandma's Boysenberry Spread",
                    BasePrice = 25.00M,
                    Price = 25.00M,
                    Description = "Grandma's Boysenberry Spread (12 - 8 oz jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 120, Reserved = 0}
                },
                new Product
                {
                    Title = "Uncle Bob's Organic Dried Pears",
                    BasePrice = 30.00M,
                    Price = 30.00M,
                    Description = "Uncle Bob's Organic Dried Pears (12 - 1 lb pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 15, Reserved = 0}
                },
                new Product
                {
                    Title = "Northwoods Cranberry Sauce",
                    BasePrice = 40.00M,
                    Price = 40.00M,
                    Description = "Northwoods Cranberry Sauce (12 - 12 oz jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 6, Reserved = 0}
                },
                new Product
                {
                    Title = "Mishi Kobe Niku",
                    BasePrice = 97.00M,
                    Price = 97.00M,
                    Description = "Mishi Kobe Niku (18 - 500 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 29, Reserved = 0}
                },
                new Product
                {
                    Title = "Ikura",
                    BasePrice = 31.00M,
                    Price = 31.00M,
                    Description = "Ikura (12 - 200 ml jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 31, Reserved = 0}
                },
                new Product
                {
                    Title = "Queso Cabrales",
                    BasePrice = 21.00M,
                    Price = 21.00M,
                    Description = "Queso Cabrales (1 kg pkg.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 22, Reserved = 0}
                },
                new Product
                {
                    Title = "Queso Manchego La Pastora",
                    BasePrice = 38.00M,
                    Price = 38.00M,
                    Description = "Queso Manchego La Pastora (10 - 500 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 86, Reserved = 0}
                },
                new Product
                {
                    Title = "Konbu",
                    BasePrice = 6.00M,
                    Price = 6.00M,
                    Description = "Konbu (2 kg box)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 24, Reserved = 0}
                },
                new Product
                {
                    Title = "Tofu",
                    BasePrice = 23.25M,
                    Price = 23.25M,
                    Description = "Tofu (40 - 100 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 35, Reserved = 0}
                },
                new Product
                {
                    Title = "Genen Shouyu",
                    BasePrice = 15.50M,
                    Price = 15.50M,
                    Description = "Genen Shouyu (24 - 250 ml bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 39, Reserved = 0}
                },
                new Product
                {
                    Title = "Pavlova",
                    BasePrice = 17.45M,
                    Price = 17.45M,
                    Description = "Pavlova (32 - 500 g boxes)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 29, Reserved = 0}
                },
                new Product
                {
                    Title = "Alice Mutton",
                    BasePrice = 39.00M,
                    Price = 39.00M,
                    Description = "Alice Mutton (20 - 1 kg tins)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 0, Reserved = 0}
                },
                new Product
                {
                    Title = "Carnarvon Tigers",
                    BasePrice = 62.50M,
                    Price = 62.50M,
                    Description = "Carnarvon Tigers (16 kg pkg.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 42, Reserved = 0}
                },
                new Product
                {
                    Title = "Teatime Chocolate Biscuits",
                    BasePrice = 9.20M,
                    Price = 9.20M,
                    Description = "Teatime Chocolate Biscuits (10 boxes x 12 pieces)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 25, Reserved = 0}
                },
                new Product
                {
                    Title = "Sir Rodney's Marmalade",
                    BasePrice = 81.00M,
                    Price = 81.00M,
                    Description = "Sir Rodney's Marmalade (30 gift boxes)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 40, Reserved = 0}
                },
                new Product
                {
                    Title = "Sir Rodney's Scones",
                    BasePrice = 10.00M,
                    Price = 10.00M,
                    Description = "Sir Rodney's Scones (24 pkgs. x 4 pieces)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 3, Reserved = 0}
                },
                new Product
                {
                    Title = "Gustaf's Knäckebröd",
                    BasePrice = 21.00M,
                    Price = 21.00M,
                    Description = "Gustaf's Knäckebröd (24 - 500 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 104, Reserved = 0}
                },
                new Product
                {
                    Title = "Tunnbröd",
                    BasePrice = 9.00M,
                    Price = 9.00M,
                    Description = "Tunnbröd (12 - 250 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 61, Reserved = 0}
                },
                new Product
                {
                    Title = "Guaraná Fantástica",
                    BasePrice = 4.50M,
                    Price = 4.50M,
                    Description = "Guaraná Fantástica (12 - 355 ml cans)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 20, Reserved = 0}
                },
                new Product
                {
                    Title = "NuNuCa Nuß-Nougat-Creme",
                    BasePrice = 14.00M,
                    Price = 14.00M,
                    Description = "NuNuCa Nuß-Nougat-Creme (20 - 450 g glasses)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 76, Reserved = 0}
                },
                new Product
                {
                    Title = "Gumbär Gummibärchen",
                    BasePrice = 31.23M,
                    Price = 31.23M,
                    Description = "Gumbär Gummibärchen (100 - 250 g bags)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 15, Reserved = 0}
                },
                new Product
                {
                    Title = "Schoggi Schokolade",
                    BasePrice = 43.90M,
                    Price = 43.90M,
                    Description = "Schoggi Schokolade (100 - 100 g pieces)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 49, Reserved = 0}
                },
                new Product
                {
                    Title = "Rössle Sauerkraut",
                    BasePrice = 45.60M,
                    Price = 45.60M,
                    Description = "Rössle Sauerkraut (25 - 825 g cans)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 26, Reserved = 0}
                },
                new Product
                {
                    Title = "Thüringer Rostbratwurst",
                    BasePrice = 123.79M,
                    Price = 123.79M,
                    Description = "Thüringer Rostbratwurst (50 bags x 30 sausgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 0, Reserved = 0}
                },
                new Product
                {
                    Title = "Nord-Ost Matjeshering",
                    BasePrice = 25.89M,
                    Price = 25.89M,
                    Description = "Nord-Ost Matjeshering (10 - 200 g glasses)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 10, Reserved = 0}
                },
                new Product
                {
                    Title = "Gorgonzola Telino",
                    BasePrice = 12.50M,
                    Price = 12.50M,
                    Description = "Gorgonzola Telino (12 - 100 g pkgs)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 0, Reserved = 0}
                },
                new Product
                {
                    Title = "Mascarpone Fabioli",
                    BasePrice = 32.00M,
                    Price = 32.00M,
                    Description = "Mascarpone Fabioli (24 - 200 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 9, Reserved = 0}
                },
                new Product
                {
                    Title = "Geitost",
                    BasePrice = 2.50M,
                    Price = 2.50M,
                    Description = "Geitost (500 g)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 112, Reserved = 0}
                },
                new Product
                {
                    Title = "Sasquatch Ale",
                    BasePrice = 14.00M,
                    Price = 14.00M,
                    Description = "Sasquatch Ale (24 - 12 oz bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 111, Reserved = 0}
                },
                new Product
                {
                    Title = "Steeleye Stout",
                    BasePrice = 18.00M,
                    Price = 18.00M,
                    Description = "Steeleye Stout (24 - 12 oz bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 20, Reserved = 0}
                },
                new Product
                {
                    Title = "Inlagd Sill",
                    BasePrice = 19.00M,
                    Price = 19.00M,
                    Description = "Inlagd Sill (24 - 250 g  jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 112, Reserved = 0}
                },
                new Product
                {
                    Title = "Gravad lax",
                    BasePrice = 26.00M,
                    Price = 26.00M,
                    Description = "Gravad lax (12 - 500 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 11, Reserved = 0}
                },
                new Product
                {
                    Title = "Côte de Blaye",
                    BasePrice = 263.50M,
                    Price = 263.50M,
                    Description = "Côte de Blaye (12 - 75 cl bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 17, Reserved = 0}
                },
                new Product
                {
                    Title = "Chartreuse verte",
                    BasePrice = 18.00M,
                    Price = 18.00M,
                    Description = "Chartreuse verte (750 cc per bottle)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 69, Reserved = 0}
                },
                new Product
                {
                    Title = "Boston Crab Meat",
                    BasePrice = 18.40M,
                    Price = 18.40M,
                    Description = "Boston Crab Meat (24 - 4 oz tins)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 123, Reserved = 0}
                },
                new Product
                {
                    Title = "Jack's New England Clam Chowder",
                    BasePrice = 9.65M,
                    Price = 9.65M,
                    Description = "Jack's New England Clam Chowder (12 - 12 oz cans)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 85, Reserved = 0}
                },
                new Product
                {
                    Title = "Singaporean Hokkien Fried Mee",
                    BasePrice = 14.00M,
                    Price = 14.00M,
                    Description = "Singaporean Hokkien Fried Mee (32 - 1 kg pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 26, Reserved = 0}
                },
                new Product
                {
                    Title = "Ipoh Coffee",
                    BasePrice = 46.00M,
                    Price = 46.00M,
                    Description = "Ipoh Coffee (16 - 500 g tins)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 17, Reserved = 0}
                },
                new Product
                {
                    Title = "Gula Malacca",
                    BasePrice = 19.45M,
                    Price = 19.45M,
                    Description = "Gula Malacca (20 - 2 kg bags)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 27, Reserved = 0}
                },
                new Product
                {
                    Title = "Rogede sild",
                    BasePrice = 9.50M,
                    Price = 9.50M,
                    Description = "Rogede sild (1k pkg.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 5, Reserved = 0}
                },
                new Product
                {
                    Title = "Spegesild",
                    BasePrice = 12.00M,
                    Price = 12.00M,
                    Description = "Spegesild (4 - 450 g glasses)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 95, Reserved = 0}
                },
                new Product
                {
                    Title = "Zaanse koeken",
                    BasePrice = 9.50M,
                    Price = 9.50M,
                    Description = "Zaanse koeken (10 - 4 oz boxes)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 36, Reserved = 0}
                },
                new Product
                {
                    Title = "Chocolade",
                    BasePrice = 12.75M,
                    Price = 12.75M,
                    Description = "Chocolade (10 pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 15, Reserved = 0}
                },
                new Product
                {
                    Title = "Maxilaku",
                    BasePrice = 20.00M,
                    Price = 20.00M,
                    Description = "Maxilaku (24 - 50 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 10, Reserved = 0}
                },
                new Product
                {
                    Title = "Valkoinen suklaa",
                    BasePrice = 16.25M,
                    Price = 16.25M,
                    Description = "Valkoinen suklaa (12 - 100 g bars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 65, Reserved = 0}
                },
                new Product
                {
                    Title = "Manjimup Dried Apples",
                    BasePrice = 53.00M,
                    Price = 53.00M,
                    Description = "Manjimup Dried Apples (50 - 300 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 20, Reserved = 0}
                },
                new Product
                {
                    Title = "Filo Mix",
                    BasePrice = 7.00M,
                    Price = 7.00M,
                    Description = "Filo Mix (16 - 2 kg boxes)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 38, Reserved = 0}
                },
                new Product
                {
                    Title = "Perth Pasties",
                    BasePrice = 32.80M,
                    Price = 32.80M,
                    Description = "Perth Pasties (48 pieces)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 0, Reserved = 0}
                },
                new Product
                {
                    Title = "Tourtière",
                    BasePrice = 7.45M,
                    Price = 7.45M,
                    Description = "Tourtière (16 pies)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 21, Reserved = 0}
                },
                new Product
                {
                    Title = "Pâté chinois",
                    BasePrice = 24.00M,
                    Price = 24.00M,
                    Description = "Pâté chinois (24 boxes x 2 pies)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 115, Reserved = 0}
                },
                new Product
                {
                    Title = "Gnocchi di nonna Alice",
                    BasePrice = 38.00M,
                    Price = 38.00M,
                    Description = "Gnocchi di nonna Alice (24 - 250 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 21, Reserved = 0}
                },
                new Product
                {
                    Title = "Ravioli Angelo",
                    BasePrice = 19.50M,
                    Price = 19.50M,
                    Description = "Ravioli Angelo (24 - 250 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 36, Reserved = 0}
                },
                new Product
                {
                    Title = "Escargots de Bourgogne",
                    BasePrice = 13.25M,
                    Price = 13.25M,
                    Description = "Escargots de Bourgogne (24 pieces)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 62, Reserved = 0}
                },
                new Product
                {
                    Title = "Raclette Courdavault",
                    BasePrice = 55.00M,
                    Price = 55.00M,
                    Description = "Raclette Courdavault (5 kg pkg.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 79, Reserved = 0}
                },
                new Product
                {
                    Title = "Camembert Pierrot",
                    BasePrice = 34.00M,
                    Price = 34.00M,
                    Description = "Camembert Pierrot (15 - 300 g rounds)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 19, Reserved = 0}
                },
                new Product
                {
                    Title = "Sirop d'érable",
                    BasePrice = 28.50M,
                    Price = 28.50M,
                    Description = "Sirop d'érable (24 - 500 ml bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 113, Reserved = 0}
                },
                new Product
                {
                    Title = "Tarte au sucre",
                    BasePrice = 49.30M,
                    Price = 49.30M,
                    Description = "Tarte au sucre (48 pies)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 17, Reserved = 0}
                },
                new Product
                {
                    Title = "Vegie-spread",
                    BasePrice = 43.90M,
                    Price = 43.90M,
                    Description = "Vegie-spread (15 - 625 g jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 24, Reserved = 0}
                },
                new Product
                {
                    Title = "Wimmers gute Semmelknödel",
                    BasePrice = 33.25M,
                    Price = 33.25M,
                    Description = "Wimmers gute Semmelknödel (20 bags x 4 pieces)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 22, Reserved = 0}
                },
                new Product
                {
                    Title = "Louisiana Fiery Hot Pepper Sauce",
                    BasePrice = 21.05M,
                    Price = 21.05M,
                    Description = "Louisiana Fiery Hot Pepper Sauce (32 - 8 oz bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 76, Reserved = 0}
                },
                new Product
                {
                    Title = "Louisiana Hot Spiced Okra",
                    BasePrice = 17.00M,
                    Price = 17.00M,
                    Description = "Louisiana Hot Spiced Okra (24 - 8 oz jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 4, Reserved = 0}
                },
                new Product
                {
                    Title = "Laughing Lumberjack Lager",
                    BasePrice = 14.00M,
                    Price = 14.00M,
                    Description = "Laughing Lumberjack Lager (24 - 12 oz bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 52, Reserved = 0}
                },
                new Product
                {
                    Title = "Scottish Longbreads",
                    BasePrice = 12.50M,
                    Price = 12.50M,
                    Description = "Scottish Longbreads (10 boxes x 8 pieces)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 6, Reserved = 0}
                },
                new Product
                {
                    Title = "Gudbrandsdalsost",
                    BasePrice = 36.00M,
                    Price = 36.00M,
                    Description = "Gudbrandsdalsost (10 kg pkg.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 26, Reserved = 0}
                },
                new Product
                {
                    Title = "Outback Lager",
                    BasePrice = 15.00M,
                    Price = 15.00M,
                    Description = "Outback Lager (24 - 355 ml bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 15, Reserved = 0}
                },
                new Product
                {
                    Title = "Flotemysost",
                    BasePrice = 21.50M,
                    Price = 21.50M,
                    Description = "Flotemysost (10 - 500 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 26, Reserved = 0}
                },
                new Product
                {
                    Title = "Mozzarella di Giovanni",
                    BasePrice = 34.80M,
                    Price = 34.80M,
                    Description = "Mozzarella di Giovanni (24 - 200 g pkgs.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 14, Reserved = 0}
                },
                new Product
                {
                    Title = "Röd Kaviar",
                    BasePrice = 15.00M,
                    Price = 15.00M,
                    Description = "Röd Kaviar (24 - 150 g jars)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 101, Reserved = 0}
                },
                new Product
                {
                    Title = "Longlife Tofu",
                    BasePrice = 10.00M,
                    Price = 10.00M,
                    Description = "Longlife Tofu (5 kg pkg.)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 4, Reserved = 0}
                },
                new Product
                {
                    Title = "Rhönbräu Klosterbier",
                    BasePrice = 7.75M,
                    Price = 7.75M,
                    Description = "Rhönbräu Klosterbier (24 - 0.5 l bottles)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 125, Reserved = 0}
                },
                new Product
                {
                    Title = "Lakkalikööri",
                    BasePrice = 18.00M,
                    Price = 18.00M,
                    Description = "Lakkalikööri (500 ml)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 57, Reserved = 0}
                },
                new Product
                {
                    Title = "Original Frankfurter grüne Soße",
                    BasePrice = 13.00M,
                    Price = 13.00M,
                    Description = "Original Frankfurter grüne Soße (12 boxes)",
                    ImageUri = "https://dummyimage.com/300x300.jpg",
                    Stock = new Stock {Total = 32, Reserved = 0}
                }
            };

            await _shopDbContext.Products.AddRangeAsync(products);
            await _shopDbContext.SaveChangesAsync();
        }
    }
}
