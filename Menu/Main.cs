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
using UnityEngine.Rendering;
using GorillaLocomotion.Climbing;

namespace silliness.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        // Constant
        public static void Prefix()
        {
            // Initialize Menu
            try
            {
                bool toOpen = !rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton || rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton;
                bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);

                if (menu == null)
                {
                    if (toOpen || keyboardOpen)
                    {
                        CreateMenu();
                        RecenterMenu(rightHanded, keyboardOpen);
                        if (reference == null)
                        {
                            CreateReference(rightHanded);
                        }
                    }
                }
                else
                {
                    if (toOpen || keyboardOpen)
                    {
                        RecenterMenu(rightHanded, keyboardOpen);
                    }
                    else
                    {
                        Rigidbody comp = menu.AddComponent(typeof(Rigidbody)) as Rigidbody;
                        if (rightHanded)
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }
                        else
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }
                        if (zeroGravityMenu == true)
                        {
                            comp.useGravity = false;
                        }

                        if (instantDestroyMenu == true)
                        {
                            UnityEngine.Object.Destroy(menu);
                            menu = null;
                        }
                        else
                        {
                            UnityEngine.Object.Destroy(menu, 2);
                            menu = null;
                        }

                        UnityEngine.Object.Destroy(reference);
                        reference = null;
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }
            if (!HasLoaded)
            {
                HasLoaded = true;
                OnLaunch();
                // Octane();
            }

            // Constant
            try
            {
                rightPrimary = ControllerInputPoller.instance.rightControllerPrimaryButton;
                rightSecondary = ControllerInputPoller.instance.rightControllerSecondaryButton;
                leftPrimary = ControllerInputPoller.instance.leftControllerPrimaryButton;
                leftSecondary = ControllerInputPoller.instance.leftControllerSecondaryButton;
                leftGrab = ControllerInputPoller.instance.leftGrab;
                rightGrab = ControllerInputPoller.instance.rightGrab;
                leftTrigger = ControllerInputPoller.TriggerFloat(XRNode.LeftHand);
                rightTrigger = ControllerInputPoller.TriggerFloat(XRNode.RightHand);
                uptimeSeconds = Mathf.FloorToInt(uptime % 60);
                uptimeMinutes = Mathf.FloorToInt((uptime / 60) % 60);
                uptimeHours = Mathf.FloorToInt(uptime / 3600);
                uptime = Time.time - gameStartTime;
                deltaTime = 0.0f;
                currentTime = DateTime.Now;
                formattedTime = currentTime.ToString("HH:mm:ss");
                if (PhotonNetwork.InRoom == false)
                {
                    room = "Not In Room";
                }
                else
                {
                    serverPing = PhotonNetwork.GetPing();
                    room = PhotonNetwork.CurrentRoom.Name;
                }

                // Pre-Execution
                if (fpsCounter && fpsObject != null)
                {
                    fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString() + " - " + "VER: PublicDev" + PluginInfo.Version;
                }
                // Execute Enabled mods
                foreach (ButtonInfo[] buttonlist in buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                try
                                {
                                    v.method.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }
            try
            {
                if (Time.time > autoSaveDelay)
                {
                    autoSaveDelay = Time.time + 60f;
                    SettingsMods.SavePreferences();
                    UnityEngine.Debug.Log("preferences saved");
                }
            }
            catch { }
        }
        // Functions
        public static void CreateMenu()
        {
            // Menu Holder
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            // Menu Background
            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = menuSize;
            menuBackground.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);
            if (shouldOutline)
            {
                OutlineObj(menuBackground, true);
            }
            
            // the images get the overlapped text of the change theme button due to it being annoying as FUCK when i have to go back and edit the numbers EACH AND EVERY TIME I ADD A NEW THEME
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [vxv pad 2] [14]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/vxvpad2bg.png", "vxvpad2bg.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/destinygradient.png", "destinygradient.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [void buddies] [16]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/voidbuddiesbg.png", "voidbuddiesbg.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [asexual] [20]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/will.png", "will.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [gay] [21]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromResource("silliness.Resources.gay.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [bisexual] [22]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromResource("silliness.Resources.bisexual.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [pansexual] [23]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromResource("silliness.Resources.pansexual.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [lesbian] [24]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromResource("silliness.Resources.lesbian.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [nonbinary] [25]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromResource("silliness.Resources.nonbinary.png");
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [trans] [26]")
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromResource("silliness.Resources.trans.png");
            }

            ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();

            // Canvas
            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                Image image = new GameObject
                {
                    transform =
                {
                    parent = canvasObject.transform
                }
                }.AddComponent<Image>();
                if (destinyIcon == null)
                {
                    destinyIcon = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/destinylogo.png", "destinylogo.png");
                }
                if (destinyMat == null)
                {
                    destinyMat = new Material(image.material);
                }
                image.material = destinyMat;
                image.material.SetTexture("_MainTex", destinyIcon);
                image.color = textColors[0];
                RectTransform homecomp = image.GetComponent<RectTransform>();
                homecomp.localPosition = Vector3.zero;
                homecomp.position = new Vector3(0.06f, 0f, 0.1615f);
                homecomp.sizeDelta = new Vector2(0.195f, 0.105f);
                homecomp.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            // Title and FPS
            Text text = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = PluginInfo.Name;
            text.fontSize = 1;
            text.color = titleColor.colors[0].color;
            text.supportRichText = true;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.36f, 0.055f);
            component.position = new Vector3(0.06f, 0f, 0.1635f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                text.text = "";
            }
            else
            {
                text.text = PluginInfo.Name;
            }

            Text text3 = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            text3.font = currentFont;
            text3.text = pageName + "<color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
            text3.fontSize = 1;
            text3.color = titleColor.colors[0].color;
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                text3.color = textColors[0];
            }
            text3.supportRichText = true;
            text3.fontStyle = FontStyle.Italic;
            text3.alignment = TextAnchor.MiddleCenter;
            text3.resizeTextForBestFit = true;
            text3.resizeTextMinSize = 0;
            RectTransform component3 = text3.GetComponent<RectTransform>();
            component3.localPosition = Vector3.zero;
            component3.sizeDelta = new Vector2(0.28f, 0.02f);
            component3.position = new Vector3(0.06f, 0f, -0.17f);
            component3.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (fpsCounter)
            {
                fpsObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Text>();
                fpsObject.font = currentFont;
                fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString() + " - " + "VER: " + PluginInfo.Version;
                fpsObject.color = titleColor.colors[0].color;
                if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
                {
                    fpsObject.color = textColors[0];
                }
                fpsObject.fontSize = 1;
                fpsObject.supportRichText = true;
                fpsObject.fontStyle = FontStyle.Italic;
                fpsObject.alignment = TextAnchor.MiddleCenter;
                fpsObject.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
                fpsObject.resizeTextForBestFit = true;
                fpsObject.resizeTextMinSize = 0;
                RectTransform component4 = fpsObject.GetComponent<RectTransform>();
                component4.localPosition = Vector3.zero;
                component4.sizeDelta = new Vector2(0.28f, 0.015f);
                component4.position = new Vector3(0.06f, 0f, 0.132f);
                component4.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            // Buttons
            // Disconnect
            if (disconnectButton)
            {
                GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    disconnectbutton.layer = 2;
                }
                UnityEngine.Object.Destroy(disconnectbutton.GetComponent<Rigidbody>());
                disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
                disconnectbutton.transform.parent = menu.transform;
                disconnectbutton.transform.rotation = Quaternion.identity;
                disconnectbutton.transform.localScale = new Vector3(0.1f, 1f, 0.12f);
                disconnectbutton.transform.localPosition = new Vector3(0.5f, 0f, 0.6f);
                disconnectbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                disconnectbutton.AddComponent<silliness.Classes.Button>().relatedText = "disconnect";
                if (shouldOutline)
                {
                    OutlineObj(disconnectbutton, true);
                }

                colorChanger = disconnectbutton.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonColors[0];
                if (GetIndex("theme: [main] [1]").overlapText == "theme: [vxv pad 2] [14]")
                {
                    colorChanger.colorInfo = buttonColors[1];
                }
                if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
                {
                    colorChanger.colorInfo = titleColor;
                }
                colorChanger.Start();

                Text discontext = new GameObject
                {
                    transform =
                {
                    parent = canvasObject.transform
                }
                }.AddComponent<Text>();
                discontext.text = "disconnect";
                discontext.font = currentFont;
                discontext.fontSize = 1;
                discontext.color = textColors[0];
                discontext.alignment = TextAnchor.MiddleCenter;
                discontext.resizeTextForBestFit = true;
                discontext.resizeTextMinSize = 0;

                RectTransform rectt = discontext.GetComponent<RectTransform>();
                rectt.localPosition = Vector3.zero;
                rectt.sizeDelta = new Vector2(0.2f, 0.04f);
                rectt.localPosition = new Vector3(0.064f, 0f, 0.23f);
                rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            // Home Button
            if (homeButton == true)
            {
                GameObject homebutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    homebutton.layer = 1;
                }
                UnityEngine.Object.Destroy(homebutton.GetComponent<Rigidbody>());
                homebutton.GetComponent<BoxCollider>().isTrigger = true;
                homebutton.transform.parent = menu.transform;
                homebutton.transform.rotation = Quaternion.identity;
                homebutton.transform.localScale = new Vector3(0.1f, 0.16f, 0.12f);
                homebutton.transform.localPosition = new Vector3(0.5f, -0.64f, 0.6f);
                homebutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                homebutton.AddComponent<silliness.Classes.Button>().relatedText = "home";

                colorChanger = homebutton.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonColors[0];
                if (themeType == 14)
                {
                    colorChanger.colorInfo = buttonColors[1];
                }
                if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
                {
                    colorChanger.colorInfo = titleColor;
                }
                colorChanger.Start();

                Image image = new GameObject
                {
                    transform =
                {
                    parent = canvasObject.transform
                }
                }.AddComponent<Image>();
                if (homeIcon == null)
                {
                    homeIcon = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/home.png", "home.png");
                }
                if (homeMat == null)
                {
                    homeMat = new Material(image.material);
                }
                image.material = homeMat;
                image.material.SetTexture("_MainTex", homeIcon);
                image.color = textColors[0];
                RectTransform homecomp = image.GetComponent<RectTransform>();
                homecomp.localPosition = new Vector3(0.06f, -0.189f, 0.225f);
                homecomp.sizeDelta = new Vector2(.04f, .04f);
                homecomp.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                if (shouldOutline)
                {
                    OutlineObj(homebutton, true);
                }
            }

            // Favorite Mods Button
            if (favoritesButton == true)
            {
                GameObject favoritesbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    favoritesbutton.layer = 1;
                }
                UnityEngine.Object.Destroy(favoritesbutton.GetComponent<Rigidbody>());
                favoritesbutton.GetComponent<BoxCollider>().isTrigger = true;
                favoritesbutton.transform.parent = menu.transform;
                favoritesbutton.transform.rotation = Quaternion.identity;
                favoritesbutton.transform.localScale = new Vector3(0.1f, 0.16f, 0.12f);
                favoritesbutton.transform.localPosition = new Vector3(0.5f, 0.64f, 0.6f);
                favoritesbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                favoritesbutton.AddComponent<silliness.Classes.Button>().relatedText = "favorite mods";

                colorChanger = favoritesbutton.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonColors[0];
                if (themeType == 14)
                {
                    colorChanger.colorInfo = buttonColors[1];
                }
                if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
                {
                    colorChanger.colorInfo = titleColor;
                }
                colorChanger.Start();

                Image image = new GameObject
                {
                    transform =
                {
                    parent = canvasObject.transform
                }
                }.AddComponent<Image>();
                if (favoriteIcon == null)
                {
                    favoriteIcon = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/favorite.png", "favorite.png");
                }
                if (favoriteMat == null)
                {
                    favoriteMat = new Material(image.material);
                }
                image.material = favoriteMat;
                image.material.SetTexture("_MainTex", favoriteIcon);
                image.color = textColors[0];
                RectTransform homecomp = image.GetComponent<RectTransform>();
                homecomp.localPosition = new Vector3(0.06f, 0.189f, 0.225f);
                homecomp.sizeDelta = new Vector2(.04f, .04f);
                homecomp.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                if (shouldOutline)
                {
                    OutlineObj(favoritesbutton, true);
                }
            }

            // Page Buttons
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.1f, 0.16f, 1f);
            gameObject.transform.localPosition = new Vector3(0.5f, 0.64f, 0);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<silliness.Classes.Button>().relatedText = "PreviousPage";
            if (shouldOutline)
            {
                OutlineObj(gameObject, true);
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/destinygradient.png", "destinygradient.png");
            }

            colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [vxv pad 2] [14]")
            {
                colorChanger.colorInfo = buttonColors[1];
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                colorChanger.colorInfo = backgroundColor;
            }
            colorChanger.Start();

            text = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = "<";
            text.fontSize = 1;
            text.color = textColors[0];
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0.19f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.1f, 0.16f, 1f);
            gameObject.transform.localPosition = new Vector3(0.5f, -0.64f, 0);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<silliness.Classes.Button>().relatedText = "NextPage";
            if (shouldOutline)
            {
                OutlineObj(gameObject, true);
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://sillinessgtag.github.io/sillinessfiles/destinygradient.png", "destinygradient.png");
            }

            colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [vxv pad 2] [14]")
            {
                colorChanger.colorInfo = buttonColors[1];
            }
            if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
            {
                colorChanger.colorInfo = backgroundColor;
            }
            colorChanger.Start();

            text = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = ">";
            text.fontSize = 1;
            text.color = textColors[0];
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, -0.19f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            // Mod Buttons
            ButtonInfo[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < activeButtons.Length; i++)
            {
                CreateButton(i * 0.083f, activeButtons[i]);
            }
        }
        
        public static void CreateButton(float offset, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.07f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<silliness.Classes.Button>().relatedText = method.buttonText;

            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            if (method.enabled)
            {
                colorChanger.colorInfo = buttonColors[1];
            }
            else
            {
                colorChanger.colorInfo = buttonColors[0];
            }
            colorChanger.Start();
            if (shouldOutline)
            {
                OutlineObj(gameObject, true);
            }

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = textColors[1];
            }
            else
            {
                text.color = textColors[0];
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Normal;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .02f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;

                    // PC Menu Background
                    if (menuPCBackground == true)
                    {
                        GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        bg.transform.localScale = new Vector3(10f, 10f, 0.01f);
                        bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
                        bg.GetComponent<Renderer>().material.color = new Color32((byte)(backgroundColor.colors[0].color.r * 50), (byte)(backgroundColor.colors[0].color.g * 50), (byte)(backgroundColor.colors[0].color.b * 50), 255);
                        if (GetIndex("theme: [main] [1]").overlapText == "theme: [destiny] [15]")
                        {
                            bg.GetComponent<Renderer>().material.color = new Color32((byte)(titleColor.colors[0].color.r * 50), (byte)(titleColor.colors[0].color.g * 50), (byte)(titleColor.colors[0].color.b * 50), 255);
                        }
                        GameObject.Destroy(bg, Time.deltaTime);
                    }

                    menu.transform.parent = TPC.transform;
                    menu.transform.position = TPC.transform.position + Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)) + Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);
                    TPC.GetComponent<Camera>().fieldOfView = 90;

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                silliness.Classes.Button collide = hit.transform.gameObject.GetComponent<silliness.Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }
        
        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
        }

        public static void Toggle(string buttonText)
        {
            int lastPage = (buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage - 1;
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            }
            else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                }
                else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (target.isTogglable)
                        {
                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.enableMethod != null)
                                {
                                    try { target.enableMethod.Invoke(); } catch { }
                                }
                            }
                            else
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.disableMethod != null)
                                {
                                    try { target.disableMethod.Invoke(); } catch { }
                                }
                            }
                        }
                        else
                        {
                            NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                            if (target.method != null)
                            {
                                try { target.method.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            RecreateMenu();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }
        public static GradientColorKey[] GetMultiGradient(Color a, Color b)
        {
            return new GradientColorKey[] { new GradientColorKey(a, 0f), new GradientColorKey(b, 0.5f), new GradientColorKey(a, 1f) };
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }
        public static void OutlineObj(GameObject toOut, bool shouldBeEnabled)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localPosition = toOut.transform.localPosition;
            gameObject.transform.localScale = toOut.transform.localScale + new Vector3(-0.01f, 0.01f, 0.0075f);
            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = shouldBeEnabled ? buttonColors[1] : buttonColors[0];
            colorChanger.Start();
        }
        public static void OutlineObjNonMenu(GameObject toOut, bool shouldBeEnabled)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.transform.parent = toOut.transform.parent;
            gameObject.transform.rotation = toOut.transform.rotation;
            gameObject.transform.localPosition = toOut.transform.localPosition;
            gameObject.transform.localScale = toOut.transform.localScale + new Vector3(0.005f, 0.005f, -0.001f);
            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = shouldBeEnabled ? buttonColors[1] : buttonColors[0];
            colorChanger.Start();
        }
        public static AssetBundle assetBundle;
        public static GameObject LoadAsset(string assetName)
        {
            GameObject gameObject = null;

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("silliness.Resources.silliness");
            if (stream != null)
            {
                if (assetBundle == null)
                {
                    assetBundle = AssetBundle.LoadFromStream(stream);
                }
                gameObject = Instantiate<GameObject>(assetBundle.LoadAsset<GameObject>(assetName));
            }
            else
            {
                Debug.LogError("Failed to load asset from resource: " + assetName);
            }

            return gameObject;
        }
        public static Texture2D LoadTextureFromURL(string resourcePath, string fileName)
        {
            Texture2D texture = new Texture2D(2, 2);

            if (!Directory.Exists("silliness"))
            {
                Directory.CreateDirectory("silliness");
            }
            if (!File.Exists("silliness/" + fileName))
            {
                UnityEngine.Debug.Log("Downloading " + fileName);
                WebClient stream = new WebClient();
                stream.DownloadFile(resourcePath, "silliness/" + fileName);
            }

            byte[] bytes = File.ReadAllBytes("silliness/" + fileName);
            texture.LoadImage(bytes);

            return texture;
        }
        public static Texture2D LoadTextureFromResource(string resourcePath)
        {
            Texture2D texture = new Texture2D(2, 2);

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            if (stream != null)
            {
                byte[] fileData = new byte[stream.Length];
                stream.Read(fileData, 0, (int)stream.Length);
                texture.LoadImage(fileData);
            }
            else
            {
                Debug.LogError("Failed to load texture from resource: " + resourcePath);
            }
            return texture;
        }

        // before you ask if ii allowed me to use this; yes he did
        public static void OnLaunch()
        {
            gameStartTime = Time.time;
            try
            {
                if (!Font.GetOSInstalledFontNames().Contains("Agency FB"))
                {
                    GameObject gameObject = LoadAsset("libytext");
                    agency = gameObject.transform.Find("Text").GetComponent<Text>().font;
                    UnityEngine.Object.Destroy(gameObject);
                }
            }
            catch { }
            if (File.Exists("silliness/EnabledMods.txt") || File.Exists("silliness/EnabledTheme.txt") || File.Exists("silliness/EnabledFont.txt"))
            {
                try
                {
                    SettingsMods.LoadPreferences();
                }
                catch
                {
                    Task.Delay(1000).ContinueWith(t => SettingsMods.LoadPreferences());
                }
            }
        }
        /*public static void Octane()
        {
            GameObject Octane = LoadAsset("OctaneFinal");
            Octane.transform.localPosition = new Vector3(-60.6296f, 2.1f, -64.4838f);
            Octane.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            Octane.transform.localScale = new Vector3(2f, 2f, 2f);
            Octane.GetComponent<MeshCollider>().enabled = false;
            Octane.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
        }*/
        public static void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        public static KeyCode[] allowedKeys = 
        {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
            KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
            KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
            KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
            KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
            KeyCode.Z, KeyCode.Space, KeyCode.Backspace, KeyCode.Escape
        };

        // the horrendous section of main.cs that is also the most understandable


        // Important
        public static bool rightPrimary = false;
        public static bool rightSecondary = false;
        public static bool leftPrimary = false;
        public static bool leftSecondary = false;
        public static bool leftGrab = false;
        public static bool rightGrab = false;
        public static float leftTrigger = 0f;
        public static float rightTrigger = 0f;
        public static bool HasLoaded = false;


        // Objects
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject reference;
        public static GameObject canvasObject;
        public static SphereCollider buttonCollider;
        public static Camera TPC;
        public static Text fpsObject;


        // Data
        public static int pageNumber = 0;
        public static string pageName = "home - ";
        public static string customMenuName = "your text here";
        public static bool doCustomName = false;
        public static int buttonsType = 0;
        public static int themeType = 1;
        public static int fontCycle = 0;
        public static int speedCycle = 0;
        public static int flyCycle = 0;
        public static float flySpeed = 9f;
        public static float jMulti = 1.1f;
        public static float mJSpeed = 7.5f;
        public static bool EverythingSlippery = false;
        public static bool EverythingGrippy = false;
        public static float autoSaveDelay = Time.time + 60f;
        public static Material homeMat = null;
        public static Texture2D homeIcon = null;
        public static Material favoriteMat = null;
        public static Texture2D favoriteIcon = null;
        public static Material destinyMat = null;
        public static Texture2D destinyIcon = null;
        public static AudioSource audioData;
        public static float gameStartTime;
        public static float deltaTime;
        public static float uptime;
        public static float serverPing;
        public static int uptimeHours;
        public static int uptimeMinutes;
        public static int uptimeSeconds;
        public static string room;
        public static DateTime currentTime;
        public static string formattedTime;
        public static bool destinyactive;
        public static string PCMenuOpenButton;


        // Color Codes
        public static Color bgColor = new Color32(255, 140, 248, 255);
        public static Color borderColor = new Color32(255, 115, 246, 255);
        public static Color buBorderColor = new Color32(255, 115, 246, 255);
        public static Color buttonDefault = new Color32(255, 140, 248, 255);
        public static Color buttonClicked = new Color32(255, 85, 244, 255);
        public static Color bgColorB = new Color32(255, 140, 248, 255);
        public static Color borderColorB = new Color32(255, 115, 246, 255);
        public static Color buBorderColorB = new Color32(255, 115, 246, 255);
        public static Color buttonDefaultB = new Color32(255, 140, 248, 255);
        public static Color buttonClickedB = new Color32(255, 85, 244, 255);
        public static Color textDefault = new Color32(255, 255, 255, 255);
        public static Color textClicked = new Color32(255, 255, 255, 255);


        // Fonts
        public static Font sans = Font.CreateDynamicFontFromOSFont("Comic Sans MS", 24);
        public static Font Arial = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        public static Font Verdana = Font.CreateDynamicFontFromOSFont("Verdana", 24);
        public static Font agency = Font.CreateDynamicFontFromOSFont("Agency FB", 24);
        public static Font consolas = Font.CreateDynamicFontFromOSFont("Consolas", 24);
        public static Font ubuntu = Font.CreateDynamicFontFromOSFont("Candara", 24);
        public static Font MSGOTHIC = Font.CreateDynamicFontFromOSFont("MS Gothic", 24);
        public static Font impact = Font.CreateDynamicFontFromOSFont("Impact", 24);
        public static Font bahnschrift = Font.CreateDynamicFontFromOSFont("Bahnschrift", 24);
        public static Font calibri = Font.CreateDynamicFontFromOSFont("Calibri", 24);
        public static Font cambria = Font.CreateDynamicFontFromOSFont("Cambria", 24);
        public static Font cascadiacode = Font.CreateDynamicFontFromOSFont("Cascadia Code", 24);
        public static Font cascadiamono = Font.CreateDynamicFontFromOSFont("Cascadia Mono", 24);
        public static Font constantia = Font.CreateDynamicFontFromOSFont("Constantia", 24);
        public static Font corbel = Font.CreateDynamicFontFromOSFont("Corbel", 24);
        public static Font couriernew = Font.CreateDynamicFontFromOSFont("Courier New", 24);
        public static Font dengxian = Font.CreateDynamicFontFromOSFont("DengXian", 24);
        public static Font ebrima = Font.CreateDynamicFontFromOSFont("Ebrima", 24);
        public static Font fangsong = Font.CreateDynamicFontFromOSFont("FangSong", 24);
        public static Font franklingothic = Font.CreateDynamicFontFromOSFont("Franklin Gothic Medium", 24);
        public static Font gabriola = Font.CreateDynamicFontFromOSFont("Gabriola", 24);
        public static Font gadugi = Font.CreateDynamicFontFromOSFont("Gadugi", 24);
        public static Font georgia = Font.CreateDynamicFontFromOSFont("Georgia", 24);
        public static Font hololens = Font.CreateDynamicFontFromOSFont("HoloLens MDL2 Assets", 24);
        public static Font inkfree = Font.CreateDynamicFontFromOSFont("Ink Free", 24);
        public static Font javanesetext = Font.CreateDynamicFontFromOSFont("Javanese Text", 24);
        public static Font kaiti = Font.CreateDynamicFontFromOSFont("KaiTi", 24);
        public static Font lucidaconsole = Font.CreateDynamicFontFromOSFont("Lucida Console", 24);
    }
}
// ITS (not) FUCKING WORKING` 