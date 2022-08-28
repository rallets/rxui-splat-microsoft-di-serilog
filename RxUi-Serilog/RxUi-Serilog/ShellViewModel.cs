using ReactiveUI;
using Services;
using Splat;

namespace RxUi_Serilog;

public class ShellViewModel : ReactiveObject
{
	private readonly IDummyService _dummyService;

	public ShellViewModel(
		IDummyService? dummyService = null
		)
	{
		_dummyService = dummyService ?? Locator.Current.GetService<IDummyService>()!;
	}

}
