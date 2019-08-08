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
    abstract class MenuItem : Script
    {
        protected UIMenu menu;
        protected Player player = Game.Player;
        protected Ped playerPed = Game.Player.Character;
        protected Vehicle playerVehicle;
        public MenuItem(UIMenu menu)
        {
            this.menu = menu;
            menu.OnItemSelect += onItemSelect;
            menu.OnCheckboxChange += onCheckboxChange;
        }
        public virtual void update()
        {
            if (this.playerPed != Game.Player.Character) this.playerPed = Game.Player.Character;
            if(this.playerVehicle != playerPed.CurrentVehicle) this.playerVehicle = playerPed.CurrentVehicle;
        }
        public virtual void onKeyDown(object sender, KeyEventArgs e)
        {

        }
        public virtual void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {

        }
        public virtual void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked)
        {

        }
    }
}
