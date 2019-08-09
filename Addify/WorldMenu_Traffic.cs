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
        static UIMenuCheckboxItem menu_traffic_ai_changer = new UIMenuCheckboxItem("Affect AI?", false);

        static UIMenuListItem menu_traffic_light_opts;

        private readonly UInt32[] TRAFFIC_LIGHTS = new UInt32[]
        {
            0x3E2B73A4, 0x336E5E2A, 0xD8EBA922, 0xD4729F50, 0x272244B2, 0x33986EAE, 0x2323CDC5
        };
        private readonly List<dynamic> TRAFFIC_LIGHT_OPTIONS = new List<dynamic>()
        {
            "Green", "Red", "Yellow"
        };
        protected internal WorldMenu_Traffic(UIMenu menu) : base(menu)
        {
            menu_traffic_light_opts = new UIMenuListItem("Traffic Light Color", TRAFFIC_LIGHT_OPTIONS, 0);
            AddMenus(
                menu_traffic_light_changer,
                menu_traffic_light_opts,
                menu_traffic_ai_changer
            );
        }
        int game_tick = 0;
        internal override void update()
        {
            game_tick++;
            if (menu_traffic_light_changer.Checked && game_tick > 16)
            {
                game_tick = 0;
                Entity[] objs = World.GetNearbyEntities(playerPed.Position, 144f);
                for (int i = 0; i < objs.Length; i++)
                {

                    /*if(TRAFFIC_LIGHTS.Any(h => h == objs[i].Model.Hash))
                    {
                        Function.Call(Hash.SET_ENTITY_TRAFFICLIGHT_OVERRIDE, objs[i], menu_traffic_light_opts.Index);
                        break;
                    }*/
                    
                    foreach (var h in TRAFFIC_LIGHTS)
                    {
                        if (h == objs[i].Model.Hash)
                        {
                            Function.Call(Hash.SET_ENTITY_TRAFFICLIGHT_OVERRIDE, objs[i], menu_traffic_light_opts.Index);
                            break;
                        }
                    }
                }
                if (menu_traffic_ai_changer.Checked)
                {
                    if (menu_traffic_light_opts.Index != 2)
                    {
                        Vehicle[] vehs = World.GetNearbyVehicles(playerPed.Position, 100f);
                        for (int i = 0; i < vehs.Length; i++)
                        {
                            if (playerPed.CurrentVehicle == vehs[i]) continue;
                            Vehicle veh = vehs[i];
                            Ped driver = veh.Driver;
                            if (menu_traffic_light_opts.Index == 0) //green light, go
                            {
                                //drivers ignore
                                if (veh.IsStoppedAtTrafficLights || veh.IsStopped)
                                {
                                    driver.Task.ClearAll();
                                    driver.DrivingStyle = DrivingStyle.IgnoreLights;
                                }
                            }
                            else if (menu_traffic_light_opts.Index == 1) //red, stop now
                            {
                                if(!veh.IsStoppedAtTrafficLights || !veh.IsStopped)
                                {
                                    driver.DrivingStyle = DrivingStyle.Normal;
                                    driver.Task.ClearAll();
                                    driver.Task.ParkVehicle(veh, veh.Position, veh.Heading,5,true);
                                }
                                
                            }
                            //veh.Driver.DrivingStyle = DrivingStyle.IgnoreLights;
                        }
                    }
                }
            }
        }
    }
}
