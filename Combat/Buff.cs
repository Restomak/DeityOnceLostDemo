using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Combat
{
    public class Buff
    {
        public const double FEEBLE_MODIFIER = 0.75; //reduced by 0.25
        public const double SLUGGISH_MODIFIER = 0.75; //reduced by 0.25
        public const double VULNERABLE_MODIFIER = 1.5; //increased by 0.5

        public enum buffType
        {
            strength, //increases/lowers damage dealt per instance linearly
            dexterity, //increases/lowers defense gained per instance linearly
            resilience, //increases/lowers damage taken per instance linearly
            feeble, //deal a percentage less damage
            sluggish, //gain a percentage less defense
            vulnerable, //take a percentage more damage
            cardDraw, //increases/lowers card draw amount linearly
            divGain //increases/lowers divination at the start of the turn linearly
        }

        buffType _buffType;
        int _amount, _duration;
        bool _hasDuration, _hasAmount, _waitATurnBeforeDecrease;

        public Buff(buffType type, int duration, int amount, bool hasDuration, bool hasAmount, bool waitATurnBeforeDecrease = false)
        {
            _buffType = type;
            _duration = duration;
            _amount = amount; //For buffs that have a severity (eg. strength)
            _hasDuration = hasDuration;
            _hasAmount = hasAmount;
            _waitATurnBeforeDecrease = waitATurnBeforeDecrease;
        }

        public buffType getType()
        {
            return _buffType;
        }

        public int getDuration()
        {
            return _duration;
        }

        public int getAmount()
        {
            return _amount;
        }

        public bool hasDuration()
        {
            return _hasDuration;
        }

        public bool hasAmount()
        {
            return _hasAmount;
        }

        public bool waitATurnBeforeDecrease()
        {
            return _waitATurnBeforeDecrease;
        }

        public void waitedATurn()
        {
            _waitATurnBeforeDecrease = false;
        }

        public void clear()
        {
            _duration = 0;
        }

        public void affectDuration(int duration)
        {
            if (_hasDuration)
            {
                _duration += duration;
            }
        }

        public void affectAmount(int amount)
        {
            if (_hasAmount)
            {
                _amount += amount;
            }
        }



        public static List<Buff> addBuff(List<Buff> buffs, Buff newBuff)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                //Make sure they're the same
                if (buffs[i].getType() == newBuff.getType() && buffs[i].hasDuration() == newBuff.hasDuration() && buffs[i].hasAmount() == newBuff.hasAmount())
                {
                    //Combine them
                    if (newBuff.hasDuration())
                    {
                        buffs[i].affectDuration(newBuff.getDuration());
                    }
                    if (newBuff.hasAmount())
                    {
                        buffs[i].affectAmount(newBuff.getAmount());
                    }

                    //If they cancel out, remove them
                    if (newBuff.hasDuration() && buffs[i].getDuration() == 0 || newBuff.hasAmount() && buffs[i].getAmount() == 0)
                    {
                        buffs.RemoveAt(i);
                    }

                    return buffs; //exit as soon as we've found a match and dealt with it
                }
            }

            buffs.Add(newBuff);

            return buffs;
        }

        public static String buffString(buffType type)
        {
            switch (type)
            {
                case buffType.strength:
                    return "Strength";
                case buffType.dexterity:
                    return "Dexterity";
                case buffType.resilience:
                    return "Resilience";
                case buffType.feeble:
                    return "Feeble";
                case buffType.sluggish:
                    return "Sluggish";
                case buffType.vulnerable:
                    return "Vulnerable";
                case buffType.cardDraw:
                    return "Card Draw";
                case buffType.divGain:
                    return "Resilience";
                default:
                    Game1.addToErrorLog("buffString not yet implemented for new buffType: " + type.ToString());
                    return type.ToString();
            }
        }

        public static List<String> buffDescription(Buff buff)
        {
            List<String> description = new List<String>();

            switch (buff.getType())
            {
                case buffType.strength:
                    description.Add("Damage dealt ");
                    if (buff.getAmount() > 0)
                    {
                        description[0] += "increased";
                    }
                    else
                    {
                        description[0] += "decreased";
                    }
                    description.Add("by " + buff.getAmount() + ".");
                    break;
                case buffType.dexterity:
                    description.Add("Defense gain ");
                    if (buff.getAmount() > 0)
                    {
                        description[0] += "increased";
                    }
                    else
                    {
                        description[0] += "decreased";
                    }
                    description.Add("by " + buff.getAmount() + ".");
                    break;
                case buffType.resilience:
                    description.Add("Takes " + buff.getAmount());
                    if (buff.getAmount() > 0)
                    {
                        description[0] += " less damage";
                    }
                    else
                    {
                        description[0] += " more damage";
                    }
                    description.Add("on each attack taken.");
                    break;
                case buffType.feeble:
                    description.Add("Deal 25% less damage.");
                    description.Add(lastsXMoreTurns(buff.getDuration()));
                    break;
                case buffType.sluggish:
                    description.Add("Gain 25% less defense");
                    description.Add(lastsXMoreTurns(buff.getDuration()));
                    break;
                case buffType.vulnerable:
                    description.Add("Take 50% more damage.");
                    description.Add(lastsXMoreTurns(buff.getDuration()));
                    break;
                /*case buffType.cardDraw:
                    break;
                case buffType.divGain:
                    break;*/
                default:
                    Game1.addToErrorLog("buffDescription not yet implemented for new buffType: " + buff.getType().ToString());
                    description.Add("???");
                    break;
            }

            return description;
        }

        private static String lastsXMoreTurns(int duration)
        {
            String text = "Lasts " + duration + " more turn";
            if (duration > 1)
            {
                text += "s.";
            }
            else
            {
                text += ".";
            }

            return text;
        }

        public static Texture2D buffIcon(buffType type)
        {
            switch (type)
            {
                /*case buffType.strength:
                    return;
                case buffType.dexterity:
                    return;
                case buffType.resilience:
                    return;
                case buffType.feeble:
                    return;
                case buffType.sluggish:
                    return;
                case buffType.vulnerable:
                    return;
                case buffType.cardDraw:
                    return;
                case buffType.divGain:
                    return;*/
                default:
                    Game1.addToErrorLog("buffIcon not yet implemented for new buffType: " + type.ToString());
                    return Game1.pic_functionality_bar;
            }
        }

        public static Color buffBorderColor(Buff buff)
        {
            switch (buff.getType())
            {
                case buffType.strength:
                case buffType.dexterity:
                case buffType.resilience:
                case buffType.cardDraw:
                case buffType.divGain:
                    if (buff.getAmount() > 0)
                    {
                        return Color.LawnGreen;
                    }
                    else
                    {
                        return Color.Red;
                    }
                case buffType.feeble:
                case buffType.sluggish:
                case buffType.vulnerable:
                    return Color.Red;
                default:
                    Game1.addToErrorLog("buffOutlineColor not yet implemented for new buffType: " + buff.getType().ToString());
                    return Color.MonoGameOrange;
            }
        }
    }
}
