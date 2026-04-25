using UnityEngine;

namespace BaseballGame.GamePlay.Camera
{
    public class CameraRigController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, 7, -10);
        public float followSpeed = 5f;

        private void LateUpdate()
        {
            if (target == null) return;

            Vector3 desired = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);
            transform.LookAt(target);
        }

        public void SetTarget(Transform nextTarget)
        {
            target = nextTarget;
        }
    }
}
