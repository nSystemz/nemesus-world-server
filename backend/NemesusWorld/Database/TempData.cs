using GTANetworkAPI;
using NemesusWorld.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Database
{
    class TempData : Script
    {
        public string rangname { get; set; }
        public int last_rang_check { get; set; }
        public bool adminduty { get; set; }
        public Vehicle adminvehicle { get; set; }
        public Vehicle rentVehicle { get; set; }
        public bool freezed { get; set; }
        public List<Items> itemlist { get; set; }
        public bool showinventory { get; set; }
        public string inventoysetting { get; set; }
        public int inventoryid { get; set; }
        public bool showspeedo { get; set; }
        public int counter { get; set; }
        public bool interiorswitch { get; set; }
        public int lasthouse { get; set; }
        public bool editfurniture { get; set; }
        public Vector3 furniturePosition { get; set; }
        public Vector3 furnitureRotation { get; set; }
        public int furnitureID { get; set; }
        public float furnitureSpeed { get; set; }
        public bool furnitureNew { get; set; }
        public string doorPosition { get; set; }
        public Vector3 furnitureOldPosition { get; set; }
        public Vector3 furnitureOldRotation { get; set; }
        public GTANetworkAPI.Object furnitureObject { get; set; }
        public int lastvehicle { get; set; }

        public int lastSeat { get; set; }
        public Vehicle jobVehicle { get; set; }
        public Vehicle jobVehicle2 { get; set; }
        public bool jobduty { get; set; }
        public SpedOrders order { get; set; }
        public TaxiBots order2 { get; set; }
        public int jobstatus { get; set; }
        public ColShape jobColshape { get; set; }
        public bool inCall { get; set; }
        public bool inCall2 { get; set; }
        public bool showSmartphone { get; set; }
        public bool handsUp { get; set; }
        public string lastShop { get; set; }
        public Dictionary<String, String> weaponTints { get; set; }
        public Dictionary<String, String> weaponComponents { get; set; }
        public Vehicle dealerShip { get; set; }
        public int tempValue { get; set; }
        public List<String>attachments { get; set; }
        public List<Tattoos> tattoos { get; set; }
        public string status { get; set; }
        public int cuffed { get; set; }
        public bool follow { get; set; }
        public bool followed { get; set; }
        public string radio { get; set; }
        public bool radiols { get; set; }
        public string killed { get; set; }
        public string undercover { get; set; }
        public GTANetworkAPI.Object ghettoblaster { get; set; }
        public int drunk { get; set; }
        public bool drunked { get; set; }
        public int ingangzone { get; set; }
        public bool crouching { get; set; }
        public bool inrob { get; set; }
        public int kills { get; set; }
        public int killsintime { get; set; }
        public bool isping { get; set; }
        public int pingcheck { get; set; }
        public bool speaker { get; set; }
        public Ped pet { get; set; }
        public int petTask { get; set; }

        public TempData()
        {
            rangname = "";
            last_rang_check = 1609462861;
            adminduty = false;
            adminvehicle = null;
            rentVehicle = null;
            freezed = false;
            itemlist = null;
            showinventory = false;
            inventoysetting = "nothing";
            inventoryid = 0;
            showspeedo = false;
            counter = 0;
            interiorswitch = false;
            lasthouse = 0;
            editfurniture = false;
            furniturePosition = null;
            furnitureRotation = null;
            furnitureID = 0;
            furnitureSpeed = 0.05f;
            furnitureNew = false;
            doorPosition = null;
            furnitureOldPosition = null;
            furnitureOldRotation = null;
            furnitureObject = null;
            lastvehicle = 0;
            lastSeat = -1;
            jobVehicle = null;
            jobVehicle2 = null;
            jobduty = false;
            order = null;
            order2 = null;
            jobstatus = 0;
            jobColshape = null;
            inCall = false;
            inCall2 = false;
            showSmartphone = false;
            handsUp = false;
            lastShop = "n/A";
            weaponTints = new Dictionary<String, String>();
            weaponComponents = new Dictionary<String, String>();
            dealerShip = null;
            tempValue = 0;
            attachments = new List<String>();
            tattoos = new List<Tattoos>();
            status = "Idle";
            cuffed = 0;
            follow = false;
            followed = false;
            radio = "";
            radiols = false;
            killed = "";
            undercover = "";
            ghettoblaster = null;
            drunk = 0;
            drunked = false;
            ingangzone = -1;
            crouching = false;
            inrob = false;
            kills = 0;
            killsintime = 1609462861;
            isping = false;
            pingcheck = 0;
            speaker = false;
            pet = null;
            petTask = 0;
        }
    }
}
