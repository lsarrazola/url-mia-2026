using System;
using System.IO;

namespace MIA_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            // ===================== ENTRADAS =====================

            // Se solicita el nombre completo del usuario
            Console.Write("Ingrese su nombre completo: ");
            string nombreCompleto = Console.ReadLine() ?? "";

            // Se reemplazan los espacios por guion bajo
            // para utilizar el nombre en rutas y archivos
            string nombreArchivo = nombreCompleto.Trim().Replace(" ", "_");

            // Se obtiene automáticamente la carpeta actual del proyecto
            // En tu Mac será algo como:
            // /Users/samanthaarrazola/MIA_Lab_5/project/Lab5
            string carpeta = Directory.GetCurrentDirectory();

            // Se construye la ruta del archivo de entrada
            string rutaArchivo = Path.Combine(carpeta, nombreArchivo + ".txt");

            Console.WriteLine("Usuario: " + nombreCompleto);
            Console.WriteLine("Archivo: " + rutaArchivo);

            // Validar que el archivo exista antes de continuar
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("El archivo no existe en la ruta indicada.");
                return;
            }

            // ===================== PROCESO =====================

            // Variables acumuladoras para el conteo manual
            int lineas = 0;
            int palabras = 0;
            int caracteres = 0;

            // Abrir el archivo en modo lectura
            StreamReader lector = new StreamReader(rutaArchivo);

            string? lineaActual;

            // Leer el archivo línea por línea hasta llegar al final
            while ((lineaActual = lector.ReadLine()) != null)
            {
                // Se encontró una línea nueva
                lineas++;

                // Contar caracteres de la línea uno por uno
                for (int i = 0; i < lineaActual.Length; i++)
                {
                    caracteres++;
                }

                // Contar palabras recorriendo carácter por carácter
                bool dentroDePalabra = false;

                for (int i = 0; i < lineaActual.Length; i++)
                {
                    char c = lineaActual[i];

                    if (c != ' ' && c != '\t')
                    {
                        if (!dentroDePalabra)
                        {
                            palabras++;
                            dentroDePalabra = true;
                        }
                    }
                    else
                    {
                        dentroDePalabra = false;
                    }
                }
            }

            // Cerrar el archivo tras terminar la lectura
            lector.Close();

            // ===================== SALIDAS =====================

            // Mostrar resultados en pantalla
            Console.WriteLine(
                "El archivo contiene: " +
                lineas + " líneas, " +
                palabras + " palabras, " +
                caracteres + " caracteres."
            );

            // Crear el archivo CSV dentro de la misma carpeta
            string rutaCsv = Path.Combine(
                carpeta,
                "resultados_" + nombreArchivo + ".csv"
            );

            StreamWriter escritor = new StreamWriter(rutaCsv);

            // Formato:
            // <Nombre_Apellido>,Lineas,Palabras,Caracteres
            escritor.WriteLine(
                nombreArchivo + "," +
                lineas + "," +
                palabras + "," +
                caracteres
            );

            escritor.Close();

            Console.WriteLine("Resultados guardados en " + rutaCsv);
        }
    }
}