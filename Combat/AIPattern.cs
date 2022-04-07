using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    abstract class AIPattern
    {
        public enum intent
        {
            ATTACK,
            DEFEND,
            BUFF,
            DEBUFF,
            AOE, //attacking the party members as well
            OMINOUS, //void or karma related bad shit
            REINFORCEMENTS,
            OTHER,
            HIDDEN
        }

        List<intent> _intentsForThisTurn;

        public AIPattern()
        {
            _intentsForThisTurn = new List<intent>();
        }
        
        public List<intent> getIntents()
        {
            return _intentsForThisTurn;
        }

        /// <summary>
        /// Call at the start of the round to update the enemy's intent so that it's shown to the player
        /// and they can figure out what they want to do for the turn.
        /// </summary>
        public abstract List<intent> determineIntents(Characters.Champion champ, List<PartyMember> party);

        /// <summary>
        /// Call when it's this enemy's turn to actually perform the action shown by the intent
        /// </summary>
        public abstract void doTurnAction(Characters.Champion champ, List<PartyMember> party);

        /// <summary>
        /// Use if the enemy receiving damage would change their intents for the turn
        /// </summary>
        public virtual List<intent> onDamaged()
        {
            return _intentsForThisTurn;
        }

        /// <summary>
        /// Use when another enemy influences this enemy's intents. Must override this function or it will
        /// change nothing; by default enemies cannot have their intents influenced. Use for minions, etc.
        /// </summary>
        public virtual void forceIntentsChange(List<intent> forcedIntents, Enemy influencer = null) { }
    }
}
