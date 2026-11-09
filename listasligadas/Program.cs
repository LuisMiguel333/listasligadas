namespace listasligadas;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Taller 5 - Lista doblemente ligada genérica ===");
        Console.WriteLine("¿Con qué tipo de dato quieres trabajar?");
        Console.WriteLine("1. Números enteros");
        Console.WriteLine("2. Texto (palabras)");
        Console.Write("Opción: ");
        string? tipo = Console.ReadLine();

        if (tipo == "1")
        {
            Menu(new ListaDoble<int>());
        }
        else if (tipo == "2")
        {
            Menu(new ListaDoble<string>());
        }
        else
        {
            Console.WriteLine("Opción no válida.");
        }
    }

    // Menu
    static void Menu<T>(ListaDoble<T> lista) where T : IComparable<T>
    {
        int opcion = -1;

        while (opcion != 0)
        {
            MostrarMenu();

            string? texto = Console.ReadLine();
            if (!int.TryParse(texto, out opcion))
            {
                opcion = -1;
                Console.WriteLine("Escribe un número del menú.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    Console.Write("Escribe el dato (puedes separar varios con coma): ");
                    string? linea = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(linea))
                    {
                        Console.WriteLine("No escribiste nada.");
                        break;
                    }

                    string[] partes = linea.Split(',');
                    foreach (string parte in partes)
                    {
                        if (parte.Trim() == "")
                        {
                            continue;
                        }

                        try
                        {
                            T valor = (T)Convert.ChangeType(parte.Trim(), typeof(T));
                            lista.Adicionar(valor);
                        }
                        catch
                        {
                            Console.WriteLine("Dato inválido: " + parte.Trim());
                        }
                    }
                    Console.WriteLine("Listo.");
                    break;

                case 2:
                    lista.MostrarAdelante();
                    break;

                case 3:
                    lista.MostrarAtras();
                    break;

                case 4:
                    lista.OrdenarDescendentemente();
                    break;

                case 5:
                    lista.MostrarModas();
                    break;

                case 6:
                    lista.MostrarGrafico();
                    break;

                case 7:
                    if (LeerDato(out T buscado))
                    {
                        if (lista.Existe(buscado))
                        {
                            Console.WriteLine("Sí existe el " + buscado + ".");
                        }
                        else
                        {
                            Console.WriteLine("No existe el " + buscado + ".");
                        }
                    }
                    break;

                case 8:
                    if (LeerDato(out T unico))
                    {
                        if (lista.EliminarUna(unico))
                        {
                            Console.WriteLine("Se eliminó una ocurrencia del " + unico + ".");
                        }
                        else
                        {
                            Console.WriteLine("No existe el " + unico + ".");
                        }
                    }
                    break;

                case 9:
                    if (LeerDato(out T varios))
                    {
                        int eliminados = lista.EliminarTodas(varios);
                        if (eliminados > 0)
                        {
                            Console.WriteLine("Se eliminaron " + eliminados + " ocurrencias del " + varios + ".");
                        }
                        else
                        {
                            Console.WriteLine("No existe el " + varios + ".");
                        }
                    }
                    break;

                case 0:
                    Console.WriteLine("Hasta luego.");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine();
        Console.WriteLine("----------- MENÚ -----------");
        Console.WriteLine("1. Adicionar");
        Console.WriteLine("2. Mostrar hacia adelante");
        Console.WriteLine("3. Mostrar hacia atrás");
        Console.WriteLine("4. Ordenar descendentemente");
        Console.WriteLine("5. Mostrar la(s) moda(s)");
        Console.WriteLine("6. Mostrar gráfico");
        Console.WriteLine("7. Existe");
        Console.WriteLine("8. Eliminar una ocurrencia");
        Console.WriteLine("9. Eliminar todas las ocurrencias");
        Console.WriteLine("0. Salir");
        Console.Write("Elige una opción: ");
    }

    // Lee un dato del teclado y lo convierte al tipo de la lista 
    static bool LeerDato<T>(out T dato)
    {
        dato = default!;

        Console.Write("Escribe el dato: ");
        string? texto = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(texto))
        {
            Console.WriteLine("No escribiste nada.");
            return false;
        }

        try
        {
            dato = (T)Convert.ChangeType(texto.Trim(), typeof(T));
            return true;
        }
        catch
        {
            Console.WriteLine("Dato inválido para este tipo de lista.");
            return false;
        }
    }
}