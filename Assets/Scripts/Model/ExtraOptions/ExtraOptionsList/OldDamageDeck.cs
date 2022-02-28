namespace ExtraOptions
{
    namespace ExtraOptionsList
    {
        public class OldDamageDeckExtraOption : ExtraOption
        {
            public OldDamageDeckExtraOption()
            {
                Name = "Old Damage Deck";
                Description = "Use origianl core set damage deck instead of Force Awakens damage deck.";
            }

            protected override void Activate()
            {
                DebugManager.OldDamageDeck = true;
            }

            protected override void Deactivate()
            {
                DebugManager.OldDamageDeck = false;
            }
        }
    }
}
