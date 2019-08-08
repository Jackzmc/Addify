using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify
{
    class WorldMenu_Weather : MenuItem
    {
        static UIMenuListItem menu_time_hr;
        static UIMenuListItem menu_time_min;
        static UIMenuListItem menu_weather;
        static UIMenuItem menu_timeInc = new UIMenuItem("Time +1hr");
        static UIMenuItem menu_timeDec = new UIMenuItem("Time -1hr");
        static UIMenuCheckboxItem menu_freezeTime = new UIMenuCheckboxItem("Freeze Time", false);


        List<dynamic> weatherList = Enum.GetValues(typeof(Weather)).Cast<dynamic>().ToList();
        List<dynamic> hourList = Enumerable.Range(0, 24).Cast<dynamic>().ToList();
        List<dynamic> minuteList = Enumerable.Range(0, 60).Cast<dynamic>().ToList();
        public WorldMenu_Weather(UIMenu menu) : base(menu)
        {
            menu_time_hr = new UIMenuListItem("Hour", hourList, 0);
            menu_time_min = new UIMenuListItem("Minutes", minuteList, 0);
            menu_weather = new UIMenuListItem("Weather", weatherList, 0);
            menu.AddItem(menu_time_hr);
            menu.AddItem(menu_time_min);
            menu.AddItem(menu_freezeTime);
            menu.AddItem(menu_timeInc);
            menu.AddItem(menu_timeDec);
            menu.AddItem(menu_weather);
        }
        public override void update()
        {
            if (menu_freezeTime.Checked)
            {
                int hour = Function.Call<int>(Hash.GET_CLOCK_HOURS);
                int min = Function.Call<int>(Hash.GET_CLOCK_MINUTES);
                Function.Call(Hash.SET_CLOCK_TIME, hour, min, 0);
            }
        }

        public override void onItemSelect(UIMenu sender, UIMenuItem item, int index)
        {
            if (item == menu_weather)
            {
                int listIndex = menu_weather.Index;
                Weather newWeather = (Weather)weatherList[listIndex];
                GTA.World.Weather = newWeather;
            }
            else if (item == menu_time_hr)
            {
                int min = GTA.Native.Function.Call<int>(Hash.GET_CLOCK_MINUTES);
                int hour = (int)hourList[menu_time_hr.Index];
                GTA.Native.Function.Call(Hash.SET_CLOCK_TIME, hour, min, 0);
            }
            else if (item == menu_time_min)
            {
                int min = (int)minuteList[menu_time_min.Index];
                int hour = GTA.Native.Function.Call<int>(Hash.GET_CLOCK_HOURS);
                GTA.Native.Function.Call(Hash.SET_CLOCK_TIME, hour, min, 0);
            }
            else if (item == menu_timeInc)
            {
                GTA.Native.Function.Call(Hash.ADD_TO_CLOCK_TIME, 1, 0, 0);
            }
            else if (item == menu_timeDec)
            {
                GTA.Native.Function.Call(Hash.ADD_TO_CLOCK_TIME, -1, 0, 0);
            }
        }
    }
}
