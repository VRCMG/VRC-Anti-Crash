using MelonLoader;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using VRC.SDKBase;
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(VRC_Anti_Crash.Mod), "VRChat Anti Crash", "4", "Kitty")]
namespace VRC_Anti_Crash
{
    public class Mod : MelonMod
    {
        public override void OnApplicationStart()
        {
            setup();
        }
        public override void OnFixedUpdate()
        {
            update();
            if (Cache.startup_continue == false)
            {
                if (CheckVRCUiManager() == true)
                {
                    Cache.startup_continue = true;
                    ui();
                }
            }
        }
        public static bool CheckVRCUiManager()
        {
            if (GameObject.Find("/UserInterface") == null)
            {
                return false;
            }
            return true;
        }
        public static void ui()
        {
            using (var wc = new WebClient())
            {
                Cache.bundle = AssetBundle.LoadFromMemory(wc.DownloadData("https://cdn.discordapp.com/attachments/821757361671635034/821804904778694676/anti_crash"));
            }
            Cache.logo = Cache.bundle.LoadAsset<Sprite>("logo");
            Cache.button = Cache.bundle.LoadAsset<Sprite>("Button");
            new Menu.Menu();
        }
        public static void update()
        {
            foreach (var player in MonoBehaviourPublicLi1APObDiLi1Ob2InUnique.Method_Public_Static_ArrayOf_MonoBehaviourPublicAPObBoVRBoObApAPInBoUnique_0())
            {
                try
                {
                    var avatarManager = player.prop_MonoBehaviour2PublicObSiObStTeObBoHaObSiUnique_0;
                    var parent = player.prop_MonoBehaviour2PublicObSiObStTeObBoHaObSiUnique_0.GetComponentInChildren<VRC_AvatarDescriptor>(includeInactive: true).transform.parent.gameObject;
                    var containBool = Cache.blacklisted_avatars.Contains(avatarManager.prop_ApiAvatar_0.id);
                    if (containBool)
                    {
                        if (parent.active)
                        {
                            parent.SetActive(false);
                        }
                    }
                    /*else
                    {
                        if (!parent.active)
                        {
                            parent.SetActive(true);
                        }
                    }*/
                }
                catch
                {
                }
            }
        }
        public static void setup()
        {
            Cache.updateWatcher = new FileSystemWatcher();
            Cache.updateWatcher.Path = "VRC Anti Crash";
            Cache.updateWatcher.Filter = "Blacklisted avatars.txt";
            Cache.updateWatcher.NotifyFilter = NotifyFilters.LastWrite;
            Cache.updateWatcher.Changed += new FileSystemEventHandler(OnChanged);
            Cache.updateWatcher.EnableRaisingEvents = true;
            Console.WriteLine("Go to " + Directory.GetCurrentDirectory() + @"\VRC Anti Crash\Blacklisted avatars.txt to view/edit black listed avatars.");
            CheckUpdateFile();
            UpdateBlaclistedAvatars();
        }
        public static void CheckUpdateFile()
        {
            Directory.CreateDirectory("VRC Anti Crash");
            if (!File.Exists(Cache.path))
            {
                File.Create(Cache.path);
            }
        }
        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            UpdateBlaclistedAvatars();
        }
        public static void UpdateBlaclistedAvatars()
        {
            Cache.blacklisted_avatars = File.ReadAllText(Cache.path);
        }
    }
    public class Cache
    {
        public static bool startup_continue = false;
        public static AssetBundle bundle;
        public static Sprite logo;
        public static Sprite button;
        public static string path = "VRC Anti Crash/Blacklisted avatars.txt";
        public static FileSystemWatcher updateWatcher;
        public static string blacklisted_avatars;
    }
}
