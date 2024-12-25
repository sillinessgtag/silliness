using BepInEx;
using HarmonyLib;
using Oculus.Platform;
using Photon.Pun;
using silliness;
using silliness.Classes;
using silliness.Mods;
using silliness.Notifications;
using System;
using System.Windows.Input;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using static silliness.Menu.Buttons;
using static silliness.Menu.Customization;
using static silliness.Classes.ButtonInfo;
using static silliness.Classes.RigManager;
using static silliness.Mods.SettingsMods;
using ExitGames.Client.Photon;
using GorillaNetworking;
using Photon.Realtime;
using PlayFab.ExperimentationModels;
using System.Collections.Generic;
using UnityEngine.Networking;
using Photon.Voice.Unity;
using GorillaTag;
using static silliness.Menu.Main;
using UnityEngine.InputSystem.Controls;

namespace silliness.Menu
{
    internal class Customization
    {
        public static ExtGradient backgroundColor = new ExtGradient { colors = GetSolidGradient(bgColor) };
        public static ExtGradient titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
        public static ExtGradient[] buttonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetMultiGradient(buttonDefault, buttonDefaultB)}, // Disabled
            new ExtGradient{colors = GetMultiGradient(buttonClicked, buttonClickedB)} // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            textDefault, // Disabled
            textClicked // Enabled
        };

        public static Font currentFont = sans;

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static bool disableNotifications = false;
        public static bool menuPCBackground = true;
        public static bool homeButton = true;
        public static bool favoritesButton = true;
        public static bool instantDestroyMenu = false;
        public static bool zeroGravityMenu = false;
        public static bool infectionDisplayText = false;
        public static bool statisticsText = false;
        public static bool shouldOutline = true;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1f); // Depth, Width, Height
        public static int buttonsPerPage = 9;

        public static void EnableOutlines()
        {
            shouldOutline = true;
        }
        public static void DisableOutlines()
        {
            shouldOutline = false;
        }
        public static void ChangeThemeTypeBackwards()
        {
            themeType--;
            if (themeType < 1)
            {
                themeType = 33;
            }

            switch (themeType)
            {
                case 1: // Main
                    GetIndex("theme: [main] [1]").overlapText = "theme: [main] [1]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 140, 248, 255), new Color32(255, 140, 248, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 140, 248, 255), new Color32(255, 140, 248, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 105, 244, 255), new Color32(255, 105, 244, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 2: // Light Purple
                    GetIndex("theme: [main] [1]").overlapText = "theme: [light purple] [2]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(190, 129, 255, 255), new Color32(190, 129, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(156, 64, 255, 255), new Color32(156, 64, 255, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(190, 129, 255, 255), new Color32(190, 129, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(172, 95, 255, 255), new Color32(172, 95, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(156, 64, 255, 255), // Disabled
                        new Color32(196, 140, 255, 255) // Enabled
                    };
                    return;
                case 3: // Purple
                    GetIndex("theme: [main] [1]").overlapText = "theme: [purple] [3]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(57, 32, 137, 255), new Color32(57, 32, 137, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(131, 103, 226, 255), new Color32(131, 103, 226, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(57, 32, 137, 255), new Color32(57, 32, 137, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(26, 11, 75, 255), new Color32(26, 11, 75, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(131, 103, 226, 255), // Disabled
                        new Color32(131, 103, 226, 255) // Enabled
                    };
                    return;
                case 4: // Light Blue 
                    GetIndex("theme: [main] [1]").overlapText = "theme: [light blue] [4]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(156, 222, 255, 255), new Color32(156, 222, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(65, 192, 255, 255), new Color32(65, 192, 255, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(156, 222, 255, 255), new Color32(156, 222, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(110, 207, 255, 255), new Color32(110, 207, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(65, 192, 255, 255), // Disabled
                        new Color32(195, 235, 255, 255) // Enabled
                    };
                    return;
                case 5: // Blue 
                    GetIndex("theme: [main] [1]").overlapText = "theme: [blue] [5]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(106, 109, 255, 255), new Color32(106, 109, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(151, 154, 255, 255), new Color32(151, 154, 255, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(106, 109, 255, 255), new Color32(106, 109, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(67, 71, 255, 255), new Color32(67, 71, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(151, 154, 255, 255), // Disabled
                        new Color32(151, 154, 255, 255) // Enabled
                    };
                    return;
                case 6: // iiDk 
                    GetIndex("theme: [main] [1]").overlapText = "theme: [iidk] [6]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 128, 0, 128), new Color32(255, 102, 0, 128)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 190, 125, 255), new Color32(255, 190, 125, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(170, 85, 0, 255), new Color32(170, 85, 0, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(85, 42, 0, 255), new Color32(85, 42, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(255, 190, 125, 255), // Disabled
                        new Color32(255, 190, 125, 255) // Enabled
                    };
                    return;
                case 7: // Wyvern
                    GetIndex("theme: [main] [1]").overlapText = "theme: [wyvern] [7]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(199, 115, 173, 255), new Color32(165, 233, 185, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(99, 58, 86, 255), new Color32(83, 116, 92, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(99, 58, 86, 255), new Color32(83, 116, 92, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.green // Enabled
                    };
                    return;
                case 8: // ShibaGT Gold
                    GetIndex("theme: [main] [1]").overlapText = "theme: [shibagt gold] [8]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.yellow, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.yellow, Color.yellow) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.magenta, Color.magenta) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.black // Enabled
                    };
                    return;
                case 9: // ShibaGT Genesis Dark
                    GetIndex("theme: [main] [1]").overlapText = "theme: [shibagt genesis dark] [9]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.black, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(32, 32, 32, 255), new Color32(32, 32, 32, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(32, 32, 32, 255), new Color32(32, 32, 32, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.magenta // Enabled
                    };
                    return;
                case 10: // ShibaGT Genesis Blue
                    GetIndex("theme: [main] [1]").overlapText = "theme: [shibagt genesis blue] [10]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(0, 0, 200, 255), Color.blue) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 200, 255), new Color32(0, 0, 200, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 200, 255), new Color32(0, 0, 200, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.magenta // Enabled
                    };
                    return;
                case 11: // Heal
                    GetIndex("theme: [main] [1]").overlapText = "theme: [heal] [11]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(10, 10, 10, 255), new Color32(10, 10, 10, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(60, 60, 60, 255), new Color32(60, 60, 60, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(20, 20, 20, 255), new Color32(20, 20, 20, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 12: // Symex
                    GetIndex("theme: [main] [1]").overlapText = "theme: [symex] [12]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(157, 80, 246, 255), new Color32(66, 55, 230, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(65, 34, 100, 255), new Color32(26, 24, 97, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(65, 34, 100, 255), new Color32(26, 24, 97, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.green // Enabled
                    };
                    return;

                case 13: // Rexon
                    GetIndex("theme: [main] [1]").overlapText = "theme: [rexon] [13]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(102, 5, 200, 255), new Color32(102, 5, 200, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(27, 14, 93, 255), new Color32(27, 14, 93, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(154, 15, 255, 255), new Color32(154, 15, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 14: // VXV Pad 2
                    GetIndex("theme: [main] [1]").overlapText = "theme: [vxv pad 2] [14]";
                    
                    backgroundColor = new ExtGradient { isRainbow = true };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(1, 1, 1, 255), new Color32(1, 1, 1, 255)) }, // Disabled
                        new ExtGradient{isRainbow = true } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 15: // Destiny
                    GetIndex("theme: [main] [1]").overlapText = "theme: [destiny] [15]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(34, 128, 95, 255), new Color32(34, 128, 95, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 180, 150, 255), new Color32(0, 180, 150, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 122, 99, 255), new Color32(0, 122, 99, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 16: // Void Buddies
                    GetIndex("theme: [main] [1]").overlapText = "theme: [void buddies] [16]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.red, Color.red) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.green, Color.green) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 17: // Dark
                    GetIndex("theme: [main] [1]").overlapText = "theme: [dark] [17]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(19, 22, 27, 255), new Color32(19, 22, 27, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(82, 96, 122, 255), new Color32(82, 96, 122, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(19, 22, 27, 255), new Color32(19, 22, 27, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(16, 18, 22, 255), new Color32(16, 18, 22, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(82, 96, 122, 255), // Disabled
                        new Color32(82, 96, 122, 255) // Enabled
                    };
                    return;
                case 18: // OLED
                    GetIndex("theme: [main] [1]").overlapText = "theme: [oled] [18]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(5, 5, 5, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(5, 5, 5, 255), new Color32(5, 5, 5, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(20, 20, 20, 255), new Color32(20, 20, 20, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 19: // Defile
                    GetIndex("theme: [main] [1]").overlapText = "theme: [defile] [19]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(0, 0, 25, 255), new Color32(0, 0, 30, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 0, 85, 255), new Color32(255, 0, 85, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 25, 255), new Color32(0, 0, 30, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 0, 85, 255), new Color32(255, 0, 85, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(255, 0, 85, 255), // Disabled
                        new Color32(0, 0, 30, 255) // Enabled
                    };
                    return;
                case 20: // Asexual
                    GetIndex("theme: [main] [1]").overlapText = "theme: [asexual] [20]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 21: // Gay
                    GetIndex("theme: [main] [1]").overlapText = "theme: [gay] [21]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 22: // Bisexual
                    GetIndex("theme: [main] [1]").overlapText = "theme: [bisexual] [22]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 23: // Pansexual
                    GetIndex("theme: [main] [1]").overlapText = "theme: [pansexual] [23]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 24: // Lesbian
                    GetIndex("theme: [main] [1]").overlapText = "theme: [lesbian] [24]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 25: // Nonbinary
                    GetIndex("theme: [main] [1]").overlapText = "theme: [nonbinary] [25]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 26: // Trans
                    GetIndex("theme: [main] [1]").overlapText = "theme: [trans] [26]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 27: // Red Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [red fade] [27]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.red, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.red, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 28: // Orange Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [orange fade] [28]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(249, 105, 14, 255), Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(249, 105, 14, 255), Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 29: // Yellow Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [yellow fade] [29]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.yellow, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.yellow, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 30: // Green Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [green fade] [30]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.green, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.green, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 31: // Cyan Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [cyan fade] [31]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.cyan, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.cyan, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 32: // Blue Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [blue fade] [32]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.blue, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.blue, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 33: // Magenta Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [magenta fade] [33]";
                    
                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.magenta, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.magenta, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                }
            }
        public static void ChangeThemeType()
        {
            themeType++;
            if (themeType > 33)
            {
                themeType = 1;
            }

            switch (themeType)
            {
                case 1: // Main
                    GetIndex("theme: [main] [1]").overlapText = "theme: [main] [1]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 140, 248, 255), new Color32(255, 140, 248, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 140, 248, 255), new Color32(255, 140, 248, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 105, 244, 255), new Color32(255, 105, 244, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 2: // Light Purple
                    GetIndex("theme: [main] [1]").overlapText = "theme: [light purple] [2]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(190, 129, 255, 255), new Color32(190, 129, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(156, 64, 255, 255), new Color32(156, 64, 255, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(190, 129, 255, 255), new Color32(190, 129, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(172, 95, 255, 255), new Color32(172, 95, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(156, 64, 255, 255), // Disabled
                        new Color32(196, 140, 255, 255) // Enabled
                    };
                    return;
                case 3: // Purple
                    GetIndex("theme: [main] [1]").overlapText = "theme: [purple] [3]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(57, 32, 137, 255), new Color32(57, 32, 137, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(131, 103, 226, 255), new Color32(131, 103, 226, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(57, 32, 137, 255), new Color32(57, 32, 137, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(26, 11, 75, 255), new Color32(26, 11, 75, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(131, 103, 226, 255), // Disabled
                        new Color32(131, 103, 226, 255) // Enabled
                    };
                    return;
                case 4: // Light Blue 
                    GetIndex("theme: [main] [1]").overlapText = "theme: [light blue] [4]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(156, 222, 255, 255), new Color32(156, 222, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(65, 192, 255, 255), new Color32(65, 192, 255, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(156, 222, 255, 255), new Color32(156, 222, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(110, 207, 255, 255), new Color32(110, 207, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(65, 192, 255, 255), // Disabled
                        new Color32(195, 235, 255, 255) // Enabled
                    };
                    return;
                case 5: // Blue 
                    GetIndex("theme: [main] [1]").overlapText = "theme: [blue] [5]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(106, 109, 255, 255), new Color32(106, 109, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(151, 154, 255, 255), new Color32(151, 154, 255, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(106, 109, 255, 255), new Color32(106, 109, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(67, 71, 255, 255), new Color32(67, 71, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(151, 154, 255, 255), // Disabled
                        new Color32(151, 154, 255, 255) // Enabled
                    };
                    return;
                case 6: // iiDk 
                    GetIndex("theme: [main] [1]").overlapText = "theme: [iidk] [6]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 128, 0, 128), new Color32(255, 102, 0, 128)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 190, 125, 255), new Color32(255, 190, 125, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(170, 85, 0, 255), new Color32(170, 85, 0, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(85, 42, 0, 255), new Color32(85, 42, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(255, 190, 125, 255), // Disabled
                        new Color32(255, 190, 125, 255) // Enabled
                    };
                    return;
                case 7: // Wyvern
                    GetIndex("theme: [main] [1]").overlapText = "theme: [wyvern] [7]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(199, 115, 173, 255), new Color32(165, 233, 185, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(99, 58, 86, 255), new Color32(83, 116, 92, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(99, 58, 86, 255), new Color32(83, 116, 92, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.green // Enabled
                    };
                    return;
                case 8: // ShibaGT Gold
                    GetIndex("theme: [main] [1]").overlapText = "theme: [shibagt gold] [8]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.yellow, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.yellow, Color.yellow) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.magenta, Color.magenta) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.black // Enabled
                    };
                    return;
                case 9: // ShibaGT Genesis Dark
                    GetIndex("theme: [main] [1]").overlapText = "theme: [shibagt genesis dark] [9]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.black, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(32, 32, 32, 255), new Color32(32, 32, 32, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(32, 32, 32, 255), new Color32(32, 32, 32, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.magenta // Enabled
                    };
                    return;
                case 10: // ShibaGT Genesis Blue
                    GetIndex("theme: [main] [1]").overlapText = "theme: [shibagt genesis blue] [10]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(0, 0, 200, 255), Color.blue) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 200, 255), new Color32(0, 0, 200, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 200, 255), new Color32(0, 0, 200, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.magenta // Enabled
                    };
                    return;
                case 11: // Heal
                    GetIndex("theme: [main] [1]").overlapText = "theme: [heal] [11]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(10, 10, 10, 255), new Color32(10, 10, 10, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(60, 60, 60, 255), new Color32(60, 60, 60, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(20, 20, 20, 255), new Color32(20, 20, 20, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 12: // Symex
                    GetIndex("theme: [main] [1]").overlapText = "theme: [symex] [12]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(157, 80, 246, 255), new Color32(66, 55, 230, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(65, 34, 100, 255), new Color32(26, 24, 97, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(65, 34, 100, 255), new Color32(26, 24, 97, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.green // Enabled
                    };
                    return;

                case 13: // Rexon
                    GetIndex("theme: [main] [1]").overlapText = "theme: [rexon] [13]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(102, 5, 200, 255), new Color32(102, 5, 200, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(27, 14, 93, 255), new Color32(27, 14, 93, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(154, 15, 255, 255), new Color32(154, 15, 255, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 14: // VXV Pad 2
                    GetIndex("theme: [main] [1]").overlapText = "theme: [vxv pad 2] [14]";

                    backgroundColor = new ExtGradient { isRainbow = true };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(1, 1, 1, 255), new Color32(1, 1, 1, 255)) }, // Disabled
                        new ExtGradient{isRainbow = true } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 15: // Destiny
                    GetIndex("theme: [main] [1]").overlapText = "theme: [destiny] [15]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(34, 128, 95, 255), new Color32(34, 128, 95, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 180, 150, 255), new Color32(0, 180, 150, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 122, 99, 255), new Color32(0, 122, 99, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 16: // Void Buddies
                    GetIndex("theme: [main] [1]").overlapText = "theme: [void buddies] [16]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.red, Color.red) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.green, Color.green) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 17: // Dark
                    GetIndex("theme: [main] [1]").overlapText = "theme: [dark] [17]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(19, 22, 27, 255), new Color32(19, 22, 27, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(82, 96, 122, 255), new Color32(82, 96, 122, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(19, 22, 27, 255), new Color32(19, 22, 27, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(16, 18, 22, 255), new Color32(16, 18, 22, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(82, 96, 122, 255), // Disabled
                        new Color32(82, 96, 122, 255) // Enabled
                    };
                    return;
                case 18: // OLED
                    GetIndex("theme: [main] [1]").overlapText = "theme: [oled] [18]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(5, 5, 5, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(5, 5, 5, 255), new Color32(5, 5, 5, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(20, 20, 20, 255), new Color32(20, 20, 20, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 19: // Defile
                    GetIndex("theme: [main] [1]").overlapText = "theme: [defile] [19]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(0, 0, 25, 255), new Color32(0, 0, 30, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 0, 85, 255), new Color32(255, 0, 85, 255)) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 25, 255), new Color32(0, 0, 30, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 0, 85, 255), new Color32(255, 0, 85, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        new Color32(255, 0, 85, 255), // Disabled
                        new Color32(0, 0, 30, 255) // Enabled
                    };
                    return;
                case 20: // Asexual
                    GetIndex("theme: [main] [1]").overlapText = "theme: [asexual] [20]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 21: // Gay
                    GetIndex("theme: [main] [1]").overlapText = "theme: [gay] [21]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 22: // Bisexual
                    GetIndex("theme: [main] [1]").overlapText = "theme: [bisexual] [22]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 23: // Pansexual
                    GetIndex("theme: [main] [1]").overlapText = "theme: [pansexual] [23]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 24: // Lesbian
                    GetIndex("theme: [main] [1]").overlapText = "theme: [lesbian] [24]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 25: // Nonbinary
                    GetIndex("theme: [main] [1]").overlapText = "theme: [nonbinary] [25]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 26: // Trans
                    GetIndex("theme: [main] [1]").overlapText = "theme: [trans] [26]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.gray, Color.gray) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 255)) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255)) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.black, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 27: // Red Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [red fade] [27]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.red, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.red, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 28: // Orange Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [orange fade] [28]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(new Color32(249, 105, 14, 255), Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(new Color32(249, 105, 14, 255), Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 29: // Yellow Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [yellow fade] [29]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.yellow, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.yellow, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 30: // Green Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [green fade] [30]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.green, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.green, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 31: // Cyan Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [cyan fade] [31]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.cyan, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.cyan, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 32: // Blue Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [blue fade] [32]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.blue, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.blue, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
                case 33: // Magenta Fade
                    GetIndex("theme: [main] [1]").overlapText = "theme: [magenta fade] [33]";

                    backgroundColor = new ExtGradient { colors = GetMultiGradient(Color.magenta, Color.black) };
                    titleColor = new ExtGradient { colors = GetMultiGradient(Color.white, Color.white) };
                    buttonColors = new ExtGradient[]
                    {
                        new ExtGradient{colors = GetMultiGradient(Color.magenta, Color.black) }, // Disabled
                        new ExtGradient{colors = GetMultiGradient(Color.black, Color.black) } // Enabled
                    };

                    textColors = new Color[]
                    {
                        Color.white, // Disabled
                        Color.white // Enabled
                    };
                    return;
            }
        }
        public static void ChangeFontTypeBackwards()
        {
            fontCycle--;
            if (fontCycle < 0) // to get the number of fonts exactly, add 1 onto this number
            {
                fontCycle = 26;
            }

            switch (fontCycle)
            {
                case 0:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [comic sans] [1]";
                    currentFont = sans;
                    return;
                case 1:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [arial] [2]";
                    currentFont = Arial;
                    return;
                case 2:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [verdana] [3]";
                    currentFont = Verdana;
                    return;
                case 3:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [agency] [4]";
                    currentFont = agency;
                    return;
                case 4:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [consolas] [5]";
                    currentFont = consolas;
                    return;
                case 5:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [ubuntu] [6]";
                    currentFont = ubuntu;
                    return;
                case 6:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [msgothic] [7]";
                    currentFont = MSGOTHIC;
                    return;
                case 7:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [impact] [8]";
                    currentFont = impact;
                    return;
                case 8:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [bahnschrift] [9]";
                    currentFont = bahnschrift;
                    return;
                case 9:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [calibri] [10]";
                    currentFont = calibri;
                    return;
                case 10:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [cambria] [11]";
                    currentFont = cambria;
                    return;
                case 11:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [cascadia code] [12]";
                    currentFont = cascadiacode;
                    return;
                case 12:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [constantia] [13]";
                    currentFont = constantia;
                    return;
                case 13:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [corbel] [14]";
                    currentFont = corbel;
                    return;
                case 14:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [courier new] [15]";
                    currentFont = couriernew;
                    return;
                case 15:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [dengxian] [16]";
                    currentFont = dengxian;
                    return;
                case 16:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [ebrima] [17]";
                    currentFont = ebrima;
                    return;
                case 17:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [fangsong] [18]";
                    currentFont = fangsong;
                    return;
                case 18:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [franklin gothic] [19]";
                    currentFont = franklingothic;
                    return;
                case 19:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [gabriola] [20]";
                    currentFont = gabriola;
                    return;
                case 20:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [gadugi] [21]";
                    currentFont = gadugi;
                    return;
                case 21:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [georgia] [22]";
                    currentFont = georgia;
                    return;
                case 22:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [hololens] [23]";
                    currentFont = hololens;
                    return;
                case 23:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [ink free] [24]";
                    currentFont = inkfree;
                    return;
                case 24:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [javanese text] [25]";
                    currentFont = javanesetext;
                    return;
                case 25:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [kaiti] [26]";
                    currentFont = kaiti;
                    return;
                case 26:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [lucida console] [27]";
                    currentFont = lucidaconsole;
                    return;
            }
        }
        public static void ChangeFontType()
        {
            fontCycle++;
            if (fontCycle > 26) // to get the number of fonts exactly, add 1 onto this number
            {
                fontCycle = 0;
            }

            switch (fontCycle)
            {
                case 0:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [comic sans] [1]";
                    currentFont = sans;
                    return;
                case 1:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [arial] [2]";
                    currentFont = Arial;
                    return;
                case 2:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [verdana] [3]";
                    currentFont = Verdana;
                    return;
                case 3:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [agency] [4]";
                    currentFont = agency;
                    return;
                case 4:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [consolas] [5]";
                    currentFont = consolas;
                    return;
                case 5:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [ubuntu] [6]";
                    currentFont = ubuntu;
                    return;
                case 6:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [msgothic] [7]";
                    currentFont = MSGOTHIC;
                    return;
                case 7:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [impact] [8]";
                    currentFont = impact;
                    return;
                case 8:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [bahnschrift] [9]";
                    currentFont = bahnschrift;
                    return;
                case 9:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [calibri] [10]";
                    currentFont = calibri;
                    return;
                case 10:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [cambria] [11]";
                    currentFont = cambria;
                    return;
                case 11:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [cascadia code] [12]";
                    currentFont = cascadiacode;
                    return;
                case 12:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [constantia] [13]";
                    currentFont = constantia;
                    return;
                case 13:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [corbel] [14]";
                    currentFont = corbel;
                    return;
                case 14:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [courier new] [15]";
                    currentFont = couriernew;
                    return;
                case 15:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [dengxian] [16]";
                    currentFont = dengxian;
                    return;
                case 16:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [ebrima] [17]";
                    currentFont = ebrima;
                    return;
                case 17:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [fangsong] [18]";
                    currentFont = fangsong;
                    return;
                case 18:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [franklin gothic] [19]";
                    currentFont = franklingothic;
                    return;
                case 19:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [gabriola] [20]";
                    currentFont = gabriola;
                    return;
                case 20:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [gadugi] [21]";
                    currentFont = gadugi;
                    return;
                case 21:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [georgia] [22]";
                    currentFont = georgia;
                    return;
                case 22:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [hololens] [23]";
                    currentFont = hololens;
                    return;
                case 23:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [ink free] [24]";
                    currentFont = inkfree;
                    return;
                case 24:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [javanese text] [25]";
                    currentFont = javanesetext;
                    return;
                case 25:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [kaiti] [26]";
                    currentFont = kaiti;
                    return;
                case 26:
                    GetIndex("font: [comic sans] [1]").overlapText = "font: [lucida console] [27]";
                    currentFont = lucidaconsole;
                    return;
            }
        }
    }
}
