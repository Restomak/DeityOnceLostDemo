using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    public abstract class AIPattern
    {
        public enum intent
        {
            ATTACK,
            DEFEND,
            BUFF,
            DEBUFF,
            MULTIATTACK, //attacking the champion with several hits
            AOE, //attacking the party members as well
            OMINOUS, //void or karma related bad shit
            REINFORCEMENTS,
            OTHER,
            HIDDEN
        }

        protected List<intent> _intentsForThisTurn;
        protected Enemy _enemy;
        protected int _numHits;

        /// <summary>
        /// Every Enemy has one AIPattern, while each AIPattern can be associated with more than one enemy.
        /// 
        /// What AIPatterns are responsible for:
        /// • determining intents each round
        /// • determining the types of attacks that occur when attacks do occur
        /// • determining the types of buffs or debuffs that occur
        /// 
        /// What Enemies are responsible for:
        /// • supplying the raw stats (texture, name, hp, etc)
        /// • supplying damage & defense amounts
        /// </summary>
        public AIPattern()
        {
            _intentsForThisTurn = new List<intent>();
        }
        
        public List<intent> getIntents()
        {
            return _intentsForThisTurn;
        }

        /// <summary>
        /// Must be called in an Enemy's constructor to pass itself to the AIPattern it uses
        /// </summary>
        public abstract void setEnemy(Enemy enemy);

        /// <summary>
        /// Used by the engine to show the user how much damage is being dealt this turn
        /// </summary>
        public abstract int getDamage();

        /// <summary>
        /// Used by the engine to show the user how many attacks the enemy will be doing this turn
        /// </summary>
        public int getNumHits()
        {
            return _numHits;
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
