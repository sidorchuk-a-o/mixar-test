using AD.Services.Router;
using UnityEngine;
using VContainer;

namespace Game.Spaceships
{
    public class SetupSpaceshipsContainer : UIContainer
    {
        [SerializeField] private SetupSpaceshipContainer[] setupContainers;

        [Inject]
        public void Inject(ISpaceshipsVMFactory spaceshipsVMF)
        {
            var setupsVM = spaceshipsVMF.GetSpaceshipSetups();

            for (var i = 0; i < setupsVM.Length; i++)
            {
                var setupVM = setupsVM[i];
                var setupContainer = setupContainers[i];

                setupVM.AddTo(disp);
                setupContainer.Init(setupVM, disp);
            }
        }
    }
}