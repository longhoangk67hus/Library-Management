# 📚 Library Management System

Hệ thống quản lý thư viện hiện đại được xây dựng với .NET Core Web API và React.js, hỗ trợ quản lý sách, người dùng, và các hoạt động mượn/trả sách.

## Kiến trúc hệ thống

### **Tổng quan Architecture**
Hệ thống được thiết kế theo mô hình **Layered Architecture** với các thành phần:

```
┌─────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                        │
│  ┌─────────────────┐           ┌─────────────────────────┐   │
│  │   React.js      │           │    .NET Core Web API    │   │
│  │   Frontend      │    HTTP   │      Controllers        │   │
│  │   (Port 3000)   │ ◄────────►│     (Port 5021)         │   │
│  └─────────────────┘           └─────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                                           │
┌─────────────────────────────────────────────────────────────┐
│                   BUSINESS LOGIC LAYER                      │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────────────┐  │
│  │   AuthBL    │  │   BookBL    │  │   BorrowRecordBL    │  │
│  │             │  │             │  │                     │  │
│  └─────────────┘  └─────────────┘  └─────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                                           │
┌─────────────────────────────────────────────────────────────┐
│                   DATA ACCESS LAYER                         │
│  ┌─────────────┐           ┌─────────────────────────────┐   │
│  │   BaseDL    │           │      DatabaseContext        │   │
│  │   (Generic) │           │     (Entity Framework)      │   │
│  └─────────────┘           └─────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                                           │
┌─────────────────────────────────────────────────────────────┐
│                      DATABASE LAYER                         │
│                    SQL Server Database                      │
└─────────────────────────────────────────────────────────────┘
```

### **Backend Architecture (.NET Core)**

#### **1. Project Structure**
```
LibraryManagement/
├── LibraryManagement.API/          # Web API Controllers
├── LibraryManagement.BL/           # Business Logic Layer
├── LibraryManagement.DL/           # Data Access Layer  
├── LibraryManagement.Entities/     # Domain Models & DTOs
└── LibraryManagement.Common/       # Shared Utilities

BaseCoreService/                    # Shared Core Services
├── BaseCoreService.API/            # Base API Controllers
├── BaseCoreService.BL/             # Base Business Logic
├── BaseCoreService.DL/             # Base Data Access
├── BaseCoreService.Entities/       # Base Entities
├── BaseCoreService.Authen/         # Authentication Module
└── BaseCoreService.Common/         # Common Utilities
```

#### **2. Design Patterns**
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: Loose coupling between layers
- **Generic Base Classes**: Code reusability
- **DTO Pattern**: Data transfer objects
- **Service Layer Pattern**: Business logic encapsulation

### **Frontend Architecture (React.js)**

#### **1. Component Structure**
```
src/
├── components/                     # Reusable Components
│   ├── Layout/                    # Header, Sidebar, Layout
│   ├── Books/                     # Book Management
│   ├── BorrowReturn/              # Borrow/Return Features
│   └── Dashboard/                 # Dashboard Components
├── pages/                         # Page Components (JSX)
│   ├── Login.jsx                  # Authentication Page
│   ├── BookList.jsx               # Book Listing
│   └── AdminBooks.jsx             # Admin Book Management
├── services/                      # API Services
├── utils/                         # Utility Functions
└── styles/                        # CSS Styles
```

#### **2. State Management**
- **React Hooks**: useState, useEffect, useCallback
- **Local Storage**: Data persistence
- **Context API**: User authentication state

## Sơ đồ cơ sở dữ liệu

### **Entity Relationship Diagram**

