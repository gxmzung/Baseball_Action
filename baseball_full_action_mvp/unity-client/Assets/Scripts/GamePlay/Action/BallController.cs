using UnityEngine;

namespace BaseballGame.GamePlay.Action
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour
    {
        public Rigidbody rb;
        public Transform resetPoint;

        public bool isPitched;
        public bool isInPlay;
        public bool isCaught;

        private Vector3 lastStablePosition;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (rb != null && rb.linearVelocity.magnitude > 0.2f)
            {
                lastStablePosition = transform.position;
            }
        }

        public void Throw(Vector3 velocity)
        {
            isPitched = true;
            isInPlay = false;
            isCaught = false;

            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(velocity, ForceMode.Impulse);
        }

        public void AddBreak(Vector3 breakForce)
        {
            rb.AddForce(breakForce, ForceMode.Force);
        }

        public void Hit(Vector3 velocity)
        {
            isPitched = false;
            isInPlay = true;
            isCaught = false;

            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(velocity, ForceMode.Impulse);
        }

        public void Catch(Transform glovePoint)
        {
            isCaught = true;
            isInPlay = false;
            isPitched = false;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            Transform parent = glovePoint != null ? glovePoint : null;
            if (parent != null)
            {
                transform.position = parent.position;
                transform.SetParent(parent);
            }
        }

        public void ResetBall()
        {
            transform.SetParent(null);

            if (resetPoint != null)
            {
                transform.position = resetPoint.position;
            }

            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            isPitched = false;
            isInPlay = false;
            isCaught = false;
        }

        public Vector3 PredictLandingPoint(float maxTime = 5.0f)
        {
            Vector3 position = transform.position;
            Vector3 velocity = rb.linearVelocity;
            Vector3 gravity = Physics.gravity;

            float step = 0.05f;
            for (float t = 0; t < maxTime; t += step)
            {
                Vector3 next = position + velocity * step + 0.5f * gravity * step * step;
                velocity += gravity * step;

                if (next.y <= 0.15f && t > 0.2f)
                {
                    return new Vector3(next.x, 0, next.z);
                }

                position = next;
            }

            return new Vector3(position.x, 0, position.z);
        }

        public Vector3 GetCurrentGroundPoint()
        {
            return new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
