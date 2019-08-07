using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Addify
{
    class VehicleMenu : MenuItem {
        static UIMenuItem menu_repair = new UIMenuItem("Repair", "Repair current car");
        static UIMenuCheckboxItem menu_godmode = new UIMenuCheckboxItem("Vehicle God", false);
        static UIMenuCheckboxItem menu_boost = new UIMenuCheckboxItem("Boost", true);
        static UIMenuCheckboxItem menu_bullet_proof_tires = new UIMenuCheckboxItem("Bulletproof Tires", true);
        static UIMenuCheckboxItem menu_show_health = new UIMenuCheckboxItem("Show Health", true);
        static UIMenuCheckboxItem menu_speed = new UIMenuCheckboxItem("Speedometer", true);
        static UIMenuListItem menu_speed_type;

        static readonly VehicleHash[] vehicles = (VehicleHash[])Enum.GetValues(typeof(VehicleHash));
        static readonly List<dynamic> speedTypes = new List<dynamic>() { "MPH", "KPH" };


        private readonly int SPEED_X = UI.WIDTH * 1/5;
        private readonly int SPEED_Y = UI.HEIGHT - 50;
        private readonly System.Drawing.Point SPEED_POINT;


        public VehicleMenu(UIMenu menu) : base(menu)
        {
            SPEED_POINT = new System.Drawing.Point(SPEED_X, SPEED_Y);
            menu_speed_type = new UIMenuListItem("Display", speedTypes, 0);
            var spawn_menu = Main.Pool.AddSubMenu(menu, "Spawn Vehicles");
            foreach(VehicleHash hash in vehicles)
            {
                var name = Enum.GetName(typeof(VehicleHash), hash).ToString();
                var _item = new UIMenuItem(name);
                _item.Activated += (sender, args) =>
                {
                    Vehicle veh = GTA.World.CreateVehicle(hash, playerPed.Position.Around(5));
                    Blip b = veh.AddBlip();
                    b.IsFriendly = true;
                    b.IsShortRange = true;
                    b.Name = String.Format("Spawned Car {0}", Enum.GetName(typeof(VehicleHash), (VehicleHash)veh.Model.Hash).ToString());
                    b.Sprite = BlipSprite.PersonalVehicleCar;
                    playerPed.SetIntoVehicle(veh, VehicleSeat.Driver);
                };
                spawn_menu.AddItem(_item);
            }

            menu.AddItem(menu_godmode);
            menu.AddItem(menu_bullet_proof_tires);
            menu.AddItem(menu_repair);
            menu.AddItem(menu_show_health);
            menu.AddItem(menu_speed);
            menu.AddItem(menu_speed_type);
            menu.AddItem(menu_boost);
        }
        public override void update()
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

        public override void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
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
            UI.ShowSubtitle("Key: " + Main.config.GetValue("Config", "Menu", Keys.F9).ToString());
        }
        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
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
        public override void onKeyDown(object sender, KeyEventArgs e)
        {
            //vehicle blinkers
            Ped playerPed = Game.Player.Character;
            if (playerPed.IsInVehicle())
            {
                if (e.KeyCode == Keys.Oemcomma && e.Shift)
                {
                    playerPed.CurrentVehicle.LeftIndicatorLightOn = true;
                    playerPed.CurrentVehicle.RightIndicatorLightOn = false;
                }
                else if (e.KeyCode == Keys.OemPeriod && e.Shift)
                {
                    playerPed.CurrentVehicle.LeftIndicatorLightOn = false;
                    playerPed.CurrentVehicle.RightIndicatorLightOn = true;
                }
                else if (e.KeyCode == Keys.OemSemicolon)
                {
                    playerPed.CurrentVehicle.StartAlarm();
                }
                else if (e.KeyCode == Keys.OemPipe)
                {
                    playerPed.CurrentVehicle.LeftIndicatorLightOn = true;
                    playerPed.CurrentVehicle.RightIndicatorLightOn = true;
                }
            }
            else if (e.KeyCode == Keys.NumPad9 && playerPed.IsInVehicle() && menu_boost.Checked)
            {
                playerPed.CurrentVehicle.ApplyForce(playerPed.ForwardVector * 3);
            }
        }
    }
}
