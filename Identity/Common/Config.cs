namespace Common;

public static class Config
{
	public static string GetDbConnectionString()
	{
		return Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")!;
	}
}
