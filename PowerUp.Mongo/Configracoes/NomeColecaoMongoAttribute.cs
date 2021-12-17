namespace PowerUp.Mongo.Configracoes;

public class NomeColecaoMongoAttribute : Attribute
{
    public string NomeColecao { get; }

    public NomeColecaoMongoAttribute(string nomeColecao)
    {
        NomeColecao = nomeColecao;
    }
}