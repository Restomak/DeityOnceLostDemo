using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    public abstract class Card
    {
        private String _name;
        private Dictionary<CardEnums.CostType, int> _playCost, _specialCost;
        private CardEnums.CardType _cardType;
        private CardEnums.CardRarity _rarity;
        public List<String> _description;
        protected bool _retainAlways; //if kept in hand at the end of the turn
        protected bool _dissipates; //if it's removed from the deck for the combat once played
        protected CardEnums.TargetingType _targetType;
        protected Combat.Unit _target;

        public Card(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, CardEnums.TargetingType targetType, bool dissipates = false, bool retainAlways = false)
        {
            _name = name;
            _cardType = cardType;
            _rarity = rarity;
            _playCost = new Dictionary<CardEnums.CostType, int>();
            _specialCost = new Dictionary<CardEnums.CostType, int>();
            _description = new List<string>();
            _description.Add("???"); //default description if none is set
            _retainAlways = retainAlways;
            _dissipates = dissipates;
            _targetType = targetType;

            //iterate through every cost type and make the costs for each default 0
            foreach (CardEnums.CostType costType in (CardEnums.CostType[])Enum.GetValues(typeof(CardEnums.CostType)))
            {
                _playCost[costType] = 0;
                _specialCost[costType] = 0;
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
        }
        public void addSpecialCost(CardEnums.CostType costType, int costAmount)
        {
            _specialCost[costType] = costAmount;
        }
        public void setTarget(Combat.Unit target)
        {
            _target = target;
        }
        public void setRetainAlways(bool retainAlways)
        {
            _retainAlways = retainAlways;
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
        public String getName()
        {
            return _name;
        }
        public int getPlayCost(CardEnums.CostType costType)
        {
            return _playCost[costType];
        }
        public int getSpecialCost(CardEnums.CostType costType)
        {
            return _specialCost[costType];
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
            return _retainAlways;
        }
        public bool getDissipates()
        {
            return _dissipates;
        }
        public abstract List<String> getDescription(Characters.Champion champ, bool activeCard = false); //always override



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
    }
}
