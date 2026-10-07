# AcxiomCRM

## 📌 Project Overview

**AcxiomCRM** is a web-based Customer Relationship Management application developed using **ASP.NET Core MVC**. The application provides a simple and organized platform for managing customers, leads, sales opportunities, and follow-up activities.

The project focuses on implementing the core CRM workflow with a clean user interface, database integration, validation, search functionality, and dashboard analytics.

---

## 🎯 Objectives

- Manage customer information efficiently.
- Track and manage sales leads.
- Monitor sales opportunities and pipeline stages.
- Schedule and manage customer follow-ups.
- Provide a centralized dashboard for important CRM information.
- Apply client-side and server-side validation.
- Provide a simple and user-friendly interface for CRM operations.

---

## ✨ Key Features

### 📊 Dashboard

The dashboard provides an overview of important CRM information, including:

- Total Customers
- Total Leads
- Open Opportunities
- Won Opportunities
- Lost Opportunities
- Total Pipeline Value
- Pending Follow-Ups
- Lead Status Chart
- Opportunity Pipeline Chart
- Upcoming Follow-Ups

### 👥 Customer Management

The Customer Management module allows users to:

- Add new customers
- View customer records
- Edit customer information
- Delete customer records
- Search customers
- Search by name, email, phone, or company
- Validate duplicate email addresses
- Validate duplicate phone numbers

### 📋 Lead Management

The Lead Management module provides:

- Lead creation
- Lead viewing
- Lead editing
- Lead deletion
- Lead search
- Lead status filtering
- Lead assignment
- Lead status management
- Conversion of a qualified lead into a customer

### 💼 Opportunity Management

The Opportunity Management module allows users to:

- Create opportunities
- View opportunities
- Edit opportunities
- Delete opportunities
- Search opportunities
- Filter opportunities by stage
- Track opportunity amount
- Track probability
- Track expected closing date
- Calculate weighted opportunity value

### 📅 Follow-Up Management

The Follow-Up module provides:

- Schedule follow-ups
- Edit follow-ups
- Delete follow-ups
- Mark follow-ups as completed
- Search follow-ups
- Filter by status
- Identify overdue follow-ups
- Display upcoming follow-ups

---

## ✅ Validation

The application implements validation at both **client-side and server-side** levels.

### Client-Side Validation

The application provides validation for:

- Required fields
- Email format
- Phone number
- Text length
- Numeric values
- Date values
- Input ranges

### Server-Side Validation

Important validation rules are also checked on the server before saving data.

This ensures that invalid data cannot be stored even if browser-side validation is bypassed.

### Business Validation Rules

The project implements rules such as:

- Opportunity amount must be greater than zero.
- Opportunity probability must be between 0 and 100.
- Expected close date cannot be in the past for an active opportunity.
- Follow-up date cannot be earlier than today for planned follow-ups.
- Customer email must be valid and unique.
- Customer phone must be valid and unique.
- Lead status must use a valid configured status.

---

## 🛡️ Security Basics

The current implementation includes basic application security practices such as:

- Anti-forgery protection for POST requests.
- Server-side validation.
- Entity Framework Core parameterized database operations.
- Protection against invalid form submissions.

---

## 🛠️ Technology Stack

| Technology | Purpose |
|------------|---------|
| **ASP.NET Core MVC (.NET 8)** | Web application framework |
| **C#** | Application development |
| **Entity Framework Core** | Database access |
| **SQLite** | Database |
| **Razor Views** | User interface |
| **Bootstrap 5** | Responsive UI |
| **Chart.js** | Dashboard visualization |
| **jQuery Validation** | Client-side validation |

---

## 🏗️ Project Architecture

The application follows the **Model-View-Controller (MVC)** architecture.

