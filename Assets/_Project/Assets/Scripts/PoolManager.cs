using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Spotnose.Stardust
{
    [DisallowMultipleComponent]
    public class PoolManager : Singleton<PoolManager>
    {
        [Tooltip("Populate this with prefabs that you want to add to the pool, and the pool size of each.")]
        [SerializeField] private Pool[] pools;

        private Transform _objectPoolTransform;
        private Dictionary<int, Queue<Component>> _poolDictionary = new();

        [Serializable]
        public struct Pool
        {
            public int poolSize;
            public GameObject prefab;
            public PoolComponentType componentType; // TODO: potentially make this of type Component
        }

        private void Start()
        {
            _objectPoolTransform = gameObject.transform;
            
            foreach (var pool in pools) CreatePool(pool.prefab, pool.poolSize, pool.componentType.ToString());
        }

        private void CreatePool(GameObject prefab, int poolSize, string componentType)
        {
            var poolKey = prefab.GetInstanceID();
            var parentTransform = new GameObject($"{prefab.name}Pool").transform;
            parentTransform.SetParent(_objectPoolTransform);
            
            // don't create the pool if the pool dict already contains a pool with this key
            if (_poolDictionary.ContainsKey(poolKey)) return;
            
            var pool = new Queue<Component>();
            var poolComponentType = Type.GetType($"Spotnose.Stardust.{componentType}");

            for (var i = 0; i < poolSize; i++)
            {
                var newObject = Object.Instantiate(prefab, parentTransform);
                newObject.SetActive(false);
                pool.Enqueue(newObject.GetComponent(poolComponentType));
            }

            _poolDictionary.Add(poolKey, pool);
        }
        
        // Returns a pooled object of the given prefab type, and sets its spawn position and rotation.
        public Component GetObject(GameObject prefab, Vector3 spawnPosition = new(), Quaternion spawnRotation = new())
        {
            var poolKey = prefab.GetInstanceID();
            
            if (!_poolDictionary.ContainsKey(poolKey)) // log warning
                Debug.LogWarning($"PoolManager: Pool for {prefab.name} does not exist.");

            var pool = _poolDictionary[poolKey];
            var objectToReturn = pool.Dequeue();
            pool.Enqueue(objectToReturn);
            
            var returnGameObject = objectToReturn.gameObject;
            if (returnGameObject.activeSelf) returnGameObject.SetActive(false); // TODO: is this necessary?
            
            returnGameObject.transform.position = spawnPosition;
            returnGameObject.transform.rotation = spawnRotation;
            returnGameObject.transform.localScale = prefab.transform.localScale;
            
            return objectToReturn;
        }

        public enum PoolComponentType
        {
            DustParticulate,
            MetalParticulate,
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            
        }
#endif
    }
}
