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

   