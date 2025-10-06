# ChallengeNET — API REST de gestión de usuarios

## 📌 Descripción

**ChallengeNET** es una aplicación backend desarrollada en **.NET 6** que expone una API REST para la gestión de usuarios.
Permite operaciones CRUD (crear, consultar, actualizar y eliminar) junto con la posibilidad de filtrar usuarios por **nombre, provincia o ciudad**.

El proyecto está estructurado siguiendo la **arquitectura Onion (Onion Architecture)**, aplicando **inyección de dependencias** mediante interfaces e implementaciones, lo que asegura separación de responsabilidades, testabilidad y flexibilidad.

Este desarrollo fue realizado como **prueba técnica** para demostrar conocimientos en .NET, arquitectura, buenas prácticas y pruebas unitarias.

---

## 🛠️ Tecnologías utilizadas

* **.NET 6 / ASP.NET Core** → construcción de la API REST.
* **Entity Framework Core** → acceso a base de datos y migraciones.
* **MySQL** → motor de base de datos utilizado.
* **AutoMapper** → mapeo entre entidades y DTOs.
* **FluentValidation** → validaciones de datos en los DTOs.
* **Swagger (Swashbuckle)** → documentación y pruebas interactivas de la API.
* **xUnit & Moq** → pruebas unitarias y mocking de dependencias.

---

## 🏗️ Arquitectura

El proyecto está estructurado siguiendo **Onion Architecture**, con las siguientes capas principales:

* **Domain**: contiene las entidades y contratos básicos.
* **Application**: contiene la lógica de negocio, DTOs, validaciones, servicios y mapeos.
* **Infrastructure**: contiene la implementación del repositorio, DbContext, migraciones y configuración de persistencia.
* **API (WebApi)**: capa de entrada que expone los endpoints al cliente.

Esto asegura un código **desacoplado, mantenible y escalable**.

---

## 📂 Estructura del proyecto

```
/UserChallenge
  /Application     → lógica de aplicación, DTOs, validaciones, servicios
  /Domain          → entidades del dominio y contratos
  /Infrastructure  → contexto EF Core, repositorios, migraciones
  /API             → proyecto Web API, controladores, configuración, handlers
/ChallengeNET.sln   → solución principal
```
---

## ⚙️ Requisitos previos

* **.NET 6 SDK** instalado.
* **MySQL** configurado localmente.
* Visual Studio 2022 / Rider / VS Code.
* Acceso a la CLI de **EF Core** (`dotnet ef`).

---

## 🚀 Pasos para levantar el proyecto

1. **Clonar el repositorio**

   ```bash
   git clone https://github.com/112916-Mateo-Salas/ChallengeNET.git
   cd ChallengeNET
   ```

2. **Restaurar paquetes NuGet**

   ```bash
   dotnet restore
   ```

3. **Configurar la base de datos**

   Revisar el archivo `appsettings.json` en el proyecto `API` y ajustar la cadena de conexión a tu instancia de **MySQL**.

   ```json
   "ConnectionStrings": {
     "MySqlConnection": "server=localhost;port=3306;database=UserChallenge;user=root;password=tuPassword"
   }
   ```

4. **Aplicar migraciones**

   ```bash
   cd UserChallenge.Infrastructure
   dotnet ef database update
   ```

5. **Ejecutar la API**

   ```bash
   cd ../UserChallenge.API
   dotnet run
   ```

   La API quedará disponible en:

   * `https://localhost:5001`
   * `http://localhost:5000`

6. **Abrir Swagger**

   Navegar a:
   `https://localhost:5001/swagger`

   Allí se pueden probar los endpoints de la API.

---

## ✅ Ejecución de pruebas unitarias

Desde la raíz de la solución:

```bash
dotnet test
```

Esto ejecutará los **tests unitarios** que validan servicios, controladores y el manejo de excepciones.

---

## 🛡️ Manejo de errores

La API implementa un **middleware global de excepciones** que convierte las excepciones en respuestas HTTP con un formato uniforme:

```json
{
  "success": false,
  "message": "Mensaje de error",
  "detail": "Detalle interno opcional"
}
```

Ejemplos:

* `KeyNotFoundException` → **404 Not Found**
* `ArgumentNullException` → **400 Bad Request**
* `InvalidOperationException` → **500 Internal Server Error**

---

## 📖 Documentación de la API

La documentación se genera automáticamente con **Swagger**. Cada endpoint está anotado con respuestas posibles y descripciones.

* **GET /usuarios** → listar usuarios o filtrar por nombre/provincia/ciudad
* **POST /usuarios** → crear usuario
* **PUT /usuarios/{id}** → actualizar usuario
* **DELETE /usuarios/{id}** → eliminar usuario

---

## 📖 Notas Adicionales

* Se utilizó **Fluent API** para la configuración de las entidades en lugar de atributos en las clases.
* Se implementó **Inyección de Dependencias** registrando interfaces y sus implementaciones en el contenedor de servicios.
* El proyecto cumple con principios de **arquitectura limpia**, **desacoplamiento** y **responsabilidad única**.

---

## 🚧 Posibles mejoras futuras

* Autenticación y autorización (JWT, roles).
* Paginación y ordenamiento en consultas.
* Caché en endpoints de lectura.
* Versionado de API (v1, v2).
* Pruebas de integración con base de datos en memoria.

---

## 👨‍💻 Autor

* **Mateo Salas**
* GitHub: [112916-Mateo-Salas](https://github.com/112916-Mateo-Salas)

---