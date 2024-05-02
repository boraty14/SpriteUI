using UnityEngine;

namespace BratyUI
{
    public class RootNode : MonoBehaviour
    {
        [SerializeField] private NodeCamera _nodeCamera;

        public NodeCamera NodeCamera => _nodeCamera;
    }
}