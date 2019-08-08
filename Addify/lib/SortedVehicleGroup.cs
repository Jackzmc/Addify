using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify.lib
{
    public class SortedVehicleGroup
    {
        public string Name { get;  private set; }
        public SortedDictionary<string, VehicleHash> Vehicles { get; private set; }

        public SortedVehicleGroup(String name, SortedDictionary<string,VehicleHash> hashes)
        {
            this.Name = name;
            this.Vehicles = hashes;
        }
        public SortedVehicleGroup(String name, VehicleHash[] hashes)
        {
            this.Name = name;
            for(int i=0;i<hashes.Length;i++)
            {
                var _name = Enum.GetName(typeof(VehicleHash), hashes[i]).ToString();
                Vehicles.Add(_name, hashes[i]);
            }
        }

        #region pub_methods
        public enum VehicleGroup
        {
            Boats,
            Commerical,
            Compacts,
            Coupes,
            Cycles,
            Emergency,
            Helicopters,
            Industrial,
            Military,
            Motorcycles,
            Muscle,
            Offroad,
            Planes,
            Sedans,
            Service,
            Sports,
            SportsClassic,
            Super,
            SUVs,
            Utility,
            Vans,
            DLC_ImportExport,
            DLC_Gunrunning,
            DLC_Smugglers,
            DLC_Doomsday,
            DLC_AfterHours,
            DLC_ArenaWar,
            DLC_Vinewood
            
        }
        public static SortedVehicleGroup[] GetVehicleGroups()
        {
            VehicleGroup[] groups = (VehicleGroup[])Enum.GetValues(typeof(VehicleGroup));
            var list = new List<SortedVehicleGroup>();
            for (int i = 0; i < groups.Length; i++)
            {
                var group = GetGroup(groups[i]);
                if (group != null) list.Add(group);
            }
            return list.ToArray();
        }
        public static SortedVehicleGroup GetGroup(VehicleGroup group)
        {
            switch (group)
            {
                case VehicleGroup.Boats:
                    return new SortedVehicleGroup("Boats", SortedVehicles.BOATS);
                case VehicleGroup.Commerical:
                    return new SortedVehicleGroup("Commerical", SortedVehicles.COMMERICAL);
                case VehicleGroup.Coupes:
                    return new SortedVehicleGroup("Coupes", SortedVehicles.COUPES);
                case VehicleGroup.Cycles:
                    return new SortedVehicleGroup("Cycles", SortedVehicles.CYCLES);
                case VehicleGroup.Emergency:
                    return new SortedVehicleGroup("Emergency", SortedVehicles.EMERGENCY);
                case VehicleGroup.Helicopters:
                    return new SortedVehicleGroup("Helicopters", SortedVehicles.HELICOPTERS);
                case VehicleGroup.Industrial:
                    return new SortedVehicleGroup("Industrial", SortedVehicles.INDUSTRIAL);
                case VehicleGroup.Military:
                    return new SortedVehicleGroup("Military", SortedVehicles.MILITARY);
                case VehicleGroup.Motorcycles:
                    return new SortedVehicleGroup("Motorcycles", SortedVehicles.MOTORCYCLES);
                case VehicleGroup.Offroad:
                    return new SortedVehicleGroup("Offroad", SortedVehicles.OFFROAD);
                case VehicleGroup.Planes:
                    return new SortedVehicleGroup("Planes", SortedVehicles.PLANES);
                case VehicleGroup.Sedans:
                    return new SortedVehicleGroup("Sedans", SortedVehicles.SEDANS);
                case VehicleGroup.Service:
                    return new SortedVehicleGroup("Service", SortedVehicles.SERVICE);
                case VehicleGroup.Sports:
                    return new SortedVehicleGroup("Sports", SortedVehicles.SPORTS);
                case VehicleGroup.SportsClassic:
                    return new SortedVehicleGroup("Sports Classic", SortedVehicles.SPORTS_CLASSIC);
                case VehicleGroup.Super:
                    return new SortedVehicleGroup("Super", SortedVehicles.SUPER);
                case VehicleGroup.SUVs:
                    return new SortedVehicleGroup("SUVs", SortedVehicles.SUV);
                case VehicleGroup.Utility:
                    return new SortedVehicleGroup("Utility", SortedVehicles.UTILITY);
                case VehicleGroup.Vans:
                    return new SortedVehicleGroup("Vans", SortedVehicles.VANS);
                case VehicleGroup.DLC_ImportExport:
                    return new SortedVehicleGroup("Import/Export", SortedVehicles.IMPORT_EXPORT);
                case VehicleGroup.DLC_Gunrunning:
                    return new SortedVehicleGroup("Gunrunning", SortedVehicles.GUNRUNNING);
                case VehicleGroup.DLC_Smugglers:
                    return new SortedVehicleGroup("Smuggler's Run", SortedVehicles.SMUGGLERS);
                case VehicleGroup.DLC_Doomsday:
                    return new SortedVehicleGroup("The Doomsday Heist", SortedVehicles.DOOMSDAY);
                case VehicleGroup.DLC_AfterHours:
                    return new SortedVehicleGroup("After Hours", SortedVehicles.AFTER_HOURS);
                case VehicleGroup.DLC_ArenaWar:
                    return new SortedVehicleGroup("Arena War", SortedVehicles.ARENA_WARS);
                case VehicleGroup.DLC_Vinewood:
                    return new SortedVehicleGroup("Diamond Casino & Resort ", SortedVehicles.VINEWOOD);
                default:
                    return null;
            }
        }
        #endregion
/*
 * case VehicleGroup.Boats:
            return new SortedVehicleGroup("", SortedVehicles.);
            */

}
}
