using static silliness.Menu.Main;
using static silliness.Menu.Customization;
using System.IO;
using static Mono.Security.X509.X520;
using UnityEngine;
using silliness.Classes;
using silliness.Menu;
using silliness.Notifications;

namespace silliness.Mods
{
    internal class SettingsMods
    {
        public static void EnterMovement()
        {
            pageName = "movement mods - ";
            buttonsType = 1;
        }
        public static void EnterVisuals ()
        {
            pageName = "visual mods - ";
            buttonsType = 2;
        }
        public static void EnterRigs()
        {
            pageName = "rig mods - ";
            buttonsType = 3;
        }
        public static void EnterSettings()
        {
            pageName = "settings - ";
            buttonsType = 4;
        }
        public static void CustomizationSettings()
        {
            pageName = "customization settings - ";
            buttonsType = 5;
        }
        public static void MenuSettings()
        {
            pageName = "menu settings - ";
            buttonsType = 6;
        }
        public static void MovementSettings()
        {
            pageName = "movement settings - ";
            buttonsType = 7;
        }
        public static void ProjectileSettings()
        {
            pageName = "projectile settings - ";
            buttonsType = 8;
        }
        public static void EnterCredits()
        {
            pageName = "credits - ";
            buttonsType = 9;
        }
        public static void EnterFavorites()
        {
            pageName = "favorite mods - ";
            buttonsType = 10;
        }
        public static void RightHand()
        {
            rightHanded = true;
        }
        public static void LeftHand()
        {
            rightHanded = false;
        }
        public static void EnableFPSCounter()
        {
            fpsCounter = true;
        }
        public static void DisableFPSCounter()
        {
            fpsCounter = false;
        }
        public static void EnableNotifications()
        {
            disableNotifications = false;
        }
        public static void DisableNotifications()
        {
            disableNotifications = true;
        }
        public static void EnableDisconnectButton()
        {
            disconnectButton = true;
        }
        public static void DisableDisconnectButton()
        {
            disconnectButton = false;
        }
        public static void EnablePCMenuBackground()
        {
            menuPCBackground = true;
        }
        public static void DisablePCMenuBackground()
        {
            menuPCBackground = false;
        }
        public static void EnableHomeButton()
        {
            homeButton = true;
        }
        public static void DisableHomeButton()
        {
            homeButton = false;
        }
        public static void EnableFavoritesButton()
        {
            favoritesButton = true;
        }
        public static void DisableFavoritesButton()
        {
            favoritesButton = false;
        }
        public static void EnableInstantDestroyMenu()
        {
            instantDestroyMenu = true;
        }
        public static void DisableInstantDestroyMenu()
        {
            instantDestroyMenu = false;
        }
        public static void EnableZeroGravityMenu()
        {
            zeroGravityMenu = true;
        }
        public static void DisableZeroGravityMenu()
        {
            zeroGravityMenu = false;
        }
        public static void SavePreferences()
        {
            string text = "";
            foreach (ButtonInfo[] buttonlist in Buttons.buttons)
            {
                foreach (ButtonInfo v in buttonlist)
                {
                    if (v.enabled && v.buttonText != "save preferences")
                    {
                        if (text == "")
                        {
                            text += v.buttonText;
                        }
                        else
                        {
                            text += "\n" + v.buttonText;
                        }
                    }
                }
            }

            if (!Directory.Exists("silliness"))
            {
                Directory.CreateDirectory("silliness");
            }
            File.WriteAllText("silliness/EnabledMods.txt", text);
            File.WriteAllText("silliness/EnabledTheme.txt", themeType.ToString());
            File.WriteAllText("silliness/EnabledFont.txt", fontCycle.ToString());
            File.WriteAllText("silliness/EnabledSpeed.txt", speedCycle.ToString());
            File.WriteAllText("silliness/EnabledFly.txt", speedCycle.ToString());
        }
        public static void LoadPreferences()
        {
            if (Directory.Exists("silliness"))
            {
                TurnOffAllMods();
                try
                {
                    string config = File.ReadAllText("silliness/EnabledMods.txt");
                    string[] activebuttons = config.Split("\n");
                    for (int index = 0; index < activebuttons.Length; index++)
                    {
                        Toggle(activebuttons[index]);
                    }
                }
                catch { }
                string themer = File.ReadAllText("silliness/EnabledTheme.txt");
                string fonter = File.ReadAllText("silliness/EnabledFont.txt");
                string speeder = File.ReadAllText("silliness/EnabledSpeed.txt");
                string flyer = File.ReadAllText("silliness/EnabledFly.txt");

                themeType = int.Parse(themer) - 1;
                Toggle("change theme >");
                fontCycle = int.Parse(fonter) - 1;
                Toggle("change font >");
                speedCycle = int.Parse(speeder) - 1;
                Toggle("speedboost speed [normal]");
                flyCycle = int.Parse(flyer) - 1;
                Toggle("fly speed [normal]");
            }
        }
        public static void TurnOffAllMods()
        {
            foreach (ButtonInfo[] buttonlist in Buttons.buttons)
            {
                foreach (ButtonInfo v in buttonlist)
                {
                    if (v.enabled)
                    {
                        Toggle(v.buttonText);
                    }
                }
            }
            NotifiLib.ClearAllNotifications();
        }
    }
}
