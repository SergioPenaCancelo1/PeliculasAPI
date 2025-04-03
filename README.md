--------------
|PeliculasAPI|
--------------

 Esta aplicacion   permite obtener información detallada sobre una película mediante un servicio RESTful,
 así como una lista de películas similares utilizando la API de The Movie Database (TMDb).

La aplicación está preparada para ser ejecutada en un contenedor Docker

REQUISITOS PREVIOS
-----------------

- .NET SDK 8.0 (Para compilar y ejecutar la aplicación sin Docker)

-Docker (Para ejecutar la aplicación en un contenedor)
 
-Cuenta en TMDb: Necesitarás una clave de API de TMDb. Puedes obtenerla aquí:

https://developers.themoviedb.org/3/getting-started/introduction


Instalación
-----------

Paso 1: Obtener la API Key de TMDb
---------------------------------
1. Regístrate en TMDb y consigue una clave de API.



3. Abre el archivo appsettings.json y reemplaza la clave en el siguiente campo:
 {
 }
    "TMDb": {
        "ApiKey": "TU_API_KEY"
    }



 Paso 2: Configuración del Proyecto
---------------------------------


Opción 1: Ejecutar sin Docker
------------------------------------------ 
 1.Clona el repositorio o descarga los archivos del proyecto  con este comando y abre el proyecto con Visual estudio:

git clone https://github.com/SergioPenaCancelo1/PeliculasAPI.git


 2. Abre una terminal y navega al directorio del proyecto.
 3. Ejecuta el siguiente comando para restaurar las dependencias del proyecto:

 dotnet restore

 4. Para compilar y ejecutar la aplicación localmente, utiliza el siguiente comando:

 dotnet run

 La aplicación escuchará en los puertos 8080 y 8081 o en su defecto 7056. Puedes acceder a la API en

 http://localhost:7056/api/movies/"titulo de la pelicula" en tu navegador y obtener información sobre una película en formato JSON


Opción 2: Ejecutar con Docker
----------------------------

Si prefieres ejecutar la aplicación en un contenedor Docker, sigue estos pasos:

 1. Debes tener Docker instalado.

Los siguientes pasos no deberian ser necesarios, unicamente con ejecutar el proyecto por ejemplo en visual studio 2022 con docker iniciado deberia crearse directamente el contenedor e iniciarse directamente la aplicacion escuchando en el puerto 7056 .
Pon esto en tu navegador http://localhost:7056/swagger/index.html para acceder a la aplicacion dentro de swagger 

De no funcionar como se ha dicho previamente seguir los siguientes pasos


 2. Entra al directorio del proyecto por comando desde la terminal.

 3.Construye la imagen con el siguiente comando:

 docker build -t peliculasapi:dev .

 4. Ejecuta la aplicación:

 docker run --name PeliculasAPI -p 8080:8080 -p 8081:8081 -d peliculasapi:dev

 La aplicación ahora debería estar ejecutándose en Docker, accesible a través de esta ruta en tu navegador
 http://localhost:8080/swagger/index.html



Paso 3: Probar la API
--------------------
La API tiene un único dato a introducir:

- GET /api/movies/{title}: Recupera información sobre una película especificada por su título


Ejemplo de solicitud:

 GET http://localhost:8080/api/movies/Inception

Respuesta esperada:

 {
    "title": "Inception",
    "originalTitle": "Inception",
    "rating": 8.8,
    "releaseDate": "2010-07-16",
    "overview": "A thief who steals corporate secrets through the use of dream-sharing technology is
 given the inverse task of planting an idea into the mind of a CEO.",
    "similarMovies": [
        "Interstellar (2014)",
        "The Prestige (2006)",
        "Memento (2000)",
        "Shutter Island (2010)",
        "Blade Runner 2049 (2017)"
    ]
 }
