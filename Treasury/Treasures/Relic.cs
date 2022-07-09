using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Treasures
{
    /// <summary>
    /// Abstract base class for all blessings and curses, as well as party buffs and
    /// including rest blessings. Relics in general can have an extremely wide variety
    /// of purposes, and so they have many abstract functions that are called all
    /// throughout combat and other times. By default, each function does nothing, and
    /// relics will individually override those that are important to their purpose.
    /// </summary>
    public abstract class Relic : Treasure
    {
        Texture2D _texture;
        String _name;
        protected List<String> _description;
        protected bool _active;
        bool _persists; //whether or not the curse or blessing disappears if your champion dies and you switch to a party member

        //FIXIT add booleans for different methods of obtaining

        public Relic(Texture2D texture, String name, List<String> description, bool persists, treasureType type) : base(type)
        {
            _treasureText = name;

            _texture = texture;
            _name = name;
            _description = description;
            _persists = persists;

            _active = true;
        }

        public String getName()
        {
            return _name;
        }

        public Texture2D getTexture()
        {
            return _texture;
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



        public static Texture2D getRelicIcon(Treasure relic)
        {
            return ((Relic)relic)._texture;
        }

        public abstract Color getBorderColor();
        
        public abstract List<UserInterface.ExtraInfo> getHoverInfo();
        public abstract List<UserInterface.ExtraInfo> getHoverExtraInfo();



        //By default, each of these does nothing. The children will override these based on their effects
        public virtual void onPickedUp() { }
        public virtual void onCombatStart() { }
        public virtual void onCombatStartAfterFirstDraw() { }
        public virtual void onTurnStart() { }
        public virtual void onTurnStartAfterDraw() { }
        public virtual void onChampionAttack() { }
        public virtual void onChampionSkill() { }
        public virtual void onChampionAttacked() { }
        public virtual void onChampionUsedSkill() { }
        public virtual void onTurnEndDealsDamage() { }
        public virtual void onTurnEndBeforeDiscard() { }
        public virtual void onTurnEnd() { }
        public virtual void onCombatEnd() { }
    }
}
