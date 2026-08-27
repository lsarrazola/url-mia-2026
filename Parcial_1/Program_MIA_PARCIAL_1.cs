using System;
using System.IO;

Console.Write("Ingrese su nombre completo: ");
string nombre = Console.ReadLine() ?? "";

Console.Write("Ingrese la ruta del archivo: ");
string ruta = Console.ReadLine() ?? "";

if (!File.Exists(ruta))
{
    Console.WriteLine("El archivo no existe.");
    return;
}

string texto = File.ReadAllText(ruta);
string[] listaLineas = File.ReadAllLines(ruta);

int lineas = listaLineas.Length;
int palabras = 0;
int caracteres = texto.Length;

string[] partes = texto.Split(' ', '\n', '\r', '\t');

foreach (string palabra in partes)
{
    if (palabra != "")
    {
        palabras++;
    }
}

string carpeta = Path.GetDirectoryName(ruta)
                  ?? Directory.GetCurrentDirectory();

string rutaCsv = Path.Combine(
    carpeta,
    "resultados_Luna_Arrazola.csv"
);

string csv =
    "Nombre_Apellido,Lineas,Palabras,Caracteres\n" +
    nombre.Replace(" ", "_") + "," +
    lineas + "," +
    palabras + "," +
    caracteres;

File.WriteAllText(rutaCsv, csv);

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