using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    /// <summary>
    /// Every event in the game has at least one Choice, which will result in a
    /// change in the gameplay in some way. In many cases, the choice will simply
    /// be a continuation of the game, but in many other cases, the result will be
    /// another event, a combat encounter, loot, or a direct function passed to
    /// the choice as an Action that is called upon selecting the choice.
    /// </summary>
    public class Choice
    {
        protected String _choiceText;
        protected Happening _resultingEvent;
        protected Combat.Encounter _resultingEncounter;
        protected Treasury.Loot _resultingLoot;
        protected Action _onChoose;
        protected List<UserInterface.ExtraInfo> _extraInfo;

        public Choice(String choiceText)
        {
            _choiceText = choiceText;
            _onChoose = () =>
            {
                //Do nothing by default: "Continue"
            };
        }

        //Setters
        public void setResult(Happening result)
        {
            _resultingEvent = result;
            _resultingEncounter = null;
        }
        public void setResult(Combat.Encounter result)
        {
            _resultingEvent = null;
            _resultingEncounter = result;
        }
        public void setResult(Treasury.Loot result)
        {
            _resultingLoot = result;
        }

        public void setOnChoose(Action onChoose)
        {
            _onChoose = onChoose;
        }

        public void setExtraInfo(List<UserInterface.ExtraInfo> extraInfo)
        {
            _extraInfo = extraInfo;
        }



        //Getters
        public Happening getResultingEvent()
        {
            return _resultingEvent;
        }
        public Combat.Encounter getResultingEncounter()
        {
            return _resultingEncounter;
        }
        public Treasury.Loot getResultingLoot()
        {
            return _resultingLoot;
        }

        public String getText()
        {
            return _choiceText;
        }
        public List<UserInterface.ExtraInfo> getExtraInfo()
        {
            return _extraInfo;
        }



        /// <summary>
        /// Always called when the choice is selected, though by default _onChoose
        /// does nothing.
        /// </summary>
        public void onChoose()
        {
            _onChoose();
        }
    }
}
