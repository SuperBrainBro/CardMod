using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CardMod
{
    internal class CardConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static CardConfig Instance;

        [ReloadRequired]
        [DefaultValue(false)]
        [Tooltip("Enables Experimental functions. (CAN BE HIGHLY UNSTABLE)" +
            "\nPossible results: crashing, game-breaking bugs, etch." +
            "\nYou was warned!")]
        [Label("Experimental Changes")]
        public bool ExperimentalFuncs;

        public override void OnLoaded() => Instance = this;
    }
}
