using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;

namespace NemesusWorld.Controllers
{
    class DoorsController : Script
    {
        public static List<Doors> doorsList = new List<Doors>();
        public static List<DoorsColshapes> doorsColshapesList = new List<DoorsColshapes>();
        public static int doorsID = 0;

        public static void GetAllDoors()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Doors door in db.Fetch<Doors>("SELECT * FROM doors"))
                {
                    AddDoor(door, false);
                }
                doorsID = doorsList.Count;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllDoors]: " + e.ToString());
            }
        }

        public static int GetFreeDoorsID()
        {
            doorsID++;
            return doorsID;
        }

        public static DoorsColshapes GetDoorsColShapeByDoorId(int doorid)
        {
            try
            {
                DoorsColshapes colReturn = null;
                foreach (DoorsColshapes doorsColshapes in doorsColshapesList)
                {
                    if (doorsColshapes != null && doorsColshapes.doorid == doorid)
                    {
                        colReturn = doorsColshapes;
                        break;
                    }
                }
                return colReturn;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetDoorsColShapeByDoorId]: " + e.ToString());
            }
            return null;
        }

        public static void UpdateDoor(Player player, Doors door, bool check = false)
        {
            try
            {
                if (door != null)
                {
                    if (check == false)
                    {
                        NAPI.ClientEvent.TriggerClientEventInRange(player.Position, 85f, "Client:UpdateDoors", NAPI.Util.ToJson(door));
                    }
                    else
                    {
                        player.TriggerEvent("Client:UpdateDoors", NAPI.Util.ToJson(door));
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateDoor]: " + e.ToString());
            }
        }

        public static void RemoveDoor(Doors deletedoor)
        {
            try
            {
                Doors removeDoor = deletedoor;
                DoorsColshapes colshape = GetDoorsColShapeByDoorId(deletedoor.id);
                if (colshape != null)
                {
                    if (colshape.doorshape != null && colshape.doorshape.Exists)
                    {
                        colshape.doorshape.Delete();
                        colshape.doorshape = null;
                    }
                    doorsColshapesList.Remove(colshape);
                }
                doorsList.Remove(removeDoor);
                removeDoor = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RemoveDoor]: " + e.ToString());
            }
        }

        public static void AddDoor(Doors adddoor, bool check = true)
        {
            try
            {
                DoorsColshapes doorColShape = new DoorsColshapes();
                doorColShape.doorid = adddoor.id;
                doorColShape.doorshape = NAPI.ColShape.CreateCylinderColShape(new Vector3(adddoor.posx, adddoor.posy, adddoor.posz), 9.5f, 0.25f, (uint)adddoor.dimension);
                doorsColshapesList.Add(doorColShape);
                doorsList.Add(adddoor);
                if (check == true)
                {
                    NAPI.ClientEvent.TriggerClientEventInRange(new Vector3(adddoor.posx, adddoor.posy, 1.5f), 85f, "Client:AddDoor", NAPI.Util.ToJson(adddoor));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[AddDoor]: " + e.ToString());
            }
        }

        public static Doors GetDoorByPosition(string getposition)
        {
            Doors retDoor = null;
            string[] doorPosition = new string[7];
            doorPosition = getposition.Split("|");
            foreach (Doors door in doorsList)
            {
                if (door != null && door.posx.ToString() == doorPosition[0] && door.posy.ToString() == doorPosition[1] && door.posz.ToString() == doorPosition[2])
                {
                    retDoor = door;
                    break;
                }
            }
            return retDoor;
        }

        public static Doors GetClosestDoor(Player player, float distance = 1.5f)
        {
            if (doorsList == null) return null;

            Doors door = doorsList.Where(d => player.Position.DistanceTo(new Vector3(d.posx, d.posy, d.posz)) <= 10f && player.Dimension == d.dimension)
            .OrderBy(d => player.Position.DistanceTo(new Vector3(d.posx, d.posy, d.posz)))
            .FirstOrDefault();

            if (door != null)
            {
                //PD 1
                if (door.hash == "2691149580" || door.hash == "725274945" || door.hash == "4104186511")
                {
                    distance = 9.15f;
                }
                else if (door.hash == "1356380196")
                {
                    distance = 8.75f;
                }
                //PD 2
                if(door.hash == "190829368")
                {
                    distance = 8.75f;
                }
                //SARC
                if(door.hash == "3931332547")
                {
                    distance = 10.85f;
                }
                if(door.hash == "2868942576" || door.hash == "2473190209" || door.hash == "3079744621" || door.hash == "270965283" || door.hash == "475418095")
                {
                    distance = 3.15f;
                }
                //Irish Pub
                if (door.hash == "90939006")
                {
                    distance = 8.75f;
                }
                //ACLS
                if (door.props == "faction-3")
                {
                    distance = 13.5f;
                }
                if (player.Position.DistanceTo(new Vector3(door.posx, door.posy, door.posz)) <= distance)
                {
                    return door;
                }
            }

            return null;
        }

        public static int GetDoorIDByPosition(float x, float y, float z)
        {
            int doorid = -1;
            foreach (Doors door in doorsList)
            {
                if (door != null && door.posx == x && door.posx == z && door.posx == y)
                {
                    doorid = door.id;
                    break;
                }
            }
            return doorid;
        }

        public static Doors GetDoorByID(int doorid)
        {
            Doors retDoor = null;
            foreach (Doors door in doorsList)
            {
                if (door != null && door.id == doorid)
                {
                    retDoor = door;
                    break;
                }
            }
            return retDoor;
        }
    }
}
