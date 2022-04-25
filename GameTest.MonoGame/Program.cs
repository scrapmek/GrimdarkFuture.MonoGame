using System;
using GameTest.MonoGame.Config;
using Microsoft.Extensions.DependencyInjection;

namespace GameTest.MonoGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
			var serviceProvider = 
				new ServiceCollection()
					.ConfigureServices()
					.BuildServiceProvider();


            using (var game = serviceProvider.GetRequiredService<GameTest>())
                game.Run();
        }
    }
}
