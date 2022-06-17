using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    public class Choice
    {
        String _choiceText;
        Happening _resultingEvent;
        Combat.Encounter _resultingEncounter;
        Treasury.Loot _resultingLoot;
        Action _onChoose;

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
            _resultingLoot = null;
        }
        public void setResult(Combat.Encounter result)
        {
            _resultingEvent = null;
            _resultingEncounter = result;
            _resultingLoot = null;
        }
        public void setResult(Treasury.Loot result)
        {
            _resultingEvent = null;
            _resultingEncounter = null;
            _resultingLoot = result;
        }

        public void setOnChoose(Action onChoose)
        {
            _onChoose = onChoose;
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



        public void onChoose()
        {
            _onChoose();
        }
    }
}
