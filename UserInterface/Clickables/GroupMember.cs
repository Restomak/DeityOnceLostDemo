using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    /// <summary>
    /// Opponents are the Clickables used for displaying party members on screen. When initialized,
    /// will also set up their HP bar.
    /// </summary>
    class GroupMember : Clickable
    {
        Combat.PartyMember _partyMember;
        int _spotInParty;

        public GroupMember(Combat.PartyMember partyMember, int spotInParty)
        {
            _partyMember = partyMember;
            _spotInParty = spotInParty;

            switch (spotInParty)
            {
                case 0:
                    _x = Drawing.DrawConstants.COMBAT_PARTY_X_1;
                    break;
                case 1:
                    _x = Drawing.DrawConstants.COMBAT_PARTY_X_2;
                    break;
                case 2:
                    _x = Drawing.DrawConstants.COMBAT_PARTY_X_3;
                    break;
                default:
                    Game1.addToErrorLog("spotInParty in GroupMember constructor out of range (max: 2): " + spotInParty);
                    _x = 0;
                    break;
            }
            _y = Drawing.DrawConstants.COMBAT_PARTY_Y;
            _width = Drawing.DrawConstants.COMBAT_PARTY_WIDTH;
            _height = Drawing.DrawConstants.COMBAT_PARTY_HEIGHT;
        }

        public Combat.PartyMember getPartyMember()
        {
            return _partyMember;
        }



        /// <summary>
        /// Handles what happens in logic when the user hovers over one of the party
        /// members in combat. Their name will appear above their head.
        /// </summary>
        public override void onHover()
        {
            _hovered = true;
            Game1.setHoveredClickable(this);
        }

        /// <summary>
        /// Handles what happens when the user is no longer hovering over this object.
        /// </summary>
        public override void onHoverEnd()
        {
            _hovered = false;
            Game1.setHoveredClickable(null);
        }

        /// <summary>
        /// Handles what happens in logic when the user clicks on a party member in
        /// combat.
        /// </summary>
        public override void onClick()
        {
            //FIXIT implement
        }



        /// <summary>
        /// Sets up the party members in combat as a UserInterface so they're interactable
        /// </summary>
        public static void setupPartyUI(UserInterface ui, List<Combat.PartyMember> party, UserInterface hoverUI)
        {
            ui.resetClickables();
            hoverUI.resetClickables();

            for (int i = 0; i < party.Count; i++)
            {
                if (!party[i].getDowned())
                {
                    GroupMember partyMember = new GroupMember(party[i], i);

                    ui.addClickableToBack(partyMember); //the first party member is closest to the champion, so always add behind the line
                    setupPartyHoversUI(hoverUI, partyMember);
                }
            }
        }

        /// <summary>
        /// Sets up the rest of the Clickables that are a part of each party member:
        /// currently only the HP bar, but maybe eventually also an icon for their buff
        /// </summary>
        private static void setupPartyHoversUI(UserInterface hoverUI, GroupMember partyMember)
        {
            //HP bar
            Hovers.HPBar hpBar = new Hovers.HPBar(new Point(partyMember._x + Drawing.DrawConstants.COMBAT_PARTY_HP_WIDTHBUFFER,
                partyMember._y - Drawing.DrawConstants.COMBAT_PARTY_HP_BUFFER_TO_TOP - Drawing.DrawConstants.COMBAT_PARTY_HP_HEIGHT),
                partyMember._width - Drawing.DrawConstants.COMBAT_PARTY_HP_WIDTHBUFFER * 2, Drawing.DrawConstants.COMBAT_PARTY_HP_HEIGHT, partyMember.getPartyMember(), Hovers.HPBar.hpBarType.party);
            hoverUI.addClickableToBack(hpBar); //order doesn't matter
        }
    }
}
