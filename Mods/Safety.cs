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
    internal class Safety
    {
        public static void AntiReportDisconnect()
        {
            try
            {
                foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    if (line.linePlayer == NetworkSystem.Instance.LocalPlayer)
                    {
                        Transform actualreportbutton = line.reportButton.gameObject.transform;
                        foreach (VRRig vrrigy in GorillaParent.instance.vrrigs)
                        {
                            if (vrrigy != GorillaTagger.Instance.offlineVRRig)
                            {
                                float righthand = Vector3.Distance(vrrigy.rightHandTransform.position, line.reportButton.gameObject.transform.position);
                                float lefthand = Vector3.Distance(vrrigy.leftHandTransform.position, line.reportButton.gameObject.transform.position);

                                if (righthand < 0.35f || lefthand < 0.35f)
                                {
                                    PhotonNetwork.Disconnect();
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
