using MelonLoader;
using RubyButtonAPI;
using System;
using System.IO;
using VRC.SDKBase;
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(VRC_Anti_Crash.Mod), "VRChat Anti Crash", "3", "Kitty")]
namespace VRC_Anti_Crash
{
    public class Mod : MelonMod
    {
        public override void VRChat_OnUiManagerInit()
        {
            ui();
        }
        public override void OnApplicationStart()
        {
            setup();
        }
        public override void OnFixedUpdate()
        {
            update();
        }
        public static void ui()
        {

        }
        public static void update()
        {
            foreach (var player in MonoBehaviourPublicLi1APObDiLi1Ob2InUnique.Method_Public_Static_ArrayOf_MonoBehaviourPublicAPObBoVRBoObApAPInBoUnique_0())
            {
                try
                {
                    var avatarManager = player.prop_MonoBehaviour2PublicObSiObStTeObBoHaObSiUnique_0;
                    if (Cache.blacklisted_avatars.Contains(avatarManager.prop_ApiAvatar_0.id))
                    {
                        var avatar = avatarManager.GetComponentInChildren<VRC_AvatarDescriptor>();
                        if (avatar.gameObject.active == true)
                        {
                            avatar.gameObject.SetActive(false);
                        }
                    }
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
        public static string path = "VRC Anti Crash/Blacklisted avatars.txt";
        public static FileSystemWatcher updateWatcher;
        public static string blacklisted_avatars;
    }
}
