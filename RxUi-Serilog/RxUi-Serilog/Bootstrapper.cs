using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using RxUi_Serilog.Configurations;
using RxUi_Serilog.Helpers;
using Splat.Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using Serilog;
using Splat;
using Splat.Serilog;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace RxUi_Serilog;

public static class Bootstrapper
{
	public static IServiceProvider Container { get; private set; }

	public static void Run()
	{
		var viewModel = new ShellViewModel();
		var view = ViewLocator.Current.ResolveView(viewModel);
		view.ViewModel = viewModel;
		Application.Run((Form)view);
	}

	public static void Init()
	{
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

		RxApp.DefaultExceptionHandler = new CustomObservableExceptionHandler();

		var host = Host
			.CreateDefaultBuilder()
			.ConfigureAppConfiguration(builder =>
			{
				builder.AddJsonFile("appsettings.json", optional: false);
			})
			.ConfigureServices((context, services) =>
            {
                // Configure DependencyInjection
                services.UseMicrosoftDependencyResolver();
                var resolver = Locator.CurrentMutable;
                resolver.InitializeSplat();
                resolver.InitializeReactiveUI();

                // Configure Services
                services.AddTransient<IDummyService, DummyService>();

                // Configure Forms
                services.AddSingleton<ShellViewModel>();
                services.AddSingleton<IViewFor<ShellViewModel>, ShellView>();
            })
			.ConfigureLogging((context, builder) =>
			{
                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(context.Configuration)
                    .CreateLogger();

                Locator.CurrentMutable.UseSerilogFullLogger(logger);
            })
			//.UseEnvironment(Environments.Development)
			.Build();

		// Since MS DI container is a different type, we need to re-register the built container with Splat again
		Container = host.Services;
		Container.UseMicrosoftDependencyResolver();
	}

}
