// See https://aka.ms/new-console-template for more information
using System.Drawing;
using GameTest.Application;
using GameTest.Application.Config;
using GameTest.Entities;
using GameTest.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var cts = new CancellationTokenSource() ;
Console.CancelKeyPress += (x, y) =>
{
	Console.WriteLine("Cancelling;");
	y.Cancel = false;
	cts.Cancel();
};

await Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
			{
				services
					.AddHostedService<App>()
					.ConfigureAssets();
			}
		)
	.ConfigureLogging( logging => { })
	.Build()
	.RunAsync(cts.Token);

internal class App : BackgroundService
{
	private readonly ILogger<App> logger;
	private readonly Renderer renderer;
	private readonly EntityStore entityStore;
	private readonly InputHandler inputHandler;

	public App(ILogger<App> logger, Renderer renderer, EntityStore entityStore, InputHandler inputHandler)
	{
		this.logger = logger;
		this.renderer = renderer;
		this.entityStore = entityStore;
		this.inputHandler = inputHandler;
	}


	protected async override Task ExecuteAsync(CancellationToken cancellationToken)
	{


		await Task.WhenAll(new Task[] {
			renderer.Render(cancellationToken),
			entityStore.Update(cancellationToken),
			inputHandler.ReadInputs(cancellationToken)
		});


	}
}