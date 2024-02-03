namespace FitFriends.ServiceLibrary.Configurations
{
    public class AppConfig
    {
        public ConnectionStringsConfig ConnectionStrings { get; set; } = null!;
        public MoySkladConfig MoySklad { get; set; } = null!;
        public FileSettings FileSettings { get; set; } = null!;
    }
}
