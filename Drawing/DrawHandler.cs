using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Drawing
{
    class DrawHandler
    {
        int _pulse, _pulseAtMax;
        double _pulseIncrease;
        EventTextBox _eventTextBox;

        public DrawHandler()
        {
            _pulse = 0;
            _pulseAtMax = 0;
            _pulseIncrease = DrawConstants.PULSE_START;
        }

        public void pulse()
        {
            _pulse += (int)(_pulseIncrease + DrawConstants.PULSE_BOOST);
            _pulseIncrease += DrawConstants.PULSE_STEP;

            if (_pulse >= DrawConstants.PULSE_MAX)
            {
                _pulse = DrawConstants.PULSE_MAX;
                _pulseAtMax += 1;

                if (_pulseAtMax > DrawConstants.PULSE_TIME_AT_MAX)
                {
                    _pulse = 0;
                    _pulseAtMax = 0;
                    _pulseIncrease = DrawConstants.PULSE_START;
                }
            }
        }

        public void setEventTextBox(EventTextBox eventTextBox)
        {
            _eventTextBox = eventTextBox;
        }



        /// <summary>
        /// Draws the space behind the UI. Aesthetics only.
        /// </summary>
        public static void drawTitle_Background(SpriteBatch sprites)
        {
            sprites.Draw(Game1.pic_functionality_uiSketch, new Rectangle(
                0, 0, Game1.VIRTUAL_WINDOW_WIDTH, Game1.VIRTUAL_WINDOW_HEIGHT), Color.White);
        }

        /// <summary>
        /// Draws the space behind the UI. Aesthetics only.
        /// </summary>
        public static void drawCombat_Background(SpriteBatch sprites)
        {
            //Nothing yet. I will probably have to split the UI up into things that will be drawn before the foreground and things drawn after it.
        }

        /// <summary>
        /// Draws the space in front of the UI. Aesthetics only.
        /// </summary>
        public static void drawCombat_Foreground(SpriteBatch sprites)
        {
            //Nothing yet. I will probably have to split the UI up into things that will be drawn before the foreground and things drawn after it. Maybe this can be Midground, who knows
        }

        /// <summary>
        /// Draws the space behind the UI. Aesthetics only.
        /// </summary>
        public static void drawMap_Background(SpriteBatch sprites)
        {
            sprites.Draw(Game1.pic_background_map, new Rectangle(
                0, DrawConstants.TOPBAR_HEIGHT, Game1.VIRTUAL_WINDOW_WIDTH, Game1.VIRTUAL_WINDOW_HEIGHT - DrawConstants.TOPBAR_HEIGHT), Color.White);
        }

        /// <summary>
        /// Draws the space in front of the UI. Aesthetics only.
        /// </summary>
        public static void drawMap_Foreground(SpriteBatch sprites)
        {
            //Nothing yet. I will probably have to split the UI up into things that will be drawn before the foreground and things drawn after it. Maybe this can be Midground, who knows
        }

        /// <summary>
        /// Draws the background for events. Aesthetics only. Drawn over whatever other
        /// background is behind it, since it doesn't take up the whole screen.
        /// </summary>
        public void drawEvent_Background(SpriteBatch sprites)
        {
            sprites.Draw(Game1.pic_background_event, new Rectangle(
                Game1.VIRTUAL_WINDOW_WIDTH / 2 - DrawConstants.EVENT_BACKGROUND_WIDTH / 2, Game1.VIRTUAL_WINDOW_HEIGHT / 2 - DrawConstants.EVENT_BACKGROUND_HEIGHT / 2,
                DrawConstants.EVENT_BACKGROUND_WIDTH, DrawConstants.EVENT_BACKGROUND_HEIGHT), Color.White);

            if (_eventTextBox != null)
            {
                _eventTextBox.draw(sprites);
            }
        }

        /// <summary>
        /// Draws the background behind a menu, which is meant as a separator between
        /// the menu UI and the UI that's being drawn over.
        /// </summary>
        public static void drawMenuBackground(SpriteBatch sprites, UserInterface.MenuUI menu)
        {
            sprites.Draw(menu._backgroundTexture, new Rectangle(menu._x, yFromBottom(menu._y, menu._height), menu._width, menu._height), menu.getBackgroundColor());

            sprites.DrawString(menu.getTitleFont(), menu.getTitle(),
                new Vector2(menu._titleX + 1, yFromBottom(menu._titleY + 1, menu._titleFontHeight)), menu.getTitleShadowColor());
            sprites.DrawString(menu.getTitleFont(), menu.getTitle(),
                new Vector2(menu._titleX + 1, yFromBottom(menu._titleY - 1, menu._titleFontHeight)), menu.getTitleShadowColor());
            sprites.DrawString(menu.getTitleFont(), menu.getTitle(),
                new Vector2(menu._titleX - 1, yFromBottom(menu._titleY + 1, menu._titleFontHeight)), menu.getTitleShadowColor());
            sprites.DrawString(menu.getTitleFont(), menu.getTitle(),
                new Vector2(menu._titleX - 1, yFromBottom(menu._titleY - 1, menu._titleFontHeight)), menu.getTitleShadowColor());

            sprites.DrawString(menu.getTitleFont(), menu.getTitle(),
                new Vector2(menu._titleX, yFromBottom(menu._titleY, menu._titleFontHeight)), menu.getTitleColor());
        }

        /// <summary>
        /// Receives as a parameter a list of every active UI element that should be on the
        /// screen. Iterates through each from back to front so that front is on top.
        /// </summary>
        public void drawUI(SpriteBatch sprites, List<UserInterface.UserInterface> activeUI, Characters.Champion champ)
        {
            //Backwards-iterate, since we're drawing from back of the screen to front
            if (activeUI != null)
            {
                for (int ui = activeUI.Count - 1; ui >= 0; ui--)
                {
                    for (int ci = activeUI[ui].getClickables().Count - 1; ci >= 0; ci--)
                    {
                        UserInterface.Clickable current = activeUI[ui].getClickables()[ci];

                        drawInterface(sprites, current, champ);
                    }
                }
            }

            if (Game1.showDebugLog)
            {
                drawGameLogs(sprites, Game1.debugLog, Color.White);
            }
            else if (Game1.showErrorLog)
            {
                drawGameLogs(sprites, Game1.errorLog, Color.Red);
            }
        }

        /// <summary>
        /// Determines the type of Clickable passed to it, and then sends it off to the
        /// appropriate function for drawing.
        /// </summary>
        public void drawInterface(SpriteBatch sprites, UserInterface.Clickable current, Characters.Champion champ)
        {
            if (current.GetType() == typeof(UserInterface.Clickables.HandCard))
            {
                drawCombat_HandCard(sprites, (UserInterface.Clickables.HandCard)current, champ);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.DeckOfCards))
            {
                if (((UserInterface.Clickables.DeckOfCards)current).getDeckType() != UserInterface.Clickables.DeckOfCards.typeOfDeck.WHOLECOLLECTION)
                {
                    drawCombat_CardPile(sprites, (UserInterface.Clickables.DeckOfCards)current, champ);
                }
                else
                {
                    //FIXIT implement
                    Game1.addToErrorLog("drawUI attempting to draw DeckOfCards but typeOfDeck is not yet implemented: " + ((UserInterface.Clickables.DeckOfCards)current).getDeckType().ToString());
                }
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Button))
            {
                drawButton(sprites, (UserInterface.Clickables.Button)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Opponent))
            {
                drawEnemy(sprites, (UserInterface.Clickables.Opponent)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Hovers.HPBar))
            {
                drawHPBar(sprites, (UserInterface.Clickables.Hovers.HPBar)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Hovers.EnemyIntent))
            {
                drawEnemyIntent(sprites, (UserInterface.Clickables.Hovers.EnemyIntent)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Avatar))
            {
                drawChampion(sprites, (UserInterface.Clickables.Avatar)current, champ);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Target))
            {
                drawTarget(sprites, (UserInterface.Clickables.Target)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Hovers.Resource))
            {
                drawResource(sprites, (UserInterface.Clickables.Hovers.Resource)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.MapGrid))
            {
                drawMapGrid(sprites, (UserInterface.Clickables.MapGrid)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Hovers.DynamicText))
            {
                drawDynamicText(sprites, (UserInterface.Clickables.Hovers.DynamicText)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Decision))
            {
                drawDecision(sprites, (UserInterface.Clickables.Decision)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.LootableTreasure))
            {
                drawLootableTreasure(sprites, (UserInterface.Clickables.LootableTreasure)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.CardChoice))
            {
                drawCardChoice(sprites, (UserInterface.Clickables.CardChoice)current, champ);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.AestheticOnly))
            {
                drawAestheticOnly(sprites, (UserInterface.Clickables.AestheticOnly)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.MenuCard))
            {
                drawMenuCard(sprites, (UserInterface.Clickables.MenuCard)current, champ);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.MultiTarget))
            {
                drawMultiTarget(sprites, (UserInterface.Clickables.MultiTarget)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.GroupMember))
            {
                drawPartyMember(sprites, (UserInterface.Clickables.GroupMember)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Hovers.StatusEffect))
            {
                drawStatusEffect(sprites, (UserInterface.Clickables.Hovers.StatusEffect)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Hovers.InventoryGrid))
            {
                drawInventoryGrid(sprites, (UserInterface.Clickables.Hovers.InventoryGrid)current);
            }
            else if (current.GetType() == typeof(UserInterface.Clickables.Hovers.CollectedKey))
            {
                drawCollectedKey(sprites, (UserInterface.Clickables.Hovers.CollectedKey)current);
            }
            else
            {
                Game1.addToErrorLog("drawUI attempting to draw from activeUI but typeof Clickable is not defined in if statement: " + current.GetType().ToString());
            }
        }

        public static void drawCard(SpriteBatch sprites, DeckBuilder.Card card, Rectangle cardRect, SpriteFont nameFont, int nameFontHeight,
            SpriteFont descFont, int descFontHeight, SpriteFont costFont, int costFontHeight, int nameAreaWidth, int nameYBuffer, int divinityAdjustX,
            int costAdjustYFromTop, int costIconSize, int bloodAdjustX, Characters.Champion champ, bool isHovered, bool isActive, bool glowCard)
        {
            String name = card.getName();

            List<String> description = card.getDescription(champ);
            if (isActive)
            {
                description = card.getDescription(champ, true);
            }

            Texture2D cardFromRarity = Game1.pic_card_front_default;
            switch (card.getCardRarity())
            {
                case DeckBuilder.CardEnums.CardRarity.COMMON:
                    cardFromRarity = Game1.pic_card_front_common;
                    break;
                case DeckBuilder.CardEnums.CardRarity.RARE:
                    cardFromRarity = Game1.pic_card_front_rare;
                    break;
                case DeckBuilder.CardEnums.CardRarity.EPIC:
                    cardFromRarity = Game1.pic_card_front_epic;
                    break;
                case DeckBuilder.CardEnums.CardRarity.GODLY:
                    cardFromRarity = Game1.pic_card_front_godly;
                    break;
                case DeckBuilder.CardEnums.CardRarity.VOID:
                    cardFromRarity = Game1.pic_card_front_void;
                    break;
            }

            //Draw card glow
            if (glowCard)
            {
                for (int i = 0; i < DrawConstants.CARD_GLOW_NUM_STEPS; i++)
                {
                    sprites.Draw(cardFromRarity,
                        new Rectangle(cardRect.X - DrawConstants.CARD_GLOW_FURTHEST + DrawConstants.CARD_GLOW_STEP * i,
                        yFromBottom(cardRect.Y - DrawConstants.CARD_GLOW_FURTHEST + DrawConstants.CARD_GLOW_STEP * i,
                        cardRect.Height + DrawConstants.CARD_GLOW_FURTHEST * 2 - DrawConstants.CARD_GLOW_STEP * i * 2),
                        cardRect.Width + DrawConstants.CARD_GLOW_FURTHEST * 2 - DrawConstants.CARD_GLOW_STEP * i * 2,
                        cardRect.Height + DrawConstants.CARD_GLOW_FURTHEST * 2 - DrawConstants.CARD_GLOW_STEP * i * 2),
                        Color.PowderBlue * DrawConstants.CARD_GLOW_OPACITY);
                }
            }

            //Draw card background
            sprites.Draw(cardFromRarity, new Rectangle(cardRect.X, yFromBottom(cardRect.Y, cardRect.Height), cardRect.Width, cardRect.Height), Color.White);

            //Draw card art


            
            int divinityCost = card.getPlayCost(DeckBuilder.CardEnums.CostType.DIVINITY);
            int bloodCost = card.getPlayCost(DeckBuilder.CardEnums.CostType.BLOOD);

            //Draw card name
            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(cardRect.X + cardRect.Width / 2 - nameAreaWidth / 2,
                yFromBottom(cardRect.Y + nameYBuffer + cardRect.Height / 2 - cardRect.Height / 12,
                nameFontHeight), nameAreaWidth, nameFontHeight),
                DeckBuilder.CardEnums.cardRarityToColor(card.getCardRarity()) * DrawConstants.CARD_NAME_RARITY_FADE);
            sprites.DrawString(nameFont, name,
                new Vector2(cardRect.X + cardRect.Width / 2 - nameFont.MeasureString(name).X / 2,
                yFromBottom(cardRect.Y + nameYBuffer + cardRect.Height / 2 - cardRect.Height / 12,
                nameFontHeight)), Color.Black);

            //Draw card description
            for (int i_d = 0; i_d < description.Count; i_d++)
            {
                sprites.DrawString(descFont, description[i_d],
                    new Vector2(cardRect.X + cardRect.Width / 2 - descFont.MeasureString(description[i_d]).X / 2,
                    yFromBottom(cardRect.Y + nameYBuffer + cardRect.Height / 2 -
                    cardRect.Height / 6 - descFontHeight * i_d,
                    descFontHeight)), Color.Black);
            }

            //Draw card cost
            if (divinityCost > 0 || bloodCost == 0 && card.getPlayCost(DeckBuilder.CardEnums.CostType.KARMA) == 0 && card.getPlayCost(DeckBuilder.CardEnums.CostType.SOUL) == 0)
            {
                sprites.Draw(Game1.pic_functionality_cardDivinityIcon, new Rectangle(cardRect.X + divinityAdjustX,
                    yFromBottom(cardRect.Y + cardRect.Height + costAdjustYFromTop, costIconSize),
                    costIconSize, costIconSize), Color.White);

                sprites.DrawString(costFont, divinityCost.ToString(),
                    new Vector2(cardRect.X + divinityAdjustX + costIconSize / 2 - costFont.MeasureString(divinityCost.ToString()).X / 2,
                    yFromBottom(cardRect.Y + cardRect.Height + costAdjustYFromTop + costIconSize / 2 - costFontHeight / 2,
                    costFontHeight)), Color.Black);
            }
            if (bloodCost > 0)
            {
                sprites.Draw(Game1.pic_functionality_cardBloodIcon, new Rectangle(cardRect.X + bloodAdjustX,
                    yFromBottom(cardRect.Y + cardRect.Height + costAdjustYFromTop, costIconSize),
                    costIconSize, costIconSize), Color.White);

                sprites.DrawString(costFont, bloodCost.ToString(),
                    new Vector2(cardRect.X + bloodAdjustX + costIconSize / 2 - costFont.MeasureString(bloodCost.ToString()).X / 2,
                    yFromBottom(cardRect.Y + cardRect.Height + costAdjustYFromTop + costIconSize / 2 - costFontHeight / 2,
                    costFontHeight)), Color.Black);
            }
        }

        public static void drawCombat_HandCard(SpriteBatch sprites, UserInterface.Clickables.HandCard card, Characters.Champion champ, bool glowCard = false)
        {
            if (card == Game1.getCombatHandler().getCombatUI().getActiveCard() || card == Game1.getHoveredClickable())
            {
                //Grow sizes
                drawCard(sprites, card.getCard(), new Rectangle(card._x, card._y, card._width, card._height), Game1.roboto_bold_20, DrawConstants.TEXT_20_HEIGHT,
                    Game1.roboto_medium_16, DrawConstants.TEXT_16_HEIGHT, Game1.roboto_black_24, DrawConstants.TEXT_24_HEIGHT, DrawConstants.COMBAT_HANDCARD_GROW_NAME_AREA_WIDTH,
                    DrawConstants.COMBAT_HANDCARD_GROW_NAME_Y_BUFFER, DrawConstants.COMBAT_HANDCARDS_GROW_DIVINITY_ADJUST_X, DrawConstants.COMBAT_HANDCARDS_GROW_COST_ADJUST_Y_FROMTOP,
                    DrawConstants.COMBAT_HANDCARDS_GROW_COST_ICON_SIZE, DrawConstants.COMBAT_HANDCARDS_GROW_BLOOD_ADJUST_X, champ, card == Game1.getHoveredClickable(),
                    card == Game1.getCombatHandler().getCombatUI().getActiveCard(), glowCard);
            }
            else
            {
                //Smaller sizes
                drawCard(sprites, card.getCard(), new Rectangle(card._x, card._y, card._width, card._height), Game1.roboto_bold_10, DrawConstants.TEXT_10_HEIGHT,
                    Game1.roboto_medium_8, DrawConstants.TEXT_8_HEIGHT, Game1.roboto_black_12, DrawConstants.TEXT_12_HEIGHT, DrawConstants.COMBAT_HANDCARD_NAME_AREA_WIDTH,
                    DrawConstants.COMBAT_HANDCARD_NAME_Y_BUFFER, DrawConstants.COMBAT_HANDCARDS_DIVINITY_ADJUST_X, DrawConstants.COMBAT_HANDCARDS_COST_ADJUST_Y_FROMTOP,
                    DrawConstants.COMBAT_HANDCARDS_COST_ICON_SIZE, DrawConstants.COMBAT_HANDCARDS_BLOOD_ADJUST_X, champ, card == Game1.getHoveredClickable(),
                    card == Game1.getCombatHandler().getCombatUI().getActiveCard(), glowCard);
            }
        }

        private static void drawCombat_CardPile(SpriteBatch sprites, UserInterface.Clickables.DeckOfCards deck, Characters.Champion champ)
        {
            List<DeckBuilder.Card> pile = new List<DeckBuilder.Card>();

            switch (deck.getDeckType())
            {
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.DRAWPILE:
                    pile = champ.getDeck().getDrawPile();
                    break;
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.DISCARDPILE:
                    pile = champ.getDeck().getDiscardPile();
                    break;
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.REMOVEDPILE:
                    pile = champ.getDeck().getRemovedCards();
                    break;
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.DECK:
                    sprites.Draw(Game1.pic_functionality_topBarDeckIcon, new Rectangle(deck._x, yFromBottom(deck._y, deck._height), deck._width, deck._height), Color.White);
                    return; //don't perform the rest of the drawing; that's for the other types
                default:
                    Game1.addToErrorLog("drawCombat_CardPile deck typeOfDeck is not one that should be accessible in combat: " + deck.getDeckType().ToString());
                    break;
            }

            //Draw cards in pile - up until at most two full hand capacities' worth of cards or the number that are actually in the pile, whichever is lowest
            for (int i = 0; i < pile.Count && i < DeckBuilder.Deck.MAX_HAND_CAPACITY * 2; i++)
            {
                sprites.Draw(Game1.pic_functionality_cardDown, new Rectangle(
                        deck._x + DrawConstants.COMBAT_DRAW_PILE_BUFFER + (int)((double)i * 0.25),
                        yFromBottom(deck._y + DrawConstants.COMBAT_DRAW_PILE_BUFFER + i * 2, DrawConstants.COMBAT_CARDDOWN_HEIGHT),
                        DrawConstants.COMBAT_CARDDOWN_WIDTH, DrawConstants.COMBAT_CARDDOWN_HEIGHT), Color.White);
            }
        }

        public static void drawUI_GlowCardPile(SpriteBatch sprites, UserInterface.Clickables.DeckOfCards deck, Characters.Champion champ)
        {
            List<DeckBuilder.Card> pile = new List<DeckBuilder.Card>();

            switch (deck.getDeckType())
            {
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.DRAWPILE:
                    pile = champ.getDeck().getDrawPile();
                    break;
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.DISCARDPILE:
                    pile = champ.getDeck().getDiscardPile();
                    break;
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.REMOVEDPILE:
                    pile = champ.getDeck().getRemovedCards();
                    break;
                case UserInterface.Clickables.DeckOfCards.typeOfDeck.DECK:
                    if (deck == Game1.getHoveredClickable())
                    {
                        //Glow
                        for (int i = 0; i < DrawConstants.TOPBARDECK_GLOW_NUM_STEPS; i++)
                        {
                            sprites.Draw(Game1.pic_functionality_topBarDeckIcon,
                                new Rectangle(deck._x - DrawConstants.TOPBARDECK_GLOW_FURTHEST + DrawConstants.TOPBARDECK_GLOW_STEP * i - DrawConstants.TOPBARDECK_GLOW_EXTRA_X_PER_STEP * i,
                                yFromBottom(deck._y - DrawConstants.TOPBARDECK_GLOW_FURTHEST + DrawConstants.TOPBARDECK_GLOW_STEP * i,
                                deck._height + DrawConstants.TOPBARDECK_GLOW_FURTHEST * 2 - DrawConstants.TOPBARDECK_GLOW_STEP * i * 2),
                                deck._width + DrawConstants.TOPBARDECK_GLOW_FURTHEST * 2 - DrawConstants.TOPBARDECK_GLOW_STEP * i * 2 + DrawConstants.TOPBARDECK_GLOW_EXTRA_X_PER_STEP * i * 2,
                                deck._height + DrawConstants.TOPBARDECK_GLOW_FURTHEST * 2 - DrawConstants.TOPBARDECK_GLOW_STEP * i * 2),
                                Color.PowderBlue * DrawConstants.TOPBARDECK_GLOW_OPACITY);
                        }
                    }
                    return; //don't perform the rest of the drawing; that's for the other types
                default:
                    Game1.addToErrorLog("drawUI_GlowCardPile deck typeOfDeck is not one that should be accessible in this function: " + deck.getDeckType().ToString());
                    break;
            }

            //Draw cards in pile - up until at most two full hand capacities' worth of cards or the number that are actually in the pile, whichever is lowest
            for (int c = 0; c < pile.Count && c < DeckBuilder.Deck.MAX_HAND_CAPACITY * 2; c++)
            {
                for (int i = 0; i < DrawConstants.BUTTON_GLOW_NUM_STEPS; i++)
                {
                    sprites.Draw(Game1.pic_functionality_cardDown,
                        new Rectangle(deck._x + DrawConstants.COMBAT_DRAW_PILE_BUFFER + (int)((double)c * 0.25) - DrawConstants.BUTTON_GLOW_FURTHEST + DrawConstants.BUTTON_GLOW_STEP * i,
                        yFromBottom(deck._y + DrawConstants.COMBAT_DRAW_PILE_BUFFER + c * 2 - DrawConstants.BUTTON_GLOW_FURTHEST + DrawConstants.BUTTON_GLOW_STEP * i,
                        DrawConstants.COMBAT_CARDDOWN_HEIGHT + DrawConstants.BUTTON_GLOW_FURTHEST * 2 - DrawConstants.BUTTON_GLOW_STEP * i * 2),
                        DrawConstants.COMBAT_CARDDOWN_WIDTH + DrawConstants.BUTTON_GLOW_FURTHEST * 2 - DrawConstants.BUTTON_GLOW_STEP * i * 2,
                        DrawConstants.COMBAT_CARDDOWN_HEIGHT + DrawConstants.BUTTON_GLOW_FURTHEST * 2 - DrawConstants.BUTTON_GLOW_STEP * i * 2),
                        Color.PowderBlue * DrawConstants.BUTTON_GLOW_OPACITY);
                }
            }
        }

        private static void drawButton(SpriteBatch sprites, UserInterface.Clickables.Button button)
        {
            sprites.Draw(button.getTexture(), new Rectangle(button._x, yFromBottom(button._y, button._height), button._width, button._height), Color.White);
            
            if (button == Game1.getHoveredClickable() && button.getHoverDescription() != null)
            {
                drawInfoBox(sprites, button.getHoverDescription(), new Rectangle(button._x, button._y, button._width, button._height));
            }
        }

        public static void drawUI_glowButton(SpriteBatch sprites, UserInterface.Clickables.Button button)
        {
            for (int i = 0; i < DrawConstants.BUTTON_GLOW_NUM_STEPS; i++)
            {
                sprites.Draw(button.getTexture(),
                    new Rectangle(button._x - DrawConstants.BUTTON_GLOW_FURTHEST + DrawConstants.BUTTON_GLOW_STEP * i,
                    yFromBottom(button._y - DrawConstants.BUTTON_GLOW_FURTHEST + DrawConstants.BUTTON_GLOW_STEP * i,
                    button._height + DrawConstants.BUTTON_GLOW_FURTHEST * 2 - DrawConstants.BUTTON_GLOW_STEP * i * 2),
                    button._width + DrawConstants.BUTTON_GLOW_FURTHEST * 2 - DrawConstants.BUTTON_GLOW_STEP * i * 2,
                    button._height + DrawConstants.BUTTON_GLOW_FURTHEST * 2 - DrawConstants.BUTTON_GLOW_STEP * i * 2),
                    Color.PowderBlue * DrawConstants.BUTTON_GLOW_OPACITY);
            }
        }

        public static void drawEnemy(SpriteBatch sprites, UserInterface.Clickables.Opponent enemy)
        {
            sprites.Draw(enemy.getEnemy()._texture, new Rectangle(enemy._x, yFromBottom(enemy._y, enemy._height), enemy._width, enemy._height), Color.White);

            //Draw name
            if (enemy == Game1.getHoveredClickable())
            {
                drawShadowedText(sprites, Game1.roboto_medium_12, enemy.getEnemy().getName(),
                    enemy._x + enemy._width / 2, enemy._y + enemy._height + DrawConstants.COMBAT_ENEMY_NAME_BUFFER, DrawConstants.TEXT_12_HEIGHT, Color.White, Color.Black);
            }

            //Draw intents here despite the AI being handled elsewhere
            List<Combat.AIPattern.intent> intents = enemy.getEnemy().getAIPattern().getIntents();
            if (intents.Contains(Combat.AIPattern.intent.DEFEND))
            {
                sprites.Draw(Game1.pic_functionality_intent_Defend, new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER, DrawConstants.COMBAT_INTENTS_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_WIDTH, DrawConstants.COMBAT_INTENTS_HEIGHT), Color.White);
            }
            if (intents.Contains(Combat.AIPattern.intent.BUFF))
            {
                sprites.Draw(Game1.pic_functionality_intent_Buff, new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2 + DrawConstants.COMBAT_INTENTS_BUFF_XADJUST,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER, DrawConstants.COMBAT_INTENTS_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_WIDTH, DrawConstants.COMBAT_INTENTS_HEIGHT), Color.White);
            }
            if (intents.Contains(Combat.AIPattern.intent.DEBUFF))
            {
                sprites.Draw(Game1.pic_functionality_intent_Debuff, new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2 + DrawConstants.COMBAT_INTENTS_DEBUFF_XADJUST,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER, DrawConstants.COMBAT_INTENTS_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_WIDTH, DrawConstants.COMBAT_INTENTS_HEIGHT), Color.White);
            }
            if (intents.Contains(Combat.AIPattern.intent.ATTACK))
            {
                sprites.Draw(Game1.pic_functionality_intent_Attack, new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER, DrawConstants.COMBAT_INTENTS_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_WIDTH, DrawConstants.COMBAT_INTENTS_HEIGHT), Color.White);

                drawShadowedText(sprites, Game1.roboto_bold_16, Game1.getChamp().calculateDamageIntakeFromEnemyAttack(enemy.getEnemy().getAIPattern().getIntendedDamage()).ToString(),
                    enemy._x + enemy._width / 2, enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER + DrawConstants.COMBAT_INTENTS_HEIGHT + DrawConstants.COMBAT_INTENTS_DAMAGE_BUFFER,
                    DrawConstants.TEXT_16_HEIGHT, Color.Red, Color.Black);
            }
            if (intents.Contains(Combat.AIPattern.intent.AOE))
            {
                sprites.Draw(Game1.pic_functionality_intent_Attack, new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2 + DrawConstants.COMBAT_INTENTS_AOE_TOTALWIDTH / 2,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER, DrawConstants.COMBAT_INTENTS_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_WIDTH, DrawConstants.COMBAT_INTENTS_HEIGHT), Color.White);

                sprites.Draw(Game1.pic_functionality_intent_AoE,
                    new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2 - DrawConstants.COMBAT_INTENTS_AOE_TOTALWIDTH / 2,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER + DrawConstants.COMBAT_INTENTS_AOE_YBUFFER, DrawConstants.COMBAT_INTENTS_AOE_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_AOE_WIDTH, DrawConstants.COMBAT_INTENTS_AOE_HEIGHT), Color.White);
                sprites.Draw(Game1.pic_functionality_intent_AoE,
                    new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2 - DrawConstants.COMBAT_INTENTS_AOE_TOTALWIDTH / 2 + DrawConstants.COMBAT_INTENTS_AOE_WIDTH + DrawConstants.COMBAT_INTENTS_AOE_BUFFER,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER + DrawConstants.COMBAT_INTENTS_AOE_YBUFFER, DrawConstants.COMBAT_INTENTS_AOE_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_AOE_WIDTH, DrawConstants.COMBAT_INTENTS_AOE_HEIGHT), Color.White);
                sprites.Draw(Game1.pic_functionality_intent_AoE,
                    new Rectangle(enemy._x + enemy._width / 2 - DrawConstants.COMBAT_INTENTS_WIDTH / 2 - DrawConstants.COMBAT_INTENTS_AOE_TOTALWIDTH / 2 + DrawConstants.COMBAT_INTENTS_AOE_WIDTH * 2 + DrawConstants.COMBAT_INTENTS_AOE_BUFFER * 2,
                    yFromBottom(enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER + DrawConstants.COMBAT_INTENTS_AOE_YBUFFER, DrawConstants.COMBAT_INTENTS_AOE_HEIGHT),
                    DrawConstants.COMBAT_INTENTS_AOE_WIDTH, DrawConstants.COMBAT_INTENTS_AOE_HEIGHT), Color.White);

                drawShadowedText(sprites, Game1.roboto_bold_16, Game1.getChamp().calculateDamageIntakeFromEnemyAttack(enemy.getEnemy().getAIPattern().getIntendedDamage()).ToString(),
                    enemy._x + enemy._width / 2, enemy._y + enemy._height + DrawConstants.COMBAT_INTENTS_BUFFER + DrawConstants.COMBAT_INTENTS_HEIGHT + DrawConstants.COMBAT_INTENTS_DAMAGE_BUFFER,
                    DrawConstants.TEXT_16_HEIGHT, Color.Red, Color.Black);
            }
        }

        public static void drawHPBar(SpriteBatch sprites, UserInterface.Clickables.Hovers.HPBar hpBar)
        {
            int defense = hpBar.getUnit().getDefense();
            Point anchor = new Point();
            
            //Draw defense: layer around HP bar
            if (defense > 0)
            {
                anchor.Y = hpBar._y + hpBar._height / 2 - DrawConstants.COMBAT_DEFENSE_ICON_HEIGHT / 2;

                //Anchor it to the left or right of the hp bar depending on where it is on the screen (make it towards center screen)
                anchor.X = hpBar._x - DrawConstants.COMBAT_DEFENSE_ICON_WIDTH + DrawConstants.COMBAT_DEFENSE_ICON_HPBAR_OVERLAP;
                if (hpBar._x + hpBar._width < Game1.VIRTUAL_WINDOW_WIDTH / 2)
                {
                    anchor.X = hpBar._x + hpBar._width - DrawConstants.COMBAT_DEFENSE_ICON_HPBAR_OVERLAP;
                }

                sprites.Draw(Game1.pic_functionality_bar,
                    new Rectangle(hpBar._x - DrawConstants.COMBAT_ENEMY_DEFENSE_BUFFER, yFromBottom(hpBar._y - DrawConstants.COMBAT_ENEMY_DEFENSE_BUFFER,
                    hpBar._height + DrawConstants.COMBAT_ENEMY_DEFENSE_BUFFER * 2),
                    hpBar._width + DrawConstants.COMBAT_ENEMY_DEFENSE_BUFFER * 2, hpBar._height + DrawConstants.COMBAT_ENEMY_DEFENSE_BUFFER * 2), Color.White);
            }

            //Draw max HP behind - adjust the height slightly for a neat effect
            sprites.Draw(Game1.pic_functionality_bar,
                new Rectangle(hpBar._x, yFromBottom(hpBar._y + 1, hpBar._height - 2),
                hpBar._width, hpBar._height - 2), Color.SlateGray);

            //Draw HP
            double hpPercent = (double)hpBar.getUnit().getCurrentHP() / (double)hpBar.getUnit().getMaxHP();
            sprites.Draw(Game1.pic_functionality_bar,
                new Rectangle(hpBar._x, yFromBottom(hpBar._y, hpBar._height),
                (int)((double)hpBar._width * hpPercent), hpBar._height), hpBar.getBarColor());

            //Draw defense icon & number
            if (defense > 0)
            {
                sprites.Draw(Game1.pic_functionality_defenseIcon,
                    new Rectangle(anchor.X, yFromBottom(anchor.Y, DrawConstants.COMBAT_DEFENSE_ICON_HEIGHT),
                    DrawConstants.COMBAT_DEFENSE_ICON_WIDTH, DrawConstants.COMBAT_DEFENSE_ICON_HEIGHT), Color.White);
                
                drawShadowedText(sprites, Game1.roboto_bold_12, defense.ToString(), anchor.X + DrawConstants.COMBAT_DEFENSE_ICON_WIDTH / 2,
                    anchor.Y + DrawConstants.COMBAT_DEFENSE_ICON_HEIGHT / 2 - DrawConstants.TEXT_12_HEIGHT / 2, DrawConstants.TEXT_12_HEIGHT, Color.Black, Color.White);
            }

            //Draw hover info & exact numbers
            if (hpBar == Game1.getHoveredClickable()) //FIXIT: or there's an option enabled to always display HP numbers
            {
                String hpText = "(" + hpBar.getUnit().getCurrentHP() + "/" + hpBar.getUnit().getMaxHP() + ")";
                drawShadowedText(sprites, Game1.roboto_medium_12, hpText,
                    hpBar._x + hpBar._width / 2, hpBar._y, DrawConstants.TEXT_12_HEIGHT, Color.White, Color.Black);

                drawInfoBox(sprites, hpBar.getDescription(), new Rectangle(hpBar._x, hpBar._y, hpBar._width, hpBar._height));
            }
        }

        public static void drawEnemyIntent(SpriteBatch sprites, UserInterface.Clickables.Hovers.EnemyIntent enemyIntent)
        {
            //Only draw info; the rest is handled in drawEnemy
            if (enemyIntent == Game1.getHoveredClickable())
            {
                drawInfoBox(sprites, enemyIntent.getDescription(), new Rectangle(enemyIntent._x + DrawConstants.COMBAT_INTENTS_WIDTH, enemyIntent._y, DrawConstants.COMBAT_INTENTS_WIDTH, enemyIntent._height));
            }
        }

        public static void drawChampion(SpriteBatch sprites, UserInterface.Clickables.Avatar champUI, Characters.Champion champ)
        {
            sprites.Draw(Game1.pic_functionality_championSilhouette, new Rectangle(champUI._x, yFromBottom(champUI._y, champUI._height), champUI._width, champUI._height), Color.White);

            //Draw name
            if (champUI == Game1.getHoveredClickable())
            {
                drawShadowedText(sprites, Game1.roboto_medium_12, champ.getName(), //FIXIT option for pronouns always displayed with name?
                    champUI._x + champUI._width / 2, champUI._y + champUI._height + DrawConstants.COMBAT_ENEMY_NAME_BUFFER, DrawConstants.TEXT_12_HEIGHT, Color.White, Color.Black);
            }
        }

        public static void drawTarget(SpriteBatch sprites, UserInterface.Clickables.Target target)
        {
            float fadeFade = DrawConstants.COMBAT_TARGET_FADE_FADE;
            float backFade = DrawConstants.COMBAT_TARGET_BACK_FADE;

            if (target == Game1.getHoveredClickable())
            {
                fadeFade = DrawConstants.COMBAT_TARGET_FADE_FADE_HOVERED;
                backFade = DrawConstants.COMBAT_TARGET_BACK_FADE_HOVERED;
            }

            sprites.Draw(Game1.pic_functionality_targeting_back_TL, target.getTopLeftTargetPiece(), Color.White * backFade);
            sprites.Draw(Game1.pic_functionality_targeting_back_TR, target.getTopRightTargetPiece(), Color.White * backFade);
            sprites.Draw(Game1.pic_functionality_targeting_back_BR, target.getBottomRightTargetPiece(), Color.White * backFade);
            sprites.Draw(Game1.pic_functionality_targeting_back_BL, target.getBottomLeftTargetPiece(), Color.White * backFade);

            sprites.Draw(Game1.pic_functionality_targeting_faded_TL, target.getTopLeftTargetPiece(), Color.White * fadeFade);
            sprites.Draw(Game1.pic_functionality_targeting_faded_TR, target.getTopRightTargetPiece(), Color.White * fadeFade);
            sprites.Draw(Game1.pic_functionality_targeting_faded_BR, target.getBottomRightTargetPiece(), Color.White * fadeFade);
            sprites.Draw(Game1.pic_functionality_targeting_faded_BL, target.getBottomLeftTargetPiece(), Color.White * fadeFade);
        }

        public static void drawResource(SpriteBatch sprites, UserInterface.Clickables.Hovers.Resource resource)
        {
            sprites.DrawString(Game1.roboto_bold_16, resource.getResourceText(),
                new Vector2(resource._x - 1, yFromBottom(resource._y - 1, DrawConstants.TEXT_16_HEIGHT)), Color.Black);
            sprites.DrawString(Game1.roboto_bold_16, resource.getResourceText(),
                new Vector2(resource._x - 1, yFromBottom(resource._y + 1, DrawConstants.TEXT_16_HEIGHT)), Color.Black);
            sprites.DrawString(Game1.roboto_bold_16, resource.getResourceText(),
                new Vector2(resource._x + 1, yFromBottom(resource._y - 1, DrawConstants.TEXT_16_HEIGHT)), Color.Black);
            sprites.DrawString(Game1.roboto_bold_16, resource.getResourceText(),
                new Vector2(resource._x + 1, yFromBottom(resource._y + 1, DrawConstants.TEXT_16_HEIGHT)), Color.Black);

            sprites.DrawString(Game1.roboto_bold_16, resource.getResourceText(),
                new Vector2(resource._x, yFromBottom(resource._y, DrawConstants.TEXT_16_HEIGHT)), resource.getColor());

            //Info box if hovered
            if (resource == Game1.getHoveredClickable())
            {
                drawInfoBox(sprites, resource.getDescription(), new Rectangle(resource._x, resource._y, resource._width, resource._height));
            }
        }

        public void drawMapGrid(SpriteBatch sprites, UserInterface.Clickables.MapGrid room)
        {
            if (room.getRoom().isRevealed())
            {
                sprites.Draw(Game1.pic_functionality_mapRoom, new Rectangle(room._x, yFromBottom(room._y, room._height), room._width, room._height), Color.Black);

                if (Game1.getHoveredClickable() == room)
                {
                    if (room.getHighlighted())
                    {
                        sprites.Draw(Game1.pic_functionality_mapRoom, new Rectangle(room._x, yFromBottom(room._y, room._height), room._width, room._height), Color.DeepSkyBlue);
                        sprites.Draw(Game1.pic_functionality_bar, new Rectangle(room._x, yFromBottom(room._y, room._height), room._width, room._height), Color.DeepSkyBlue * DrawConstants.MAP_GRIDSPACE_HIGHLIGHT_FADE);
                    }
                }

                List<Dungeon.Room.roomContents> roomContents = room.getRoom().getRoomContents();
                if (roomContents != null)
                {
                    int centering = ((roomContents.Count + 1) % 2);
                    bool centerSwitched = false;
                    for (int i = roomContents.Count - 1; i >= 0; i--)
                    {
                        if (!centerSwitched && i < roomContents.Count / 2)
                        {
                            centering = -centering;
                            centerSwitched = true;
                        }

                        switch (roomContents[i])
                        {
                            case Dungeon.Room.roomContents.story:
                            case Dungeon.Room.roomContents.happening: //FIXIT?
                                sprites.Draw(Game1.pic_functionality_mapStoryIcon,
                                    new Rectangle(room._x + DrawConstants.MAP_GRIDSPACE_WIDTH / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH / 2 + centering + centering * (roomContents.Count - 1) * 2,
                                    yFromBottom(room._y + DrawConstants.MAP_GRIDSPACE_HEIGHT / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT / 2 - centering - centering * (roomContents.Count - 1) * 2,
                                    DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), Color.White);
                                break;
                            case Dungeon.Room.roomContents.combat:
                                sprites.Draw(Game1.pic_functionality_mapCombatIcon,
                                    new Rectangle(room._x + DrawConstants.MAP_GRIDSPACE_WIDTH / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH / 2 + centering + centering * (roomContents.Count - 1) * 2,
                                    yFromBottom(room._y + DrawConstants.MAP_GRIDSPACE_HEIGHT / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT / 2 - centering - centering * (roomContents.Count - 1) * 2,
                                    DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), Color.Black);
                                break;
                            case Dungeon.Room.roomContents.treasure:
                                sprites.Draw(Game1.pic_functionality_mapTreasureIcon,
                                    new Rectangle(room._x + DrawConstants.MAP_GRIDSPACE_WIDTH / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH / 2 + centering + centering * (roomContents.Count - 1) * 2,
                                    yFromBottom(room._y + DrawConstants.MAP_GRIDSPACE_HEIGHT / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT / 2 - centering - centering * (roomContents.Count - 1) * 2,
                                    DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), Color.Black);
                                break;
                            case Dungeon.Room.roomContents.key:
                                sprites.Draw(Game1.pic_functionality_mapKeyIcon,
                                    new Rectangle(room._x + DrawConstants.MAP_GRIDSPACE_WIDTH / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH / 2 + centering + centering * (roomContents.Count - 1) * 2,
                                    yFromBottom(room._y + DrawConstants.MAP_GRIDSPACE_HEIGHT / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT / 2 - centering - centering * (roomContents.Count - 1) * 2,
                                    DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT),
                                    Treasury.Equipment.Key.getKeyColor(room.getRoom().getRoomKey().getKeyColor()));
                                break;
                            case Dungeon.Room.roomContents.exit:
                                sprites.Draw(Game1.pic_functionality_mapExitIcon,
                                    new Rectangle(room._x + DrawConstants.MAP_GRIDSPACE_WIDTH / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH / 2 + centering + centering * (roomContents.Count - 1) * 2,
                                    yFromBottom(room._y + DrawConstants.MAP_GRIDSPACE_HEIGHT / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT / 2 - centering - centering * (roomContents.Count - 1) * 2,
                                    DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), Color.Black);
                                break;
                        }
                    }
                }

                if (Game1.getHoveredClickable() == room)
                {
                    //FIXIT info box for room contents?
                }

                if (Game1.getDungeonHandler().getPlayerLocation() == room.getGridLocation())
                {
                    sprites.Draw(Game1.pic_functionality_mapChampLoc, new Rectangle(room._x + DrawConstants.MAP_GRIDSPACE_WIDTH / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH / 2,
                        yFromBottom(room._y + DrawConstants.MAP_GRIDSPACE_HEIGHT / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT / 2, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT),
                        DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT), Color.White);

                    sprites.Draw(Game1.pic_functionality_mapChampLoc, new Rectangle(room._x + DrawConstants.MAP_GRIDSPACE_WIDTH / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH / 2 - _pulse,
                        yFromBottom(room._y + DrawConstants.MAP_GRIDSPACE_HEIGHT / 2 - DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT / 2 - _pulse, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT + _pulse * 2),
                        DrawConstants.MAP_ROOM_CONTENTS_ICON_WIDTH + _pulse * 2, DrawConstants.MAP_ROOM_CONTENTS_ICON_HEIGHT + _pulse * 2),
                        Color.White * (DrawConstants.MAP_ROOM_PLAYER_PULSE_FADE - (float)_pulseAtMax * DrawConstants.MAP_ROOM_PLAYER_PULSE_FADE_STEP_AT_MAX));
                }

                //North connector
                if (room.getRoom().getConnector(Dungeon.Connector.direction.north) != null)
                {
                    drawConnector(sprites, room, Dungeon.Connector.direction.north);
                }
                //East connector
                if (room.getRoom().getConnector(Dungeon.Connector.direction.east) != null)
                {
                    drawConnector(sprites, room, Dungeon.Connector.direction.east);
                }
                //South connector
                if (room.getRoom().getConnector(Dungeon.Connector.direction.south) != null)
                {
                    drawConnector(sprites, room, Dungeon.Connector.direction.south);
                }
                //West connector
                if (room.getRoom().getConnector(Dungeon.Connector.direction.west) != null)
                {
                    drawConnector(sprites, room, Dungeon.Connector.direction.west);
                }
            }
            else if (room.getRoom().isPartialRevealed())
            {
                sprites.Draw(Game1.pic_functionality_mapRoom, new Rectangle(room._x, yFromBottom(room._y, room._height), room._width, room._height), Color.LightGray);

                if (Game1.getHoveredClickable() == room)
                {
                    if (room.getHighlighted())
                    {
                        sprites.Draw(Game1.pic_functionality_mapRoom, new Rectangle(room._x, yFromBottom(room._y, room._height), room._width, room._height), Color.DeepSkyBlue);
                        sprites.Draw(Game1.pic_functionality_bar, new Rectangle(room._x, yFromBottom(room._y, room._height), room._width, room._height), Color.DeepSkyBlue * DrawConstants.MAP_GRIDSPACE_HIGHLIGHT_FADE);
                    }
                }

                sprites.DrawString(Game1.roboto_black_24, "?",
                    new Vector2(room._x + room._width / 2 - Game1.roboto_black_24.MeasureString("?").X / 2,
                    yFromBottom(room._y + room._height / 2 - DrawConstants.TEXT_24_HEIGHT / 2, DrawConstants.TEXT_24_HEIGHT)), Color.Gray);
            }
        }

        public static void drawConnector(SpriteBatch sprites, UserInterface.Clickables.MapGrid room, Dungeon.Connector.direction dir)
        {
            //Currently every connector will be drawn twice unless the room on the other side isn't fully revealed, but that's fine for now

            Dungeon.Connector connector = room.getRoom().getConnector(dir);

            int xOffset = 0;
            int yOffset = 0;
            int width = DrawConstants.MAP_GRIDSPACE_WIDTH;
            int height = DrawConstants.MAP_GRIDSPACE_HEIGHT;
            bool horizontal = true;
            Texture2D connectorTexture = Game1.pic_functionality_mapConnectorH;

            switch (dir)
            {
                case Dungeon.Connector.direction.north:
                    yOffset = DrawConstants.MAP_GRIDSPACE_HEIGHT;
                    height = DrawConstants.MAP_GRID_CONNECTOR_SPACING;
                    horizontal = false;
                    connectorTexture = Game1.pic_functionality_mapConnectorV;
                    break;
                case Dungeon.Connector.direction.east:
                    xOffset = DrawConstants.MAP_GRIDSPACE_WIDTH;
                    width = DrawConstants.MAP_GRID_CONNECTOR_SPACING;
                    break;
                case Dungeon.Connector.direction.south:
                    yOffset = -DrawConstants.MAP_GRID_CONNECTOR_SPACING;
                    height = DrawConstants.MAP_GRID_CONNECTOR_SPACING;
                    horizontal = false;
                    connectorTexture = Game1.pic_functionality_mapConnectorV;
                    break;
                case Dungeon.Connector.direction.west:
                    xOffset = -DrawConstants.MAP_GRID_CONNECTOR_SPACING;
                    width = DrawConstants.MAP_GRID_CONNECTOR_SPACING;
                    break;
            }

            switch (connector.getConnectorType())
            {
                case Dungeon.Connector.connectorType.open:
                    sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.Black);

                    if (horizontal)
                    {
                        connectorTexture = Game1.pic_functionality_mapOpenConnectorH;
                    }
                    else
                    {
                        connectorTexture = Game1.pic_functionality_mapOpenConnectorV;
                    }
                    sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.DarkGray);
                    break;
                case Dungeon.Connector.connectorType.window:
                    if (horizontal)
                    {
                        connectorTexture = Game1.pic_functionality_mapConnectorWindowH;
                    }
                    else
                    {
                        connectorTexture = Game1.pic_functionality_mapConnectorWindowV;
                    }
                    sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.DarkGray);
                    break;
                case Dungeon.Connector.connectorType.closed:
                    sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.Black);

                    if (horizontal)
                    {
                        connectorTexture = Game1.pic_functionality_mapConnectorDoorH;
                    }
                    else
                    {
                        connectorTexture = Game1.pic_functionality_mapConnectorDoorV;
                    }
                    sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.DarkGray);
                    break;
                case Dungeon.Connector.connectorType.locked:
                    if (connector.canTraverse())
                    {
                        sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.Black);
                    }
                    else
                    {
                        sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height),
                            Treasury.Equipment.Key.getKeyColor(((Dungeon.Connectors.LockedDoor)connector).getLockColor()));
                    }

                    Texture2D connectorKey = Game1.pic_functionality_mapConnectorKeyH;
                    if (horizontal)
                    {
                        connectorTexture = Game1.pic_functionality_mapConnectorDoorH;
                    }
                    else
                    {
                        connectorTexture = Game1.pic_functionality_mapConnectorDoorV;
                        connectorKey = Game1.pic_functionality_mapConnectorKeyV;
                    }
                    sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.DarkGray);

                    if (!connector.canTraverse())
                    {
                        sprites.Draw(connectorKey, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height),
                            Treasury.Equipment.Key.getKeyColor(((Dungeon.Connectors.LockedDoor)connector).getLockColor()));
                    }
                    break;
                case Dungeon.Connector.connectorType.none:
                    //Draw nothing
                    break;
                default:
                    sprites.Draw(connectorTexture, new Rectangle(room._x + xOffset, yFromBottom(room._y + yOffset, height), width, height), Color.Red);
                    Game1.addToErrorLog("drawConnector connectorType is not yet implemented for drawing: " + connector.getConnectorType().ToString());
                    break;
            }
        }

        public static void drawDynamicText(SpriteBatch sprites, UserInterface.Clickables.Hovers.DynamicText dynamicText)
        {
            drawShadowedText(sprites, dynamicText.getFont(), dynamicText.getDisplayText(), dynamicText._x + dynamicText._width / 2, dynamicText._y, dynamicText._height,
                dynamicText.getColor(), dynamicText.getShadowColor());

            //Info box if hovered
            if (dynamicText == Game1.getHoveredClickable())
            {
                drawInfoBox(sprites, dynamicText.getDescription(), new Rectangle(dynamicText._x, dynamicText._y, dynamicText._width, dynamicText._height));
            }
        }

        public static void drawDecision(SpriteBatch sprites, UserInterface.Clickables.Decision decision)
        {
            //If hovered, glow
            if (Game1.getHoveredClickable() == decision)
            {
                for (int i = 0; i < DrawConstants.CHOICE_GLOW_NUM_STEPS; i++)
                {
                    sprites.Draw(Game1.pic_functionality_bar,
                        new Rectangle(decision._x - DrawConstants.CHOICE_GLOW_FURTHEST + DrawConstants.CHOICE_GLOW_STEP * i,
                        yFromBottom(decision._y - DrawConstants.CHOICE_GLOW_FURTHEST + DrawConstants.CHOICE_GLOW_STEP * i,
                        decision._height + DrawConstants.CHOICE_GLOW_FURTHEST * 2 - DrawConstants.CHOICE_GLOW_STEP * i * 2),
                        decision._width + DrawConstants.CHOICE_GLOW_FURTHEST * 2 - DrawConstants.CHOICE_GLOW_STEP * i * 2,
                        decision._height + DrawConstants.CHOICE_GLOW_FURTHEST * 2 - DrawConstants.CHOICE_GLOW_STEP * i * 2),
                        Color.LightYellow * DrawConstants.CHOICE_GLOW_OPACITY);
                }
            }

            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(decision._x, yFromBottom(decision._y, decision._height), decision._width, decision._height),
                new Color(DrawConstants.EVENT_CHOICE_BACKGROUND_RED, DrawConstants.EVENT_CHOICE_BACKGROUND_GREEN, DrawConstants.EVENT_CHOICE_BACKGROUND_BLUE));
            
            EventTextConverting.drawLineOfText(sprites, new Point(decision._x + DrawConstants.EVENT_CHOICE_TEXT_X_BUFFER, decision._y + decision._height / 2 - DrawConstants.TEXT_12_HEIGHT / 2),
                decision.getChoice().getText());
        }

        public static void drawLootableTreasure(SpriteBatch sprites, UserInterface.Clickables.LootableTreasure treasure)
        {
            float backFade = DrawConstants.LOOTMENU_TREASURE_UNSELECT_FADE;

            if (Game1.getHoveredClickable() == treasure)
            {
                //Draw glow
                for (int i = 0; i < DrawConstants.BUTTON_GLOW_NUM_STEPS; i++)
                {
                    sprites.Draw(Game1.pic_functionality_bar,
                        new Rectangle(treasure._x - DrawConstants.LOOT_GLOW_FURTHEST + DrawConstants.LOOT_GLOW_STEP * i,
                        yFromBottom(treasure._y - DrawConstants.LOOT_GLOW_FURTHEST + DrawConstants.LOOT_GLOW_STEP * i,
                        treasure._height + DrawConstants.LOOT_GLOW_FURTHEST * 2 - DrawConstants.LOOT_GLOW_STEP * i * 2),
                        treasure._width + DrawConstants.LOOT_GLOW_FURTHEST * 2 - DrawConstants.LOOT_GLOW_STEP * i * 2,
                        treasure._height + DrawConstants.LOOT_GLOW_FURTHEST * 2 - DrawConstants.LOOT_GLOW_STEP * i * 2),
                        Color.Gold * DrawConstants.LOOT_GLOW_OPACITY);
                }

                backFade = DrawConstants.LOOTMENU_TREASURE_SELECT_FADE;
            }

            //Draw background
            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(treasure._x, yFromBottom(treasure._y, treasure._height), treasure._width, treasure._height), Color.PowderBlue * backFade);

            //Draw icon
            sprites.Draw(treasure.getTreasure().getIcon(), new Rectangle(treasure._x + DrawConstants.LOOTMENU_TREASURE_ICON_BUFFER,
                yFromBottom(treasure._y + DrawConstants.LOOTMENU_TREASURE_ICON_BUFFER, DrawConstants.LOOTMENU_TREASURE_ICON_HEIGHT),
                DrawConstants.LOOTMENU_TREASURE_ICON_WIDTH, DrawConstants.LOOTMENU_TREASURE_ICON_HEIGHT), treasure.getTreasure().getIconColor());

            //Draw text
            sprites.DrawString(Game1.roboto_regular_20, treasure.getTreasure().getTreasureText(),
                new Vector2(treasure._x + DrawConstants.LOOTMENU_TREASURE_ICON_BUFFER * 2 + DrawConstants.LOOTMENU_TREASURE_ICON_WIDTH + 1,
                yFromBottom(treasure._y + treasure._height / 2 - DrawConstants.TEXT_20_HEIGHT / 2 + 1, DrawConstants.TEXT_20_HEIGHT)), Color.Black);
            sprites.DrawString(Game1.roboto_regular_20, treasure.getTreasure().getTreasureText(),
                new Vector2(treasure._x + DrawConstants.LOOTMENU_TREASURE_ICON_BUFFER * 2 + DrawConstants.LOOTMENU_TREASURE_ICON_WIDTH + 1,
                yFromBottom(treasure._y + treasure._height / 2 - DrawConstants.TEXT_20_HEIGHT / 2 - 1, DrawConstants.TEXT_20_HEIGHT)), Color.Black);
            sprites.DrawString(Game1.roboto_regular_20, treasure.getTreasure().getTreasureText(),
                new Vector2(treasure._x + DrawConstants.LOOTMENU_TREASURE_ICON_BUFFER * 2 + DrawConstants.LOOTMENU_TREASURE_ICON_WIDTH - 1,
                yFromBottom(treasure._y + treasure._height / 2 - DrawConstants.TEXT_20_HEIGHT / 2 + 1, DrawConstants.TEXT_20_HEIGHT)), Color.Black);
            sprites.DrawString(Game1.roboto_regular_20, treasure.getTreasure().getTreasureText(),
                new Vector2(treasure._x + DrawConstants.LOOTMENU_TREASURE_ICON_BUFFER * 2 + DrawConstants.LOOTMENU_TREASURE_ICON_WIDTH - 1,
                yFromBottom(treasure._y + treasure._height / 2 - DrawConstants.TEXT_20_HEIGHT / 2 - 1, DrawConstants.TEXT_20_HEIGHT)), Color.Black);

            sprites.DrawString(Game1.roboto_regular_20, treasure.getTreasure().getTreasureText(),
                new Vector2(treasure._x + DrawConstants.LOOTMENU_TREASURE_ICON_BUFFER * 2 + DrawConstants.LOOTMENU_TREASURE_ICON_WIDTH,
                yFromBottom(treasure._y + treasure._height / 2 - DrawConstants.TEXT_20_HEIGHT / 2, DrawConstants.TEXT_20_HEIGHT)), Color.White);
        }

        public static void drawCardChoice(SpriteBatch sprites, UserInterface.Clickables.CardChoice card, Characters.Champion champ, bool glowCard = false)
        {
            drawCard(sprites, card.getCard(), new Rectangle(card._x, card._y, card._width, card._height), Game1.roboto_bold_15, DrawConstants.TEXT_15_HEIGHT,
                Game1.roboto_medium_12, DrawConstants.TEXT_12_HEIGHT, Game1.roboto_black_18, DrawConstants.TEXT_18_HEIGHT, DrawConstants.CARDSELECTIONMENU_CARD_NAME_AREA_WIDTH,
                DrawConstants.CARDSELECTIONMENU_CARD_NAME_Y_BUFFER, DrawConstants.CARDSELECTIONMENU_CARDS_DIVINITY_ADJUST_X, DrawConstants.CARDSELECTIONMENU_CARDS_COST_ADJUST_Y_FROMTOP,
                DrawConstants.CARDSELECTIONMENU_CARDS_COST_ICON_SIZE, DrawConstants.CARDSELECTIONMENU_CARDS_BLOOD_ADJUST_X, champ, card == Game1.getHoveredClickable(), false, glowCard);
        }

        public static void drawAestheticOnly(SpriteBatch sprites, UserInterface.Clickables.AestheticOnly aesthetic)
        {
            sprites.Draw(aesthetic.getTexture(), new Rectangle(aesthetic._x, yFromBottom(aesthetic._y, aesthetic._height), aesthetic._width, aesthetic._height), aesthetic.getColor());
        }

        public static void drawMenuCard(SpriteBatch sprites, UserInterface.Clickables.MenuCard card, Characters.Champion champ, bool glowCard = false)
        {
            drawCard(sprites, card.getCard(), new Rectangle(card._x, card._y, card._width, card._height), Game1.roboto_bold_15, DrawConstants.TEXT_15_HEIGHT,
                Game1.roboto_medium_12, DrawConstants.TEXT_12_HEIGHT, Game1.roboto_black_18, DrawConstants.TEXT_18_HEIGHT, DrawConstants.CARDSELECTIONMENU_CARD_NAME_AREA_WIDTH,
                DrawConstants.CARDSELECTIONMENU_CARD_NAME_Y_BUFFER, DrawConstants.CARDSELECTIONMENU_CARDS_DIVINITY_ADJUST_X, DrawConstants.CARDSELECTIONMENU_CARDS_COST_ADJUST_Y_FROMTOP,
                DrawConstants.CARDSELECTIONMENU_CARDS_COST_ICON_SIZE, DrawConstants.CARDSELECTIONMENU_CARDS_BLOOD_ADJUST_X, champ, card == Game1.getHoveredClickable(), false, glowCard);
        }

        public static void drawMultiTarget(SpriteBatch sprites, UserInterface.Clickables.MultiTarget target)
        {
            float fadeFade = DrawConstants.COMBAT_TARGET_FADE_FADE;
            float backFade = DrawConstants.COMBAT_TARGET_BACK_FADE;

            if (target.isHovered())
            {
                fadeFade = DrawConstants.COMBAT_TARGET_FADE_FADE_HOVERED;
                backFade = DrawConstants.COMBAT_TARGET_BACK_FADE_HOVERED;
            }

            sprites.Draw(Game1.pic_functionality_targeting_back_TL, target.getTopLeftTargetPiece(), Color.White * backFade);
            sprites.Draw(Game1.pic_functionality_targeting_back_TR, target.getTopRightTargetPiece(), Color.White * backFade);
            sprites.Draw(Game1.pic_functionality_targeting_back_BR, target.getBottomRightTargetPiece(), Color.White * backFade);
            sprites.Draw(Game1.pic_functionality_targeting_back_BL, target.getBottomLeftTargetPiece(), Color.White * backFade);

            sprites.Draw(Game1.pic_functionality_targeting_faded_TL, target.getTopLeftTargetPiece(), Color.White * fadeFade);
            sprites.Draw(Game1.pic_functionality_targeting_faded_TR, target.getTopRightTargetPiece(), Color.White * fadeFade);
            sprites.Draw(Game1.pic_functionality_targeting_faded_BR, target.getBottomRightTargetPiece(), Color.White * fadeFade);
            sprites.Draw(Game1.pic_functionality_targeting_faded_BL, target.getBottomLeftTargetPiece(), Color.White * fadeFade);
        }

        public static void drawPartyMember(SpriteBatch sprites, UserInterface.Clickables.GroupMember partyMember)
        {
            sprites.Draw(Game1.pic_functionality_championSilhouette, new Rectangle(partyMember._x, yFromBottom(partyMember._y, partyMember._height),
                partyMember._width, partyMember._height), Color.White);

            //Draw name
            if (partyMember == Game1.getHoveredClickable())
            {
                drawShadowedText(sprites, Game1.roboto_medium_12, partyMember.getPartyMember().getName(),
                    partyMember._x + partyMember._width / 2, partyMember._y + partyMember._height + DrawConstants.COMBAT_PARTY_NAME_BUFFER, DrawConstants.TEXT_12_HEIGHT, Color.White, Color.Black);
            }
        }

        public static void drawStatusEffect(SpriteBatch sprites, UserInterface.Clickables.Hovers.StatusEffect statusEffect)
        {
            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(statusEffect._x, yFromBottom(statusEffect._y, statusEffect._height),
                statusEffect._width, statusEffect._height), statusEffect.getBorderColor());

            sprites.Draw(statusEffect.getIcon(), new Rectangle(statusEffect._x + DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER,
                yFromBottom(statusEffect._y + DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER, statusEffect._height - DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2),
                statusEffect._width - DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2, statusEffect._height - DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER * 2), Color.White);

            String importantBuffNumber = "" + statusEffect.getImportantBuffNumber();
            drawShadowedText(sprites, Game1.roboto_medium_11, importantBuffNumber,
                statusEffect._x + statusEffect._width / 2, statusEffect._y + DrawConstants.COMBAT_DEBUFF_BORDER_BUFFER, DrawConstants.TEXT_11_HEIGHT, statusEffect.getBorderColor(), Color.Black);

            //Draw info if hovered
            if (statusEffect == Game1.getHoveredClickable())
            {
                drawInfoBox(sprites, statusEffect.getDescription(), new Rectangle(statusEffect._x, statusEffect._y, statusEffect._width, statusEffect._height));
            }
        }

        public static void drawInventoryGrid(SpriteBatch sprites, UserInterface.Clickables.Hovers.InventoryGrid grid)
        {
            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(grid._x - Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER,
                yFromBottom(grid._y - Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER, grid._height + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2),
                grid._width + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2, grid._height + Drawing.DrawConstants.INVENTORYMENU_GRIDSPACE_BUFFER * 2), Color.White);

            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(grid._x, yFromBottom(grid._y, grid._height), grid._width, grid._height), grid.getGridColor());
        }

        public static void drawCollectedKey(SpriteBatch sprites, UserInterface.Clickables.Hovers.CollectedKey key)
        {
            sprites.Draw(Game1.pic_item_key, new Rectangle(key._x, yFromBottom(key._y, key._height),
                key._width, key._height), Treasury.Equipment.Key.getKeyColor(key.getKey().getKeyColor()));
            
            if (key == Game1.getHoveredClickable() && key.getDescription() != null && key.getDescription().Count > 0)
            {
                drawInfoBox(sprites, key.getDescription(), new Rectangle(key._x, key._y, key._width, key._height));
            }
        }



        /// <summary>
        /// Calculates the dimensions of the info box and draws it at the corner of the
        /// anchor (prefers top-right, but will adjust if that's off the screen edges).
        /// </summary>
        public static void drawInfoBox(SpriteBatch sprites, List<String> contents, Rectangle anchor)
        {
            if (contents.Count == 0)
            {
                return; //don't draw a box if there's no text
            }

            //Point anchorPos = Game1.getInputController().getMousePos();
            Point anchorPos = new Point(anchor.Right, anchor.Bottom); //uses bottom because the y's are flipped

            float totalText = -DrawConstants.INFO_BOX_BUFFER_BETWEEN_LINES; //negate the first addition of the buffer space
            float longestText = 0.0f;
            for (int i = 0; i < contents.Count; i++)
            {
                totalText += DrawConstants.TEXT_10_HEIGHT + DrawConstants.INFO_BOX_BUFFER_BETWEEN_LINES;
                float measureText = Game1.roboto_medium_10.MeasureString(contents[i]).X;
                if (measureText > longestText)
                {
                    longestText = measureText;
                }
            }

            int width = DrawConstants.INFO_BOX_BUFFER_EDGES * 2 + (int)longestText;
            int height = DrawConstants.INFO_BOX_BUFFER_EDGES * 2 + (int)totalText;

            //Check if the anchor needs to switch to the other sides
            if (anchorPos.X + width > Game1.VIRTUAL_WINDOW_WIDTH)
            {
                anchorPos.X = anchor.Left - width;
            }
            if (anchorPos.Y + height > Game1.VIRTUAL_WINDOW_HEIGHT)
            {
                anchorPos.Y = anchor.Top - height;
            }

            //Borders
            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(anchorPos.X - 1, yFromBottom(anchorPos.Y - 1, height + 2), width + 2, height + 2), Color.White * 0.5f);

            //Box
            sprites.Draw(Game1.pic_functionality_bar, new Rectangle(anchorPos.X, yFromBottom(anchorPos.Y, height), width, height), Color.Black * 0.75f);

            //Text
            for (int i = contents.Count - 1; i >= 0; i--)
            {
                sprites.DrawString(Game1.roboto_medium_10, contents[i],
                    new Vector2(anchorPos.X + width / 2 - Game1.roboto_medium_10.MeasureString(contents[i]).X / 2,
                    yFromBottom(anchorPos.Y + DrawConstants.INFO_BOX_BUFFER_EDGES + (contents.Count - 1 - i) * (DrawConstants.INFO_BOX_BUFFER_BETWEEN_LINES + DrawConstants.TEXT_10_HEIGHT), DrawConstants.TEXT_10_HEIGHT)), Color.White);
            }
        }

        public static void drawGameLogs(SpriteBatch sprites, List<String> log, Color textColor)
        {
            for (int i = 0; i < log.Count; i++)
            {
                sprites.DrawString(Game1.roboto_bold_10, log[log.Count - 1 - i],
                    new Vector2(DrawConstants.DEBUG_LOG_X - 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i - 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                sprites.DrawString(Game1.roboto_bold_10, log[log.Count - 1 - i],
                    new Vector2(DrawConstants.DEBUG_LOG_X - 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i + 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                sprites.DrawString(Game1.roboto_bold_10, log[log.Count - 1 - i],
                    new Vector2(DrawConstants.DEBUG_LOG_X + 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i - 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                sprites.DrawString(Game1.roboto_bold_10, log[log.Count - 1 - i],
                    new Vector2(DrawConstants.DEBUG_LOG_X + 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i + 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);

                sprites.DrawString(Game1.roboto_bold_10, log[log.Count - 1 - i],
                    new Vector2(DrawConstants.DEBUG_LOG_X, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i, DrawConstants.TEXT_10_HEIGHT)), textColor);
            }
        }

        

        /// <summary>
        /// My function for replicating shadowed text through the process of drawing the
        /// text five times - four times behind offset by 1 x & y, as the shadows - and
        /// one time in front. Text can be made to glow in this manner as well if black
        /// is not the color sent as the shadow.
        /// </summary>
        public static void drawShadowedText(SpriteBatch sprites, SpriteFont font, String text, int textXFromCenter, int y, int height, Color textColor, Color shadowColor)
        {
            sprites.DrawString(font, text,
                new Vector2(textXFromCenter - font.MeasureString(text).X / 2 + 1,
                yFromBottom(y + 1, height)), shadowColor);
            sprites.DrawString(font, text,
                new Vector2(textXFromCenter - font.MeasureString(text).X / 2 + 1,
                yFromBottom(y - 1, height)), shadowColor);
            sprites.DrawString(font, text,
                new Vector2(textXFromCenter - font.MeasureString(text).X / 2 - 1,
                yFromBottom(y + 1, height)), shadowColor);
            sprites.DrawString(font, text,
                new Vector2(textXFromCenter - font.MeasureString(text).X / 2 - 1,
                yFromBottom(y - 1, height)), shadowColor);

            sprites.DrawString(font, text,
                new Vector2(textXFromCenter - font.MeasureString(text).X / 2,
                yFromBottom(y, height)), textColor);
        }



        /// <summary>
        /// Since y is calculated from the top of the screen, this function is to quickly
        /// calculate the y from the bottom of the screen instead.
        /// </summary>
        public static int yFromBottom(int y, int height)
        {
            return Game1.VIRTUAL_WINDOW_HEIGHT - y - height;
        }

        public static int xFromRight(int x, int width)
        {
            return Game1.VIRTUAL_WINDOW_WIDTH - x - width;
        }
    }
}
