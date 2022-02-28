using Ship;
using SubPhases;
using System;
using System.Collections.Generic;
using System.Linq;
using Upgrade;

namespace DamageDeckCardFE
{

    public class MunitionsFailure : GenericDamageCard
    {
        public MunitionsFailure()
        {
            Name = "Munitions Failure";
            Type = CriticalCardType.Ship;
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            if (Host.UpgradeBar.GetSpecialWeaponsAll().Count == 0)
            {
                Messages.ShowInfo("Munitions Failure: No secondary weapon to discard");
                DiscardEffect();
                Triggers.FinishTrigger();
            }
            else
            {
                ShowDecision();
            }
        }

        private void ShowDecision()
        {
            DiscardWeaponDecisionSubPhase subphase = Phases.StartTemporarySubPhaseNew<DiscardWeaponDecisionSubPhase>(
                Host.PilotInfo.PilotName,
                Triggers.FinishTrigger
            );

            subphase.HostShip = Host;
            subphase.DescriptionShort = Name;
            subphase.DescriptionLong = "Select secondary wepon Upgrade card to discard";

            subphase.DecisionOwner = Host.Owner;

            subphase.Start();
        }
    }

}

namespace SubPhases
{
    public class DiscardWeaponDecisionSubPhase : DecisionSubPhase
    {
        public GenericShip HostShip;

        public override void PrepareDecision(Action callBack)
        {
            DescriptionLong = "Select secondary wepon Upgrade card to discard";

            foreach(GenericUpgrade upgrade in HostShip.UpgradeBar.GetSpecialWeaponsAll())
            {
                    AddDecision(
                        upgrade.UpgradeInfo.Name,
                        delegate { DiscardWeapon(upgrade); }
                    );
                    AddTooltip(
                        upgrade.UpgradeInfo.Name,
                        upgrade.ImageUrl
                    );
            }

            DefaultDecisionName = decisions.First().Name;

            DecisionViewType = DecisionViewTypes.ImagesUpgrade;

            callBack();
        }

        private void DiscardWeapon(GenericUpgrade upgrade)
        {
            upgrade.Discard(ConfirmDecision);
        }

    }
}