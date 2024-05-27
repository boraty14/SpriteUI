using System;
using BratyUI.Element.Gesture;
using UnityEngine;

namespace BratyUI.Node
{
    [DisallowMultipleComponent]
    public class ButtonNode : ImageNode, IClickElement
    {
        [SerializeField] private ButtonSettings _settings;
        
        public event Action OnButtonClick;

        public bool IsEnabled
        {
            get => ImageCollider.enabled;
            set => ImageCollider.enabled = value;
        }
        public void HandleClickStart()
        {
            SetClickState();
        }

        public void HandleClickCancel()
        {
            SetNormalState();
        }

        public void HandleClickComplete()
        {
            SetNormalState();
            OnButtonClick?.Invoke();
            Debug.Log(name);
        }

        private void SetNormalState()
        {
            SetScale(_settings.NormalScale);
            SetAlpha(_settings.NormalAlpha);
        }

        private void SetClickState()
        {
            SetScale(_settings.ClickScale);
            SetAlpha(_settings.ClickAlpha);
        }

        public void SetScale(Vector2 scale)
        {
            transform.localScale = new Vector3(scale.x, scale.y, transform.localScale.z);
        }

        public void SetAlpha(float alpha)
        {
            var color = SpriteRenderer.color;
            color.a = alpha;
            SpriteRenderer.color = color;
        }
    }
    
    [Serializable]
    public class ButtonSettings
    {
        public Vector2 NormalScale = Vector2.one;
        public Vector2 ClickScale = Vector2.one * 0.8f;
        [Range(0f, 1f)] public float NormalAlpha = 1f;
        [Range(0f, 1f)] public float ClickAlpha = 1f;
    }
}