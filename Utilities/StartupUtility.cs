namespace Planitly.Backend.Utilities
{
    public static class StartupUtility
    {
        public static void CheckConfigVars()
        {
            if (Environment.GetEnvironmentVariable("JWT_SECRET") is null) throw new Exception("Missing JWT_SECRET environmental variable");
            if (Environment.GetEnvironmentVariable("DATABASE_URL") is null) throw new Exception("Missing DATABASE_URL environmental variable");
        }
    }
}