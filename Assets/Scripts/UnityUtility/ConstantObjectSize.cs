using UnityEngine;

namespace UnityUtility
{
    /// <summary>
    /// Scales the object so it always looks the same size from any distance away from the camera.
    /// </summary>
    public class ConstantObjectSize : MonoBehaviour
    {
        private Camera activeCam;
        public float targetSize = 20;

        private void Start()
        {
            activeCam = Camera.main;
        }

        public void SetActiveCamera(Camera cam)
        {
            activeCam = cam;
        }

        public void FixedUpdate()
        {
            Vector3 textScreenSpace = activeCam.WorldToScreenPoint(transform.position);
            Vector3 adjustedScreenSpace = new Vector3(textScreenSpace.x + targetSize, textScreenSpace.y, textScreenSpace.z);
            Vector3 adjustedWorldSpace = activeCam.ScreenToWorldPoint(adjustedScreenSpace);
            transform.localScale = Vector3.one * (transform.position - adjustedWorldSpace).magnitude;
        }
    }
}