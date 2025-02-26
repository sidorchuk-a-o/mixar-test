namespace AD.Services.Router
{
    public interface IRouterService
    {
        void GoTo<TContainer>() where TContainer : UIContainer;
    }
}