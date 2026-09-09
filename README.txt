# ESTADOTC

Aplicación web para la gestión y consulta del estado de cuenta de una tarjeta de crédito.

El proyecto fue desarrollado utilizando una arquitectura separada entre una API REST y una aplicación MVC que consume los servicios expuestos por dicha API.

---

## Tecnologías

### Backend

- .NET 6
- ASP.NET Core Web API
- C#
- SQL Server
- Stored Procedures
- Dapper
- MediatR
- CQRS
- Unit of Work
- AutoMapper
- FluentValidation
- Swagger / OpenAPI
- QuestPDF
- Global Exception Handling
- Healthcheck

### Frontend

- ASP.NET Core MVC
- F#
- Razor
- Bootstrap
- jQuery
- HttpClient

---

## Arquitectura

La solución está compuesta por dos proyectos principales:

```text
ESTADOTC
├── ESTADOTC.API
└── ESTADOTC.MVC
```

### ESTADOTC.API

Responsable de:

- Exponer los endpoints REST.
- Aplicar las reglas de negocio.
- Ejecutar Commands y Queries mediante CQRS y MediatR.
- Acceder a SQL Server mediante Dapper.
- Ejecutar Stored Procedures.
- Utilizar DTOs para la transferencia de información.
- Realizar mapeos mediante AutoMapper.
- Validar solicitudes mediante FluentValidation.
- Centralizar el acceso a repositorios mediante Unit of Work.
- Manejar excepciones globalmente.
- Generar el estado de cuenta en PDF.
- Exponer documentación mediante Swagger.
- Proporcionar un Healthcheck para verificar el estado de la aplicación.

### ESTADOTC.MVC

Responsable de:

- Presentar la interfaz de usuario.
- Consumir `ESTADOTC.API` mediante HttpClient.
- Mostrar el estado de cuenta de la tarjeta.
- Mostrar el resumen financiero.
- Registrar compras.
- Registrar pagos.
- Consultar movimientos.
- Filtrar movimientos mediante jQuery.
- Descargar el estado de cuenta en PDF.
- Manejar mensajes de éxito y errores provenientes de la API.

---

# Requisitos previos

Para ejecutar el proyecto localmente se requiere:

- .NET 6 SDK o un SDK compatible con proyectos `net6.0`.
- SQL Server.
- Acceso a una instancia local de SQL Server.
- Certificado HTTPS de desarrollo de ASP.NET Core, si se utilizará HTTPS.
- Postman, opcionalmente, para ejecutar la colección incluida en el proyecto.

---

# Instalación y ejecución local

## 1. Clonar el repositorio

Clonar el repositorio y ubicarse en la carpeta raíz de la solución.

```bash
git clone <URL_DEL_REPOSITORIO>
cd ESTADOTC
```

> Sustituir `<URL_DEL_REPOSITORIO>` por la dirección correspondiente al repositorio del proyecto.

---

## 2. Restaurar dependencias

Las dependencias NuGet ya se encuentran declaradas dentro de los archivos de proyecto.

Por lo tanto, después de clonar el repositorio no es necesario instalar cada paquete manualmente.

Ejecutar:

```bash
dotnet restore
```

Para consultar los paquetes utilizados por la solución puede ejecutarse:

```bash
dotnet list package
```

---

## 3. Dependencias principales

Las principales dependencias utilizadas durante el desarrollo son:

| Paquete | Versión | Propósito |
|---|---:|---|
| Dapper | 2.1.66 | Acceso ligero a SQL Server |
| Microsoft.Data.SqlClient | 5.2.2 | Conexión con SQL Server |
| MediatR | 14.2.0 | Commands, Queries y Handlers |
| AutoMapper | 12.0.1 | Mapeo entre entidades y DTOs |
| AutoMapper.Extensions.Microsoft.DependencyInjection | 12.0.1 | Integración de AutoMapper con DI |
| FluentValidation | 11.11.0 | Validación de solicitudes |
| FluentValidation.AspNetCore | 11.3.1 | Integración de validaciones con ASP.NET Core |
| FluentValidation.DependencyInjectionExtensions | 11.11.0 | Registro de validadores mediante DI |
| Swashbuckle.AspNetCore | 6.5.0 | Swagger / OpenAPI |
| QuestPDF | 2024.12.3 | Generación del estado de cuenta PDF |


---

# Base de datos

La aplicación utiliza SQL Server.

La base de datos utilizada es:

```text
ESTADOTC
```

## Tablas principales

### Cards

Contiene la información de las tarjetas.

Campos principales:

- Id
- CardNumber
- HolderName
- CreditLimit
- InterestRate
- MinimumPaymentRate
- CreatedAt

