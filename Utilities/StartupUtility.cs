namespace Planitly.Backend.Utilities
{
    public static class StartupUtility
    {
        private static IConfiguration? _config;

        public static void Configure(IConfiguration? config)
        {
            _config = config;
        }

        public static void CheckConfigVars()
        {
            if (_config.GetValue<string?>("JwtSecret") is null) throw new Exception("Missing JWT_SECRET environmental variable");
            if (_config.GetConnectionString("DefaultConnection") is null) throw new Exception("Missing DATABASE_URL environmental variable");
        }
    }
}