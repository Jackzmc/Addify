using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NativeUI;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Windows.Forms;

namespace Addify {
    class TeleportMenu : MenuItem {
        //public static UIMenu menu;

        static UIMenuItem waypoint;
        static List<UIMenuItem> waypointItems = new List<UIMenuItem>();
        static Dictionary<String, Vector3> locations = new Dictionary<String, Vector3>() {
            { "Maze Bank", new Vector3(-75, -819, 326 ) },
            {"Strip Club DJ Booth", new Vector3(126.135f,-1278.583f,29.270f)},
            {"Blaine County Savings Bank", new Vector3(-109.299f,6464.035f,31.627f)},
            {"Police Station", new Vector3(436.491f, -982.172f,30.699f)},
            {"Humane Labs Entrance", new Vector3(3619.749f,2742.740f,28.690f)},
            {"Burnt FIB Building", new Vector3(160.868f,-745.831f,250.063f)},
            {"10 Car Garage Back Room", new Vector3(223.193f,-967.322f,99.000f)},
            {"Humane Labs Tunnel", new Vector3(3525.495f,3705.301f,20.992f)},
            {"Ammunation Office", new Vector3(12.494f,-1110.130f,29.797f)},
            {"Ammunation Gun Range", new Vector3( 22.153f,-1072.854f,29.797f)},
            {"Trevor's Meth Lab", new Vector3(1391.773f,3608.716f,38.942f)},
            {"Pacific Standard Bank Vault", new Vector3(255.851f, 217.030f,101.683f)},
            {"Lester's House", new Vector3(1273.898f,-1719.304f,54.771f)},
            {"Floyd's Apartment", new Vector3(-1150.703f,-1520.713f,10.633f)},
            {"FIB Top Floor", new Vector3(135.733f,-749.216f,258.152f)},
            {"IAA Office", new Vector3(117.220f,-620.938f,206.047f)},
            {"Pacific Standard Bank", new Vector3(235.046f,216.434f,106.287f)},
            {"Fort Zancudo ATC entrance", new Vector3(-2344.373f,3267.498f,32.811f)},
            {"Fort Zancudo ATC top floor", new Vector3(-2358.132f,3249.754f,101.451f)},
            {"Torture Room", new Vector3( 147.170f,-2201.804f,4.688f)},
            {"Main LS Customs", new Vector3(-365.425f,-131.809f,37.873f)},
            {"Very High Up", new Vector3(-129.964f,8130.873f,6705.307f)},
            {"IAA Roof", new Vector3(134.085f,-637.859f,262.851f)},
            {"FIB Roof", new Vector3(150.126f,-754.591f,262.865f)},
            {"Maze Bank Roof", new Vector3(-75.015f,-818.215f,326.176f)},
            {"Top of the Mt Chilad", new Vector3(450.718f,5566.614f,806.183f)},
            {"Most Northerly Point", new Vector3(24.775f,7644.102f,19.055f)},
            {"Vinewood Bowl Stage", new Vector3(686.245f,577.950f,130.461f)},
            {"Sisyphus Theater Stage", new Vector3(205.316f,1167.378f,227.005f)},
            {"Director Mod Trailer", new Vector3(-20.004f,-10.889f,500.602f)},
            {"Galileo Observatory Roof", new Vector3(-438.804f,1076.097f,352.411f)},
            {"Kortz Center", new Vector3(-2243.810f,264.048f,174.615f)},
            {"Chumash Historic Family Pier", new Vector3(-3426.683f,967.738f,8.347f)},
            {"Paleto Bay Pier", new Vector3(-275.522f,6635.835f,7.425f)},
            {"God's thumb", new Vector3(-1006.402f,6272.383f,1.503f)},
            {"Calafia Train Bridge", new Vector3(-517.869f,4425.284f,89.795f)},
            {"Altruist Cult Camp", new Vector3(-1170.841f,4926.646f,224.295f)},
            {"Maze Bank Arena Roof", new Vector3(-324.300f,-1968.545f,67.002f)},
            {"Marlowe Vineyards", new Vector3(-1868.971f,2095.674f,139.115f)},
            {"Hippy Camp", new Vector3(2476.712f,3789.645f,41.226f)},
            {"Devin Weston's House", new Vector3(-2639.872f,1866.812f,160.135f)},
            {"Abandon Mine", new Vector3(-595.342f, 2086.008f,131.412f)},
            {"Weed Farm", new Vector3(2208.777f,5578.235f,53.735f)},
            {"Stab City", new Vector3( 126.975f,3714.419f,46.827f)},
            {"Airplane Graveyard Airplane Tail", new Vector3(2395.096f,3049.616f,60.053f)},
            {"Satellite Dish Antenna", new Vector3(2034.988f,2953.105f,74.602f)},
            {"Satellite Dishes", new Vector3( 2062.123f,2942.055f,47.431f)},
            {"Windmill Top", new Vector3(2026.677f,1842.684f,133.313f)},
            {"Sandy Shores Building Site Crane", new Vector3(1051.209f,2280.452f,89.727f)},
            {"Rebel Radio", new Vector3(736.153f,2583.143f,79.634f)},
            {"Quarry", new Vector3(2954.196f,2783.410f,41.004f)},
            {"Palmer-Taylor Power Station Chimney", new Vector3( 2732.931f, 1577.540f,83.671f)},
            {"Merryweather Dock", new Vector3( 486.417f,-3339.692f,6.070f)},
            {"Cargo Ship", new Vector3(899.678f,-2882.191f,19.013f)},
            {"Del Perro Pier", new Vector3(-1850.127f,-1231.751f,13.017f)},
            {"Play Boy Mansion", new Vector3(-1475.234f,167.088f,55.841f)},
            {"Jolene Cranley-Evans Ghost", new Vector3(3059.620f,5564.246f,197.091f)},
            {"NOOSE Headquarters", new Vector3(2535.243f,-383.799f,92.993f)},
            {"Snowman", new Vector3( 971.245f,-1620.993f,30.111f)},
            {"Oriental Theater", new Vector3(293.089f,180.466f,104.301f)},
            {"Beach Skatepark", new Vector3(-1374.881f,-1398.835f,6.141f)},
            {"Underpass Skatepark", new Vector3(718.341f,-1218.714f,26.014f)},
            {"Casino", new Vector3(925.329f,46.152f,80.908f)},
            {"University of San Andreas", new Vector3(-1696.866f,142.747f,64.372f)},
            {"La Puerta Freeway Bridge", new Vector3( -543.932f,-2225.543f,122.366f)},
            {"Land Act Dam", new Vector3( 1660.369f,-12.013f,170.020f)},
            {"Mount Gordo", new Vector3( 2877.633f,5911.078f,369.624f)},
            {"Little Seoul", new Vector3(-889.655f,-853.499f,20.566f)},
            {"Epsilon Building", new Vector3(-695.025f,82.955f,55.855f)},
            {"The Richman Hotel", new Vector3(-1330.911f,340.871f,64.078f)},
            {"Vinewood sign", new Vector3(711.362f,1198.134f,348.526f)},
            {"Los Santos Golf Club", new Vector3(-1336.715f,59.051f,55.246f)},
            {"Chicken", new Vector3(-31.010f,6316.830f,40.083f)},
            {"Little Portola", new Vector3(-635.463f,-242.402f,38.175f)},
            {"Pacific Bluffs Country Club", new Vector3(-3022.222f,39.968f,13.611f)},
            {"Vinewood Cemetery", new Vector3(-1659993f,-128.399f,59.954f)},
            {"Paleto Forest Sawmill Chimney", new Vector3(-549.467f,5308.221f,114.146f)},
            {"Mirror Park", new Vector3(1070.206f,-711.958f,58.483f)},
            {"Rocket", new Vector3(1608.698f,6438.096f,37.637f)},
            {"El Gordo Lighthouse", new Vector3(3430.155f,5174.196f,41.280f)},
            {"Mile High Club", new Vector3(-144.274f,-946.813f,269.135f)}
        };

