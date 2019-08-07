using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using GTA;
using GTA.Native;
using Addify.statics;

namespace Addify {
    class WeaponMenu : MenuItem {

        //public static UIMenuListItem list;

        UIMenuItem giveAll = new UIMenuItem("Give all weapons");
        UIMenuItem removeAll = new UIMenuItem("Remove all weapons");
        //todo: add inf. ammo, and no reload

        //Enum.GetValues(typeof(Weather)).Cast<dynamic>().ToList();
        static readonly List<dynamic> allWeaponsList = Enum.GetValues(typeof(WeaponHash)).Cast<dynamic>().ToList();


        public WeaponMenu(UIMenu menu) : base(menu) {
            var pool = Main.getMenuPool();
            var weaponGroups = SortedWeapons.GetWeaponGroups();
            
            menu.AddItem(giveAll);
            menu.AddItem(removeAll);

            foreach (SortedWeaponGroup group in weaponGroups)
            {
                UI.Notify($"N: {group.Name}");
                var submenu = pool.AddSubMenu(menu, group.Name);
                List<dynamic> groups = group.Weapons.Cast<dynamic>().ToList();

                var _sub_get_all = new UIMenuItem("Get All");
                var _sub_rem_all = new UIMenuItem("Remove All");
                var _sub_get_list = new UIMenuListItem("Get Weapon", groups, 0);
                submenu.AddItem(_sub_get_all);
                submenu.AddItem(_sub_rem_all);
                submenu.AddItem(_sub_get_list);
                submenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == _sub_get_list)
                    {
                        playerPed.Weapons.Give(group.Weapons[_sub_get_list.Index], 999, true, true);
                    }
                    else if (item == _sub_get_all)
                    {
                        foreach (var wpn in group.Weapons)
                        {
                            playerPed.Weapons.Give(wpn, 999, true, true);
                        }
                    }else if(item == _sub_rem_all)
                    {
                        foreach(var wpn in group.Weapons)
                        {
                            playerPed.Weapons.Remove(wpn);
                        }
                    }
                };
            }

        }

        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
 
            if (item == giveAll)
            {
                foreach (var weapon in allWeaponsList)
                {
                    Game.Player.Character.Weapons.Give(weapon, 9999, false, true);
                }

            }
            else if (item == removeAll)
            {
                Game.Player.Character.Weapons.RemoveAll();
            }
        }

    }
}
