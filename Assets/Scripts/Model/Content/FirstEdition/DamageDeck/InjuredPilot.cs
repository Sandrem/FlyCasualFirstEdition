using Abilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrade;

namespace DamageDeckCardFE
{

    public class InjuredPilot : GenericDamageCard
    {
        public InjuredPilot()
        {
            Name = "Injured Pilot";
            Type = CriticalCardType.Pilot;
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Host.Tokens.AssignCondition(typeof(Tokens.InjuredPilotCritToken));

            foreach (GenericAbility ability in Host.PilotAbilities)
            {
                ability.DeactivateAbility();
            }

            foreach (GenericUpgrade upgrade in Host.UpgradeBar.GetInstalledUpgrades(UpgradeType.Talent))
            {
                foreach (GenericAbility ability in upgrade.UpgradeAbilities)
                {
                    ability.DeactivateAbility();
                }
            }

            Triggers.FinishTrigger();
        }

        public override void DiscardEffect()
        {
            base.DiscardEffect();

            Host.Tokens.RemoveCondition(typeof(Tokens.InjuredPilotCritToken));

            foreach (GenericAbility ability in Host.PilotAbilities)
            {
                ability.DeactivateAbility();
            }

            foreach (GenericUpgrade upgrade in Host.UpgradeBar.GetInstalledUpgrades(UpgradeType.Talent))
            {
                foreach (GenericAbility ability in upgrade.UpgradeAbilities)
                {
                    ability.ActivateAbility();
                }
            }

            Messages.ShowInfo("Injured Pilot has been repaired, all abilities are active again");
        }
    }

}