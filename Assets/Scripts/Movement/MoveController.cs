﻿using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;
using System;

namespace Assets.Scripts.Movement
{
    public class MoveController : MonoBehaviour
    {
        public Transform FpsCamera;
        public float Speed;
        public float JumpForce;
        public float GroundCheckDistance = 0.1f;
        public MouseLook MouseLook = new MouseLook();
        private bool _canMove = true;
		private bool _canJump = true;

        private Vector3 _movementDirection;
        private Rigidbody _rigidbody;
        private bool _isGrounded;
        private GravityController _gravityController;
        private CapsuleCollider _capsuleCollider;
        private Vector3 _jumpForce;
        private bool _jump;
        private bool _isJumping;
        private bool _previouslyGrounde;
        private Animator _animator;

        [SerializeField] private AudioClip[] _footstepSounds;
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _landSound;

        private readonly int _aniOnGround = Animator.StringToHash("OnGround");
        private readonly int _aniTurn = Animator.StringToHash("Turn");
        private readonly int _aniForward = Animator.StringToHash("Forward");
        private readonly int _aniJump = Animator.StringToHash("Jump");
        private readonly int _aniJumpLeg = Animator.StringToHash("JumpLeg");

        #region Properties

        public Vector3 Velocity
        {
            get { return _rigidbody.velocity; }
        }

        public bool Grounded
        {
            get { return _isGrounded; }
        }

        public bool IsRunning
        {
            get { return false; }
        }

        public bool CanMove
        {
            get { return _canMove; }
            set
            {
                _canMove = value;
                if (!value)
                {
                    _rigidbody.velocity = Vector3.zero;
                }
            }
        }

		public bool CanJump 
		{
			get { return _canJump; }
			set 
			{
				_canJump = value;
			}
		}

        public Vector3 FootPosition
        {
            get { return transform.Find("FootPosition").transform.position; }
        }

        #endregion


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _gravityController = GetComponent<GravityController>();
            _audioSource = GetComponent<AudioSource>();
            MouseLook.Init(transform, FpsCamera);
            _nextStep = _stepCycle / 2f;
            //_animator = gameObject.GetComponentInChildren<Animator>();

            CommandConsole.Console.Instance.OnActivate += ConsoleOnOnActivate;
            CommandConsole.Console.Instance.OnDeActivate += ConsoleOnOnDeActivate;
        }

        private void ConsoleOnOnDeActivate(object sender, EventArgs eventArgs)
        {
            MouseLook.SetCursorLock(true);
            CanMove = true;
        }

        private void ConsoleOnOnActivate(object o, EventArgs eventArgs)
        {
            MouseLook.SetCursorLock(false);
            CanMove = false;
        }

        private void OnDestroy()
        {
            Debug.Log("MovementController Destroyed");
            CommandConsole.Console.Instance.OnActivate -= ConsoleOnOnActivate;
            CommandConsole.Console.Instance.OnDeActivate -= ConsoleOnOnDeActivate;
        }

        private void Update()
        {
            if (CanMove)
            {
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxisRaw("Vertical");

                var movementVector = FpsCamera.TransformDirection(new Vector3(x, 0f, y));
                movementVector = Vector3.ProjectOnPlane(movementVector, transform.up);
                _movementDirection = movementVector.normalized * Speed;
                ProgressStepCycle(Speed, x, y);

                //_animator.SetFloat(_aniForward, y);
                //_animator.SetFloat(_aniTurn, x);
            }

			if (CanJump && _isGrounded && Input.GetButtonDown("Jump"))
            {
                _jump = true;
            }

            RotateView();
        }

        private void FixedUpdate()
        {
            if (_jump && _isGrounded)
            {
                _rigidbody.drag = 0f;
                //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
                var jumpVector = transform.TransformDirection(new Vector3(0f, JumpForce, 0f));
                _rigidbody.AddForce(jumpVector, ForceMode.Impulse);

                //_animator.SetFloat(_aniJump, jumpVector.magnitude);

                _jumpForce = Vector3.zero;
                _jump = false;
                _isJumping = true;
                _audioSource.clip = _jumpSound;
                _audioSource.Play();
            }
            else
            {
                CheckIsGrounded();
                if (!_isJumping && _isGrounded && CanMove)
                {
                    _rigidbody.velocity = _movementDirection;
                }
            }
        }

        private void CheckIsGrounded()
        {
            _previouslyGrounde = _isGrounded;
            var ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));
            if (Physics.SphereCast(ray, _capsuleCollider.radius, ((_capsuleCollider.height/2f) - _capsuleCollider.radius) + GroundCheckDistance,
                Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                _isGrounded = true;
                //_animator.SetBool(_aniOnGround, true);
            }
            else
            {
                _isGrounded = false;
                //_animator.SetBool(_aniOnGround, false);
            }

            if (!_previouslyGrounde && _isJumping && _isGrounded)
            {
                _isJumping = false;
                PlayLandingSound();
            }
        }

        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed

            if (_gravityController.UsesGravityManipultation)
            {
                var newRotation = Quaternion.FromToRotation(transform.up, _gravityController.Normal)*transform.rotation;
                newRotation = Quaternion.Slerp(transform.localRotation, newRotation, 5 * Time.deltaTime);
                transform.rotation = newRotation;
            }


            var mouseRotation = MouseLook.LookRotation(transform, FpsCamera);
            transform.localRotation *= mouseRotation;

            float oldYRotation = transform.eulerAngles.y;

            if (_isGrounded)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                _rigidbody.velocity = velRotation * _rigidbody.velocity;
            }
        }

        private float _stepCycle;
        private float _nextStep;
        [SerializeField] private float _stepInterval;
        private AudioSource _audioSource;

        private void ProgressStepCycle(float speed, float x, float y)
        {
            if (_rigidbody.velocity.sqrMagnitude > 0 && (x != 0 || y != 0))
            {
                //_stepCycle += (_rigidbody.velocity.magnitude + (speed)) * Time.fixedDeltaTime;
                _stepCycle += _rigidbody.velocity.sqrMagnitude * speed * Time.deltaTime;
            }

            if (!(_stepCycle > _nextStep))
            {
                return;
            }

            _nextStep = _stepCycle + _stepInterval;

            PlayFootStepAudio();
        }

        private void PlayFootStepAudio()
        {
            if (!_isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, _footstepSounds.Length);
            _audioSource.clip = _footstepSounds[n];
            _audioSource.PlayOneShot(_audioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            _footstepSounds[n] = _footstepSounds[0];
            _footstepSounds[0] = _audioSource.clip;
        }

        private void PlayLandingSound()
        {
            _audioSource.clip = _landSound;
            _audioSource.Play();
            _nextStep = _stepCycle + .5f;
        }
    }
}