# 🎓 Student Management System

A comprehensive ASP.NET Core MVC web application for managing students, courses, and branches in educational institutions.

## 🚀 Features

### Core Functionality
- ✅ **Student Management** - Full CRUD operations for students
- ✅ **Course Management** - Create and manage academic courses
- ✅ **Branch Management** - Handle different educational branches
- ✅ **Relationships** - Establish connections between students, courses, and branches

### Technical Features
- 🏗 **Repository Pattern** - Clean separation of data access layer
- 🎯 **Service Layer** - Business logic separation
- 💉 **Dependency Injection** - Proper dependency management
- 🎨 **Bootstrap UI** - Responsive and modern user interface
- 🔒 **Security** - Anti-forgery tokens and input validation
- 📊 **Entity Framework Core** - ORM with SQL Server
- 🗃 **Migrations** - Database version control

### Advanced Architecture
- 📝 **Logging** - Comprehensive application logging
- ⚡ **Caching** - Memory caching for performance
- 🛡 **Exception Handling** - Global error handling middleware
- 🧪 **Testable Design** - Built with unit testing in mind

## 🛠 Technology Stack

- **Backend**: ASP.NET Core 8.0 MVC
- **Frontend**: Bootstrap 5.3, Font Awesome
- **Database**: SQL Server with Entity Framework Core
- **Architecture**: Repository Pattern, Service Layer, Dependency Injection
- **Security**: Anti-Forgery Tokens, Input Validation

## 📁 Project Structure

```
StudentManagementSystem/
├── Controllers/          # MVC Controllers
├── Models/              # Entity Models
├── Views/               # Razor Views
├── Services/            # Business Logic Layer
├── Interfaces/          # Repository Contracts
├── Repositories/        # Data Access Layer
├── Context/             # DbContext Configuration
├── Middlewares/         # Custom Middlewares
└── wwwroot/            # Static Files
```

## 🗂 Database Models

### Student
- `Id` (PK, Identity)
- `Name` (Required, NVARCHAR(100))
- `Address` (Nullable)
- `CreatedAt` (DateTime)
- `CourseId` (FK)
- `BranchId` (FK)

### Course
- `Id` (PK, Identity starting from 100, increment by 100)
- `Title` (Required, NVARCHAR(50))
- `CreatedAt` (DateTime)
- `BranchId` (FK)

### Branch
- `Id` (PK, Identity starting from 1001)
- `Title` (Required, NVARCHAR(50))
- `Location` (Required, NVARCHAR(100))

## 🔗 Relationships

- **Student M:1 Course** - Many students can belong to one course
- **Student M:1 Branch** - Many students can belong to one branch  
- **Course 1:1 Branch** - One course belongs to one branch

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/NadaAshraf12/StudentManagementSystem.git
   cd StudentManagementSystem
   ```

2. **Configure database connection**
   Update `appsettings.json` with your SQL Server connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=your-server;Database=StudentManagementDB;Trusted_Connection=true;"
   }
   ```

3. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   Navigate to `https://localhost:7000`

### Usage

1. **Manage Branches**: Create educational branches with locations
2. **Manage Courses**: Set up courses and assign to branches
3. **Manage Students**: Add students with course and branch assignments
4. **View Relationships**: See how entities are connected

## 🎯 API Endpoints

### Student Controller
- `GET /Student` - List all students
- `GET /Student/Create` - Create student form
- `POST /Student/Create` - Create new student
- `GET /Student/Edit/{id}` - Edit student form
- `POST /Student/Edit/{id}` - Update student
- `GET /Student/Delete/{id}` - Delete confirmation
- `POST /Student/Delete/{id}` - Delete student

### Course Controller  
- `GET /Course` - List all courses with student counts
- `GET /Course/Create` - Create course form
- `POST /Course/Create` - Create new course
- `GET /Course/Edit/{id}` - Edit course form
- `POST /Course/Edit/{id}` - Update course
- `GET /Course/Delete/{id}` - Delete confirmation
- `POST /Course/Delete/{id}` - Delete course

### Branch Controller
- `GET /Branch` - List all branches
- `GET /Branch/Create` - Create branch form
- `POST /Branch/Create` - Create new branch
- `GET /Branch/Edit/{id}` - Edit branch form
- `POST /Branch/Edit/{id}` - Update branch
- `GET /Branch/Delete/{id}` - Delete confirmation (with safety checks)
- `POST /Branch/Delete/{id}` - Delete branch (if no students)

## 🏗 Architecture Patterns

### Repository Pattern
```csharp
public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student> GetByIdAsync(int id);
    Task<Student> AddAsync(Student student);
    Task<Student> UpdateAsync(Student student);
    Task DeleteAsync(int id);
}
```

### Service Layer
```csharp
public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(int id);
    Task<Student> CreateStudentAsync(Student student);
    Task<Student> UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
}
```

### Dependency Injection
```csharp
// Program.cs
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
```

## 🔧 Configuration

### AppSettings
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=StudentManagementDB;Trusted_Connection=true;"
  }
}
```

## 🧪 Testing

The application is built with testability in mind:
- Repository pattern allows mocking data access
- Service layer enables business logic testing
- Dependency injection supports unit testing

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## 👥 Authors

-NadaAshraf12  - [GitHub Profile](https://github.com/NadaAshraf12)

## 🙏 Acknowledgments

- ASP.NET Core Team
- Bootstrap Team
- Entity Framework Core Team

---

<div align="center">

**⭐ Star this repo if you find it helpful!**

</div>

## 📞 Support

If you have any questions or need help with setup, please open an issue on GitHub.

---

**Happy Coding!** 🚀
