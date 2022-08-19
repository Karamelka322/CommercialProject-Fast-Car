using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Logic.Car
{
    [ExecuteInEditMode]
    public class StaticObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] 
        private T _prefab;
        
        [Space, SerializeField, OnValueChanged(nameof(OnPoolSizeChanged))] 
        private int _poolSize;
        
        [Space, SerializeField, ReadOnly]
        private List<T> _poolObjects;
        
        private void Awake()
        {
            foreach (T trail in _poolObjects) 
                trail.gameObject.hideFlags = HideFlags.HideInHierarchy;
        }
        
        protected bool TakeDisableObject<TWrapper>(out TWrapper wrapper) where TWrapper : MonoBehaviour, IWrapper
        {
            foreach (TWrapper obj in _poolObjects as List<TWrapper>)
            {
                if (obj.Enabled == false)
                {
                    wrapper = obj;
                    return true;
                }
            }

            wrapper = null;
            return false;
        }

#if UNITY_EDITOR

        private void OnPoolSizeChanged()
        {
            ClearPool();
            FillPool();
        }

        private void FillPool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject obj = (GameObject) PrefabUtility.InstantiatePrefab(_prefab.gameObject, transform);
                T type = obj.GetComponent<T>();
                type.gameObject.hideFlags = HideFlags.HideInHierarchy;
                _poolObjects.Add(type);
            }
        }

        private void ClearPool()
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                if(_poolObjects[i] != null)
                    DestroyImmediate(_poolObjects[i].gameObject, true);
            }

            _poolObjects.Clear();
        }

#endif
    }
}