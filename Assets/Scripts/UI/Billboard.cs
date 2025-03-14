using UnityEngine;

namespace UI
{
    public class Billboard : MonoBehaviour
    {
        private readonly Quaternion _originalRotation = Quaternion.identity;

        private void LateUpdate() => transform.rotation = _originalRotation;
    }
}