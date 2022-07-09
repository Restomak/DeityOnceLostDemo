using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RegularCards.RareCards
{
    /// <summary>
    /// Quake Stomp | Rare
    ///     (Costs 2 Divinity)
    /// Deal 12 damage.
    /// Choose up to 3 cards to discard from your draw pile and
    ///     then draw 1 card.
    /// </summary>
    class QuakeStomp : AttackAndDrawCard
    {
        public const String NAME = "Quake Stomp";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.HYBRID;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 12;
        public const int CARD_DRAW = 1;
        public const int DISCARD_CHOICE_AMOUNT = 3;

        public QuakeStomp(int damage = ATTACK_DAMAGE, int cardDraw = CARD_DRAW) : base(NAME, CARDTYPE, RARITY, damage, cardDraw)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override void onPlay()
        {
            iDealDamage();
            Game1.addToMenus(new UserInterface.Menus.CombatCardChoiceMenu(Game1.getChamp().getDeck().getDrawPile(), UserInterface.Menus.CombatCardChoiceMenu.whereFrom.drawToDiscard, () =>
            {
                iCardDraw();
            }, DISCARD_CHOICE_AMOUNT));
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<String>();

            Combat.Unit descTarget = null;
            if (activeCard)
            {
                descTarget = Card.getTargetForDescription(_targetType);
            }
            int damage = champ.getDamageAffectedByBuffs(iDamage, descTarget);
            desc.Add("Deal " + damage + " damage.");

            desc.Add("Choose up to " + DISCARD_CHOICE_AMOUNT + " cards");
            desc.Add("to discard from your");
            desc.Add("draw pile and then");

            String drawCardString = "draw " + iDrawAmount + " card";
            if (iDrawAmount > 1)
            {
                drawCardString += "s";
            }
            drawCardString += ".";

            desc.Add(drawCardString);

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

            String cardDrawString = "[f: " + descFontSize + " m]" + "[c: Black]draw ";
            if (iDrawAmount == CARD_DRAW)
            {
                cardDrawString += iDrawAmount;
            }
            else if (iDrawAmount < CARD_DRAW)
            {
                cardDrawString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + iDrawAmount + "|[f: " + descFontSize + " m][c: Black]";
            }
            else //greater
            {
                cardDrawString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + iDrawAmount + "|[f: " + descFontSize + " m][c: Black]";
            }
            cardDrawString += " card";
            if (iDrawAmount > 1)
            {
                cardDrawString += "s";
            }
            cardDrawString += ".";

            desc.Add(CardStylizing.basicDamageString(ATTACK_DAMAGE, damage, descFontSize));
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]Choose up to " + DISCARD_CHOICE_AMOUNT + " cards");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]to discard from your");
            desc.Add("[f: " + descFontSize + " m]" + "[c: Black]draw pile and then");
            desc.Add(cardDrawString);

            return desc;
        }

        public override Card getNewCard()
        {
            return new QuakeStomp();
        }

        public override Card getEmpoweredCard()
        {
            return new EmpoweredCards.RareCards.QuakeStomp_Empowered();
        }
    }
}
