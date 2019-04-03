namespace Hub.Services.Reporting.Interfaces
{
    public interface IServiceFactory
    {
        T Resolve<T>() where T : class;
    }
}
