using System;

namespace BaseballGame.Models
{
    [Serializable]
    public class Player
    {
        public string id;
        public string name;
        public string position;

        public int age;
        public int overall;

        public int contact;
        public int power;
        public int speed;
        public int defense;
        public int throwing;

        public int pitchingControl;
        public int pitchingVelocity;
        public int pitchingMovement;

        public int marketValue;

        public bool IsPitcher()
        {
            return position == "SP" || position == "RP" || position == "CP";
        }

        public float Contact01()
        {
            return Clamp01(contact / 100f);
        }

        public float Power01()
        {
            return Clamp01(power / 100f);
        }

        public float Defense01()
        {
            return Clamp01(defense / 100f);
        }

        private float Clamp01(float v)
        {
            if (v < 0f) return 0f;
            if (v > 1f) return 1f;
            return v;
        }
    }
}
