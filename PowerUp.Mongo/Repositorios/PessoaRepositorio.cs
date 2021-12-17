using PowerUp.Mongo.Contextos;
using PowerUp.Mongo.Entidades;
using PowerUp.Mongo.Implementacoes;

namespace PowerUp.Mongo.Repositorios;

public class PessoaRepositorio : RepositorioBaseMongo<Pessoa>, IPessoaRepositorio
{
    public PessoaRepositorio(ContextoMongo contextoMongo) : base(contextoMongo)
    { }
}