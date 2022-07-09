using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures.Blessings.Rest
{
    /// <summary>
    /// Draw 1 more card each turn. Lasts 2 combats.
    /// </summary>
    class OwlsWisdom : RestBlessing
    {
        public const int DURATION = 2;

        int _remainingDuration;

        public OwlsWisdom() : base(Game1.pic_relic_owlsWisdom, "Owl's Wisdom", new List<String>()
            { "Owl's Wisdom",
              "",
              "Draw 1 more card",
              "each turn.",
              "",
              "Lasts 2 combats."
            })
        {
            _remainingDuration = DURATION;
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.TextInfo(_description));
            extraInfo.Add(RestBlessing.getRestBlessingInfo());

            return extraInfo;
        }

        public override void onTaken()
        {
            Game1.getDungeonHandler().addRelic(this);
            _taken = true;
            Game1.updateTopBar();
        }

        public override RestBlessing getNewRestBlessing()
        {
            return new OwlsWisdom();
        }



        public override void onTurnStartAfterDraw()
        {
            Game1.getChamp().getDeck().drawNumCards(1);
        }

        public override void onCombatEnd()
        {
            _remainingDuration--;

            if (_remainingDuration == 0)
            {
                Game1.getDungeonHandler().removeRelic(this);
            }
        }
    }
}
