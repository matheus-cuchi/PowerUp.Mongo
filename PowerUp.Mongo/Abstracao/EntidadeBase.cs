using MongoDB.Bson.Serialization.Attributes;

namespace PowerUp.Mongo.Abstracao;

public abstract class EntidadeBase
{
    [BsonId]
    [BsonElement("Id")]
    public Guid Id { get; protected init; }

    protected EntidadeBase()
    { }
}