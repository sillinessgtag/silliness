using BepInEx;
using GorillaNetworking;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using static silliness.Menu.Main;
using static silliness.Menu.Customization;

namespace silliness.Patches
{
    [Description(PluginInfo.Description)]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private void OnEnable()
        {
            Menu.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            Menu.RemoveHarmonyPatches();
        }

        private string whatshappeningimscared;
        public static Texture2D icon;
        private void OnGUI()
        {
            GUI.skin.textField.fontSize = 13;
            GUI.skin.button.fontSize = 20;
            GUI.skin.textField.font = currentFont;
            GUI.skin.button.font = currentFont;
            GUI.skin.label.font = currentFont;
            GUI.color = textColors[0];
            icon = LoadTextureFromResource("silliness.Resources.icon.png");
            GUI.DrawTexture(new Rect(Screen.width - 1920, Screen.height - 50, 200, 50), icon);
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.LowerLeft;
            GUI.Label(new Rect(Screen.width - 1915, Screen.height - 105, 512, 64), ("Version: PublicDev" + PluginInfo.Version), style);
            whatshappeningimscared = GUI.TextField(new Rect(Screen.width - 320, 20, 300, 20), whatshappeningimscared);
            if (GUI.Button(new Rect(Screen.width - 320, 50, 300, 50), "join this FUCKING room"))
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(whatshappeningimscared, JoinType.Solo);
            }
        }
    }
}
