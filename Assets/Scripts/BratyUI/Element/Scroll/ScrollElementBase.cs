using System;
using System.Collections.Generic;
using BratyUI.Element.Gesture;
using UnityEngine;

namespace BratyUI.Element.Scroll
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class ScrollElementBase<TScrollItemElement, TScrollItemModel> : MonoBehaviour, IDragElement
        where TScrollItemElement : ScrollItemElementBase
        where TScrollItemModel : ScrollItemModelBase
    {
        [SerializeField] private TScrollItemElement _scrollItemElement;
        [SerializeField] private ScrollSettings _settings;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] protected Transform View;

        private readonly List<TScrollItemModel> _models = new();
        private float _scrollPercentage;

        private float ScrollSize => _collider.size.y;

        public bool IsScrollEnabled
        {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        public abstract void HandleDrag(Vector2 delta);

        public abstract void HandleBeginDrag(Vector2 point);

        public abstract void HandleEndDrag(Vector2 point);

        private void Start()
        {
            InitializeScrollElement();
        }

        protected virtual void InitializeScrollElement()
        {
            
        }

        public void ScrollToIndex(int index)
        {
            
        }

        #region ModelOperations

        public void AddModel(TScrollItemModel scrollItemModel)
        {
            _models.Add(scrollItemModel);
        }

        public void InsertModel(int index, TScrollItemModel scrollItemModel)
        {
            if (!IsModelIndexValid(index) && index != _models.Count)
            {
                Debug.LogError($"[${name}]:: index {index} is not valid, can't insert model");
                return;
            }

            _models.Insert(index, scrollItemModel);
        }

        public void RemoveModelAtIndex(int index)
        {
            if (!IsModelIndexValid(index))
            {
                Debug.LogError($"[${name}]:: index {index} is not valid, can't remove model");
                return;
            }

            _models.RemoveAt(index);
        }

        public bool RemoveModel(TScrollItemModel scrollItemModel)
        {
            return _models.Remove(scrollItemModel);
        }

        private bool IsModelIndexValid(int index)
        {
            return index >= 0 && index < _models.Count;
        }

        #endregion

#if UNITY_EDITOR
        private void OnValidate()
        {
            _collider = GetComponent<BoxCollider2D>();
        }
#endif
    }

    [Serializable]
    public class ScrollSettings
    {
        [Header("Margin")]
        public float StartMargin;
        public float EndMargin;

        [Header("Scroll")]
        public bool IsReversed;
        public float Speed = 1f;
        public float Inertia = 0.15f;
        public float Space = 0f;
    }
}