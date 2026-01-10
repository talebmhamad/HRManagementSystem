#  HR Management System
**A Clean Architecture Backend with MVC Admin UI and Mobile (Flutter) Integration**

---

##  Project Overview

The **HR Management System** is a robust **ASP.NET Core MVC** backend application designed to manage core enterprise HR operations such as **employees, departments, contracts, attendance, and user authentication**.

The project adopts a **Hybrid Data Access approach (Entity Framework Core + ADO.NET)** to balance **developer productivity** with **high-performance database operations**. It is intentionally designed to be **API-ready**, allowing seamless integration with a **Flutter mobile application** or any other front-end client.


---

##  Global Architecture

The system follows an **N-Tier Layered Architecture** to ensure maintainability, scalability, and testability.

```
Presentation Layer (Web / API)
↓
Business Logic Layer (Services)
↓
Data Access Layer (Repositories)
↓
Persistence Layer (SQL Server)
```

### Architectural Layers

- **Presentation (Web):** ASP.NET MVC for admin management + REST APIs for Flutter clients
- **Business Logic (Services):** Validations, workflows, and business rules
- **Data Access (Repositories):** Abstracted persistence using Repository Pattern
- **Persistence (Data):** SQL Server managed via EF Core and optimized with ADO.NET

---

##  Visual Architecture Diagram (Conceptual)

```
[ Flutter App ]        [ Web Browser ]
        │                     │
        └────── HTTP / JSON ──┘
                    ↓
            Controllers (MVC / API)
                    ↓
               Services Layer
                    ↓
             Repositories Layer
                    ↓
              SQL Server DB
```

 This diagram highlights how both **Web UI and Flutter mobile clients** consume the same backend logic.

---

##  Technology Stack

| Category | Technology |
|--------|------------|
| Framework | ASP.NET Core 8.0 (MVC & Web API) |
| Language | C# |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Low-level Data Access | ADO.NET |
| Mapping | Manual Mappers |
| UI | Razor Views + Bootstrap |
| Authentication | Cookie-based (JWT planned) |

---

##  Project Structure

```
src/
├── HRManagementSystem.Data/          # Entities, DbContext, DTOs
├── HRManagementSystem.Repositories/  # Repository Interfaces & Implementations
├── HRManagementSystem.Services/      # Service Interfaces & Implementations, Business Logic, Mappers
└── HRManagementSystem.Web/           # MVC Controllers, API Endpoints, Views, Middleware
```

---

##  Layer Responsibilities (Concise)

### 1️ Data Layer
- Database entities
- EF Core DbContext
- DTO definitions

### 2️ Repository Layer
- Interfaces + implementations
- EF Core for CRUD operations
- ADO.NET for optimized queries (counts, dashboards)

### 3️ Service Layer
- Business rules & validation
- Interfaces + implementations
- Coordinates repositories
- Maps Entities ↔ DTOs

### 4️ Web Layer
- MVC Controllers & API endpoints
- ViewModels & Razor UI
- Global exception middleware
- Maps Models ↔ DTOs

---

##  Data Schema Overview

**Core Relationships:**

- **Department → Employees** (One-to-Many)
- **Employee → Contract** (One-to-One, only one active contract)
- **Employee → Attendance** (One-to-Many)
- **Employee → User Account** (Optional One-to-One)

 These relationships enforce real HR constraints at the business logic level.

---

##  Why EF Core + ADO.NET Are Both Used

- **Entity Framework Core** is used for:
  - Standard CRUD operations
  - Relationship management
  - Rapid development and maintainability

- **ADO.NET** is used for:
  - Aggregations (counts, summaries)
  - Performance-critical queries
  - Full control over SQL execution


---

##  API Documentation (Mobile-Ready Proof)

The backend exposes API-ready endpoints that can be consumed by a **Flutter application**.

### Example Endpoints

- `GET /api/employees` → List all employees
- `GET /api/attendance?startDate=&endDate=` → Attendance records
- `POST /api/attendance/check` → Check-in / Check-out
- `POST /auth/login` → User authentication

 The same service layer is shared between MVC and API endpoints.

---

##  Security Considerations

- Password hashing using `IPasswordHasher`
- Claims-based authentication
- Global exception handling middleware
- Future-ready for JWT-based mobile authentication

---

##  Future Work

- Advanced attendance analytics & reports
- Unit & integration testing (xUnit)
- Role-based authorization policies
- Cloud deployment (Azure / AWS)

---

## Setup & Installation

```bash
# Clone repository
git clone <repository-url>

# Apply migrations
dotnet ef database update

# Run application
dotnet run --project HRManagementSystem.Web
```

Default entry point:
```
/Auth/Index
```

---

##  Author

**Project:** HR Management System  
**Level:** Master 2  
**Field:** Web Development

---
