using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using GTA;
using GTA.Native;
using GTA.Math;

namespace Addify {

    class PlayerMenu : MenuItem {
        //static UIMenu pmenu = new UIMenu("Player Settings", "SELECT AN OPTION");


        static UIMenuItem menu_resetWantedLevel = new UIMenuItem("Clear Wanted Level","Removes your current wanted level");
        static UIMenuItem menu_addMoney = new UIMenuItem("Add $1M","Give yourself $1,000,000");

        static UIMenuListItem menu_wanted_list;

        static UIMenuCheckboxItem menu_neverWanted = new UIMenuCheckboxItem("Never Wanted", false, "Have no wanted level");
        static UIMenuCheckboxItem menu_toggleGod = new UIMenuCheckboxItem("Godmode", false, "Cannot die");
        static UIMenuCheckboxItem menu_always_wanted = new UIMenuCheckboxItem("Always Wanted", false, "Keep wanted level active");

        static readonly PedHash[] MODEL_HASHES = (PedHash[])Enum.GetValues(typeof(PedHash));

        public PlayerMenu(UIMenu menu) : base(menu) {
            List<dynamic> wantedLevels = new List<dynamic>(Enumerable.Range(1, 5).Cast<dynamic>().ToList());
            menu_wanted_list = new UIMenuListItem("Wanted Level", wantedLevels, 0, "Number of stars");
            menu.AddItem(menu_toggleGod);
            var model_menu = Main.Pool.AddSubMenu(menu, "Change Model >","Change Model");
            foreach(PedHash hash in MODEL_HASHES)
            {
                var name = Enum.GetName(typeof(PedHash),hash).ToString();
                var _item = new UIMenuItem(name);
                _item.Activated += (sender, args) =>
                {
                    var playerModel = new Model(hash);
                    playerModel.Request(500);
                    // Check the model is valid
                    if (playerModel.IsInCdImage && playerModel.IsValid)
                    {
                        // If the model isn't loaded, wait until it is
                        while (!playerModel.IsLoaded) Script.Wait(100);

                        // Set the player's model
                        player.ChangeModel(playerModel);
                    }
                };
                model_menu.AddItem(_item);
            }

            menu.AddItem(menu_neverWanted);
            menu.AddItem(menu_always_wanted);
            menu.AddItem(menu_wanted_list);
            menu.AddItem(menu_resetWantedLevel);

            menu.AddItem(menu_addMoney);
        }
        public override void update() {
            playerPed.IsInvincible = menu_toggleGod.Checked;
            if (menu_neverWanted.Checked)
            {
                player.WantedLevel = 0;
            }
            else if (menu_always_wanted.Checked)
            {
                player.WantedLevel = menu_wanted_list.Index + 1;
                if (playerPed.IsInVehicle()) playerVehicle.IsWanted = true;
                player.WantedCenterPosition = Game.Player.Character.Position;
                //GTA.Native.Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, Game.Player, menu_wanted_list.Index);
            }
        }
        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index) {
            if (item == menu_resetWantedLevel) {
                if (Game.Player.WantedLevel == 0) {
                    UI.ShowSubtitle("You are not wanted");
                } else {
                    player.WantedLevel = 0;
                }
            } else if (item == menu_addMoney) {
                player.Money += 1000000;
            } else if(item == menu_wanted_list)
            {
                player.WantedLevel = menu_wanted_list.Index + 1;
            }
            
        }
        public override void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked) {
            if(checkbox == menu_neverWanted)
            {
                if(menu_neverWanted.Checked)
                {
                    Function.Call(Hash.SET_MAX_WANTED_LEVEL, 0);
                }
                else
                {
                    Function.Call(Hash.SET_MAX_WANTED_LEVEL, 5);
                }
            }
        }
    }
}
