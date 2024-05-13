using System;
using UnityEngine;

namespace BratyUI.Element
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ButtonElement : MonoBehaviour, IClickElement
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;

        public event Action OnButtonClick;

        public bool IsEnabled
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }
        
        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        public void HandleClickStart()
        {
            
        }

        public void HandleClickCancel()
        {
        }

        public void HandleClickComplete()
        {
            OnButtonClick?.Invoke();
            Debug.Log(213);
        }
    }
}