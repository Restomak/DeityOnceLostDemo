using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeityOnceLost.Characters
{
    class Names
    {
        public const double CHANCE_OF_WHATEVER = 0.1;
        public const double CHANCE_OF_STEP = 0.2;

        public static List<String> heroNames_masc = new List<string>();
        public static List<String> heroNames_femme = new List<string>();
        public static List<String> heroNames_nonbinary = new List<string>();
        public static List<String> heroNames_androgynous = new List<string>();

        public static void initializeNameList()
        {
            //FIXIT make a better way to add names - probably loading from a text document so users can add their own names. still use something like this for legendaries/presets/etc

            heroNames_masc.Add("Alard");
            heroNames_masc.Add("Allison");
            heroNames_masc.Add("Anthon");
            heroNames_masc.Add("Arled");
            heroNames_masc.Add("Atham");
            heroNames_masc.Add("Blake");
            heroNames_masc.Add("David");
            heroNames_masc.Add("Elend");
            heroNames_masc.Add("Gery");
            heroNames_masc.Add("Gregory");
            heroNames_masc.Add("Hamund");
            heroNames_masc.Add("James");
            heroNames_masc.Add("Johny");
            heroNames_masc.Add("Kathon");
            heroNames_masc.Add("Kelsier");
            heroNames_masc.Add("Masym");
            heroNames_masc.Add("Michael");
            heroNames_masc.Add("Nieleon");
            heroNames_masc.Add("Ollie");
            heroNames_masc.Add("Paul");
            heroNames_masc.Add("Ralphye");
            heroNames_masc.Add("Ridger");
            heroNames_masc.Add("Robert");
            heroNames_masc.Add("Sazed");
            heroNames_masc.Add("Stephen");
            heroNames_masc.Add("Thomes");
            heroNames_masc.Add("Vyncent");

            heroNames_femme.Add("Amanda");
            heroNames_femme.Add("Anel");
            heroNames_femme.Add("Cassandra");
            heroNames_femme.Add("Cassi");
            heroNames_femme.Add("Christina");
            heroNames_femme.Add("Elyn");
            heroNames_femme.Add("Eryn");
            heroNames_femme.Add("Heather");
            heroNames_femme.Add("Jane");
            heroNames_femme.Add("Josey");
            heroNames_femme.Add("Joyce");
            heroNames_femme.Add("Joycie");
            heroNames_femme.Add("Katie");
            heroNames_femme.Add("Kylie");
            heroNames_femme.Add("Lene");
            heroNames_femme.Add("Liz");
            heroNames_femme.Add("Makayla");
            heroNames_femme.Add("Maly");
            heroNames_femme.Add("Marey");
            heroNames_femme.Add("Mary");
            heroNames_femme.Add("Nicole");
            heroNames_femme.Add("Pearl");
            heroNames_femme.Add("Rae");
            heroNames_femme.Add("Raychel");
            heroNames_femme.Add("Sabeth");
            heroNames_femme.Add("Sane");
            heroNames_femme.Add("Sarah");
            heroNames_femme.Add("Serra");
            heroNames_femme.Add("Sophie");
            heroNames_femme.Add("Steph");
            heroNames_femme.Add("Suse");
            heroNames_femme.Add("Vin");
            heroNames_femme.Add("Violet");
            heroNames_femme.Add("Zoey");

            heroNames_nonbinary.Add("Bone");
            heroNames_nonbinary.Add("Brick");
            heroNames_nonbinary.Add("Choko");
            heroNames_nonbinary.Add("Han");
            heroNames_nonbinary.Add("Krysie");
            heroNames_nonbinary.Add("Link");
            heroNames_nonbinary.Add("Rakan");
            heroNames_nonbinary.Add("Seven");

            heroNames_androgynous.Add("Annan");
            heroNames_androgynous.Add("Ati");
            heroNames_androgynous.Add("Atril");
            heroNames_androgynous.Add("Cece");
            heroNames_androgynous.Add("Drichye");
            heroNames_androgynous.Add("Efrix");
            heroNames_androgynous.Add("Eleth");
            heroNames_androgynous.Add("Ellet");
            heroNames_androgynous.Add("Erard");
            heroNames_androgynous.Add("Finy");
            heroNames_androgynous.Add("Frewis");
            heroNames_androgynous.Add("Grewilh");
            heroNames_androgynous.Add("Gylas");
            heroNames_androgynous.Add("Gylew");
            heroNames_androgynous.Add("Hany");
            heroNames_androgynous.Add("Jamie");
            heroNames_androgynous.Add("Kater");
            heroNames_androgynous.Add("Leras");
            heroNames_androgynous.Add("Lesym");
            heroNames_androgynous.Add("Marger");
            heroNames_androgynous.Add("Mathye");
            heroNames_androgynous.Add("Narder");
            heroNames_androgynous.Add("Phames");
            heroNames_androgynous.Add("Pyffe");
            heroNames_androgynous.Add("Rarder");
            heroNames_androgynous.Add("Ryany");
            heroNames_androgynous.Add("Stomund");
            heroNames_androgynous.Add("Willex");
            heroNames_androgynous.Add("Wisym");
        }
    }
}
