using Cinemachine;
using MeltDown.Modules.CoreGame.Scripts.Views;
using UnityEngine;

namespace MeltDown.Modules.SmoothCamera.Scripts
{
    public class SmoothCamera : MonoBehaviour
    {
        private GameViewController _gameViewController;
        private CinemachineVirtualCamera _virtualCamera;
        private CharacterController _characterController;
        
        // Base camera size (In case change in editor)
        private readonly float _baseOrthoSize = 10f;
        private readonly float _baseZpos = -10f;

        private void Awake()
        {
           this._gameViewController = GameViewController.Instance;
           this._characterController = this._gameViewController.Player;
           this._virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            this.transform.position.Set(0,0, this._baseZpos);
            this._virtualCamera.m_Lens.OrthographicSize = _baseOrthoSize;
            this._virtualCamera.Follow = this._gameViewController.Player.transform;
            this._ReplaceWithTransposer();
        }

        private void _ReplaceWithTransposer()
        {
            // remove whatever Body you have
            _virtualCamera.DestroyCinemachineComponent<CinemachineComponentBase>(CinemachineCore.Stage.Body);
            // add a fresh Transposer
            var transposer = _virtualCamera
                .AddCinemachineComponent<CinemachineTransposer>();
            transposer.m_BindingMode = 
                CinemachineTransposer.BindingMode.LockToTarget;
            transposer.m_FollowOffset = new Vector3(0, 0, _baseZpos);
        }
    }
}
