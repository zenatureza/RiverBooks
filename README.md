# getting-started-modular-monoliths-in-net

### Creating migrations
```sh
# Creating the first UsersDbContext migration
 dotnet ef migrations add Initial -c UsersDbContext -p ..\RiverBooks.Users\RiverBooks.Users.csproj -s .\RiverBooks.Web.csproj -o Data/Migrations
```

### Running migrations
```sh
dotnet ef database update -c UsersDbContext
```