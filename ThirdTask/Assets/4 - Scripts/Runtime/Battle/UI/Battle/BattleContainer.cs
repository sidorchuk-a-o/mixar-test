using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Battle
{
    public class BattleContainer : UIContainer
    {
        [SerializeField] private SpaceshipStateContainer[] spaceshipStates;

        private IBattleVMFactory battleVMF;
        private SpaceshipVM[] spaceshipsVM;

        [Inject]
        public void Inject(IBattleVMFactory battleVMF)
        {
            this.battleVMF = battleVMF;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            spaceshipsVM = battleVMF.GetSpacehips();
            spaceshipsVM.ForEach(x => x.AddTo(disp));

            foreach (var spaceshipVM in spaceshipsVM)
            {
                var stateContainer = spaceshipStates.First(x =>
                {
                    return x.SpacehipId == spaceshipVM.Id;
                });

                stateContainer.Init(spaceshipVM, disp);
            }
        }
    }
}