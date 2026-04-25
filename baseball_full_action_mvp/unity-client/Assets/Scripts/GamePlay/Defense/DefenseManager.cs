using System.Collections.Generic;
using UnityEngine;
using BaseballGame.GamePlay.Action;

namespace BaseballGame.GamePlay.Defense
{
    public class DefenseManager : MonoBehaviour
    {
        public List<FielderController> fielders = new List<FielderController>();

        public void ReactToBattedBall(BallController ball, Vector3 predictedPoint)
        {
            if (ball == null || fielders.Count == 0) return;

            FielderController nearest = FindNearestFielder(predictedPoint);

            Debug.Log("타구 예상 위치: " + predictedPoint);
            Debug.Log("수비 선택: " + nearest.role);

            nearest.ChaseBall(ball, predictedPoint);
        }

        private FielderController FindNearestFielder(Vector3 point)
        {
            FielderController best = null;
            float bestDistance = float.MaxValue;

            foreach (var fielder in fielders)
            {
                if (fielder == null) continue;

                float distance = Vector3.Distance(fielder.transform.position, point);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = fielder;
                }
            }

            return best;
        }

        public void ThrowToBase(FielderController fielder, Transform baseTarget, BallController ball)
        {
            if (fielder == null) return;
            fielder.ThrowTo(baseTarget, ball);
        }

        public void ResetFielders()
        {
            foreach (var fielder in fielders)
            {
                if (fielder != null)
                {
                    fielder.ResetPosition();
                }
            }
        }
    }
}
