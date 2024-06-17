namespace Loja.Domain.Config
{
    public static class Constants
    {
        public static ProjectConfig Project { get; set; } = new();
        public static ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ProjectConfig
    {
        public string Name { get; set; }
        public string ResponsibleDevName { get; set; }
        public string ResponsibleDevEmail { get; set; }
        public string ResponsibleDevCompanyWebSite { get; set; }
    }

    public class ConnectionStrings
    {
        public string Database { get; set; }
    }
}
