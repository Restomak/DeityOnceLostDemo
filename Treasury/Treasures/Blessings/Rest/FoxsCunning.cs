using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures.Blessings.Rest
{
    /// <summary>
    /// At the end of your turn, choose 1 card to Preserve. Lasts 2 combats.
    /// </summary>
    class FoxsCunning : RestBlessing
    {
        public const int DURATION = 3;

        int _remainingDuration;

        public FoxsCunning() : base(Game1.pic_relic_foxsCunning, "Fox's Cunning", new List<String>()
            { "Fox's Cunning",
              "",
              "At the end of your turn,",
              "choose 1 card to Preserve.",
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
            extraInfo.Add(DeckBuilder.Card.getPreserveExtraInfo());

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
            return new FoxsCunning();
        }



        public override void onTurnEndBeforeDiscard()
        {
            List<DeckBuilder.Card> choices = Game1.getChamp().getDeck().getHand();
            Game1.addToMenus(new UserInterface.Menus.CombatCardChoiceMenu(choices, UserInterface.Menus.CombatCardChoiceMenu.whereFrom.handToPreserve, () => { }));
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