El saldo actual y el crédito disponible se obtienen mediante las consultas correspondientes.

### Transactions

Contiene las compras y pagos realizados.

Campos principales:

- Id
- CardId
- TransactionDate
- Description
- Amount
- TransactionType

Los tipos de transacción utilizados son:

```text
PURCHASE
PAYMENT
```

---

## Stored Procedures

El acceso a los datos se realiza principalmente mediante Dapper y Stored Procedures.

Los principales Stored Procedures son:

- `sp_GetCardStatement`
- `sp_GetTransactions`
- `sp_GetCurrentMonthTransactions`
- `sp_GetMonthlyPurchaseTotals`
- `sp_GetCardFinancialSummary`
- `sp_AddPurchase`
- `sp_AddPayment`

Estos procedimientos permiten obtener el estado de cuenta, movimientos, información financiera y registrar compras y pagos.

---

# Configuración de conexión

La API necesita una cadena de conexión hacia SQL Server.

Ejemplo de configuración local:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ESTADOTC;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

La cadena debe modificarse de acuerdo con la instancia de SQL Server utilizada en el equipo donde se ejecute la solución.

---

# Certificado HTTPS

Si el equipo no posee un certificado HTTPS de desarrollo confiable, puede configurarse mediante:

```bash
dotnet dev-certs https --trust
```

---

# Ejecutar la API

Desde la raíz de la solución:

```bash
dotnet run --project ESTADOTC.API
```

Durante el desarrollo se utilizaron las siguientes direcciones:

```text
HTTPS: https://localhost:7104
HTTP:  http://localhost:5164
```

Los puertos pueden variar dependiendo del perfil de ejecución configurado.

Swagger permite consultar y ejecutar los endpoints expuestos por la API.

---

# Configurar ESTADOTC.MVC

El proyecto MVC consume `ESTADOTC.API` mediante HttpClient.

La URL de la API debe configurarse en la sección `ApiSettings`.

Ejemplo:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7104/"
  }
}
```

El valor de `BaseUrl` debe coincidir con la dirección donde se encuentre ejecutándose `ESTADOTC.API`.

---

# Ejecutar MVC

En otra terminal ejecutar:

```bash
dotnet run --project ESTADOTC.MVC
```

El proyecto MVC consumirá los servicios de la API y quedará disponible en la dirección indicada por ASP.NET Core durante el arranque.

---

# Funcionalidades

La aplicación permite:

- Consultar el estado de cuenta de una tarjeta.
- Consultar saldo actual.
- Consultar límite de crédito.
- Consultar crédito disponible.
- Consultar interés bonificable.
- Consultar pago mínimo.
- Consultar total a pagar.
- Consultar pago de contado con interés.
- Comparar compras del mes actual contra el mes anterior.
- Consultar movimientos del mes actual.
- Consultar historial completo de movimientos.
- Registrar compras.
- Registrar pagos.
- Exportar el estado de cuenta en PDF.
- Filtrar visualmente movimientos.
- Consultar el estado de salud de la API.

---


# Reglas de negocio

## Compras

Para registrar una compra:

- El monto debe ser mayor que cero.
- La tarjeta debe existir.
- El monto de la compra no puede superar el crédito disponible.

Las reglas se validan en el backend antes de registrar la transacción.

---

## Pagos

Para registrar un pago:

- El monto debe ser mayor que cero.
- La tarjeta debe existir.
- El monto del pago no puede superar el saldo actual.

Las reglas se validan en el backend antes de registrar la transacción.

---

# Validaciones

La aplicación utiliza FluentValidation para validar las solicitudes recibidas por la API.

Por ejemplo:

- Descripción obligatoria en una compra.
- Monto mayor que cero.
- Validación de datos antes de ejecutar las operaciones.

Las validaciones de entrada se complementan con las reglas de negocio implementadas en los Handlers.

---

# Manejo global de excepciones

La API posee middleware para centralizar el manejo de excepciones.

Entre los códigos manejados se encuentran:

```text
InvalidOperationException -> HTTP 400 Bad Request
KeyNotFoundException      -> HTTP 404 Not Found
Exception                 -> HTTP 500 Internal Server Error
```

Los errores controlados devuelven información que puede ser consumida por el proyecto MVC para mostrar mensajes adecuados al usuario.

---

# CQRS y MediatR

La API utiliza CQRS para separar las operaciones de lectura y escritura.

Las solicitudes son enviadas mediante MediatR hacia sus respectivos Handlers.

Flujo simplificado:

```text
Controller
    ↓
MediatR
    ↓
Command / Query
    ↓
Handler
    ↓
Repository / Unit of Work
    ↓
Dapper
    ↓
Stored Procedure
    ↓
