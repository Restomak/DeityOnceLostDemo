using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters
{
    class HeroConstants
    {
        public enum Pronouns
        {
            sheHerHerHers,
            heHimHisHis,
            theyThemTheirTheirs,
            xeXirXem,
            VeVer
        }

        public const String THEY_SHEHER = "She";
        public const String THEM_SHEHER = "Her";
        public const String THEIR_SHEHER = "Her";
        public const String THEIRS_SHEHER = "Hers";
        public const String THEY_HEHIM = "He";
        public const String THEM_HEHIM = "Him";
        public const String THEIR_HEHIM = "His";
        public const String THEIRS_HEHIM = "His";
        public const String THEY_THEYTHEM = "They";
        public const String THEM_THEYTHEM = "Them";
        public const String THEIR_THEYTHEM = "Their";
        public const String THEIRS_THEYTHEM = "Theirs";
        public const String THEY_XEXIR = "Xe";
        public const String THEM_XEXIR = "Xir";
        public const String THEIR_XEXIR = "Xir";
        public const String THEIRS_XEXIR = "Xirs";
        public const String THEY_VEVER = "Ve";
        public const String THEM_VEVER = "Ver";
        public const String THEIR_VEVER = "Ver";
        public const String THEIRS_VEVER = "Vers";

        public const int PRONOUN_WEIGHT_SHE = 6;
        public const int PRONOUN_WEIGHT_HE = 6;
        public const int PRONOUN_WEIGHT_THEY = 5;
        public const int PRONOUN_WEIGHT_XE = 2;
        public const int PRONOUN_WEIGHT_VE = 2;

        public const int NUM_SKILL_CARDS_IN_DEFAULT_DECK = 5;
        public const int NUM_ATTACK_CARDS_IN_DEFAULT_DECK = 5;
        public const int NUM_OTHER_CARD_ROLLS_IN_DEFAULT_DECK = 2;
        public const int OTHER_CARD_CHANCE_IN_DEFAULT_DECK = 30;
        public const int MIN_TOTAL_NUM_CARDS_IN_DEFAULT_DECK = 14;
        public const int MAX_TOTAL_NUM_CARDS_IN_DEFAULT_DECK = 15;

        public const int HERO_MAX_HP_MIN = 21;
        public const int HERO_MAX_HP_MAX = 35;

        public const int CHANCE_AT_SPECIALIZED_DECK = 75;
        public const int CHANCE_AT_SPECIALIZED_CARD = 90;
    }
}
