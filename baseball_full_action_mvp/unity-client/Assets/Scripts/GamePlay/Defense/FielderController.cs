using UnityEngine;
using BaseballGame.GamePlay.Action;

namespace BaseballGame.GamePlay.Defense
{
    public class FielderController : MonoBehaviour
    {
        public FielderRole role;
        public Transform homePosition;
        public Transform glovePoint;

        [Header("Ability")]
        public float moveSpeed = 8.0f;
        public float catchRadius = 1.35f;
        public float throwingPower = 28f;
        [Range(0f, 1f)] public float fieldingSkill = 0.72f;

        private Vector3 targetPosition;
        private bool isChasing;
        private BallController targetBall;
        private ActionGameManager gameManager;

        private void Awake()
        {
            gameManager = FindFirstObjectByType<ActionGameManager>();
            if (homePosition != null)
            {
                targetPosition = homePosition.position;
            }
        }

        private void Update()
        {
            if (!isChasing) return;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (targetBall != null)
            {
                float distance = Vector3.Distance(transform.position, targetBall.transform.position);

                if (distance <= catchRadius)
                {
                    TryCatch();
                }
            }
        }

        public void ChaseBall(BallController ball, Vector3 predictedPoint)
        {
            targetBall = ball;
            targetPosition = predictedPoint;
            isChasing = true;
        }

        private void TryCatch()
        {
            if (targetBall == null || targetBall.isCaught) return;

            float difficulty = Mathf.Clamp01(targetBall.rb.linearVelocity.magnitude / 55f);
            float chance = Mathf.Clamp01(fieldingSkill - difficulty * 0.25f);

            bool success = Random.value <= chance;

            if (success)
            {
                targetBall.Catch(glovePoint != null ? glovePoint : transform);
                isChasing = false;

                if (gameManager != null)
                {
                    gameManager.OnBallCaught(this);
                }
            }
            else
            {
                isChasing = false;

                if (gameManager != null)
                {
                    gameManager.OnFielderMissed(this);
                }
            }
        }

        public void ThrowTo(Transform targetBase, BallController ball)
        {
            if (targetBase == null || ball == null) return;

            ball.transform.SetParent(null);
            ball.rb.isKinematic = false;
            ball.transform.position = glovePoint != null ? glovePoint.position : transform.position;

            Vector3 direction = (targetBase.position - ball.transform.position).normalized;
            ball.Throw(direction * throwingPower);
        }

        public void ResetPosition()
        {
            isChasing = false;
            targetBall = null;

            if (homePosition != null)
            {
                transform.position = homePosition.position;
            }
        }
    }
}
