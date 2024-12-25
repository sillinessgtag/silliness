using PlayFab.ExperimentationModels;
using silliness.Classes;
using silliness.Mods;
using static silliness.Menu.Customization;

namespace silliness.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main [0]
                new ButtonInfo { buttonText = "settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "movement mods", method =() => SettingsMods.EnterMovement(), isTogglable = false, toolTip = "Opens the movement page for the menu."},
                new ButtonInfo { buttonText = "visual mods", method =() => SettingsMods.EnterVisuals(), isTogglable = false, toolTip = "Opens the visual page for the menu."},
                new ButtonInfo { buttonText = "rig mods", method =() => SettingsMods.EnterRigs(), isTogglable = false, toolTip = "Opens the rig page for the menu."},
            },

            new ButtonInfo[] { // Movement Mods [1]
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "platforms <color=white>[</color><color=green>G</color><color=white>]</color>", method =() => Movement.Platforms(), toolTip = "Spawns an object at your hands that you can stand on."},
                new ButtonInfo { buttonText = "trigger platforms", method =() => Movement.TriggerPlatforms(), toolTip = "Same as platforms but with triggers."},
                new ButtonInfo { buttonText = "speedboost", method =() => Movement.Speedboost(), toolTip = "Makes you go faster, speed can be changed with [speedboost speed]."},
                new ButtonInfo { buttonText = "speedboost speed [normal]", method =() => Movement.ChangeSpeedboostType(), isTogglable = false, toolTip = "Changes the speed you go with speedboost."},
                new ButtonInfo { buttonText = "low gravity", method =() => Movement.LowGravity(), toolTip = "Makes you fall slower."},
                new ButtonInfo { buttonText = "zero gravity", method =() => Movement.ZeroGravity(), toolTip = "Makes you float away."},
                new ButtonInfo { buttonText = "slippery hands", enableMethod =() => Movement.EnableSlipperyHands(), disableMethod =() => Movement.DisableSlipperyHands(), toolTip = "Makes you slip on everything."},
                new ButtonInfo { buttonText = "grippy hands", enableMethod =() => Movement.EnableGrippyHands(), disableMethod =() => Movement.DisableGrippyHands(), toolTip = "Makes slippery objects not slippery."},
                new ButtonInfo { buttonText = "fly <color=white>[</color><color=green>G</color><color=white>]</color>", method =() => Movement.Fly(), toolTip = "Allows you to fly through the air, speed can be changed with [fly speed]."},
                new ButtonInfo { buttonText = "trigger fly", method =() => Movement.TriggerFly(), toolTip = "Same as fly but with triggers."},
                new ButtonInfo { buttonText = "noclip fly <color=white>[</color><color=green>G</color><color=white>]</color>", method =() => Movement.NoclipFly(), toolTip = "Allows you to fly through the air whilst clipping through stuff, speed can be changed with [fly speed]."},
                new ButtonInfo { buttonText = "trigger noclip fly", method =() => Movement.TriggerNoclipFly(), toolTip = "Same as noclip fly but with triggers."},
                new ButtonInfo { buttonText = "fly speed [normal]", method =() => Movement.ChangeFlyType(), isTogglable = false, toolTip = "Changes your flying speed."},
            },

            new ButtonInfo[] { // Visual Mods [2]
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "fullbright", method =() => Visuals.EnableFullBright(), toolTip = "Removes the lighting."},
                new ButtonInfo { buttonText = "set time day", method =() => Visuals.SetDay(), toolTip = "Sets the time to day."},
                new ButtonInfo { buttonText = "set time afternoon", method =() => Visuals.SetAfternoon(), toolTip = "Sets the time to afternoon."},
                new ButtonInfo { buttonText = "set time night", method =() => Visuals.SetNight(), toolTip = "Sets the time to night."},
                new ButtonInfo { buttonText = "spaz time", method =() => Visuals.SpazTime(), toolTip = "Makes the time go all over the place."},
                new ButtonInfo { buttonText = "casual chams", enableMethod =() => Visuals.CasualModeChams(), disableMethod =() => Visuals.DisableChams(), toolTip = "Shows you everyone on the map even through walls."},
                new ButtonInfo { buttonText = "casual tracers", method =() => Visuals.CasualModeTracers(), toolTip = "Shows lines that go to everyone on the map."},
                new ButtonInfo { buttonText = "infection details", enableMethod =() => Visuals.InfectionDetails(), disableMethod =() => Visuals.DisableInfectionDetails(), enabled = infectionDisplayText, toolTip = "Shows you info about your infection lobby."},
                new ButtonInfo { buttonText = "statistics", enableMethod =() => Visuals.Stats(), disableMethod =() => Visuals.DisableStats(), enabled = statisticsText, toolTip = "Shows you some useful info."},
                new ButtonInfo { buttonText = "limit 60 fps PC", enableMethod =() => Visuals.FPS60Cap(), disableMethod =() => Visuals.FPSFix(), toolTip = "Limits your fps to 60."},
                new ButtonInfo { buttonText = "limit 30 fps PC", enableMethod =() => Visuals.FPS30Cap(), disableMethod =() => Visuals.FPSFix(), toolTip = "Limits your fps to 30."},
            },

            new ButtonInfo[] { // Rig Mods [3]
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "sideways head <color=white>[</color><color=green>G</color><color=white>]</color>", method =() => Movement.SidewaysHead(), toolTip = "Makes your head go sideways."},
                new ButtonInfo { buttonText = "backwards head <color=white>[</color><color=green>G</color><color=white>]</color>", method =() => Movement.BackwardsHead(), toolTip = "Makes your head go backwards."},
                new ButtonInfo { buttonText = "ghost monke", method =() => Rig.GhostMonke(), toolTip = "Freezes your rig whilst you hold grip, allowing you to teleport around people."},
                new ButtonInfo { buttonText = "invisible monke", method =() => Rig.InvisibleMonke(), toolTip = "Makes you completely invisible."},
                new ButtonInfo { buttonText = "grab rig", method =() => Rig.GhostMonke(), toolTip = "Puts your rig in your hand."},
                new ButtonInfo { buttonText = "rig gun", method =() => Rig.GhostMonke(), toolTip = "Puts your rig wherever you point."},
            },

            new ButtonInfo[] { // Settings [4]
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "menu", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
                new ButtonInfo { buttonText = "customization", method =() => SettingsMods.CustomizationSettings(), isTogglable = false, toolTip = "Opens the customization settings for the menu."},
                new ButtonInfo { buttonText = "anti report", method =() => Safety.AntiReportDisconnect(), isTogglable = false, toolTip = "Disconnects you from the lobby before you get reported."},
                new ButtonInfo { buttonText = "manual save preferences", method =() => SettingsMods.SavePreferences(), isTogglable = false, toolTip = "Manually saves preferences."},
                new ButtonInfo { buttonText = "manual load preferences", method =() => SettingsMods.LoadPreferences(), isTogglable = false, toolTip = "Manually loads preferences."},
            },

            new ButtonInfo[] { // Customization Settings [5]
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "theme: [main] [1]", isTogglable = false, toolTip = ""},
                new ButtonInfo { buttonText = "change theme <", method =() => Customization.ChangeThemeTypeBackwards(), isTogglable = false, toolTip = "Changes the theme backwards once."},
                new ButtonInfo { buttonText = "change theme >", method =() => Customization.ChangeThemeType(), isTogglable = false, toolTip = "Changes the theme forwards once."},
                new ButtonInfo { buttonText = "font: [comic sans] [1]", isTogglable = false, toolTip = ""},
                new ButtonInfo { buttonText = "change font <", method =() => Customization.ChangeFontTypeBackwards(), isTogglable = false, toolTip = "Changes the font backwards once."},
                new ButtonInfo { buttonText = "change font >", method =() => Customization.ChangeFontType(), isTogglable = false, toolTip = "Changes the font forwards once."},
                new ButtonInfo { buttonText = "menu outlines", enableMethod =() => Customization.EnableOutlines(), disableMethod =() => Customization.DisableOutlines(), enabled = shouldOutline, toolTip = "Enables outlines on the menu."},
            },

            new ButtonInfo[] { // Menu Settings [6]
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "right hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
                new ButtonInfo { buttonText = "FPS counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the FPS counter."},
                new ButtonInfo { buttonText = "disconnect button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = "PC menu background", enableMethod =() => SettingsMods.EnablePCMenuBackground(), disableMethod =() => SettingsMods.DisablePCMenuBackground(), enabled = menuPCBackground, toolTip = "Toggles the background that appears on the pc gui."},
                new ButtonInfo { buttonText = "home button", enableMethod =() => SettingsMods.EnableHomeButton(), disableMethod =() => SettingsMods.DisableHomeButton(), enabled = homeButton, toolTip = "Toggles the home button."},
                new ButtonInfo { buttonText = "favorites button", enableMethod =() => SettingsMods.EnableFavoritesButton(), disableMethod =() => SettingsMods.DisableFavoritesButton(), enabled = favoritesButton, toolTip = "Toggles the favorites button."},
                new ButtonInfo { buttonText = "instant destroy menu", enableMethod =() => SettingsMods.EnableInstantDestroyMenu(), disableMethod =() => SettingsMods.DisableInstantDestroyMenu(), enabled = instantDestroyMenu, toolTip = "Instantly destroys the menu when it is dropped."},
                new ButtonInfo { buttonText = "zero gravity menu", enableMethod =() => SettingsMods.EnableZeroGravityMenu(), disableMethod =() => SettingsMods.DisableZeroGravityMenu(), enabled = zeroGravityMenu, toolTip = "Makes the menu float away when it is dropped."},
            },

            new ButtonInfo[] { // Movement Settings [7]
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
            },

            new ButtonInfo[] { // Projectile Settings [8]
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
            },

            new ButtonInfo[] { // Credits [9]
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
            },

            new ButtonInfo[] { // Favorite Mods [10]
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
            },

            new ButtonInfo[] { // Other Stuff
                new ButtonInfo { buttonText = "home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "disconnect", method =() => Main.Disconnect(), isTogglable = false},
                new ButtonInfo { buttonText = "favorite mods", method =() => SettingsMods.EnterFavorites(), isTogglable = false, toolTip = "Not added yet."},
            },
        };
    }
}
// 