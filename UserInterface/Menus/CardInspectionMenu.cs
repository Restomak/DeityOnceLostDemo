using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface.Menus
{
    /// <summary>
    /// Menu used when a card in a CardMenu is right clicked. The card will be displayed alone
    /// on screen, along with buttons to view its various versions (regular, empowered,
    /// bloodstained, and void-touched).
    /// </summary>
    class CardInspectionMenu : MenuUI
    {
        DeckBuilder.Card _inspectedCard;
        UserInterface _buttons;
        UserInterface _checkboxes;
        UserInterface _card;
        Clickables.Checkbox _empoweredCheckbox, _bloodstainedCheckbox, _voidCheckbox;

        public CardInspectionMenu(DeckBuilder.Card inspectedCard) : base(Drawing.DrawConstants.CARDINSPECTIONMENU_X, Drawing.DrawConstants.CARDINSPECTIONMENU_BACKGROUND_Y,
            Drawing.DrawConstants.CARDINSPECTIONMENU_WIDTH, Drawing.DrawConstants.CARDINSPECTIONMENU_BACKGROUND_HEIGHT, Game1.pic_functionality_bar,
            Color.DarkSlateGray * Drawing.DrawConstants.CARDINSPECTIONMENU_BACKGROUND_FADE, "", 0, 0, null, 0, Color.White, Color.White) //No title for this menu
        {
            _inspectedCard = inspectedCard;
            _buttons = new UserInterface();
            _checkboxes = new UserInterface();
            _card = new UserInterface();

            _wholeUI.Add(_buttons);
            _wholeUI.Add(_checkboxes);
            _wholeUI.Add(_card);

            initializeCheckboxesAndExitButton();
        }

        public override bool addTopBar() { return true; }

        public override void updateUI()
        {
            updateInpectedCard();
        }

        public override void onEscapePressed()
        {
            Game1.closeMenu(this);
        }



        private void updateInpectedCard()
        {
            _card.resetClickables();

            Point xy = new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDINSPECTIONMENU_CARD_WIDTH / 2,
                Drawing.DrawConstants.CARDINSPECTIONMENU_Y + Drawing.DrawConstants.CARDINSPECTIONMENU_BOTTOM_BUFFER + Drawing.DrawConstants.CARDINSPECTIONMENU_EXIT_BUTTON_HEIGHT +
                Drawing.DrawConstants.CARDINSPECTIONMENU_SPACE_BETWEEN * 2 + Drawing.DrawConstants.TEXT_12_HEIGHT);

            DeckBuilder.Card cardToShow;
            if (_empoweredCheckbox.isChecked())
            {
                cardToShow = _inspectedCard.getEmpoweredCard();
            }
            else
            {
                //FIXIT show other options
                cardToShow = _inspectedCard.getNewCard();
            }

            Clickables.AestheticOnly_InspectedCard card = new Clickables.AestheticOnly_InspectedCard(cardToShow, xy,
                Drawing.DrawConstants.CARDINSPECTIONMENU_CARD_WIDTH, Drawing.DrawConstants.CARDINSPECTIONMENU_CARD_HEIGHT);
            _card.addClickableToBack(card);
        }

        /// <summary>
        /// When initializing the checkboxes, checks to see if the card is already one of the
        /// upgraded versions, and sets the appropriate upgrade checkbox already to true if so.
        /// </summary>
        private void initializeCheckboxesAndExitButton()
        {
            //Exit button
            Clickables.Button exitButton = new Clickables.Button(Game1.pic_functionality_exitButton,
                new Point(Game1.VIRTUAL_WINDOW_WIDTH / 2 - Drawing.DrawConstants.CARDINSPECTIONMENU_EXIT_BUTTON_WIDTH / 2,
                Drawing.DrawConstants.CARDINSPECTIONMENU_Y + Drawing.DrawConstants.CARDINSPECTIONMENU_BOTTOM_BUFFER),
                Drawing.DrawConstants.CARDINSPECTIONMENU_EXIT_BUTTON_WIDTH, Drawing.DrawConstants.CARDINSPECTIONMENU_EXIT_BUTTON_HEIGHT, () =>
                {
                    Game1.closeMenu(this);
                }, new List<String>());
            _buttons.addClickableToBack(exitButton); //order doesn't matter

            //Calculate width for the checkbox area
            int width = Drawing.DrawConstants.CARDINSPECTIONMENU_SPACE_BETWEEN_TEXT * 2 + Drawing.DrawConstants.CHECKBOX_SIZE * 3 + Drawing.DrawConstants.CARDINSPECTIONMENU_CHECKBOX_BUFFER * 3 +
                (int)(Game1.roboto_black_12.MeasureString("Empowered").X + Game1.roboto_black_12.MeasureString("Bloodstained").X + Game1.roboto_black_12.MeasureString("Void-Touched").X);

            //Empowered checkbox
            _empoweredCheckbox = new Clickables.Checkbox(Game1.roboto_black_12, "Empowered", Color.PowderBlue, Color.Black, true);
            _empoweredCheckbox._x = Game1.VIRTUAL_WINDOW_WIDTH / 2 - width / 2;
            _empoweredCheckbox._y = Drawing.DrawConstants.CARDINSPECTIONMENU_Y + Drawing.DrawConstants.CARDINSPECTIONMENU_BOTTOM_BUFFER + Drawing.DrawConstants.CARDINSPECTIONMENU_EXIT_BUTTON_HEIGHT +
                Drawing.DrawConstants.CARDINSPECTIONMENU_SPACE_BETWEEN;
            _empoweredCheckbox._width = (int)(Game1.roboto_black_12.MeasureString("Empowered").X) + Drawing.DrawConstants.CARDINSPECTIONMENU_CHECKBOX_BUFFER + Drawing.DrawConstants.CHECKBOX_SIZE;
            _empoweredCheckbox._height = Drawing.DrawConstants.TEXT_12_HEIGHT;
            _checkboxes.addClickableToFront(_empoweredCheckbox);

            //Bloodstained checkbox
            _bloodstainedCheckbox = new Clickables.Checkbox(Game1.roboto_black_12, "Bloodstained", Color.Red, Color.Black, true);
            _bloodstainedCheckbox._x = _empoweredCheckbox._x + _empoweredCheckbox._width + Drawing.DrawConstants.CARDINSPECTIONMENU_SPACE_BETWEEN_TEXT;
            _bloodstainedCheckbox._y = Drawing.DrawConstants.CARDINSPECTIONMENU_Y + Drawing.DrawConstants.CARDINSPECTIONMENU_BOTTOM_BUFFER + Drawing.DrawConstants.CARDINSPECTIONMENU_EXIT_BUTTON_HEIGHT +
                Drawing.DrawConstants.CARDINSPECTIONMENU_SPACE_BETWEEN;
            _bloodstainedCheckbox._width = (int)(Game1.roboto_black_12.MeasureString("Bloodstained").X) + Drawing.DrawConstants.CARDINSPECTIONMENU_CHECKBOX_BUFFER + Drawing.DrawConstants.CHECKBOX_SIZE;
            _bloodstainedCheckbox._height = Drawing.DrawConstants.TEXT_12_HEIGHT;
            _checkboxes.addClickableToBack(_bloodstainedCheckbox);

            //Void-touched checkbox
            _voidCheckbox = new Clickables.Checkbox(Game1.roboto_black_12, "Void-Touched",
                new Color(Drawing.DrawConstants.BRIGHT_PURPLE_RED, Drawing.DrawConstants.BRIGHT_PURPLE_GREEN, Drawing.DrawConstants.BRIGHT_PURPLE_BLUE), Color.Black, true);
            _voidCheckbox._x = _bloodstainedCheckbox._x + _bloodstainedCheckbox._width + Drawing.DrawConstants.CARDINSPECTIONMENU_SPACE_BETWEEN_TEXT;
            _voidCheckbox._y = Drawing.DrawConstants.CARDINSPECTIONMENU_Y + Drawing.DrawConstants.CARDINSPECTIONMENU_BOTTOM_BUFFER + Drawing.DrawConstants.CARDINSPECTIONMENU_EXIT_BUTTON_HEIGHT +
                Drawing.DrawConstants.CARDINSPECTIONMENU_SPACE_BETWEEN;
            _voidCheckbox._width = (int)(Game1.roboto_black_12.MeasureString("Void-Touched").X) + Drawing.DrawConstants.CARDINSPECTIONMENU_CHECKBOX_BUFFER + Drawing.DrawConstants.CHECKBOX_SIZE;
            _voidCheckbox._height = Drawing.DrawConstants.TEXT_12_HEIGHT;
            _checkboxes.addClickableToBack(_voidCheckbox);

            //Link checkboxes and set them up properly
            _empoweredCheckbox.addToUncheckables(_bloodstainedCheckbox);
            _empoweredCheckbox.addToUncheckables(_voidCheckbox);
            _bloodstainedCheckbox.addToUncheckables(_empoweredCheckbox);
            _bloodstainedCheckbox.addToUncheckables(_voidCheckbox);
            _voidCheckbox.addToUncheckables(_empoweredCheckbox);
            _voidCheckbox.addToUncheckables(_bloodstainedCheckbox);

            if (_inspectedCard.isEmpowered())
            {
                _empoweredCheckbox.setChecked(true);
            }
            else if (_inspectedCard.isBloodstained())
            {
                _bloodstainedCheckbox.setChecked(true);
            }
            else if (_inspectedCard.isVoidtouched())
            {
                _voidCheckbox.setChecked(true);
            }
        }
    }
}
