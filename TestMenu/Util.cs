using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify
{
    class Util
    {
        public static Vector3 GetWaypoint()
        {
            // search for marker blip
            int blipIterator = Function.Call<int>(Hash._GET_BLIP_INFO_ID_ITERATOR);
            for (Blip i = Function.Call<Blip>(Hash.GET_FIRST_BLIP_INFO_ID, blipIterator); Function.Call<int>(Hash.DOES_BLIP_EXIST, i) != 0; i = Function.Call<Blip>(Hash.GET_NEXT_BLIP_INFO_ID, blipIterator))
            {
                if (Function.Call<int>(Hash.GET_BLIP_INFO_ID_TYPE, i) == 4)
                {
                    return Function.Call<Vector3>(Hash.GET_BLIP_INFO_ID_COORD, i);
                }
            }
            return Vector3.Zero;
        }
        public static void Teleport(Vector3 location)
        {
            if (Game.Player.Character.IsInVehicle())
            {
                Game.Player.Character.CurrentVehicle.Position = location;
            }
            else
            {
                Game.Player.Character.Position = location;
            }
        }
        public static void Teleport(float x, float y, float z)
        {
            Teleport(new Vector3(x, y, z));
        }
        public static string GetVehicleName(Vehicle veh)
        {
            return Enum.GetName(typeof(VehicleHash), (VehicleHash)veh.Model.Hash).ToString();
        }
        public static string GetPedName(Ped ped)
        {
            if (ped.Model == null) return null;
            return Enum.GetName(typeof(PedHash), (PedHash)ped.Model.Hash).ToString();
        }
        public static void DrawSimpleText(String text,int x, int y, float scale = .4f, Color? _color = null)
        {
            var pt = new Point(x, y);
            Color color =  (_color.HasValue) ? _color.Value : Color.White;
            var ui = new UIResText(text, pt, scale, color);
            ui.Draw();
        }
    }
}
