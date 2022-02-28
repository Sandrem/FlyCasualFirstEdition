namespace ExtraOptions
{
    namespace ExtraOptionsList
    {
        public class OldDamageDeckExtraOption : ExtraOption
        {
            public OldDamageDeckExtraOption()
            {
                Name = "Old Damage Deck";
                Description = "Use Original Core Set damage deck instead of Force Awakens Core Set damage deck.";
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
