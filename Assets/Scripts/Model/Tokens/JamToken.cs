using Editions;
using Ship;

namespace Tokens
{
    public class JamToken : GenericToken
    {
        public Players.GenericPlayer Assigner;

        public JamToken(GenericShip host, Players.GenericPlayer assigner) : base(host)
        {
            Name = "Jam Token";
            ImageName = "Jam Token FE";
            Temporary = false;
            TokenColor = TokenColors.Orange;
            PriorityUI = 40;
            Tooltip = "https://raw.githubusercontent.com/guidokessels/xwing-data/master/images/reference-cards/ReloadActionAndJamTokens.png";
            Assigner = assigner;
        }
    }

}
