using iteraBus.Repositorio.Contexto;

public abstract class BaseRepositorio
{
    protected readonly IteraBusContexto _contexto;

    protected BaseRepositorio(IteraBusContexto contexto)
    {
        _contexto = contexto;
    }
}