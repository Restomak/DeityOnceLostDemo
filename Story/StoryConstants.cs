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
         * [s: c] is also now implemented, and for grammar purposes, it will place an s in its place unless the champion's pronouns are they/them
         */

        public const String INTRO_LINE_1 = "[f: 14 bb]Emptiness";
        public const String INTRO_LINE_2 = "";
        public const String INTRO_LINE_3 = "How long has it been since you've known anything other than emptiness? Centuries? Millennia? For a deity, neither is much time at all. But with your";
        public const String INTRO_LINE_4 = "consciousness so weak, it seems to make the time drag on, and on, and on...";
        public const String INTRO_LINE_5 = "";
        public const String INTRO_LINE_6 = "With effort, you remember a little. You need |[c: Powder Blue][f: 12 bb]followers|. Belief and worship are what power a deity. Without them, you see nothing, you feel nothing.";
        public const String INTRO_LINE_7 = "Without them, you |[f: 12 bb][s: Black]are| nothing but a |[s: Darker Purple][c: Bright Purple]shadow| of yourself.";
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

        public const String INTRO_ROOM2_LINE_1 = "As you reach the cell door with your new champion, you can feel [their: c] confidence |[c: Lawn Green][f: 12 bb]growing|. You can tell that [they're: c] a fighter among [their: c] people, and";
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
        public const String INTRO_ROOM3_LINE_5 = "You focus inward, looking through the vastness of your very being. Long has it been since you've known anything other than the |[s: Darker Purple][c: Bright Purple]void|.";
        public const String INTRO_ROOM3_LINE_6 = "";
        public const String INTRO_ROOM3_LINE_7 = "Something catches your attention. A hint of |[s: Purple][c: Powder Blue][f: 12 bb]something greater|- a fragment of a memory long lost- gains some tenuous hold in your mind.";
        public const String INTRO_ROOM3_LINE_8 = "";
        public const String INTRO_ROOM3_LINE_9 = "Without hesitation, you |[s: Monogame Orange][f: 14 bb]PULL| on that fragment, forcing it forward, |[c: Red][f: 12 bb]pushing| it into your champion. You feel [them: c] shudder at the sudden invasion,";
        public const String INTRO_ROOM3_LINE_10 = "but [their: c] discomfort passes. Left in its place, infusing [them: c], is a fragment of your former |[c: Gold][s: Dark Goldenrod][f: 12 bb]power|. You can feel [their: c] awe.";
        public const String INTRO_ROOM3_LINE_11 = "";
        public const String INTRO_ROOM3_LINE_12 = "This is |[f: 12 bb]much| better.";

        public const String INTRO_F2_RANDOM_ROOM_LINE_1 = "The prisoners that you freed have been following you. They keep their distance, letting your champion deal with the |[c: Dark Red][f: 12 bb]dangers| ahead. You can see";
        public const String INTRO_F2_RANDOM_ROOM_LINE_2 = "[c: Powder Blue][f: 12 bb]hope| kindling in their spirits as they slowly shed their feelings of hopelessness and numbness to despair.";
        public const String INTRO_F2_RANDOM_ROOM_LINE_3 = "";
        public const String INTRO_F2_RANDOM_ROOM_LINE_4 = "Emotions build in your champion as you watch through [their: c] eyes. You feel [their: c] resolve |[f: 12 bb][c: Lawn Green]strengthen|. [Their: c] desire to save these people is |[f: 12 bb]strong|.";
        public const String INTRO_F2_RANDOM_ROOM_LINE_5 = "";
        public const String INTRO_F2_RANDOM_ROOM_LINE_6 = "It is a desire you will see |[c: Powder Blue][f: 12 bb]fulfilled|.";
        public const String INTRO_F2_RANDOM_ROOM_LINE_7 = "";
        public const String INTRO_F2_RANDOM_ROOM_LINE_8 = "One of the freed prisoners approaches you, |[f: 12 bb]trembling| yet |[s: Purple][c: Powder Blue][f: 12 bb]brave|. Cautious optimism fills her expression as she looks to you, hoping, waiting";
        public const String INTRO_F2_RANDOM_ROOM_LINE_9 = "for something.";
        public const String INTRO_F2_RANDOM_ROOM_CHOICE_1 = "[f: 12 bb]Pass on a self-defense technique |[c: Gold][f: 12 bb](Remove a card from your deck)";
        public const String INTRO_F2_RANDOM_ROOM_CHOICE_2 = "[f: 12 bb]Inspire her with a new move |[c: Gold][f: 12 bb](Add a card to your deck)";

        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_1 = "Confidence bolstered and appreciative, she moves back to the group, speaking in a hushed voice. Many of them look at you with awe, but some";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_2 = "display a new expression on their faces: one of |[c: Powder Blue][f: 12 bb]faith|.";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_3 = "";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C1_4 = "Feeling |[c: Powder Blue][f: 12 bb]invigorated|, you continue on.";

        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_1 = "Face filled with awe, she stays for a moment before moving back to the group, speaking in a hushed yet excited voice. Many of them look at you with awe, but some";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_2 = "display a new expression on their faces: a |[c: Powder Blue][f: 12 bb]belief| in your existence.";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_3 = "";
        public const String INTRO_F2_RANDOM_ROOM_RESULT_C2_4 = "Feeling |[c: Powder Blue][f: 12 bb]invigorated|, you continue on.";

        public const String INTRO_F2_PARTY_ROOM_LINE_1 = "Figures huddle together in the shadows, |[c: Bright Purple][f: 12 bb]cowering| as you enter the room. Frail and half-starved, they wear the tattered rags of prisoners, much like";
        public const String INTRO_F2_PARTY_ROOM_LINE_2 = "those your of your new followers. They appear to have escaped on their own, somehow. Perhaps there are some among them with the ability to";
        public const String INTRO_F2_PARTY_ROOM_LINE_3 = "defend themselves|.";
        public const String INTRO_F2_PARTY_ROOM_LINE_4 = "";
        public const String INTRO_F2_PARTY_ROOM_LINE_5 = "As you approach, two haggard prisoners step forward protectively, one of them wielding a |[c: Dark Red][f: 12 bb]weapon| and ready to |[c: Red][f: 12 bb]fight| if needed. Both seem to";
        public const String INTRO_F2_PARTY_ROOM_LINE_6 = "recognize your champion, however, and their eyes widen.";
        public const String INTRO_F2_PARTY_ROOM_LINE_7 = "";
        public const String INTRO_F2_PARTY_ROOM_LINE_8 = "The one with the weapon lowers it and gestures incredulously. |[c: Lawn Green]\"[Name: c]? You're alive! But how...?\"";
        public const String INTRO_F2_PARTY_ROOM_LINE_9 = "";
        public const String INTRO_F2_PARTY_ROOM_LINE_10 = "You relinquish some of your control over your champion, allowing [them: c] to tell [their: c] story thus far. [They: c] tell[s: c] of the |[c: Powder Blue][f: 12 bb]plea for help|, of the |[c: Gold][s: Dark Goldenrod][f: 12 bb]power| you";
        public const String INTRO_F2_PARTY_ROOM_LINE_11 = "have given [them: c]. [They: c] tell[s: c] of the escape, and the defeat of the |[c: Red][f: 12 bb]guards| and |[c: Red][f: 12 bb]monsters| on your way here. The pair listens intently, |[c: Powder Blue][f: 12 bb]wonder| on their";
        public const String INTRO_F2_PARTY_ROOM_LINE_12 = "faces, and afterwards expresses their intent to |[c: Lawn Green][f: 12 bb]escape| this dungeon.";
        public const String INTRO_F2_PARTY_ROOM_LINE_13 = "";
        public const String INTRO_F2_PARTY_ROOM_LINE_14 = "They want to join you. They want to help. They could be |[s: Purple][c: Powder Blue][f: 12 bb]useful|.";
        public const String INTRO_F2_PARTY_ROOM_CHOICE = "[f: 12 bb]Accept their offer |[c: Lawn Green][f: 12 bb](Gain party members)";

        public const String INTRO_F3_CORPSE_ROOM_LINE_1 = "The smell of |[c: Red][f: 12 bb]blood| assaults your senses as you step into the room, and among the wreckage of destroyed furniture you spot a |[c: Dark Red][f: 12 bb]body|.";
        public const String INTRO_F3_CORPSE_ROOM_LINE_2 = "";
        public const String INTRO_F3_CORPSE_ROOM_LINE_3 = "Recently-deceased, the corpse is covered in |[c: Red][f: 12 bb]bite marks| and the same weird |[c: Dark Olive Green][f: 12 bb]goop| spat by crawlers. The bite marks, however, are much |[f: 12 bb]larger|";
        public const String INTRO_F3_CORPSE_ROOM_LINE_4 = "than what a crawler can make.";
        public const String INTRO_F3_CORPSE_ROOM_LINE_5 = "";
        public const String INTRO_F3_CORPSE_ROOM_LINE_6 = "There is likely something |[c: Darker Red][s: Red][f: 12 bb]dangerous| up ahead. It might be wise to |[c: Powder Blue][f: 12 bb]rest| using the |[c: Green Yellow][f: 12 bb]Firewood| you just found.";
        public const String INTRO_F3_CORPSE_ROOM_LINE_7 = "";
        public const String INTRO_F3_CORPSE_ROOM_LINE_8 = "Taking one last look at the body, you get the sudden |[s: Purple][c: Powder Blue][f: 12 bb]impulse| to carry it with you. If you were to carry them the entire way out of this dungeon";
        public const String INTRO_F3_CORPSE_ROOM_LINE_9 = "and to safety, you could resurrect them. You get the feeling that whoever this was, they would |[s: Purple][c: Powder Blue][f: 12 bb]follow| you if you did.";
        public const String INTRO_F3_CORPSE_ROOM_CHOICE_1 = "[f: 12 bb]Carry them out with you.";
        public const String INTRO_F3_CORPSE_ROOM_CHOICE_2 = "[f: 12 bb]Leave them behind.";

        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_1 = "Your footsteps echo faintly on the stone flooring of the empty hallway as you stride purposely towards the stairwell that leads to the next level of the";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_2 = "dungeon. Your champion swells with a sense of |[c: Lawn Green][f: 12 bb]accomplishment|, a confidence that [their: c] goal is attainable. Pausing for a moment to allow the rest of";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_3 = "your followers to catch up, you watch as they scramble nervously past the massive carcass- all that remains of the battle from moments ago.";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_4 = "";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_5 = "Most eye the felled creature as they pass, as if in disbelief that such a |[c: Red][f: 12 bb]terror| lies slain. You have an unshakable feeling that there will be worse";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_6 = "dangers to come, however your champion grins at them proudly, and you sense a new emotion budding within the group: |[s: Dark Goldenrod][c: Powder Blue][f: 14 bb]faith|. They see what";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_7 = "you've accomplished, and they now believe that you will give them the freedom they so crave.";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_8 = "";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_9 = "One of them stumbles out of the group towards you, pushed by two others. He looks back at his companions nervously, but seeing their encouraging";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_10 = "gestures, he steps forward. He holds his hands out before him, palms upwards, displaying a crude |[c: Brown][f: 12 bb]wooden sculpture|. You aren't sure exactly when";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_11 = "he had the time to make this, however it would appear to be some kind of |[c: Powder Blue][f: 12 bb]offering| to you, whittled with one of the shoddy knives left behind by the";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_12 = "downed guards as you've made your escape.";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_13 = "";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_14 = "You sense a hidden |[s: Dark Goldenrod][f: 12 bb]potential| within this offering, a small power that would |[c: Gold][f: 12 bb]flourish| should you accept it. Despite its craftsmanship, it has an";
        public const String INTRO_F3_MINIBOSS_DEFEATED_LINE_15 = "important quality: it was made in reverence of |[s: Purple][c: Powder Blue][f: 12 bb]you|.";
        public const String INTRO_F3_MINIBOSS_DEFEATED_CHOICE = "[f: 12 bb]Accept it as your blessing.";
    }
}
