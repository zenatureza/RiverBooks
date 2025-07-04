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

### Running redis:
```sh
docker run --name my-redis -p 6379:6379 -d redis
```

### Running papercut
```sh
docker run --name=papercut -p 25:25 -p 37408:37408 -d jijiechen/papercut:latest
```

### Running mongodb
```sh
docker run --name=mongodb -d -p 27017:27017 mongo
```
