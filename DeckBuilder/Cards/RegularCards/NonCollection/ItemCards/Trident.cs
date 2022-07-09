using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.NonCollection.ItemCards
{
    /// <summary>
    /// Trident | Default (item card)
    ///     (Costs 1 Divinity)
    /// Deal 12 damage.
    /// Gain 3 Strength.
    /// Dissipates.
    /// </summary>
    class Trident : BasicAttackCard, IStatBuffCard
    {
        public const String NAME = "Trident";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.DEFAULT;
        public const int PLAYCOST_DIV = 1;
        public const int ATTACK_DAMAGE = 12;
        public const int STRENGTH_AMOUNT = 3;

        public Trident(int strengthAmount = STRENGTH_AMOUNT) : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE)
        {
            iStatBuffAmount = strengthAmount;

            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
            _dissipates = true;
        }

        public int iStatBuffAmount { get; }

        public Combat.Unit.statType iBuffStat
        {
            get => Combat.Unit.statType.strength;
        }

        public override void onPlay()
        {
            iDealDamage();
            iApplyBuff();
        }

        public void iApplyBuff()
        {
            Game1.getChamp().gainBuff(new Combat.Buff(Combat.Buff.buffType.strength, 1, iStatBuffAmount, false, true));
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

            desc.Add("Deal " + damage + " damage.");
            desc.Add("Gain " + iStatBuffAmount + " Strength.");
            desc.Add("Dissipates.");

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

            desc.Add(CardStylizing.basicDamageString(ATTACK_DAMAGE, damage, descFontSize));
            desc.Add(CardStylizing.basicStatBuffString(STRENGTH_AMOUNT, iStatBuffAmount, Combat.Unit.statType.strength, descFontSize));
            desc.Add(CardStylizing.dissipates(descFontSize));

            return desc;
        }

        public override Card getNewCard()
        {
            return new Trident();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.NonCollection.ItemCards.Trident_Empowered();
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(Combat.Buff.getExtraInfo(Combat.Buff.buffType.strength));
            extraInfo.Add(getDissipateExtraInfo());

            return extraInfo;
        }
    }
}
