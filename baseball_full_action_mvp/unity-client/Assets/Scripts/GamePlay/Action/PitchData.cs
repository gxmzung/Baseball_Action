using UnityEngine;

namespace BaseballGame.GamePlay.Action
{
    [System.Serializable]
    public class PitchData
    {
        public PitchType type;
        public float speed;
        public float controlError;
        public Vector3 breakVector;

        public PitchData(PitchType type, float speed, float controlError, Vector3 breakVector)
        {
            this.type = type;
            this.speed = speed;
            this.controlError = controlError;
            this.breakVector = breakVector;
        }
    }
}
