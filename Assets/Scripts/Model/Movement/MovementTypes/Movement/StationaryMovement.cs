﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Movement
{

    public class StationaryMovement : GenericMovement
    {
        public StationaryMovement(int speed, ManeuverDirection direction, ManeuverBearing bearing, MovementComplexity color) : base(speed, direction, bearing, color)
        {

        }

        public override IEnumerator Perform()
        {
            Initialize();

            movementPrediction = new MovementPrediction(TheShip, this);
            yield return movementPrediction.CalculateMovementPredicition();
        }

        protected override float SetProgressTarget()
        {
            return 0;
        }

        protected override float SetAnimationSpeed()
        {
            return 1f;
        }

        public override GameObject[] PlanMovement()
        {
            GameObject[] result = new GameObject[1];

            Vector3 position = TheShip.GetPosition();

            GameObject prefab = (GameObject)Resources.Load(TheShip.ShipBase.TemporaryPrefabPath, typeof(GameObject));
            GameObject ShipStand = MonoBehaviour.Instantiate(prefab, position, TheShip.GetRotation(), BoardTools.Board.GetBoard());

            Renderer[] renderers = ShipStand.GetComponentsInChildren<Renderer>();
            foreach (var render in renderers)
            {
                render.enabled = false;
            }

            result[0] = ShipStand;

            return result;
        }

        public override GameObject[] PlanFinalPosition()
        {
            return PlanMovement();
        }

        public override void AdaptSuccessProgress() { }

    }

}

