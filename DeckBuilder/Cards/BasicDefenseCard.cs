using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    /// <summary>
    /// Base class to make card design easier for cards that gain defense.
    /// </summary>
    abstract class BasicDefenseCard : Card, IDefendingCard
    {
        public BasicDefenseCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int defense) : base(name, cardType, rarity, CardEnums.TargetingType.champion)
        {
            iDefense = defense;
        }

        public int iDefense { get; }

        public override void onPlay()
        {
            iGainDefense();
        }

        public void iGainDefense()
        {
            Game1.getChamp().gainDefense(iDefense);
        }

        public override List<UserInterface.ExtraInfo> getHoverInfo()
        {
            List<UserInterface.ExtraInfo> extraInfo = new List<UserInterface.ExtraInfo>();

            extraInfo.Add(getDefenseExtraInfo());

            return extraInfo;
        }
    }
}
