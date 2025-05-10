using Cinemachine;
using MeltDown.Modules.CoreGame.Scripts.Views;
using UnityEngine;

namespace MeltDown.Modules.SmoothCamera.Scripts
{
    public class SmoothCamera : MonoBehaviour
    {
        [Header("Required Component")]
        [SerializeField] private PolygonCollider2D mapCollider;
        
        private GameViewController _gameViewController;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineTransposer _transposer;
        private CinemachineConfiner2D _confiner;
        private CharacterController _characterController;
        private Vector2  _playerMoveInput;
        
        // Base camera size (In case change in editor)
        private const float BaseOrthoSize = 10f;
        private const float BaseZPos = -10f;
        private const float BaseZoomSpeed = 2.2f;
        private const float BaseZoomInSize = 0.8f;
        private const float DampingSpeed = 0.5f;
        private const float BonusDamping = 0.1f;

        private void Awake()
        {
           _gameViewController = GameViewController.Instance;
           _characterController = _gameViewController.Player;
           _playerMoveInput = _characterController.MoveInput;
           _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            transform.position.Set(0,0, BaseZPos);
            _virtualCamera.m_Lens.OrthographicSize = BaseOrthoSize;
            _virtualCamera.Follow = _gameViewController.Player.transform;
            _ReplaceWithTransposer();
            _SetVirtualCameraExtension();
        }

        private void _ReplaceWithTransposer()
        {
            // remove whatever Body you have
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (_transposer == null)
            {
                _virtualCamera.DestroyCinemachineComponent<CinemachineComponentBase>(CinemachineCore.Stage.Body);
                _transposer = _virtualCamera
                    .AddCinemachineComponent<CinemachineTransposer>();
            }
            _transposer.m_BindingMode = 
                CinemachineTransposer.BindingMode.LockToTarget;
            _transposer.m_FollowOffset = new Vector3(0, 0, BaseZPos);
            _transposer.m_XDamping = 0;
            _transposer.m_YDamping = 0;
        }

        private void LateUpdate()
        {
            _SetCameraZoom();
            _SetCameraDamping();
        }

        private void _SetCameraZoom()
        {
            var currentZoomInSize = _virtualCamera.m_Lens.OrthographicSize;
            var targetZoomInSize = BaseOrthoSize - (_characterController.MoveInput.magnitude * BaseZoomInSize);
            var weight = Time.deltaTime * BaseZoomSpeed;
            var targetOrthoSize = Mathf.Lerp(currentZoomInSize, targetZoomInSize, weight);
            _virtualCamera.m_Lens.OrthographicSize = targetOrthoSize;
        }

        private void _SetCameraDamping()
        {
            var currDampingX = _transposer.m_XDamping;
            var currDampingY = _transposer.m_YDamping;
            var targetDampingX = Mathf.Abs(_characterController.MoveInput.x) + BonusDamping;
            var targetDampingY = Mathf.Abs(_characterController.MoveInput.y) + BonusDamping;
            
            _transposer.m_XDamping = Mathf.Lerp(currDampingX, targetDampingX, Time.deltaTime * DampingSpeed);
            _transposer.m_YDamping = Mathf.Lerp(currDampingY, targetDampingY, Time.deltaTime * DampingSpeed);
        }
         private void _SetVirtualCameraExtension()
         {
             _confiner = gameObject.AddComponent<CinemachineConfiner2D>();
             _virtualCamera.AddExtension(_confiner);
             _confiner.m_BoundingShape2D = mapCollider;
         }

         public void DisconnectCamera()
         {
             _virtualCamera.Follow = null;
         }

         public void ConnectCamera()
         {
             _virtualCamera.Follow = _gameViewController.Player.transform;
             _playerMoveInput = _characterController.MoveInput;
         }

         public void SetMapBound(PolygonCollider2D mapBound)
         {
             mapCollider = mapBound;
             _confiner.m_BoundingShape2D = mapCollider;
         }
    }
}
