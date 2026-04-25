using UnityEngine;

namespace BaseballGame.GamePlay.Running
{
    public class RunnerController : MonoBehaviour
    {
        public float runSpeed = 7.8f;
        public BaseController targetBase;
        public bool isRunning;
        public bool isBatterRunner;

        private void Update()
        {
            if (!isRunning || targetBase == null) return;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetBase.transform.position,
                runSpeed * Time.deltaTime
            );

            float distance = Vector3.Distance(transform.position, targetBase.transform.position);
            if (distance < 0.25f)
            {
                ArriveBase();
            }
        }

        public void RunTo(BaseController nextBase)
        {
            targetBase = nextBase;
            isRunning = true;
        }

        private void ArriveBase()
        {
            isRunning = false;
            targetBase.occupied = true;
            targetBase.currentRunner = this;
            Debug.Log("주자 도착: " + targetBase.baseName);
        }

        public float DistanceToTarget()
        {
            if (targetBase == null) return float.MaxValue;
            return Vector3.Distance(transform.position, targetBase.transform.position);
        }
    }
}
