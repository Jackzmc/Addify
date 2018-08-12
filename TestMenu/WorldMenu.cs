using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using NativeUI;

namespace TestMenu {
    class WorldMenu {
        public static UIMenu menu;

        public static UIMenuListItem time_hr;
        public static UIMenuListItem time_min;
        public static UIMenuListItem weather;
        public static UIMenuCheckboxItem freezeTime;
        public static UIMenuCheckboxItem blackout = new UIMenuCheckboxItem("Blackout", false);

        public static UIMenuItem timeInc;
        public static UIMenuItem timeDec;

        public static bool timeFrozen = false;
        public static bool blackoutActive = false;

        public static List<dynamic> weatherList = new List<dynamic>();
        public static List<dynamic> hourList = new List<dynamic>();
        public static List<dynamic> minuteList = new List<dynamic>() {

        };

        public static void menuInit(UIMenu menuin) {
            menu = menuin;

            Weather[] allWeathers = (Weather[])Enum.GetValues(typeof(Weather));
            for(int i=0;i<allWeathers.Length;i++) {
                weatherList.Add(allWeathers[i]);
            }
            for(int i=0;i<=24;i++) {
                hourList.Add(i);
            }
            for(int i=0;i<=60;i++) {
                minuteList.Add(i);
            }

            weather = new UIMenuListItem("Weather", weatherList, 0);
            timeInc = new UIMenuItem("Time ahead an hour");
            timeDec = new UIMenuItem("Time behind an hour");
            freezeTime = new UIMenuCheckboxItem("Freeze Time",false);
            time_hr = new UIMenuListItem("Hour", hourList, 0);
            time_min = new UIMenuListItem("Minutes", minuteList, 0);

            menu.OnItemSelect += (sender, item, index) => {
                if(item == weather) {
                    int listIndex = weather.Index;
                    Weather newWeather = (Weather)weatherList[listIndex];
                    GTA.World.Weather = newWeather;
                }else if(item == time_hr) {
                    int min = GTA.Native.Function.Call<int>(Hash.GET_CLOCK_MINUTES);
                    int hour = (int)hourList[time_hr.Index];
                    GTA.Native.Function.Call(Hash.SET_CLOCK_TIME, hour, min, 0);
                } else if(item == time_min) {
                    int min = (int)minuteList[time_min.Index];
                    int hour = GTA.Native.Function.Call<int>(Hash.GET_CLOCK_HOURS);
                    GTA.Native.Function.Call(Hash.SET_CLOCK_TIME, hour, min, 0);
                }else if(item == timeInc) {
                    GTA.Native.Function.Call(Hash.ADD_TO_CLOCK_TIME, 1, 0, 0);
                } else if(item == timeDec) {
                    GTA.Native.Function.Call(Hash.ADD_TO_CLOCK_TIME, -1, 0, 0);
                }
            };
            menu.OnCheckboxChange += onCheckboxChange;
            void onCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkbox, bool Checked) {
                if(checkbox == freezeTime) {
                    //hour,min,sec
                    timeFrozen = !timeFrozen;
                } else if (checkbox == blackout) {
                    blackoutActive = !blackoutActive;
                    GTA.World.SetBlackout(blackoutActive);
                }
            }

            menu.AddItem(time_hr);
            menu.AddItem(time_min);
            menu.AddItem(timeInc);
            menu.AddItem(timeDec);
            menu.AddItem(weather);
            menu.AddItem(freezeTime);
            menu.AddItem(blackout);
        }
        public static void update() {
            if(timeFrozen) {
                int hour = GTA.Native.Function.Call<int>(Hash.GET_CLOCK_HOURS);
                int min = GTA.Native.Function.Call<int>(Hash.GET_CLOCK_MINUTES);
                GTA.Native.Function.Call(Hash.SET_CLOCK_TIME, hour, min, 0);
            }
        }
    }
}
