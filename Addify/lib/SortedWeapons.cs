using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify.statics
{
    class SortedWeapons
    {
        public static readonly WeaponHash[] WEAPONS_MELEE = new WeaponHash[]
        {
            WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Nightstick, WeaponHash.Hammer, WeaponHash.Bat, WeaponHash.Crowbar, WeaponHash.GolfClub, WeaponHash.Bottle, WeaponHash.Dagger, WeaponHash.Hatchet, WeaponHash.KnuckleDuster, WeaponHash.Machete, WeaponHash.Flashlight, WeaponHash.SwitchBlade, WeaponHash.PoolCue, WeaponHash.Wrench, WeaponHash.BattleAxe, WeaponHash.BattleAxe
        };

        public static readonly WeaponHash[] WEAPONS_PISTOL = new WeaponHash[]
        {
            WeaponHash.Pistol, WeaponHash.CombatPistol, WeaponHash.APPistol, WeaponHash.Pistol50, WeaponHash.StunGun, WeaponHash.SNSPistol, WeaponHash.HeavyPistol, WeaponHash.VintagePistol, WeaponHash.FlareGun, WeaponHash.MarksmanPistol, WeaponHash.Revolver, WeaponHash.PistolMk2, WeaponHash.SNSPistolMk2, WeaponHash.RevolverMk2, WeaponHash.DoubleActionRevolver, WeaponHash.UpNAtomizer
        };

        public static readonly WeaponHash[] WEAPONS_MACHINEGUN = new WeaponHash[]
        {
            WeaponHash.MicroSMG, WeaponHash.SMG, WeaponHash.AssaultSMG, WeaponHash.MG, WeaponHash.CombatMG, WeaponHash.Gusenberg, WeaponHash.CombatPDW, WeaponHash.MachinePistol, WeaponHash.MiniSMG, WeaponHash.SMGMk2, WeaponHash.UnholyHellbringer, WeaponHash.CombatMGMk2
        };

        public static readonly WeaponHash[] WEAPONS_ASSAULTRIFLE = new WeaponHash[]
        {
            WeaponHash.AssaultRifle, WeaponHash.CarbineRifle, WeaponHash.AdvancedRifle, WeaponHash.SpecialCarbine, WeaponHash.BullpupRifle, WeaponHash.CompactRifle, WeaponHash.AssaultrifleMk2, WeaponHash.CarbineRifleMk2, WeaponHash.SpecialCarbineMk2, WeaponHash.BullpupRifleMk2
        };
        public static readonly WeaponHash[] WEAPONS_SHOTGUN = new WeaponHash[]
        {
            WeaponHash.SawnOffShotgun, WeaponHash.PumpShotgun, WeaponHash.AssaultShotgun, WeaponHash.BullpupShotgun, WeaponHash.Musket, WeaponHash.HeavyShotgun, WeaponHash.DoubleBarrelShotgun, WeaponHash.SweeperShotgun, WeaponHash.PumpShotgunMk2
        };
        public static readonly WeaponHash[] WEAPONS_SNIPER = new WeaponHash[]
        {
            WeaponHash.SniperRifle, WeaponHash.HeavySniper, WeaponHash.MarksmanPistol, WeaponHash.HeavySniperMk2, WeaponHash.MarksmanRifleMk2
        };
        public static readonly WeaponHash[] WEAPONS_HEAVY = new WeaponHash[]
        {
            WeaponHash.GrenadeLauncher, WeaponHash.RPG, WeaponHash.Minigun, WeaponHash.Firework, WeaponHash.Railgun, WeaponHash.HomingLauncher, WeaponHash.CompactGrenadeLauncher, WeaponHash.Widowmaker
        };
        public static readonly WeaponHash[] WEAPONS_THROWN = new WeaponHash[]
        {
            WeaponHash.Grenade, WeaponHash.StickyBomb, WeaponHash.SmokeGrenade, WeaponHash.Molotov, WeaponHash.Ball, WeaponHash.Flare, WeaponHash.PetrolCan, WeaponHash.ProximityMine, WeaponHash.Snowball, WeaponHash.PipeBomb
        };
        public static readonly WeaponHash[] WEAPONS_ALL = (WeaponHash[])Enum.GetValues(typeof(WeaponHash));


        public static WeaponHash[][] GetWeaponGroupsArray()
        {
            return new WeaponHash[][]
            {
                WEAPONS_MELEE, WEAPONS_PISTOL, WEAPONS_MACHINEGUN, WEAPONS_ASSAULTRIFLE, WEAPONS_SHOTGUN, WEAPONS_SNIPER, WEAPONS_HEAVY, WEAPONS_THROWN
            };
        }
        public static SortedWeaponGroup[] GetWeaponGroups()
        {
            WeaponGroup[] groups = (WeaponGroup[])Enum.GetValues(typeof(WeaponGroup));
            var list = new List<SortedWeaponGroup>();
            for (int i = 0; i < groups.Length; i++)
            {
                var group = GetGroup(groups[i]);
                if(group != null) list.Add(group);
            }
            return list.ToArray();
        }
        public static SortedWeaponGroup GetGroup(WeaponGroup group)
        {
            switch(group)
            {
                case WeaponGroup.Melee:
                    return new SortedWeaponGroup("Melee", WEAPONS_MELEE);
                case WeaponGroup.Pistol:
                    return new SortedWeaponGroup("Pistol", WEAPONS_PISTOL);
                case WeaponGroup.MG:
                    return new SortedWeaponGroup("Machine Gun", WEAPONS_MACHINEGUN);
                case WeaponGroup.AssaultRifle:
                    return new SortedWeaponGroup("Assault Rifle", WEAPONS_ASSAULTRIFLE);
                case WeaponGroup.Shotgun:
                    return new SortedWeaponGroup("Shotgun", WEAPONS_SHOTGUN);
                case WeaponGroup.Sniper:
                    return new SortedWeaponGroup("Sniper", WEAPONS_SNIPER);
                case WeaponGroup.Heavy:
                    return new SortedWeaponGroup("Heavy", WEAPONS_HEAVY);
                case WeaponGroup.Thrown:
                    return new SortedWeaponGroup("Throwable", WEAPONS_THROWN);
                default:
                    return null;
            }
        }
    }
    class SortedWeaponGroup
    {
        private String _name;
        private WeaponHash[] _hashes;

        public SortedWeaponGroup(String name, WeaponHash[] hashes)
        {
            this._name = name;
            this._hashes = hashes;
        }

        public string Name { get => _name; }

        public WeaponHash[] Weapons { get => _hashes; }
    }
}
