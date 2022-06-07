using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables.Hovers
{
    public class EnemyIntent : HoverInfo
    {
        Combat.AIPattern _enemyAI;

        public EnemyIntent(Point xy, int width, int height, Combat.AIPattern enemyAI) : base(xy, width, height, new List<string>())
        {
            _enemyAI = enemyAI;
            _description.Add("Shows what the enemy intends to do this turn.");
        }

        public override List<String> getDescription()
        {
            List<String> intents = new List<String>();
            List<Combat.AIPattern.intent> enemyIntents = _enemyAI.getIntents();

            List<String> description = new List<string>();
            for (int i = 0; i < _description.Count; i++)
            {
                description.Add(_description[i]);
            }

            for (int i = 0; i < enemyIntents.Count; i++)
            {
                if (!intents.Contains(Combat.AIPattern.getIntentToString(enemyIntents[i])))
                {
                    intents.Add(Combat.AIPattern.getIntentToString(enemyIntents[i]));
                }
            }
            
            description.Add("The enemy intends " + intents[0] + ".");
            if (intents.Count > 1)
            {
                for (int i = 1; i < intents.Count; i++)
                {
                    description.Add("The enemy also intends " + intents[i] + ".");
                }
            }

            return description;
        }
    }
}
