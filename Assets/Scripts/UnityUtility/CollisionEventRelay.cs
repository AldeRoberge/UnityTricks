using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Utils.Collision
{
    /// <summary>
    /// Relay collision events to another object or function.
    /// <example>
    /// For example:
    /// <code>
    ///     CollisionListener c = obj.AddComponent<CollisionListener>();
    ///     c.OnTriggerEnterEvent.AddListener((other) => {
    ///        Debug.Log("Obj collided with other!");
    ///     });
    /// </code>
    /// </example>
    ///</summary>
    public class CollisionEventRelay : MonoBehaviour
    {
        public readonly UnityEvent<Collider> OnTriggerEnterEvent = new UnityEvent<Collider>();
        public readonly UnityEvent<Collider> OnTriggerExitEvent = new UnityEvent<Collider>();

        public readonly UnityEvent<Collider> OnCollisionEnterEvent = new UnityEvent<Collider>();
        public readonly UnityEvent<Collider> OnCollisionExitEvent = new UnityEvent<Collider>();

        void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }

        void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent?.Invoke(other);
        }

        void OnCollisionEnter(UnityEngine.Collision other)
        {
            OnCollisionEnterEvent?.Invoke(other.collider);
        }

        void OnCollisionExit(UnityEngine.Collision other)
        {
            OnCollisionExitEvent?.Invoke(other.collider);
        }
    }
}