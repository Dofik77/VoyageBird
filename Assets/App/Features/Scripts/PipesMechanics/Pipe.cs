using System;
using System.Collections;
using UnityEngine;

namespace App.Fetures.Scripts.PipesMechanics
{
    public class Pipe : MonoBehaviour
    {
        public event Action<Pipe> Out;
        
        private Vector2 _leftCameraBorder;
        private float _pipeWidth;
        private float _speed;

        private Coroutine _moveCoroutine;

        private void Awake()
        {
            _leftCameraBorder = Camera.main.ViewportToWorldPoint(Vector2.zero);
            _pipeWidth = transform.localScale.x;
        }

        private void Update()
        {
            Move();
            
            if(IsOut())
                Out?.Invoke(this);
        }
        
        public void Initialize(float speed)
        {
            _speed = speed;
        }
        
        public void StartMove()
        {
            _moveCoroutine = StartCoroutine(Move());
        }

        public void StopMove()
        {
            StopCoroutine(_moveCoroutine);
        }

        private IEnumerator Move()
        {
            var wait = new WaitForEndOfFrame();
            
            while (true)
            {
                yield return wait;
                
                var direction = Vector2.left;
                var velocity = direction * (_speed * Time.deltaTime);
            
                transform.Translate(velocity);
            }
        }

        private bool IsOut()
        {
            return transform.position.x < _leftCameraBorder.x - _pipeWidth;
        }
    }
}