```
┌─────────────────────┐         ┌─────────────────────┐         ┌─────────────────────┐
│        USER         │         │    BORROW_RECORD    │         │        BOOK         │
├─────────────────────┤         ├─────────────────────┤         ├─────────────────────┤
│ Id (PK)            │◄────────┤ Id (PK)            │────────►│ Id (PK)            │
│ Username (UK)      │    1    │ UserId (FK)        │    1    │ Title              │
│ Password           │         │ BookId (FK)        │         │ Author             │
│ FullName           │         │ BorrowDate         │         │ Category           │
│ Email              │         │ DueDate            │         │ PublishYear        │
│ Phone              │         │ ReturnDate         │         │ Publisher          │
│ Role               │         │ Status             │         │ TotalCopies        │
│ CreatedAt          │         │ Notes              │         │ AvailableCopies    │
│ UpdatedAt          │         │ CreatedAt          │         │ Description        │
└─────────────────────┘         └─────────────────────┘         │ CreatedAt          │
                                                                │ UpdatedAt          │
                                                                └─────────────────────┘
                                                                         ▲
                                                                         │
                                                                         │ 1:N
                                                                         │
┌─────────────────────┐                                         ┌─────────────────────┐
│       AUTHOR        │                                         │      CATEGORY       │
├─────────────────────┤                                         ├─────────────────────┤
│ Id (PK)            │─────────────────────────────────────────►│ Id (PK)            │
│ Name               │                    1:N                   │ Name               │
│ Biography          │                                          │ Description        │
│ BirthDate          │                                          │ CreatedAt          │
│ Nationality        │                                          └─────────────────────┘
│ CreatedAt          │
└─────────────────────┘

Relationships:
• USER (1) ──── (N) BORROW_RECORD: Một user có thể mượn nhiều sách
• BOOK (1) ──── (N) BORROW_RECORD: Một sách có thể được mượn nhiều lần
• AUTHOR (1) ──── (N) BOOK: Một tác giả có thể viết nhiều sách  
• CATEGORY (1) ──── (N) BOOK: Một thể loại có thể có nhiều sách
```
```

### **Database Schema Details**

#### **Users Table**
| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Id | int | PK, Identity | Khóa chính |
| Username | nvarchar(50) | Unique, Not Null | Tên đăng nhập |
| Password | nvarchar(255) | Not Null | Mật khẩu (đã hash) |
| FullName | nvarchar(100) | Not Null | Họ tên đầy đủ |
| Email | nvarchar(100) | Unique | Email |
| Phone | nvarchar(20) | | Số điện thoại |
| Role | nvarchar(20) | Default: 'User' | Vai trò (Admin/User) |

#### **Books Table**
| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Id | int | PK, Identity | Khóa chính |
| Title | nvarchar(200) | Not Null | Tên sách |
| Author | nvarchar(100) | Not Null | Tác giả |
| Category | nvarchar(50) | | Thể loại |
| PublishYear | int | | Năm xuất bản |
| Publisher | nvarchar(100) | | Nhà xuất bản |
| TotalCopies | int | Default: 1 | Tổng số bản |
| AvailableCopies | int | Default: 1 | Số bản có sẵn |

#### **BorrowRecords Table**
| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Id | int | PK, Identity | Khóa chính |
| UserId | int | FK to Users | ID người mượn |
| BookId | int | FK to Books | ID sách |
| BorrowDate | datetime | Not Null | Ngày mượn |
| DueDate | datetime | Not Null | Ngày hạn trả |
| ReturnDate | datetime | Nullable | Ngày trả thực tế |
| Status | nvarchar(20) | Default: 'Borrowed' | Trạng thái |

## Sơ đồ lớp (Class Diagram)

### **Backend Classes**

```
                    ┌─────────────────────────┐
                    │   <<interface>>         │
                    │     IBaseBL<T>         │
                    ├─────────────────────────┤
                    │ + GetAllAsync()        │
                    │ + GetByIdAsync()       │
                    │ + CreateAsync()        │
                    │ + UpdateAsync()        │
                    │ + DeleteAsync()        │
                    └─────────────────────────┘
                                ▲
                                │ implements
                                │
                    ┌─────────────────────────┐
                    │   <<abstract>>          │
                    │     BaseBL<T>          │
                    ├─────────────────────────┤
                    │ # _baseDL: IBaseDL<T>  │
                    │ + GetAllAsync()        │
                    │ + GetByIdAsync()       │
                    │ + CreateAsync()        │
                    │ + UpdateAsync()        │
                    │ + DeleteAsync()        │
                    └─────────────────────────┘
                                ▲
                ┌───────────────┼───────────────┐
                │               │               │
    ┌─────────────────┐ ┌─────────────────┐ ┌─────────────────────┐
    │     BookBL      │ │     AuthBL      │ │   BorrowRecordBL    │
    ├─────────────────┤ ├─────────────────┤ ├─────────────────────┤
    │ + GetByCategory │ │ + Login()       │ │ + BorrowBook()      │
    │ + SearchBooks() │ │ + Register()    │ │ + ReturnBook()      │
    │ + UpdateCopies()│ │ + ValidateToken │ │ + GetUserBorrows()  │
    └─────────────────┘ └─────────────────┘ └─────────────────────┘
             │                   │                       │
             ▼                   ▼                       ▼
    ┌─────────────────┐ ┌─────────────────┐ ┌─────────────────────┐
    │      Book       │ │      User       │ │   BorrowRecord      │
    ├─────────────────┤ ├─────────────────┤ ├─────────────────────┤
    │ + Id            │ │ + Id            │ │ + Id                │
    │ + Title         │ │ + Username      │ │ + UserId            │
    │ + Author        │ │ + Password      │ │ + BookId            │
    │ + Category      │ │ + FullName      │ │ + BorrowDate        │
    │ + PublishYear   │ │ + Email         │ │ + DueDate           │
    │ + TotalCopies   │ │ + Role          │ │ + ReturnDate        │
    │ + AvailableCopies│ └─────────────────┘ │ + Status            │
    └─────────────────┘                     └─────────────────────┘
