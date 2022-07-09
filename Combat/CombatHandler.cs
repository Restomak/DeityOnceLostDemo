using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Combat
{
    /// <summary>
    /// The game's combat handler, which manages every step of combat. Turns are broken
    /// down into tiny steps, each handling a single aspect of combat before moving on to
    /// the next. Ends when all enemies are defeated, or all friendly units are downed.
    /// </summary>
    public class CombatHandler
    {
        public enum combatTurn
        {
            COMBAT_START,
            RESETSTUFF_AND_INTENTS,
            TURN_START,
            DRAW_CARDS,
            TURN_START_AFTER_DRAW_CARDS,
            LOWER_BUFFS,
            PLAY,
            TURN_END_RESET_CARD_STUFF,
            TURN_END_DEALS_DAMAGE,
            TURN_END_BEFORE_DISCARD,
            TURN_END_DISCARD,
            TURN_END,
            RESET_ENEMY_DEFENSE,
            ENEMY_TURN,
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
        bool _lootHandled;
        Enemy _lastAttackedEnemy;
        List<DeckBuilder.Card> _freeForTheTurn;
        bool _firstDraw;
        int _relicIterator, _enemyIterator;

        public CombatHandler()
        {
            _combatUI = new UserInterface.CombatUI(this);

            _turn = combatTurn.COMBAT_START;
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

            _freeForTheTurn = new List<DeckBuilder.Card>();
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
        public void makeCardFreeForTurn(DeckBuilder.Card freeCard)
        {
            _freeForTheTurn.Add(freeCard);
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
        public List<DeckBuilder.Card> getCardsFreeForTheTurn()
        {
            return _freeForTheTurn;
        }

        //Setters
        public void setLastAttackedEnemy(Enemy target)
        {
            _lastAttackedEnemy = target;
        }



        /// <summary>
        /// Make sure setNewEncounter is called first so that the encounter's enemies are properly set up.
        /// </summary>
        public void combatStart(List<UserInterface.UserInterface> activeUI)
        {
            Game1.debugLog.Add("Beginning combat");

            _combatUI.setAsActiveUI(activeUI);
            _lastAttackedEnemy = null;
            _freeForTheTurn.Clear();

            _turn = combatTurn.COMBAT_START;

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

            _lootHandled = false;
            _firstDraw = false;
            _relicIterator = -1;
            _enemyIterator = -1;
        }

        public void combatResetTurn_ChampDied(List<UserInterface.UserInterface> activeUI)
        {
            _combatUI.setAsActiveUI(activeUI);
            _lastAttackedEnemy = null;
            _freeForTheTurn.Clear();

            _turn = combatTurn.RESETSTUFF_AND_INTENTS;

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

            _lootHandled = false;
            _relicIterator = -1;
            _enemyIterator = -1;
        }

        /// <summary>
        /// Main game loop method for handling combat. Whether or not combat is over gets handled here (except for menu quitting, etc).
        /// </summary>
        public void handleCombat()
        {
            //Check if the combat needs to end
            if (_champ.getDowned())
            {
                if (_partyMembers != null && _partyMembers.Count > 0)
                {
                    Treasury.Loot currentEncounterLoot = _currentEncounter.getLoot();
                    currentEncounterLoot.addTreasure(new Treasury.Equipment.Items.Corpse(Game1.getChamp().getName(), Game1.getChamp().getHero()));
                    _currentEncounter.setLoot(currentEncounterLoot);

                    //FIXIT make the new champion a choice and not random
                    int partyIndex = Game1.randint(0, _partyMembers.Count - 1);
                    Game1.setupNewChampion(_partyMembers[partyIndex].getHero(), true);
                    return;
                }
                else
                {
                    Game1.endDemo(); //Game over
                    return;
                }
            }
            else if (_currentEncounter.areAllEnemiesDefeated())
            {
                endCombat();
                return;
            }
            
            //Perform turn logic
            switch (_turn)
            {
                case combatTurn.COMBAT_START:
                    //Relics & party buffs that affect the start of combat before any cards are drawn
                    _relicIterator++;
                    if (_relicIterator < _partyMembers.Count)
                    {
                        _partyMembers[_relicIterator].getPartyMemberBuff().onCombatStart();
                    }
                    else if (_relicIterator < _partyMembers.Count + Game1.getDungeonHandler().getRelics().Count)
                    {
                        Game1.getDungeonHandler().getRelics()[_relicIterator - _partyMembers.Count].onCombatStart();
                    }
                    else
                    {
                        _relicIterator = -1;
                        Game1.getDungeonHandler().clearRemovedRelics();
                        nextTurn();
                    }
                    updateCombatUI();
                    break;
                case combatTurn.RESETSTUFF_AND_INTENTS:
                    _champ.resetDivinity();
                    _champ.resetDefense();
                    _currentEncounter.determineIntents(_champ, _partyMembers);
                    nextTurn();
                    updateCombatUI();
                    break;
                case combatTurn.TURN_START:
                    //Relics & party buffs that affect the start of the turn before any cards are drawn
                    _relicIterator++;
                    if (_relicIterator < _partyMembers.Count)
                    {
                        _partyMembers[_relicIterator].getPartyMemberBuff().onTurnStart();
                    }
                    else if (_relicIterator < _partyMembers.Count + Game1.getDungeonHandler().getRelics().Count)
                    {
                        Game1.getDungeonHandler().getRelics()[_relicIterator - _partyMembers.Count].onTurnStart();
                    }
                    else
                    {
                        _relicIterator = -1;
                        Game1.getDungeonHandler().clearRemovedRelics();
                        nextTurn();
                    }
                    updateCombatUI();
                    break;
                case combatTurn.DRAW_CARDS:
                    _champ.getDeck().drawNumCards(_champ.getCardDraw(true)); //Draw new cards and set the champion's card draw back to normal
                    nextTurn();
                    updateCombatUI();
                    break;
                case combatTurn.TURN_START_AFTER_DRAW_CARDS:
                    if (!_firstDraw)
                    {
                        //Relics & party buffs that affect the start of the combat after the first time the champion draws cards
                        _relicIterator++;
                        if (_relicIterator < _partyMembers.Count)
                        {
                            _partyMembers[_relicIterator].getPartyMemberBuff().onCombatStartAfterFirstDraw();
                        }
                        else if (_relicIterator < _partyMembers.Count + Game1.getDungeonHandler().getRelics().Count)
                        {
                            Game1.getDungeonHandler().getRelics()[_relicIterator - _partyMembers.Count].onCombatStartAfterFirstDraw();
                        }
                        else
                        {
                            _relicIterator = -1;
                            Game1.getDungeonHandler().clearRemovedRelics();
                            _firstDraw = true;
                        }
                    }
                    else
                    {
                        //Relics & party buffs that affect the start of the turn after the champion draws cards
                        _relicIterator++;
                        if (_relicIterator < _partyMembers.Count)
                        {
                            _partyMembers[_relicIterator].getPartyMemberBuff().onTurnStartAfterDraw();
                        }
                        else if (_relicIterator < _partyMembers.Count + Game1.getDungeonHandler().getRelics().Count)
                        {
                            Game1.getDungeonHandler().getRelics()[_relicIterator - _partyMembers.Count].onTurnStartAfterDraw();
                        }
                        else
                        {
                            _relicIterator = -1;
                            Game1.getDungeonHandler().clearRemovedRelics();
                            nextTurn();
                        }
                    }
                    updateCombatUI();
                    break;
                case combatTurn.LOWER_BUFFS:
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
                    nextTurn();
                    updateCombatUI();
                    break;
                case combatTurn.PLAY:
                    //Nothing needs to be called here since it's all handled by the UI
                    break;
                case combatTurn.TURN_END_RESET_CARD_STUFF:
                    //Reset cards to normal costs if any were made free for the turn
                    for (int i = 0; i < _freeForTheTurn.Count; i++)
                    {
                        findAndResetCardCost(_freeForTheTurn[i]);
                    }
                    nextTurn();
                    updateCombatUI();
                    break;
                case combatTurn.TURN_END_DEALS_DAMAGE:
                    _relicIterator++;
                    if (_relicIterator < _partyMembers.Count)
                    {
                        _partyMembers[_relicIterator].getPartyMemberBuff().onTurnEndDealsDamage();
                    }
                    else if (_relicIterator < _partyMembers.Count + Game1.getDungeonHandler().getRelics().Count)
                    {
                        Game1.getDungeonHandler().getRelics()[_relicIterator - _partyMembers.Count].onTurnEndDealsDamage();
                    }
                    else
                    {
                        _relicIterator = -1;
                        Game1.getDungeonHandler().clearRemovedRelics();
                        nextTurn();
                    }
                    updateCombatUI();
                    break;
                case combatTurn.TURN_END_BEFORE_DISCARD:
                    _relicIterator++;
                    if (_relicIterator < _partyMembers.Count)
                    {
                        _partyMembers[_relicIterator].getPartyMemberBuff().onTurnEndBeforeDiscard();
                    }
                    else if (_relicIterator < _partyMembers.Count + Game1.getDungeonHandler().getRelics().Count)
                    {
                        Game1.getDungeonHandler().getRelics()[_relicIterator - _partyMembers.Count].onTurnEndBeforeDiscard();
                    }
                    else
                    {
                        _relicIterator = -1;
                        Game1.getDungeonHandler().clearRemovedRelics();
                        nextTurn();
                    }
                    updateCombatUI();
                    break;
                case combatTurn.TURN_END_DISCARD:
                    //Discard cards that are in your hand
                    Game1.getChamp().getDeck().turnEndDiscardAll();
                    nextTurn();
                    updateCombatUI();
                    break;
                case combatTurn.TURN_END:
                    _relicIterator++;
                    if (_relicIterator < _partyMembers.Count)
                    {
                        _partyMembers[_relicIterator].getPartyMemberBuff().onTurnEnd();
                    }
                    else if (_relicIterator < _partyMembers.Count + Game1.getDungeonHandler().getRelics().Count)
                    {
                        Game1.getDungeonHandler().getRelics()[_relicIterator - _partyMembers.Count].onTurnEnd();
                    }
                    else
                    {
                        _relicIterator = -1;
                        Game1.getDungeonHandler().clearRemovedRelics();
                        nextTurn();
                    }
                    updateCombatUI();
                    break;
                case combatTurn.RESET_ENEMY_DEFENSE:
                    _currentEncounter.resetDefense();
                    nextTurn();
                    updateCombatUI();
                    break;
                case combatTurn.ENEMY_TURN:
                    _enemyIterator++;
                    if (_enemyIterator < _currentEncounter.getEnemies().Count)
                    {
                        if (!_currentEncounter.getEnemies()[_enemyIterator].getDowned())
                        {
                            _currentEncounter.getEnemies()[_enemyIterator].getAIPattern().doTurnAction(_champ, _partyMembers);
                        }
                    }
                    else
                    {
                        _enemyIterator = -1;
                        nextTurn();
                    }
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
        /// Transitions to the next step of combat (not necessarily he next turn).
        /// </summary>
        public void nextTurn()
        {
            switch (_turn)
            {
                case combatTurn.COMBAT_START:
                    _turn = combatTurn.RESETSTUFF_AND_INTENTS;
                    break;
                case combatTurn.RESETSTUFF_AND_INTENTS:
                    _turn = combatTurn.TURN_START;
                    break;
                case combatTurn.TURN_START:
                    _turn = combatTurn.DRAW_CARDS;
                    break;
                case combatTurn.DRAW_CARDS:
                    _turn = combatTurn.TURN_START_AFTER_DRAW_CARDS;
                    break;
                case combatTurn.TURN_START_AFTER_DRAW_CARDS:
                    _turn = combatTurn.LOWER_BUFFS;
                    break;
                case combatTurn.LOWER_BUFFS:
                    _turn = combatTurn.PLAY;
                    break;
                case combatTurn.PLAY:
                    _turn = combatTurn.TURN_END_RESET_CARD_STUFF;
                    break;
                case combatTurn.TURN_END_RESET_CARD_STUFF:
                    _turn = combatTurn.TURN_END_DEALS_DAMAGE;
                    break;
                case combatTurn.TURN_END_DEALS_DAMAGE:
                    _turn = combatTurn.TURN_END_BEFORE_DISCARD;
                    break;
                case combatTurn.TURN_END_BEFORE_DISCARD:
                    _turn = combatTurn.TURN_END_DISCARD;
                    break;
                case combatTurn.TURN_END_DISCARD:
                    _turn = combatTurn.TURN_END;
                    break;
                case combatTurn.TURN_END:
                    _turn = combatTurn.RESET_ENEMY_DEFENSE;
                    break;
                case combatTurn.RESET_ENEMY_DEFENSE:
                    _turn = combatTurn.ENEMY_TURN;
                    break;
                case combatTurn.ENEMY_TURN:
                    _turn = combatTurn.KARMA;
                    break;
                case combatTurn.KARMA:
                    _turn = combatTurn.VOID;
                    break;
                case combatTurn.VOID:
                    _turn = combatTurn.ROUND_END;
                    break;
                case combatTurn.ROUND_END:
                    _turn = combatTurn.RESETSTUFF_AND_INTENTS;
                    break;
            }
        }

        /// <summary>
        /// Handles the end of combat when all enemies have been defeated. Sets up loot if
        /// that hasn't been done yet, and tells the engine to return to the dungeon otherwise.
        /// </summary>
        public void endCombat(bool enemiesDefeated = true)
        {
            //Reset things so weird stuff doesn't happen out of combat
            _champ.resetBuffs();
            _champ.resetDefense();

            if (enemiesDefeated) //double-check to make sure
            {
                if (!_lootHandled)
                {
                    //Relics on champion attack after use, including party member buffs
                    for (int i = 0; i < Game1.getCombatHandler().getParty().Count; i++)
                    {
                        Game1.getCombatHandler().getParty()[i].getPartyMemberBuff().onCombatEnd();
                    }
                    for (int i = 0; i < Game1.getDungeonHandler().getRelics().Count; i++)
                    {
                        Game1.getDungeonHandler().getRelics()[i].onCombatEnd();
                    }

                    Game1.debugLog.Add("Enemies defeated; generating loot.");

                    //Set stats in a way that they don't affect card numbers
                    _champ.tempSetStrengthTo0();
                    _champ.tempSetDexterityTo0();
                    _champ.tempSetResilienceTo0();
                    Game1.addToMenus(new UserInterface.Menus.LootMenu(_currentEncounter.getLoot(), UserInterface.Menus.LootMenu.COMBAT_LOOT));
                    _lootHandled = true;
                }
                else
                {
                    if (Game1.getDungeonHandler().isRandomEncounter())
                    {
                        Game1.getDungeonHandler().randomEncounterComplete();
                    }
                    else if (Game1.getDungeonHandler().isRestEncounter())
                    {
                        Game1.getDungeonHandler().restEncounterComplete();
                    }
                    else
                    {
                        _currentRoom.finishTopContent();
                    }
                    Game1.returnToDungeon();
                }
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



        public void findAndResetCardCost(DeckBuilder.Card cardToReset)
        {
            //Check hand first
            if (_champ.getDeck().getHand().Contains(cardToReset))
            {
                _champ.getDeck().getHand()[_champ.getDeck().getHand().IndexOf(cardToReset)].resetCosts();
                return;
            }
            
            //Check discard pile next; maybe the card was played
            if (_champ.getDeck().getDiscardPile().Contains(cardToReset))
            {
                _champ.getDeck().getDiscardPile()[_champ.getDeck().getDiscardPile().IndexOf(cardToReset)].resetCosts();
                return;
            }

            //Check draw pile next
            if (_champ.getDeck().getDrawPile().Contains(cardToReset))
            {
                _champ.getDeck().getDrawPile()[_champ.getDeck().getDrawPile().IndexOf(cardToReset)].resetCosts();
                return;
            }

            //Check removed cards last
            if (_champ.getDeck().getRemovedCards().Contains(cardToReset))
            {
                _champ.getDeck().getRemovedCards()[_champ.getDeck().getRemovedCards().IndexOf(cardToReset)].resetCosts();
                return;
            }

            //Can't find it; that's bad
            Game1.addToErrorLog("Tried to reset card costs at end of turn but couldn't find card: " + cardToReset.getName());
        }
    }
}