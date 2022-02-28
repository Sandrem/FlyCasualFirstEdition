using Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageDeckCardFE
{
    public class MinorHullBreach : GenericDamageCard
    {
        public MinorHullBreach()
        {
            Name = "Minor Hull Breach";
            Type = CriticalCardType.Ship;
        }

        public override void ApplyEffect(object sender, EventArgs e)
        {
            Host.OnMovementFinish += CheckCrit;

            Host.Tokens.AssignCondition(typeof(Tokens.MinorHullBreachCritToken));
            Triggers.FinishTrigger();
        }

        private void CheckCrit(GenericShip ship)
        {
            if (ship.AssignedManeuver != null && ship.AssignedManeuver.ColorComplexity == Movement.MovementComplexity.Complex)
            {
                Triggers.RegisterTrigger(new Trigger()
                {
                    Name = "Minor Hull Breach",
                    TriggerOwner = Host.Owner.PlayerNo,
                    TriggerType = TriggerTypes.OnMovementFinish,
                    EventHandler = RollForDamage
                });
            }
        }

        protected void RollForDamage(object sender, EventArgs e)
        {
            Selection.ActiveShip = Host;
            Selection.ThisShip = Host;
            Phases.StartTemporarySubPhaseOld(
                "Minor Hull Breach",
                typeof(SubPhases.MinorHullBreachCheckSubPhase),
                delegate {
                    Phases.FinishSubPhase(typeof(SubPhases.MinorHullBreachCheckSubPhase));
                    Triggers.FinishTrigger();
                });
        }

        public override void DiscardEffect()
        {
            base.DiscardEffect();

            Host.Tokens.RemoveCondition(typeof(Tokens.MinorHullBreachCritToken));

            Host.OnMovementFinish -= CheckCrit;
        }
    }

}

namespace SubPhases
{

    public class MinorHullBreachCheckSubPhase : DiceRollCheckSubPhase
    {

        public override void Prepare()
        {
            DiceKind = DiceKind.Attack;
            DiceCount = 1;

            AfterRoll = FinishAction;

            Name = "Minor Hull Breach";
        }

        protected override void FinishAction()
        {
            HideDiceResultMenu();

            if (CurrentDiceRoll.DiceList[0].Side == DieSide.Success)
            {
                SufferDamage();
            }
            else
            {
                NoDamage();
            }

            
        }

        private void SufferDamage()
        {
            Messages.ShowInfo("Minor Hull Breach: This ship suffered 1 damage");

            DamageSourceEventArgs consolefireDamage = new DamageSourceEventArgs()
            {
                Source = "Critical hit card",
                DamageType = DamageTypes.CriticalHitCard
            };

            Selection.ActiveShip.Damage.TryResolveDamage(1, consolefireDamage, CallBack);
        }

        private void NoDamage()
        {
            Messages.ShowInfoToHuman("Minor Hull Breach: No damage was suffered");
            CallBack();
        }
    }

}