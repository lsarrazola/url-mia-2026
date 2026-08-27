using System;
using System.IO;

// Entradas

Console.Write("Ingrese su nombre completo: ");
string nombre = Console.ReadLine() ?? "";

Console.Write("Ingrese la ruta del archivo: ");
string ruta = Console.ReadLine() ?? "";

if (!File.Exists(ruta))
{
    Console.WriteLine("El archivo no existe.");
    return;
}

// Proceso

string texto = File.ReadAllText(ruta);

string[] lineasArchivo = File.ReadAllLines(ruta);

int lineas = lineasArchivo.Length;


int caracteres = texto.Length;

int palabras = 0;

string[] listaPalabras = texto.Split(' ', '\n', '\r', '\t');

foreach (string palabra in listaPalabras)
{
    if (palabra != "")
    {
        palabras++;
    }
}

string carpeta = Path.GetDirectoryName(ruta) ?? "";

string nombreArchivo = nombre.Replace(" ", "_");
string rutaCsv = Path.Combine(carpeta, "resultados_" + nombreArchivo + ".csv");

string csv =
"Nombre_Apellido,Lineas,Palabras,Caracteres\n" +
nombreArchivo + "," +
lineas + "," +
palabras + "," +
caracteres;

File.WriteAllText(rutaCsv, csv);

// Salidas

Console.WriteLine();
Console.WriteLine("Resultaos...");
Console.WriteLine("Usuario: " + nombre);
Console.WriteLine("Archivo: " + ruta);
Console.WriteLine("Líneas: " + lineas);
Console.WriteLine("Palabras: " + palabras);
Console.WriteLine("Caracteres: " + caracteres);
Console.WriteLine();
Console.WriteLine("Resultados guardados en:");
Console.WriteLine(rutaCsv);