La solucion esta dividad en dos partes, una proyecto para Backend en .Net Core 2.2 y otro proyecto para el frontend en Angular 8.
Para el funcionamiento correcto de la solucion seguir los siguientes pasos, en el mismo orden.



- Backend

Se debe abrir con Visual Studio 2017 o la version mas reciente.

Para inicar la solucion se debe tener un motor de base de datos para almacenar la informacion.

En la clase Library.Backend.Repository.LibraryContext  comentar las siguientes lineas:

//public LibraryContext(DbContextOptions options): base(options)
        //{
        //}

Ir por consola a la carpeta donde esta el proyecto Library.Backend.Repository.

Al abrir la solucion.

En los appsettings.json cambiar la cadena de conexion por la del servidor a utilizar, el nombre de la llave es LibraryDB.

Ejecutar los siguientes los siguientes comandos:

dotnet restore

dotnet ef

dotnet ef migrations add initial Library.Backend.Repository.LibraryContext

dotnet ef database update

Ya con estos comandos se encuentra actualizado la base de datos con el modelo.

Compilar la solucion.

Iniciar la solucion desde el proyecto de Library.Backend.API.

Se iniciara un servidor local, el cual esta en la siguiente URL https://localhost:5002/, la cual presenta la documentacion del servicio.


- Frontend

Abrir en el editor de su preferencia.

Abrir una terminal y ubicarse en la carpeta Frontend del proyecto, ejecutar el comando 'npm install' para instalar las dependencias del proyecto.

Ejecutar el comando 'ng build' para compilar la solucion y verificar que todo esta correctamente instalado y configurado.

Iniciar el servicidor con el comando 'ng serve'.

La URL donde se ubica el sitio web es http://localhost:4200


Ya se pueden iniciar las pruebas sobre la App.
