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
                            Game1.errorLog.Add("drawUI attempting to draw DeckOfCards (ui = " + ui + ", ci = " + ci + ") but typeOfDeck is not yet implemented: " + ((UserInterface.Clickables.DeckOfCards)current).getDeckType().ToString());
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
                    else
                    {
                        Game1.errorLog.Add("drawUI attempting to draw from activeUI (ui = " + ui + ", ci = " + ci + ") but typeof Clickable is not defined in if statement: " + current.GetType().ToString());
                    }
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
        }

        private void drawCombat_HandCard(SpriteBatch sprites, UserInterface.Clickables.HandCard card, Characters.Champion champ)
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
            
            //Draw card background
            sprites.Draw(cardFromRarity, new Rectangle(card._x, yFromBottom(card._y, card._height), card._width, card._height), Color.White);
            
            //Draw card art

            //Draw card name
            sprites.DrawString(Game1.roboto_bold_10, name,
                new Vector2(card._x + DrawConstants.COMBAT_HANDCARD_WIDTH / 2 - Game1.roboto_medium_10.MeasureString(name).X / 2,
                yFromBottom(card._y + DrawConstants.COMBAT_HANDCARD_HEIGHT / 2 - DrawConstants.COMBAT_HANDCARD_HEIGHT / 12, DrawConstants.TEXT_10_HEIGHT)), Color.Black);

            //Draw card description
            for (int i_d = 0; i_d < description.Count; i_d++)
            {
                sprites.DrawString(Game1.roboto_medium_8, description[i_d],
                    new Vector2(card._x + DrawConstants.COMBAT_HANDCARD_WIDTH / 2 - Game1.roboto_medium_8.MeasureString(description[i_d]).X / 2,
                    yFromBottom(card._y + DrawConstants.COMBAT_HANDCARD_HEIGHT / 2 - DrawConstants.COMBAT_HANDCARD_HEIGHT / 6 - DrawConstants.TEXT_8_HEIGHT * i_d * 3 / 2, DrawConstants.TEXT_8_HEIGHT)), Color.Black);
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

        private void drawButton(SpriteBatch sprites, UserInterface.Clickables.Button button)
        {
            sprites.Draw(button.getTexture(), new Rectangle(button._x, yFromBottom(button._y, button._height), button._width, button._height), Color.White);
        }

        public void drawEnemy(SpriteBatch sprites, UserInterface.Clickables.Opponent enemy)
        {
            sprites.Draw(enemy.getEnemy()._texture, new Rectangle(enemy._x, yFromBottom(enemy._y, enemy._height), enemy._width, enemy._height), Color.White);
            
            if (enemy == Game1.getHoveredClickable())
            {
                drawShadowedText(sprites, Game1.roboto_medium_12, enemy.getEnemy().getName(),
                    enemy._x + enemy._width / 2, enemy._y + enemy._height + DrawConstants.COMBAT_ENEMY_NAME_BUFFER, DrawConstants.TEXT_12_HEIGHT, Color.White, Color.Black);
            }

            //FIXIT draw intents
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
