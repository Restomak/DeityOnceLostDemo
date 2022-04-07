using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    class CombatHandler
    {
        public const int MAX_PARTY_MEMBERS = 3; //will get moved into RunHandler when I eventually make that, since this is just combat
        public void addToParty(PartyMember newMember)
        {
            if (_partyMembers == null)
            {
                _partyMembers = new List<PartyMember>();
            }

            if (_partyMembers.Count < 3)
            {
                _partyMembers.Add(newMember);
            }
            else
            {
                String log = "Tried to add a fourth party member " + newMember.getName() + " when three already exist: ";
                foreach (PartyMember member in _partyMembers)
                {
                    log += member.getName() + " ";
                }
                Game1.errorLog.Add(log);
            }
        }  //will get moved into RunHandler when I eventually make that, since this is just combat

        public enum combatTurn
        {
            CHAMPION,
            PARTY,
            ENEMIES,
            KARMA,
            VOID
        }

        Characters.Champion _champ;
        List<PartyMember> _partyMembers;
        Encounter _currentEncounter;
        combatTurn _turn;

        public CombatHandler(Characters.Champion champ, List<PartyMember> party)
        {
            _turn = combatTurn.CHAMPION;
            _champ = champ;
            if (party == null || party.Count <= 3)
            {
                _partyMembers = party;
            }
            else
            {
                Game1.errorLog.Add("Tried to create a party of more than three heroes (size: " + party.Count + "). Adding only the first three");
                _partyMembers = new List<PartyMember>();
                _partyMembers.Add(party[0]);
                _partyMembers.Add(party[1]);
                _partyMembers.Add(party[2]);
            }
        }

        //Setters
        public void setNewChamp(Characters.Champion champ)
        {
            _champ = champ;
        }
        public void setNewPartyMembers(List<PartyMember> party)
        {
            if (party == null || party.Count <= 3)
            {
                _partyMembers = party;
            }
            else
            {
                Game1.errorLog.Add("Tried to create a party of more than three heroes (size: " + party.Count + "). Adding only the first three");
                _partyMembers = new List<PartyMember>();
                _partyMembers.Add(party[0]);
                _partyMembers.Add(party[1]);
                _partyMembers.Add(party[2]);
            }
        }
        public void setNewEncounter(Encounter newEncounter)
        {
            _currentEncounter = newEncounter;
        }
        public void setTurn(combatTurn turn)
        {
            _turn = turn;
        }

        //Getters
        public Characters.Champion getChamp()
        {
            return _champ;
        }
        public List<PartyMember> getParty()
        {
            return _partyMembers;
        }
        public Encounter getCurrentEncounter()
        {
            return _currentEncounter;
        }




        public void nextTurn()
        {
            switch (_turn)
            {
                case combatTurn.CHAMPION:
                    _turn = combatTurn.PARTY;
                    break;
                case combatTurn.PARTY:
                    _turn = combatTurn.ENEMIES;
                    break;
                case combatTurn.ENEMIES:
                    _turn = combatTurn.KARMA;
                    break;
                case combatTurn.KARMA:
                    _turn = combatTurn.VOID;
                    break;
                case combatTurn.VOID:
                    _turn = combatTurn.CHAMPION;
                    break;
            }
        }

        /// <summary>
        /// Main game loop method for handling combat. Whether or not combat is over gets handled here (except for menu quitting, etc).
        /// </summary>
        public void handleCombat()
        {
            switch (_turn)
            {
                case combatTurn.CHAMPION:
                    handleChampionTurn();
                    break;
                case combatTurn.PARTY:
                    nextTurn(); //FIXIT when party members are added, do their logic. remember to ADD PARTY CONTROL BUTTONS/COMMANDS for the player
                    break;
                case combatTurn.ENEMIES:
                    //FIXIT go through _currentEncounter & its enemies to handle AI & enemy actions
                    break;
                case combatTurn.KARMA:
                    nextTurn(); //don't have anything for karma turns yet, so skip
                    break;
                case combatTurn.VOID:
                    nextTurn(); //don't have anything for void turns yet, so skip
                    break;
            }

            //FIXIT handle whether or not combat has ended
        }

        /// <summary>
        /// Main game loop method for handling champion's turn during combat. Checks in with the UIHandler until the End Turn button is
        /// pressed or something else forces the champion's turn to end.
        /// </summary>
        public void handleChampionTurn()
        {
            //FIXIT do the above, also make UIHandler class in Input folder
        }

        /// <summary>
        /// Main game loop for handling the enemies' turn during combat. Checks _currentEncounter's enemies & performs logic based on their AI
        /// </summary>
        public void handleEnemyTurn()
        {
            //FIXIT do the above
        }
    }
}