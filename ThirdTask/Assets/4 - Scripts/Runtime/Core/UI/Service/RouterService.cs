using static UnityEngine.Object;

namespace AD.Services.Router
{
    public class RouterService : IRouterService
    {
        private readonly RouterContainer routerContainer;

        public RouterService()
        {
            routerContainer = FindAnyObjectByType<RouterContainer>();
            routerContainer.Init();
        }

        public void GoTo<TContainer>() where TContainer : UIContainer
        {
            routerContainer.GoToContainer<TContainer>();
        }
    }
}