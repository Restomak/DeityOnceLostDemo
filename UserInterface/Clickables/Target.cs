﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Clickables
{
    public class Target : Clickable
    {
        Clickable _clickableUnit;
        DeckBuilder.CardEnums.TargetingType _targetType;

        public Target(Clickable clickableUnit, DeckBuilder.CardEnums.TargetingType targetType)
        {
            _clickableUnit = clickableUnit;
            _targetType = targetType;

            _x = _clickableUnit._x;
            _y = _clickableUnit._y;
            _width = _clickableUnit._width;
            _height = _clickableUnit._height;
        }

        public Rectangle getTopLeftTargetPiece()
        {
            return new Rectangle(_x, _y - Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT * 2,
                Drawing.DrawConstants.COMBAT_TARGET_CORNER_WIDTH, Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT);
        }
        public Rectangle getTopRightTargetPiece()
        {
            return new Rectangle(_x + _width - Drawing.DrawConstants.COMBAT_TARGET_CORNER_WIDTH, _y - Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT * 2,
                Drawing.DrawConstants.COMBAT_TARGET_CORNER_WIDTH, Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT);
        }
        public Rectangle getBottomRightTargetPiece()
        {
            return new Rectangle(_x + _width - Drawing.DrawConstants.COMBAT_TARGET_CORNER_WIDTH, _y + _height - Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT,
                Drawing.DrawConstants.COMBAT_TARGET_CORNER_WIDTH, Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT);
        }
        public Rectangle getBottomLeftTargetPiece()
        {
            return new Rectangle(_x, _y + _height - Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT,
                Drawing.DrawConstants.COMBAT_TARGET_CORNER_WIDTH, Drawing.DrawConstants.COMBAT_TARGET_CORNER_HEIGHT);
        }



        /// <summary>
        /// Handles what happens in logic when the user hovers over the target. Makes
        /// it glow a different colour to signify that it's selectable.
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
        /// Handles what happens in logic when the user clicks on a target. Assuming the
        /// target is because of a HandCard, play the card!
        /// </summary>
        public override void onClick()
        {
            //First, check if there's an active HandCard (currently the only possibility but that may change in the future)
            if (Game1.getActiveCard() != null)
            {
                DeckBuilder.Card card = Game1.getActiveCard().getCard();

                //Set the card's target to what was just clicked on
                if (_clickableUnit.GetType() == typeof(Opponent))
                {
                    card.setTarget(((Opponent)_clickableUnit).getEnemy());
                }
                /*else if (_clickableUnit.GetType() == typeof(Party))
                {
                    card.setTarget(((Party)_clickableUnit).getPartyMember());
                }*/
                else if (_clickableUnit.GetType() == typeof(Avatar))
                {
                    card.setTarget(Game1.getChamp());
                }

                //Remove the appropriate card costs
                Game1.getChamp().spendDivinity(card.getPlayCost(DeckBuilder.CardEnums.CostType.DIVINITY));
                Game1.getChamp().takeDamage(card.getPlayCost(DeckBuilder.CardEnums.CostType.BLOOD), false);
                //FIXIT karma and soul costs for cards when implemented

                //Play the card
                card.onPlay();

                //Discard the card
                Game1.getChamp().getDeck().discardBecausePlayed(card);

                //Update UI
                Game1.updateBattleUI();

                //Set active card as null & remove targeting from current hover when finished
                Game1.setActiveCard(null);
                Game1.setHoveredClickable(null);
            }
        }



        /// <summary>
        /// Sets up the targets (already made and given as a List) of the currently selected card
        /// and puts them into the given UserInterface. Called when a new card gets selected.
        /// </summary>
        public static void setupTargets(UserInterface ui, UserInterface enemies/*, UserInterface party*/, Avatar champion, DeckBuilder.CardEnums.TargetingType targetType)
        {
            switch (targetType)
            {
                case DeckBuilder.CardEnums.TargetingType.enemies:
                    for (int i = 0; i < enemies.getClickables().Count; i++)
                    {
                        ui.addClickableToBack(new Target(enemies.getClickables()[i], DeckBuilder.CardEnums.TargetingType.enemies));
                    }
                    break;
                case DeckBuilder.CardEnums.TargetingType.all:
                    for (int i = 0; i < enemies.getClickables().Count; i++)
                    {
                        ui.addClickableToBack(new Target(enemies.getClickables()[i], DeckBuilder.CardEnums.TargetingType.enemies));
                    }
                    //FIXIT do when _party is implemented
                    /*for (int i = 0; i < _party.getClickables().Count; i++)
                    {
                        ui.addClickableToBack(new Target(_party.getClickables()[i], DeckBuilder.CardEnums.TargetingType.party));
                    }*/
                    ui.addClickableToBack(new Target(champion, DeckBuilder.CardEnums.TargetingType.champion));
                    break;
                case DeckBuilder.CardEnums.TargetingType.party:
                    //FIXIT do when _party is implemented
                    /*for (int i = 0; i < _party.getClickables().Count; i++)
                    {
                        ui.addClickableToBack(new Target(_party.getClickables()[i], DeckBuilder.CardEnums.TargetingType.party));
                    }*/
                    break;
                case DeckBuilder.CardEnums.TargetingType.friendlies:
                    //FIXIT do when _party is implemented
                    /*for (int i = 0; i < _party.getClickables().Count; i++)
                    {
                        ui.addClickableToBack(new Target(_party.getClickables()[i], DeckBuilder.CardEnums.TargetingType.party));
                    }*/
                    ui.addClickableToBack(new Target(champion, DeckBuilder.CardEnums.TargetingType.champion));
                    break;
                case DeckBuilder.CardEnums.TargetingType.champion:
                    ui.addClickableToBack(new Target(champion, DeckBuilder.CardEnums.TargetingType.champion));
                    break;
                default:
                    Game1.errorLog.Add("Game1.getTargetsOfCard trying to get a target from a targetingType that wasn't previously defined: " + targetType.ToString());
                    break;
            }
        }
    }
}