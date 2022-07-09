using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.Treasury.Treasures
{
    /// <summary>
    /// Abstract base class for each of the six possible RestBlessings. These can only be
    /// obtained via the rest blessing option while resting using the Firewood item, and
    /// so some redundant things (such as retrieving hover info) are unnecessary.
    /// </summary>
    abstract class RestBlessing : Relic
    {
        public static List<RestBlessing> _restBlessings;

        public RestBlessing(Texture2D texture, String name, List<String> description) : base(texture, name, description, false, treasureType.blessing)
        {

        }

        public override Color getBorderColor()
        {
            return Color.Gold;
        }

        public abstract RestBlessing getNewRestBlessing();

        public static UserInterface.ExtraInfos.TextInfo getRestBlessingInfo()
        {
            return new UserInterface.ExtraInfos.TextInfo(new List<String>()
            { "Rest Blessing:",
              "One of the blessings",
              "that can be gained",
              "when resting."
            });
        }

        public override List<UserInterface.ExtraInfo> getHoverExtraInfo()
        {
            return null; //Not needed
        }



        /// <summary>
        /// Generates the collection list of each possible rest blessing to be used when
        /// the player is receiving one at random.
        /// </summary>
        public static void setupRestBlessings()
        {
            _restBlessings = new List<RestBlessing>();

            _restBlessings.Add(new Blessings.Rest.EaglesSlpendor());
            _restBlessings.Add(new Blessings.Rest.BullsStrength());
            _restBlessings.Add(new Blessings.Rest.CatsGrace());
            _restBlessings.Add(new Blessings.Rest.BoarsEndurance());
            _restBlessings.Add(new Blessings.Rest.FoxsCunning());
            _restBlessings.Add(new Blessings.Rest.OwlsWisdom());
        }
    }
}