        private static Keys WaypointTeleport;
        public TeleportMenu() : base("Teleport") {
            WaypointTeleport = Main.Config.GetValue("Keybinds", "TPWaypoint", Keys.F5);
            waypoint = new UIMenuItem($"Teleport to waypoint [{WaypointTeleport}]");
            Ped playerPed = Game.Player.Character;

            menu.AddItem(waypoint);
            foreach (KeyValuePair<string, Vector3> entry in locations)
            {
                var item = new UIMenuItem(entry.Key, $"Teleport to {entry.Key}");
                //waypointItems.Add(item);
                item.Activated += (_menu, sender) =>
                {
                    if(playerPed.IsInVehicle())
                    {
                        playerPed.CurrentVehicle.Position = entry.Value;
                    }
                    else
                    {
                        playerPed.Position = entry.Value;
                    }
                };
                menu.AddItem(item);
            }
            //menu.MenuItems.AddRange(waypointItems);
        }
        public override void onKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == WaypointTeleport)
            {
                TeleportPlayerToWaypoint();
            }
        }
        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if (item == waypoint)
            {
                TeleportPlayerToWaypoint();
            }
            /*else
            {
                foreach (UIMenuItem waypoint_entry in waypointItems)
                {
                    if (item == waypoint_entry)
                    {
                        waypoint_entry.
                        //Game.Player.Character.Position = waypoint_entry.
                    }
                }
            }*/
        }
        private void TeleportPlayerToWaypoint()
        {
            if (!Game.IsWaypointActive)
            {
                UI.ShowSubtitle("You don't have a waypoint selected");
            }
            else
            {
                //int Handle = Game.Player.Character.Handle;
                Vector3 waypointBlipLoc = Util.GetWaypoint();
                Function.Call(Hash.LOAD_SCENE, waypointBlipLoc.X, waypointBlipLoc.Y, 150f);
                Util.Teleport(waypointBlipLoc);
                OutputArgument groundZPoint = new OutputArgument();
                //GTA.Native.Pointer groundZPointer = new GTA.Native.Pointer(typeof(float))
                float groundZ = 0.0f;
                int counter = 0;
                while (groundZ == 0.0f)
                {
                    if(counter > 20)
                    {
                        UI.Notify("HIT LIMIT");
                        groundZ = 25;
                        break;
                    }
                    Function.Call(Hash.GET_GROUND_Z_FOR_3D_COORD, waypointBlipLoc.X, waypointBlipLoc.Y, 1000f, groundZPoint, false);
                    groundZ = groundZPoint.GetResult<float>();
                    counter++;
                    Thread.Sleep(5);
                }
                //float groundZ = GTA.World.GetGroundHeight(waypointBlipLoc);
                UI.Notify($"GROUND HEIGHT: {groundZ}");
                UI.Notify("Waypoint Height: " + waypointBlipLoc.Z);
                Vector3 dest = new Vector3(waypointBlipLoc.X, waypointBlipLoc.Y, groundZ+=5f);
                Util.Teleport(dest);
                //groundZ += 15.0f;
                
            }
        }
    }
    class Waypoint {
        public String Name;
        public Vector3 Location;
        Waypoint(String name,Vector3 vector)
        {
            this.Name = name;
            this.Location = vector;
        }
    }
}
