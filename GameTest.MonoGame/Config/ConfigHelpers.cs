using System.Collections.Generic;
using GameTest.Application;
using GameTest.Entities;
using GameTest.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace GameTest.MonoGame.Config
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