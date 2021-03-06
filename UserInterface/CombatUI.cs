using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface
{
    /// <summary>
    /// One of the major three gameState UIs along with MapI and EventUI. CombatUI is the
    /// most involved of the three, requiring UserInterfaces for all of the different parts
    /// of combat (cards, piles, enemies, party members, and the champion, as well as the
    /// less obvious ones such as buffs/debuffs and targets that appear when a card is in
    /// use. For the CombatUI in particular, proper ordering of Clickables is important, as
    /// there is much more opportunity for overlap.
    /// </summary>
    public class CombatUI
    {
        List<UserInterface> _wholeUI;

        UserInterface _handCards;
        UserInterface _cardPiles;
        UserInterface _combatUIButtons;
        UserInterface _enemies;
        UserInterface _enemyHovers;
        UserInterface _partyMembers;
        UserInterface _partyMemberHovers;
        Clickables.Avatar _championUI;
        UserInterface _championHovers;
        Clickables.HandCard _activeCard;
        UserInterface _targets;
        UserInterface _resources;
        UserInterface _relics;

        public CombatUI(Combat.CombatHandler combatHandler)
        {
            _wholeUI = new List<UserInterface>();

            _handCards = new UserInterface();
            _cardPiles = new UserInterface();
            _combatUIButtons = new UserInterface();
            _enemies = new UserInterface();
            _enemyHovers = new UserInterface();
            _partyMembers = new UserInterface();
            _partyMemberHovers = new UserInterface();
            _championUI = new Clickables.Avatar();
            _championHovers = new UserInterface();
            _targets = new UserInterface();
            _resources = new UserInterface();
            _relics = new UserInterface();

            //added in sorted fashion, top to bottom is front of the screen to back
            _wholeUI.Add(_targets);
            _wholeUI.Add(_handCards);
            _wholeUI.Add(_enemyHovers);
            _wholeUI.Add(_enemies);
            _wholeUI.Add(_partyMemberHovers);
            _wholeUI.Add(_partyMembers);
            _wholeUI.Add(_championHovers);
            _championHovers.addClickableToFront(_championUI);
            _wholeUI.Add(_cardPiles);
            _wholeUI.Add(_combatUIButtons);
            _wholeUI.Add(_resources);
            _wholeUI.Add(_relics);

            //Further initialization
            initializeCombatButtons(combatHandler);
        }

        public void initializeCombatButtons(Combat.CombatHandler combatHandler)
        {
            _combatUIButtons.resetClickables();

            //End Turn button
            Clickables.Button endTurnButton = new Clickables.Button(Game1.pic_functionality_endTurnButton,
                new Point(Game1.VIRTUAL_WINDOW_WIDTH - Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_X_FROMRIGHT - Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_WIDTH, Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_Y),
                Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_WIDTH, Drawing.DrawConstants.COMBAT_ENDTURNBUTTON_HEIGHT, () =>
                {
                    //Make sure it's your turn
                    if (combatHandler.getTurn() == Combat.CombatHandler.combatTurn.PLAY)
                    {
                        combatHandler.nextTurn();
                    }
                }, new List<String>() { "Ends your turn." });

            _combatUIButtons.addClickableToFront(endTurnButton);

            //Inventory button
            if (Game1.getInventory() != null)
            {
                Clickables.Button inventoryButton = new Clickables.Button(Game1.pic_functionality_combatInventoryIcon,
                    new Point(Drawing.DrawConstants.COMBAT_INVENTORY_BUTTON_X, Drawing.DrawConstants.COMBAT_INVENTORY_BUTTON_Y),
                    Drawing.DrawConstants.COMBAT_INVENTORY_BUTTON_WIDTH, Drawing.DrawConstants.COMBAT_INVENTORY_BUTTON_HEIGHT, () =>
                    {
                        Game1.addToMenus(new Menus.InventoryMenu(Game1.getInventory()));
                    }, new List<String>() { "Inventory" });

                _combatUIButtons.addClickableToFront(inventoryButton);
            }
        }


        //Setters
        public void setAsActiveUI(List<UserInterface> activeUI)
        {
            activeUI.Clear();

            for (int i = 0; i < _wholeUI.Count; i++)
            {
                activeUI.Add(_wholeUI[i]);
            }

            Game1.addTopBar();
        }
        public void setActiveCard(Clickables.HandCard card)
        {
            _activeCard = card;

            _targets.resetClickables(); //do it here instead of in setupTargets because it needs to reset even when deseslecting

            //If selecting (and not unselecting), setup targets for that card
            if (_activeCard != null)
            {
                Clickables.Target.setupTargets(_targets, _enemies, _partyMembers, _championUI, card.getCard().getTargetType());
            }
        }
        public void setEnemiesAsUI(UserInterface enemies)
        {
            _enemies = enemies;
        }
        public void setTargets(UserInterface targets)
        {
            _targets = targets;
        }



        //Getters
        public Clickables.HandCard getActiveCard()
        {
            return _activeCard;
        }
        public UserInterface getTargets()
        {
            return _targets;
        }
        public UserInterface getEnemies()
        {
            return _enemies;
        }



        public void updateCombatUI(Combat.CombatHandler combatHandler)
        {
            Game1.setHoveredClickable(null); //just in case so no outdated hovers stay in memory. if they're hovering over something, it'll just get re-hovered next game tick at latest, so it's fine

            Clickables.HandCard.setupHandUI(_handCards, Game1.getChamp().getDeck().getHand());

            _cardPiles.resetClickables();
            _cardPiles.addClickableToFront(new Clickables.DeckOfCards(Clickables.DeckOfCards.typeOfDeck.REMOVEDPILE, Game1.getChamp(), "Removed card"));
            _cardPiles.addClickableToFront(new Clickables.DeckOfCards(Clickables.DeckOfCards.typeOfDeck.DISCARDPILE, Game1.getChamp(), "Discard pile"));
            _cardPiles.addClickableToFront(new Clickables.DeckOfCards(Clickables.DeckOfCards.typeOfDeck.DRAWPILE, Game1.getChamp(), "Draw pile"));

            initializeCombatButtons(combatHandler);

            Clickables.Opponent.setupEnemyUI(_enemies, combatHandler.getCurrentEncounter(), _enemyHovers);

            if (combatHandler.getParty().Count > 0)
            {
                Clickables.GroupMember.setupPartyUI(_partyMembers, combatHandler.getParty(), _partyMemberHovers);
            }

            Clickables.Avatar.setupChampionUI(_championUI, Game1.getChamp(), _championHovers);
            Clickables.Hovers.RelicDisplay.setupRelicsUI(_relics);

            _resources.resetClickables();
            _resources.addClickableToBack(new Clickables.Hovers.Resource(new Point(Drawing.DrawConstants.COMBAT_DIVINITY_X, Drawing.DrawConstants.COMBAT_DIVINITY_Y),
                (int)Game1.roboto_bold_16.MeasureString(Clickables.Hovers.Resource.DIVINITY_SAMPLE_STRING).X, Drawing.DrawConstants.TEXT_16_HEIGHT, DeckBuilder.CardEnums.CostType.DIVINITY));

            Game1.updateTopBar();
        }

        public void updateCardDescriptions()
        {
            Clickables.HandCard.setupHandUI(_handCards, Game1.getChamp().getDeck().getHand());
        }

        public void updateEnemyIntents(Combat.CombatHandler combatHandler)
        {
            Clickables.Opponent.setupEnemyUI(_enemies, combatHandler.getCurrentEncounter(), _enemyHovers);
        }
    }
}
