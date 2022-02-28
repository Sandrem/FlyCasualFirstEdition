using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageDeckCardFE
{

    public class MinorExplosion : GenericDamageCard
    {
        public MinorExplosion()
        {
            Name = "Minor Explosion";
            Type = CriticalCardType.Ship;
            AiAvoids = true;
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Selection.ActiveShip = Host;

            Triggers.RegisterTrigger(new Trigger()
            {
                Name = "Roll for explosion damage",
                TriggerOwner = Selection.ActiveShip.Owner.PlayerNo,
                TriggerType = TriggerTypes.OnMajorExplosionCrit,
                EventHandler = RollForDamage
            });

            Triggers.ResolveTriggers(TriggerTypes.OnMajorExplosionCrit, delegate { Triggers.FinishTrigger(); });
        }

        private void RollForDamage(object sender, EventArgs e)
        {
            Phases.StartTemporarySubPhaseOld(
                "Minor Explosion",
                typeof(SubPhases.MinorExplosionCheckSubPhase),
                delegate {
                    Phases.FinishSubPhase(typeof(SubPhases.MinorExplosionCheckSubPhase));
                    Triggers.FinishTrigger();
                });
        }

    }

}

namespace SubPhases
{

    public class MinorExplosionCheckSubPhase : DiceRollCheckSubPhase
    {

        public override void Prepare()
        {
            DiceKind = DiceKind.Attack;
            DiceCount = 1;

            AfterRoll = FinishAction;
        }

        protected override void FinishAction()
        {
            HideDiceResultMenu();

            if (CurrentDiceRoll.DiceList[0].Side == DieSide.Success)
            {
                DealDamage();
            }
            else
            {
                NoDamage();
            }
        }

        private void DealDamage()
        {
            Messages.ShowInfo("A Minor Explosion causes " + Selection.ActiveShip.PilotInfo.PilotName + " to suffer an additional Critical Hit!");

            DamageSourceEventArgs minorExplosionDamage = new DamageSourceEventArgs()
            {
                Source = "Critical hit card",
                DamageType = DamageTypes.CriticalHitCard
            };

            Selection.ActiveShip.Damage.TryResolveDamage(0, 1, minorExplosionDamage, CallBack);
        }

        private void NoDamage()
        {
            Messages.ShowInfo("No damage was dealt");
            CallBack();
        }
    }

}