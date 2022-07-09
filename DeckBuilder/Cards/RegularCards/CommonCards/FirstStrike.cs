using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.CommonCards
{
    /// <summary>
    /// First Strike | Common
    ///     (Costs 1 Divinity)
    /// Deal 8 damage.
    /// Damage is doubled if target is full HP.
    /// </summary>
    class FirstStrike : BasicAttackCard
    {
        public const String NAME = "First Strike";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.COMMON;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 8;

        public FirstStrike(int damage = ATTACK_DAMAGE) : base(NAME, CARDTYPE, RARITY, damage)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override Card getNewCard()
        {
            return new FirstStrike();
        }

        public override void iDealDamage()
        {
            if (_target != null)
            {
                if (_target.getCurrentHP() == _target.getMaxHP())
                {
                    _target.takeDamage(Game1.getChamp().getDamageAffectedByBuffs(iDamage * 2, _target));
                }
                else
                {
                    _target.takeDamage(Game1.getChamp().getDamageAffectedByBuffs(iDamage, _target));
                }
            }
            else
            {
                Game1.addToErrorLog("Attempted to use damage card without a target!");
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<string>();
            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(iDamage, descTarget);
            if (descTarget != null && descTarget.getCurrentHP() == descTarget.getMaxHP())
            {
                damage = champ.getDamageAffectedByBuffs(iDamage * 2, descTarget);
            }

            desc.Add("Deal " + damage + " damage.");
            desc.Add("Damage is doubled if");
            desc.Add("target is full HP.");

            return desc;
        }

        public override List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(iDamage, descTarget);
            if (descTarget != null && descTarget.getCurrentHP() == descTarget.getMaxHP())
            {
                damage = champ.getDamageAffectedByBuffs(iDamage * 2, descTarget);
            }

            desc.Add(CardStylizing.basicDamageString(ATTACK_DAMAGE, damage, descFontSize));
            desc.Add("[f: " + descFontSize + " m][c: Black]Damage is doubled if");
            desc.Add("[f: " + descFontSize + " m][c: Black]target is full HP.");

            return desc;
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.CommonCards.FirstStrike_Empowered();
        }
    }
}
