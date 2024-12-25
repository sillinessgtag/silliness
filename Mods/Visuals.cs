using BepInEx;
using ExitGames.Client.Photon;
using GorillaTag.GuidedRefs;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.ClientModels;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using static silliness.Classes.RigManager;
using static silliness.Menu.Main;
using static silliness.Menu.Customization;
using static silliness.Mods.Global;
using static UnityEngine.Object;
using static UnityEngine.UI.CanvasScaler;
using silliness.Classes;
using GorillaTagScripts;
using PlayFab.Internal;
using System.Threading.Tasks;
using PlayFab.Json;

namespace silliness.Mods
{
    internal class Visuals
    {
        public static void EnableFullBright()
        {
            LightmapSettings.lightmaps = null;
        }
        public static void SetNight()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
        }
        public static void SetAfternoon()
        {
            BetterDayNightManager.instance.SetTimeOfDay(1);
        }
        public static void SetDay()
        {
            BetterDayNightManager.instance.SetTimeOfDay(3);
        }
        public static int TimeCount = 0;
        public static void SpazTime()
        {
            BetterDayNightManager.instance.SetTimeOfDay(TimeCount);
            TimeCount = UnityEngine.Random.Range(0, 3);
        }
        public static void CasualModeChams()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    rig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    rig.mainSkin.material.color = rig.playerColor;
                }
            }
        }
        public static void DisableChams()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    rig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                }
            }
        }
        public static void CasualModeTracers()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject line = new GameObject("Line");
                    LineRenderer PointerLine = line.AddComponent<LineRenderer>();
                    PointerLine.startWidth = 0.015f; PointerLine.endWidth = 0.015f; PointerLine.positionCount = 2; PointerLine.useWorldSpace = true;
                    PointerLine.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                    PointerLine.SetPosition(1, rig.transform.position);
                    PointerLine.material.shader = Shader.Find("GUI/Text Shader");
                    PointerLine.material.color = rig.playerColor;
                    UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }
        public static void InfectionDetails()
        {
            infectionDisplayText = true;
        }
        public static void DisableInfectionDetails()
        {
            infectionDisplayText = false;
        }
        public static void Stats()
        {
            statisticsText = true;
        }
        public static void DisableStats()
        {
            statisticsText = false;
        }
        public static void FPSFix()
        {
            Application.targetFrameRate = 144;
        }
        public static void FPS60Cap()
        {
            Application.targetFrameRate = 60;
        }
        public static void FPS30Cap()
        {
            Application.targetFrameRate = 30;
        }
    }
}