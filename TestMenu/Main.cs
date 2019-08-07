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

namespace Addify {
    public class Main : Script {
        private static MenuPool menuPool;
        private UIMenu mainMenu;
        public static ScriptSettings config;

        private PlayerMenu playerMenu;
        private VehicleMenu vehicleMenu;
        private WeaponMenu weaponMenu;
        private TeleportMenu teleportMenu;
        private WorldMenu worldMenu;
        private DebugMenu debugMenu;

        private Keys _open_menu_key;
        public static MenuPool getMenuPool()
        {
            return menuPool;
        }
        public Main() {
            config = ScriptSettings.Load("scripts\\addify.ini");
            _open_menu_key = config.GetValue("Keybinds", "openMenu", Keys.Subtract);

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            UI.Notify($"Addify v{version} loaded");
            menuPool = new MenuPool();
            mainMenu = new UIMenu("Addify", $"Version {version} by Jackz") {
                Visible = true
            };

            menuPool.Add(mainMenu);
            playerMenu = new PlayerMenu(menuPool.AddSubMenu(mainMenu, "Player"));
            vehicleMenu = new VehicleMenu(menuPool.AddSubMenu(mainMenu, "Vehicle Options"));
            worldMenu = new WorldMenu(menuPool.AddSubMenu(mainMenu, "World"));
            teleportMenu = new TeleportMenu(menuPool.AddSubMenu(mainMenu, "Teleport"));
            debugMenu = new DebugMenu(menuPool.AddSubMenu(mainMenu, "Debug"));


            menuPool.RefreshIndex();
            Tick += onTick;
            KeyDown += onKeyDown;
            
        }
        void onTick(object sender, EventArgs e) {
            if (menuPool != null)
                menuPool.ProcessMenus();
            playerMenu.update();
            worldMenu.update();
            vehicleMenu.update();
            debugMenu.update();
            if(World.GetWaypointPosition() != null) {
                World.DrawSpotLight(World.GetWaypointPosition(), Game.Player.Character.ForwardVector, Color.Orange, 1, 2, 25, 2, 2);
            }
            
        }
        void onKeyDown(object sender, KeyEventArgs e) {
            try
            {
                vehicleMenu.onKeyDown(sender, e);
                debugMenu.onKeyDown(sender, e);
                teleportMenu.onKeyDown(sender, e);
            }catch(Exception exception)
            {
                UI.Notify("~r~ Exception on keyDown:" + exception.Message);
            }
            Ped playerPed = Game.Player.Character;
            //if (Game.IsPaused) return;
            if (e.KeyCode == _open_menu_key && !menuPool.IsAnyMenuOpen())
            {
                mainMenu.Visible = !mainMenu.Visible; //toggle
            }
        
        }

    }
}
