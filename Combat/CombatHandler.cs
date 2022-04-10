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
            ROUND_START,
            CHAMPION,
            PARTY,
            ENEMIES,
            KARMA,
            VOID,
            ROUND_END
        }

        Characters.Champion _champ;
        List<PartyMember> _partyMembers;
        Encounter _currentEncounter;
        combatTurn _turn;

        public CombatHandler(Characters.Champion champ, List<PartyMember> party)
        {
            _turn = combatTurn.ROUND_START;
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


        
        /// <summary>
        /// Make sure setNewEncounter is called first so that the encounter's enemies are properly set up
        /// </summary>
        public void combatStart()
        {
            _turn = combatTurn.ROUND_START;
            _champ.resetDivinity();
            _champ.resetDefense();
            foreach (Unit party in _partyMembers)
            {
                party.resetDefense();
            }
            _currentEncounter.resetDefense();

            _champ.getDeck().start();
        }

        public void nextTurn()
        {
            switch (_turn)
            {
                case combatTurn.ROUND_START:
                    _turn = combatTurn.CHAMPION;
                    _champ.resetDefense();
                    break;
                case combatTurn.CHAMPION:
                    _turn = combatTurn.PARTY;
                    foreach (Unit party in _partyMembers)
                    {
                        party.resetDefense();
                    }
                    break;
                case combatTurn.PARTY:
                    _turn = combatTurn.ENEMIES;
                    _currentEncounter.resetDefense();
                    break;
                case combatTurn.ENEMIES:
                    _turn = combatTurn.KARMA;
                    break;
                case combatTurn.KARMA:
                    _turn = combatTurn.VOID;
                    break;
                case combatTurn.VOID:
                    _turn = combatTurn.ROUND_END;
                    break;
                case combatTurn.ROUND_END:
                    _turn = combatTurn.ROUND_START;
                    break;
            }
        }

        /// <summary>
        /// Main game loop method for handling combat. Whether or not combat is over gets handled here (except for menu quitting, etc).
        /// </summary>
        public void handleCombat()
        {
            if (_champ.getDowned())
            {
                //FIXIT include party member swapping mechanics when they're implemented
                endCombat(true);
            }
            else if (_currentEncounter.areAllEnemiesDefeated())
            {
                endCombat();
            }

            switch (_turn)
            {
                case combatTurn.ROUND_START:
                    _champ.resetDivinity();
                    _currentEncounter.determineIntents(_champ, _partyMembers);
                    nextTurn();
                    break;
                case combatTurn.CHAMPION:
                    handleChampionTurn();
                    break;
                case combatTurn.PARTY:
                    nextTurn(); //FIXIT when party members are added, do their logic. remember to ADD PARTY CONTROL BUTTONS/COMMANDS for the player
                    break;
                case combatTurn.ENEMIES:
                    handleEnemyTurn();
                    break;
                case combatTurn.KARMA:
                    nextTurn(); //don't have anything for karma turns yet, so skip
                    break;
                case combatTurn.VOID:
                    nextTurn(); //don't have anything for void turns yet, so skip
                    break;
                case combatTurn.ROUND_END:
                    //Stuff that happens at the end of a round goes here
                    nextTurn();
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
            //this implementation will change when animations are in: will need a currentEnemyIndex variable, etc
            for (int i = 0; i < _currentEncounter.getEnemies().Count; i++)
            {
                _currentEncounter.getEnemies()[0].getAIPattern().doTurnAction(_champ, _partyMembers);
            }
        }

        public void endCombat(bool runOver = false)
        {
            //FIXIT make this function do anything when runs are implemented
        }
    }
}