```

### **Frontend Components**

```
                         ┌─────────────────────────┐
                         │          App            │
                         ├─────────────────────────┤
                         │ + state: AuthState     │
                         │ + renderCurrentPage()  │
                         │ + handleLogin()        │
                         │ + handleLogout()       │
                         └─────────────────────────┘
                                      │
                    ┌─────────────────┼─────────────────┐
                    ▼                 ▼                 ▼
        ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐
        │     Layout      │  │   Dashboard     │  │   LoginPage     │
        ├─────────────────┤  ├─────────────────┤  ├─────────────────┤
        │ + props: user   │  │ + props: role   │  │ + state: form   │
        │ + renderHeader()│  │ + loadStats()   │  │ + handleLogin() │
        │ + renderSidebar │  │ + quickActions()│  │ + handleRegister│
        └─────────────────┘  └─────────────────┘  │ + validateForm()│
                │                                  └─────────────────┘
      ┌─────────┼─────────┐
      ▼         ▼         ▼
┌──────────┐ ┌──────────┐ ┌──────────────────┐
│  Header  │ │ Sidebar  │ │ BooksManagement  │
│          │ │          │ ├──────────────────┤
└──────────┘ └──────────┘ │ + state: books   │
                          │ + loadBooks()    │
                          │ + handleCRUD()   │
                          │ + filterBooks()  │
                          └──────────────────┘
                                   │
                          ┌────────▼────────┐
                          │  BorrowReturn   │
                          ├─────────────────┤
                          │ + borrowRecords │
                          │ + handleBorrow()│
                          │ + handleReturn()│
                          │ + loadRecords() │
                          └─────────────────┘
```
```

## Hệ thống phân quyền

### **Roles & Permissions**

| Role | Permissions | Features Access |
|------|-------------|----------------|
| **Admin** | Full CRUD operations | • Quản lý sách<br>• Quản lý người dùng<br>• Xem báo cáo<br>• Quản lý mượn/trả |
| **User** | Limited operations | • Xem danh sách sách<br>• Mượn sách<br>• Trả sách<br>• Xem lịch sử cá nhân |

