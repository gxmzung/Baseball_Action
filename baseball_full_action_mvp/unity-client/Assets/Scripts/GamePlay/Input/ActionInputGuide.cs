using UnityEngine;

namespace BaseballGame.GamePlay.Input
{
    public class ActionInputGuide : MonoBehaviour
    {
        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 650, 30), "P: 투구 / Space: 스윙 / 1: 직구 / 2: 슬라이더 / 3: 커브 / 4: 체인지업");
        }
    }
}
