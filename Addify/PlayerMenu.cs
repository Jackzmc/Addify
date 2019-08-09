using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Windows.Forms;
using Addify.lib;

namespace Addify {

    class PlayerMenu : MenuItem {
        //static UIMenu pmenu = new UIMenu("Player Settings", "SELECT AN OPTION");


        static UIMenuItem menu_resetWantedLevel = new UIMenuItem("Clear Wanted Level","Removes your current wanted level");
        static UIMenuItem menu_addMoney = new UIMenuItem("Add $1M","Give yourself $1,000,000");

        static UIMenuListItem menu_wanted_list;

        static UIMenuCheckboxItem menu_neverWanted = new UIMenuCheckboxItem("Never Wanted", false, "Have no wanted level");
        static UIMenuCheckboxItem menu_noclip = new UIMenuCheckboxItem("Noclip [F6]", false);
        static UIMenuCheckboxItem menu_toggleGod = new UIMenuCheckboxItem("Godmode", false, "Cannot die");
        static UIMenuCheckboxItem menu_always_wanted = new UIMenuCheckboxItem("Always Wanted", false, "Keep wanted level active");

        static readonly PedHash[] MODEL_HASHES = ((PedHash[])Enum.GetValues(typeof(PedHash))).Cast<PedHash>().OrderBy(x => x.ToString()).ToArray();

        private static Keys key_noclip;

        protected internal PlayerMenu() : base("Player Options") {
            key_noclip = Main.Config.GetValue("Keybinds", "Noclip", Keys.F6);

            List<dynamic> wantedLevels = new List<dynamic>(Enumerable.Range(1, 5).Cast<dynamic>().ToList());
            menu_wanted_list = new UIMenuListItem("Wanted Level", wantedLevels, 0, "Number of stars");
            menu.AddItem(menu_toggleGod);
            menu.AddItem(menu_noclip);
            
            var model_menu = Main.Pool.AddSubMenu(menu, "Change Model >");
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

        #region events

        internal override void onKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == key_noclip)
            {
                menu_noclip.Checked = !menu_noclip.Checked;
                ProcessNoclip();
            }
            if(menu_noclip.Checked)
            {
                Vector3 pos = playerPed.Position;
                Vector3 rot = playerPed.Rotation;
                //rot.X = Cursor.Position.X;
                //rot.Y = Cursor.Position.Y;
                if(e.KeyCode == Keys.W)
                {
                    pos.X += 5;
                    pos.Y += 5;
                }
                else if(e.KeyCode == Keys.S)
                {
                    pos.X -= 5;
                    pos.Y -= 5;
                }
                else if(e.KeyCode == Keys.A)
                {
                    
                }
                else if(e.KeyCode == Keys.D)
                {
                    playerPed.Heading -= 15f;
                }else if(e.KeyCode == Keys.NumPad8)
                {

                }else if(e.KeyCode == Keys.NumPad2)
                {

                }else if(e.KeyCode == Keys.NumPad4)
                {

                }else if(e.KeyCode == Keys.NumPad6)
                {

                }
                playerPed.Rotation = rot;
                playerPed.PositionNoOffset = pos;
            }
        }
        internal override void update() {
            playerPed.IsInvincible = menu_toggleGod.Checked;
            if (menu_noclip.Checked)
            {
                playerPed.Task.ClearAll();

                Vector3 pos = playerPed.Position;
                //Vector3 rot = playerPed.Rotation;
                playerPed.HasCollision = false;
   
                playerPed.PositionNoOffset = pos;
            }
            if (menu_neverWanted.Checked)
            {
                player.WantedLevel = 0;
            }
            else if (menu_always_wanted.Checked)
            {
                player.WantedLevel = menu_wanted_list.Index + 1;
                if (playerPed.IsInVehicle()) playerPed.CurrentVehicle.IsWanted = true;
                player.WantedCenterPosition = Game.Player.Character.Position;
                //GTA.Native.Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, Game.Player, menu_wanted_list.Index);
            }
        }
        internal override void onItemSelect(UIMenu sender, UIMenuItem item, int index) {
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
        internal override void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked) {
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
            }else if(checkbox == menu_noclip)
            {
                ProcessNoclip();
            }
        }
        #endregion events
        #region methods
        private void ProcessNoclip()
        {
            playerPed.HasCollision = !menu_noclip.Checked;
            playerPed.FreezePosition = menu_noclip.Checked;
            if (!menu_noclip.Checked) //set noclip setitngs
            {

            }
        }
        #endregion methods
    }
}
