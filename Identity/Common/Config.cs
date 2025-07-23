namespace Common;

public static class Config
{
	public static string GetDbConnectionString()
	{
		return Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")!;
	}

	public static string GetSecretKey()
	{
		return Environment.GetEnvironmentVariable("SECRET_KEY")!;
	}

	public static string GetIssuer()
	{
		return Environment.GetEnvironmentVariable("ISSUER")!;
	}

	public static string GetAudience()
	{
		return Environment.GetEnvironmentVariable("AUDIENCE")!;
	}
}
