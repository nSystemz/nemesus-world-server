using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GTANetworkAPI;
using GTANetworkMethods;
using NemesusWorld.Controllers;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Vehicle = GTANetworkAPI.Vehicle;

namespace NemesusWorld.Database
{
    class Cars
    {
        public static List<Cars> carList = new List<Cars>();

        public int id { get; set; }
        public Vehicle vehicleHandle { get; set; }
        public VehicleData vehicleData { get; set; }

        public Cars()
        {
            id = -1;
            vehicleHandle = null;
            vehicleData = null;
        }

        public static Vehicle createNewCar(string vehname, Vector3 position, float heading, int color1, int color2, string numberplate, string owner, bool locked = true, bool engine = false, bool addlist = true, uint dimension = 0, VehicleData vehicleData = null, bool insert = false, bool insert2 = true, int garage = -1)
        {
            try
            {
                Cars car = new Cars();
                if (vehicleData == null)
                {
                    if (addlist == true)
                    {
                        car.id = 7500 + 1;
                    }
                    else
                    {
                        car.id = -1;
                    }
                    uint vehash = NAPI.Util.GetHashKey(vehname.ToLower());
                    car.vehicleHandle = NAPI.Vehicle.CreateVehicle(vehash, position, heading, color1, color2);
                    car.vehicleHandle.Dimension = dimension;
                    if (numberplate != "n/A" && numberplate.Length > 0)
                    {
                        car.vehicleHandle.NumberPlate = numberplate;
                    }
                    car.vehicleHandle.Locked = locked;
                    car.vehicleHandle.SetData<string>("Vehicle:Owner", owner);
                    car.vehicleHandle.SetSharedData("Vehicle:Name", vehname.ToLower());
                    SpedVehicles spedVehicle = Helper.GetSpedVehicleByModel(vehname.ToLower());
                    if (spedVehicle == null)
                    {
                        car.vehicleHandle.SetData<int>("Vehicle:Jobid", 0);
                    }
                    else
                    {
                        car.vehicleHandle.SetData<int>("Vehicle:Jobid", spedVehicle.id);
                    }
                    car.vehicleHandle.SetData<int>("Vehicle:Products", 0);
                    car.vehicleHandle.SetData<int>("Vehicle:Tuev", Helper.UnixTimestamp() + (93 * 86400));
                    car.vehicleHandle.SetData<Vector3>("Vehicle:Position", position);
                    car.vehicleHandle.SetData<float>("Vehicle:Rotation", heading);
                    car.vehicleHandle.SetSharedData("Vehicle:Speedlimit", 0);
                    car.vehicleHandle.SetData<int>("Vehicle:VLock", 2);
                    if (car.vehicleHandle.Class != 13)
                    {
                        car.vehicleHandle.SetSharedData("Vehicle:MaxFuel", GetVehicleFuel(car.vehicleHandle));
                        car.vehicleHandle.SetSharedData("Vehicle:Fuel", GetVehicleFuel(car.vehicleHandle));
                        car.vehicleHandle.SetSharedData("Vehicle:Oel", 100);
                        car.vehicleHandle.SetSharedData("Vehicle:Battery", 100);
                    }
                    else
                    {
                        car.vehicleHandle.SetSharedData("Vehicle:MaxFuel", 0.0f);
                        car.vehicleHandle.SetSharedData("Vehicle:Fuel", 0.0f);
                        car.vehicleHandle.SetSharedData("Vehicle:Oel", 0);
                        car.vehicleHandle.SetSharedData("Vehicle:Battery", 0);
                    }
                    car.vehicleHandle.SetSharedData("Vehicle:MaxSpeed", GetVehicleMaxSpeed(car.vehicleHandle));
                    car.vehicleHandle.SetSharedData("Vehicle:Kilometre", 0.0f);
                    string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehicleSecondaryColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehiclePearlescentColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehicleWheelColor(car.vehicleHandle)}";
                    car.vehicleHandle.SetSharedData("Vehicle:Color", color);
                    Helper.SetVehicleEngine(car.vehicleHandle, engine);
                    if (owner != "Anonym")
                    {
                        if (car.vehicleHandle.NumberPlate == "Autohaus")
                        {
                            car.vehicleHandle.SetSharedData("Vehicle:Sync", $"0,0,0,0,0,1|{car.vehicleHandle.Position.Z.ToString(new CultureInfo("en-US"))},0");
                        }
                        else
                        {
                            car.vehicleHandle.SetSharedData("Vehicle:Sync", "0,0,0,0,0,0,0");
                        }
                        car.vehicleHandle.SetSharedData("Vehicle:Doors", "[false,false,false,false,false,false]");
                    }
                    else
                    {
                        car.vehicleHandle.SetSharedData("Vehicle:Doors", "[true,true,true,true,true,true]");
                        car.vehicleHandle.SetSharedData("Vehicle:Sync", "0,0,0,1,1,1,0");
                    }
                    car.vehicleHandle.SetSharedData("Vehicle:Windows", "[false,false,false,false]");
                    if (addlist == true)
                    {
                        carList.Add(car);
                    }
                }
                else
                {
                    car.id = vehicleData.id;
                    if (car.id != -1)
                    {
                        if (insert2 == false)
                        {
                            foreach (Cars car2 in Cars.carList)
                            {
                                if (car2.vehicleData != null && car2.vehicleData.id == vehicleData.id)
                                {
                                    car = car2;
                                    break;
                                }
                            }
                        }
                        uint vehash = NAPI.Util.GetHashKey(vehicleData.vehiclename.ToLower());
                        string[] vehiclePosition = new string[7];
                        vehiclePosition = vehicleData.position.Split("|");
                        string[] vehicleColor = new string[4];
                        vehicleColor = vehicleData.color.Split(",");
                        Vector3 postionsVector = new Vector3(float.Parse(vehiclePosition[0], new CultureInfo("en-US")), float.Parse(vehiclePosition[1], new CultureInfo("en-US")), float.Parse(vehiclePosition[2], new CultureInfo("en-US")) + 0.25);
                        car.vehicleHandle = NAPI.Vehicle.CreateVehicle(vehash, postionsVector, float.Parse(vehiclePosition[3], new CultureInfo("en-US")), Convert.ToInt32(vehicleColor[0]), Convert.ToInt32(vehicleColor[1]));
                        string[] vehicleHealth = new string[3];
                        if (!vehicleData.health.Contains("|"))
                        {
                            vehicleData.health = "1000|1000|1000";
                        }
                        vehicleHealth = vehicleData.health.Split("|");
                        if (car.vehicleHandle != null)
                        {
                            if (vehicleHealth.Length >= 3 && vehicleHealth[0].Length > 0 && vehicleHealth[1].Length > 1 && vehicleHealth[2].Length > 0)
                            {
                                NAPI.Vehicle.SetVehicleBodyHealth(car.vehicleHandle, float.Parse(vehicleHealth[0]));
                                NAPI.Vehicle.SetVehicleEngineHealth(car.vehicleHandle, float.Parse(vehicleHealth[1]));
                                NAPI.Vehicle.SetVehicleHealth(car.vehicleHandle, float.Parse(vehicleHealth[2]));
                            }
                            else
                            {
                                NAPI.Vehicle.SetVehicleBodyHealth(car.vehicleHandle, 1000f);
                                NAPI.Vehicle.SetVehicleEngineHealth(car.vehicleHandle, 1000f);
                                NAPI.Vehicle.SetVehicleHealth(car.vehicleHandle, 1000f);
                            }
                            car.vehicleHandle.Dimension = dimension;
                            if (vehicleData.plate != "n/A" && vehicleData.plate.Length > 0)
                            {
                                car.vehicleHandle.NumberPlate = vehicleData.plate;
                            }
                            car.vehicleHandle.Locked = vehicleData.status == 1 ? true : false;
                            car.vehicleHandle.SetData<string>("Vehicle:Owner", vehicleData.owner);
                            car.vehicleHandle.SetSharedData("Vehicle:Name", vehicleData.vehiclename.ToLower());
                            SpedVehicles spedVehicle = Helper.GetSpedVehicleByModel(vehname.ToLower());
                            if (spedVehicle == null)
                            {
                                car.vehicleHandle.SetData<int>("Vehicle:Jobid", 0);
                            }
                            else
                            {
                                car.vehicleHandle.SetData<int>("Vehicle:Jobid", spedVehicle.id);
                            }
                            car.vehicleHandle.SetData<int>("Vehicle:Products", vehicleData.products);
                            car.vehicleHandle.SetData<int>("Vehicle:Tuev", vehicleData.tuev);
                            car.vehicleHandle.SetData<Vector3>("Vehicle:Position", postionsVector);
                            car.vehicleHandle.SetData<float>("Vehicle:Rotation", float.Parse(vehiclePosition[3], System.Globalization.CultureInfo.InvariantCulture));
                            car.vehicleHandle.SetSharedData("Vehicle:Speedlimit", 0);
                            car.vehicleHandle.SetData<int>("Vehicle:VLock", vehicleData.vlock);
                            if (car.vehicleHandle.Class != 13)
                            {
                                car.vehicleHandle.SetSharedData("Vehicle:MaxFuel", GetVehicleFuel(car.vehicleHandle));
                                car.vehicleHandle.SetSharedData("Vehicle:Fuel", vehicleData.fuel);
                                if (vehicleData.fuel == -1)
                                {
                                    car.vehicleHandle.SetSharedData("Vehicle:Fuel", GetVehicleFuel(car.vehicleHandle));
                                    vehicleData.fuel = GetVehicleFuel(car.vehicleHandle);
                                }
                                car.vehicleHandle.SetSharedData("Vehicle:Oel", vehicleData.oel);
                                car.vehicleHandle.SetSharedData("Vehicle:Battery", vehicleData.battery);
                            }
                            else
                            {
                                car.vehicleHandle.SetSharedData("Vehicle:MaxFuel", 0.0f);
                                car.vehicleHandle.SetSharedData("Vehicle:Fuel", 0.0f);
                                if (vehicleData.fuel == -1)
                                {
                                    car.vehicleHandle.SetSharedData("Vehicle:Fuel", 0.0f);
                                    vehicleData.fuel = 0.0f;
                                }
                                car.vehicleHandle.SetSharedData("Vehicle:Oel", 0);
                                car.vehicleHandle.SetSharedData("Vehicle:Battery", 0);
                            }
                            car.vehicleHandle.SetSharedData("Vehicle:MaxSpeed", GetVehicleMaxSpeed(car.vehicleHandle));
                            car.vehicleHandle.SetSharedData("Vehicle:Kilometre", vehicleData.kilometre);
                            if (vehicleData.tuning.Length > 10 && vehicleData.tuning != "n/A")
                            {
                                car.vehicleHandle.SetSharedData("Vehicle:Tuning", vehicleData.tuning);
                                string[] vehicleTuning = new string[69];
                                vehicleTuning = vehicleData.tuning.Split(",");
                                TuningController.OnTuningPreview(null, 53, Convert.ToInt32(vehicleTuning[53]), false, car.vehicleHandle);
                                TuningController.OnTuningPreview(null, 56, Convert.ToInt32(vehicleTuning[56]), false, car.vehicleHandle);
                                TuningController.OnTuningPreview(null, 60, Convert.ToInt32(vehicleTuning[60]), false, car.vehicleHandle);
                                TuningController.OnTuningPreview(null, 66, Convert.ToInt32(vehicleTuning[66]), false, car.vehicleHandle);
                                TuningController.OnTuningPreview(null, 67, Convert.ToInt32(vehicleTuning[67]), false, car.vehicleHandle);
                                TuningController.OnTuningPreview(null, 68, Convert.ToInt32(vehicleTuning[68]), false, car.vehicleHandle);
                                TuningController.OnTuningPreview(null, 69, Convert.ToInt32(vehicleTuning[69]), false, car.vehicleHandle);
                            }
                            else
                            {
                                car.vehicleHandle.SetSharedData("Vehicle:Tuning", "n/A");
                            }
                            string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehicleSecondaryColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehiclePearlescentColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehicleWheelColor(car.vehicleHandle)}";
                            car.vehicleHandle.SetSharedData("Vehicle:Color", color);
                            Helper.SetVehicleEngine(car.vehicleHandle, vehicleData.engine == 1 ? true : false);
                            car.vehicleHandle.SetSharedData("Vehicle:Sync", vehicleData.sync);
                            if (vehicleData.sync.Split(",")[5] != "0")
                            {
                                string[] vehicleArray = new string[7];
                                vehicleArray = vehicleData.sync.Split(",");
                                car.vehicleHandle.SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},0,{vehicleArray[6]}");
                                vehicleData.sync = car.vehicleHandle.GetSharedData<string>("Vehicle:Sync");
                            }
                            car.vehicleHandle.SetData<bool>("Vehicle:EngineStatus", vehicleData.engine == 1 ? true : false);
                            car.vehicleHandle.SetData<int>("Vehicle:Tuev", vehicleData.tuev);
                            vehicleData.doors = vehicleData.doors != null ? vehicleData.doors : "[false,false,false,false,false,false]";
                            vehicleData.windows = vehicleData.windows != null ? vehicleData.windows : "[false,false,false,false]";
                            car.vehicleHandle.SetSharedData("Vehicle:Doors", vehicleData.doors);
                            car.vehicleHandle.SetSharedData("Vehicle:Windows", vehicleData.windows);
                            if (garage != -1)
                            {
                                if (garage == 31)
                                {
                                    vehicleData.garage = "bizz-34";
                                }
                                else if (garage == 32)
                                {
                                    vehicleData.garage = "bizz-35";
                                }
                                else
                                {
                                    vehicleData.garage = "bizz-33";
                                }
                            }
                            if (garage != -1 || vehicleData.garage != "n/A")
                            {
                                Helper.SetVehicleEngine(car.vehicleHandle, false);
                                car.vehicleHandle.Dimension = 150;
                            }
                        }
                        if (insert == true)
                        {
                            PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                            db.Insert(vehicleData);
                        }
                        car.vehicleData = vehicleData;
                    }
                    if (insert2 == true)
                    {
                        carList.Add(car);
                    }
                }
                if (car != null && vehicleData != null)
                {
                    car.vehicleData = vehicleData;
                    return car.vehicleHandle;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[createNewCar]: " + e.ToString());
            }
            return null;
        }

        public static string GetTuev(int tuev)
        {
            try
            {
                if (tuev == 2)
                {
                    return "Vorhanden";
                }
                else if (tuev > 2 && tuev >= Helper.UnixTimestamp())
                {
                    return $"Vorhanden - {Helper.UnixTimeStampToDateTime(tuev)}";
                }
                else
                {
                    return "Nicht vorhanden";
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetTuev]: " + e.ToString());
            }
            return "Nicht vorhanden";
        }

        public static bool IsCarInGarage(string identifier)
        {
            try
            {
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleData != null && car.vehicleData.garage == identifier)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsCarInGarage]: " + e.ToString());
            }
            return false;
        }

        public static bool CanCarInGarage(string identifier, int klasse)
        {
            try
            {
                int count = 0;
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleData != null && car.vehicleData.garage == identifier)
                    {
                        count++;
                    }
                }
                if (klasse == 0 && count >= 2) return false;
                if (klasse == 1 && count >= 4) return false;
                if (klasse == 2 && count >= 6) return false;
                if (klasse >= 3 && count >= 8) return false;
                return true;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsCarInGarage]: " + e.ToString());
            }
            return false;
        }

        public static int GetVehicleMaxSpeed(Vehicle vehicle)
        {
            int speed = 0;
            try
            {
                foreach (VehicleShop vehicleShop in DealerShipController.dealerShipList)
                {
                    if (vehicleShop.vehiclename.ToLower() == vehicle.GetSharedData<string>("Vehicle:Name").ToLower())
                    {
                        return vehicleShop.maxspeed;
                    }
                }
                if (speed == 0)
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetVehicleMaxSpeed]: " + e.ToString());
            }
            return speed;
        }

        public static int GetVehicleTrunk(Vehicle vehicle)
        {
            if (vehicle == null) return 0;
            int trunk = 15;
            if (vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("trailer") || vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("tanker"))
            {
                trunk = 200;
                return trunk;
            }
            if (vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("stockade"))
            {
                trunk = 85;
                return trunk;
            }
            if (vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("sweeper"))
            {
                trunk = 0;
                return trunk;
            }
            switch (vehicle.Class)
            {
                case 0:
                    {
                        trunk = 35;
                        break;
                    }
                case 1:
                    {
                        trunk = 20;
                        break;
                    }
                case 2:
                    {
                        trunk = 45;
                        break;
                    }
                case 3:
                    {
                        trunk = 25;
                        break;
                    }
                case 4:
                    {
                        trunk = 20;
                        break;
                    }
                case 5:
                    {
                        trunk = 20;
                        break;
                    }
                case 6:
                    {
                        trunk = 20;
                        break;
                    }
                case 7:
                    {
                        trunk = 15;
                        break;
                    }
                case 8:
                    {
                        trunk = 5;
                        break;
                    }
                case 9:
                    {
                        trunk = 20;
                        break;
                    }
                case 10:
                    {
                        trunk = 100;
                        break;
                    }
                case 11:
                    {
                        trunk = 90;
                        break;
                    }
                case 12:
                    {
                        trunk = 85;
                        break;
                    }
                case 13:
                    {
                        trunk = 0;
                        break;
                    }
                case 14:
                    {
                        trunk = 55;
                        break;
                    }
                case 15:
                    {
                        trunk = 60;
                        break;
                    }
                case 16:
                    {
                        trunk = 60;
                        break;
                    }
                case 17:
                    {
                        trunk = 100;
                        break;
                    }
                case 18:
                    {
                        trunk = 35;
                        break;
                    }
                case 19:
                    {
                        trunk = 50;
                        break;
                    }
                case 20:
                    {
                        trunk = 100;
                        break;
                    }
                case 21:
                    {
                        trunk = 150;
                        break;
                    }
                case 22:
                    {
                        trunk = 5;
                        break;
                    }
            }
            return trunk;
        }

        public static float GetVehicleFuel(Vehicle vehicle)
        {
            if (vehicle == null) return 0;
            float fuel = 75.0f;
            if (vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("trailer") || vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("tanker"))
            {
                fuel = 0;
                return fuel;
            }
            if (vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("sweeper"))
            {
                fuel = 25;
                return fuel;
            }
            switch (vehicle.Class)
            {
                case 0:
                    {
                        fuel = 55.0f;
                        break;
                    }
                case 1:
                    {
                        fuel = 75.0f;
                        break;
                    }
                case 2:
                    {
                        fuel = 100.0f;
                        break;
                    }
                case 3:
                    {
                        fuel = 65.0f;
                        break;
                    }
                case 4:
                    {
                        fuel = 60.0f;
                        break;
                    }
                case 5:
                    {
                        fuel = 85.0f;
                        break;
                    }
                case 6:
                    {
                        fuel = 80.0f;
                        break;
                    }
                case 7:
                    {
                        fuel = 75.0f;
                        break;
                    }
                case 8:
                    {
                        fuel = 30.0f;
                        break;
                    }
                case 9:
                    {
                        fuel = 80.0f;
                        break;
                    }
                case 10:
                    {
                        fuel = 150.0f;
                        break;
                    }
                case 11:
                    {
                        fuel = 65.0f;
                        break;
                    }
                case 12:
                    {
                        fuel = 60.0f;
                        break;
                    }
                case 13:
                    {
                        fuel = 0.0f;
                        break;
                    }
                case 14:
                    {
                        fuel = 125.0f;
                        break;
                    }
                case 15:
                    {
                        fuel = 175.0f;
                        break;
                    }
                case 16:
                    {
                        fuel = 215.0f;
                        break;
                    }
                case 17:
                    {
                        fuel = 125.0f;
                        break;
                    }
                case 18:
                    {
                        fuel = 125.0f;
                        break;
                    }
                case 19:
                    {
                        fuel = 125.0f;
                        break;
                    }
                case 20:
                    {
                        fuel = 150.0f;
                        break;
                    }
                case 21:
                    {
                        fuel = 315.0f;
                        break;
                    }
                case 22:
                    {
                        fuel = 25.0f;
                        break;
                    }
            }
            return fuel;
        }
    }
}
