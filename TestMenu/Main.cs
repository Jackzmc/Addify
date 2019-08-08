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
using Addify.lib;

namespace Addify {
    public class Main : Script {
        private UIMenu mainMenu;
        public static ScriptSettings config;

        private PlayerMenu playerMenu;
        private VehicleMenu vehicleMenu;
        private WeaponMenu weaponMenu;
        private TeleportMenu teleportMenu;
        private WorldMenu worldMenu;
        private Logger logger = new Logger();
        #if DEBUG
        private DebugMenu debugMenu;
        #endif

        private Keys _open_menu_key;
        public static MenuPool Pool { get; private set; }
        public Main() {
            config = ScriptSettings.Load("scripts\\addify.ini");
            _open_menu_key = config.GetValue("Keybinds", "openMenu", Keys.Subtract);

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            UI.Notify($"Addify v{version} loaded");
            Pool = new MenuPool();
            mainMenu = new UIMenu("Addify", $"Version {version} by Jackz") {
                Visible = true
            };

            Pool.Add(mainMenu);
            playerMenu = new PlayerMenu(Pool.AddSubMenu(mainMenu, "Player"));
            vehicleMenu = new VehicleMenu(Pool.AddSubMenu(mainMenu, "Vehicle Options"));
            worldMenu = new WorldMenu(Pool.AddSubMenu(mainMenu, "World"));
            weaponMenu = new WeaponMenu(Pool.AddSubMenu(mainMenu, "Weapons"));
            teleportMenu = new TeleportMenu(Pool.AddSubMenu(mainMenu, "Teleport"));
            #if DEBUG
            debugMenu = new DebugMenu(Pool.AddSubMenu(mainMenu, "Debug"));
            #endif
            setupKeys();

            logger.Info($"Addify V{version} loaded");

            Pool.RefreshIndex();
            Tick += onTick;
            KeyDown += onKeyDown;
        }
        void setupKeys()
        {
            Pool.SetKey(UIMenu.MenuControls.Up, Keys.NumPad8);
            Pool.SetKey(UIMenu.MenuControls.Down, Keys.NumPad2);
            Pool.SetKey(UIMenu.MenuControls.Left, Keys.NumPad4);
            Pool.SetKey(UIMenu.MenuControls.Right, Keys.NumPad6);
            Pool.SetKey(UIMenu.MenuControls.Back, Keys.NumPad0);
            Pool.SetKey(UIMenu.MenuControls.Select, Keys.NumPad5);
           // mainMenu.SetKey(UIMenu.MenuControls.Up, Keys.NumPad8);
           // menuPool.SetKey(UIMenu.MenuControls.Up, GTA.Control.PhoneUp);


        }
        int tick_counter = 0;
        void onTick(object sender, EventArgs e) {
            tick_counter++;

            if (Pool != null)
            {

                Pool.ProcessMenus();
                if (tick_counter > 3)
                {
                    Pool.ProcessKey(Keys.NumPad0);
                    /*menuPool.ProcessKey(Keys.NumPad2);
                    menuPool.ProcessKey(Keys.NumPad4);
                    menuPool.ProcessKey(Keys.NumPad6);
                    menuPool.ProcessKey(Keys.NumPad5);
                    menuPool.ProcessKey(Keys.NumPad0);*/
                    tick_counter = 0;
                }
            }
            //menuPool.ProcessKey(Keys.Nu)
            try
            {
                playerMenu.update();
                worldMenu.update();
                vehicleMenu.update();
                #if DEBUG
                debugMenu.update();
                #endif
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        void onKeyDown(object sender, KeyEventArgs e) {
            try
            {
                vehicleMenu.onKeyDown(sender, e);
                #if DEBUG
                debugMenu.onKeyDown(sender, e);

                #endif
                teleportMenu.onKeyDown(sender, e);
                playerMenu.onKeyDown(sender, e);
            }catch(Exception exception)
            {
                UI.Notify("~r~ Exception on keyDown:" + exception.Message);
                logger.Error(exception);
            }
            Ped playerPed = Game.Player.Character;
            //if (Game.IsPaused) return;
            if (e.KeyCode == _open_menu_key && !Pool.IsAnyMenuOpen())
            {
                mainMenu.Visible = !mainMenu.Visible; //toggle
            }

        }

    }
}
