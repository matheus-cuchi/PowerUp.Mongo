using PowerUp.Mongo.Abstracao;

namespace PowerUp.Mongo.Entidades;

public class Pessoa : EntidadeBase
{
    public string Nome { get; set; }
    public byte Idade { get; set; }
}