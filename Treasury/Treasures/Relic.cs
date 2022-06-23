using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Treasury.Treasures
{
    public abstract class Relic : Treasure
    {
        String _name;
        List<String> _description;
        protected bool _active;
        bool _persists; //whether or not the curse or blessing disappears if your champion dies and you switch to a party member

        //FIXIT add booleans for different methods of obtaining

        public Relic(String name, List<String> description, bool persists, treasureType type) : base(type)
        {
            _treasureText = name;

            _name = name;
            _description = description;
            _persists = persists;

            _active = true;
        }

        public String getName()
        {
            return _name;
        }

        public List<String> getDescription()
        {
            return _description;
        }

        public bool persists()
        {
            return _persists;
        }

        public bool getActive()
        {
            return _active;
        }



        //By default, each of these does nothing. The children will override these based on their effects
        public virtual void onCombatStart() { }
        public virtual void onTurnStart() { }
        public virtual void onChampionAttack() { }
        public virtual void onChampionUsedSkill() { }
        public virtual void onTurnEnd() { }
    }
}
