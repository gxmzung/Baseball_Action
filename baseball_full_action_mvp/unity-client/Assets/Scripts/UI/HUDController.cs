using UnityEngine;
using UnityEngine.UI;
using BaseballGame.GamePlay.Rules;
using BaseballGame.GamePlay.Action;

namespace BaseballGame.UI
{
    public class HUDController : MonoBehaviour
    {
        public ScoreManager scoreManager;
        public PitcherController pitcher;
        public Text countText;
        public Text pitchText;

        private void Update()
        {
            if (countText != null && scoreManager != null)
            {
                countText.text = scoreManager.GetCountText();
            }

            if (pitchText != null && pitcher != null)
            {
                pitchText.text = "Pitch: " + pitcher.selectedPitch.ToString() + "  [1]FB [2]SL [3]CB [4]CH";
            }
        }
    }
}
