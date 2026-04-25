using UnityEngine;

namespace BaseballGame.GamePlay.Rules
{
    public class FairFoulJudge : MonoBehaviour
    {
        [Header("Field Lines")]
        public float foulLineSlope = 1.0f;
        public float homeRunDistance = 52f;

        public BallFieldZone Judge(Vector3 landingPoint)
        {
            float z = landingPoint.z;
            float x = landingPoint.x;

            if (z >= homeRunDistance && Mathf.Abs(x) < z * 0.75f)
            {
                return BallFieldZone.HomeRun;
            }

            float lineX = z * foulLineSlope;

            if (x < -lineX)
            {
                return BallFieldZone.FoulLeft;
            }

            if (x > lineX)
            {
                return BallFieldZone.FoulRight;
            }

            return BallFieldZone.Fair;
        }
    }
}
