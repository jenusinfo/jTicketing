# jTicketing

A full-stack, multi-tenant Ticketing Management System with:

- ✅ ASP.NET Core 8.0 Web API (`TicketingSystem.API`)
- ✅ React + Vite + Tailwind Frontend (`TicketingSystem.CLI`)
- ✅ SQL Server with EF Core & Migrations
- ✅ JWT Authentication with Role-based Access (Customer, Agent, Admin)

---

## 🔧 Folder Structure

```
/TicketingSystem.sln
/TicketingSystem.API/     ← Backend (.NET 8 API)
/TicketingSystem.CLI/     ← Frontend (React + Vite)
```

---

## 🚀 Getting Started

### Backend (API)
```bash
cd TicketingSystem.API
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend (Client)
```bash
cd TicketingSystem.CLI
npm install
npm run dev
```

---

## 🔐 Login Roles
- **Admin**: Can view all tickets
- **Agent**: Can assign and resolve tickets
- **Customer**: Can create and comment on tickets

---

## 🧪 Tech Stack

- ASP.NET Core 8.0
- React 18 + Vite
- TailwindCSS
- SQL Server + EF Core
- JWT Auth

---

## 📦 Build & Deploy

### Frontend
```bash
npm run build
```

### Backend
Publish using Visual Studio or:
```bash
dotnet publish -c Release
```
