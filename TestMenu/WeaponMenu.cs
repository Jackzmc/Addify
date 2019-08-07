using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using GTA;
using GTA.Native;

namespace Addify {
    class WeaponMenu : MenuItem {

        //public static UIMenuListItem list;

        UIMenuItem giveAll = new UIMenuItem("Give all weapons");
        UIMenuItem removeAll = new UIMenuItem("Remove all weapons");

        UIMenuListItem allMelee;
        //Enum.GetValues(typeof(Weather)).Cast<dynamic>().ToList();
        static readonly List<dynamic> allWeaponsList = Enum.GetValues(typeof(WeaponHash)).Cast<dynamic>().ToList();
        static readonly List<dynamic> meleeList = new List<dynamic>() {
            WeaponHash.BattleAxe, WeaponHash.Bat, WeaponHash.Crowbar
        };
        static List<dynamic> pistolList = new List<dynamic>();

        public WeaponMenu(UIMenu menu) : base(menu) {
            allMelee = new UIMenuListItem("Melee", meleeList, 0);

            menu.AddItem(allMelee);
            menu.AddItem(giveAll);
            menu.AddItem(removeAll);

            

        }
        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if (item == allMelee)
            {
                int listIndex = allMelee.Index;
                WeaponHash currentHash = (WeaponHash)meleeList[listIndex];
                Game.Player.Character.Weapons.Give(currentHash, 9999, true, true);
            }
            else if (item == giveAll)
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
