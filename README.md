# ProductosCycle Api

API REST de la aplicación de productos referente a la prueba tecnica de Cycle

## Autor 🖋️

Sergio Mercado Salazar

- [Linkedin](https://www.linkedin.com/in/devsergiom/)
- [Github](https://github.com/mercadosergio)

## Requisitos

- .NET 6 SDK
- Visual Studio Community 2022
- SQL Server

## Instalación

1. Clonar el repositorio
   `git clone https://github.com/mercadosergio/productos_cycle_api.git`

2. Una vez clonado el repositorio, debe crear la base de datos en un entorno SQL SERVER y ejecutar los scripts SQL que se encuentran en el archivo `TicketsApi_Scripts.sql` en un entorno SQL SERVER.

3. Ejecute la solución `TicketsApi.sln` que se encuentra en la raíz del proyecto.

4. Configurar la cadena de conexión en el archivo `appsettings.json`.

```json
"ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DB_NAME;Trusted_Connection=True;User Id=usuario; Password=contraseña;MultipleActiveResultSets=true;TrustServerCertificate=True"
 }
```

5. Compilar aplicación.  


## Documentación con Swagger

Para interactuar con los endpoints en la documentación en Swagger, debera colocar la url de la documentación, ejemplo `https://localhost:7285/swagger`
 