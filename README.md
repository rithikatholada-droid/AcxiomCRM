# AcxiomCRM

A simple, student-level CRM web application built with **ASP.NET Core MVC (.NET 8)**, **Entity Framework Core**, **SQLite** and **Bootstrap 5**.

## Features
- **Dashboard** – KPI cards (customers, leads, open/won/lost opportunities, pipeline value, pending follow-ups) and two Chart.js charts (lead status, opportunity pipeline) plus upcoming follow-ups
- **Customers** – create, edit, delete, search (name / email / phone / company)
- **Leads** – CRUD, search + status filter, **convert a Qualified lead into a Customer**
- **Opportunities** – CRUD, search + stage filter, weighted value (Amount × Probability / 100)
- **Follow-Ups** – schedule, edit, delete, mark completed, overdue highlight, search + status filter

## Validation
| Level | What |
|-------|------|
| Client-side | Data annotations + jQuery unobtrusive validation (required, email, 10-digit phone, length, date, numeric range) |
| Server-side | `ModelState.IsValid` in every POST action (works even if the browser check is bypassed) |
| Business rules | Amount > 0 · Probability 0–100 · Expected close date not in the past for active opportunities · Follow-up date not before today for planned follow-ups · unique customer email/phone · valid lead status only |

Security basics: `[ValidateAntiForgeryToken]` on all POST actions and EF Core (parameterised queries, so no SQL injection).

## Project structure
```
Controllers/   Home (dashboard), Customers, Leads, Opportunities, FollowUps
Models/        Entities with validation rules + DashboardViewModel + Options (dropdown lists)
Data/          AppDbContext (EF Core) and SeedData (sample records)
Views/         Razor views (Index / Create / Edit + shared _Form partial per module)
wwwroot/css/   site.css (custom theme)
```

## How to run
Requirements: [.NET 8 SDK](https://dotnet.microsoft.com/download)

```bash
git clone <your-repo-url>
cd AcxiomCRM
dotnet run
```
Open the URL shown in the console (e.g. `http://localhost:5000`). The SQLite database (`acxiomcrm.db`) and sample data are created automatically on first run. To reset the data, stop the app and delete `acxiomcrm.db`.

## Not included (future improvements)
Login / roles (ASP.NET Core Identity), audit log, REST API, reports, activity module.
