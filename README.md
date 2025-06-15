# RepositoryFactory.EntityFramework

A lightweight, generic repository abstraction over Entity Framework Core supporting both synchronous and asynchronous operations. Provides optional integration with `IServiceCollection`.

---

## 📦 Features

* Generic repository pattern for any EF `DbContext`
* Supports **CRUD**, pagination, includes, and expression filters
* Both **sync** and **async** APIs
* Defensive programming with exception handling
* Optional integration via **DI extensions**

---

## 🛠 Installation

```bash
dotnet add package RepositoryFactory.EntityFramework
```

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

## 🧱 Base Entity Requirement

In order to use the repository, your entities **must implement** the `IEntityBase` interface. The recommended pattern is to create a common base entity:

```csharp
public class EntityBase : IEntityBase
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
```

All your EF entities should then inherit from this base class:

```csharp
public class User : EntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public bool IsActive { get; set; }
}
```

This ensures the repository correctly identifies the entity type and supports base-level properties like `Id`, `CreatedAt`, and `UpdatedAt`.

---

## 🌐 Database Provider Compatibility

`RepositoryFactory.EntityFramework` is compatible with the following Entity Framework Core providers:

* **SQL Server**
* **SQLite**
* **MySQL**
* **PostgreSQL**

Make sure the correct EF Core provider NuGet package is installed and properly configured in your application.
