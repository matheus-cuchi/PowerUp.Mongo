using System.Linq.Expressions;

namespace PowerUp.Mongo.Abstracao;

public interface IRepositorio<TEntidade>
    where TEntidade : EntidadeBase
{
    Task AdicionarAsync(TEntidade entidade);

    Task AtualizarAsync(TEntidade entidade);

    Task DeletarAsync(TEntidade entidade);

    Task<TEntidade> PrimeiroAsync(Expression<Func<TEntidade, bool>> filtro);

    Task<List<TEntidade>> FiltrarAsync(Expression<Func<TEntidade, bool>> filtro);
}