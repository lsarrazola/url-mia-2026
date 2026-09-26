using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string? connectionString =
    Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("ERROR: No se encontró la Connection String.");
    return;
}

string containerName = "mia-archivos";

BlobServiceClient blobServiceClient =
    new BlobServiceClient(connectionString);

BlobContainerClient containerClient =
    blobServiceClient.GetBlobContainerClient(containerName);

await containerClient.CreateIfNotExistsAsync();

bool continuar = true;

while (continuar)
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("     MIA - AZURE BLOB STORAGE");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Subir archivo");
    Console.WriteLine("2. Listar archivos");
    Console.WriteLine("3. Descargar archivo");
    Console.WriteLine("4. Eliminar archivo");
    Console.WriteLine("5. Salir");
    Console.WriteLine("=================================");
    Console.Write("Seleccione una opción: ");

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Console.WriteLine();
            Console.Write("Ingrese la ruta del archivo local: ");
            string? ruta = Console.ReadLine();

            // Validar que la ruta no esté vacía
            if (string.IsNullOrWhiteSpace(ruta))
            {
                Console.WriteLine("Error: La ruta no puede estar vacía.");
                break;
            }

            ruta = ruta.Trim().Trim('"');

            // Validar que el archivo exista
            if (!File.Exists(ruta))
            {
                Console.WriteLine("Error: El archivo no existe.");
                break;
            }

            Console.WriteLine("Archivo encontrado correctamente.");

            // Obtener el nombre del archivo
            string nombreArchivo = Path.GetFileName(ruta);
            Console.WriteLine($"Nombre del archivo: {nombreArchivo}");

            // Conectarse al container
            Console.WriteLine($"Conectando al container: {containerName}...");

            BlobClient blobClient =
                containerClient.GetBlobClient(nombreArchivo);

            Console.WriteLine("Conexión al container realizada correctamente.");

            // Subir el archivo a Azure Blob Storage
            Console.WriteLine("Subiendo archivo a Azure Blob Storage...");

            await blobClient.UploadAsync(ruta, overwrite: true);

            // Informar que la operación fue exitosa
            Console.WriteLine($"Archivo '{nombreArchivo}' subido correctamente.");
            break;

        case "2":
            Console.WriteLine();
            Console.WriteLine("Nombre\t\t\tTamaño");
            Console.WriteLine("--------------------------------------------");

            bool hayArchivos = false;

            await foreach (BlobItem blob in containerClient.GetBlobsAsync())
            {
                hayArchivos = true;
                long tamanio = blob.Properties.ContentLength ?? 0;
                Console.WriteLine($"{blob.Name}\t\t{tamanio} bytes");
            }

            if (!hayArchivos)
            {
                Console.WriteLine("No hay archivos en el container.");
            }

            break;

        case "3":
            Console.WriteLine();
            Console.Write("Ingrese el nombre del blob: ");
            string? nombreBlobDescarga = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombreBlobDescarga))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                break;
            }

            BlobClient blobDescarga =
                containerClient.GetBlobClient(nombreBlobDescarga);

            if (!await blobDescarga.ExistsAsync())
            {
                Console.WriteLine("El archivo no existe en Azure.");
                break;
            }

            Console.Write("Ingrese la carpeta de destino: ");
            string? carpetaDestino = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(carpetaDestino))
            {
                Console.WriteLine("La carpeta no puede estar vacía.");
                break;
            }

            carpetaDestino = carpetaDestino.Trim().Trim('"');

            if (!Directory.Exists(carpetaDestino))
            {
                Console.WriteLine("La carpeta de destino no existe.");
                break;
            }

            string rutaDestino =
                Path.Combine(carpetaDestino, nombreBlobDescarga);

            await blobDescarga.DownloadToAsync(rutaDestino);

            Console.WriteLine("Archivo descargado correctamente.");
            Console.WriteLine($"Ubicación: {rutaDestino}");
            break;

        case "4":
            Console.WriteLine();
            Console.Write("Ingrese el nombre del blob a eliminar: ");
            string? nombreBlobEliminar = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombreBlobEliminar))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                break;
            }

            BlobClient blobEliminar =
                containerClient.GetBlobClient(nombreBlobEliminar);

            if (!await blobEliminar.ExistsAsync())
            {
                Console.WriteLine("El archivo no existe en Azure.");
                break;
            }

            Console.Write($"¿Está seguro de eliminar '{nombreBlobEliminar}'? (S/N): ");
            string? confirmacion = Console.ReadLine();

            if (confirmacion?.Trim().ToUpper() != "S")
            {
                Console.WriteLine("Eliminación cancelada.");
                break;
            }

            await blobEliminar.DeleteAsync();

            Console.WriteLine($"Archivo '{nombreBlobEliminar}' eliminado correctamente.");
            break;

        case "5":
            continuar = false;
            Console.WriteLine("Programa finalizado.");
            break;

        default:
            Console.WriteLine("Opción inválida. Intente nuevamente.");
            break;
    }
}