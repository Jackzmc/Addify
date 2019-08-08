using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify.lib
{
    class SortedVehicles
    {
        public static readonly VehicleHash[] UNSORTED_VEHICLES = new VehicleHash[]
        {
            VehicleHash.Adder, VehicleHash.Airbus, VehicleHash.Airtug, VehicleHash.Akuma, VehicleHash.Asea, VehicleHash.Asea2, VehicleHash.Asterope, VehicleHash.Bagger, VehicleHash.BaleTrailer, VehicleHash.Baller, VehicleHash.Baller2, VehicleHash.Banshee, VehicleHash.Barracks, VehicleHash.Bati, VehicleHash.Bati2, VehicleHash.Benson, VehicleHash.BfInjection, VehicleHash.Biff, VehicleHash.Bison, VehicleHash.Bison2, VehicleHash.Bison3, VehicleHash.BJXL, VehicleHash.Blazer, VehicleHash.Blazer2, VehicleHash.Blazer3, VehicleHash.Blimp, VehicleHash.Blista, VehicleHash.BoatTrailer, VehicleHash.BobcatXL, VehicleHash.Boxville, VehicleHash.Boxville2, VehicleHash.Boxville3, VehicleHash.Buccaneer, VehicleHash.Bullet, VehicleHash.Carbonizzare, VehicleHash.Cavalcade, VehicleHash.Cavalcade2, VehicleHash.Cheetah, VehicleHash.Coach, VehicleHash.CogCabrio, VehicleHash.Comet2, VehicleHash.Coquette, VehicleHash.Cruiser, VehicleHash.Crusader, VehicleHash.Cuban800, VehicleHash.Daemon, VehicleHash.Dilettante, VehicleHash.Dilettante2, VehicleHash.DLoader, VehicleHash.DockTrailer, VehicleHash.Docktug, VehicleHash.Dominator, VehicleHash.Double, VehicleHash.Dubsta, VehicleHash.Dubsta2, VehicleHash.Dune, VehicleHash.Dune2, VehicleHash.Elegy2, VehicleHash.Emperor, VehicleHash.Emperor2, VehicleHash.Emperor3, VehicleHash.EntityXF, VehicleHash.Exemplar, VehicleHash.F620, VehicleHash.Faggio2,
        };
        public static readonly SortedDictionary<string, VehicleHash> HELICOPTERS = new SortedDictionary<string, VehicleHash>()
        {
            {"Annihilator", VehicleHash.Annihilator },
            {"Buzzard", VehicleHash.Buzzard },
            {"Buzzard 2", VehicleHash.Buzzard2 },
            {"Cargbob", VehicleHash.Cargobob },
            {"Cargobob 2", VehicleHash.Cargobob2 },
            {"Cargobob3 ", VehicleHash.Cargobob3 },
            {"Cargobob4", VehicleHash.Cargobob4 },
            {"Frogger", VehicleHash.Frogger },
            {"Frogger FBI",VehicleHash.Frogger2 },
            {"Police Heli",VehicleHash.Polmav }
        };
        /*public readonly VehicleHash[] HELICOPTERS = new VehicleHash[]
        {
            VehicleHash.Annihilator, VehicleHash.Buzzard, VehicleHash.Buzzard2, VehicleHash.Cargobob, VehicleHash.Cargobob2, VehicleHash.Cargobob3, VehicleHash.Frogger, VehicleHash.Frogger2, VehicleHash.Polmav
        };*/
        public static readonly VehicleHash[] EMERGENCY = new VehicleHash[]
        {
            VehicleHash.Ambulance, VehicleHash.FireTruck, VehicleHash.FBI, VehicleHash.FBI2, VehicleHash.Police, VehicleHash.Police2, VehicleHash.Police3, VehicleHash.Police4, VehicleHash.Policeb, VehicleHash.PoliceOld1, VehicleHash.PoliceOld2, VehicleHash.PoliceT
        };
        public static readonly VehicleHash[] MILITARY = new VehicleHash[]
        {
            VehicleHash.ArmyTanker, VehicleHash.ArmyTrailer, VehicleHash.ArmyTrailer2, VehicleHash.Barracks, VehicleHash.Barracks2, VehicleHash.Barracks3
        };
        public static readonly VehicleHash[] CYCLES = new VehicleHash[]
        {
            VehicleHash.Bmx
        };
        public static readonly VehicleHash[] BOATS = new VehicleHash[]
        {
            VehicleHash.Dinghy, VehicleHash.Dinghy2, VehicleHash.Dinghy3, VehicleHash.Dinghy4
        };
        public static readonly VehicleHash[] COMMERICAL = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] COMPACTS = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] COUPES = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] INDUSTRIAL = new VehicleHash[]
        {
            VehicleHash.Bulldozer, VehicleHash.Cutter, VehicleHash.Dump
        };
        public static readonly VehicleHash[] MOTORCYCLES = new VehicleHash[]
        {
            VehicleHash.Buffalo, VehicleHash.Buffalo2, VehicleHash.Buffalo3
        };
        public static readonly VehicleHash[] MUSCLE = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] OFFROAD = new VehicleHash[]
        {
            VehicleHash.Bodhi2
        };
        public static readonly VehicleHash[] PLANES = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] SEDANS = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] SERVICE = new VehicleHash[]
        {
            VehicleHash.Bus, VehicleHash.Caddy, VehicleHash.Caddy2, VehicleHash.TowTruck, VehicleHash.TowTruck2
        };
        public static readonly VehicleHash[] SPORTS = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] SPORTS_CLASSIC = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] SUPER = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] SUV = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] UTILITY = new VehicleHash[]
        {

        };
        public static readonly VehicleHash[] VANS = new VehicleHash[]
        {
            VehicleHash.Burrito, VehicleHash.Burrito2, VehicleHash.Burrito3, VehicleHash.Burrito4, VehicleHash.Burrito5, VehicleHash.Camper
        };
        public static readonly VehicleHash[] IMPORT_EXPORT = new VehicleHash[]
        {
            VehicleHash.Elegy,  VehicleHash.Tempesta,  VehicleHash.ItaliGTB,  VehicleHash.ItaliGTB2,  VehicleHash.Nero,  VehicleHash.Nero2,  VehicleHash.Specter,  VehicleHash.Specter2,  VehicleHash.Diablous2,  VehicleHash.Blazer5,  VehicleHash.Ruiner2,  VehicleHash.Dune4,  VehicleHash.Dune5,  VehicleHash.Phantom2,  VehicleHash.Voltic2,  VehicleHash.Penetrator,  VehicleHash.Boxville5,  VehicleHash.Wastelander,  VehicleHash.Technical2,  VehicleHash.FCR,  VehicleHash.FCR2,  VehicleHash.Comet3,  VehicleHash.ruiner3
        };
        public static readonly VehicleHash[] GUNRUNNING = new VehicleHash[]
        {

        };
        
    }
    
}
