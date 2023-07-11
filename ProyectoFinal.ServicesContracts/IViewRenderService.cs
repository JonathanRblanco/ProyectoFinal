namespace ProyectoFinal.ServicesContracts
{
    public interface IViewRenderService
    {
        Task<string> RenderToString(string viewPathOrName, object model);
    }
}
