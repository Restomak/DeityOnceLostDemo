using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Drawing
{
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

        public static void drawLineOfText(SpriteBatch sprites, Point drawStartLoc, String text)
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
                if (!piecesWithData[i]._shadow)
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

        private static List<String> trimToPieces(String text)
        {
            String[] pieces = text.Split('|');

            List<String> returnPieces = new List<string>();
            
            for (int i = 0; i < pieces.Length; i++)
            {
                returnPieces.Add(pieces[i]);
            }

            return returnPieces;
        }

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
                    case 16:
                        return Game1.roboto_black_16;
                    case 20:
                        return Game1.roboto_black_20;
                    case 24:
                        return Game1.roboto_black_24;
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
                    case 16:
                        return Game1.roboto_bold_16;
                    case 20:
                        return Game1.roboto_bold_20;
                    case 24:
                        return Game1.roboto_bold_24;
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
                    case 16:
                        return Game1.roboto_medium_16;
                    case 20:
                        return Game1.roboto_medium_20;
                    case 24:
                        return Game1.roboto_medium_24;
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
                    case 16:
                        return Game1.roboto_regular_16;
                    case 20:
                        return Game1.roboto_regular_20;
                    case 24:
                        return Game1.roboto_regular_24;
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

            Game1.addToErrorLog("Attempting to get Color from getColorFromText but " + piece + " not implemented");
            return color;
        }
    }
}
