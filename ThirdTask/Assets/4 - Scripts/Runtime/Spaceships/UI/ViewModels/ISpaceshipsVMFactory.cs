using AD.Services.Router;

namespace Game.Spaceships
{
    public interface ISpaceshipsVMFactory
    {
        OptionDataList<int> WeaponOptions { get; }
        OptionDataList<int> ModuleOptions { get; }

        SpaceshipSetupVM[] GetSpaceshipSetups();
    }
}