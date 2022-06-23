using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    class BasicDebuffCard : Card, IDebuffCard
    {
        protected Combat.Buff.buffType _buffType;
        protected int _duration, _amount;
        protected bool _hasDuration, _hasAmount;

        public BasicDebuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity, Combat.Buff.buffType buffType,
            int duration, int amount, bool hasDuration, bool hasAmount) :
            base(name, cardType, rarity, CardEnums.TargetingType.enemies)
        {
            _buffType = buffType;
            _duration = duration;
            _amount = amount;
            _hasDuration = hasDuration;
            _hasAmount = hasAmount;
        }

        public Combat.Buff.buffType buffType
        {
            get => _buffType;
        }
        public int duration
        {
            get => _duration;
        }
        public int amount
        {
            get => _amount;
        }
        public bool hasDuration
        {
            get => _hasDuration;
        }
        public bool hasAmount
        {
            get => _hasAmount;
        }

        public override void onPlay()
        {
            applyDebuff();
        }

        public virtual void applyDebuff()
        {
            if (_target != null)
            {
                _target.gainBuff(new Combat.Buff(_buffType, _duration, _amount, _hasDuration, _hasAmount));
            }
            else
            {
                Game1.errorLog.Add("Attempted to use debuff card without a target!");
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = new List<string>();

            if (_hasDuration && !_hasAmount)
            {
                desc.Add("Apply " + _duration + " " + Combat.Buff.buffString(buffType) + ".");
            }
            else if(!_hasDuration && _hasAmount)
            {
                desc.Add("Apply " + _amount + " " + Combat.Buff.buffString(buffType) + ".");
            }
            else
            {
                desc.Add("Apply " + _amount + " " + Combat.Buff.buffString(buffType));
                desc.Add("for " + _duration + " turns.");
            }

            return desc;
        }
    }
}
