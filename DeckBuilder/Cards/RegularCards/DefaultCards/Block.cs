using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.DefaultCards
{
    /// <summary>
    /// Parry | Default
    ///     (Costs 1 Divinity)
    /// Gain 5 defense.
    /// </summary>
    class Block : BasicDefenseCard
    {
        public const String NAME = "Block";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.SKILL;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int DEFENSE_GAIN = 5;

        public Block(int defense = DEFENSE_GAIN) : base(NAME, CARDTYPE, RARITY, defense)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            desc.Add("Gain " + defense + " defense.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            int defense = champ.getDefenseAffectedByBuffs(iDefense);

            desc.Add(CardStylizing.basicDefenseString(DEFENSE_GAIN, defense, descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new Block();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.DefaultCards.Block_Empowered();
        }
    }
}
