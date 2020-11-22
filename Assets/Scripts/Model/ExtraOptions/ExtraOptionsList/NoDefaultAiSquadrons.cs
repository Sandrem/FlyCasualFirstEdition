namespace ExtraOptions
{
    namespace ExtraOptionsList
    {
        public class NoDefaultAiSquadronsExtraOption : ExtraOption
        {
            public NoDefaultAiSquadronsExtraOption()
            {
                Name = "No Default AI Squadrons";
                Description = "Use only own custom squadrons for \"Vs Random AI Squadron\" mode.\n" +
                    "(Folder path (Windows): %userprofile%\\AppData\\LocalLow\\Sandrem\\Fly Casual FE\\First Edition\\RandomAiSquadrons)\n" +
                    "(Folder path (MacOS): ~/Library/Application Support/unity.Sandrem.X-Wing TMG Simulator FE/First Edition/RandomAiSquadrons)";
            }

            protected override void Activate()
            {
                DebugManager.NoDefaultAiSquads = true;
            }

            protected override void Deactivate()
            {
                DebugManager.NoDefaultAiSquads = false;
            }
        }
    }
}
