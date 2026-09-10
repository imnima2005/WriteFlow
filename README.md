# WriteFlow ✍️

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4?style=flat&logo=nuget)](https://learn.microsoft.com/ef/core/)
[![Architecture](https://img.shields.io/badge/Architecture-3--Layer%20Clean-brightgreen)](https://github.com/)

**WriteFlow** is a modern, responsive, and maintainable Content Management System (CMS) and blogging platform built with **ASP.NET Core (.NET 10)**. It is structured with a strict **3-Layer Architecture** combining **Razor Pages** on the client-facing UI and **MVC Areas** on the Admin Control Panel.

---

## 🏗 Architecture & Project Structure

The solution enforces a clean **Separation of Concerns (SoC)** across three primary layers:
```text
WriteFlow/
├── WriteFlow.Web/               # UI Layer (Presentation)
│   ├── Areas/Admin/             # Dedicated Admin Panel (MVC Pattern: Controllers, Models, Views)
│   ├── Pages/                   # Public-Facing Client Pages (Razor Pages: Index, Article, Search, Auth)
│   ├── wwwroot/                 # Static assets, uploads, styles & JS
│   └── Program.cs               # Service registrations, Auth, & Middleware pipeline
│
├── WriteFlow.CoreLayer/         # Business Logic Layer (Application & Domain)
│   ├── DTOs/                    # Data Transfer Objects
│   ├── Mappers/                 # Custom Entity-to-DTO Mapping profiles
│   ├── Services/                # Service implementations (Post, User, Category, Comment, etc.)
│   └── Utilities/               # Helpers (Pagination, Slugs, Persian Dates, File Manager)
│
└── WriteFlow.DataLayer/         # Data Access Layer (Infrastructure)
├── Context/                 # WriteFlowContext with Fluent API configuration
├── Entities/                # Base & Domain Entities (User, Post, Category, PostComment)
└── Migrations/              # EF Core Code-First Migrations
```

---

## 🌟 Key Features

### 👤 1. Authentication & Role-Based Authorization
- Cookie Authentication with persistent sessions (IsPersistent = true).
- Claim-Based Authorization (NameIdentifier, Name, Role).
- Role Hierarchy: Enums defining Admin, Writer, and User roles with area-level route protection (AdminControllerBase).

### 📰 2. Content & Article Management
- Rich-Text Content Creation: Integrated CKEditor 4 with dedicated secure asynchronous image uploading (/Upload/Article).
- SEO & Routing: Automatic URL slugification (ToSlug()) for dynamic SEO-friendly URLs.
- Article Discovery: Post visit tracker (visitCount), featured/special post flags (IsSpecial), and automated related articles recommendation engine.

### 🗂️ 3. Hierarchical Category System
- Recursive parent-child category tree.
- Recursive Soft-Delete Engine: Deleting a parent category recursively marks all descendants as deleted in a single transactional operation.

### 💬 4. Threaded Commenting System
- Secure user-associated comments linked directly to articles.
- Server-side validation and claim extraction via custom extension methods.

### ⚡ 5. Seamless Asynchronous Client UI (Razor Pages + AJAX)
- Dynamic partial page rendering (_LatestPosts, _PopularPosts, _SearchView) without full page reloads.
- AJAX Sliding-Window Pagination: Built-in BasePagination calculating start/end items, sliding bounds, and dynamic page buttons.

### 🛡️ 6. Data Integrity & File Management
- Global Query Filters: Automatic system-wide Soft Delete handling (.HasQueryFilter(x => !x.IsDelete)).
- Referential Integrity: Set all foreign key relations to DeleteBehavior.Restrict to eliminate accidental cascading deletions.
- Secure File Storage: File manager validating MIME types, image headers, unique GUID hashing, and obsolete image cleanup during updates.
- Persian Calendar & Humanized Timestamps: Custom Persian date conversion and relative “time-ago” calculation (e.g., ۳ روز پیش).

---

## 🛠 Tech Stack & Tools

- Backend: C# | .NET 10 | ASP.NET Core
- Data Access: Entity Framework Core 10 (Code-First)
- Database: Microsoft SQL Server
- UI & Templating: Razor Pages (Client) + MVC Areas (Admin Panel) + Partial Views
- Frontend / Client Interactivity: HTML5, CSS3, JavaScript (AJAX / Fetch API)
- Editor: CKEditor 4
- Security: Anti-Forgery Tokens ([ValidateAntiForgeryToken]), Cookie Authentication, Role-based Route Protection

### 🚀 Getting Started

1. Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or full instance)
2. Configuration
- Clone the repository and configure your database connection string in WriteFlow.Web/appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=WriteFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. Database Migration
Run the following command from the root directory:
```bash
dotnet ef database update --project WriteFlow.DataLayer --startup-project WriteFlow.Web
```

4. Run Application
```bash
dotnet run --project WriteFlow.Web
```
Navigate to https://localhost:5001 in your browser.
