using RubyButtonAPI;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace VRC_Anti_Crash.Menu
{
    class Menu : QMNestedButton
    {
        public Menu() : base("UserInteractMenu", 4, -2, "", "Open Anti Crash", Color.white, Color.white)
        {
            new QMSingleButton(this, 1, 0, "Blacklist avatar", delegate
            {
                var avid = QMStuff.GetQuickMenuInstance().field_Private_MonoBehaviourPublicAPObBoVRBoObApAPInBoUnique_0.prop_ApiAvatar_0.id;
                File.AppendAllText(Cache.path, "\n"+avid);
                Cache.blacklisted_avatars += "\n"+avid;
            }, "Blacklists the avatar.");
            new QMSingleButton(this, 2, 0, "Remove avatar\nfrom\nblacklist", delegate
            {
                File.WriteAllText(Cache.path, File.ReadAllText(Cache.path).
                    Replace(QMStuff.GetQuickMenuInstance().field_Private_MonoBehaviourPublicAPObBoVRBoObApAPInBoUnique_0.prop_ApiAvatar_0.id, ""));
            }, "Removes avatar from blacklist.");
            new QMSingleButton(this, 3, 0, "Show avatar", delegate
            {
                var player = QMStuff.GetQuickMenuInstance().field_Private_MonoBehaviourPublicAPObBoVRBoObApAPInBoUnique_0;
                if (!Cache.blacklisted_avatars.Contains(player.prop_MonoBehaviour2PublicObSiObStTeObBoHaObSiUnique_0.prop_ApiAvatar_0.id))
                {
                    player.prop_MonoBehaviour2PublicObSiObStTeObBoHaObSiUnique_0.GetComponentInChildren<VRC_AvatarDescriptor>(includeInactive: true).transform.parent.gameObject.SetActive(true);
                }
            }, "Will only show avatar if the avatar is not blacklisted.");
            new QMSingleButton(this, 4, 0, "Copy avatar\nID", delegate
            {
                System.Windows.Forms.Clipboard.SetText(QMStuff.GetQuickMenuInstance().field_Private_MonoBehaviourPublicAPObBoVRBoObApAPInBoUnique_0.prop_ApiAvatar_0.id);
            }, "Copys avatar id to clipboard.");
            foreach (var button in QMButtonAPI.allSingleButtons)
            {
                button.getGameObject().GetComponentInChildren<Image>().sprite = Cache.button;
                button.getGameObject().GetComponentInChildren<Button>().colors = new ColorBlock
                {
                    colorMultiplier = 1f,
                    disabledColor = Color.grey,
                    highlightedColor = Color.grey * 1.5f,
                    normalColor = Color.grey,
                    pressedColor = Color.grey * 1.5f
                };
            }
            getMainButton().getGameObject().GetComponentInChildren<Image>().sprite = Cache.logo;
        }
    }
}
