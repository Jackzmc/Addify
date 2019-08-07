using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify
{
    class WorldMenu_Train : MenuItem
    {
        static Random random = new Random();

        static UIMenuCheckboxItem menu_crazy_train = new UIMenuCheckboxItem("Fast Crazy Trains", true);
        static UIMenuListItem menu_crazy_train_forward_speed;
        static UIMenuListItem menu_crazy_train_backward_speed;
        static UIMenuListItem menu_train_model;

        static bool use_forward_list = true;

        static int BASE_X = UI.WIDTH * 2/3 + 40;
        static int BASE_Y = UI.HEIGHT;

        static readonly VehicleHash[] TRAIN_MODELS = new VehicleHash[] {
            VehicleHash.MetroTrain, VehicleHash.Freight, VehicleHash.FreightCar, VehicleHash.FreightCont1, VehicleHash.FreightCont2,  VehicleHash.FreightGrain, VehicleHash.FreightTrailer
        };
        //generate 0-500
        static readonly List<dynamic> TRAIN_SPEEDS_FORWARD = Enumerable.Range(0, 101).Select(n => 5 * n).Cast<dynamic>().ToList();
        static readonly List<dynamic> TRAIN_SPEEDS_BACKWARD = Enumerable.Range(0, 101).Select(n => -5 * n).Cast<dynamic>().ToList();
        public WorldMenu_Train(UIMenu menu) : base(menu)
        {
            menu_train_model = new UIMenuListItem("Spawn Train", TRAIN_MODELS.Cast<dynamic>().ToList(), 0);
            menu_crazy_train_forward_speed = new UIMenuListItem("Train Forward Speed [Active]", TRAIN_SPEEDS_FORWARD, 0);
            menu_crazy_train_backward_speed = new UIMenuListItem("Train Backward Speed", TRAIN_SPEEDS_BACKWARD, 0);
            
            menu.AddItem(menu_crazy_train);
            menu.AddItem(menu_crazy_train_forward_speed);
            menu.AddItem(menu_crazy_train_backward_speed);
            menu.AddItem(menu_train_model);
        }
        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if (item == menu_train_model)
            {
                var trainModel = new Model(TRAIN_MODELS[menu_train_model.Index]);
                trainModel.Request(500);

                // Check the model is valid
                if (trainModel.IsInCdImage && trainModel.IsValid)
                {
                    // If the model isn't loaded, wait until it is
                    while (!trainModel.IsLoaded) Script.Wait(100);

                    // Set the player's model
                    Vector3 location = playerPed.Position;

                    Function.Call(Hash.CREATE_MISSION_TRAIN, 18, 517.6304f, -1060.372f, 26.7011f, 0);
                }
            } else if(item == menu_crazy_train_forward_speed)
            {
                menu_crazy_train_forward_speed.Text = "Train Forward Speed [Active]";
                menu_crazy_train_backward_speed.Text = "Train Backward Speed";
                use_forward_list = true;
            } else if(item == menu_crazy_train_backward_speed)
            {
                menu_crazy_train_forward_speed.Text = "Train Forward Speed";
                menu_crazy_train_backward_speed.Text = "Train Backward Speed [Active]";
                use_forward_list = false;
            }
        }
        public override void update()
        {
            base.update();
            if(menu_crazy_train.Checked)
            {
                Vehicle[] vehicles = World.GetNearbyVehicles(playerPed.Position, 200f);
                int indexer = 0;
                foreach( Vehicle vehicle in vehicles)
                {
                    if (TRAIN_MODELS.Contains((VehicleHash)vehicle.Model.Hash))
                    {

                        /*int y = BASE_Y - (20 * indexer);
                        var veh_name = Util.GetVehicleName(vehicle);
                        Util.DrawSimpleText($"Train [{indexer}]: {vehicle.Model.Hash} ({veh_name}) s: {vehicle.Speed}", BASE_X, y);
                        //var debug = new UIResText($"Train: {vehicle.Model.Hash} ({veh_name}) s: {vehicle.Speed}", point, .4f);
                        //debug.Draw();*/

                        vehicle.Speed = use_forward_list ? TRAIN_SPEEDS_FORWARD[menu_crazy_train_forward_speed.Index] : TRAIN_SPEEDS_BACKWARD[menu_crazy_train_backward_speed.Index];
                        indexer++;
                    }
                }
            }
        }
    }
}
