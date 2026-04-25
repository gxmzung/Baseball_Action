using UnityEngine;

namespace BaseballGame.GamePlay.Action
{
    public class PitcherController : MonoBehaviour
    {
        public BallController ball;
        public Transform releasePoint;
        public Transform strikeZoneTarget;

        public PitchType selectedPitch = PitchType.Fastball;

        private PitchData currentPitch;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) selectedPitch = PitchType.Fastball;
            if (Input.GetKeyDown(KeyCode.Alpha2)) selectedPitch = PitchType.Slider;
            if (Input.GetKeyDown(KeyCode.Alpha3)) selectedPitch = PitchType.Curveball;
            if (Input.GetKeyDown(KeyCode.Alpha4)) selectedPitch = PitchType.Changeup;
        }

        public void Pitch()
        {
            currentPitch = CreatePitchData(selectedPitch);

            if (ball == null || releasePoint == null || strikeZoneTarget == null)
            {
                Debug.LogWarning("PitcherController 연결이 부족합니다.");
                return;
            }

            ball.transform.SetParent(null);
            ball.transform.position = releasePoint.position;

            Vector3 target = strikeZoneTarget.position;
            target += new Vector3(
                Random.Range(-currentPitch.controlError, currentPitch.controlError),
                Random.Range(-currentPitch.controlError, currentPitch.controlError),
                0f
            );

            Vector3 direction = (target - releasePoint.position).normalized;
            ball.Throw(direction * currentPitch.speed);

            Debug.Log("투구: " + selectedPitch);
        }

        private void FixedUpdate()
        {
            if (ball != null && ball.isPitched && currentPitch != null)
            {
                ball.AddBreak(currentPitch.breakVector);
            }
        }

        private PitchData CreatePitchData(PitchType type)
        {
            switch (type)
            {
                case PitchType.Slider:
                    return new PitchData(type, 28f, 0.42f, new Vector3(8.0f, -0.5f, 0f));
                case PitchType.Curveball:
                    return new PitchData(type, 25f, 0.55f, new Vector3(0f, -7.5f, 0f));
                case PitchType.Changeup:
                    return new PitchData(type, 23f, 0.35f, new Vector3(-1.0f, -1.5f, 0f));
                default:
                    return new PitchData(type, 33f, 0.28f, Vector3.zero);
            }
        }
    }
}
