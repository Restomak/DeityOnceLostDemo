using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// Abstract base class for all cards in the game.
    /// </summary>
    public abstract class Card
    {
        /* Card ideas:
         * 
         * putting an enemy into statis; it doesn't perform a turn action this turn, or maybe for two turns unless it gets damaged
         * ritual dagger of some kind, the next card you play this turn costs x blood (you generally want to play it last in the turn)
         */

        private String _name;
        private Dictionary<CardEnums.CostType, int> _playCost, _defaultCost, _baseCost;
        private CardEnums.CardType _cardType;
        private CardEnums.CardRarity _rarity;
        public List<String> _description;
        protected bool _preserveAlways, _preserve; //if kept in hand at the end of the turn
        protected bool _dissipates; //if it's removed from the deck for the combat once played
        protected CardEnums.TargetingType _targetType;
        protected Combat.Unit _target;
        protected bool _empowered, _bloodstained, _voidtouched;
        protected Texture2D _cardArt;

        public Card(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, CardEnums.TargetingType targetType, bool dissipates = false, bool preserveAlways = false)
        {
            _cardArt = Game1.pic_card_art_blank;
            _name = name;
            _cardType = cardType;
            _rarity = rarity;
            _playCost = new Dictionary<CardEnums.CostType, int>();
            _defaultCost = new Dictionary<CardEnums.CostType, int>();
            _baseCost = new Dictionary<CardEnums.CostType, int>();
            _description = new List<string>();
            _description.Add("???"); //default description if none is set
            _preserveAlways = preserveAlways;
            _preserve = preserveAlways;
            _dissipates = dissipates;
            _targetType = targetType;
            _empowered = false;
            _bloodstained = false;
            _voidtouched = false;

            //iterate through every cost type and make the costs for each default 0
            foreach (CardEnums.CostType costType in (CardEnums.CostType[])Enum.GetValues(typeof(CardEnums.CostType)))
            {
                _playCost[costType] = 0;
                _defaultCost[costType] = 0;
                _baseCost[costType] = 0;
            }
        }

        /// <summary>
        /// Used to check if the card is playable (eg. do you have enough divinity? or is it meant to be an unplayable card? etc)
        /// </summary>
        public bool canPlay()
        {
            return (Game1.getChamp().getDivinity() >= getPlayCost(CardEnums.CostType.DIVINITY) && //Make sure there's enough Divinity to play the card
                Game1.getChamp().getCurrentHP() >= getPlayCost(CardEnums.CostType.BLOOD)); //Make sure there's enough HP to play the card
        }

        //Setters
        public void addPlayCost(CardEnums.CostType costType, int costAmount)
        {
            _playCost[costType] = costAmount;
            _defaultCost[costType] = costAmount;
            _baseCost[costType] = costAmount;
        }
        public void changeCost(CardEnums.CostType costType, int newCostAmount)
        {
            _playCost[costType] = newCostAmount;
        }
        public void resetCost(CardEnums.CostType costType)
        {
            _playCost[costType] = _defaultCost[costType];
        }
        public void resetCosts()
        {
            foreach (CardEnums.CostType costType in (CardEnums.CostType[])Enum.GetValues(typeof(CardEnums.CostType)))
            {
                _playCost[costType] = _defaultCost[costType];
            }
        }
        public void addBaseCost(CardEnums.CostType costType, int costAmount)
        {
            _baseCost[costType] = costAmount;
        }
        public void setTarget(Combat.Unit target)
        {
            _target = target;
        }
        public void setRetainAlways(bool preserveAlways)
        {
            _preserveAlways = preserveAlways;
        }
        public void setRetain(bool preserve)
        {
            _preserve = preserve;
        }
        public void resetRetain()
        {
            _preserve = _preserveAlways;
        }
        public void setDissipates(bool dissipates)
        {
            _dissipates = dissipates;
        }


        //Override these functions if used
        public virtual void onDraw() { }
        public virtual void onPlay() { }
        public virtual void onDiscard() { }
        public virtual void onOtherCardPlayed() { }
        public virtual void onTurnEnd() { }
        public virtual void onRemove() { }
        public virtual void onCombatStart() { }
        public virtual void onCombatEnd() { }
        public virtual void onKeep() { }



        //Getters
        public virtual String getName(int nameFontSize)
        {
            return "[f: " + nameFontSize + " b][c: Black]" + _name;
        }
        /// <summary>
        /// For use in debug and error logs and measuring the name string.
        /// </summary>
        public String getName()
        {
            return _name;
        }
        public int getPlayCost(CardEnums.CostType costType)
        {
            return _playCost[costType];
        }
        public int getDefaultCost(CardEnums.CostType costType)
        {
            return _defaultCost[costType];
        }
        public int getBaseCost(CardEnums.CostType costType)
        {
            return _baseCost[costType];
        }
        public CardEnums.CardType getCardType()
        {
            return _cardType;
        }
        public CardEnums.CardRarity getCardRarity()
        {
            return _rarity;
        }
        public CardEnums.TargetingType getTargetType()
        {
            return _targetType;
        }
        public Combat.Unit getTarget()
        {
            return _target;
        }
        public bool getRetainAlways()
        {
            return _preserveAlways;
        }
        public bool getRetain()
        {
            return _preserve;
        }
        public bool getDissipates()
        {
            return _dissipates;
        }
        public bool isEmpowered()
        {
            return _empowered;
        }
        public bool isBloodstained()
        {
            return _bloodstained;
        }
        public bool isVoidtouched()
        {
            return _voidtouched;
        }
        public Texture2D getCardArt()
        {
            return _cardArt;
        }
        public abstract List<String> getDescription(Characters.Champion champ, bool activeCard = false); //always override
        public abstract List<String> getStylizedDescription(Characters.Champion champ, int descFontSize, bool activeCard = false); //always override
        public abstract Card getNewCard();
        public abstract Card getEmpoweredCard();
        public abstract List<UserInterface.ExtraInfo> getHoverInfo();



        public static Card getCopyOfCard(Card cardToCopy)
        {
            Card newCard = cardToCopy.getNewCard();

            //Make sure all the costs are the same
            foreach (CardEnums.CostType costType in (CardEnums.CostType[])Enum.GetValues(typeof(CardEnums.CostType)))
            {
                newCard._playCost[costType] = cardToCopy._playCost[costType];
            }

            //Make sure if dissipates or preserveAlways had changed, that's copied over too
            newCard._preserveAlways = cardToCopy._preserveAlways;
            newCard._dissipates = cardToCopy._dissipates;

            return newCard;
        }
        
        public static Combat.Unit getTargetForDescription(CardEnums.TargetingType targetType)
        {
            Combat.Unit target = null;

            if (targetType == CardEnums.TargetingType.enemies && Game1.getHoveredClickable() != null &&
                Game1.getHoveredClickable().GetType() == typeof(UserInterface.Clickables.Target))
            {
                target = ((UserInterface.Clickables.Opponent)((UserInterface.Clickables.Target)Game1.getHoveredClickable()).getUnitClickable()).getEnemy();
            }

            return target;
        }



        public static UserInterface.ExtraInfos.TextInfo getPreserveExtraInfo()
        {
            return new UserInterface.ExtraInfos.TextInfo(new List<String>()
            { "Preserve:",
              "At the end of your turn,",
              "do not discard this card",
              "from your hand."
            });
        }

        public static UserInterface.ExtraInfos.TextInfo getDissipateExtraInfo()
        {
            return new UserInterface.ExtraInfos.TextInfo(new List<String>()
            { "Dissipates:",
              "When played, this card",
              "is removed from play",
              "for the combat."
            });
        }

        public static UserInterface.ExtraInfos.TextInfo getBloodExtraInfo()
        {
            return new UserInterface.ExtraInfos.TextInfo(new List<String>()
            { "Blood Cost:",
              "This card costs",
              "HP to play."
            });
        }

        public static UserInterface.ExtraInfos.TextInfo getDivinityExtraInfo()
        {
            return new UserInterface.ExtraInfos.TextInfo(new List<String>()
            { "Divinity:",
              "Most cards cost",
              "Divinity to play."
            });
        }

        public static UserInterface.ExtraInfos.TextInfo getDefenseExtraInfo()
        {
            return new UserInterface.ExtraInfos.TextInfo(new List<String>()
            { "Defense:",
              "Reduces incoming damage."
            });
        }

        public static UserInterface.ExtraInfos.TextInfo getEmpowerExtraInfo()
        {
            return new UserInterface.ExtraInfos.TextInfo(new List<String>()
            { "Empowered cards are",
              "stronger than their.",
              "regular counterparts."
            });
        }
    }
}
