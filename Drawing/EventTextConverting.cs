using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Drawing
{
    /// <summary>
    /// The purpose of this class is a text parser and stylizer. It breaks apart lines of text
    /// sent to it and converts the stuff in square brackets into formatting (font, size, color,
    /// shadow color, and even names, pronouns, and HP percentages).
    /// </summary>
    class EventTextConverting
    {
        /*
         * SPECIAL TEXT - HOW IT WORKS
         * 
         * Separate lines with the | character. At the beginning of a new section, the
         * below tags will alter the text for that entire section.
         * 
         * Default text is in size 12 in regular format. To change the text into different sizes/formats:
         * [f: 12 bb] size 12, black
         * [f: 10 b] size 10, bold
         * [f: 16 m] size 16, medium
         * [f: 14 r] size 14, regular
         * etc.
         * 
         * Default text is /not/ shadowed. To change this:
         * [s: White] to white shadowed text
         * [s: Red] to red shadowed text
         * etc.
         * 
         * Default text is /white/. To change this:
         * [c: Blue] to blue
         * [c: Red] to red
         * etc.
         */
        
        /// <summary>
        /// Takes a line of text and sends it off to the various functions that prepare style,
        /// and the various pieces of the line are then drawn one at a time with the proper
        /// formatting.
        /// </summary>
        public static void drawLineOfText(SpriteBatch sprites, Point drawStartLoc, String text, bool grayOutText = false)
        {
            text = insertNames(text);
            text = insertPronouns(text);
            text = insertChampionHitpoints(text);

            List<String> pieces = trimToPieces(text);
            List<StringWithData> piecesWithData = new List<StringWithData>();

            for (int i = 0; i < pieces.Count; i++)
            {
                piecesWithData.Add(new StringWithData(pieces[i]));
            }

            int lineX = drawStartLoc.X;
            for (int i = 0; i < piecesWithData.Count; i++)
            {
                if (grayOutText)
                {
                    sprites.DrawString(piecesWithData[i]._font, piecesWithData[i]._text,
                        new Vector2(lineX, DrawHandler.yFromBottom(drawStartLoc.Y, (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).Y)), Color.DarkGray);
                }
                else if (!piecesWithData[i]._shadow)
                {
                    sprites.DrawString(piecesWithData[i]._font, piecesWithData[i]._text,
                        new Vector2(lineX, DrawHandler.yFromBottom(drawStartLoc.Y, (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).Y)), piecesWithData[i]._color);
                }
                else
                {
                    lineX += 2;

                    sprites.DrawString(piecesWithData[i]._font, piecesWithData[i]._text,
                        new Vector2(lineX + 1, DrawHandler.yFromBottom(drawStartLoc.Y + 1, (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).Y)), piecesWithData[i]._shadowColor);
                    sprites.DrawString(piecesWithData[i]._font, piecesWithData[i]._text,
                        new Vector2(lineX + 1, DrawHandler.yFromBottom(drawStartLoc.Y - 1, (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).Y)), piecesWithData[i]._shadowColor);
                    sprites.DrawString(piecesWithData[i]._font, piecesWithData[i]._text,
                        new Vector2(lineX - 1, DrawHandler.yFromBottom(drawStartLoc.Y + 1, (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).Y)), piecesWithData[i]._shadowColor);
                    sprites.DrawString(piecesWithData[i]._font, piecesWithData[i]._text,
                        new Vector2(lineX - 1, DrawHandler.yFromBottom(drawStartLoc.Y - 1, (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).Y)), piecesWithData[i]._shadowColor);

                    sprites.DrawString(piecesWithData[i]._font, piecesWithData[i]._text,
                        new Vector2(lineX, DrawHandler.yFromBottom(drawStartLoc.Y, (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).Y)), piecesWithData[i]._color);
                }

                lineX += (int)piecesWithData[i]._font.MeasureString(piecesWithData[i]._text).X;
            }
        }

        /// <summary>
        /// Replaces all name indicators with the associated names (see StoryConstants
        /// for a better description on the formatting required for this).
        /// </summary>
        private static String insertNames(String text)
        {
            if (Game1.getChamp() != null)
            {
                text = text.Replace("[Name: c]", Game1.getChamp().getName());
            }

            if (Game1.getPartyMembers() != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Game1.getPartyMembers().Count > i)
                    {
                        text = text.Replace("[Name: " + i + 1 + "]", Game1.getPartyMembers()[i].getName());
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Replaces all pronoun indicators with the associated pronouns (see StoryConstants
        /// for a better description on the formatting required for this).
        /// </summary>
        private static String insertPronouns(String text)
        {
            if (Game1.getChamp() != null)
            {
                text = text.Replace("[They: c]", Game1.getChamp().getHero().getPronoun_they());
                text = text.Replace("[Them: c]", Game1.getChamp().getHero().getPronoun_them());
                text = text.Replace("[Their: c]", Game1.getChamp().getHero().getPronoun_their());
                text = text.Replace("[Theirs: c]", Game1.getChamp().getHero().getPronoun_theirs());
                text = text.Replace("[They're: c]", Game1.getChamp().getHero().getPronoun_theyre());
                text = text.Replace("[They've: c]", Game1.getChamp().getHero().getPronoun_theyve());

                text = text.Replace("[they: c]", Game1.getChamp().getHero().getPronoun_they().ToLower());
                text = text.Replace("[them: c]", Game1.getChamp().getHero().getPronoun_them().ToLower());
                text = text.Replace("[their: c]", Game1.getChamp().getHero().getPronoun_their().ToLower());
                text = text.Replace("[theirs: c]", Game1.getChamp().getHero().getPronoun_theirs().ToLower());
                text = text.Replace("[they're: c]", Game1.getChamp().getHero().getPronoun_theyre().ToLower());
                text = text.Replace("[they've: c]", Game1.getChamp().getHero().getPronoun_theyve().ToLower());

                text = text.Replace("[THEY: c]", Game1.getChamp().getHero().getPronoun_they().ToUpper());
                text = text.Replace("[THEM: c]", Game1.getChamp().getHero().getPronoun_them().ToUpper());
                text = text.Replace("[THEIR: c]", Game1.getChamp().getHero().getPronoun_their().ToUpper());
                text = text.Replace("[THEIRS: c]", Game1.getChamp().getHero().getPronoun_theirs().ToUpper());
                text = text.Replace("[THEY'RE: c]", Game1.getChamp().getHero().getPronoun_theyre().ToUpper());
                text = text.Replace("[THEY'VE: c]", Game1.getChamp().getHero().getPronoun_theyve().ToUpper());

                if (Game1.getChamp().getHero().getPronoun_they() == Characters.HeroConstants.THEY_THEYTHEM)
                {
                    text = text.Replace("[s: c]", "");
                }
                else
                {
                    text = text.Replace("[s: c]", "s");
                }
            }

            if (Game1.getPartyMembers() != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Game1.getPartyMembers().Count > i)
                    {
                        text = text.Replace("[They: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_they());
                        text = text.Replace("[Them: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_them());
                        text = text.Replace("[Their: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_their());
                        text = text.Replace("[Theirs: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theirs());
                        text = text.Replace("[They're: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theyre());
                        text = text.Replace("[They've: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theyve());

                        text = text.Replace("[they: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_they().ToLower());
                        text = text.Replace("[them: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_them().ToLower());
                        text = text.Replace("[their: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_their().ToLower());
                        text = text.Replace("[theirs: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theirs().ToLower());
                        text = text.Replace("[they're: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theyre().ToLower());
                        text = text.Replace("[they've: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theyve().ToLower());

                        text = text.Replace("[THEY: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_they().ToUpper());
                        text = text.Replace("[THEM: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_them().ToUpper());
                        text = text.Replace("[THEIR: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_their().ToUpper());
                        text = text.Replace("[THEIRS: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theirs().ToUpper());
                        text = text.Replace("[THEY'RE: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theyre().ToUpper());
                        text = text.Replace("[THEY'VE: " + i + 1 + "]", Game1.getPartyMembers()[i].getPronoun_theyve().ToUpper());

                        if (Game1.getPartyMembers()[i].getPronoun_they() == Characters.HeroConstants.THEY_THEYTHEM)
                        {
                            text = text.Replace("[s: " + i + 1 + "]", "");
                        }
                        else
                        {
                            text = text.Replace("[s: " + i + 1 + "]", "s");
                        }
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Replaces all HP percentage indicators with the proper integer amount of HP (see
        /// StoryConstants for a better description on the formatting required for this).
        /// </summary>
        private static String insertChampionHitpoints(String text)
        {
            if (Game1.getChamp() != null)
            {
                bool done = false;
                while (!done)
                {
                    if (text.Contains("[HP: "))
                    {
                        int index = text.IndexOf("[HP: ");
                        text = text.Remove(index, 5);
                        int index2 = text.IndexOf("%]");
                        int percentage = Int32.Parse(text.Substring(index, index2 - index));
                        int hpAmount = (int)Math.Round((double)Game1.getChamp().getMaxHP() * ((double)percentage / 100.0));
                        text = text.Remove(index, index2 - index + 2);
                        text = text.Insert(index, hpAmount.ToString());
                    }
                    else
                    {
                        done = true;
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Splits the line of text into pieces based on the separator character |, which
        /// indicates a change in format for that piece.
        /// </summary>
        private static List<String> trimToPieces(String text)
        {
            String[] pieces = text.Split('|');

            List<String> returnPieces = new List<String>();
            
            for (int i = 0; i < pieces.Length; i++)
            {
                returnPieces.Add(pieces[i]);
            }

            return returnPieces;
        }

        /// <summary>
        /// A struct that is basically an improved-upon version of a String containing
        /// format data such as font (including boldness and size), text color, shadow
        /// color, and whether or not the text even should draw a shadow color.
        /// 
        /// The StringWithData is created before formatting by passing a String, and
        /// that string is sent off to the assignData function which converts the
        /// formatting from text to actual data stored in the struct.
        /// </summary>
        struct StringWithData
        {
            public StringWithData(String text)
            {
                _text = text;
                _font = Game1.roboto_regular_12;
                _shadow = false;
                _shadowColor = Color.Black;
                _color = Color.White;
                assignData();
            }

            public String _text { get; set; }
            public SpriteFont _font { get; set; }
            public bool _shadow { get; set; }
            public Color _shadowColor { get; set; }
            public Color _color { get; set; }

            private void assignData()
            {
                String[] pieces = _text.Split(']');

                if (pieces.Length > 1)
                {
                    for (int i = 0; i < pieces.Length; i++)
                    {
                        if (pieces[i].Contains("[f:"))
                        {
                            _font = getFontFromText(pieces[i]);
                        }
                        else if (pieces[i].Contains("[s:"))
                        {
                            _shadow = true;
                            _shadowColor = getColorFromText(pieces[i], true);
                        }
                        else if (pieces[i].Contains("[c:"))
                        {
                            _color = getColorFromText(pieces[i]);
                        }
                    }

                    _text = pieces[pieces.Length - 1];
                }
            }
        }
        
        /// <summary>
        /// Determines the appropriate font for the piece of text. See the top of this
        /// class for more information.
        /// </summary>
        public static SpriteFont getFontFromText(String piece)
        {
            SpriteFont font = Game1.roboto_medium_12;

            piece = piece.Remove(0, 4); //remove "[f: "
            String[] pieces = piece.Split(' ');
            int fontSize = Int32.Parse(pieces[0]);
            
            if (pieces[1] == "bb")
            {
                switch (fontSize)
                {
                    case 8:
                        return Game1.roboto_black_8;
                    case 10:
                        return Game1.roboto_black_10;
                    case 11:
                        return Game1.roboto_black_11;
                    case 12:
                        return Game1.roboto_black_12;
                    case 14:
                        return Game1.roboto_black_14;
                    case 15:
                        return Game1.roboto_black_15;
                    case 16:
                        return Game1.roboto_black_16;
                    case 18:
                        return Game1.roboto_black_18;
                    case 20:
                        return Game1.roboto_black_20;
                    case 21:
                        return Game1.roboto_black_21;
                    case 24:
                        return Game1.roboto_black_24;
                    case 28:
                        return Game1.roboto_black_28;
                    default:
                        Game1.addToErrorLog("Attempting to getFontFromText but the font size doesn't exist: " + fontSize);
                        break;
                }
            }
            else if (pieces[1] == "b")
            {
                switch (fontSize)
                {
                    case 8:
                        return Game1.roboto_bold_8;
                    case 10:
                        return Game1.roboto_bold_10;
                    case 11:
                        return Game1.roboto_bold_11;
                    case 12:
                        return Game1.roboto_bold_12;
                    case 14:
                        return Game1.roboto_bold_14;
                    case 15:
                        return Game1.roboto_bold_15;
                    case 16:
                        return Game1.roboto_bold_16;
                    case 18:
                        return Game1.roboto_bold_18;
                    case 20:
                        return Game1.roboto_bold_20;
                    case 21:
                        return Game1.roboto_bold_21;
                    case 24:
                        return Game1.roboto_bold_24;
                    case 28:
                        return Game1.roboto_bold_28;
                    default:
                        Game1.addToErrorLog("Attempting to getFontFromText but the font size doesn't exist: " + fontSize);
                        break;
                }
            }
            else if (pieces[1] == "m")
            {
                switch (fontSize)
                {
                    case 8:
                        return Game1.roboto_medium_8;
                    case 10:
                        return Game1.roboto_medium_10;
                    case 11:
                        return Game1.roboto_medium_11;
                    case 12:
                        return Game1.roboto_medium_12;
                    case 14:
                        return Game1.roboto_medium_14;
                    case 15:
                        return Game1.roboto_medium_15;
                    case 16:
                        return Game1.roboto_medium_16;
                    case 18:
                        return Game1.roboto_medium_18;
                    case 20:
                        return Game1.roboto_medium_20;
                    case 21:
                        return Game1.roboto_medium_20;
                    case 24:
                        return Game1.roboto_medium_24;
                    case 28:
                        return Game1.roboto_medium_20;
                    default:
                        Game1.addToErrorLog("Attempting to getFontFromText but the font size doesn't exist: " + fontSize);
                        break;
                }
            }
            else if (pieces[1] == "r")
            {
                switch (fontSize)
                {
                    case 8:
                        return Game1.roboto_regular_8;
                    case 10:
                        return Game1.roboto_regular_10;
                    case 11:
                        return Game1.roboto_regular_11;
                    case 12:
                        return Game1.roboto_regular_12;
                    case 14:
                        return Game1.roboto_regular_14;
                    case 15:
                        return Game1.roboto_regular_15;
                    case 16:
                        return Game1.roboto_regular_16;
                    case 18:
                        return Game1.roboto_regular_18;
                    case 20:
                        return Game1.roboto_regular_20;
                    case 21:
                        return Game1.roboto_regular_20;
                    case 24:
                        return Game1.roboto_regular_24;
                    case 28:
                        return Game1.roboto_regular_20;
                    default:
                        Game1.addToErrorLog("Attempting to getFontFromText but the font size doesn't exist: " + fontSize);
                        break;
                }
            }
            else
            {
                Game1.addToErrorLog("Attempting to getFontFromText but the tag for the font code isn't implemented: " + pieces[1]);
            }

            return font;
        }
        
        /// <summary>
        /// Determines the appropriate text color for the piece of text. See the top of
        /// this class for more information. Handles both text color and shadow color.
        /// </summary>
        public static Color getColorFromText(String piece, bool isShadow = false)
        {
            //Defaults
            Color color = Color.White;
            if (isShadow)
            {
                color = Color.Black;
            }

            piece = piece.Remove(0, 4); //remove "[c: " or "[s: "

            if (piece == "Powder Blue")
            {
                return Color.PowderBlue;
            }
            else if (piece == "Dark Purple")
            {
                return new Color(DrawConstants.DARK_PURPLE_RED, DrawConstants.DARK_PURPLE_GREEN, DrawConstants.DARK_PURPLE_BLUE);
            }
            else if (piece == "Purple")
            {
                return Color.Purple;
            }
            else if (piece == "Lawn Green")
            {
                return Color.LawnGreen;
            }
            else if (piece == "Red")
            {
                return Color.Red;
            }
            else if (piece == "Gold")
            {
                return Color.Gold;
            }
            else if (piece == "Gray")
            {
                return Color.Gray;
            }
            else if (piece == "Black")
            {
                return Color.Black;
            }
            else if (piece == "White")
            {
                return Color.White;
            }
            else if (piece == "Dark Goldenrod")
            {
                return Color.DarkGoldenrod;
            }
            else if (piece == "Light Gray")
            {
                return Color.LightGray;
            }
            else if (piece == "Dark Gray")
            {
                return Color.DarkGray;
            }
            else if (piece == "Dim Gray")
            {
                return Color.DimGray;
            }
            else if (piece == "Darker Gray")
            {
                return new Color(DrawConstants.DARKER_GRAY_RED, DrawConstants.DARKER_GRAY_GREEN, DrawConstants.DARKER_GRAY_BLUE);
            }
            else if (piece == "Dark Red")
            {
                return Color.DarkRed;
            }
            else if (piece == "Monogame Orange")
            {
                return Color.MonoGameOrange;
            }
            else if (piece == "Darker Red")
            {
                return new Color(DrawConstants.DARKER_RED_RED, DrawConstants.DARKER_RED_GREEN, DrawConstants.DARKER_RED_BLUE);
            }
            else if (piece == "Darker Purple")
            {
                return new Color(DrawConstants.DARK_PURPLE_RED, DrawConstants.DARK_PURPLE_GREEN, DrawConstants.DARK_PURPLE_BLUE);
            }
            else if (piece == "Green Yellow")
            {
                return Color.GreenYellow;
            }
            else if (piece == "Dark Olive Green")
            {
                return Color.DarkOliveGreen;
            }
            else if (piece == "Bright Purple")
            {
                return new Color(DrawConstants.BRIGHT_PURPLE_RED, DrawConstants.BRIGHT_PURPLE_GREEN, DrawConstants.BRIGHT_PURPLE_BLUE);
            }
            else if (piece == "Brown")
            {
                return Color.SandyBrown;
            }

            Game1.addToErrorLog("Attempting to get Color from getColorFromText but " + piece + " not implemented");
            return color;
        }
    }
}
