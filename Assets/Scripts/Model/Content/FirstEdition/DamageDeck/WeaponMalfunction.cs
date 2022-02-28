using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageDeckCardFE
{

    public class WeaponMalfunction : GenericDamageCard
    {
        public WeaponMalfunction()
        {
            Name = "Weapon Malfunction";
            Type = CriticalCardType.Ship;
            CancelDiceResults.Add(DieSide.Success);
            CancelDiceResults.Add(DieSide.Crit);
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Host.AfterGotNumberOfPrimaryWeaponAttackDice += ReduceNumberOfAttackDice;
            Host.OnGenerateActions += CallAddCancelCritAction;

            Host.Tokens.AssignCondition(typeof(Tokens.WeaponsMalfunctionCritToken));
            Triggers.FinishTrigger();
        }

        public override void DiscardEffect()
        {
            base.DiscardEffect();

            Messages.ShowInfo("Weapons Malfunction has been repaired, " + Host.PilotInfo.PilotName + "'s number of attack dice is restored");

            Host.Tokens.RemoveCondition(typeof(Tokens.WeaponsMalfunctionCritToken));
            Host.AfterGotNumberOfPrimaryWeaponAttackDice -= ReduceNumberOfAttackDice;
            Host.OnGenerateActions -= CallAddCancelCritAction;
        }

        private void ReduceNumberOfAttackDice(ref int value)
        {
            Messages.ShowInfo("Weapons Malfunction: " + Host.PilotInfo.PilotName + "'s number of attack dice has been reduced by 1");

            value--;
        }

    }

}