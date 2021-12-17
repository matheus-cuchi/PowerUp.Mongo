using Microsoft.Extensions.DependencyInjection.Extensions;
using PowerUp.Mongo.Abstracao;
using PowerUp.Mongo.Configracoes;
using PowerUp.Mongo.Contextos;

namespace PowerUp.Mongo.Modulos;

public static class Dependencias
{
    public static void AdicionarMongo(this IServiceCollection servicos, IConfiguration configuracoes)
    {
        servicos.AdicionarConfiguracao<ConfiguracaoMongo>(configuracoes);
        servicos.AddSingleton<IMongoDataBaseFabrica, MongoDataBaseFabrica>();

        servicos.AddScoped(
            provider =>
            {
                var fabrica = provider.GetService<IMongoDataBaseFabrica>() ?? throw new ArgumentException(nameof(IMongoDataBaseFabrica));
                var dataBase = fabrica.CriarMongoDataBase();

                return new ContextoMongo(dataBase);
            });
    }

    public static void AdicionarConfiguracao<TConfiguracao>(this IServiceCollection servicos, IConfiguration configuracoes)
        where TConfiguracao : class
    {
        var configuracaoInstancia = Activator.CreateInstance<TConfiguracao>();
        configuracoes.Bind(typeof(TConfiguracao).Name, configuracaoInstancia);
        servicos.TryAddSingleton(configuracaoInstancia);
    }
}