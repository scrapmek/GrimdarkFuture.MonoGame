using System;
using GrimdarkFuture.MonoGame.Config;
using Microsoft.Extensions.DependencyInjection;

namespace GrimdarkFuture.MonoGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {

			// Dependency Injection, yay!
			var serviceProvider = 
				new ServiceCollection()
					.ConfigureServices()
					.BuildServiceProvider();


            using (var game = serviceProvider.GetRequiredService<GameTest>())
                game.Run();
        }
    }
}
