using NemesusWorld.Database;
using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("animations")]
    [PetaPoco.PrimaryKey("id")]
    class Animations
    {
        public int id { get; set; }
        public string name { get; set; }
        public string animation { get; set; }
        public string category { get; set; }
        public int duration { get; set; }

        public Animations()
        {
            id = 0;
            name = "";
            animation = "";
            category = "";
            duration = -1;
        }

        public static void GetAllAnimations()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Animations animation in db.Fetch<Animations>("SELECT * FROM animations"))
                {
                    Helper.animList.Add(animation);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllAnimations]: " + e.ToString());
            }
        }
    }
}
