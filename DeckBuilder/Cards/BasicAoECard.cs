﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    class BasicAoECard : Card, IDamagingCard
    {
        protected int _damage;

        public BasicAoECard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, int damage) : base(name, cardType, rarity, CardEnums.TargetingType.aoeEnemies)
        {
            _damage = damage;
        }

        public int damage
        {
            get => _damage;
        }

        public override void onPlay()
        {
            dealDamage();
        }

        public void dealDamage()
        {
            List<Combat.Enemy> enemies = Game1.getCombatHandler().getCurrentEncounter().getEnemies();

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].takeDamage(Game1.getChamp().getDamageAffectedByBuffs(_damage, enemies[i]));
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<string>();
            int damage = champ.getDamageAffectedByBuffs(_damage, null);

            desc.Add("Deal " + damage + " damage to all");
            desc.Add("enemies.");

            return desc;
        }
    }
}
