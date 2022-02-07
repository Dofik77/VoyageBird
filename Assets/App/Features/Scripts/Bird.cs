using System;
using UnityEngine;

namespace App.Features.Scripts
{
    public class Bird : MonoBehaviour
    {
        public event Action<Bird> Died;
        public event Action PipePassed;
        
        [SerializeField] private float _jumpForce;
        [SerializeField] private Rigidbody2D _rigidbody;

        private bool _isBlockingJump;
        private bool _canJump;

        private void Update()
        {
            _canJump = CanJump();
            transform.up = _rigidbody.velocity;
        }

        private void LateUpdate()
        {
            if (_canJump)
            {
                Jump();
            }
        }

        public void BlockJump()
        {
            _isBlockingJump = true;
        }

        private bool CanJump()
        {
            return _isBlockingJump == false 
                   && Input.GetMouseButtonDown(0);
        }

        private void Jump()
        {
            _rigidbody.velocity = _jumpForce * Vector2.up;
        } 

        private void OnCollisionEnter2D()
        {
            Died?.Invoke(this);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.TryGetComponent(out Point _))
                PipePassed?.Invoke();
        }

    }
}