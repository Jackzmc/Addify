using Addify.lib;
using GTA;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Addify
{
    internal abstract class MenuItem
    {
        protected UIMenu menu;
        protected Player player = Game.Player;
        protected Ped playerPed = Game.Player.Character;
        protected Vehicle playerVehicle = Game.Player.Character.CurrentVehicle;

        public MenuItem(String name)
        {
            this.menu = Main.Pool.AddSubMenu(Main.Menu, name);
            menu.OnItemSelect += onItemSelect;
            menu.OnCheckboxChange += onCheckboxChange;
        }
        public MenuItem()
        {
            this.menu = Main.Pool.AddSubMenu(Main.Menu, this.GetType().Name);
            menu.OnItemSelect += onItemSelect;
            menu.OnCheckboxChange += onCheckboxChange;
        }
        public MenuItem(UIMenu menu)
        {
            this.menu = menu;
            menu.OnItemSelect += onItemSelect;
            menu.OnCheckboxChange += onCheckboxChange;
        }
        public void AddMenus(params UIMenuItem[] menus)
        {
            for(int i=0;i<menus.Length;i++)
            {
                menu.AddItem(menus[i]);
            }
        }
        internal virtual void update()
        {
            if (this.playerPed != Game.Player.Character) this.playerPed = Game.Player.Character;
            if(this.playerVehicle != playerPed.CurrentVehicle) this.playerVehicle = playerPed.CurrentVehicle;
        }
        internal virtual void onKeyDown(object sender, KeyEventArgs e)
        {

        }
        internal virtual void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {

        }
        internal virtual void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
        {

        }
    }
}
