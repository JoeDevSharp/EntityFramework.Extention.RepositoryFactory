# RepositoryFactory.EntityFramework

**RepositoryFactory.EntityFramework** is a lightweight, generic repository framework designed to simplify data access with Entity Framework Core. It provides a robust abstraction layer that supports both synchronous and asynchronous operations, targeting maintainable and scalable enterprise-grade applications.

---

## Core Components

### 1. **GenericRepository\<E>**

* Implements a generic repository pattern for any Entity Framework entity `E` constrained by `IEntityBase`.
* Supports comprehensive CRUD operations: Add, Update, Remove, Find, and Get with support for expression filters, eager loading (`include`), and pagination.
* Includes both synchronous and asynchronous variants of all key methods, facilitating flexible integration into modern async workflows.
* Enforces defensive programming with input validation and exception throwing to prevent runtime errors.
* Designed for seamless integration with any EF `DbContext`.

### 2. **RepositoryFactory\<C>**

* A factory class to instantiate `GenericRepository<E>` instances bound to a single EF `DbContext` of type `C`.
* Manages the lifecycle of the shared `DbContext`, implementing `IDisposable` for proper resource cleanup.
* Simplifies repository creation by encapsulating the context and ensuring consistent repository instantiation.

---

## Key Features

* **Entity-agnostic:** Works with any entity implementing the `IEntityBase` interface.
* **Sync & Async:** Full support for asynchronous programming patterns using `Task`-based methods.
* **Flexible Querying:** Supports LINQ expressions for filtering, eager loading with `Func<IQueryable<E>, IQueryable<E>>` includes, and paginated retrieval.
* **Safe Operations:** Throws explicit exceptions for invalid inputs such as null entities or empty collections.
* **Scoped Context Management:** Factory pattern ensures efficient and safe reuse of EF `DbContext` instances.

---

## Typical Usage Scenario

```csharp
using var factory = new RepositoryFactory<MyDbContext>();
var userRepository = factory.CreateRepository<User>();

// Check if user exists
if (!userRepository.Exists(u => u.Name == "John Doe"))
{
    userRepository.Add(new User { Name = "John Doe", Email = "john@example.com" });
    userRepository.Save();
}

// Retrieve and update
var user = userRepository.Find(u => u.Name == "John Doe");
if (user != null)
{
    user.Name = "John Doe Updated";
    userRepository.Update(user);
    userRepository.Save();
}
```

---

## Benefits for Software Engineers

* Accelerates development by abstracting repetitive EF Core CRUD operations.
* Promotes clean separation of concerns and testability.
* Enables smooth migration and modernization of legacy data access layers.
* Supports future-proof asynchronous programming models.
* Facilitates enterprise scalability through pagination and filtered queries.

---

If you seek a pragmatic, extensible repository solution for your EF Core projects that balances simplicity and functionality, **RepositoryFactory.EntityFramework** provides a solid foundation aligned with modern .NET best practices.

---

## Example: Using `RepositoryFactory` with Dependency Injection

This example demonstrates how to configure and use the generic repository framework with a DI container (`IServiceCollection`), Entity Framework Core `DbContext`, and asynchronous operations.

### Setup and Usage

```csharp
class Program
{
    static async Task Main(string[] args)
    {
        // 1. Configure DI container
        var services = new ServiceCollection();

        // Register your EF Core DbContext
        services.AddDbContext<AppDbContext>();

        // Register RepositoryFactory and GenericRepository with DI
        services.AddRepositoryFactory<AppDbContext>();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();

        // 2. Resolve the generic repository from DI
        var userRepository = serviceProvider.GetRequiredService<IGenericRepository<User>>();

        // 3. Check if a user exists asynchronously
        var exists = await userRepository.ExistsAsync(u => u.Name == "John Doe");

        // 4. Add a new user if not exists
        if (!exists)
        {
            await userRepository.AddAsync(new User { Name = "John Doe", Email = "john@example.com" });
            await userRepository.SaveAsync();
        }

        // 5. Retrieve the user and update asynchronously
        var user = await userRepository.FindAsync(u => u.Name == "John Doe");
        if (user != null)
        {
            user.Name = "John Doe Updated";
            userRepository.Update(user);
            await userRepository.SaveAsync();
        }

        // 6. Output the updated user's name
        Console.WriteLine($"User updated: {user?.Name}");
    }
}
```

