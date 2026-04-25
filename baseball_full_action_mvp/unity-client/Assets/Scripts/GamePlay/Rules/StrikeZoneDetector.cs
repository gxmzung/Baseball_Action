using UnityEngine;
using BaseballGame.GamePlay.Action;

namespace BaseballGame.GamePlay.Rules
{
    public class StrikeZoneDetector : MonoBehaviour
    {
        public ActionGameManager gameManager;

        [Header("Zone Size")]
        public Vector2 zoneHalfSize = new Vector2(0.45f, 0.65f);

        private bool judged;

        private void OnTriggerEnter(Collider other)
        {
            BallController ball = other.GetComponent<BallController>();
            if (ball == null || !ball.isPitched || judged) return;

            judged = true;

            Vector3 local = transform.InverseTransformPoint(ball.transform.position);
            bool strike = Mathf.Abs(local.x) <= zoneHalfSize.x && Mathf.Abs(local.y) <= zoneHalfSize.y;

            if (gameManager != null)
            {
                gameManager.OnPitchPassedStrikeZone(strike);
            }

            Invoke(nameof(ResetJudge), 1.0f);
        }

        private void ResetJudge()
        {
            judged = false;
        }
    }
}
