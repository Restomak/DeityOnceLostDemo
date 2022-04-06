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
        private bool _retainAlways;

        public Card(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity)
        {
            _name = name;
            _cardType = cardType;
            _rarity = rarity;
            _playCost = new Dictionary<CardEnums.CostType, int>();
            _specialCost = new Dictionary<CardEnums.CostType, int>();
            _description = new List<string>();
            _description.Add("???"); //default description if none is set
            _retainAlways = false;

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
            //FIXIT check champion stats vs card cost
            return true;
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



        //Override these functions if used
        public virtual bool onDraw() { return true; }
        public virtual bool onPlay() { return true; }
        public virtual bool onDiscard() { return true; }
        public virtual bool onOtherCardPlayed() { return true; }
        public virtual bool onTurnEnd() { return true; }
        public virtual bool onRemove() { return true; }
        public virtual bool onCombatStart() { return true; }
        public virtual bool onCombatEnd() { return true; }
        public virtual bool onKeep() { return true; }



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
        public bool getRetainAlways()
        {
            return _retainAlways;
        }
        public abstract List<String> getDescription(Characters.Champion champ); //always override

        //Setters
        public void setDescription(List<String> description)
        {
            _description = description;
        }
        public void setRetainAlways(bool retain)
        {
            _retainAlways = retain;
        }
    }
}
