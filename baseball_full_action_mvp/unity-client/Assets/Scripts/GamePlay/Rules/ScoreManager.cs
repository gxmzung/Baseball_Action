using UnityEngine;

namespace BaseballGame.GamePlay.Rules
{
    public class ScoreManager : MonoBehaviour
    {
        public int inning = 1;
        public bool top = true;

        public int homeScore;
        public int awayScore;

        public int balls;
        public int strikes;
        public int outs;

        public void AddBall()
        {
            balls++;
            if (balls >= 4)
            {
                Debug.Log("볼넷");
                ResetCount();
            }
        }

        public void AddStrike()
        {
            strikes++;
            if (strikes >= 3)
            {
                Debug.Log("삼진");
                AddOut();
                ResetCount();
            }
        }

        public void AddFoul()
        {
            if (strikes < 2)
            {
                strikes++;
            }
        }

        public void AddOut()
        {
            outs++;
            if (outs >= 3)
            {
                ChangeHalfInning();
            }
        }

        public void AddRun(int count)
        {
            if (top)
            {
                awayScore += count;
            }
            else
            {
                homeScore += count;
            }
        }

        public void ResetCount()
        {
            balls = 0;
            strikes = 0;
        }

        public void ChangeHalfInning()
        {
            outs = 0;
            ResetCount();

            if (top)
            {
                top = false;
            }
            else
            {
                top = true;
                inning++;
            }

            Debug.Log("공수교대: " + inning + "회 " + (top ? "초" : "말"));
        }

        public string GetCountText()
        {
            return $"{inning}회 {(top ? "초" : "말")} / B:{balls} S:{strikes} O:{outs} / Away {awayScore} : Home {homeScore}";
        }
    }
}
