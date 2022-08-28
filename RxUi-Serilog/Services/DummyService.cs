using Microsoft.Extensions.Logging;

namespace Services;

public interface IDummyService
{
	bool Get();
}

public class DummyService : IDummyService
{
	private readonly ILogger<DummyService> _logger;

	public DummyService(
		ILogger<DummyService> logger
	)
	{
		_logger = logger;

		_logger.LogError("This message should be logged in a file using Serilog in /bin/x64/Debug/net6.0-windows/log[yyyymmdd].txt");
	}

	public bool Get()
	{
		return true;
	}

}
