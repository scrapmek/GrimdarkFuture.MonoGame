using GrimdarkFuture.Application;
using Microsoft.Extensions.DependencyInjection;

namespace GrimdarkFuture.MonoGame.Config
{
	public static class ConfigHelpers
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddSingleton<EntityStore>();
			services.AddSingleton<InputHandler>();
			services.AddSingleton<Renderer>();
			services.AddSingleton<GameTest>();

			return services;
		}
	}
}