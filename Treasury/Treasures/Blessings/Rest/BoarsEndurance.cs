using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures.Blessings.Rest
{
    /// <summary>
    /// At the start of combat, gain 2 Resilience. Lasts 2 combats.
    /// </summary>
    class BoarsEndurance : RestBlessing
    {
        public const int DURATION = 2;
        public const int RESILIENCE_AMOUNT = 2;

        int _remainingDuration;

        public BoarsEndurance() : base(Game1.pic_relic_boarsEndurance, "Boar's Endurance", new List<String>()
            { "Boar's Endurance",
              "",
              "At the start of combat,",
              "gain 2 Resilience.",
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
            extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.resilience));

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
            return new BoarsEndurance();
        }



        public override void onCombatStart()
        {
            Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.resilience, 1, RESILIENCE_AMOUNT, false, true));
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
