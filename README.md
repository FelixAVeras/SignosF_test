
# 🧩 Catálogo de Usuarios: Backend (.NET) + Frontend (React)

Este proyecto incluye:
- ✅ API REST desarrollada con **.NET 8/9** + EF Core + Docker + SQL Server/InMemory fallback.
- ✅ Frontend con **React + TypeScript**, consumo de API REST y GraphQL, Zustand, Zod y TailwindCSS.

---

## 🚀 Requisitos

- Docker + Docker Compose
- Node.js (v18 o superior)
- .NET SDK 8 o 9

---

## ⚙️ Instrucciones Backend (.NET API)

### 1. Configuración con SQL Server (via Docker)

```bash
cd BackendTest
docker-compose up --build
```

> Esto levantará:
> - SQL Server (mcr.microsoft.com/mssql/server)
> - API .NET 8 (`ProductsAPI`) con migraciones automáticas y seeder

### 2. API disponible en:

```
http://localhost:8080/api/products
```

#### Migraciones automáticas:
El `Program.cs` aplica `context.Database.Migrate()` automáticamente al iniciar.

#### ¿Sin SQL Server? Usa InMemory:

Si no hay SQL Server accesible, la API cambiará a modo **InMemory** y cargará el seeder.

---

## 🎨 Instrucciones Frontend (React + TypeScript)

### 1. Instalación

```bash
cd user-catalog
npm install
```

### 2. Ejecutar app

```bash
npm start
```

> Se abrirá automáticamente en `http://localhost:3000`

---

## 🌐 Funcionalidades del Frontend

- Buscar usuarios (API REST: jsonplaceholder)
- Ver detalle de usuario
- Ver posts de usuario (API GraphQL: graphqlzero)
- Validación con Zod
- Estado global con Zustand
- UI responsive con TailwindCSS

---

## 📁 Estructura del Proyecto

```
.
├── BackendTest/
│   ├── Dockerfile
│   ├── docker-compose.yml
│   └── ProductsAPI/
├── user-catalog/
│   ├── src/
│   └── package.json
```

---

## 🛠 Tecnologías utilizadas

### Backend
- .NET 8/9
- Entity Framework Core (SQL Server + InMemory)
- Docker + Docker Compose

### Frontend
- React + TypeScript
- Zustand, Zod, TailwindCSS
- REST + GraphQL + styled-components

---

## 🤝 Autor

Desarrollado por Felix Carvajal © 2025
