namespace listasligadas;


public class Ocurrencia<T>
{
    public T Valor;
    public int Veces;

    public Ocurrencia(T valor, int veces)
    {
        Valor = valor;
        Veces = veces;
    }
}
