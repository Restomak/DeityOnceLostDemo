using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures.Blessings.Rest
{
    /// <summary>
    /// At the start of combat, gain 2 Dexterity. Lasts 3 combats.
    /// </summary>
    class CatsGrace : RestBlessing
    {
        public const int DURATION = 3;
        public const int DEXTERITY_AMOUNT = 2;

        int _remainingDuration;

        public CatsGrace() : base(Game1.pic_relic_catsGrace, "Cat's Grace", new List<String>()
            { "Cat's Grace",
              "",
              "At the start of combat,",
              "gain 2 Dexterity.",
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
            extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.dexterity));

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
            return new CatsGrace();
        }



        public override void onCombatStart()
        {
            Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.dexterity, 1, DEXTERITY_AMOUNT, false, true));
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
