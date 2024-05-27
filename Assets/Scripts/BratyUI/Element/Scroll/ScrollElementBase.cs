using System;
using System.Collections.Generic;
using BratyUI.Element.Gesture;
using UnityEngine;
using UnityEngine.Pool;

namespace BratyUI.Element.Scroll
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class ScrollElementBase<TScrollItemModel, TScrollItemElement> : MonoBehaviour, IDragElement
        where TScrollItemModel : ScrollItemModelBase
        where TScrollItemElement : ScrollItemElementBase
    {
        [SerializeField] private TScrollItemElement _scrollItemElementPrefab;
        [SerializeField] private ScrollSettings _scrollSettings;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider;
        
        protected float ScrollPercentage;
        
        private readonly List<TScrollItemModel> _models = new();
        private ObjectPool<TScrollItemElement> _scrollItemElementPool;

        public bool IsScrollEnabled
        {
            get => _boxCollider.enabled;
            set => _boxCollider.enabled = value;
        }

        public abstract void HandleDrag(Vector2 delta);

        public abstract void HandleBeginDrag(Vector2 point);

        public abstract void HandleEndDrag(Vector2 point);

        private void Start()
        {
            InitScrollElement();
            BuildScrollElement();
        }

        protected virtual void InitScrollElement()
        {
            _scrollItemElementPool = new ObjectPool<TScrollItemElement>(
                CreateSetup,
                GetSetup,
                ReleaseSetup,
                DestroySetup,
                CollectionChecks,
                InitialCount,
                MaxCount);
        }

        protected virtual void BuildScrollElement()
        {
            
        }

        public void ScrollToIndex(int index, float duration)
        {
            
        }
        
        public void ScrollToIndexImmediately(int index)
        {
            
        }

        #region ModelOperations

        public void AddModel(TScrollItemModel scrollItemModel)
        {
            _models.Add(scrollItemModel);
            BuildScrollElement();
        }

        public void InsertModel(int index, TScrollItemModel scrollItemModel)
        {
            if (!IsModelIndexValid(index) && index != _models.Count)
            {
                Debug.LogError($"[${name}]:: index {index} is not valid, can't insert model");
                return;
            }

            _models.Insert(index, scrollItemModel);
            BuildScrollElement();
        }

        public void RemoveModelAtIndex(int index)
        {
            if (!IsModelIndexValid(index))
            {
                Debug.LogError($"[${name}]:: index {index} is not valid, can't remove model");
                return;
            }

            _models.RemoveAt(index);
            BuildScrollElement();
        }

        public bool RemoveModel(TScrollItemModel scrollItemModel)
        {
            bool isRemoved = _models.Remove(scrollItemModel);
            if (isRemoved)
            {
                BuildScrollElement();
            }

            return isRemoved;
        }

        private bool IsModelIndexValid(int index)
        {
            return index >= 0 && index < _models.Count;
        }

        #endregion
        
        #region PoolOverrides
        protected virtual TScrollItemElement CreateSetup() => Instantiate(_scrollItemElementPrefab);
        protected virtual void GetSetup(TScrollItemElement scrollItemElement) => scrollItemElement.gameObject.SetActive(true);
        protected virtual void ReleaseSetup(TScrollItemElement scrollItemElement) => scrollItemElement.gameObject.SetActive(false);
        protected virtual void DestroySetup(TScrollItemElement scrollItemElement) => Destroy(scrollItemElement);
        protected virtual bool CollectionChecks => false;
        protected virtual int InitialCount => 5;
        protected virtual int MaxCount => 20;
        #endregion

#if UNITY_EDITOR
        private void OnValidate()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDrawGizmosSelected()
        {
            _boxCollider.size = _scrollSettings.Size;
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _spriteRenderer.size = _scrollSettings.Size;
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
        public Vector2 Size = Vector2.one;
        public float Speed = 1f;
        public float Inertia = 0.15f;
        public float Space = 0f;
    }
}