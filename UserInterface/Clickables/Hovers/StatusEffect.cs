using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    class StatusEffect : HoverInfo
    {
        Combat.Buff _buff;

        public StatusEffect(Point xy, int width, int height, Combat.Buff buff) : base(xy, width, height, new List<string>())
        {
            _buff = buff;

            _description = Combat.Buff.buffDescription(_buff);
            _description.Insert(0, Combat.Buff.buffString(_buff.getType()));
        }

        public Combat.Buff getBuff()
        {
            return _buff;
        }

        public Texture2D getIcon()
        {
            return Combat.Buff.buffIcon(_buff.getType());
        }

        public Color getBorderColor()
        {
            return Combat.Buff.buffBorderColor(_buff);
        }

        public int getImportantBuffNumber()
        {
            if (_buff.hasAmount())
            {
                return _buff.getAmount();
            }
            else
            {
                return _buff.getDuration();
            }
        }
    }
}
