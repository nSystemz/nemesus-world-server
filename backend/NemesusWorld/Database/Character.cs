using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NemesusWorld.Database
{
    [PetaPoco.TableName("characters")]
    [PetaPoco.PrimaryKey("id")]
    class Character
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string name { get; set; }
        public string json { get; set; }
        public int cash { get; set; }
        public string birth { get; set; }
        public string size { get; set; }
        public string eyecolor { get; set; }
        public int job { get; set; }
        public int minijob { get; set; }
        public int lastonline { get; set; }
        public string licenses { get; set; }
        public string education { get; set; }
        public string origin { get; set; }
        public string skills { get; set; }
        public string appearance { get; set; }
        public int gender { get; set; }
        public int faction { get; set; }
        public int rang { get; set; }
        public int faction_dutytime { get; set; }
        public int faction_since { get; set; }
        public string last_spawn { get; set; }
        public int ucp_privat { get; set; }
        public int ck { get; set; }
        public int mygroup { get; set; }
        public int closed { get; set; }
        public int tutorialstep { get; set; }
        public int health { get; set; }
        public int armor { get; set; }
        public int thirst { get; set; }
        public int hunger { get; set; }
        public string screen { get; set; }
        public string lastpos { get; set; }
        public string items { get; set; }
        public int inhouse { get; set; }
        public string defaultbank { get; set; }
        public int online { get; set; }
        public int truckerskill { get; set; }
        public int nextpayday { get; set; }
        public int payday_points { get; set; }
        public int sellprods { get; set; }
        public int abusemoney { get; set; }
        public string lastsmartphone { get; set; }
        public int thiefskill { get; set; }
        public int fishingskill { get; set; }
        public int busskill { get; set; }
        public int farmingskill { get; set; }
        public string animations { get; set; }
        public string walkingstyle { get; set; }
        public string clothing { get; set; }
        public bool factionduty { get; set; }
        public int sapoints { get; set; }
        public int swat { get; set; }
        public int arrested { get; set; }
        public int cell { get; set; }
        public string dutyjson { get; set; }
        public bool death { get; set; }        
        public int disease { get; set; }
        public int craftingskill { get; set; }
        [PetaPoco.ResultColumn]
        public int afk { get; set; }
        [PetaPoco.ResultColumn]
        public int bank { get; set; }
        public int robcooldown { get; set; }
        public int adcount { get; set; }
        public int guessvalue { get; set; }
        public int jobless { get; set; }

        public Character()
        {
            id = 0;
            userid = 0;
            name = "";
            json = "";
            cash = 5000;
            birth = "01.01.2020";
            size = "n/A";
            eyecolor = "n/A";
            job = 0;
            minijob = 0;
            lastonline = 1609462861;
            licenses = "0|0|0|0|0|0|0";
            education = "n/A";
            origin = "n/A";
            skills = "n/A";
            appearance = "n/A";
            gender = 1;
            faction = 0;
            rang = 0;
            faction_dutytime = 0;
            faction_since = 1609462861;
            last_spawn = "Los-Santos";
            ucp_privat = 0;
            ck = 0;
            mygroup = -1;
            closed = 0;
            tutorialstep = 0;
            health = 200;
            armor = 1;
            thirst = 100;
            hunger = 100;
            screen = "https://i.imgur.com/JjpH0qO.jpg";
            lastpos = "0|0|0|0|0";
            items = "";
            inhouse = -1;
            defaultbank = "n/A";
            online = 0;
            truckerskill = 45;
            nextpayday = 0;
            payday_points = 0;
            sellprods = 0;
            abusemoney = 0;
            lastsmartphone = "";
            thiefskill = 25;
            fishingskill = 35;
            busskill = 35;
            farmingskill = 25;
            animations = "[\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\"]";
            walkingstyle = "";
            clothing = "1,1,1,1,1,1,1,1";
            factionduty = false;
            sapoints = 0;
            swat = 0;
            arrested = 0;
            cell = 0;
            dutyjson = "{\"clothing\":[-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}";
            death = false;
            disease = 0;
            craftingskill = 25;
            afk = 0;
            bank = 0;
            robcooldown = 0;
            adcount = 0;
            guessvalue = 0;
            jobless = 0;
        }
    }
}
