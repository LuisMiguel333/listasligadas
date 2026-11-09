namespace listasligadas;

public class ListaDoble<T> where T : IComparable<T>
{
    private Nodo<T>? cabeza;      
    private Nodo<T>? cola;        
    private int cantidad;         
    private bool esDescendente;  

    public ListaDoble()
    {
        cabeza = null;
        cola = null;
        cantidad = 0;
        esDescendente = false;
    }

    //  ADD (inserts directly in its position - ordenado)

    public void Adicionar(T dato)
    {
        Nodo<T> nuevo = new Nodo<T>(dato);

        // the list is empty - vacios
        if (cabeza == null || cola == null)
        {
            cabeza = nuevo;
            cola = nuevo;
            cantidad++;
            return;
        }


        Nodo<T>? actual = cabeza;
        while (actual != null && !DebeIrAntes(dato, actual.Dato))
        {
            actual = actual.Siguiente;
        }

        if (actual == null)
        {
            // goes at the end - finaly
            nuevo.Anterior = cola;
            cola.Siguiente = nuevo;
            cola = nuevo;
        }
        else if (actual == cabeza)
        {
            //  goes at beginning - inizio
            nuevo.Siguiente = cabeza;
            cabeza.Anterior = nuevo;
            cabeza = nuevo;
        }
        else
        {

            // insert in the middle - medio
            Nodo<T>? anterior = actual.Anterior;
            if (anterior != null)
            {
                anterior.Siguiente = nuevo;
            }
            nuevo.Anterior = anterior;
            nuevo.Siguiente = actual;
            actual.Anterior = nuevo;
        }

        cantidad++;
    }


    private bool DebeIrAntes(T nuevo, T existente)
    {
        int comparacion = nuevo.CompareTo(existente);

        if (esDescendente)
        {
            return comparacion > 0;
        }
        return comparacion < 0;
    }


    public void MostrarAdelante()
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        Nodo<T>? actual = cabeza;
        // traverse forward from head
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }


    public void MostrarAtras()
    {
        if (cola == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        Nodo<T>? actual = cola;
        // traverse backwards from tail - hacia atras
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Anterior;
        }
        Console.WriteLine();
    }

    public void OrdenarDescendentemente()
    {
        if (esDescendente)
        {
            Console.WriteLine("La lista ya está en orden descendente.");
            return;
        }

        Nodo<T>? actual = cabeza;
        while (actual != null)
        {

            // swap the ponters
            Nodo<T>? temporal = actual.Siguiente;
            actual.Siguiente = actual.Anterior;
            actual.Anterior = temporal;

            // move to next node - siguiente
            actual = temporal;
        }

        Nodo<T>? auxiliar = cabeza;
        cabeza = cola;
        cola = auxiliar;

        esDescendente = true;
        Console.WriteLine("La lista quedó en orden descendente.");
    }

    private List<Ocurrencia<T>> ContarOcurrencias()
    {
        List<Ocurrencia<T>> resultado = new List<Ocurrencia<T>>();

        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            int ultimo = resultado.Count - 1;

            if (ultimo >= 0 && resultado[ultimo].Valor.CompareTo(actual.Dato) == 0)
            {
                // increment the count - incremento
                resultado[ultimo].Veces++;
            }
            else
            {
                // create new occurrance entry - entrada
                resultado.Add(new Ocurrencia<T>(actual.Dato, 1));
            }

            actual = actual.Siguiente;
        }

        return resultado;
    }


    public void MostrarModas()
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        List<Ocurrencia<T>> ocurrencias = ContarOcurrencias();


        // find the maximum ocurances
        int maximo = 0;
        foreach (Ocurrencia<T> o in ocurrencias)
        {
            if (o.Veces > maximo)
            {
                maximo = o.Veces;
            }
        }

        // collect all modes - modas are the most frequent values
        List<T> modas = new List<T>();
        foreach (Ocurrencia<T> o in ocurrencias)
        {
            if (o.Veces == maximo)
            {
                modas.Add(o.Valor);
            }
        }

        if (modas.Count == 1)
        {
            Console.WriteLine("La moda es " + modas[0] + ".");
        }
        else
        {
            Console.Write("Las modas son ");
            for (int i = 0; i < modas.Count; i++)
            {
                if (i > 0 && i == modas.Count - 1)
                {
                    Console.Write(" y ");
                }
                else if (i > 0)
                {
                    Console.Write(", ");
                }
                Console.Write(modas[i]);
            }
            Console.WriteLine(".");
        }
    }


    public void MostrarGrafico()
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía.");
            return;
        }

        List<Ocurrencia<T>> ocurrencias = ContarOcurrencias();

        foreach (Ocurrencia<T> o in ocurrencias)
        {
            Console.WriteLine(o.Valor + " " + new string('*', o.Veces));
        }
    }


    public bool Existe(T dato)
    {
        // search for the element
        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            if (actual.Dato.CompareTo(dato) == 0)
            {
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }


    public bool EliminarUna(T dato)
    {
        Nodo<T>? actual = cabeza;
        while (actual != null)
        {
            if (actual.Dato.CompareTo(dato) == 0)
            {
                EliminarNodo(actual);
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }


    public int EliminarTodas(T dato)
    {
        int eliminados = 0;

        // traverse and remove all occuances
        Nodo<T>? actual = cabeza;
        while (actual != null)
        {

            // save next before deletion
            Nodo<T>? siguiente = actual.Siguiente;

            if (actual.Dato.CompareTo(dato) == 0)
            {
                EliminarNodo(actual);
                eliminados++;
            }

            actual = siguiente;
        }

        return eliminados;
    }


    // removes a node from the doubly linked list 
    private void EliminarNodo(Nodo<T> nodo)
    {
        Nodo<T>? anterior = nodo.Anterior;
        Nodo<T>? siguiente = nodo.Siguiente;

        if (anterior != null)
        {
            anterior.Siguiente = siguiente;
        }
        else
        {
            cabeza = siguiente;   // delete the first on
        }

        if (siguiente != null)
        {
            siguiente.Anterior = anterior;
        }
        else
        {
            cola = anterior;      // delete the last one 
        }

        cantidad--;
    }
}
