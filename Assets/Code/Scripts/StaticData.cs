﻿using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        public GameObject playerPrefab;
        public float playerSpeed;
        public float smoothTime;
        public Vector3 followOffset;
    }
}