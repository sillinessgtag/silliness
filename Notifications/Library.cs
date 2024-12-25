using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BepInEx;
using Photon.Pun;
using silliness.Classes;
using silliness.Menu;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static silliness.Menu.Customization;
using static silliness.Menu.Main;

namespace silliness.Notifications
{
    [BepInPlugin("org.gorillatag.lars.notifications2", "NotificationLibrary", "1.0.5")]
    public class NotifiLib : BaseUnityPlugin
    {
        private void Awake()
        {
            base.Logger.LogInfo("Plugin NotificationLibrary is loaded!");
        }

        private void Init()
        {
            MainCamera = GameObject.Find("Main Camera");
            HUDObj = new GameObject();
            HUDObj2 = new GameObject();
            HUDObj2.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj.AddComponent<Canvas>();
            HUDObj.AddComponent<CanvasScaler>();
            HUDObj.AddComponent<GraphicRaycaster>();
            HUDObj.GetComponent<Canvas>().enabled = true;
            HUDObj.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            HUDObj.GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
            HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
            HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - 4.6f);
            HUDObj.transform.parent = HUDObj2.transform;
            HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            Vector3 eulerAngles = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
            eulerAngles.y = -270f;
            HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
            HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);
            Testtext = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            Testtext.text = "";
            Testtext.fontSize = 20;
            Testtext.font = currentFont;
            Testtext.rectTransform.sizeDelta = new Vector2(450f, 210f);
            Testtext.alignment = TextAnchor.LowerLeft;
            Testtext.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            Testtext.rectTransform.localPosition = new Vector3(-1f, -1f, -0.5f);
            Testtext.material = AlertText;
            NotifiText = Testtext;

            Text2 = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            Text2.text = "";
            Text2.fontSize = 20;
            Text2.font = currentFont;
            Text2.rectTransform.sizeDelta = new Vector2(450f, 1000f);
            Text2.alignment = TextAnchor.LowerRight;
            Text2.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            Text2.rectTransform.localPosition = new Vector3(-0.5f, 0.65f, 0.5f);
            Text2.material = AlertText;
            InfectionDisplayText = Text2;

            Text3 = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            Text3.text = "";
            Text3.fontSize = 20;
            Text3.font = currentFont;
            Text3.rectTransform.sizeDelta = new Vector2(450f, 1000f);
            Text3.alignment = TextAnchor.UpperLeft;
            Text3.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            Text3.rectTransform.localPosition = new Vector3(-0.5f, -1.1f, -0.25f);
            Text3.material = AlertText;
            StatisticsDisplayText = Text3;

            Text4 = new GameObject
            {
                transform =
                {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            Text4.text = "";
            Text4.fontSize = 20;
            Text4.font = currentFont;
            Text4.rectTransform.sizeDelta = new Vector2(450f, 1000f);
            Text4.alignment = TextAnchor.UpperRight;
            Text4.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            Text4.rectTransform.localPosition = new Vector3(-0.5f, -1.1f, 0.5f);
            Text4.material = AlertText;
            StatisticsDisplayText2 = Text4;
        }

        private void FixedUpdate()
        {

            GorillaTagManager tagman = GameObject.Find("GT Systems/GameModeSystem/Gorilla Tag Manager").GetComponent<GorillaTagManager>();
            bool flag = !HasInit && GameObject.Find("Main Camera") != null;
            if (flag)
            {
                Init();
                HasInit = true;
            }
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            HUDObj2.transform.rotation = MainCamera.transform.rotation;
            if (infectionDisplayText == true)
            {
                InfectionDisplayText.text = String.Format("Infected: {0}\nNot Infected: {1}", tagman.currentInfected.Count(), PhotonNetwork.PlayerList.Count() - tagman.currentInfected.Count());
            }
            else
            {
                InfectionDisplayText.text = "";
            }
            if (statisticsText == true)
            {
                StatisticsDisplayText.text = $"FPS: {Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString()}\nPing: {serverPing} ms\nPlaying: {uptimeHours:D1}:{uptimeMinutes:D2}:{uptimeSeconds:D2}";
                StatisticsDisplayText2.text = $"Room: {room}\nTime: {formattedTime}\nPlayers In Lobby: {PhotonNetwork.PlayerList.Count()}";
            }
            else
            {
                StatisticsDisplayText.text = "";
                StatisticsDisplayText2.text = "";
            }
            if (Testtext.text != "")
            {
                NotificationDecayTimeCounter++;
                if (NotificationDecayTimeCounter > NotificationDecayTime)
                {
                    Notifilines = null;
                    newtext = "";
                    NotificationDecayTimeCounter = 0;
                    Notifilines = Enumerable.Skip<string>(Testtext.text.Split(Environment.NewLine.ToCharArray()), 1).ToArray();
                    foreach (string text in Notifilines)
                    {
                        if (text != "")
                        {
                            newtext = newtext + text + "\n";
                        }
                    }
                    Testtext.text = newtext;
                }
            }
            else
            {
                NotificationDecayTimeCounter = 0;
            }
        }

        public static void SendNotification(string NotificationText)
        {
            if (!disableNotifications)
            {
                try
                {
                    if (IsEnabled && PreviousNotifi != NotificationText)
                    {
                        if (!NotificationText.Contains(Environment.NewLine))
                        {
                            NotificationText += Environment.NewLine;
                        }
                        NotifiText.text = NotifiText.text + NotificationText;
                        NotifiText.supportRichText = true;
                        PreviousNotifi = NotificationText;
                    }
                }
                catch
                {
                    UnityEngine.Debug.LogError("Notification failed, object probably nil due to third person ; " + NotificationText);
                }
            }
        }

        public static void ClearAllNotifications()
        {
            //NotifiLib.NotifiText.text = "<color=grey>[</color><color=green>SUCCESS</color><color=grey>]</color> <color=white>Notifications cleared.</color>" + Environment.NewLine;
            NotifiText.text = "";
        }

        public static void ClearPastNotifications(int amount)
        {
            string text = "";
            foreach (string text2 in Enumerable.Skip<string>(NotifiText.text.Split(Environment.NewLine.ToCharArray()), amount).ToArray())
            {
                if (text2 != "")
                {
                    text = text + text2 + "\n";
                }
            }
            NotifiText.text = text;
        }

        private GameObject HUDObj;

        private GameObject HUDObj2;

        private GameObject MainCamera;

        private Text Testtext;
        private Text Text2;
        private Text Text3;
        private Text Text4;

        private Material AlertText = new Material(Shader.Find("GUI/Text Shader"));

        private int NotificationDecayTime = 144;

        private int NotificationDecayTimeCounter;

        public static int NoticationThreshold = 30;

        private string[] Notifilines;

        private string newtext;

        public static string PreviousNotifi;

        private bool HasInit;

        private static Text NotifiText;
        private static Text InfectionDisplayText;
        private static Text StatisticsDisplayText;
        private static Text StatisticsDisplayText2;

        public static bool IsEnabled = true;
    }
}
