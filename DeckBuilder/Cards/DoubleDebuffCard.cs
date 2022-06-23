using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.DeckBuilder.Cards
{
    class DoubleDebuffCard : BasicDebuffCard
    {
        protected Combat.Buff.buffType _buffType2;
        protected int _duration2, _amount2;
        protected bool _hasDuration2, _hasAmount2;

        public DoubleDebuffCard(String name, CardEnums.CardType cardType, CardEnums.CardRarity rarity,
            Combat.Buff.buffType buffType, Combat.Buff.buffType buffType2,
            int duration, int duration2, int amount, int amount2,
            bool hasDuration, bool hasDuration2, bool hasAmount, bool hasAmount2) :
            base(name, cardType, rarity, buffType, duration, amount, hasDuration, hasAmount)
        {
            _buffType2 = buffType2;
            _duration2 = duration2;
            _amount2 = amount2;
            _hasDuration2 = hasDuration2;
            _hasAmount2 = hasAmount2;
        }

        public Combat.Buff.buffType buffType2
        {
            get => _buffType2;
        }
        public int duration2
        {
            get => _duration2;
        }
        public int amount2
        {
            get => _amount2;
        }
        public bool hasDuration2
        {
            get => _hasDuration2;
        }
        public bool hasAmount2
        {
            get => _hasAmount2;
        }

        public override void onPlay()
        {
            applyDebuff();
            applyDebuff2();
        }

        public virtual void applyDebuff2()
        {
            if (_target != null)
            {
                _target.gainBuff(new Combat.Buff(_buffType2, _duration2, _amount2, _hasDuration2, _hasAmount2));
            }
        }

        public override List<String> getDescription(Characters.Champion champ, bool activeCard = false)
        {
            List<String> desc = base.getDescription(champ);

            if (_hasDuration2 && !_hasAmount2)
            {
                desc.Add("Apply " + _duration2 + " " + Combat.Buff.buffString(buffType2) + ".");
            }
            else if (!_hasDuration2 && _hasAmount2)
            {
                desc.Add("Apply " + _amount2 + " " + Combat.Buff.buffString(buffType2) + ".");
            }
            else
            {
                desc.Add("Apply " + _amount2 + " " + Combat.Buff.buffString(buffType2));
                desc.Add("for " + _duration2 + " turns.");
            }

            return desc;
        }
    }
}
