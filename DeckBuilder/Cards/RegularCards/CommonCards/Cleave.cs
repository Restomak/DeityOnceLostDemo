using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// Cleave | Common
    ///     (Costs 1 Divinity)
    /// Deal 6 damage to all enemies.
    /// </summary>
    class Cleave : BasicAoECard
    {
        public const String NAME = "Cleave";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 6;

        public Cleave(int damage = ATTACK_DAMAGE) : base(NAME, CARDTYPE, RARITY, damage)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            int damage = champ.getDamageAffectedByBuffs(iDamage, null);

            desc.Add("Deal " + damage + " damage to all");
            desc.Add("enemies.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();
            int damage = champ.getDamageAffectedByBuffs(iDamage, null);

            String damageString = "";
            if (damage == ATTACK_DAMAGE)
            {
                damageString += damage + "|";
            }
            else if (damage < ATTACK_DAMAGE)
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + damage + "|";
            }
            else //greater
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + damage + "|";
            }

            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Deal " + damageString + "[f: " + descFontSize + " m][c: Black]" + " damage to all");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]enemies.");

            return desc;
        }

        public override Card getNewCard()
        {
            return new Cleave();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.Cleave_Empowered();
        }
    }
}
