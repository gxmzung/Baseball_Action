using UnityEngine;

namespace BaseballGame.GamePlay.Running
{
    public class BaseController : MonoBehaviour
    {
        public string baseName;
        public bool occupied;
        public RunnerController currentRunner;

        public void Clear()
        {
            occupied = false;
            currentRunner = null;
        }
    }
}
