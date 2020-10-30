using UnityEngine;

namespace Utilities
{
    public class RotaterOnYAxis : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool startAtRandomYRotation = true;
        [SerializeField] private bool mayTurnInTheOppositeDirection = true;

        private void Start()
        {
            if (mayTurnInTheOppositeDirection)
            {
                if ((int) Random.Range(0, 2) == 1)
                {
                    speed *= -1;
                }
            }

            if (!startAtRandomYRotation)
                return;

            var randomEuler = new Vector3(transform.rotation.eulerAngles.x, Random.Range(0, 355), transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(randomEuler);
        }

        private void Update()
        {
            var newVector = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + speed * Time.deltaTime, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(newVector.x, newVector.y, newVector.z);
        }
    }
}