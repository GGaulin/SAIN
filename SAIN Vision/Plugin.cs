﻿using BepInEx;
using DrakiaXYZ.VersionChecker;
using Vision.UserSettings;
using System;
using System.Diagnostics;

namespace Vision
{
    [BepInPlugin("me.sol.sainvision", "SAIN Vision", "1.3")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            CheckEftVersion();

            VisionConfig.Init(Config);

            try
            {
                new Patches.VisibleDistancePatch().Enable();
                new Patches.GainSightPatch().Enable();
                new Patches.VisionOverridesPatch().Enable();
                //new Patches.IsPartVisiblePatch().Enable();

                //new Patches.AddComponentPatch().Enable();
            }
            catch (Exception ex)
            {
                Logger.LogError($"{GetType().Name}: {ex}");
                throw;
            }
        }
        private void CheckEftVersion()
        {
            // Make sure the version of EFT being run is the correct version
            int currentVersion = FileVersionInfo.GetVersionInfo(BepInEx.Paths.ExecutablePath).FilePrivatePart;
            int buildVersion = TarkovVersion.BuildVersion;
            if (currentVersion != buildVersion)
            {
                Logger.LogError($"ERROR: This version of {Info.Metadata.Name} v{Info.Metadata.Version} was built for Tarkov {buildVersion}, but you are running {currentVersion}. Please download the correct plugin version.");
                throw new Exception($"Invalid EFT Version ({currentVersion} != {buildVersion})");
            }
        }
    }
}