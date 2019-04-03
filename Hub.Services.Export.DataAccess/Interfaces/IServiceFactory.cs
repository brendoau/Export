namespace Hub.Services.Export.Interfaces
{
    public interface IServiceFactory
    {
        T Resolve<T>() where T : class;
    }
}
