using Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageDeckCardFE
{

    public class DamagedSensorArrayOld : GenericDamageCard
    {
        public DamagedSensorArrayOld()
        {
            Name = "Damaged Sensor Array";
            Type = CriticalCardType.Ship;
            CancelDiceResults.Add(DieSide.Success);
            CancelDiceResults.Add(DieSide.Crit);
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Host.OnTryAddAction += ForbidActionbarActions;
            Host.OnGenerateActions += CallAddCancelCritAction;

            Host.Tokens.AssignCondition(typeof(Tokens.DamagedSensorArrayOldCritToken));
            Triggers.FinishTrigger();
        }

        public override void DiscardEffect()
        {
            base.DiscardEffect();

            Messages.ShowInfo("Damaged Sensor Array has been repaired,  " + Host.PilotInfo.PilotName + " can perform actions as usual");
            Host.Tokens.RemoveCondition(typeof(Tokens.DamagedSensorArrayOldCritToken));

            Host.OnTryAddAction -= ForbidActionbarActions;

            Host.OnGenerateActions -= CallAddCancelCritAction;
        }

        private void ForbidActionbarActions(GenericShip ship, ActionsList.GenericAction action, ref bool result)
        {
            if (action.IsInActionBar) result = false;
        }

    }

}