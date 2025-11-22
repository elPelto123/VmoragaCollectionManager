# VmoragaCollectionManager

Aplicación web ASP.NET Core MVC para gestionar múltiples colecciones genéricas por usuario. Incluye autenticación, CRUD de elementos y gestión de imágenes referenciales.

## Características
- Login y registro de usuarios
- Cada usuario puede crear y gestionar varias colecciones independientes
- CRUD completo para elementos de cada colección
- Subida y visualización de imágenes referenciales
- Interfaz basada en Razor

## Requisitos
- .NET 10.0 SDK
- Visual Studio Code

## Primeros pasos
1. Compila el proyecto:
   ```powershell
   dotnet build
   ```
2. Ejecuta la aplicación:
   ```powershell
   dotnet run
   ```
3. Accede a la web en https://localhost:5001

## Personalización
- Modifica los modelos en `/Models` para adaptar los campos de los elementos de colección.
- Personaliza las vistas Razor en `/Views` para mejorar la experiencia de usuario.

---
