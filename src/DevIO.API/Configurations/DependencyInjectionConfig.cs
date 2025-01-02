using DevIO.Business.Interfaces;
using DevIO.Business.Notificacoes;
using DevIO.Business.Servicos;
using DevIO.Data.Context;
using DevIO.Data.Repositorio;

namespace DevIO.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Data
            services.AddScoped<AppDbContext>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IFornecedorRepositorio, FornecedorRepositorio>();

            // Business
            services.AddScoped<IFornecedorService, FornecedorServico>();
            services.AddScoped<IProdutoService, ProdutoServico>();
            services.AddScoped<INotificador, Notificador>();

            return services;
        }

    }
}
