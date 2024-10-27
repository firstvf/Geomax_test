using System;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 4f;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _invisibleCollider;
        [SerializeField] private Candle _candle;
        private CharacterController _characterController;
        private const string HAND_COVERED_ANIMATION = "IsCovered";

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            MovementInput();
            HandInput();
        }

        private void HandInput()
        {
            if (Input.GetKey(KeyCode.E))
            {
                _invisibleCollider.isTrigger = false;
                _animator.SetBool(HAND_COVERED_ANIMATION, true);
                _candle.CoverCandle(true);
            }
            else
            {
                _invisibleCollider.isTrigger = true;
                _animator.SetBool(HAND_COVERED_ANIMATION, false);
                _candle.CoverCandle(false);
            }

            if (Input.GetKeyUp(KeyCode.F))
                _candle.ActivateCandle();
        }

        private void MovementInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var moveDirection = transform.forward * vertical + transform.right * horizontal;

            _characterController.Move(_movementSpeed * Time.deltaTime * moveDirection);
        }
    }
}