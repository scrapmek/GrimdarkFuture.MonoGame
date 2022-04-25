using System.Collections.Generic;
using GrimdarkFuture.Application;
using GrimdarkFuture.Entities;
using GrimdarkFuture.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

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