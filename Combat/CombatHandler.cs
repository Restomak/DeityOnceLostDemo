using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    class CombatHandler
    {
        public const int MAX_PARTY_MEMBERS = 3;

        Characters.Champion _champ;
        List<Characters.Hero> _partyMembers;
        Encounter _currentEncounter;

        public CombatHandler(Characters.Champion champ, List<Characters.Hero> party)
        {
            _champ = champ;
            _partyMembers = party;
        }
    }
}