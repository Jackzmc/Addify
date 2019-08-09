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
        public static ScriptSettings Config { get; private set; }
        public static MenuPool Pool { get; private set; }
        public static UIMenu Menu { get; private set; }
        public static Logger Logger { get; private set; }

        private PlayerMenu playerMenu;
        private VehicleMenu vehicleMenu;
        private WeaponMenu weaponMenu;
        private TeleportMenu teleportMenu;
        private WorldMenu worldMenu;
        #if DEBUG
        private DebugMenu debugMenu;
        #endif

        private Keys _open_menu_key;
       

        public Main() {
            Logger = new Logger();
            Config = ScriptSettings.Load("scripts\\addify.ini");
            setupKeybinds();

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Pool = new MenuPool();
            Menu = new UIMenu("Addify", $"Version {version} by Jackz") {
                Visible = true
            };

            Pool.Add(Menu);
            playerMenu = new PlayerMenu();
            vehicleMenu = new VehicleMenu();
            worldMenu = new WorldMenu();
            weaponMenu = new WeaponMenu();
            teleportMenu = new TeleportMenu();
            #if DEBUG
            debugMenu = new DebugMenu();
            #endif
            Pool.RefreshIndex();

            setupKeys();

            UI.Notify($"Addify v{version} loaded");
            Logger.Info($"Addify V{version} loaded");

            Tick += onTick;
            KeyDown += onKeyDown;
        }
        
        #region priv_methods
        private void setupKeys()
        {
            Pool.SetKey(UIMenu.MenuControls.Up, Keys.NumPad8);
            Pool.SetKey(UIMenu.MenuControls.Down, Keys.NumPad2);
            Pool.SetKey(UIMenu.MenuControls.Left, Keys.NumPad4);
            Pool.SetKey(UIMenu.MenuControls.Right, Keys.NumPad6);
            Pool.SetKey(UIMenu.MenuControls.Back, Keys.NumPad0);
            Pool.SetKey(UIMenu.MenuControls.Select, Keys.NumPad5);
           // Menu.SetKey(UIMenu.MenuControls.Up, Keys.NumPad8);
           // menuPool.SetKey(UIMenu.MenuControls.Up, GTA.Control.PhoneUp);


        }
        private void setupKeybinds()
        {
            _open_menu_key = Config.GetValue("Keybinds", "openMenu", Keys.Subtract);
        }
        #endregion
        int tick_counter = 0;

        #region events
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
                Logger.Error(ex);
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
                Logger.Error(exception);
            }
            Ped playerPed = Game.Player.Character;
            //if (Game.IsPaused) return;
            if (e.KeyCode == _open_menu_key && !Pool.IsAnyMenuOpen())
            {
                Menu.Visible = !Menu.Visible; //toggle
            }

        }
        #endregion
    }
}
