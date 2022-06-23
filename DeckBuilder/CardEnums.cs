﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.DeckBuilder
{
    public static class CardEnums
    {
        public enum CostType
        {
            DIVINITY, //standard energy cost, resets every turn
            BLOOD, //health cost, run out and you die
            KARMA, //every threshold reached is a new bad thing, resets every combat
            SOUL //limited amount every run, increases as you gain story progress
        }
        public enum CardType //just StS typing for now
        {
            ATTACK,
            SKILL,
            HYBRID, //Attack and skill
            POWER,
            STATUS,
            CURSE
        }
        public enum CardRarity
        {
            DEFAULT,
            COMMON,
            RARE,
            EPIC,
            GODLY,
            VOID //curse cards etc
        }
        public enum TargetingType
        {
            enemies,
            champion,
            party,
            friendlies, //champion & party
            all, //anyone
            aoeEnemies, //all enemies at once
            aoeParty //all party members at once
        }

        public static String costTypeToString(CostType ct)
        {
            switch (ct)
            {
                case CostType.DIVINITY:
                    return "Divinity";
                case CostType.BLOOD:
                    return "Blood";
                case CostType.KARMA:
                    return "Karma";
                case CostType.SOUL:
                    return "Soul";
                default:
                    Game1.errorLog.Add("Card CostType error: " + ct.ToString());
                    return "ERROR";
            }
        }

        public static String cardTypeToString(CardType ct)
        {
            switch (ct)
            {
                case CardType.ATTACK:
                    return "Attack";
                case CardType.SKILL:
                    return "Skill";
                case CardType.POWER:
                    return "Power";
                case CardType.STATUS:
                    return "Status";
                case CardType.CURSE:
                    return "Curse";
                default:
                    Game1.errorLog.Add("Card CardType error: " + ct.ToString());
                    return "ERROR";
            }
        }

        public static String cardRarityToString(CardRarity ct)
        {
            switch (ct)
            {
                case CardRarity.DEFAULT:
                    return "Default";
                case CardRarity.COMMON:
                    return "Common";
                case CardRarity.RARE:
                    return "Rare";
                case CardRarity.EPIC:
                    return "Epic";
                case CardRarity.GODLY:
                    return "Godly";
                case CardRarity.VOID:
                    return "Void";
                default:
                    Game1.errorLog.Add("Card CardRarity error: " + ct.ToString());
                    return "ERROR";
            }
        }

        public static Color cardRarityToColor(CardRarity ct)
        {
            switch (ct)
            {
                case CardRarity.DEFAULT:
                    return Color.Gray;
                case CardRarity.COMMON:
                    return Color.Green;
                case CardRarity.RARE:
                    return Color.Blue;
                case CardRarity.EPIC:
                    return Color.Purple;
                case CardRarity.GODLY:
                    return Color.Gold;
                case CardRarity.VOID:
                    return new Color(Drawing.DrawConstants.DARK_PURPLE_RED, Drawing.DrawConstants.DARK_PURPLE_GREEN, Drawing.DrawConstants.DARK_PURPLE_BLUE);
                default:
                    Game1.errorLog.Add("Card CardRarity error: " + ct.ToString());
                    return Color.Red;
            }
        }
    }
}