### Explanation

* **Dependency Injection Setup:**

  * Registers the EF Core `AppDbContext` with the DI container.
  * Registers the `RepositoryFactory<AppDbContext>` and generic repositories via `AddRepositoryFactory<TContext>()` extension method.

* **Repository Resolution:**
  Retrieves an instance of `IGenericRepository<User>` from the DI container, enabling repository usage without manual instantiation.

* **CRUD Operations:**

  * Uses `ExistsAsync` to check if the user "John Doe" exists.
  * If not, adds the user asynchronously and saves changes.
  * Finds the user asynchronously, updates the name, and saves the changes.

* **Asynchronous Pattern:**
  All database calls are awaited to ensure non-blocking execution, following best practices for EF Core in modern applications.

---

This pattern ensures clean separation of concerns, testability, and scalability, leveraging DI and async features efficiently.

---

## Example: Using `RepositoryFactory` Without Dependency Injection

This example shows how to use the generic repository framework manually, without relying on a DI container. It demonstrates synchronous CRUD operations with a direct instantiation of `RepositoryFactory` and repositories.

### Code Example

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        // 1. Create a repository factory with your DbContext type
        var factory = new RepositoryFactory<AppContext>();

        // 2. Create a repository for the entity type User
        var userRepository = factory.CreateRepository<User>();

        // 3. Check synchronously if a user named "John Doe" exists
        var exist = userRepository.Exists(u => u.Name == "John Doe");

        // 4. If not existing, add the user and save changes
        if (exist is false)
        {
            userRepository.Add(new User { Name = "John Doe", Email = "" });
            userRepository.Save();
        }

        // 5. Find the user synchronously
        User? user = userRepository.Find(u => u.Name == "John Doe");

        if (user is null)
            return;

        // 6. Update the user and save changes
        user.Name = "John Doe Updated";
        userRepository.Update(user);
        userRepository.Save();

        // 7. Output the existence check result
        Console.WriteLine($"User exists: {exist}");
    }
}
```

### Explanation

* **Manual Instantiation:**
  `RepositoryFactory<AppContext>` is instantiated directly, managing the lifecycle of the `DbContext`.

* **Synchronous Operations:**
  The example uses synchronous methods `Exists`, `Add`, `Find`, `Update`, and `Save` to perform CRUD operations.

* **Workflow:**

  * Check if a user named "John Doe" exists in the database.
  * If not, create and save a new user.
  * Retrieve the user, update their name, and persist changes.
  * Finally, print whether the user existed before.

* **Use Case:**
  Useful in simple console apps or legacy projects where DI is not used or available.

---

This straightforward approach enables quick adoption of the repository pattern with minimal setup but lacks the flexibility and testability benefits provided by DI.

---

## 🌐 Database Provider Compatibility

`RepositoryFactory.EntityFramework` is compatible with the following Entity Framework Core providers:

* **SQL Server**
* **SQLite**
* **MySQL**
* **PostgreSQL**

Make sure the correct EF Core provider NuGet package is installed and properly configured in your application.
---
Por supuesto, aquí tienes la documentación de los métodos con ejemplos de uso para cada uno, en inglés y con un estilo profesional y claro:

---

## `GenericRepository<E>` Method Documentation with Usage Examples

This generic repository provides synchronous and asynchronous CRUD operations, filtering, pagination, and eager loading over an EF Core `DbContext`.

---

### Constructor

**`GenericRepository(DbContext context)`**
Creates a repository instance with the given EF Core context.

```csharp
var repository = new GenericRepository<User>(dbContext);
```

---

### Create Methods

**`void Add(E entity)`**
Adds a single entity to the context.

```csharp
var user = new User { Name = "John" };
repository.Add(user);
repository.Save();
```

**`Task AddAsync(E entity)`**
Asynchronously adds a single entity.

```csharp
var user = new User { Name = "Jane" };
await repository.AddAsync(user);
await repository.SaveAsync();
```

**`void AddRange(IEnumerable<E> entities)`**
Adds multiple entities.

```csharp
var users = new List<User> {
    new User { Name = "Alice" },
    new User { Name = "Bob" }
};
repository.AddRange(users);
repository.Save();
```

**`Task AddRangeAsync(IEnumerable<E> entities)`**
Asynchronously adds multiple entities.

```csharp
var users = new List<User> {
    new User { Name = "Charlie" },
    new User { Name = "Diana" }
};
await repository.AddRangeAsync(users);
await repository.SaveAsync();
```

---

### Read Methods

**`int Count(Expression<Func<E, bool>>? filter = null)`**
Returns the count of entities optionally filtered.

```csharp
int activeUserCount = repository.Count(u => u.IsActive);
int totalUsers = repository.Count();
```

**`Task<int> CountAsync(Expression<Func<E, bool>>? filter = null)`**
Asynchronously returns the count.

```csharp
int activeUserCount = await repository.CountAsync(u => u.IsActive);
int totalUsers = await repository.CountAsync();
```

**`bool Exists(Expression<Func<E, bool>> filter)`**
Checks if any entity matches the filter.

```csharp
bool hasAdmin = repository.Exists(u => u.Role == "Admin");
```

**`Task<bool> ExistsAsync(Expression<Func<E, bool>> filter)`**
Async version.

```csharp
bool hasAdmin = await repository.ExistsAsync(u => u.Role == "Admin");
```

**`E? Find(Expression<Func<E, bool>>? filter = null, Func<IQueryable<E>, IQueryable<E>>? include = null)`**
Finds a single entity optionally including navigation properties.

```csharp
var user = repository.Find(u => u.Email == "user@example.com", query => query.Include(u => u.Roles));
```

**`Task<E?> FindAsync(Expression<Func<E, bool>> filter, Func<IQueryable<E>, IQueryable<E>>? include = null)`**
Async version.

```csharp
var user = await repository.FindAsync(u => u.Email == "user@example.com", query => query.Include(u => u.Roles));
```

**`IEnumerable<E> Get(Expression<Func<E, bool>>? filter = null, int pageNumber = 1, int pageSize = 10, Func<IQueryable<E>, IQueryable<E>>? include = null)`**
Returns a paginated list optionally filtered and including related entities.

```csharp
var activeUsers = repository.Get(u => u.IsActive, pageNumber: 1, pageSize: 20);
var usersWithRoles = repository.Get(include: query => query.Include(u => u.Roles));
```

**`Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null, int pageNumber = 1, int pageSize = 10, Func<IQueryable<E>, IQueryable<E>>? include = null)`**
Async version.

```csharp
var activeUsers = await repository.GetAsync(u => u.IsActive, pageNumber: 1, pageSize: 20);
```

---

### Update Methods

**`void Update(E entity)`**
Updates an entity.

```csharp
user.Name = "Updated Name";
repository.Update(user);
repository.Save();
```

**`Task UpdateAsync(E entity)`**
Async version.

```csharp
user.Name = "Updated Name Async";
await repository.UpdateAsync(user);
await repository.SaveAsync();
```

---

### Delete Methods

**`void Remove(E entity)`**
Removes a single entity.

```csharp
repository.Remove(user);
repository.Save();
```

**`Task RemoveAsync(E entity)`**
Async version.

```csharp
await repository.RemoveAsync(user);
await repository.SaveAsync();
```

**`void RemoveRange(IEnumerable<E> entities)`**
Removes multiple entities.

```csharp
repository.RemoveRange(usersToDelete);
repository.Save();
```

**`Task RemoveRangeAsync(IEnumerable<E> entities)`**
Async version.

```csharp
await repository.RemoveRangeAsync(usersToDelete);
await repository.SaveAsync();
```

---

### Save Changes

**`void Save()`**
Saves changes synchronously.

```csharp
repository.Save();
```

**`Task SaveAsync()`**
Saves changes asynchronously.

```csharp
await repository.SaveAsync();
```


