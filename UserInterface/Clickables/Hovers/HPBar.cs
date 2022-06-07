using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    public class HPBar : HoverInfo
    {
        public enum hpBarType
        {
            enemy,
            champion,
            party,
            minion
        }

        hpBarType _type;
        Combat.Unit _unit;

        public HPBar(Point xy, int width, int height, Combat.Unit unit, hpBarType type) : base(xy, width, height, new List<string>())
        {
            _unit = unit;
            _type = type;

            switch (_type)
            {
                case hpBarType.enemy:
                    _description.Add("The enemy's hitpoints.");
                    _description.Add("When they reach zero, the");
                    _description.Add("enemy will be defeated.");
                    break;
                case hpBarType.champion:
                    _description.Add("Your champion's hitpoints. If");
                    _description.Add("they reach zero, your champion");
                    _description.Add("will die and you will be forced");
                    _description.Add("out of " + Game1.getChamp().getHero().getPronoun_their().ToLower() + " body.");
                    _description.Add("Cards that cost Blood spend");
                    _description.Add("hitpoints when played.");
                    break;
                case hpBarType.party:
                    _description.Add("A party member's hitpoints.");
                    _description.Add("If they reach zero, the");
                    _description.Add("party member will die.");
                    break;
                case hpBarType.minion:
                    _description.Add("A minion's hitpoints. When");
                    _description.Add("they reach zero, the minion");
                    _description.Add("will be desummoned. If the");
                    _description.Add("summoner is defeated, that");
                    _description.Add("will also desummon the minion.");
                    break;
            }
        }

        public Color getBarColor()
        {
            switch (_type)
            {
                case hpBarType.champion:
                case hpBarType.party:
                    return Color.Green;
                default:
                    return Color.Red;
            }
        }

        public Combat.Unit getUnit()
        {
            return _unit;
        }
    }
}
