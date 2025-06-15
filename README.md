# RepositoryFactory.For.EntityFramework

A lightweight, generic repository abstraction over Entity Framework Core supporting both synchronous and asynchronous operations. Provides optional integration with `IServiceCollection`.

---

## 📦 Features

- Generic repository pattern for any EF `DbContext`
- Supports **CRUD**, pagination, includes, and expression filters
- Both **sync** and **async** APIs
- Defensive programming with exception handling
- Optional integration via **DI extensions**

---

## 🛠 Installation

```bash
dotnet add package RepositoryFactory.For.EntityFramework
````

---

## 🔧 Interface

```csharp
public interface IGenericRepository<E> where E : class, IEntityBase
```

### Supports

* `Add`, `AddRange`, `AddAsync`, `AddRangeAsync`
* `Find`, `FindAsync`, `Get`, `GetAsync`
* `Count`, `CountAsync`
* `Exists`, `ExistsAsync`
* `Update`, `UpdateAsync`
* `Remove`, `RemoveRange`, `RemoveAsync`, `RemoveRangeAsync`
* `Save`, `SaveAsync`

---

## ⚙️ Usage

### ✅ With `ServiceCollectionExtensions`

#### 1. Register in `Program.cs`

```csharp
using JoeDevSharp.RepositoryFactory.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

builder.Services.AddDbContext<MyAppContext>(options =>
    options.UseSqlServer("your_connection_string"));

builder.Services.AddRepositories(); // Registers GenericRepository<T>
```

#### 2. Inject in your services

```csharp
public class UserService
{
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _userRepository.GetAsync(u => u.IsActive);
    }
}
```

---

### 🧩 Without DI (`ServiceCollectionExtensions`)

#### 1. Instantiate manually

```csharp
using JoeDevSharp.RepositoryFactory.EntityFramework.Core;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

var context = new MyAppContext();
var userRepository = new GenericRepository<User>(context);

var users = userRepository.Get(u => u.IsActive);
```

#### 2. Async version

```csharp
var users = await userRepository.GetAsync(u => u.IsActive);
```

---

## 🧪 Sample Entity

```csharp
public class User : IEntityBase
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
```

---

## 📁 Project Structure

```
RepositoryFactory.For.EntityFramework/
├── Interfaces/
│   └── IGenericRepository.cs
├── Core/
│   ├── GenericRepository.cs
│   └── RepositoryFactory.cs
├── Extensions/
│   └── ServiceCollectionExtensions.cs
├── README.md
├── LICENSE
├── RepositoryFactory.For.EntityFramework.csproj
```

---

## 🔒 License

This project is licensed under the MIT License.

```

---

¿Deseas que lo prepare también en formato `.md` descargable o quieres una versión en francés/español para la documentación de tu repositorio?
```
