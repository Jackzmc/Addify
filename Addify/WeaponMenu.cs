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
        UIMenuCheckboxItem menu_unlimited_ammo = new UIMenuCheckboxItem("Unlimited Ammo", false);
        UIMenuCheckboxItem menu_no_reload = new UIMenuCheckboxItem("No Reload", false);
        //todo: add inf. ammo, and no reload

        //Enum.GetValues(typeof(Weather)).Cast<dynamic>().ToList();
        static readonly List<dynamic> allWeaponsList = Enum.GetValues(typeof(WeaponHash)).Cast<dynamic>().ToList();


        protected internal WeaponMenu() : base("Weapons") {
            var weaponGroups = SortedWeapons.GetWeaponGroups();
            
            menu.AddItem(giveAll);
            menu.AddItem(removeAll);
            var group_menu = Main.Pool.AddSubMenu(menu, "Groups >");

            foreach (SortedWeaponGroup group in weaponGroups)
            {
                var submenu = Main.Pool.AddSubMenu(group_menu, group.Name);

                var _sub_get_all = new UIMenuItem("[Get All]");
                var _sub_rem_all = new UIMenuItem("[Remove All]");
                submenu.AddItem(_sub_get_all);
                submenu.AddItem(_sub_rem_all);
                foreach (WeaponHash wpn in group.Weapons)
                {
                    var name = Enum.GetName(typeof(WeaponHash), (WeaponHash)wpn).ToString();
                    var wpn_menu = new UIMenuItem(name,$"Spawn a {name}");
                    wpn_menu.Activated += (sender, args) =>
                    {
                        playerPed.Weapons.Give(wpn, 999, true, true);

                    };
                    
                    submenu.AddItem(wpn_menu);
                }
                
                submenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == _sub_get_all)
                    {
                        foreach (var wpn in group.Weapons)
                        {
                            playerPed.Weapons.Give(wpn, 999, true, false);
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

        internal override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
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
            }else if(item == menu_unlimited_ammo)
            {
                playerPed.Weapons.Current.InfiniteAmmo = menu_unlimited_ammo.Checked;
            }else if(item == menu_no_reload)
            {
                playerPed.Weapons.Current.InfiniteAmmoClip = menu_no_reload.Checked;
            }
        }

    }
}
