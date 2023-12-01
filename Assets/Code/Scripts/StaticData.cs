using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        public GameObject playerPrefab;
        public float playerSpeed;
    }
}