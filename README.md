# MultiTenantWebAPI
Proyecto MultiTenantWebAPI utilizando NET CORE 6, Clean Architecture, EntityFrameworkCore y CQRS.
Cuenta con un Midleware que obtiene el Tenant por medio de la url, de esta manera resolver a que base de datos se debe conectar, para los controllers Account y Organization no aplica está validación ya que pertencen a la base de datos principal.

## OrganizationController
- El Proyecto ejecuta una primer migración automaticaque generar la base de datos master la cual conteiene 2 Tablas Organizaciones y Usuarios.
- Al crear una nueva organización ejecuta una migración automatica para generar una base de datos de productos para la orgnización.
- Endpoint Get que consulta todas las organizaciones posibles

## AccountController
- Endpoint Login, valida credenciales contra Users y de ser exitoso genera un token de acceso.
- Endpoint Register, registra un nuevo usuarios el cual debe indicar a que organización pertenece.

## ProductController
Este controller debe contener en el path el Tenant para resolver la base de datos en caso de no propocionar el Tenant, devuelve un mensaje indicado que el Tenant no ha sido configurado.

- Endpoint de consulta de productos
- Endpoint de consulta de producto por id
- Endpoint de creación de producto

### Ejecución :
`Se debe modificar los string de conexión Data Source y User Id y Password en caso de tenerlo,`
#### Ejemplo
`Data Source=yourServer;Initial Catalog=DBOrganizationUser;User ID = yourUserID, Password=yourPassword; Integrated Security=True;MultipleActiveResultSets=True`

Una vez cambiado esto, solo queda ejecutar el proyecto.