```text
AcxiomCRM
│
├── Controllers
│   ├── HomeController.cs
│   ├── CustomersController.cs
│   ├── LeadsController.cs
│   ├── OpportunitiesController.cs
│   └── FollowUpsController.cs
│
├── Data
│   ├── AppDbContext.cs
│   └── SeedData.cs
│
├── Models
│   ├── Customer.cs
│   ├── Lead.cs
│   ├── Opportunity.cs
│   ├── FollowUp.cs
│   ├── DashboardViewModel.cs
│   └── Options.cs
│
├── Views
│   ├── Customers
│   ├── Leads
│   ├── Opportunities
│   ├── FollowUps
│   ├── Home
│   └── Shared
│
├── wwwroot
│   └── css
│       └── site.css
├── Program.cs
├── appsettings.json
├── AcxiomCRM.csproj
└── README.md
```

## 🗄️ Database

The application uses **SQLite** as the database and **Entity Framework Core** for database operations.

The main database entities used in the project are:

- **Customer** – Stores customer details such as name, email, phone, company, address, status and notes.
- **Lead** – Stores lead information including contact details, source, status, priority and assigned user.
- **Opportunity** – Stores sales opportunity details such as name, customer, stage, amount, probability and expected close date.
- **FollowUp** – Stores follow-up activities including date, subject, type, status, related customer, lead or opportunity, and notes.

### Database Integration

Entity Framework Core is used to:

- Create and manage database tables.
- Perform CRUD operations.
- Retrieve and update CRM records.
- Apply database-level constraints and relationships.
- Store and retrieve application data using SQLite.

### Sample Data

The application includes sample CRM records for demonstration purposes. These records help display meaningful information on the dashboard and allow testing of the different CRM modules.

### Database Structure

```text
SQLite Database
       │
       ▼
Entity Framework Core
       │
       ▼
   AppDbContext
       │
 ┌─────┼─────────┬───────────┐
 ▼     ▼         ▼           ▼
Customer  Lead  Opportunity  FollowUp
```
## 🚀 Getting Started

### Prerequisites

Before running the application, install:

- .NET 8 SDK
- Visual Studio Code or Visual Studio

### Clone the Repository

```bash
git clone https://github.com/rithikatholada-droid/AcxiomCRM.git
```

### Navigate to the Project

```bash
cd AcxiomCRM
```

### Run the Application

```bash
dotnet run
```

The application will start on the local development server.

Open the local URL displayed in the terminal.

---

## 📦 Sample Data

The project contains sample CRM records to demonstrate the application workflow, including:

- Customers
- Leads
- Opportunities
- Follow-Ups

The sample data also helps demonstrate the dashboard statistics and charts.

---

## 🔎 Main Application Workflow

The basic CRM workflow is:

```text
Customer / Lead
       ↓
Lead Management
       ↓
Qualified Lead
       ↓
Customer Conversion
       ↓
Opportunity
       ↓
Sales Pipeline
       ↓
Follow-Up
       ↓
Opportunity Outcome
```

---

## 📈 Dashboard Analytics

The dashboard provides visual information about CRM data using:

- Lead Status Chart
- Opportunity Pipeline Chart
- KPI Cards
- Upcoming Follow-Ups

**Chart.js** is used for graphical representation of CRM information.

---

## 🔍 Search and Filtering

Search and filtering functionality is provided for the main CRM modules.

### Customers

Search using:

- Customer name
- Email
- Phone
- Company

### Leads

Search and filter using:

- Lead name
- Company
- Lead status

### Opportunities

Search and filter using:

- Opportunity name
- Stage

### Follow-Ups

Search and filter using:

- Follow-up details
- Status

---
## ✅ Conclusion

AcxiomCRM provides a simple and organized platform for managing core CRM activities such as customers, leads, opportunities, and follow-ups.

The application demonstrates the use of **ASP.NET Core MVC, C#, Entity Framework Core, SQLite, Bootstrap, and Chart.js** to build a responsive and database-driven web application.

The project provides a foundation that can be extended with additional CRM features as required.

---
