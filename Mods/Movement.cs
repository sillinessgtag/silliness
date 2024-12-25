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
    internal class Movement
    {
        public static GameObject LeftPlatform;
        public static GameObject RightPlatform;
        public static void Platforms()
        {
            if (leftGrab)
            {
                if (LeftPlatform == null)
                {
                    LeftPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LeftPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    LeftPlatform.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    LeftPlatform.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    ColorChanger colorChanger = LeftPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();
                }
            }
            else
            {
                if (LeftPlatform != null)
                {
                    Destroy(LeftPlatform);
                }
            }
            if (rightGrab)
            {
                if (RightPlatform == null)
                {
                    RightPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RightPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    RightPlatform.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    RightPlatform.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    ColorChanger colorChanger = RightPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();
                }
            }
            else
            {
                if (RightPlatform != null)
                {
                    Destroy(RightPlatform);
                }
            }
        }
        public static void TriggerPlatforms()
        {
            if (leftTrigger > 0.5f)
            {
                if (LeftPlatform == null)
                {
                    LeftPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LeftPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    LeftPlatform.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    LeftPlatform.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    ColorChanger colorChanger = LeftPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();
                }
            }
            else
            {
                if (LeftPlatform != null)
                {
                    Destroy(LeftPlatform);
                }
            }
            if (rightTrigger > 0.5f)
            {
                if (RightPlatform == null)
                {
                    RightPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RightPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    RightPlatform.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    RightPlatform.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    ColorChanger colorChanger = RightPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();
                }
            }
            else
            {
                if (RightPlatform != null)
                {
                    Destroy(RightPlatform);
                }
            }
        }
        public static void Speedboost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = jMulti;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = mJSpeed;
        }
        public static void ChangeSpeedboostType()
        {
            speedCycle++;
            if (speedCycle > 2)
            {
                speedCycle = 0;
            }

            switch (speedCycle)
            {
                case 0:
                    GetIndex("speedboost speed [normal]").overlapText = "speedboost speed [normal]";
                    jMulti = 1.1f;
                    mJSpeed = 7.5f;
                    return;
                case 1:
                    GetIndex("speedboost speed [normal]").overlapText = "speedboost speed [fast]";
                    jMulti = 2f;
                    mJSpeed = 9f;
                    return;
                case 2:
                    GetIndex("speedboost speed [normal]").overlapText = "speedboost speed [insane]";
                    jMulti = 4f;
                    mJSpeed = 18f;
                    return;
            }
        }
        public static void LowGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
        }
        public static void ZeroGravity()
        {
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity -= Vector3.down * 9.81f * Time.deltaTime;
        }
        public static void EnableSlipperyHands()
        {
            EverythingSlippery = true;
        }
        public static void DisableSlipperyHands()
        {
            EverythingSlippery = false;
        }
        public static void EnableGrippyHands()
        {
            EverythingGrippy = true;
        }
        public static void DisableGrippyHands()
        {
            EverythingGrippy = false;
        }
        public static void Fly()
        {
            if (rightGrab || leftGrab)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * flySpeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void NoclipFly()
        {
            if (rightGrab || leftGrab)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * flySpeed;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    v.enabled = false;
                }
            }
            else
            {
                foreach (MeshCollider v in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    v.enabled = true;
                }
            }
        }
        public static void TriggerFly()
        {
            bool leftTriggerBool = leftGrab;
            bool rightTriggerBool = rightGrab;
            leftGrab = leftTrigger > 0.5f;
            rightGrab = rightTrigger > 0.5f;
            Fly();
            leftGrab = leftTriggerBool;
            rightGrab = rightTriggerBool;
        }
        public static void TriggerNoclipFly()
        {
            bool leftTriggerBool = leftGrab;
            bool rightTriggerBool = rightGrab;
            leftGrab = leftTrigger > 0.5f;
            rightGrab = rightTrigger > 0.5f;
            NoclipFly();
            leftGrab = leftTriggerBool;
            rightGrab = rightTriggerBool;
        }
        public static void ChangeFlyType()
        {
            flyCycle++;
            if (flyCycle > 2)
            {
                flyCycle = 0;
            }

            switch (flyCycle)
            {
                case 0:
                    GetIndex("fly speed [normal]").overlapText = "fly speed [normal]";
                    flySpeed = 9f;
                    return;
                case 1:
                    GetIndex("fly speed [normal]").overlapText = "fly speed [fast]";
                    flySpeed = 15f;
                    return;
                case 2:
                    GetIndex("fly speed [normal]").overlapText = "fly speed [insane]";
                    flySpeed = 30f;
                    return;
            }
        }
        public static void BackwardsHead()
        {
            if (rightGrab || leftGrab)
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 180f;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            }
        }
        public static void SidewaysHead()
        {
            if (rightGrab || leftGrab)
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 90f;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            }
        }
    }
}
