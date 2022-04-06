using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Draw
{
    class DrawHandler
    {
        public const int MAX_SCREEN_X = Game1.VIRTUAL_WINDOW_WIDTH;
        public const int MAX_SCREEN_Y = Game1.VIRTUAL_WINDOW_HEIGHT;

        public const int COMBAT_ENDTURNBUTTON_X_FROMRIGHT = 220;
        public const int COMBAT_ENDTURNBUTTON_Y = 200;
        public const int COMBAT_ENDTURNBUTTON_WIDTH = 120;
        public const int COMBAT_ENDTURNBUTTON_HEIGHT = 50;

        public const int COMBAT_CARDDOWN_WIDTH = 86;
        public const int COMBAT_CARDDOWN_HEIGHT = 42;

        public const int COMBAT_DRAWPILE_X = 20;
        public const int COMBAT_DRAWPILE_Y = 20;

        public const int COMBAT_DISCARDPILE_X = 20;
        public const int COMBAT_DISCARDPILE_Y = 20;

        public const int COMBAT_REMOVEDCARDS_X = 20;
        public const int COMBAT_REMOVEDCARDS_Y = 80;

        public const int COMBAT_DIVINITY_AMOUNT_X = 250;
        public const int COMBAT_DIVINITY_AMOUNT_Y_FROMBOTTOM = 210;

        public const int TEXT_8_HEIGHT = 8;
        public const int TEXT_10_HEIGHT = 10;
        public const int TEXT_12_HEIGHT = 12;
        public const int TEXT_24_HEIGHT = 24;

        public const int COMBAT_HANDCARDS_X_FROMMIDLEFT = 500;
        public const int COMBAT_HANDCARDS_Y_FROMBOTTOM = -12;
        public const int COMBAT_HANDCARDS_AREAWIDTH = 1000;

        public const int COMBAT_HANDCARD_WIDTH = 141;
        public const int COMBAT_HANDCARD_HEIGHT = 195;

        public DrawHandler()
        {

        }

        public void drawCombat_Background(SpriteBatch sprites)
        {
            sprites.Draw(Game1.pic_functionality_uiSketch, new Rectangle(
                    0, 0, MAX_SCREEN_X, MAX_SCREEN_Y), Color.White);
        }

        public void drawCombat_UI(SpriteBatch sprites, Characters.Champion champ)
        {
            //End turn button
            sprites.Draw(Game1.pic_functionality_endTurnButton, new Rectangle(
                    xFromRight(COMBAT_ENDTURNBUTTON_X_FROMRIGHT, COMBAT_ENDTURNBUTTON_WIDTH), yFromBottom(COMBAT_ENDTURNBUTTON_Y, COMBAT_ENDTURNBUTTON_HEIGHT),
                    COMBAT_ENDTURNBUTTON_WIDTH, COMBAT_ENDTURNBUTTON_HEIGHT), Color.White);
            
            //Card piles
            drawPile(sprites, champ.getDeck().getDrawPile(), COMBAT_DRAWPILE_X, COMBAT_DRAWPILE_Y, false);
            drawPile(sprites, champ.getDeck().getDiscardPile(), COMBAT_DISCARDPILE_X, COMBAT_DISCARDPILE_Y, true);
            drawPile(sprites, champ.getDeck().getRemovedCards(), COMBAT_REMOVEDCARDS_X, COMBAT_REMOVEDCARDS_Y, true);

            //Divinity
            sprites.DrawString(Game1.roboto_medium_24, champ.getDivinity() + "/" + Characters.Champion.DEFAULT_DIVINITY,
                new Vector2(COMBAT_DIVINITY_AMOUNT_X, yFromBottom(COMBAT_DIVINITY_AMOUNT_Y_FROMBOTTOM, TEXT_24_HEIGHT)), Color.PowderBlue);
            if (champ.getDivinity() == 0)
            {
                sprites.DrawString(Game1.roboto_medium_24, "0",
                    new Vector2(COMBAT_DIVINITY_AMOUNT_X, yFromBottom(COMBAT_DIVINITY_AMOUNT_Y_FROMBOTTOM, TEXT_24_HEIGHT)), Color.Gray);
            }

            //Hand
            List<DeckBuilder.Card> hand = champ.getDeck().getHand();
            for (int i = 0; i < hand.Count; i++)
            {
                //Calculations
                int cardX = i * ((COMBAT_HANDCARDS_AREAWIDTH - COMBAT_HANDCARD_WIDTH) / (hand.Count - 1));
                String name = hand[i].getName();
                List<String> description = hand[i].getDescription(champ);

                Texture2D cardFromRarity = Game1.pic_card_front_default;
                switch (hand[i].getCardRarity())
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
                sprites.Draw(cardFromRarity, new Rectangle(MAX_SCREEN_X / 2 - COMBAT_HANDCARDS_X_FROMMIDLEFT + cardX, yFromBottom(COMBAT_HANDCARDS_Y_FROMBOTTOM, COMBAT_HANDCARD_HEIGHT),
                        COMBAT_HANDCARD_WIDTH, COMBAT_HANDCARD_HEIGHT), Color.White);

                //Draw card art

                //Draw card name
                sprites.DrawString(Game1.roboto_bold_10, name,
                    new Vector2(MAX_SCREEN_X / 2 - COMBAT_HANDCARDS_X_FROMMIDLEFT + cardX + COMBAT_HANDCARD_WIDTH / 2 - Game1.roboto_medium_10.MeasureString(name).X / 2,
                    yFromBottom(COMBAT_HANDCARDS_Y_FROMBOTTOM + COMBAT_HANDCARD_HEIGHT / 2 - COMBAT_HANDCARD_HEIGHT / 12, TEXT_10_HEIGHT)), Color.Black);

                //Draw card description
                for (int i_d = 0; i_d < description.Count; i_d++)
                {
                    sprites.DrawString(Game1.roboto_medium_8, description[i_d],
                        new Vector2(MAX_SCREEN_X / 2 - COMBAT_HANDCARDS_X_FROMMIDLEFT + cardX + COMBAT_HANDCARD_WIDTH / 2 - Game1.roboto_medium_8.MeasureString(description[i_d]).X / 2,
                        yFromBottom(COMBAT_HANDCARDS_Y_FROMBOTTOM + COMBAT_HANDCARD_HEIGHT / 2 - COMBAT_HANDCARD_HEIGHT / 6 - TEXT_8_HEIGHT * i_d * 3 / 2, TEXT_8_HEIGHT)), Color.Black);
                }
            }

            //temp top bar writing
            sprites.DrawString(Game1.roboto_medium_12, champ.getHero().getName() + " (" + champ.getHero().getPronoun_they() + "/" + champ.getHero().getPronoun_them() + ")", new Vector2(50, 10), Color.LawnGreen);
        }

        private void drawPile(SpriteBatch sprites, List<DeckBuilder.Card> pile, int x, int y, bool drawFromRight)
        {
            for (int i = 0; i < pile.Count && i < DeckBuilder.Deck.MAX_HAND_CAPACITY * 2; i++) //draw at most two full hand capacities' worth of cards, up until the number that are actually in the pile
            {
                if (drawFromRight)
                {
                    drawCard(sprites, x + (int)((double)i * 0.25), y + i * 2, drawFromRight);
                }
                else
                {
                    drawCard(sprites, x - (int)((double)i * 0.25), y + i * 2, drawFromRight);
                }
            }
        }

        private void drawCard(SpriteBatch sprites, int x, int y, bool drawFromRight)
        {
            int drawX = x;
            if (drawFromRight)
            {
                drawX = xFromRight(x, COMBAT_CARDDOWN_WIDTH);
            }

            sprites.Draw(Game1.pic_functionality_cardDown, new Rectangle(
                    drawX, yFromBottom(y, COMBAT_CARDDOWN_HEIGHT),
                    COMBAT_CARDDOWN_WIDTH, COMBAT_CARDDOWN_HEIGHT), Color.White);
        }

        public void drawCombatArea()
        {
            //Champion


            //Party Members


            //Enemies
        }

        

        public static int yFromBottom(int y, int height)
        {
            return MAX_SCREEN_Y - y - height;
        }
        public static int xFromRight(int x, int width)
        {
            return MAX_SCREEN_X - x - width;
        }
    }
}
