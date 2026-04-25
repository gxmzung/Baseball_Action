using UnityEngine;
using BaseballGame.GamePlay.Action;
using BaseballGame.GamePlay.Rules;

namespace BaseballGame.GamePlay.Running
{
    public class RunnerManager : MonoBehaviour
    {
        public RunnerController batterRunnerPrefab;
        public Transform batterStartPoint;

        public BaseController firstBase;
        public BaseController secondBase;
        public BaseController thirdBase;
        public BaseController homeBase;

        private RunnerController batterRunner;

        public void OnBallHit()
        {
            SpawnBatterRunner();

            if (batterRunner != null)
            {
                batterRunner.RunTo(firstBase);
            }
        }

        public void AdvanceBatterByHitQuality(float quality)
        {
            if (batterRunner == null) return;

            if (quality > 0.82f && thirdBase != null)
            {
                batterRunner.RunTo(thirdBase);
            }
            else if (quality > 0.62f && secondBase != null)
            {
                batterRunner.RunTo(secondBase);
            }
            else
            {
                batterRunner.RunTo(firstBase);
            }
        }

        public bool IsBatterRunnerOutAtFirst(BallController ball)
        {
            if (batterRunner == null || firstBase == null || ball == null) return false;

            float ballDistanceToFirst = Vector3.Distance(ball.transform.position, firstBase.transform.position);
            float runnerDistanceToFirst = batterRunner.DistanceToTarget();

            bool ballReachedFirst = ballDistanceToFirst < 1.8f;
            bool runnerNotArrived = runnerDistanceToFirst > 0.5f;

            return ballReachedFirst && runnerNotArrived;
        }

        public void ScoreHomeRun(ScoreManager scoreManager)
        {
            if (scoreManager != null)
            {
                scoreManager.AddRun(1);
            }

            Debug.Log("홈런 득점 +1");
        }

        private void SpawnBatterRunner()
        {
            if (batterRunnerPrefab == null || batterStartPoint == null) return;

            batterRunner = Instantiate(
                batterRunnerPrefab,
                batterStartPoint.position,
                batterStartPoint.rotation
            );

            batterRunner.isBatterRunner = true;
        }
    }
}
