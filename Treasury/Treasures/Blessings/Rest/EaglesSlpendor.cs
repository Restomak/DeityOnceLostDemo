using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Treasures.Blessings.Rest
{
    /// <summary>
    /// Gain 1 Divinity at the start of each turn. Lasts 2 combats.
    /// </summary>
    class EaglesSlpendor : RestBlessing
    {
        public const int DURATION = 2;

        int _remainingDuration;

        public EaglesSlpendor() : base(Game1.pic_relic_eaglesSplendor, "Eagle's Splendor", new List<String>()
            { "Eagle's Splendor",
              "",
              "Gain 1 Divinity at the",
              "start of each turn.",
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
            return new EaglesSlpendor();
        }



        public override void onTurnStart()
        {
            Game1.getChamp().spendDivinity(-1);
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
