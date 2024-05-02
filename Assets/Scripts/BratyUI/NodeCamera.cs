using UnityEngine;

namespace BratyUI
{
    public class NodeCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public float Aspect => _camera.aspect;
        public float Size => _camera.orthographicSize;
        
    }
}