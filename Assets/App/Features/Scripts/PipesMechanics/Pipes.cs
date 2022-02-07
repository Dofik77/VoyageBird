using System.Collections.Generic;
using UnityEngine;

namespace App.Features.Scripts.PipesMechanics
{
    public class Pipes : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private PipeGenerator _generator;

        private Queue<Pipe> _pipes = new Queue<Pipe>();

        private void OnEnable()
        {
            _generator.Spawned += OnPipeSpawned;
        }

        private void OnDisable()
        {
            _generator.Spawned -= OnPipeSpawned;
        }

        public void StartMove()
        {
            _generator.StartSpawning();   
        }
        
        public void StopMove()
        {
            _generator.StopSpawning();
            StopPipes();
        }

        private void StopPipes()
        {
            foreach (var pipe in _pipes)
                pipe.StopMove();
        }

        private void OnPipeSpawned(Pipe pipe)
        {
            pipe.Out += OnPipeOut;
            pipe.Initialize(_speed);
            pipe.StartMove();
            
            _pipes.Enqueue(pipe);
        }

        private void OnPipeOut(Pipe pipe)
        {
            pipe.Out -= OnPipeOut;
            pipe.StopMove();
            
            _pipes.Dequeue();
        }
    }  
}
