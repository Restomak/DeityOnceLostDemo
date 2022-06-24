using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards.RareCards
{
    class QuakeStomp : AttackAndDrawCard
    {
        public const String NAME = "Quake Stomp";
        public const CardEnums.CardType CARDTYPE = CardEnums.CardType.ATTACK;
        public const CardEnums.CardRarity RARITY = CardEnums.CardRarity.RARE;
        public const int PLAYCOST_DIV = 2;
        public const int ATTACK_DAMAGE = 8;
        public const int CARD_DRAW = 1;
        public const int DISCARD_CHOICE_AMOUNT = 3;

        public QuakeStomp() : base(NAME, CARDTYPE, RARITY, ATTACK_DAMAGE, CARD_DRAW)
        {
            addPlayCost(CardEnums.CostType.DIVINITY, PLAYCOST_DIV);
        }

        public override void onPlay()
        {
            dealDamage();
            Game1.addToMenus(new UserInterface.Menus.CombatCardChoiceMenu(Game1.getChamp().getDeck().getDrawPile(), UserInterface.Menus.CombatCardChoiceMenu.whereFrom.drawToDiscard, () =>
            {
                cardDraw();
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
            int damage = champ.getDamageAffectedByBuffs(_damage, descTarget);
            desc.Add("Deal " + damage + " damage.");

            desc.Add("Discard " + DISCARD_CHOICE_AMOUNT + " cards from");
            desc.Add("your draw pile and");
            desc.Add("then draw " + _drawAmount + " card.");

            return desc;
        }
    }
}
