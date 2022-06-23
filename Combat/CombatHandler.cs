using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    public class CombatHandler
    {
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

        UserInterface.CombatUI _combatUI;
        Characters.Champion _champ;
        List<PartyMember> _partyMembers;
        Encounter _currentEncounter;
        combatTurn _turn;
        Dungeon.Room _currentRoom;
        bool lootHandled;
        Enemy _lastAttackedEnemy;

        public CombatHandler()
        {
            _combatUI = new UserInterface.CombatUI(this);

            _turn = combatTurn.ROUND_START;
            _champ = Game1.getChamp();

            _partyMembers = new List<PartyMember>();
            List<Characters.Hero> heroes = Game1.getPartyMembers();
            if (heroes != null)
            {
                for (int i = 0; i < heroes.Count; i++)
                {
                    _partyMembers.Add(new PartyMember(heroes[i]));
                }
            }
        }

        //Setters
        public void setNewChamp(Characters.Champion champ)
        {
            _champ = champ;
        }
        public void setNewPartyMembers(List<Characters.Hero> heroes)
        {
            _partyMembers.Clear();
            if (heroes != null)
            {
                for (int i = 0; i < heroes.Count; i++)
                {
                    _partyMembers.Add(new PartyMember(heroes[i]));
                }
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
        public void setCurrentRoom(Dungeon.Room currentRoom)
        {
            _currentRoom = currentRoom;
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
        public combatTurn getTurn()
        {
            return _turn;
        }
        public UserInterface.CombatUI getCombatUI()
        {
            return _combatUI;
        }
        public Enemy getLastAttackedEnemy()
        {
            return _lastAttackedEnemy;
        }
        public Enemy getRandomEnemy()
        {
            List<Enemy> targets = new List<Enemy>();

            for (int i = 0; i < _currentEncounter.getEnemies().Count; i++)
            {
                if (!_currentEncounter.getEnemies()[i].getDowned())
                {
                    targets.Add(_currentEncounter.getEnemies()[i]);
                }
            }

            if (targets.Count == 0)
            {
                return null; //should not happen
            }

            return targets[Game1.randint(0, targets.Count - 1)];
        }

        //Setters
        public void setLastAttackedEnemy(Enemy target)
        {
            _lastAttackedEnemy = target;
        }



        /// <summary>
        /// Make sure setNewEncounter is called first so that the encounter's enemies are properly set up
        /// </summary>
        public void combatStart(List<UserInterface.UserInterface> activeUI)
        {
            Game1.debugLog.Add("Beginning combat");

            _combatUI.setAsActiveUI(activeUI);
            _lastAttackedEnemy = null;

            _turn = combatTurn.ROUND_START;

            _champ = Game1.getChamp();
            _champ.resetDivinity();
            _champ.resetDefense();
            _champ.resetBuffs();
            _champ.getDeck().start();

            foreach (PartyMember party in _partyMembers)
            {
                party.resetDefense();
                party.resetStrength();
                party.resetDexterity();
                party.resetResilience();
                party.resetBuffs();
            }

            lootHandled = false;

            
            //Relics that affect the start of combat, including party member buffs
            for (int i = 0; i < Game1.getDungeonHandler().getRelics().Count; i++)
            {
                Game1.getDungeonHandler().getRelics()[i].onCombatStart();
            }
            for (int i = 0; i < _partyMembers.Count; i++)
            {
                _partyMembers[i].getPartyMemberBuff().onCombatStart();
            }
        }

        /// <summary>
        /// Main game loop method for handling combat. Whether or not combat is over gets handled here (except for menu quitting, etc).
        /// </summary>
        public void handleCombat()
        {
            //Check if the combat needs to end
            if (_champ.getDowned())
            {
                //FIXIT include party member swapping mechanics when they're implemented instead of immediately ending combat here
                endCombat(false);
            }
            else if (_currentEncounter.areAllEnemiesDefeated())
            {
                endCombat();
            }
            
            //Perform turn logic
            switch (_turn)
            {
                case combatTurn.ROUND_START:
                    _champ.getDeck().drawNumCards(_champ.getCardDraw(true)); //Draw new cards at turn start, and set the champion's card draw back to normal
                    nextTurn();
                    break;
                case combatTurn.CHAMPION:
                    //Nothing much needs to be called here, since it's all handled by the UI. Even the End Turn button that switches to the next turn is handled by the UI
                    break;
                case combatTurn.PARTY:
                    nextTurn(); //FIXIT when party members are added, do their logic. remember to ADD PARTY CONTROL BUTTONS/COMMANDS for the player
                    break;
                case combatTurn.ENEMIES:
                    handleEnemyTurn();
                    nextTurn(); //FIXIT this will disappear when things get done one by one visually (animations, etc)
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
        }

        /// <summary>
        /// Handles what happens when handleCombat wants to begin the next turn. Only things that happen in an instant should be
        /// handled here; anything with an animation or something that needs to happen gradually should be handled in handleCombat
        /// or any other function it will call more than once per turn transition.
        /// </summary>
        public void nextTurn()
        {
            switch (_turn)
            {
                case combatTurn.ROUND_START:
                    _turn = combatTurn.CHAMPION;

                    //Relics that affect the start of your turn, including party member buffs
                    for (int i = 0; i < Game1.getDungeonHandler().getRelics().Count; i++)
                    {
                        Game1.getDungeonHandler().getRelics()[i].onTurnStart();
                    }
                    for (int i = 0; i < _partyMembers.Count; i++)
                    {
                        _partyMembers[i].getPartyMemberBuff().onTurnStart();
                    }


                    _champ.newTurnLowerBuffs();
                    for (int i = 0; i < _currentEncounter.getEnemies().Count; i++)
                    {
                        if (!_currentEncounter.getEnemies()[i].getDowned())
                        {
                            _currentEncounter.getEnemies()[i].newTurnLowerBuffs();
                        }
                    }
                    if (_partyMembers != null)
                    {
                        foreach (Unit party in _partyMembers)
                        {
                            party.newTurnLowerBuffs();
                        }
                    }
                    _champ.resetDivinity();
                    _champ.resetDefense();
                    _currentEncounter.determineIntents(_champ, _partyMembers);
                    updateCombatUI();
                    break;
                case combatTurn.CHAMPION:
                    _turn = combatTurn.PARTY;

                    //Relics that affect the end of your turn, including party member buffs
                    for (int i = 0; i < Game1.getDungeonHandler().getRelics().Count; i++)
                    {
                        Game1.getDungeonHandler().getRelics()[i].onTurnEnd();
                    }
                    for (int i = 0; i < _partyMembers.Count; i++)
                    {
                        _partyMembers[i].getPartyMemberBuff().onTurnEnd();
                    }


                    if (_partyMembers != null)
                    {
                        foreach (Unit party in _partyMembers)
                        {
                            party.resetDefense();
                        }
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
        /// Main game loop for handling the enemies' turn during combat. Checks _currentEncounter's enemies & performs logic based on their AI
        /// </summary>
        public void handleEnemyTurn()
        {
            //this implementation will change when animations are in: will need a currentEnemyIndex variable, etc
            for (int i = 0; i < _currentEncounter.getEnemies().Count; i++)
            {
                if (!_currentEncounter.getEnemies()[i].getDowned())
                {
                    _currentEncounter.getEnemies()[i].getAIPattern().doTurnAction(_champ, _partyMembers);
                }
            }
        }

        public void endCombat(bool enemiesDefeated = true)
        {
            _champ.resetBuffs();
            if (enemiesDefeated)
            {
                if (!lootHandled)
                {
                    Game1.debugLog.Add("Enemies defeated; generating loot.");

                    //Set stats in a way that they don't affect card numbers
                    _champ.tempSetStrengthTo0();
                    _champ.tempSetDexterityTo0();
                    _champ.tempSetResilienceTo0();
                    Game1.addToMenus(new UserInterface.Menus.LootMenu(_currentEncounter.getLoot(), UserInterface.Menus.LootMenu.COMBAT_LOOT));
                    lootHandled = true;
                }
                else
                {
                    if (Game1.getDungeonHandler().isRandomEncounter())
                    {
                        Game1.getDungeonHandler().randomEncounterComplete();
                    }
                    else
                    {
                        _currentRoom.finishTopContent();
                    }
                    Game1.returnToDungeon();
                }

            }
            else //the champion has died and no party members can replace them
            {

            }
        }

        public void updateCombatUI()
        {
            _combatUI.updateCombatUI(this);
        }

        public void updateEnemyIntents()
        {
            _combatUI.updateEnemyIntents(this);
        }
    }
}