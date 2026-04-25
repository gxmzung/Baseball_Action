using UnityEngine;

namespace BaseballGame.GamePlay.Action
{
    public class BatterController : MonoBehaviour
    {
        public ActionGameManager gameManager;
        public Transform idealContactPoint;

        [Header("Timing")]
        public float perfectTiming = 0.55f;
        public float swingWindow = 0.27f;

        [Header("Hit Power")]
        public float minHitPower = 22f;
        public float maxHitPower = 62f;

        [Header("Hitter Ability")]
        [Range(0f, 1f)] public float contactAbility = 0.7f;
        [Range(0f, 1f)] public float powerAbility = 0.7f;

        private float pitchStartTime;
        private bool canSwing;

        public void EnableSwingWindow()
        {
            pitchStartTime = Time.time;
            canSwing = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TrySwing();
            }
        }

        public void TrySwing()
        {
            if (!canSwing) return;

            canSwing = false;

            float elapsed = Time.time - pitchStartTime;
            float timingError = Mathf.Abs(elapsed - perfectTiming);

            if (timingError > swingWindow)
            {
                Debug.Log("헛스윙");
                if (gameManager != null) gameManager.OnSwingMiss();
                return;
            }

            float timingQuality = 1f - Mathf.Clamp01(timingError / swingWindow);
            float finalQuality = Mathf.Clamp01((timingQuality * 0.75f) + (contactAbility * 0.25f));

            Vector3 hitVelocity = CalculateHitVelocity(finalQuality);

            if (gameManager != null)
            {
                gameManager.OnBallHit(hitVelocity, finalQuality);
            }
        }

        private Vector3 CalculateHitVelocity(float quality)
        {
            float power = Mathf.Lerp(minHitPower, maxHitPower, quality) * Mathf.Lerp(0.85f, 1.2f, powerAbility);

            float side = Random.Range(-0.65f, 0.65f);
            float upward = Mathf.Lerp(0.08f, 0.62f, quality);

            if (quality < 0.25f)
            {
                upward = Random.Range(0.02f, 0.22f);
                side *= 1.7f;
            }

            Vector3 direction = new Vector3(side, upward, 1f).normalized;
            return direction * power;
        }
    }
}
