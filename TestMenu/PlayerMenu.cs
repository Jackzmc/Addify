using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using GTA;

namespace TestMenu {

    public class PlayerMenu : Script {
        public static UIMenu pmenu = new UIMenu("Player Settings", "SELECT AN OPTION");

        static bool player_god = false;
        static bool player_neverwanted = false;

        static UIMenuItem resetWantedLevel = new UIMenuItem("Reset Wanted Level");
        static UIMenuItem addMoney = new UIMenuItem("Add Money");
        static UIMenuItem addWanted = new UIMenuItem("Increase wanted level");
        static UIMenuItem delWanted = new UIMenuItem("Decrease wanted level");

        
        static UIMenuCheckboxItem neverWanted = new UIMenuCheckboxItem("Never Wanted", false);
        static UIMenuCheckboxItem toggleGod = new UIMenuCheckboxItem("Godmode", false);

        public static void menuInit(UIMenu menu) {
            pmenu = menu;
            pmenu.AddItem(resetWantedLevel);
            pmenu.AddItem(addWanted);
            pmenu.AddItem(delWanted);
            pmenu.AddItem(neverWanted);
            pmenu.AddItem(toggleGod);
            pmenu.AddItem(addMoney);

            pmenu.OnItemSelect += onItemSelect;
            pmenu.OnCheckboxChange += onCheckboxChange;
        }
        public static void update() {
            Game.Player.Character.IsInvincible = player_god;
            if (player_neverwanted) Game.Player.WantedLevel = 0;
        }
        public static void onItemSelect(UIMenu sender, UIMenuItem item, int index) {
            if (item == resetWantedLevel) {
                if (Game.Player.WantedLevel == 0) {
                    UI.ShowSubtitle("You are not wanted");
                } else {
                    Game.Player.WantedLevel = 0;
                }
            } else if (item == addMoney) {
                Game.Player.Money += 100000;
            } else if (item == addWanted) {
                if (Game.Player.WantedLevel < 5) Game.Player.WantedLevel++;
            } else if (item == delWanted) {
                if (Game.Player.WantedLevel > 0) Game.Player.WantedLevel--;
            }
        }
        public static void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked) {
            if (checkbox == toggleGod) {
                player_god = toggleGod.Checked;
            } else if (checkbox == neverWanted) {

                player_neverwanted = neverWanted.Checked;
            }
        }
    }
}