### **Authentication Flow**
```
[User Login] → [Validate Credentials] → [Generate JWT Token] → [Store in LocalStorage] → [Access Protected Routes]
```

## Hướng dẫn chạy chương trình

### **Yêu cầu hệ thống**
- **.NET Core 8.0** hoặc mới hơn
- **Node.js 16+** và **npm**
- **SQL Server** (LocalDB hoặc Express)
- **Visual Studio 2022** hoặc **VS Code**

### **1. Setup Backend (.NET Core API)**

#### **Bước 1: Clone repository**
```bash
git clone https://github.com/longhoangk67hus/Library-Management.git
cd Library-Management
```

#### **Bước 2: Restore packages**
```bash
cd LibraryManagement/LibraryManagement.API
dotnet restore
```

#### **Bước 3: Update Database Connection**
Chỉnh sửa `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementDB;Trusted_Connection=true;"
  },
  "Jwt": {
    "Key": "your-secret-key-here",
    "Issuer": "LibraryManagement",
    "Audience": "LibraryManagementUsers",
    "Expired": "24"
  }
}
```

#### **Bước 4: Run Database Migrations**
```bash
dotnet ef database update
```

#### **Bước 5: Start Backend Server**
```bash
dotnet run
```
Backend sẽ chạy tại: `https://localhost:5021`

### **2. Setup Frontend (React.js)**

#### **Bước 1: Navigate to Client folder**
```bash
cd "LibraryManagement - Client"
```

#### **Bước 2: Install dependencies**
```bash
npm install
```

#### **Bước 3: Start Development Server**
```bash
npm start
```
Frontend sẽ chạy tại: `http://localhost:3000`

### **3. Demo Accounts**

| Username | Password | Role | Access Level |
|----------|----------|------|-------------|
| `admin` | `123456` | Admin | Full access |
| `long` | `123456` | User | Limited access |

### **4. API Documentation**

#### **Authentication Endpoints**
```
POST /api/v1/Authens/login
POST /api/v1/Authens/user/register
```

#### **Books Endpoints**
```
GET    /api/v1/Books              # Get all books
GET    /api/v1/Books/{id}         # Get book by ID
POST   /api/v1/Books              # Create new book (Admin)
PUT    /api/v1/Books/{id}         # Update book (Admin)
DELETE /api/v1/Books/{id}         # Delete book (Admin)
```

#### **Borrow Records Endpoints**
```
GET    /api/v1/BorrowRecords      # Get borrow records
POST   /api/v1/BorrowRecords      # Borrow a book
PUT    /api/v1/BorrowRecords/{id} # Return a book
```

## Tính năng chính

### **✅ Đã hoàn thành**
- [x] Hệ thống đăng nhập/đăng ký
- [x] Phân quyền Admin/User
- [x] Quản lý sách (CRUD)
- [x] Mượn/trả sách
- [x] Dashboard thống kê
- [x] Responsive design
- [x] Data persistence (localStorage)


## Technologies Stack

### **Backend**
- **.NET Core 8.0** - Web API Framework
- **Entity Framework Core** - ORM
- **SQL Server** - Database
- **JWT Authentication** - Security
- **Swagger** - API Documentation

### **Frontend**
- **React.js 18** - UI Framework
- **JavaScript (ES6+)** - Programming Language
- **CSS3** - Styling
- **LocalStorage** - Client-side Storage
- **Responsive Design** - Mobile-friendly

### **Development Tools**
- **Visual Studio 2022** - IDE
- **VS Code** - Code Editor
- **Postman** - API Testing
- **Git** - Version Control

## Contributors

- **Hoàng Ngọc Long**
- **Email**: hoangngoclong.dev@gmail.com
- **GitHub**: [@longhoangk67hus](https://github.com/longhoangk67hus)

---
