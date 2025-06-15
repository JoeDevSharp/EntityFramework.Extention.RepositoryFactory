# RepositoryFactory.EntityFramework

A lightweight, pragmatic implementation of the **generic repository pattern** with factory-based instantiation for **Entity Framework Core**.

Designed for maximum reusability, this library allows you to create and manage repositories decoupled from your business logic, with explicit control over the underlying `DbContext`.

---

## ✨ Features

- ✅ Generic repository with LINQ-based filtering and pagination  
- ✅ Centralized factory that manages `DbContext` lifecycle  
- ✅ Optional `UpdatedAt` auto-setting (if defined in the entity)  
- ✅ Works with any class-based entity (`class`)  
- ✅ Compatible with `.NET Standard 2.1+`, `.NET 6+`, `.NET 8`  
- ✅ Optional DI extension for `IServiceCollection`

---

## 🚀 Installation

```bash
dotnet add package RepositoryFactory.EntityFramework
