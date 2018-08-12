using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using GTA;
using GTA.Native;

namespace TestMenu {
    class WeaponMenu {
        public static UIMenu menu;


        //public static UIMenuListItem list;
        public static List<dynamic> allWeaponsList = new List<dynamic>();
        public static List<dynamic> meleeList = new List<dynamic>() {
            WeaponHash.BattleAxe, WeaponHash.Bat, WeaponHash.Crowbar
        };
        public static List<dynamic> pistolList = new List<dynamic>();

        public static void menuInit(UIMenu menuin) {
            menu = menuin;
            UIMenuListItem allMelee = new UIMenuListItem("Melee", meleeList, 0);
            UIMenuItem giveAll = new UIMenuItem("Give all weapons");
            UIMenuItem removeAll = new UIMenuItem("Remove all weapons");
           
            menu.AddItem(allMelee);
            menu.AddItem(giveAll);
            menu.AddItem(removeAll);

            WeaponHash[] allWeaponHashes = (WeaponHash[])Enum.GetValues(typeof(WeaponHash));
            for(int i=0;i<allWeaponHashes.Length;i++) {
                allWeaponsList.Add(allWeaponHashes[i]);
            }

            menu.OnItemSelect += (sender, item, index) => {
                if (item == allMelee) {
                    int listIndex = allMelee.Index;
                    WeaponHash currentHash = (WeaponHash)meleeList[listIndex];
                    Game.Player.Character.Weapons.Give(currentHash, 9999, true, true);
                }else if(item == giveAll) { 
                    foreach(var weapon in allWeaponsList) {
                        Game.Player.Character.Weapons.Give(weapon, 9999, false, true);
                    }
                    
                } else if(item == removeAll) {
                    Game.Player.Character.Weapons.RemoveAll();
                }
            };
        }

    }
}
