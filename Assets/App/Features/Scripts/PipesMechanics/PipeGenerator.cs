using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Features.Scripts.PipesMechanics
{
    
    public class  PipeGenerator : MonoBehaviour
    {
        public event Action<Pipe> Spawned;
        
        [SerializeField] private Pipe _pipeObject;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _pipeOffsetRangeY;
        [SerializeField] private float _pipeOffset;

        private PipePool _pipePool;
        private Vector2 _rightCameraBorder;
        private Coroutine _spawnCoroutine;
        private float _pipeWidth;

        private void Awake()
        {
            _rightCameraBorder = Camera.main.ViewportToWorldPoint(Vector2.one);
            _pipeWidth = transform.localScale.x;
            _pipePool = new PipePool(_pipeObject);
        }

        public void StartSpawning()
        {
            _spawnCoroutine = StartCoroutine(Spawning());
        }
        
        public void StopSpawning()
        {
            StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator Spawning()
        {
            // spawnRate - кол-во труб/сек -> 1/spawnRate
            var spawnDelay = 1f/_spawnRate; 
            var spawnWait = new WaitForSeconds(spawnDelay);
            
            while (true)
            {
                yield return spawnWait;

                var pipe = _pipePool.GetPooledObject();
                PlacePipe(pipe);
                Spawned?.Invoke(pipe);
            }
        }

        private void PlacePipe(Pipe pipe)
        {
            var randomY = Random.Range(-_pipeOffsetRangeY, _pipeOffsetRangeY);
            var newPosition = new Vector3(_rightCameraBorder.x + _pipeWidth, randomY - _pipeOffset);
            
            pipe.transform.position = newPosition;
            
            pipe.Out += OnPipeOut;
        }

        private void OnPipeOut(Pipe pipe)
        {
            pipe.Out -= OnPipeOut;
            _pipePool.ReturnObjectToPool(pipe);
        }
        
    }
}