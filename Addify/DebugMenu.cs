using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Addify
{
    class DebugMenu : MenuItem
    {
        static UIMenuCheckboxItem menu_npc_model = new UIMenuCheckboxItem("Get NPC Model", false);
        static UIMenuCheckboxItem menu_veh_model = new UIMenuCheckboxItem("Get Vehicle Models", false);
        static UIMenuCheckboxItem menu_mouse_loc = new UIMenuCheckboxItem("Tog Mouse Loc", false);
        static UIMenuCheckboxItem menu_get_targeted_ent = new UIMenuCheckboxItem("Get Targeted Ent", false);
        static UIMenuCheckboxItem menu_fps_counter = new UIMenuCheckboxItem("FPS Counter", true);
        static UIMenuCheckboxItem menu_get_loc = new UIMenuCheckboxItem("Get Location Hotkey", true);
        static UIMenuCheckboxItem menu_generic_toggle = new UIMenuCheckboxItem("Generic Toggle", false);

        static UIMenuItem menu_generic_click = new UIMenuItem("Generic Click");
        static UIMenuItem menu_mouse_loc_action = new UIMenuItem("Get Mouse Loc");


        static readonly VehicleHash[] TRAIN_MODELS = new VehicleHash[] {
            VehicleHash.MetroTrain, VehicleHash.Freight, VehicleHash.FreightCar, VehicleHash.FreightCont1, VehicleHash.FreightCont2,  VehicleHash.FreightGrain, VehicleHash.FreightTrailer
        };
        private readonly int BASE_X = UI.WIDTH /2;
        private readonly int BASE_Y = 120;

        public DebugMenu() : base("Debug Menu")
        {
            AddMenus(
                menu_npc_model,
                menu_veh_model,
                menu_get_targeted_ent,
                menu_mouse_loc,
                menu_mouse_loc_action,
                menu_get_loc,
                menu_fps_counter,
                menu_generic_click,
                menu_generic_toggle
            );
        }
        internal override void onKeyDown(object sender, KeyEventArgs e)
        {
            if(menu_get_loc.Checked)
            {
                if (e.KeyCode == Keys.OemQuestion)
                {
                    Vector3 loc = (Game.IsWaypointActive) ? Util.GetWaypoint() : Game.Player.Character.Position;
                    UI.Notify(String.Format("Location {3}: {0} {1} {2}", loc.X, loc.Y, loc.Z,Game.IsWaypointActive?"[Waypoint]":""));
                }
            }
            
        }
        internal override void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
        {
            if(checkbox == menu_npc_model)
            {
                menu_veh_model.Checked = false;
            }else if (checkbox == menu_veh_model)
            {
                menu_npc_model.Checked = false;
            }
        }
        internal override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if(item == menu_mouse_loc_action)
            {
                UI.ShowSubtitle($"X: {Cursor.Position.X} | y: {Cursor.Position.Y}",5000);
            }else if(item == menu_generic_click)
            {
                
            }
        }
        int frame_counter = 0;
        int last_fps = 0;
        internal override void update()
        {
            base.update();
            frame_counter++;
            if (menu_fps_counter.Checked ) {
                Util.DrawSimpleText($"{last_fps} fps", UI.WIDTH-50, 0,.4f, System.Drawing.Color.Green);
                if (frame_counter > last_fps)
                {
                    last_fps = (int)Game.FPS;
                    frame_counter = 0;
                }
            }
            if (menu_mouse_loc.Checked)
            {

                UI.ShowSubtitle($"X: {Cursor.Position.X} | y: {Cursor.Position.Y}");
            }
            if(menu_get_targeted_ent.Checked)
            {
                Entity ent = player.GetTargetedEntity();
                if(ent != null && ent.Model != null)
                {
                    UI.Notify($"ent: {ent?.Model.ToString()}");
                }
            }
            if (menu_npc_model.Checked)
            {
                float closest_distance = 60;
                Ped ped = null;
                Ped[] nearby_peds = World.GetNearbyPeds(playerPed.Position, closest_distance);
                for(int i=0;i<nearby_peds.Length;i++)
                {
                    if (nearby_peds[i].IsPlayer) continue;
                    if (nearby_peds[i].IsDead) continue;
                    float distance = nearby_peds[i].Position.DistanceTo(playerPed.Position);
                    if(distance < closest_distance) {
                        closest_distance = distance;
                        ped = nearby_peds[i];
                    }
                }
                if(ped != null)
                {
                    //ped.AddBlip();
                    string name = Enum.GetName(typeof(PedHash), (PedHash)ped.Model.Hash).ToString();
                    UI.ShowSubtitle($"Ped: {ped.Model} ({name})");
                }
                else
                {
                    UI.ShowSubtitle("Ped: ");
                }
                
            }
            if (menu_veh_model.Checked)
            {
                float closest_distance = 60;
                int closest_id = 0;
                int indexer = 0;
                Vehicle closest_vehicle = null;

                Vehicle[] nearby_vehs = World.GetNearbyVehicles(playerPed.Position, closest_distance);
                foreach(Vehicle vehicle in nearby_vehs)
                {
                    if (vehicle.IsDead) continue;
                    bool is_players = playerPed.IsInVehicle() && playerVehicle.Equals(vehicle);
                    var name = Util.GetVehicleName(vehicle);
                    int speed_value = (int)(vehicle.Speed * 2.236936f);
                    string speed = $"s: ~o~{speed_value,3} mph~w~";
                    int passengers = vehicle.PassengerCount + (vehicle.Driver.Model!=null ? 1 : 0);
                    string stop_status = "~w~l: ~g~driving~w~";
                    if(vehicle.IsStopped)
                    {
                        stop_status = vehicle.IsStoppedAtTrafficLights ? "~w~l: ~r~light~w~" : "~w~l: ~r~stopped~w~";
                    }
                    
                    string driver = vehicle.Driver.Model != null ? $"d: ~o~{Util.GetPedName(vehicle.Driver)}~w~" : "";
                    string hp = $"~w~hp: ~g~ {vehicle.Health}~w~";
                    string engine_hp = $"~w~ehp: ~g~{(int)vehicle.EngineHealth}~w~";
                    string body_hp = $"~w~bhp: ~g~{(int)vehicle.BodyHealth}~w~";
                    if (is_players)
                    {
                        Util.DrawSimpleText($"~o~[P]. ~w~{name} (~g~{vehicle.Model}~w~) {speed} p: ~o~{passengers} {stop_status} {driver} {hp} {engine_hp} {body_hp}", BASE_X, BASE_Y - 22, .35f);
                        continue;
                    }
                    int y = BASE_Y + (indexer * 22);
                    var extra_text = is_players ? "~o~ [P]~r~" : "";
                    Util.DrawSimpleText($"~g~{++indexer,2}. ~w~{name,-40}\t(~g~{vehicle.Model,10}~w~)\t{speed} p: ~o~{passengers} {stop_status} {driver} {hp} {engine_hp} {body_hp}", BASE_X, y, .35f);

                    float distance = vehicle.Position.DistanceTo(playerPed.Position);
                    if (distance < closest_distance)
                    {
                        closest_distance = distance;
                        closest_id = indexer;
                        closest_vehicle = vehicle;
                    }
                }
                if(closest_vehicle != null)
                {
                    string name = Enum.GetName(typeof(VehicleHash), (VehicleHash)closest_vehicle.Model.Hash).ToString();
                    Util.DrawSimpleText($"Closest: ~o~#{closest_id}~w~ {name} (~g~{closest_vehicle.Model}~w~)", BASE_X, BASE_Y - 44, .35f);
                }
                else
                {
                    Util.DrawSimpleText($"Closest: ", BASE_X, BASE_Y - 44, .35f);
                }
                
            }
        }
    }
}
