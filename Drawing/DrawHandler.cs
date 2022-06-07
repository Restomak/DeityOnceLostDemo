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
        //FIXIT split this into several smaller classes for readability

        public DrawHandler()
        {

        }

        public void drawCombat_Background(SpriteBatch sprites)
        {
            sprites.Draw(Game1.pic_functionality_uiSketch, new Rectangle(
                    0, 0, Game1.VIRTUAL_WINDOW_WIDTH, Game1.VIRTUAL_WINDOW_HEIGHT), Color.White);
        }

        public void drawUI(SpriteBatch sprites, List<UserInterface.UserInterface> activeUI, Characters.Champion champ)
        {
            //Backwards-iterate, since we're drawing from back of the screen to front
            for (int ui = activeUI.Count - 1; ui >= 0; ui--)
            {
                for (int ci = activeUI[ui].getClickables().Count - 1; ci >= 0; ci--)
                {
                    UserInterface.Clickable current = activeUI[ui].getClickables()[ci];

                    drawInterface(sprites, current, champ);
                }
            }
            /*
            //Divinity
            sprites.DrawString(Game1.roboto_medium_24, champ.getDivinity() + "/" + Characters.Champion.DEFAULT_DIVINITY,
                new Vector2(COMBAT_DIVINITY_AMOUNT_X, yFromBottom(COMBAT_DIVINITY_AMOUNT_Y_FROMBOTTOM, TEXT_24_HEIGHT)), Color.PowderBlue);
            if (champ.getDivinity() == 0)
            {
                sprites.DrawString(Game1.roboto_medium_24, "0",
                    new Vector2(COMBAT_DIVINITY_AMOUNT_X, yFromBottom(COMBAT_DIVINITY_AMOUNT_Y_FROMBOTTOM, TEXT_24_HEIGHT)), Color.Gray);
            }

            //temp top bar writing
            sprites.DrawString(Game1.roboto_medium_12, champ.getHero().getName() + " (" + champ.getHero().getPronoun_they() + "/" + champ.getHero().getPronoun_them() + ")", new Vector2(50, 10), Color.LawnGreen);
            */

            if (Game1.showDebugLog)
            {
                for (int i = 0; i < Game1.debugLog.Count; i++)
                {
                    sprites.DrawString(Game1.roboto_bold_10, Game1.debugLog[Game1.debugLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X - 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i - 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                    sprites.DrawString(Game1.roboto_bold_10, Game1.debugLog[Game1.debugLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X - 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i + 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                    sprites.DrawString(Game1.roboto_bold_10, Game1.debugLog[Game1.debugLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X + 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i - 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                    sprites.DrawString(Game1.roboto_bold_10, Game1.debugLog[Game1.debugLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X + 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i + 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);

                    sprites.DrawString(Game1.roboto_bold_10, Game1.debugLog[Game1.debugLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i, DrawConstants.TEXT_10_HEIGHT)), Color.White);
                }
            }
            else if (Game1.showErrorLog)
            {
                for (int i = 0; i < Game1.errorLog.Count; i++)
                {
                    sprites.DrawString(Game1.roboto_bold_10, Game1.errorLog[Game1.errorLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X - 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i - 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                    sprites.DrawString(Game1.roboto_bold_10, Game1.errorLog[Game1.errorLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X - 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i + 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                    sprites.DrawString(Game1.roboto_bold_10, Game1.errorLog[Game1.errorLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X + 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i - 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);
                    sprites.DrawString(Game1.roboto_bold_10, Game1.errorLog[Game1.errorLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X + 1, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i + 1, DrawConstants.TEXT_10_HEIGHT)), Color.Black);

                    sprites.DrawString(Game1.roboto_bold_10, Game1.errorLog[Game1.errorLog.Count - 1 - i],
                        new Vector2(DrawConstants.DEBUG_LOG_X, yFromBottom(DrawConstants.DEBUG_LOG_Y_START + DrawConstants.TEXT_12_HEIGHT * i, DrawConstants.TEXT_10_HEIGHT)), Color.Red);
                }
            }
        }

        /// <summary>
        /// Called twice and nearly last so that the currently-active HandCard is drawn on top. The first time it's
        /// called, glow should be true - then the second time should be false so the regular card is drawn on top.
        /// </summary>
        public void drawUI_LateActiveCard(SpriteBatch sprites, Characters.Champion champ, bool glow)
        {
            drawCombat_HandCard(sprites, Game1.getActiveCard(), champ, glow);
        }

        /// <summary>
        /// Called last so that the currently-hovered Clickable is drawn on top after any glows have been drawn
        /// </summary>
        public void drawUI_LateHover(SpriteBatch sprites, Characters.Champion champ)
        {
            drawInterface(sprites, Game1.getHoveredClickable(), champ);
        }

        private void drawInterface(SpriteBatch sprites, UserInterface.Clickable current, Characters.Champion champ)
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
                    Game1.errorLog.Add("drawUI attempting to draw DeckOfCards but typeOfDeck is not yet implemented: " + ((UserInterface.Clickables.DeckOfCards)current).getDeckType().ToString());
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
            else
            {
                Game1.errorLog.Add("drawUI attempting to draw from activeUI but typeof Clickable is not defined in if statement: " + current.GetType().ToString());
            }
        }

        private void drawCombat_HandCard(SpriteBatch sprites, UserInterface.Clickables.HandCard card, Characters.Champion champ, bool glowCard = false)
        {
            String name = card.getCard().getName();
            List<String> description = card.getCard().getDescription(champ);

            Texture2D cardFromRarity = Game1.pic_card_front_default;
            switch (card.getCard().getCardRarity())
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
                for (int i = 0; i < DrawConstants.HANDCARD_GLOW_NUM_STEPS; i++)
                {
                    sprites.Draw(cardFromRarity,
                        new Rectangle(card._x - DrawConstants.HANDCARD_GLOW_FURTHEST + DrawConstants.HANDCARD_GLOW_STEP * i,
                        yFromBottom(card._y - DrawConstants.HANDCARD_GLOW_FURTHEST + DrawConstants.HANDCARD_GLOW_STEP * i,
                        card._height + DrawConstants.HANDCARD_GLOW_FURTHEST * 2 - DrawConstants.HANDCARD_GLOW_STEP * i * 2),
                        card._width + DrawConstants.HANDCARD_GLOW_FURTHEST * 2 - DrawConstants.HANDCARD_GLOW_STEP * i * 2,
                        card._height + DrawConstants.HANDCARD_GLOW_FURTHEST * 2 - DrawConstants.HANDCARD_GLOW_STEP * i * 2),
                        Color.PowderBlue * DrawConstants.HANDCARD_GLOW_OPACITY);
                }
            }

            //Draw card background
            sprites.Draw(cardFromRarity, new Rectangle(card._x, yFromBottom(card._y, card._height), card._width, card._height), Color.White);
            
            //Draw card art


            //Draw card text
            if (card == Game1.getActiveCard() || card == Game1.getHoveredClickable())
            {
                //Nme
                sprites.DrawString(Game1.roboto_bold_20, name,
                    new Vector2(card._x + DrawConstants.COMBAT_HANDCARD_GROW_WIDTH / 2 - Game1.roboto_medium_20.MeasureString(name).X / 2,
                    yFromBottom(card._y + DrawConstants.COMBAT_HANDCARD_GROW_HEIGHT / 2 - DrawConstants.COMBAT_HANDCARD_GROW_HEIGHT / 12, DrawConstants.TEXT_20_HEIGHT)), Color.Black);

                //Description
                for (int i_d = 0; i_d < description.Count; i_d++)
                {
                    sprites.DrawString(Game1.roboto_medium_16, description[i_d],
                        new Vector2(card._x + DrawConstants.COMBAT_HANDCARD_GROW_WIDTH / 2 - Game1.roboto_medium_16.MeasureString(description[i_d]).X / 2,
                        yFromBottom(card._y + DrawConstants.COMBAT_HANDCARD_GROW_HEIGHT / 2 - DrawConstants.COMBAT_HANDCARD_GROW_HEIGHT / 6 - DrawConstants.TEXT_16_HEIGHT * i_d * 3 / 2, DrawConstants.TEXT_16_HEIGHT)), Color.Black);
                }
            }
            else
            {
                //Name
                sprites.DrawString(Game1.roboto_bold_10, name,
                    new Vector2(card._x + DrawConstants.COMBAT_HANDCARD_WIDTH / 2 - Game1.roboto_medium_10.MeasureString(name).X / 2,
                    yFromBottom(card._y + DrawConstants.COMBAT_HANDCARD_HEIGHT / 2 - DrawConstants.COMBAT_HANDCARD_HEIGHT / 12, DrawConstants.TEXT_10_HEIGHT)), Color.Black);

                //Description
                for (int i_d = 0; i_d < description.Count; i_d++)
                {
                    sprites.DrawString(Game1.roboto_medium_8, description[i_d],
                        new Vector2(card._x + DrawConstants.COMBAT_HANDCARD_WIDTH / 2 - Game1.roboto_medium_8.MeasureString(description[i_d]).X / 2,
                        yFromBottom(card._y + DrawConstants.COMBAT_HANDCARD_HEIGHT / 2 - DrawConstants.COMBAT_HANDCARD_HEIGHT / 6 - DrawConstants.TEXT_8_HEIGHT * i_d * 3 / 2, DrawConstants.TEXT_8_HEIGHT)), Color.Black);
                }
            }
        }

        private void drawCombat_CardPile(SpriteBatch sprites, UserInterface.Clickables.DeckOfCards deck, Characters.Champion champ)
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
                    //FIXIT implement
                    return; //don't perform the rest of the drawing; that's for the other types
                default:
                    Game1.errorLog.Add("drawCombat_CardPile deck typeOfDeck is not one that should be accessible in combat: " + deck.getDeckType().ToString());
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

        public void drawUI_GlowCardPile(SpriteBatch sprites, UserInterface.Clickables.DeckOfCards deck, Characters.Champion champ)
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
                default:
                    Game1.errorLog.Add("drawUI_GlowCardPile deck typeOfDeck is not one that should be accessible in this function: " + deck.getDeckType().ToString());
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

        private void drawButton(SpriteBatch sprites, UserInterface.Clickables.Button button)
        {
            sprites.Draw(button.getTexture(), new Rectangle(button._x, yFromBottom(button._y, button._height), button._width, button._height), Color.White);
            
            if (button == Game1.getHoveredClickable() && button.getHoverDescription() != null)
            {
                drawInfoBox(sprites, button.getHoverDescription(), new Rectangle(button._x, button._y, button._width, button._height));
            }
        }
        
        public void drawUI_glowButton(SpriteBatch sprites, UserInterface.Clickables.Button button)
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

        public void drawEnemy(SpriteBatch sprites, UserInterface.Clickables.Opponent enemy)
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

        public void drawHPBar(SpriteBatch sprites, UserInterface.Clickables.Hovers.HPBar hpBar)
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
                    new Rectangle(hpBar._x - 2, yFromBottom(hpBar._y - 2, hpBar._height + 4),
                    hpBar._width + 4, hpBar._height + 4), Color.White);
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

        public void drawEnemyIntent(SpriteBatch sprites, UserInterface.Clickables.Hovers.EnemyIntent enemyIntent)
        {
            //Only draw info; the rest is handled in drawEnemy
            if (enemyIntent == Game1.getHoveredClickable())
            {
                drawInfoBox(sprites, enemyIntent.getDescription(), new Rectangle(enemyIntent._x + DrawConstants.COMBAT_INTENTS_WIDTH, enemyIntent._y, DrawConstants.COMBAT_INTENTS_WIDTH, enemyIntent._height));
            }
        }

        public void drawChampion(SpriteBatch sprites, UserInterface.Clickables.Avatar champUI, Characters.Champion champ)
        {
            sprites.Draw(Game1.pic_functionality_championSilhouette, new Rectangle(champUI._x, yFromBottom(champUI._y, champUI._height), champUI._width, champUI._height), Color.White);

            //Draw name
            if (champUI == Game1.getHoveredClickable())
            {
                drawShadowedText(sprites, Game1.roboto_medium_12, champ.getName(), //FIXIT option for pronouns always displayed with name?
                    champUI._x + champUI._width / 2, champUI._y + champUI._height + DrawConstants.COMBAT_ENEMY_NAME_BUFFER, DrawConstants.TEXT_12_HEIGHT, Color.White, Color.Black);
            }
        }

        public void drawTarget(SpriteBatch sprites, UserInterface.Clickables.Target target)
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

        public void drawResource(SpriteBatch sprites, UserInterface.Clickables.Hovers.Resource resource)
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



        public void drawCombatArea()
        {
            //Champion


            //Party Members


            //Enemies
        }

        

        public void drawShadowedText(SpriteBatch sprites, SpriteFont font, String text, int textXFromCenter, int y, int height, Color textColor, Color shadowColor)
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

        public void drawInfoBox(SpriteBatch sprites, List<String> contents, Rectangle anchor)
        {
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
