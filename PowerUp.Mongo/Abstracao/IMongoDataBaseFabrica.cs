using MongoDB.Driver;

namespace PowerUp.Mongo.Abstracao;

public interface IMongoDataBaseFabrica
{
    IMongoDatabase CriarMongoDataBase();
}