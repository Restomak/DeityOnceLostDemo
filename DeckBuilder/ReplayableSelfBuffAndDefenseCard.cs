﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder
{
    class ReplayableSelfBuffAndDefenseCard : BasicReplayableSelfBuffCard, IDefendingCard
    {
        protected int _defense;

        public ReplayableSelfBuffAndDefenseCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int buffAmount, Combat.Unit.statType buffType, int defense) :
            base(name, cardType, rarity, buffAmount, buffType)
        {
            _defense = defense;
        }

        public int defense
        {
            get => _defense;
        }

        public override void onPlay()
        {
            applyBuff();
            gainDefense();
        }

        public void gainDefense()
        {
            Game1.getChamp().gainDefense(_defense);
        }

        public override List<String> getDescription(Characters.Champion champ)
        {
            List<String> desc = new List<string>();
            int defense = champ.getDexterity() + _defense;

            desc.Add("Gain " + _buffAmount + " " + _buffType.ToString() + ".");
            desc.Add("Gain " + defense + " defense.");

            return desc;
        }
    }
}
