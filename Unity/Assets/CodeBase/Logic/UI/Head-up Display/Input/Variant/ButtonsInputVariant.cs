using CodeBase.Services.Input.Element;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Input
{
    public class ButtonsInputVariant : MonoBehaviour, IInputVariant
    {
        [SerializeField] 
        private ButtonInputElement _upLeftButton;
        
        [SerializeField] 
        private ButtonInputElement _downLeftButton;
        
        [SerializeField] 
        private ButtonInputElement _upRightButton;
        
        [SerializeField] 
        private ButtonInputElement _downRightButton;
        
        [Space, SerializeField] 
        private ButtonInputElement _driftLeftButton;
        
        [SerializeField] 
        private ButtonInputElement _driftRightButton;

        private static readonly Vector2 UpLeft = new Vector2(1, -1);
        private static readonly Vector2 DownLeft = new Vector2(-2, -1);
        private static readonly Vector2 UpRight = new Vector2(1, 1);
        private static readonly Vector2 DownRight = new Vector2(-2, 1);

        private static readonly Vector2 _default = new Vector2(1, 0);
        
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        private Vector2 _axis;
        public Vector2 Axis => Drift == false ? MovementAxis() : DriftAxis();

        public bool Drift { get; private set; }

        private IUpdateService _updateService;

        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void OnEnable()
        {
            
#if UNITY_EDITOR
            _updateService.OnFixedUpdate += OnFixedUpdate;
#endif

            _driftLeftButton.Enabled += OnEnabledDriftButton;
            _driftRightButton.Enabled += OnEnabledDriftButton;
            _driftLeftButton.Disabled += OnDisabledDriftButton;
            _driftRightButton.Disabled += OnDisabledDriftButton;
        }

        private void OnDisable()
        {
            
#if UNITY_EDITOR
            _updateService.OnFixedUpdate -= OnFixedUpdate;
#endif
            
            _driftLeftButton.Enabled -= OnEnabledDriftButton;
            _driftRightButton.Enabled -= OnEnabledDriftButton;
            _driftLeftButton.Disabled -= OnDisabledDriftButton;
            _driftRightButton.Disabled -= OnDisabledDriftButton;
        }

        public void EnableMoveBackwardsButton()
        {
            _driftLeftButton.gameObject.SetActive(false);
            _driftRightButton.gameObject.SetActive(false);
            
            _downLeftButton.gameObject.SetActive(true);
            _downRightButton.gameObject.SetActive(true);
        }

        public (ButtonInputElement, ButtonInputElement) GetBackwardsButton() => 
            (_downLeftButton, _downRightButton);

        public void DisableMoveBackwardsButton()
        {
            _driftLeftButton.gameObject.SetActive(true);
            _driftRightButton.gameObject.SetActive(true);
            
            _downLeftButton.gameObject.SetActive(false);
            _downRightButton.gameObject.SetActive(false);
        }

        private Vector2 MovementAxis()
        {   
            _axis = _default;

            _axis += _upLeftButton.Click ? UpLeft : Vector2.zero;
            _axis += _downLeftButton.Click ? DownLeft : Vector2.zero;
            _axis += _upRightButton.Click ? UpRight : Vector2.zero;
            _axis += _downRightButton.Click ? DownRight : Vector2.zero;
            
#if UNITY_EDITOR
            
            _axis.x += UnityEngine.Input.GetAxis(Vertical);
            _axis.y += UnityEngine.Input.GetAxis(Horizontal);
            
#endif

            _axis.x = Mathf.Clamp(_axis.x, -1, 1);
            _axis.y = Mathf.Clamp(_axis.y, -1, 1);
            
            return _axis;
        }

        private Vector2 DriftAxis()
        {
            _axis = _default;
            
            _axis += _driftLeftButton.Click ? UpLeft : Vector2.zero;
            _axis += _driftRightButton.Click ? UpRight : Vector2.zero;
            
#if UNITY_EDITOR
            
            _axis.x += UnityEngine.Input.GetAxis(Vertical);
            _axis.y += UnityEngine.Input.GetAxis(Horizontal);
            
#endif

            _axis.x = Mathf.Clamp(_axis.x, -1, 1);
            _axis.y = Mathf.Clamp(_axis.y, -1, 1);
            
            return _axis;
        }

        private void OnEnabledDriftButton() => 
            Drift = true;

        private void OnDisabledDriftButton() => 
            Drift = false;

#if UNITY_EDITOR

        private void OnFixedUpdate()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                Drift = true;
            
            if (UnityEngine.Input.GetKeyUp(KeyCode.Space))
                Drift = false;
        }

#endif
    }
}