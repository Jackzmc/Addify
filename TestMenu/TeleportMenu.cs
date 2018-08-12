using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NativeUI;
using GTA;
using GTA.Native;
using GTA.Math;


namespace TestMenu {
    class TeleportMenu {
        public static UIMenu menu;

        public static UIMenuItem waypoint;

        public static void menuInit(UIMenu menuin) {
            menu = menuin;

            waypoint = new UIMenuItem("Teleport to waypoint");
            menu.AddItem(waypoint);

            menu.OnItemSelect += (sender, item, index) => {
                if(item == waypoint) {
                    if (!Game.IsWaypointActive) {
                        UI.ShowSubtitle("You don't have a waypoint selected");
                    } else {
                        //int Handle = Game.Player.Character.Handle;
                        Vector3 waypointBlipLoc = get_blip_marker();
                        waypointBlipLoc.Z += 5;

                        if (Game.Player.Character.IsInVehicle()) {
                            Game.Player.Character.CurrentVehicle.Position = waypointBlipLoc;
                        } else {
                            Game.Player.Character.Position = waypointBlipLoc;
                        }
                        Thread.Sleep(200);
                        float groundZ = GTA.World.GetGroundHeight(waypointBlipLoc);
                        groundZ += 5.0f;
                        if (Game.Player.Character.IsInVehicle()) {
                            Game.Player.Character.CurrentVehicle.Position = new Vector3(waypointBlipLoc.X, waypointBlipLoc.Y, groundZ);
                        } else {
                            Game.Player.Character.Position = new Vector3(waypointBlipLoc.X, waypointBlipLoc.Y, groundZ);
                        }



                        //Game.Player.Character.Position = Game.Player.way
                        //Game.Player.Character

                    }
                }
            };
        }
        public static Vector3 get_blip_marker() {
            Vector3 zero = new Vector3(0, 0, 0);
            Vector3 coords = new Vector3(0, 0, 0);

            bool blipFound = false;
            // search for marker blip
            int blipIterator = Function.Call<int>(Hash._GET_BLIP_INFO_ID_ITERATOR);
            for (Blip i = Function.Call<Blip>(Hash.GET_FIRST_BLIP_INFO_ID,blipIterator); Function.Call<int>(Hash.DOES_BLIP_EXIST,i) != 0; i = Function.Call<Blip>(Hash.GET_NEXT_BLIP_INFO_ID,blipIterator)) {
                if (Function.Call<int>(Hash.GET_BLIP_INFO_ID_TYPE,i) == 4) {
                    coords = Function.Call<Vector3>(Hash.GET_BLIP_INFO_ID_COORD, i);
                    blipFound = true;
                    break;
                }
            }
            if (blipFound) {
                return coords;
            }

            UI.ShowSubtitle("Map marker isn't set");
            return zero;
        }
    }

}
