using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace RxUi_Serilog.Configurations;

internal static class ConfigureDependencyInjectionExtensionMethods
{
	public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
	{
		services.UseMicrosoftDependencyResolver();
		var resolver = Locator.CurrentMutable;
		resolver.InitializeSplat();
		resolver.InitializeReactiveUI();

		return services;
	}
}
