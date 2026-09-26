# Laboratorio No. 2 - Cloud Storage

**Estudiante:** Luna Samantha Arrazola Escobar  
**Carné:** 1022725  

## Objetivo de la aplicación

Desarrollar una aplicación de consola en C# que permita conectarse a Azure Blob Storage y realizar operaciones básicas de almacenamiento en la nube: subir, listar, descargar y eliminar archivos.

La aplicación permite interactuar con un Blob Container de Azure utilizando el SDK `Azure.Storage.Blobs`.

## Tecnologías utilizadas

- C#
- .NET
- Azure Blob Storage
- SDK `Azure.Storage.Blobs`
- Microsoft Azure Portal
- Terminal de macOS
- Git y GitHub

## Configuración de Azure

Para realizar el laboratorio se creó y configuró un Storage Account en Microsoft Azure llamado `storagesemana12la`.

Dentro del Storage Account se creó un Blob Container con acceso privado llamado:

`mia-archivos`

El acceso privado permite evitar que los archivos almacenados puedan ser consultados de forma anónima.

La guía indicaba utilizar el nombre `mia_archivos`. Sin embargo, Azure no permite utilizar el carácter guion bajo (`_`) en los nombres de Blob Containers, por lo que se utilizó `mia-archivos`.

La aplicación se conecta al Storage Account mediante una Connection String almacenada de forma segura en una variable de entorno.

## Arquitectura de la solución

La aplicación utiliza la siguiente estructura:

`Usuario → Aplicación de consola C# → Azure.Storage.Blobs → Azure Blob Storage`

Para trabajar con Azure Blob Storage se utilizan principalmente tres clases:

- `BlobServiceClient`: permite establecer la conexión con el servicio de Azure Blob Storage.
- `BlobContainerClient`: permite acceder y trabajar con el container `mia-archivos`.
- `BlobClient`: permite realizar operaciones sobre un archivo o blob específico.

El programa presenta un menú desde el cual el usuario puede seleccionar las diferentes operaciones disponibles.

## Descripción de las cuatro operaciones

### 1. Subir archivo

La aplicación solicita al usuario la ruta del archivo local que desea subir.

Primero se valida que la ruta no esté vacía y que el archivo exista. Después se obtiene el nombre del archivo mediante `Path.GetFileName()` y se crea un `BlobClient`.

Finalmente, el archivo se carga en Azure Blob Storage mediante `UploadAsync()` y se informa al usuario si la operación fue realizada correctamente.

### 2. Listar archivos

La aplicación consulta los blobs almacenados en el container mediante `GetBlobsAsync()`.

Por cada archivo encontrado se muestra:

- Nombre del archivo.
- Tamaño en bytes.

Si el container está vacío, el programa informa que no existen archivos almacenados.

### 3. Descargar archivo

La aplicación solicita el nombre del blob que se desea descargar y verifica mediante `ExistsAsync()` que exista en Azure.

Después solicita la carpeta local de destino y comprueba que dicha carpeta exista.

El archivo se descarga mediante `DownloadToAsync()` y al finalizar se muestra la ubicación donde fue guardado.

### 4. Eliminar archivo

La aplicación solicita el nombre del blob que se desea eliminar y verifica que exista en Azure.

Antes de eliminarlo, se solicita una confirmación al usuario.

Si el usuario confirma la operación, el archivo se elimina mediante `DeleteAsync()` y se muestra un mensaje indicando que la eliminación fue realizada correctamente.

## Manejo de errores

La aplicación incluye diferentes validaciones para evitar operaciones incorrectas y mostrar mensajes claros al usuario.

Entre las validaciones implementadas se encuentran:

- Verificar que la Connection String se encuentre configurada.
- Evitar rutas vacías.
- Verificar que el archivo local exista antes de subirlo.
- Verificar que un blob exista antes de descargarlo.
- Comprobar que la carpeta de destino exista.
- Verificar que un blob exista antes de eliminarlo.
- Solicitar confirmación antes de eliminar un archivo.
- Detectar opciones inválidas ingresadas en el menú.

Estas validaciones permiten evitar operaciones incorrectas durante el uso de la aplicación.

## Seguridad de la Connection String

La Connection String de Azure contiene información sensible, como las credenciales necesarias para acceder al Storage Account. Por esta razón, no se almacenó directamente dentro del código fuente ni se incluyó en el repositorio de GitHub.

Para proteger la credencial durante el laboratorio se utilizó una variable de entorno llamada:

`AZURE_STORAGE_CONNECTION_STRING`

El programa obtiene su valor mediante:

```csharp
Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING")
```

De esta manera, la aplicación puede utilizar la Connection String sin escribirla directamente dentro de `Program.cs`.

También se creó un archivo `.gitignore` para evitar publicar archivos innecesarios o archivos locales que puedan contener información sensible.

No se incluyen en el repositorio:

- Connection Strings reales.
- Account Keys.
- Credenciales de Azure.
- Archivos que contengan secretos.

La Connection String utilizada para ejecutar el proyecto debe configurarse únicamente en el entorno local del usuario.

## Instrucciones para ejecutar el proyecto

### 1. Clonar o descargar el proyecto

Ingresar desde Terminal a la carpeta que contiene el proyecto `MIA_AzureBlob`.

### 2. Restaurar las dependencias

Ejecutar:

```bash
dotnet restore
```

### 3. Configurar la Connection String

Antes de ejecutar el programa se debe configurar la variable de entorno:

```bash
export AZURE_STORAGE_CONNECTION_STRING='TU_CONNECTION_STRING'
```

`TU_CONNECTION_STRING` debe sustituirse localmente por la Connection String correspondiente al Storage Account.

La Connection String real no debe escribirse en el código ni publicarse en GitHub.

### 4. Ejecutar la aplicación

Ejecutar:

```bash
dotnet run
```

### 5. Utilizar el menú

Al iniciar, la aplicación muestra el siguiente menú:

```text
=================================
     MIA - AZURE BLOB STORAGE
=================================
1. Subir archivo
2. Listar archivos
3. Descargar archivo
4. Eliminar archivo
5. Salir
=================================
```

El usuario debe seleccionar la operación que desea realizar y seguir las instrucciones mostradas en pantalla.

## Evidencias

Durante el desarrollo del laboratorio se realizaron pruebas para comprobar el funcionamiento de la aplicación y la configuración de Azure.

Las evidencias realizadas incluyen:

1. Storage Account configurado en Microsoft Azure.
2. Blob Container `mia-archivos` configurado con acceso privado.
3. Aplicación de consola ejecutándose.
4. Archivos locales antes de ser cargados.
5. Archivos visibles en Azure Portal después de subirlos.
6. Listado de archivos almacenados desde la aplicación en C#.
7. Archivos descargados correctamente de Azure Blob Storage.
8. Comprobación de los archivos descargados localmente.
9. Eliminación de archivos mediante la aplicación.
10. Comprobación de la eliminación de los archivos almacenados.

Los pantallazos correspondientes a estas pruebas se incluyen como parte de las evidencias de la entrega.

## Estructura de la entrega

La entrega del laboratorio se encuentra dentro de:

`semana-12/lab2/`

En esta carpeta se incluyen:

- Proyecto desarrollado en C#.
- Archivo `README.md`.
- PDF con la solución del laboratorio.
- Pantallazos de las pruebas realizadas.
- Archivos de respaldo requeridos para la práctica.

Por motivos de seguridad, no se incluyen Connection Strings, Account Keys ni otros secretos de acceso a Azure.