SQL Server
```

Esta separación permite mantener los controladores desacoplados de la lógica de negocio y del acceso directo a datos.

---

# Unit of Work

La solución utiliza el patrón Unit of Work para centralizar el acceso a los repositorios requeridos por la aplicación.

Esto evita que las capas superiores dependan directamente de implementaciones concretas de acceso a datos.

---

# DTOs y ViewModels

La solución mantiene separados los modelos utilizados por la API y los utilizados por la interfaz.

### API

Utiliza DTOs para exponer información mediante los endpoints REST.

### MVC

Utiliza ViewModels para representar la información necesaria en las vistas Razor.

AutoMapper es utilizado en la API para facilitar el mapeo entre entidades y DTOs.

---

# PDF

La aplicación permite exportar el estado de cuenta utilizando QuestPDF.

El documento incluye:

- Información del tarjetahabiente.
- Número de tarjeta.
- Saldo actual.
- Límite de crédito.
- Crédito disponible.
- Interés bonificable.
- Pago mínimo.
- Total a pagar.
- Pago con interés.
- Compras del mes actual.
- Compras del mes anterior.
- Movimientos del mes.
- Fecha de cada movimiento.
- Descripción.
- Tipo de transacción.
- Monto.
- Fecha de generación.
- Numeración de páginas.

El PDF puede descargarse desde la aplicación MVC o directamente mediante el endpoint de la API.

---

# jQuery

El proyecto MVC utiliza jQuery para proporcionar interacciones dinámicas en la interfaz.

Entre ellas se encuentra el filtrado de movimientos del estado de cuenta.

El usuario puede visualizar:

- Todos los movimientos.
- Compras.
- Pagos.

El filtrado se realiza del lado del cliente sin necesidad de recargar la página.

---

# Postman

El repositorio incluye una colección Postman para probar los principales endpoints de la API.

Archivo:

```text
Postman/ESTADOTC.postman_collection.json
```

La colección incluye:

- Healthcheck.
- Estado de cuenta.
- Historial de movimientos.
- Movimientos del mes actual.
- Totales mensuales.
- Resumen financiero.
- Registro de compras.
- Registro de pagos.
- Descarga del PDF.

## Variables de Postman

La colección utiliza:

```text
baseUrl
cardId
```

Ejemplo:

```text
baseUrl = https://localhost:7104
cardId = 1
```

Gracias a estas variables no es necesario modificar individualmente las URLs de cada request.

Si la API se ejecuta en otro puerto, solamente debe modificarse `baseUrl`.

---

# Swagger

`ESTADOTC.API` incorpora Swagger / OpenAPI.

Swagger permite:

- Consultar los endpoints disponibles.
- Visualizar los modelos de entrada.
- Ejecutar solicitudes contra la API.
- Revisar los códigos de respuesta.

---

# Decisiones técnicas

### Dapper

Se utilizó Dapper como micro ORM para mantener un acceso a datos sencillo y explícito mediante Stored Procedures.

### CQRS

Se utilizó CQRS para separar responsabilidades entre operaciones de consulta y operaciones que modifican información.

### MediatR

Se utilizó MediatR para desacoplar los Controllers de los Handlers que contienen la lógica de cada caso de uso.

### FluentValidation

Se utilizó FluentValidation para mantener las validaciones de entrada fuera de los Controllers.

### AutoMapper

Se utilizó AutoMapper para mantener separado el modelo interno de dominio de los DTOs expuestos por la API.

### Unit of Work

Se utilizó Unit of Work para centralizar el acceso a los repositorios.

### QuestPDF

Se utilizó QuestPDF para generar programáticamente el estado de cuenta en formato PDF.

### Separación API / MVC

La interfaz MVC no accede directamente a SQL Server.

El flujo utilizado es:

```text
Usuario
   ↓
ESTADOTC.MVC
   ↓
HTTP
   ↓
ESTADOTC.API
   ↓
Application / Handlers
   ↓
Repositories
   ↓
Dapper
   ↓
SQL Server
```

Esto mantiene separadas las responsabilidades de presentación, negocio y acceso a datos.

---

# Consideraciones

- El proyecto utiliza `.NET 6` debido a los requerimientos de la prueba técnica.
- La dirección y los puertos de ejecución pueden variar dependiendo del entorno.
- La cadena de conexión debe configurarse de acuerdo con la instancia local de SQL Server.
- La URL configurada en `ESTADOTC.MVC` debe apuntar a la instancia activa de `ESTADOTC.API`.
- Las dependencias NuGet se restauran mediante `dotnet restore`.
- Las reglas de negocio críticas son verificadas en el backend y no dependen únicamente de validaciones de interfaz.

---
