using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify
{
    class WorldMenu_Traffic : MenuItem
    {
        static UIMenuCheckboxItem menu_traffic_light_changer = new UIMenuCheckboxItem("Change Traffic Lights", false);

        static UIMenuListItem menu_traffic_light_opts;

        private readonly UInt32[] TRAFFIC_LIGHTS = new UInt32[]
        {
            0x3E2B73A4, 0x336E5E2A, 0xD8EBA922, 0xD4729F50, 0x272244B2, 0x33986EAE, 0x2323CDC5
        };
        private readonly List<dynamic> TRAFFIC_LIGHT_OPTIONS = new List<dynamic>()
        {
            "Green", "Red", "Yellow"
        };
        public WorldMenu_Traffic(UIMenu menu) : base(menu)
        {
            menu_traffic_light_opts = new UIMenuListItem("Traffic Light Color", TRAFFIC_LIGHT_OPTIONS, 0);
            menu.AddItem(menu_traffic_light_changer);
            menu.AddItem(menu_traffic_light_opts);
        }

        public override void update()
        {
            if (menu_traffic_light_changer.Checked)
            {
                Entity[] objs = World.GetNearbyEntities(playerPed.Position, 150f);
                for (int i = 0; i < objs.Length; i++)
                {
                    Entity ent = objs[i];
                    foreach (var h in TRAFFIC_LIGHTS)
                    {
                        if (h == ent.Model.Hash)
                        {
                            Function.Call(Hash.SET_ENTITY_TRAFFICLIGHT_OVERRIDE, ent, menu_traffic_light_opts.Index);
                            break;
                        }
                    }
                }
                if (menu_traffic_light_opts.Index != 2)
                {
                    Vehicle[] vehs = World.GetNearbyVehicles(playerPed.Position, 100f);
                    for (int i = 0; i < vehs.Length; i++)
                    {
                        Vehicle veh = vehs[i];
                        if (playerPed.IsInVehicle() && playerVehicle == veh) continue;
                        if (menu_traffic_light_opts.Index == 0)
                        {
                            //drivers ignore
                            veh.Driver.DrivingStyle = DrivingStyle.IgnoreLights;
                        }
                        else if (menu_traffic_light_opts.Index == 1)
                        {
                            veh.Driver.DrivingStyle = DrivingStyle.Normal;
                            veh.Driver.Task.ClearAll();
                            veh.Driver.Task.ParkVehicle(veh, veh.Position, veh.Heading);
                        }
                        //veh.Driver.DrivingStyle = DrivingStyle.IgnoreLights;
                    }
                }
            }
        }
    }
}
