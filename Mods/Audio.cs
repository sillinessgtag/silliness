﻿using BepInEx;
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

namespace silliness.Mods
{
    internal class Audio
    {
        public static void BackgroundMusic()
        {
            audioData.GetComponent<AudioSource>();
            audioData.Play();
            audioData.loop = true;
        }
    }
}