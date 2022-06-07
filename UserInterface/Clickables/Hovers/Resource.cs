using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    public class Resource : HoverInfo
    {
        public const String DIVINITY_SAMPLE_STRING = "Divinity: 4 / 4";

        DeckBuilder.CardEnums.CostType _resource;
        Color _color;

        public Resource(Point xy, int width, int height, DeckBuilder.CardEnums.CostType resource) : base (xy, width, height, new List<string>())
        {
            _resource = resource;
           
            switch (_resource)
            {
                case DeckBuilder.CardEnums.CostType.DIVINITY:
                    _description.Add("Divinity: resets every turn.");
                    _description.Add("Most cards cost Divinity. When");
                    _description.Add("you run out, you won't be able");
                    _description.Add("to use any cards that cost");
                    _description.Add("Divinity until the next turn.");

                    _color = Color.PowderBlue;
                    break;
                default:
                    _description.Add("Not yet implemented!"); //FIXIT implement
                    break;
            }
        }

        public String getResourceText()
        {
            return DeckBuilder.CardEnums.costTypeToString(_resource) + ": " + getResourceCurrent() + " / " + getResourceMax();
        }

        public Color getColor()
        {
            return _color;
        }

        private int getResourceCurrent()
        {

            switch (_resource)
            {
                case DeckBuilder.CardEnums.CostType.DIVINITY:
                    return Game1.getChamp().getDivinity();
                default:
                    Game1.errorLog.Add("Other card costs not yet implemented in Resource.cs"); //FIXIT implement
                    return 0;
            }
        }

        private int getResourceMax()
        {

            switch (_resource)
            {
                case DeckBuilder.CardEnums.CostType.DIVINITY:
                    return Game1.getChamp().getRunDivinity();
                default:
                    Game1.errorLog.Add("Other card costs not yet implemented in Resource.cs"); //FIXIT implement
                    return 0;
            }
        }
    }
}
