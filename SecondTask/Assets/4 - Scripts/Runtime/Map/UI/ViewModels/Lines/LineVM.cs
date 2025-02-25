using AD.Services.Router;
using UnityEngine;

namespace Game.Map
{
    public class LineVM : ViewModel
    {
        public int Id { get; }
        public Color Color { get; }

        public LineVM(LineData data)
        {
            Id = data.Id;
            Color = data.Color;
        }

        protected override void InitSubscribes()
        {
        }
    }
}