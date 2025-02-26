using System.Linq;
using AD.Services.Router;
using static TMPro.TMP_Dropdown;

namespace Game.Spaceships
{
    public class SpaceshipsVMFactory : ISpaceshipsVMFactory
    {
        private readonly SpaceshipsConfig config;

        public OptionDataList<int> WeaponOptions { get; }
        public OptionDataList<int> ModuleOptions { get; }

        public SpaceshipsVMFactory(SpaceshipsConfig config)
        {
            this.config = config;

            WeaponOptions = GetWeaponOptions();
            ModuleOptions = GetModuleOptions();
        }

        private OptionDataList<int> GetWeaponOptions()
        {
            var values = config.Weapons.Select(x => x.Id).ToList();
            var options = config.Weapons.Select(x => new OptionData($"{x.Title} | {x.GetDesc()}")).ToList();

            values.Insert(0, -1);
            options.Insert(0, new OptionData("Пусто"));

            return new(values, options);
        }

        private OptionDataList<int> GetModuleOptions()
        {
            var values = config.Modules.Select(x => x.Id).ToList();
            var options = config.Modules.Select(x => new OptionData($"{x.Title} | {x.GetDesc()}")).ToList();

            values.Insert(0, -1);
            options.Insert(0, new OptionData("Пусто"));

            return new(values, options);
        }

        public SpaceshipSetupVM[] GetSpaceshipSetups()
        {
            return config.Spaceships
                .Select(x => new SpaceshipSetupVM(x))
                .ToArray();
        }
    }
}