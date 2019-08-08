using GTA.Native;
using System.Collections.Generic;

namespace Addify.lib
{
    public class SortedVehicles
    {
        /*public static readonly VehicleHash[] UN_VEHICLES = new VehicleHash[]
        {
            VehicleHash.Adder, VehicleHash.Airbus, VehicleHash.Airtug, VehicleHash.Akuma, VehicleHash.Asea, VehicleHash.Asea2, VehicleHash.Asterope, VehicleHash.Bagger, VehicleHash.BaleTrailer, VehicleHash.Baller, VehicleHash.Baller2, VehicleHash.Banshee, VehicleHash.Barracks, VehicleHash.Bati, VehicleHash.Bati2, VehicleHash.Benson, VehicleHash.BfInjection, VehicleHash.Biff, VehicleHash.Bison, VehicleHash.Bison2, VehicleHash.Bison3, VehicleHash.BJXL, VehicleHash.Blazer, VehicleHash.Blazer2, VehicleHash.Blazer3, VehicleHash.Blimp, VehicleHash.Blista, VehicleHash.BoatTrailer, VehicleHash.BobcatXL, VehicleHash.Boxville, VehicleHash.Boxville2, VehicleHash.Boxville3, VehicleHash.Buccaneer, VehicleHash.Bullet, VehicleHash.Carbonizzare, VehicleHash.Cavalcade, VehicleHash.Cavalcade2, VehicleHash.Cheetah, VehicleHash.Coach, VehicleHash.CogCabrio, VehicleHash.Comet2, VehicleHash.Coquette, VehicleHash.Cruiser, VehicleHash.Crusader, VehicleHash.Cuban800, VehicleHash.Daemon, VehicleHash.Dilettante, VehicleHash.Dilettante2, VehicleHash.DLoader, VehicleHash.DockTrailer, VehicleHash.Docktug, VehicleHash.Dominator, VehicleHash.Double, VehicleHash.Dubsta, VehicleHash.Dubsta2, VehicleHash.Dune, VehicleHash.Dune2, VehicleHash.Elegy2, VehicleHash.Emperor, VehicleHash.Emperor2, VehicleHash.Emperor3, VehicleHash.EntityXF, VehicleHash.Exemplar, VehicleHash.F620, VehicleHash.Faggio2
        };*/
        public static readonly SortedDictionary<string, VehicleHash> HELICOPTERS = new SortedDictionary<string, VehicleHash>()
        {
            {"Annihilator", VehicleHash.Annihilator },
            {"Attack Buzzard", VehicleHash.Buzzard },
            {"Buzzard", VehicleHash.Buzzard2 },
            {"Cargbob", VehicleHash.Cargobob },
            {"Medical Cargobob", VehicleHash.Cargobob2 },
            {"Trevor's Cargobob ", VehicleHash.Cargobob3 },
            {"Cargobob 4", VehicleHash.Cargobob4 },
            {"Frogger", VehicleHash.Frogger },
            {"Frogger FBI",VehicleHash.Frogger2 },
            {"Police Heli",VehicleHash.Polmav }
        };
        public static readonly SortedDictionary<string, VehicleHash> EMERGENCY = new SortedDictionary<string, VehicleHash>
        {
            {"Ambulance", VehicleHash.Ambulance },
            {"Fire Truck", VehicleHash.FireTruck },
            {"FBI Buffalo", VehicleHash.FBI },
            {"FBI Granger", VehicleHash.FBI2 },
            {"Police Stanier", VehicleHash.Police },
            {"Police Buffalo", VehicleHash.Police2 },
            {"Police Interceptor",VehicleHash.Police3 },
            {"Undercover Stanier", VehicleHash.Police4 },
            {"Police Bike", VehicleHash.Policeb },
            {"Snowy Rancher",VehicleHash.PoliceOld1 },
            {"Snowy Esperanto", VehicleHash.PoliceOld2 },
            {"Swat Van",VehicleHash.PoliceT },
            {"Sherrif", VehicleHash.Sheriff },
            {"Sherrif 2", VehicleHash.Sheriff2 }
        };
        public static readonly SortedDictionary<string, VehicleHash> MILITARY = new SortedDictionary<string, VehicleHash>
        {
            {"Tank", VehicleHash.ArmyTanker },
            {"Army Flatbed Trailer", VehicleHash.ArmyTrailer },
            {"Army Flatbed Cutter", VehicleHash.ArmyTrailer2 },
            {"Barracks Covered", VehicleHash.Barracks },
            {"Barracks Semi", VehicleHash.Barracks2 },
            {"Barracks Covered 2", VehicleHash.Barracks3 }
        };
        public static readonly SortedDictionary<string, VehicleHash> CYCLES = new SortedDictionary<string, VehicleHash>
        {
            {"Bmx", VehicleHash.Bmx },
            {"Cruiser",VehicleHash.Cruiser},
            {"Endurex Race Bike",VehicleHash.TriBike2},
            {"Fixter",VehicleHash.Fixter},
            {"Scorcher",VehicleHash.Scorcher},
            {"Tri-Cycles Race Bike",VehicleHash.TriBike3},
            {"Whippet Race Bike",VehicleHash.TriBike}
        };
        public static readonly SortedDictionary<string, VehicleHash> BOATS = new SortedDictionary<string, VehicleHash>()
        {
            {"Dinghy", VehicleHash.Dinghy  },
            {"Dinghy 2",  VehicleHash.Dinghy2 },
            {"Dinghy 3", VehicleHash.Dinghy3 },
            {"Dinghy 4", VehicleHash.Dinghy4 },
            {"Jetmax", VehicleHash.Jetmax },
            {"Marquis", VehicleHash.Marquis },
            {"Police Predator",  VehicleHash.Predator },
            {"Seashark", VehicleHash.Seashark },
            {"Seashark 2",  VehicleHash.Seashark2 },
            {"Squalo", VehicleHash.Squalo },
            {"Submersible", VehicleHash.Submersible },
            {"Suntrap", VehicleHash.Suntrap },
            {"Tropic", VehicleHash.Tropic },
            {"Tropic 2", VehicleHash.Tropic2 },
            {"Seashark Lifeguard", VehicleHash.Seashark3 },
            {"Speeder", VehicleHash.Speeder },
            {"Speeder 2",  VehicleHash.Speeder2 },
            {"Kraken",  VehicleHash.Submersible2 },
            {"Toro", VehicleHash.Toro },
            {"Toro 2",  VehicleHash.Toro2 },
            {"Tug",  VehicleHash.Tug }
        };
        public static readonly SortedDictionary<string, VehicleHash> COMMERICAL = new SortedDictionary<string, VehicleHash>
        {
            {"Benson",VehicleHash.Benson},
            {"Biff",VehicleHash.Biff},
            {"Hauler",VehicleHash.Hauler},
            {"Mule",VehicleHash.Mule},
            {"Packer",VehicleHash.Packer},
            {"Phantom",VehicleHash.Phantom},
            {"Pounder",VehicleHash.Pounder},
            {"Stockade",VehicleHash.Stockade},
            {"Mule (Armored)",VehicleHash.Mule2}
        };
        public static readonly SortedDictionary<string, VehicleHash> COMPACTS = new SortedDictionary<string, VehicleHash>
        {
            {"Blista",VehicleHash.Blista},
            {"Dilettante",VehicleHash.Dilettante},
            {"Issi",VehicleHash.Issi2},
            {"Prairie",VehicleHash.Prairie},
            {"Dilettante (Patrol)",VehicleHash.Dilettante2 },
            {"Panto",VehicleHash.Panto},
            {"Rhapsody",VehicleHash.Rhapsody},
            {"Brioso R/A",VehicleHash.Brioso },
            {"Issi Classic", VehicleHash.Issi3 }
        };
        public static readonly SortedDictionary<string, VehicleHash> COUPES = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> INDUSTRIAL = new SortedDictionary<string, VehicleHash>
        {
            {"Bulldozer", VehicleHash.Bulldozer },
            {"Cutter", VehicleHash.Cutter },
            {"Dump Truck", VehicleHash.Dump }
        };
        public static readonly SortedDictionary<string, VehicleHash> MOTORCYCLES = new SortedDictionary<string, VehicleHash>
        {
            {"Buffalo", VehicleHash.Buffalo },
            {"Buffalo 2", VehicleHash.Buffalo2 },
            {"Buffalo 3", VehicleHash.Buffalo3 }
        };
        public static readonly SortedDictionary<string, VehicleHash> MUSCLE = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> OFFROAD = new SortedDictionary<string, VehicleHash>
        {
            {"Trevor's Truck", VehicleHash.Bodhi2 }
        };
        public static readonly SortedDictionary<string, VehicleHash> PLANES = new SortedDictionary<string, VehicleHash>
        {
            {"Atomic Blimp",VehicleHash.Blimp},
            {"Cuban 800",VehicleHash.Cuban800},
            {"Duster",VehicleHash.Duster},
            {"Jet",VehicleHash.Jet},
            {"Luxor",VehicleHash.Luxor},
            {"Luxor Deluxe",VehicleHash.Luxor2},
            {"Mammatus",VehicleHash.Mammatus},
            {"P-996 Lazer",VehicleHash.Lazer},
            {"Shamal",VehicleHash.Shamal},
            {"Titan",VehicleHash.Titan},
            {"Velum",VehicleHash.Velum},
            {"Cargo Plane",VehicleHash.CargoPlane},
            {"Vestra",VehicleHash.Vestra},
            {"Besra",VehicleHash.Besra},
            {"Miljet",VehicleHash.Miljet},
            {"Dodo",VehicleHash.Dodo},
            {"Xero Blimp",VehicleHash.Blimp2},
            {"Hydra",VehicleHash.Hydra},
            {"Velum (5-Seater)",VehicleHash.Velum2},
            {"Nimbus",VehicleHash.Nimbus}
            //smuggler
            
        };
        public static readonly SortedDictionary<string, VehicleHash> SEDANS = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> SERVICE = new SortedDictionary<string, VehicleHash>
        {
            {"Bus", VehicleHash.Bus },
            {"Prison Bus", VehicleHash.PBus },
            {"Prison Bus", VehicleHash.PBus2 },
            {"Rental", VehicleHash.RentalBus },
            {"Airport Bus", VehicleHash.Airbus },
            {"Caddy",VehicleHash.Caddy },
            {"Caddy 2", VehicleHash.Caddy2 },
            {"Tow Truck", VehicleHash.TowTruck },
            {"Tow Truck 2", VehicleHash.TowTruck2 }
        };
        public static readonly SortedDictionary<string, VehicleHash> SPORTS = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> SPORTS_CLASSIC = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> SUPER = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> SUV = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> UTILITY = new SortedDictionary<string, VehicleHash>
        {

        };
        public static readonly SortedDictionary<string, VehicleHash> VANS = new SortedDictionary<string, VehicleHash>
        {
            {"Burrito Van", VehicleHash.Burrito },
            {"Burrito Van 2", VehicleHash.Burrito2},
            {"Burrito Van 3", VehicleHash.Burrito3 },
            {"Burrito Van 4",  VehicleHash.Burrito4},
            {"Burrito Van 5",  VehicleHash.Burrito5},
            {"Camper Van",  VehicleHash.Camper}
        };
        public static readonly SortedDictionary<string, VehicleHash> IMPORT_EXPORT = new SortedDictionary<string, VehicleHash>
        {
            {"Elegy",VehicleHash.Elegy},
            {"Tempesta",VehicleHash.Tempesta},
            {"Italia GTB",VehicleHash.ItaliGTB},
            {"Italia GTB 2",VehicleHash.ItaliGTB2},
            {"Nero",VehicleHash.Nero},
            {"Nero 2",VehicleHash.Nero2},
            {"Specter",VehicleHash.Specter},
            {"Specter 2",VehicleHash.Specter2},
            {"Diablous 2",VehicleHash.Diablous2},
            {"Blazer",VehicleHash.Blazer5},
            {"Ruiner2??",VehicleHash.Ruiner2},
            {"Dune 4",VehicleHash.Dune4},
            {"Dune 5",VehicleHash.Dune5},
            {"Phantom Wedge",VehicleHash.Phantom2},
            {"Rocket Voltic",VehicleHash.Voltic2},
            {"Penetrator",VehicleHash.Penetrator},
            {"Boxville",VehicleHash.Boxville5},
            {"Wastelander",VehicleHash.Wastelander},
            {"Techanical2",VehicleHash.Technical2},
            {"FCR?",VehicleHash.FCR},
            {"FCR2?",VehicleHash.FCR2},
            {"Comet",VehicleHash.Comet3},
            {"Ruiner??",VehicleHash.Ruiner3}
        };
        public static readonly SortedDictionary<string, VehicleHash> GUNRUNNING = new SortedDictionary<string, VehicleHash>
        {
            {"Hauler Custom",VehicleHash.Hauler2},
            {"Phantom Custom",VehicleHash.Phantom2 },
            {"Mule custom",VehicleHash.Mule3 },
            {"APC Tank", VehicleHash.APC },
            {"Mobile Operations Center (Trailer)",VehicleHash.PropTrailer }
        };
        public static readonly SortedDictionary<string, VehicleHash> AFTER_HOURS = new SortedDictionary<string, VehicleHash>
        {
            {"Pounder Custom",VehicleHash.Pounder2 },
            {"Terrorbyte",VehicleHash.Terrorbyte },
            {"Scarab",VehicleHash.Scarab },
            {"Blimp",VehicleHash.Blimp3},
            {"B-11 Strikeforce",VehicleHash.Strikeforce}

        };
        public static readonly SortedDictionary<string, VehicleHash> ARENA_WARS = new SortedDictionary<string, VehicleHash>
        {
            {"Issi Arena",VehicleHash.Issi4 },
            {"Cerberus",VehicleHash.Cerberus }
        };
        public static readonly SortedDictionary<string, VehicleHash> DOOMSDAY = new SortedDictionary<string, VehicleHash>
        {
            {"Barrage",VehicleHash.Barrage },
            {"Chernobog",VehicleHash.Chernobog},
            {"Khanjari Tank",VehicleHash.Khanjari },
            {"Thruster Jetpack",VehicleHash.Thruster },
            {"Volatol",VehicleHash.Volatol},
            {"Avenger",VehicleHash.Avenger }
        };
        public static readonly SortedDictionary<string, VehicleHash> SMUGGLERS = new SortedDictionary<string, VehicleHash>
        {
            {"Alpha-Z1",VehicleHash.AlphaZ1},
            {"LF-22 Starling",VehicleHash.Starling},
            {"Grotti Visione",VehicleHash.Visione },
            {"Tula",VehicleHash.Tula},
            {"Nagasaki Havok",VehicleHash.Havok},
            {"Nagasaki Ultralight",VehicleHash.Microlight },
            {"V-65 Moltok",VehicleHash.Molotok},
            {"Vapid Retinue",VehicleHash.Retinue },
            {"Rogue",VehicleHash.Rogue },
            {"Rapid GT Classic",VehicleHash.RapidGT3 },
            {"Bombushka",VehicleHash.Bombushka},
            {"Howard NX-25",VehicleHash.Howard},
            {"Mogul",VehicleHash.Mogul},
            {"Pyro",VehicleHash.Pyro},
            {"Seabreeze",VehicleHash.Seabreeze},
            {"Vigilante",VehicleHash.Vigilante },
            {"Nokota",VehicleHash.Nokota},
            {"Coil Cyclone",VehicleHash.Cyclone },
            {"FH-1 Hunter",VehicleHash.Hunter }
        };
        public static readonly SortedDictionary<string, VehicleHash> VINEWOOD = new SortedDictionary<string, VehicleHash>
        {
            {"Annis S80RR",VehicleHash.S80 },
            {"Enus Paragon R",VehicleHash.Paragon },
            {"Enus Paragon R (Armored)",VehicleHash.Paragon2 },
            {"Obey 8F Drafter",VehicleHash.Drafter },
            {"Thruffade Thrax",VehicleHash.Thrax},
            {"Vapid Caracara 4x4", VehicleHash.Caracara2},
            {"Weeny Issi Sport", VehicleHash.Issi7},
            {"Vysser Neo",VehicleHash.Neo},
            {"Bravado Guantlet Classic",VehicleHash.Gauntlet2 },
            {"Zorrusso",VehicleHash.Zorrusso },
            {"Zion 3",VehicleHash.Zion3  },
            {"Rrocket", VehicleHash.RRocket },
            {"Gaunlet 3", VehicleHash.Gauntlet3 },
            {"Gaunlet 4", VehicleHash.Gauntlet4 },
            {"Hellion", VehicleHash.Hellion },
            {"Emerus",VehicleHash.Emerus },
            {"Jugular",VehicleHash.Jugular },
            {"Krieger",VehicleHash.Krieger },
            {"Locust", VehicleHash.Locust},
            {"Nebula", VehicleHash.Nebula},
            {"Novak",VehicleHash.Novak },
            {"Peyote2",VehicleHash.Peyote2 }
        };
    }
}
