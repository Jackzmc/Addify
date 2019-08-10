using Addify.lib;
using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Addify
{
    internal class VehicleMenu : MenuItem {
        static UIMenuItem menu_repair = new UIMenuItem("Repair", "Repair current car");
        static UIMenuCheckboxItem menu_godmode = new UIMenuCheckboxItem("Vehicle God", false);
        static UIMenuCheckboxItem menu_boost = new UIMenuCheckboxItem("Boost", true);
        static UIMenuCheckboxItem menu_bullet_proof_tires = new UIMenuCheckboxItem("Bulletproof Tires", true);
        static UIMenuCheckboxItem menu_show_health = new UIMenuCheckboxItem("Show Health", true);
        static UIMenuCheckboxItem menu_speed = new UIMenuCheckboxItem("Speedometer", true);
        static UIMenuCheckboxItem menu_spawner_spawninveh = new UIMenuCheckboxItem("Spawn in Vehicle", true);
        static UIMenuCheckboxItem menu_spawner_showblip = new UIMenuCheckboxItem("Add Blip for Spawned Vehicles", true);
        static UIMenuListItem menu_speed_type;

        static readonly VehicleHash[] vehicles = (VehicleHash[])Enum.GetValues(typeof(VehicleHash));
        static readonly List<dynamic> speedTypes = new List<dynamic>() { "MPH", "KPH" };

        private VehicleMenu_DoorControl doorControl;


        private readonly int SPEED_X = UI.WIDTH * 1/5;
        private readonly int SPEED_Y = UI.HEIGHT - 50;
        private readonly System.Drawing.Point SPEED_POINT;


        protected internal VehicleMenu() : base("Vehicle Options")
        {
            SPEED_POINT = new System.Drawing.Point(SPEED_X, SPEED_Y);
            menu_speed_type = new UIMenuListItem("Display", speedTypes, 0);


            var spawn_menu = Main.Pool.AddSubMenu(menu, "Vehicles >>");
            doorControl = new VehicleMenu_DoorControl(Main.Pool.AddSubMenu(menu, "Door Control >>"));
            spawn_menu.AddItem(menu_spawner_spawninveh);
            spawn_menu.AddItem(menu_spawner_showblip);
            foreach(SortedVehicleGroup group in SortedVehicleGroup.GetVehicleGroups())
            {
                var submenu = Main.Pool.AddSubMenu(spawn_menu, group.Name);
                foreach(KeyValuePair<string,VehicleHash> pair in group.Vehicles)
                {
                    var _menu = new UIMenuItem(pair.Key, $"Spawn a {pair.Key}");
                    _menu.Activated += (sender, args) =>
                    {
                        Vehicle veh = GTA.World.CreateVehicle(pair.Value, playerPed.Position.Around(5));
                        if (menu_spawner_showblip.Checked)
                        {
                            Blip b = veh.AddBlip();
                            b.IsFriendly = true;
                            b.IsShortRange = true;
                            b.Name = String.Format("Spawned Car {0}", Enum.GetName(typeof(VehicleHash), (VehicleHash)veh.Model.Hash).ToString());
                            b.Sprite = BlipSprite.PersonalVehicleCar;
                        }
                        if (menu_spawner_spawninveh.Checked)
                        {
                            playerPed.SetIntoVehicle(veh, VehicleSeat.Driver);
                        }
                    };
                    submenu.AddItem(_menu);
                }
            }

            menu.AddItem(menu_godmode);
            menu.AddItem(menu_bullet_proof_tires);
            menu.AddItem(menu_repair);
            menu.AddItem(menu_show_health);
            menu.AddItem(menu_speed);
            menu.AddItem(menu_speed_type);
            menu.AddItem(menu_boost);
        }
        internal override void update()
        {
            base.update();
            Ped playerPed = Game.Player.Character;
            if(playerPed.IsInVehicle() && playerVehicle != null)
            {
                if(menu_speed.Checked)
                {
                    bool isMPH = speedTypes[menu_speed_type.Index] == "MPH";
                    int speed = isMPH ?  (int)(playerVehicle.Speed * 2.236936f) : (int)(playerVehicle.Speed * 3.6f);
                    string type = isMPH ? "mph" : "kph";
                    UIText speedText = new UIText($"{speed} {type}", SPEED_POINT,.5f);
                    speedText.Draw();
                }
                if (playerVehicle.CanTiresBurst == menu_bullet_proof_tires.Checked)
                {
                    playerVehicle.CanTiresBurst = !menu_bullet_proof_tires.Checked;
                }
                
            }
            if (menu_show_health.Checked)
            {
                Vehicle veh = playerPed.IsInVehicle() ? playerPed.CurrentVehicle : playerPed.LastVehicle;
                if (veh != null)
                {
                    var text = String.Format("General HP: ~o~{0}~w~\nEngine HP:  ~o~{1}~w~\nBody HP:  ~o~{2}~w~\nPetrol HP:  ~o~{3}~w~",
                        (int)veh.Health,
                        (int)veh.EngineHealth,
                        (int)veh.BodyHealth,
                        (int)veh.PetrolTankHealth
                    );
                    Util.DrawSimpleText(text, UI.WIDTH, UI.HEIGHT, .3f);
                }
            }
        }

        internal override void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
        {
            if (checkbox == menu_godmode)
            {
                bool godmode = menu_godmode.Checked;
                if (playerPed.IsInVehicle())
                {
                    playerVehicle.IsBulletProof = godmode;
                    playerVehicle.IsExplosionProof = godmode;
                    playerVehicle.IsFireProof = godmode;
                    playerVehicle.IsMeleeProof = godmode;
                    playerVehicle.IsInvincible = godmode;
                    playerVehicle.CanBeVisiblyDamaged = !godmode;
                    playerVehicle.CanTiresBurst = !godmode;
                }
            } else if(checkbox == menu_bullet_proof_tires)
            {
                if(menu_bullet_proof_tires.Checked && playerPed.IsInVehicle())
                {
                    for(int i=0;i<4;i++) playerVehicle.FixTire(i);
                }
            }
            UI.ShowSubtitle("Key: " + Main.Config.GetValue("Config", "Menu", Keys.F9).ToString());
        }
        internal override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if (item == menu_repair)
            {
                if (playerPed.IsInVehicle())
                {
                    if(playerPed.CurrentVehicle.IsDamaged)
                    {
                        playerPed.CurrentVehicle.Repair();
                        playerPed.CurrentVehicle.IsDriveable = true;
                    }
                }
            }
            /*else if (item == menu_vehicles)
            {
                VehicleHash hash = (VehicleHash)vehicles[menu_vehicles.Index];
                Vehicle veh = GTA.World.CreateVehicle(hash, playerPed.Position.Around(5));
                Blip b = veh.AddBlip();
                b.IsFriendly = true;
                b.IsShortRange = true;
                b.Name = String.Format("Spawned Car {0}", Enum.GetName(typeof(VehicleHash), (VehicleHash)veh.Model.Hash).ToString());
                b.Sprite = BlipSprite.PersonalVehicleCar;
                playerPed.SetIntoVehicle(veh,VehicleSeat.Driver);
            }*/
        }
        bool VEH_ISHAZARD = false;
        bool VEH_LBLINK = false;
        bool VEH_RBLINK = false;
        internal override void onKeyDown(object sender, KeyEventArgs e)
        {
            //vehicle blinkers
            if (playerPed.IsInVehicle())
            {
                if(!Main.Pool.IsAnyMenuOpen())
                {
                    if (e.KeyCode == Keys.Left && !e.Shift)
                    {
                        VEH_LBLINK = !VEH_LBLINK;
                        VEH_RBLINK = false;
                        playerPed.CurrentVehicle.LeftIndicatorLightOn = VEH_LBLINK;
                        playerPed.CurrentVehicle.RightIndicatorLightOn = false;
                    }
                    else if (e.KeyCode == Keys.Right && !e.Shift)
                    {
                        VEH_RBLINK = !VEH_RBLINK;
                        VEH_LBLINK = false;
                        playerPed.CurrentVehicle.LeftIndicatorLightOn = false;
                        playerPed.CurrentVehicle.RightIndicatorLightOn = VEH_RBLINK;
                    }
                    if (VEH_ISHAZARD) VEH_ISHAZARD = false;
                }
                
                if (e.KeyCode == Keys.OemSemicolon)
                {
                    playerPed.CurrentVehicle.StartAlarm();
                }
                else if (e.KeyCode == Keys.OemPipe)
                {
                    if(VEH_LBLINK) VEH_LBLINK = false;
                    if (VEH_RBLINK) VEH_RBLINK = false;
                    if(VEH_ISHAZARD)
                    {
                        playerPed.CurrentVehicle.LeftIndicatorLightOn = false;
                        playerPed.CurrentVehicle.RightIndicatorLightOn = false;
                        VEH_ISHAZARD = false;

                    }
                    else
                    {
                        playerPed.CurrentVehicle.LeftIndicatorLightOn = true;
                        playerPed.CurrentVehicle.RightIndicatorLightOn = true;
                        VEH_ISHAZARD = true;
                    }

                }
            }
            else if (e.KeyCode == Keys.NumPad9 && playerPed.IsInVehicle() && menu_boost.Checked)
            {
                playerPed.CurrentVehicle.ApplyForce(playerPed.ForwardVector * 3);
            }
        }
    }
}
