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
            _topBar.resetClickables();

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

            //Party member 1
            if (Game1.getCombatHandler().getParty().Count > 0)
            {
                String partyText = "Party: ";
                Clickables.Hovers.DynamicText partyTextHover = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_PARTY_X,
                    Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                    Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, partyText, new List<String>(), Color.LawnGreen);
                _topBar.addClickableToBack(partyTextHover);

                Combat.PartyMember partyMember1 = Game1.getCombatHandler().getParty()[0];
                String party1String = partyMember1.getName() + " (" + partyMember1.getHero().getPronoun_they() + "/" + partyMember1.getHero().getPronoun_them() + ")";
                List<String> partyMember1_Description = new List<String>();
                partyMember1_Description.Add("Hitpoints: " + partyMember1.getCurrentHP() + "/" + partyMember1.getMaxHP());
                partyMember1_Description.Add("");
                partyMember1_Description.AddRange(partyMember1.getPartyMemberBuff().getDescription());
                Clickables.Hovers.DynamicText party1Hover = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_PARTY_1_X,
                    Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                    Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, party1String, partyMember1_Description, Color.LawnGreen);
                party1Hover.setOnUpdate(() =>
                {
                    partyMember1_Description[0] = "Hitpoints: " + partyMember1.getCurrentHP() + "/" + partyMember1.getMaxHP();
                });
                _topBar.addClickableToBack(party1Hover);

                //Party member 2
                if (Game1.getCombatHandler().getParty().Count > 1)
                {
                    Combat.PartyMember partyMember2 = Game1.getCombatHandler().getParty()[1];
                    String party2String = partyMember2.getName() + " (" + partyMember2.getHero().getPronoun_they() + "/" + partyMember2.getHero().getPronoun_them() + ")";
                    List<String> partyMember2_Description = new List<String>();
                    partyMember2_Description.Add("Hitpoints: " + partyMember2.getCurrentHP() + "/" + partyMember2.getMaxHP());
                    partyMember2_Description.Add("");
                    partyMember2_Description.AddRange(partyMember2.getPartyMemberBuff().getDescription());
                    Clickables.Hovers.DynamicText party2Hover = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_PARTY_2_X,
                        Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                        Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, party2String, partyMember2_Description, Color.LawnGreen);
                    party2Hover.setOnUpdate(() =>
                    {
                        partyMember2_Description[0] = "Hitpoints: " + partyMember2.getCurrentHP() + "/" + partyMember2.getMaxHP();
                    });
                    _topBar.addClickableToBack(party2Hover);

                    //Party member 3
                    if (Game1.getCombatHandler().getParty().Count > 2)
                    {
                        Combat.PartyMember partyMember3 = Game1.getCombatHandler().getParty()[2];
                        String party3String = partyMember3.getName() + " (" + partyMember3.getHero().getPronoun_they() + "/" + partyMember3.getHero().getPronoun_them() + ")";
                        List<String> partyMember3_Description = new List<String>();
                        partyMember3_Description.Add("Hitpoints: " + partyMember3.getCurrentHP() + "/" + partyMember3.getMaxHP());
                        partyMember3_Description.Add("");
                        partyMember3_Description.AddRange(partyMember3.getPartyMemberBuff().getDescription());
                        Clickables.Hovers.DynamicText party3Hover = new Clickables.Hovers.DynamicText(new Point(Drawing.DrawConstants.TOPBAR_PARTY_3_X,
                            Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT / 2 - Drawing.DrawConstants.TEXT_12_HEIGHT / 2),
                            Game1.roboto_black_12, Drawing.DrawConstants.TEXT_12_HEIGHT, party3String, partyMember3_Description, Color.LawnGreen);
                        party3Hover.setOnUpdate(() =>
                        {
                            partyMember3_Description[0] = "Hitpoints: " + partyMember3.getCurrentHP() + "/" + partyMember3.getMaxHP();
                        });
                        _topBar.addClickableToBack(party3Hover);
                    }
                }
            }


            //Top bar background - always do this last!
            Clickables.AestheticOnly topBarBackground = new Clickables.AestheticOnly(Game1.pic_functionality_bar,
                new Point(0, Game1.VIRTUAL_WINDOW_HEIGHT - Drawing.DrawConstants.TOPBAR_HEIGHT), Game1.VIRTUAL_WINDOW_WIDTH, Drawing.DrawConstants.TOPBAR_HEIGHT, Color.DarkSlateBlue);
            _topBar.addClickableToBack(topBarBackground); //the order DOES matter here
        }
    }
}
