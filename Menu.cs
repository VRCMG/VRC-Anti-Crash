using RubyButtonAPI;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
            }, "Blacklists the avatar");
            new QMSingleButton(this, 2, 0, "Remove avatar\nfrom\nblacklist", delegate
            {
                File.WriteAllText(Cache.path, File.ReadAllText(Cache.path).Replace(QMStuff.GetQuickMenuInstance().field_Private_MonoBehaviourPublicAPObBoVRBoObApAPInBoUnique_0.prop_ApiAvatar_0.id, ""));
            }, "Removes avatar from blacklist");
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
