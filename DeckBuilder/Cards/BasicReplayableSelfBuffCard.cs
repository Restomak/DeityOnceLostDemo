using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    abstract class BasicReplayableSelfBuffCard : Card, IStatBuffCard
    {
        protected int _buffAmount;
        protected Combat.Unit.statType _buffType;

        public BasicReplayableSelfBuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int buffAmount, Combat.Unit.statType buffType) : base(name, cardType, rarity, CardEnums.TargetingType.champion)
        {
            _buffAmount = buffAmount;
            _buffType = buffType;
        }

        public int amount
        {
            get => _buffAmount;
        }
        public Combat.Unit.statType stat
        {
            get => _buffType;
        }

        public override void onPlay()
        {
            applyBuff();
        }

        public void applyBuff()
        {
            switch (_buffType)
            {
                case Combat.Unit.statType.strength:
                    Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.strength, 1, _buffAmount, false, true, false));
                    break;
                case Combat.Unit.statType.dexterity:
                    Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.dexterity, 1, _buffAmount, false, true, false));
                    break;
                case Combat.Unit.statType.resilience:
                    Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.resilience, 1, _buffAmount, false, true, false));
                    break;
                default:
                    Game1.errorLog.Add("New Combat.Unit.statType was declared but not setup in BasicReplayableSelfBuffCard.applyBuff: " + _buffType.ToString());
                    break;
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<string>();

            desc.Add("Gain " + _buffAmount + " " + _buffType.ToString() + ".");

            return desc;
        }
    }
}
