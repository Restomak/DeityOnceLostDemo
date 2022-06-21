using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Story
{
    class StoryConstants
    {
        /*
         * REPLACERS
         * 
         * The below inserts the champion's pronouns, depending on which is used
         * [they: c] / [them: c] / [their: c] / [theirs: c]
         * 
         * Use pronouns other than the champion's by replacing the c with another char:
         * 1 for the first party member
         * 2 for the second party member
         * 3 for the third party member
         * n for a new character
         * 
         * Note that tags in the pronouns that have upper-case letters (eg. [They: c]) will use that upper-case letter
         * 
         * The same above format works with [Name: c] as well, for their name.
         * 
         * Edit: now implemented [HP: 20%] for 20% of the champion's HP. Percentages must be whole numbers.
         */

        public const String INTRO_LINE_1 = "[f: 14 bb]Emptiness";
        public const String INTRO_LINE_2 = "";
        public const String INTRO_LINE_3 = "How long has it been since you've known anything other than emptiness? Centuries? Millennia? For a deity, neither is much time at all. But with your";
        public const String INTRO_LINE_4 = "consciousness so weak, it seems to make the time drag on, and on, and on...";
        public const String INTRO_LINE_5 = "";
        public const String INTRO_LINE_6 = "With effort, you remember a little. You need |[c: Powder Blue][f: 12 bb]followers|. Belief and worship are what power a deity. Without them, you see nothing, you feel nothing.";
        public const String INTRO_LINE_7 = "Without them, you |[f: 12 bb][s: Black]are| nothing but a |[s: Darker Purple][c: Purple]shadow| of yourself.";
        public const String INTRO_LINE_8 = "";
        public const String INTRO_LINE_9 = "A voice sounds in your mind, faint and desperate. |[c: Lawn Green][f: 10 b]\"...please...\"";
        public const String INTRO_LINE_10 = "";
        public const String INTRO_LINE_11 = "It has been so long, you can't remember what happened to your followers anymore.";
        public const String INTRO_LINE_12 = "";
        public const String INTRO_LINE_13 = "[c: Lawn Green][f: 10 b]\"I don't know if anyone is listening... if anyone is out there, please help me\"";
        public const String INTRO_LINE_14 = "";
        public const String INTRO_LINE_15 = "New followers, however... |[c: Red][f: 12 bb]Intriguing|. There is only one thing to do:";
        public const String INTRO_CHOICE = "[c: Powder Blue][f: 12 bb]Answer the plea.";

        public const String INTRO_RESULT_LINE_1 = "[c: Gold][s: Dark Goldenrod][f: 14 bb]POWER| rushes into you as you feel [their: c] awe and [their: c] newfound |[c: Powder Blue][f: 12 bb]belief|.";
        public const String INTRO_RESULT_LINE_2 = "";
        public const String INTRO_RESULT_LINE_3 = "Through [them: c], you can |[f: 12 bb]see|. You can |[f: 12 bb]hear|. You can |[f: 12 bb]feel|.";
        public const String INTRO_RESULT_LINE_4 = "";
        public const String INTRO_RESULT_LINE_5 = "There are others with [them: c], around [them: c], sitting quietly amongst themselves. Some have reacted and are looking at you curiously. Others stare blankly";
        public const String INTRO_RESULT_LINE_6 = "forward with defeat on their faces. These are a people who have nearly given up hope. A people that could need you. A people that could be |[c: Red][f: 12 bb]yours|.";
        public const String INTRO_RESULT_LINE_7 = "";
        public const String INTRO_RESULT_LINE_8 = "You can sense their desires, and you see what they need: |[c: Lawn Green][f: 12 bb]freedom|. Trapped in a cell in some |[c: Dark Gray][s: Darker Gray][f: 12 bb]underground dungeon| with the rest of the remnants of";
        public const String INTRO_RESULT_LINE_9 = "their tribe, they need you. Not only to get them out of here, but to continue to guide them them once you do.";
        public const String INTRO_RESULT_LINE_10 = "";
        public const String INTRO_RESULT_LINE_11 = "It seems you have found the followers that you need, but you'll need to |[c: Powder Blue][f: 12 bb]inspire| them if you wish to continue to grow in power. It's time to break them";
        public const String INTRO_RESULT_LINE_12 = "out of here. You focus on the one who prayed for you. The one who you shall make their leader. Yes, |[they: c] will do just fine.";
        public const String INTRO_RESULT_LINE_13 = "";
        public const String INTRO_RESULT_LINE_14 = "[They: c] shall be your |[c: Gold][f: 12 bb]champion|.";

        public const String INTRO_ROOM2_LINE_1 = "As you reach the cell door with your new champion, you can feel [their: c] confidence |[c: Powder Blue][f: 12 bb]growing|. You can tell that [they're: c] a fighter among [their: c] people, and";
        public const String INTRO_ROOM2_LINE_2 = "under your control [they're: c] a |[f: 12 bb]force to be reckoned with|.";
        public const String INTRO_ROOM2_LINE_3 = "";
        public const String INTRO_ROOM2_LINE_4 = "A |[c: Dark Red][s: Darker Red][f: 12 bb]dungeon guard| leans against the wall by the cell door, scowling at your champion and [their: c] people. You can sense that the guard is itching for a";
        public const String INTRO_ROOM2_LINE_5 = "reason to |[c: Red][f: 12 bb]fight|.";
        public const String INTRO_ROOM2_LINE_6 = "";
        public const String INTRO_ROOM2_LINE_7 = "With your deific power, a simple dungeon door is |[f: 12 bb]nothing| to you. You shall give the guard their wish.";
        public const String INTRO_ROOM2_CHOICE = "[f: 12 bb]Break down the door.";

        public const String INTRO_ROOM2_RESULT_LINE_1 = "[s: Monogame Orange][f: 14 bb]CRASH";
        public const String INTRO_ROOM2_RESULT_LINE_2 = "";
        public const String INTRO_ROOM2_RESULT_LINE_3 = "The guard jumps, surprise on their face. They quickly recover, however, and step forward, |[c: Dark Red][f: 12 bb]weapon| drawn.";
        public const String INTRO_ROOM2_RESULT_LINE_4 = "";
        public const String INTRO_ROOM2_RESULT_LINE_5 = "A |[f: 12 bb][c: Red]wicked grin| forms on the guard's face, thinking this will be a |[c: Darker Red][s: Red][f: 12 bb]slaughter|, but you will |[c: Powder Blue][f: 12 bb]not| let them reach your soon-to-be followers.";

        public const String INTRO_ROOM3_LINE_1 = "With the defeated dungeon guard behind you, you have your champion stand for a moment as you contemplate your victory. You can sense the";
        public const String INTRO_ROOM3_LINE_2 = "pleasure they feel over it, but to you, that fight felt sloppy. It was an |[c: Red][f: 12 bb]unrefined melee| between mortals, and you know that |[f: 12 bb]you| can do |[f: 12 bb]better|.";
        public const String INTRO_ROOM3_LINE_3 = "You could infuse |[f: 12 bb]more| of yourself into your champion, if only you could remember your own powers a little better...";
        public const String INTRO_ROOM3_LINE_4 = "";
        public const String INTRO_ROOM3_LINE_5 = "You focus inward, looking through the vastness of your very being. Long has it been since you've known anything other than the |[s: Darker Purple][c: Purple]void|.";
        public const String INTRO_ROOM3_LINE_6 = "";
        public const String INTRO_ROOM3_LINE_7 = "Something catches your attention. A hint of |[s: Purple][c: Powder Blue][f: 12 bb]something greater|- a fragment of a memory long lost- gains some tenuous hold in your mind.";
        public const String INTRO_ROOM3_LINE_8 = "";
        public const String INTRO_ROOM3_LINE_9 = "Without hesitation, you |[s: Monogame Orange][f: 14 bb]PULL| on that fragment, forcing it forward, |[c: Red][f: 12 bb]pushing| it into your champion. You feel [them: c] shudder at the sudden invasion,";
        public const String INTRO_ROOM3_LINE_10 = "but [their: c] discomfort passes. Left in its place, infusing [them: c], is a fragment of your former |[c: Gold][s: Dark Goldenrod][f: 12 bb]power|. You can feel [their: c] awe.";
        public const String INTRO_ROOM3_LINE_11 = "";
        public const String INTRO_ROOM3_LINE_12 = "This is |[f: 12 bb]much| better.";

        public const String INTRO_F2_RANDOM_ROOM_LINE_1 = "The prisoners that you freed have been following you. They keep their distance, letting your champion deal with the |[c: Dark Red][f: 12 bb]dangers| ahead. You can";
        public const String INTRO_F2_RANDOM_ROOM_LINE_2 = "see |[c: Powder Blue][f: 12 bb]hope| kindling in their spirits as they slowly shed their feelings of hopelessness and numbness to despair.";
        public const String INTRO_F2_RANDOM_ROOM_LINE_3 = "";
        public const String INTRO_F2_RANDOM_ROOM_LINE_4 = "Emotions build in your champion as you watch through [their: c] eyes. You feel [their: c] resolve |[f: 12 bb][c: Lawn Green]strengthen|. [Their: c] desire to save these people is |[f: 12 bb]strong|.";
        public const String INTRO_F2_RANDOM_ROOM_LINE_5 = "";
        public const String INTRO_F2_RANDOM_ROOM_LINE_6 = "It is a desire you will see |[c: Powder Blue][f: 12 bb]fulfilled|.";
        public const String INTRO_F2_RANDOM_ROOM_LINE_7 = "";
        public const String INTRO_F2_RANDOM_ROOM_LINE_8 = "One of the freed prisoners approaches you, |[f: 12 bb]trembling| yet |[s: Purple][c: Powder Blue][f: 12 bb]brave|. Cautious optimism fills her expression as she looks to you, hoping,";
        public const String INTRO_F2_RANDOM_ROOM_LINE_9 = "waiting for something.";
        public const String INTRO_F2_RANDOM_ROOM_CHOICE_1 = "[f: 12 bb]Pass on a self-defense technique |[c: Gold][f: 12 bb](Remove a card from your deck)";
        public const String INTRO_F2_RANDOM_ROOM_CHOICE_2 = "[f: 12 bb]Inspire her with a new move |[c: Gold][f: 12 bb](Add a card to your deck)";

        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_1 = "Confidence bolstered and appreciative, she moves back to the group, speaking in a hushed voice. Many of them look at you with awe, but some";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_2 = "display a new expression on their faces: one of |[c: Powder Blue][f: 12 bb]faith|.";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_3 = "";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_4 = "Feeling |[c: Powder Blue][f: 12 bb]invigorated|, you continue on.";

        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_1 = "Face filled with awe, she stays for a moment before moving back to the group, speaking in a hushed yet excited voice. Many of them look at you with awe, but some";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_2 = "display a new expression on their faces: one of |[c: Powder Blue][f: 12 bb]faith|.";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_3 = "";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_4 = "Feeling |[c: Powder Blue][f: 12 bb]invigorated|, you continue on.";
    }
}
