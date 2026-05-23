using GameWideSystems.TooltipsManagement;
using UnityEngine;

namespace Game.UI.Tooltips
{
    public class TextTooltipParams : TooltipParams
    {
        public string Header;
        public string Body;

        public TextTooltipParams(string header, string body, Vector2 position) : base(position)
        {
            Header = header;
            Body = body;
        }
    }
}