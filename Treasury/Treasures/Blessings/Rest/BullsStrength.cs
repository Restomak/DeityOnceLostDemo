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
    /// At the start of combat, gain 2 Strength. Lasts 3 combats.
    /// </summary>
    class BullsStrength : RestBlessing
    {
        public const int DURATION = 3;
        public const int STRENGTH_AMOUNT = 2;

        int _remainingDuration;

        public BullsStrength() : base(Game1.pic_relic_bullsStrength, "Bull's Strength", new List<String>()
            { "Bull's Strength",
              "",
              "At the start of combat,",
              "gain 2 Strength.",
              "",
              "Lasts 3 combats."
            })
        {
            _remainingDuration = DURATION;
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(new UserInterface.ExtraInfos.TextInfo(_description));
            extraInfo.Add(RestBlessing.getRestBlessingInfo());
            extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.strength));

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
            return new BullsStrength();
        }



        public override void onCombatStart()
        {
            Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.strength, 1, STRENGTH_AMOUNT, false, true));
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
