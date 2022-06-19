using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.UserInterface.Clickables
{
    class MultiTarget : Target
    {
        Clickable _clickableUnit;
        List<MultiTarget> otherTargets;
        DeckBuilder.CardEnums.TargetingType _targetType;

        public MultiTarget(Clickable clickableUnit, DeckBuilder.CardEnums.TargetingType targetType) : base (clickableUnit, targetType)
        {
            otherTargets = new List<MultiTarget>();
        }

        public void linkMultiTargets(List<MultiTarget> allTargets)
        {
            for (int i = 0; i < allTargets.Count; i++)
            {
                if (allTargets[i] != this)
                {
                    otherTargets.Add(allTargets[i]);
                }
            }
        }

        public bool isHovered()
        {
            return _hovered;
        }

        public void setHover(bool hovered)
        {
            _hovered = hovered;
        }



        /// <summary>
        /// Same as Target, except makes sure all the otherTargets also glow
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            for (int i = 0; i < otherTargets.Count; i++)
            {
                otherTargets[i].setHover(true);
            }
            Game1.setHoveredClickable(this);
        }

        /// <summary>
        /// Same as Target, except makes sure all the otherTargets stop glowing
        /// </summary>
        public override void onHoverEnd()
        {
            _hovered = false;
            for (int i = 0; i < otherTargets.Count; i++)
            {
                otherTargets[i].setHover(false);
            }
            Game1.setHoveredClickable(null);
        }
    }
}
