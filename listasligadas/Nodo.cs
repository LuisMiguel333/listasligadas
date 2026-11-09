namespace listasligadas;


public class Nodo<T>
{
    public T Dato;
    public Nodo<T>? Siguiente;
    public Nodo<T>? Anterior;

    public Nodo(T dato)
    {
        Dato = dato;
        Siguiente = null;
        Anterior = null;
    }
}
