using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    /// <summary>
    /// The class that contains all of the details of an event, containing both the
    /// event writing as well as the choices the player can make when presented with
    /// this event.
    /// </summary>
    public class Happening
    {
        /* Possible event results
         * 
         * (possible alterations:
         *    - Random chance at positive or negative
         *    - StS also has event minigame results (match cards, roulette, betting on odds, etc)
         *    - Repeatable choices (with a negative effect, sometimes scaling)
         *    - Not being fully clear what the result is
         *    - Requires a specific relic or card
         * 
         * Positive:
         *    - Add a card to your deck (random/specific/specific rarity/otherwise unobtainable)
         *    - Remove a card from your deck (specific/all of a kind/disallow rarities)
         *    - Heal (# health/to full)
         *    - Gain a blessing
         *    - Duplicate a card in your deck
         *    - Gain a specific inventory item (specific or random of type?)
         *    - Remove a curse
         *    - Gain gold
         *    
         *    - Upgrade a card (specific/random) (FIXIT figure out card upgrading system)
         * 
         * Neutral:
         *    - Do nothing
         *    - Transform a card in your deck
         *    - Combat (encounter/miniboss/boss, random/specific, random/specific combat rewards)
         *    - (StS has immediately travel to the boss)
         * 
         * Negative:
         *    - Take damage (#, %, random range/specific)
         *    - Gain a curse
         *    - Add a void card to your deck (random/specific)
         *    - Remove a blessing
         *    - Lose gold
         */

        List<String> _writing;
        protected List<Choice> _choices;

        public Happening(List<String> writing)
        {
            _writing = writing;
        }

        //Setters
        public void setChoices(List<Choice> choices)
        {
            _choices = choices;
        }



        //Getters
        public List<Choice> getChoices()
        {
            return _choices;
        }

        public List<String> getWriting()
        {
            return _writing;
        }
    }
}
