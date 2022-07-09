using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    /// <summary>
    /// This class holds a collection of useful functions used in the getStylizedDescription
    /// function of cards.
    /// </summary>
    class CardStylizing
    {
        public static String dissipates(int descFontSize)
        {
            return "[f: " + descFontSize + " m][c: Darker Purple]Dissipates|[f: " + descFontSize + " m][c: Black].";
        }

        public static String basicDamageString(int normalDamage, int changedDamage, int descFontSize)
        {
            String damageString = "";

            if (changedDamage == normalDamage)
            {
                damageString += changedDamage + "|";
            }
            else if (changedDamage < normalDamage)
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + changedDamage + "|";
            }
            else //greater
            {
                damageString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + changedDamage + "|";
            }

            return "[f: " + descFontSize + " m]" + "[c: Black]Deal " + damageString + "[f: " + descFontSize + " m][c: Black]" + " damage.";
        }

        public static String basicDefenseString(int normalDefense, int changedDefense, int descFontSize)
        {
            String defenseString = "";

            if (changedDefense == normalDefense)
            {
                defenseString += changedDefense + "|";
            }
            else if (changedDefense < normalDefense)
            {
                defenseString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + changedDefense + "|";
            }
            else //greater
            {
                defenseString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + changedDefense + "|";
            }

            return "[f: " + descFontSize + " m]" + "[c: Black]Gain " + defenseString + "[f: " + descFontSize + " m][c: Black]" + " defense.";
        }

        public static String basicCardDrawString(int normalCardDraw, int changedCardDraw, int descFontSize)
        {
            String cardDrawString = "[f: " + descFontSize + " m]" + "[c: Black]Draw ";

            if (changedCardDraw == normalCardDraw)
            {
                cardDrawString += changedCardDraw;
            }
            else if (changedCardDraw < normalCardDraw)
            {
                cardDrawString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + changedCardDraw + "|[f: " + descFontSize + " m][c: Black]";
            }
            else //greater
            {
                cardDrawString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + changedCardDraw + "|[f: " + descFontSize + " m][c: Black]";
            }

            cardDrawString += " card";

            if (changedCardDraw > 1)
            {
                cardDrawString += "s";
            }
            cardDrawString += ".";

            return cardDrawString;
        }

        public static String basicStatBuffString(int normalStatBuffAmount, int changedStatBuffAmount, Combat.Unit.statType stat, int descFontSize)
        {
            String statString = "";

            if (changedStatBuffAmount == normalStatBuffAmount)
            {
                statString += changedStatBuffAmount;
            }
            else if (changedStatBuffAmount < normalStatBuffAmount)
            {
                statString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + changedStatBuffAmount + "|[f: " + descFontSize + " m]" + "[c: Black]";
            }
            else //greater
            {
                statString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + changedStatBuffAmount + "|[f: " + descFontSize + " m]" + "[c: Black]";
            }

            return "[f: " + descFontSize + " m]" + "[c: Black]Gain " + statString + " " + stat.ToString() + ".";
        }

        public static String basicApplyDebuffString(int normalDebuffDurationOrAmount, int changedDebuffDurationOrAmount, Combat.Buff.buffType buffType, int descFontSize)
        {
            String debuffString = "";

            if (changedDebuffDurationOrAmount == normalDebuffDurationOrAmount)
            {
                debuffString += changedDebuffDurationOrAmount;
            }
            else if (changedDebuffDurationOrAmount < normalDebuffDurationOrAmount)
            {
                debuffString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + changedDebuffDurationOrAmount + "|[f: " + descFontSize + " m]" + "[c: Black]";
            }
            else //greater
            {
                debuffString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + changedDebuffDurationOrAmount + "|[f: " + descFontSize + " m]" + "[c: Black]";
            }

            return "[f: " + descFontSize + " m]" + "[c: Black]Apply " + debuffString + " " + Combat.Buff.buffString(buffType) + ".";
        }

        public static String basicDivGainString(int normalDivGain, int changedDivGain, int descFontSize)
        {
            String divinityString = "";

            if (changedDivGain == normalDivGain)
            {
                divinityString += changedDivGain + "|";
            }
            else if (changedDivGain < normalDivGain)
            {
                divinityString += "|[f: " + descFontSize + " m][s: Black][c: Red]" + changedDivGain + "|";
            }
            else //greater
            {
                divinityString += "|[f: " + descFontSize + " m][s: Black][c: Lawn Green]" + changedDivGain + "|";
            }
            
            return "[f: " + descFontSize + " m]" + "[c: Black]Gain " + divinityString + "[f: " + descFontSize + " m][c: Black]" + " defense.";
        }
    }
}
