using AD.Services.Router;

namespace Game.Spaceships
{
    public class ModuleVM : ViewModel
    {
        public int Id { get; }
        public string Title { get; }
        public string Desc { get; }

        public ModuleVM(ModuleData data)
        {
            Id = data.Id;
            Title = data.Title;
            Desc = data.GetDesc();
        }

        protected override void InitSubscribes()
        {
        }
    }
}