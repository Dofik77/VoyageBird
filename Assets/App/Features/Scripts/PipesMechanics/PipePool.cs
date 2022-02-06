using System.Collections.Generic;
using UnityEngine;

namespace App.Fetures.Scripts.PipesMechanics
{
    public class PipePool
    {
        private readonly Stack<Pipe> _pooledObjects;
        private readonly Pipe _prefab;
        
        public PipePool(Pipe prefab)
        {
            _prefab = prefab;
            _pooledObjects = new Stack<Pipe>();
        }

        private bool HasObjects => _pooledObjects.Count > 0;

        public Pipe GetPooledObject()
        {
            return HasObjects 
                ? ReturnObjectFromStack() 
                : ReturnNewObject();
        }
        public void ReturnObjectToPool(Pipe poolObject)
        {
            poolObject.gameObject.SetActive(false);
            _pooledObjects.Push(poolObject);
        }
        
        private Pipe ReturnObjectFromStack()
        {
            var pipe = _pooledObjects.Pop();
            pipe.gameObject.SetActive(true);
            
            return pipe;
        }

        private Pipe ReturnNewObject()
        {
            return Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        }
    }
}
