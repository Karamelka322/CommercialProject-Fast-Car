using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.Tween;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Car
{
    [ExecuteInEditMode]
    public class WheelTrail : MonoBehaviour
    {
        [SerializeField] 
        private TrailRenderer _prefab;
        
        [Space, SerializeField, OnValueChanged(nameof(OnPoolSizeChanged))] 
        private int _poolSize;
        
        [Space, SerializeField, ReadOnly]
        private List<TrailRenderer> _poolTrails;

        private ITweenService _tweenService;
        private TrailRenderer _currentTrail;

        [Inject]
        private void Construct(ITweenService tweenService)
        {
            _tweenService = tweenService;
        }

        private void Awake()
        {
            foreach (TrailRenderer trail in _poolTrails) 
                trail.gameObject.hideFlags = HideFlags.HideInHierarchy;

            _prefab.forceRenderingOff = false;
        }
        
        public void StartDrawing()
        {
            _currentTrail = TakeFreeTrail();
                    
            if(_currentTrail == null)
                return;
                    
            _currentTrail.enabled = true;
        }

        public void StopDrawing()
        {
            if(_currentTrail == null)
                return;
                    
            _currentTrail.transform.parent = null;
                    
            _tweenService.Timer(_currentTrail.time, _currentTrail, component =>
            {
                component.transform.parent = transform;
                component.transform.localPosition = Vector3.zero;
                component.enabled = false;
            });

            _currentTrail = null;
        }

        private TrailRenderer TakeFreeTrail() => 
            _poolTrails.FirstOrDefault(t => t.enabled == false);


#if UNITY_EDITOR

        private void OnPoolSizeChanged()
        {
            DestroyTrails();
            CreateTrails();
        }

        private void CreateTrails()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject obj = (GameObject) PrefabUtility.InstantiatePrefab(_prefab.gameObject, transform);
                TrailRenderer trailRenderer = obj.GetComponent<TrailRenderer>();
                trailRenderer.enabled = false;
                trailRenderer.gameObject.hideFlags = HideFlags.HideInHierarchy;
                _poolTrails.Add(trailRenderer);
            }
        }

        private void DestroyTrails()
        {
            for (int i = 0; i < _poolTrails.Count; i++)
                DestroyImmediate(_poolTrails[i].gameObject, true);

            _poolTrails.Clear();
        }

#endif
        
    }
}