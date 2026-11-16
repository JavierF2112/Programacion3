Esta es una Web API RESTful desarrollada en .NET 9 (o 8) para la gestión de productos, implementando una arquitectura en capas (Controller -> Service -> Repository -> ADO.NET).

## Requisitos previos
* [.NET SDK 9.0](https://dotnet.microsoft.com/download) (o la versión que hayas usado).
* SQL Server (local o remoto).

## Instrucciones para ejecutar el proyecto

1. **Clonar o descargar el proyecto:**
   Descargá el código fuente y abrí la solución (`.sln`) en Visual Studio.

2. **Preparar la Base de Datos:**
   * Abrí SQL Server Management Studio (SSMS).
   * Ejecutá el script `SQL/Database.sql` que se encuentra en la carpeta `SQL` de este repositorio. Esto creará la base de datos `Programacion3`, la tabla `Productos` y cargará los datos de prueba.

3. **Configurar la Connection String:**
   * Abrí el archivo `appsettings.json`.
   * Verificá que la cadena de conexión `"DefaultConnection"` apunte a tu servidor de SQL Server local o remoto.
   *(Nota: Por defecto viene configurada para el servidor de la facultad).*

4. **Ejecutar la aplicación:**
   * En Visual Studio, presioná `F5` o hacé clic en el botón de "Iniciar".
   * La consola mostrará la URL donde se está ejecutando la API (ej: `https://localhost:7021`).
   * Abrí tu navegador y agregá `/swagger` al final de la URL (ej: `https://localhost:7021/swagger`) para acceder a la interfaz de Swagger UI.

## Endpoints disponibles
* `GET /api/productos` - Obtiene todos los productos.
* `GET /api/productos/{id}` - Obtiene un producto por su ID.

## Tecnologías utilizadas
* ASP.NET Core Web API
* ADO.NET (Microsoft.Data.SqlClient)
* Inyección de Dependencias (AddScoped)
* Patrón Repository