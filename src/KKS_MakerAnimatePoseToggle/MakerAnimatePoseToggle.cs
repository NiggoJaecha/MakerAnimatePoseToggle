using BepInEx;
using BepInEx.Configuration;
using KKAPI;
using KKAPI.Maker;
using KKAPI.Maker.UI.Sidebar;
using UniRx;

namespace MakerAnimatePoseToggle
{
    [BepInPlugin(GUID, PluginName, Version)]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]
    [BepInProcess(KoikatuAPI.GameProcessName)]
    public class MakerAnimatePoseToggle : BaseUnityPlugin
    {
        public const string GUID = "com.njaecha.MakerAnimatePoseToggle";
        public const string PluginName = "KKS_MakerAnimatePoseToggle";
        public const string Version = "1.1.0";

        private SidebarToggle toggle;

        internal static ConfigEntry<bool> default_;

        private void Awake()
        {
            MakerAPI.MakerBaseLoaded += createSideBarToggle;
            default_ = Config.Bind("Gernal", "Animate Pose Default", true, "If disabled the Pose Animation will be disabled by dafault");
        }

        public void setAnimSpeed(int speed)
        {
            ChaControl chara = MakerAPI.GetCharacterControl();
            chara.animBody.speed = speed;
        }

        private void createSideBarToggle(object sender, RegisterCustomControlsEvent e)
        {
            toggle = e.AddSidebarControl(new SidebarToggle("Animate Pose", default_.Value, this));
            toggle.ValueChanged.Subscribe(delegate (bool b) { setAnimSpeed(b ? 1 : 0); });
        }
    }
}
