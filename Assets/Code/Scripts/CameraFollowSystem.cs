using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Player> _filter;
        private SceneData _sceneData;
        private StaticData _staticData;
        
        private Vector3 currentVelocity;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);

                var currentPos = _sceneData.mainCamera.transform.position;
                currentPos = Vector3.SmoothDamp(currentPos, player.playerTransform.position + _staticData.followOffset,
                    ref currentVelocity, _staticData.smoothTime);
                _sceneData.mainCamera.transform.position = currentPos;
            }
        }
    }
}