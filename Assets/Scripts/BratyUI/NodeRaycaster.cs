using UnityEngine;
using UnityEngine.InputSystem;

namespace BratyUI
{
    public class NodeRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        private readonly RaycastHit2D[] _results = new RaycastHit2D[10];

        private void Update()
        {
            if (!Touchscreen.current.primaryTouch.press.isPressed)
            {
                return;
            }
            Vector2 mousePosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            int hitCount = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, _results, Mathf.Infinity, _layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                Debug.Log($"Hit: {_results[i].collider.gameObject.name}");
            }
        }
    }
}