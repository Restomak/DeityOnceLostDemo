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
        Happening _result;
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
            _result = result;
        }

        public void setOnChoose(Action onChoose)
        {
            _onChoose = onChoose;
        }


        
        //Getters
        public Happening getResult()
        {
            return _result;
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
