using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    /// <summary>
    /// Variant of event Choice that can be gated behind a required item, relic, or amount of
    /// gold. If the player doesn't meet the requirements, they will be able to see but not
    /// select it as an option.
    /// </summary>
    public class MissableChoice : Choice
    {
        Treasury.Equipment.Item _requiredItem;
        Treasury.Treasures.Relic _requiredRelic;
        int _requiredGold;

        /// <summary>
        /// Currently only designed to handle one of the three options (required item, relic,
        /// or amount of gold). Don't set more than one!
        /// </summary>
        public MissableChoice(String choiceText) : base(choiceText)
        {
            _requiredGold = -1;
        }

        //Getters
        public Treasury.Equipment.Item getRequiredItem()
        {
            return _requiredItem;
        }
        public Treasury.Treasures.Relic getRequiredRelic()
        {
            return _requiredRelic;
        }
        public int getRequiredGold()
        {
            return _requiredGold;
        }

        //Setters
        public void setRequiredItem(Treasury.Equipment.Item requiredItem)
        {
            _requiredItem = requiredItem;
        }
        public void setRequiredRelic(Treasury.Treasures.Relic requiredRelic)
        {
            _requiredRelic = requiredRelic;
        }
        public void setRequiredGold(int requiredGold)
        {
            _requiredGold = requiredGold;
        }



        /// <summary>
        /// Currently only designed to handle one of the three options (required item, relic,
        /// or amount of gold). Don't set more than one!
        /// </summary>
        public bool canChoose()
        {
            if (_requiredItem != null)
            {
                List<Treasury.Equipment.Item> inventoryItems = Game1.getInventory().getItems();

                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (_requiredItem.GetType() == inventoryItems[i].GetType())
                    {
                        return true;
                    }
                }
            }
            else if (_requiredRelic != null)
            {
                List<Treasury.Treasures.Relic> relics = Game1.getDungeonHandler().getRelics();

                for (int i = 0; i < relics.Count; i++)
                {
                    if (_requiredItem.GetType() == relics[i].GetType())
                    {
                        return true;
                    }
                }
            }
            else if (_requiredGold > 0 && Game1.getDungeonHandler().getGold() >= _requiredGold)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called upon selecting this choice; it will make sure whatever the required item or
        /// relic or amount of money was is used up.
        /// </summary>
        public void consumeRequirement()
        {
            if (_requiredItem != null)
            {
                List<Treasury.Equipment.Item> inventoryItems = Game1.getInventory().getItems();

                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (_requiredItem.GetType() == inventoryItems[i].GetType())
                    {
                        Game1.getInventory().removeFromInventory(_requiredItem);
                        return;
                    }
                }
            }
            else if (_requiredRelic != null)
            {
                List<Treasury.Treasures.Relic> relics = Game1.getDungeonHandler().getRelics();

                for (int i = 0; i < relics.Count; i++)
                {
                    if (_requiredItem.GetType() == relics[i].GetType())
                    {
                        Game1.getDungeonHandler().removeRelic(_requiredRelic);
                        return;
                    }
                }
            }
            else if (_requiredGold > 0 && Game1.getDungeonHandler().getGold() >= _requiredGold)
            {
                Game1.getDungeonHandler().addGold(-_requiredGold);
                return;
            }
        }
    }
}
