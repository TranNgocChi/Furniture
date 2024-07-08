# FURNITURE SHOP

We created this platform for buying and selling furniture items.   

## Technologies

ASP.NET CORE 8(Razor Pages), C#, SQL SERVER, GITHUB.

## Skills

OOP, Entity Framework(Code First), Three Layers, Repository,Singleton Design Pattern, Dependency Injection.  

## IDE

Visual Studio 2022, Sql Server 2019

## How to run source

- Clone src from github
- Go to appsettings in webapp and modify information to be suitable for your database.
- In DataAccess Layer, run some code lines to map code to db (Entity Framework).
  ```sh
   dotnet ef migrations  add "Initial"
   dotnet ef database update
- Run src
## How to use Github

To install this project, follow these steps:

1. Clone the repository:
   ```sh
   git clone https://github.com/TranNgocChi/FurnitureShop.git

2. When clone done, create a branch base on master:
   ```sh
   git checkout -b NameBranch
   
2. Implement Coding, Push to created branch (Just push necessary files, not push unnecessary files):
   ```sh
   git add .
   git commit -m"Push Code"
   git push origin NameBranch

2. Create merge request on github:
   - Go to github
   - Choose "Pull Request"
   - Choose "New Pull Request"
   - Choose based on branch is "master" and compare branch is "branch to merge"

## Database Initialization
1. Create data for categories
```sh
INSERT INTO Categories (Id, CategoryName) VALUES
(NEWID(), 'Chairs'),
(NEWID(), 'Tables'),
(NEWID(), 'Sofas'),
(NEWID(), 'Beds'),
(NEWID(), 'Cabinets'),
(NEWID(), 'Desks'),
(NEWID(), 'Bookshelves'),
(NEWID(), 'Dining Sets'),
(NEWID(), 'Wardrobes'),
(NEWID(), 'Outdoor Furniture');
```

2. Create data for Products
```sh
INSERT INTO Products (Id, ProductName, ProductDescription, ProductPrice, Quantity, ProductImage, CategoryId) VALUES
(NEWID(), 'Wooden Chair', 'A sturdy wooden chair', 49.99, 10, 'wooden_chair.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Chairs')),
(NEWID(), 'Glass Dining Table', 'A sleek glass dining table', 199.99, 5, 'glass_dining_table.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Tables')),
(NEWID(), 'Leather Sofa', 'A comfortable leather sofa', 599.99, 3, 'leather_sofa.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Sofas')),
(NEWID(), 'King Size Bed', 'A spacious king size bed', 499.99, 2, 'king_size_bed.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Beds')),
(NEWID(), 'Oak Cabinet', 'A large oak cabinet', 299.99, 4, 'oak_cabinet.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Cabinets')),
(NEWID(), 'Modern Desk', 'A modern office desk', 149.99, 7, 'modern_desk.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Desks')),
(NEWID(), 'Bookshelf', 'A tall bookshelf', 89.99, 8, 'bookshelf.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Bookshelves')),
(NEWID(), 'Dining Set', 'A dining set with table and 4 chairs', 399.99, 2, 'dining_set.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Dining Sets')),
(NEWID(), 'Wardrobe', 'A spacious wardrobe', 349.99, 6, 'wardrobe.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Wardrobes')),
(NEWID(), 'Outdoor Patio Set', 'A patio set for outdoor use', 259.99, 3, 'outdoor_patio_set.jpg', (SELECT Id FROM Categories WHERE CategoryName = 'Outdoor Furniture'));
```