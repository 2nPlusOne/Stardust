using UnityEngine;

namespace Spotnose.Stardust
{
    public class GravitySource : MonoBehaviour
    {
        private void OnEnable() => CustomGravity.AddGravitySource(this);
        private void OnDisable() => CustomGravity.RemoveGravitySource(this);

        public virtual Vector3 GetGravity(Vector3 position)
        {
            return Physics.gravity;
        }
    }
}
