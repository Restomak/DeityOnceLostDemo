using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Events
{
    public class Happening
    {
        List<String> _writing;
        List<Choice> _choices;

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
