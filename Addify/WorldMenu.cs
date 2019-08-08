using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;

namespace Addify {
    class WorldMenu : MenuItem {

        static Random rnd = new Random();

        static UIMenuCheckboxItem menu_blackout = new UIMenuCheckboxItem("Blackout", false);
        static UIMenuCheckboxItem menu_no_helis = new UIMenuCheckboxItem("No Police Helicopters", true);
        static UIMenuCheckboxItem menu_angry_cops = new UIMenuCheckboxItem("Cop Forcefield", true);
        static UIMenuCheckboxItem menu_angry_cop_cars = new UIMenuCheckboxItem("Suicidal Cops Cars", false);
        
        static UIMenuItem menu_add_ped = new UIMenuItem("Add Bodyguard");

        private static WorldMenu_Train trainMenu;
        private static WorldMenu_Weather weatherMenu;
        private static WorldMenu_Traffic trafficMenu;

        
        private readonly PedHash[] POLICE_MODELS = new PedHash[]
        {
            PedHash.Cop01SFY, PedHash.Cop01SMY, PedHash.Swat01SMY, PedHash.UndercoverCopCutscene, PedHash.Sheriff01SFY, PedHash.Sheriff01SMY, PedHash.SecuroGuardMale01, PedHash.Security01SMM, PedHash.Marine01SMM, PedHash.Marine01SMY, PedHash.Marine02SMM, PedHash.Marine02SMY, PedHash.Marine03SMY
        };
        

        //static bool no_police_helis = false;


        public WorldMenu (UIMenu menu) : base(menu) {
            //set list items
            
            //menu_weather = new UIMenuListItem("Weather", weatherList, 0);
            var timeweathermenu = Main.Pool.AddSubMenu(menu, "Time / Weather");
            var trainmenu = Main.Pool.AddSubMenu(menu, "Train");
            var trafficmenu = Main.Pool.AddSubMenu(menu, "Traffic");
            weatherMenu = new WorldMenu_Weather(timeweathermenu);
            trafficMenu = new WorldMenu_Traffic(trafficmenu);
            trainMenu = new WorldMenu_Train(trainmenu);
            menu.AddItem(menu_blackout);
            menu.AddItem(menu_no_helis);
            menu.AddItem(menu_add_ped);
            
            menu.AddItem(menu_angry_cops);
            menu.AddItem(menu_angry_cop_cars);
        }
        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            /*switch(item)
                {
                    case menu_weather:
                    case menu_time_hr:
                    case menu_time_min:
                    case menu_timeInc:
                    case menu_timeDec:
                        break;
                }*/
            
            if (item == menu_add_ped)
            {
                Ped ped = GTA.World.CreatePed(PedHash.Chef, Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0)));
                ped.MaxDrivingSpeed = 200;
                ped.Armor = 100;
                ped.Weapons.Give(WeaponHash.PumpShotgun, 500, true, true);
                ped.Task.ReloadWeapon();
                GTA.Native.Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, Game.Player.Character, ped, Relationship.Companion.GetHashCode());
                /*Ped[] targets = World.GetNearbyPeds(Game.Player.WantedCenterPosition, 50);
                for(int i=0;i<targets.Length;i++)
                {
                    GTA.World.
                    if(targets[i].)
                }*/

            }
        }
        public override void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
        {
            if(checkbox == menu_blackout)
            {
                GTA.World.SetBlackout(menu_blackout.Checked);
            }
        }
        public override void update() {
            base.update();
            weatherMenu.update();
            trainMenu.update();
            
            if(menu_no_helis.Checked)
            {
                Vehicle[] nearby = World.GetNearbyVehicles(Game.Player.Character.Position,200,"polmav");
                for(int i=0;i<nearby.Length;i++)
                {
                    Vehicle veh = nearby[i];

                    if(veh.IsAlive && veh != Game.Player.Character.CurrentVehicle)
                    {
                        Ped driver = veh.Driver;
                        if (driver.IsInPoliceVehicle)
                        {
                            veh.EngineRunning = false;
                            driver.Kill();
                        }

                        for (int k = 0;k< veh.Passengers.Length;k++)
                        {
                            veh.Passengers[k].Delete();
                        }
                        
                        //GTA.Math.Vector3 new_direction = new GTA.Math.Vector3(veh.ForwardVector.X, veh.ForwardVector.Y, veh.ForwardVector.Z - 1);
                        //veh.ApplyForce(new_direction, veh.Rotation);
                    }
                }
            }
           
            if (player.WantedLevel > 0) {
                if(menu_angry_cop_cars.Checked )
                {
                    Vehicle[] nearby = World.GetNearbyVehicles(Game.Player.Character.Position, 150);
                    for (int i = 0; i < nearby.Length; i++)
                    {
                        Vehicle veh = nearby[i];
                        float distance = veh.Position.DistanceTo(Game.Player.Character.Position);

                        if (veh.IsAlive && veh != Game.Player.Character.CurrentVehicle)
                        {
                            Ped driver = veh.Driver;
                            if (driver.IsInPoliceVehicle)
                            {
                                //driver.Kill();
                                //veh.Speed = 100;
                                driver.Task.VehicleChase(Game.Player.Character);
                                driver.DrivingSpeed = 110;
                                //driver.Task.DriveTo(veh,Game.Player.Character.Position,5,120,8388614);
                                veh.LockStatus = VehicleLockStatus.Locked;
                                veh.SoundHorn(1);
                                if (distance <= 30 && driver.IsAlive)
                                {
                                    driver.MaxDrivingSpeed = 160;
                                    driver.MaxSpeed = 160;
                                    veh.MaxSpeed = 160;
                                    veh.Speed = 130;
                                    driver.DrivingSpeed = 150;
                                    //veh.EngineRunning = false;
                                    //driver.Kill();
                                    if (!veh.IsTireBurst(0)) veh.BurstTire(0);
                                }

                                //veh.ApplyForceRelative(new GTA.Math.Vector3(0, 0, -2), new GTA.Math.Vector3(0, 0, 0));
                            }
                            //GTA.Math.Vector3 new_direction = new GTA.Math.Vector3(veh.ForwardVector.X, veh.ForwardVector.Y, veh.ForwardVector.Z - 1);
                            //veh.ApplyForce(new_direction, veh.Rotation);
                        }
                    }
                } 
                if(menu_angry_cops.Checked)
                {
                    Ped[] nearby_peds = World.GetNearbyPeds(playerPed.Position, 45);
                    for (int i = 0; i < nearby_peds.Length; i++)
                    {
                        Ped cop = nearby_peds[i];
                        if(POLICE_MODELS.Contains((PedHash)cop.Model.Hash))
                        {
                            if (!cop.IsInVehicle() && cop.IsInCombatAgainst(playerPed))
                            {
                                if (rnd.Next(100) < .1)
                                {
                                    cop.Delete();
                                    World.CreatePed(PedHash.Trevor, cop.Position);
                                }
                                else
                                {
                                    cop.Kill();

                                }
                            }
                        }
                        
                    }
                }
                
            }

        }
    }
}
