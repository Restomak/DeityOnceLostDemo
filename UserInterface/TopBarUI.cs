using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeityOnceLost.UserInterface
{
    class TopBarUI
    {
        UserInterface _topBar;

        public TopBarUI()
        {
            _topBar = new UserInterface();
        }

        public List<UserInterface> getUIForLateDraw()
        {
            return new List<UserInterface>() { _topBar };
        }

        public void addToActiveUI(List<UserInterface> activeUI)
        {
            activeUI.Insert(0, _topBar);
        }

        public void updateUI()
        {
            for (int i = 0; i < _topBar.getClickables().Count; i++)
            {
                Clickable current = _topBar.getClickables()[i];

                if (current.GetType() == typeof(Clickables.Hovers.DynamicText))
                {
                    ((Clickables.Hovers.DynamicText)current).onUpdate();
                }
            }
        }

        public void setupUI()
        {
            String championString = "Champion: " + Game1.getChamp().getName() + " (" + Game1.getChamp().getHero().getPronoun_they() + "/" + Game1.getChamp().getHero().getPronoun_them() + ")";
            List<String> championDescription = new List<String>();
            championDescription.Add("Your current champion.");
            Clickables.Hovers.DynamicText champName = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_CHAMPION_X,
                Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, championString, championDescription, Color.Gold);
            champName.setOnUpdate(() =>
            {
                champName.updateString("Champion: " + Game1.getChamp().getName() + " (" + Game1.getChamp().getHero().getPronoun_they() + "/" + Game1.getChamp().getHero().getPronoun_them() + ")");
            });
            _topBar.addClickableToBack(champName);

            String hpString = "HP: " + Game1.getChamp().getCurrentHP() + " / " + Game1.getChamp().getMaxHP();
            List<String> hpDescription = new List<String>();
            hpDescription.Add("Your champion's hitpoints. If");
            hpDescription.Add("they reach zero, your champion");
            hpDescription.Add("will die and you will be forced");
            hpDescription.Add("out of " + Game1.getChamp().getHero().getPronoun_their().ToLower() + " body.");
            Clickables.Hovers.DynamicText hpText = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_HP_X,
                Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, hpString, hpDescription, Color.Red);
            hpText.setOnUpdate(() =>
            {
                hpText.updateString("HP: " + Game1.getChamp().getCurrentHP() + " / " + Game1.getChamp().getMaxHP());
            });
            _topBar.addClickableToBack(hpText);

            int gold = Game1.getDungeonHandler().getGold();
            String goldString = "Gold: " + gold;
            List<String> goldDescription = new List<String>();
            goldDescription.Add("Your current amount of gold.");
            goldDescription.Add("You will need it later when"); //FIXIT implement description for after first dungeon as well
            goldDescription.Add("your champion and followers");
            goldDescription.Add("escape the dungeon.");
            /*
            goldDescription.Add("Your current amount of gold.");
            goldDescription.Add("You can use it in shops found");
            goldDescription.Add("in the dungeon, or you can");
            goldDescription.Add("bring it back to town!");
            */
            Clickables.Hovers.DynamicText goldText = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_GOLD_X,
                Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, goldString, goldDescription, Color.Gold);
            goldText.setOnUpdate(() =>
            {
                goldText.updateString("Gold: " + Game1.getDungeonHandler().getGold());
            });
            _topBar.addClickableToBack(goldText);

            
            String deckString = "Deck: ";
            Clickables.Hovers.DynamicText deckText = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_DECK_TEXT_X,
                Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, deckString, new List<String>(), Color.PowderBlue);
            _topBar.addClickableToBack(deckText);

            Clickables.DeckOfCards topBarDeckIcon = new Clickables.DeckOfCards(Clickables.DeckOfCards.typeOfDeck.DECK, Game1.getChamp());
            _topBar.addClickableToBack(topBarDeckIcon);

            Clickables.AestheticOnly topBarBackground = new Clickables.AestheticOnly(Game1.pic_functionality_bar,
                new Point(0, Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT), Game1.VIRTUAL_WINDOW_WIDTH, Drawing.DrawConstants.TOPBAR_HEIGHT, Color.DarkSlateBlue);
            _topBar.addClickableToBack(topBarBackground);
        }
    }
}
