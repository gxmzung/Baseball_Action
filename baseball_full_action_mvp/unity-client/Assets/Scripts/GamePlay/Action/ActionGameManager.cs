using UnityEngine;
using BaseballGame.GamePlay.Defense;
using BaseballGame.GamePlay.Running;
using BaseballGame.GamePlay.Rules;
using BaseballGame.GamePlay.Camera;

namespace BaseballGame.GamePlay.Action
{
    public class ActionGameManager : MonoBehaviour
    {
        public BaseballGameState state = BaseballGameState.Ready;

        [Header("Core")]
        public PitcherController pitcher;
        public BatterController batter;
        public BallController ball;

        [Header("Systems")]
        public DefenseManager defenseManager;
        public RunnerManager runnerManager;
        public ScoreManager scoreManager;
        public FairFoulJudge fairFoulJudge;
        public CameraRigController cameraRig;

        [Header("Field")]
        public Transform firstBase;

        private float lastHitQuality;

        private void Start()
        {
            SetState(BaseballGameState.Ready);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) && state == BaseballGameState.Ready)
            {
                StartPitch();
            }
        }

        public void SetState(BaseballGameState nextState)
        {
            state = nextState;
            Debug.Log("[GameState] " + state);
        }

        public void StartPitch()
        {
            SetState(BaseballGameState.Pitching);
            if (cameraRig != null && pitcher != null) cameraRig.SetTarget(pitcher.transform);

            pitcher.Pitch();
            batter.EnableSwingWindow();

            SetState(BaseballGameState.BatterTiming);
        }

        public void OnSwingMiss()
        {
            if (scoreManager != null)
            {
                scoreManager.AddStrike();
            }

            EndPitchResult("스트라이크");
        }

        public void OnPitchPassedStrikeZone(bool isStrike)
        {
            if (state != BaseballGameState.BatterTiming) return;

            if (isStrike)
            {
                scoreManager.AddStrike();
                EndPitchResult("루킹 스트라이크");
            }
            else
            {
                scoreManager.AddBall();
                EndPitchResult("볼");
            }
        }

        public void OnBallHit(Vector3 hitVelocity, float hitQuality)
        {
            lastHitQuality = hitQuality;

            SetState(BaseballGameState.BallInPlay);
            ball.Hit(hitVelocity);

            if (cameraRig != null) cameraRig.SetTarget(ball.transform);

            Vector3 predicted = ball.PredictLandingPoint();

            BallFieldZone zone = fairFoulJudge != null
                ? fairFoulJudge.Judge(predicted)
                : BallFieldZone.Fair;

            if (zone == BallFieldZone.FoulLeft || zone == BallFieldZone.FoulRight)
            {
                scoreManager.AddFoul();
                EndPitchResult("파울");
                return;
            }

            if (zone == BallFieldZone.HomeRun)
            {
                if (runnerManager != null) runnerManager.ScoreHomeRun(scoreManager);
                scoreManager.ResetCount();
                EndPitchResult("홈런");
                return;
            }

            if (runnerManager != null)
            {
                runnerManager.OnBallHit();
            }

            SetState(BaseballGameState.Fielding);

            if (defenseManager != null)
            {
                defenseManager.ReactToBattedBall(ball, predicted);
            }
        }

        public void OnBallCaught(FielderController fielder)
        {
            Debug.Log("포구 성공: " + fielder.name);

            if (lastHitQuality > 0.45f)
            {
                // 잘 맞은 뜬공 포구
                scoreManager.AddOut();
                EndPitchResult("뜬공 아웃");
                return;
            }

            SetState(BaseballGameState.Throwing);

            if (defenseManager != null && firstBase != null)
            {
                defenseManager.ThrowToBase(fielder, firstBase, ball);
                Invoke(nameof(CheckForceOutAtFirst), 0.85f);
            }
        }

        public void OnFielderMissed(FielderController fielder)
        {
            Debug.Log("수비 실패: " + fielder.name);

            if (runnerManager != null)
            {
                runnerManager.AdvanceBatterByHitQuality(lastHitQuality);
            }

            scoreManager.ResetCount();
            EndPitchResult(lastHitQuality > 0.6f ? "안타/장타" : "내야안타 가능");
        }

        private void CheckForceOutAtFirst()
        {
            bool outAtFirst = runnerManager != null && runnerManager.IsBatterRunnerOutAtFirst(ball);

            if (outAtFirst)
            {
                scoreManager.AddOut();
                EndPitchResult("1루 포스아웃");
            }
            else
            {
                scoreManager.ResetCount();
                EndPitchResult("세이프");
            }
        }

        private void EndPitchResult(string result)
        {
            Debug.Log("결과: " + result);

            if (scoreManager != null)
            {
                Debug.Log(scoreManager.GetCountText());
            }

            SetState(BaseballGameState.PlayResult);
            Invoke(nameof(ResetPlay), 1.4f);
        }

        public void ResetPlay()
        {
            if (ball != null) ball.ResetBall();
            if (defenseManager != null) defenseManager.ResetFielders();

            SetState(BaseballGameState.Ready);
        }
    }
}
