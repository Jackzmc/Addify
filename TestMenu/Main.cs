using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeUI;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Drawing;
using System.Windows.Forms;

namespace TestMenu {
    public class Main : Script {
        public MenuPool menuPool;
        public UIMenu mainMenu;

        public Main() {
            UI.Notify("testmenu has loaded");
            menuPool = new MenuPool();
            mainMenu = new UIMenu("jMenu", "SELECT AN OPTION") {
                Visible = true
            };

            menuPool.Add(mainMenu);
            PlayerMenu.menuInit(menuPool.AddSubMenu(mainMenu, "Player"));
            WeaponMenu.menuInit(menuPool.AddSubMenu(mainMenu, "Weapons"));
            WorldMenu.menuInit(menuPool.AddSubMenu(mainMenu, "World"));
            TeleportMenu.menuInit(menuPool.AddSubMenu(mainMenu, "Teleport"));
            Tick += onTick;
            KeyDown += onKeyDown;
            
        }
        void onTick(object sender, EventArgs e) {
            if (menuPool != null)
                menuPool.ProcessMenus();
            PlayerMenu.update();
            WorldMenu.update();
            
        }
        void onKeyDown(object sender, KeyEventArgs e) {
            //if (Game.IsPaused) return;

            if (e.KeyCode == Keys.F9 && !menuPool.IsAnyMenuOpen()) {
                mainMenu.Visible = !mainMenu.Visible; //toggle
            }else if(e.KeyCode == Keys.NumPad9 && Game.Player.Character.IsInVehicle()) {
                Game.Player.Character.CurrentVehicle.ApplyForce(Game.Player.Character.ForwardVector*3);
   
            }
        }

    }
}
