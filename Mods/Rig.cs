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
    internal class Rig
    {
        public static void RigGun()
        {
            RaycastHit PointerPos;
            GameObject Pointer;
            GameObject line = new GameObject("Line");
            LineRenderer PointerLine = line.AddComponent<LineRenderer>();
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, GorillaLocomotion.Player.Instance.rightControllerTransform.forward, out PointerPos);
            if (rightGrab)
            {
                Pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Pointer.transform.position = PointerPos.point;
                Pointer.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Destroy(Pointer.GetComponent<Collider>());
                Destroy(Pointer.GetComponent<Rigidbody>());
                Pointer.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                Destroy(Pointer, Time.deltaTime);

                ColorChanger colorChanger = Pointer.AddComponent<ColorChanger>();
                colorChanger.colorInfo = backgroundColor;
                colorChanger.Start();

                PointerLine.startWidth = 0.025f; PointerLine.endWidth = 0.025f; PointerLine.positionCount = 2; PointerLine.useWorldSpace = true;
                PointerLine.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                PointerLine.SetPosition(1, Pointer.transform.localPosition);
                PointerLine.material.shader = Shader.Find("GUI/Text Shader");
                Destroy(line, Time.deltaTime);

                colorChanger = PointerLine.AddComponent<ColorChanger>();
                colorChanger.colorInfo = backgroundColor;
                colorChanger.Start();

                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = Pointer.transform.position - new Vector3(0f, 1f, 0f);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void GhostMonke()
        {
            if (rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void InvisibleMonke()
        {
            if (rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.headBodyOffset = new Vector3(999999f, 999999f, 999999f);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.headBodyOffset = new Vector3(0f, 0f, 0f);
            }
        }
        public static void GrabRig()
        {
            if (rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
    }
}
