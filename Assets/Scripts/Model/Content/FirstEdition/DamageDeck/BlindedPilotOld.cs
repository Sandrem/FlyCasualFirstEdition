using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageDeckCardFE
{
    public class BlindedPilotOld : GenericDamageCard
    {
        public BlindedPilotOld()
        {
            Name = "Blinded Pilot";
            Type = CriticalCardType.Pilot;
            AiAvoids = true;
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Host.AfterGotNumberOfAttackDiceCap += SetCapZero;
            Host.Tokens.AssignCondition(typeof(Tokens.BlindedPilotOldCritToken));
            Triggers.FinishTrigger();
        }

        private void SetCapZero(ref int data)
        {
            Messages.ShowInfo($"{Name}: {Host.PilotInfo.PilotName} do not roll any attack dice");
            data = 0;
            DiscardEffect();
        }

        public override void DiscardEffect()
        {
            base.DiscardEffect();

            Messages.ShowInfo("Blinded Pilot has been repaired. The pilot can once again roll dice");

            Host.AfterGotNumberOfAttackDiceCap -= SetCapZero;
            Host.Tokens.RemoveCondition(typeof(Tokens.BlindedPilotOldCritToken));

            Host.AfterAttackWindow -= DiscardEffect;
        }
    }

}