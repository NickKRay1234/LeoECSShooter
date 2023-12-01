using Leopotam.Ecs;
using UnityEngine;

namespace Shooter {
    internal sealed class EcsStartup : MonoBehaviour {

        public StaticData configuration;
        public SceneData sceneData;
        private EcsWorld _ecsWorld;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;


        private void Start () 
        {
            _ecsWorld = new EcsWorld ();
            _updateSystems = new EcsSystems (_ecsWorld);
            _fixedUpdateSystems = new EcsSystems(_ecsWorld);
            RuntimeData runtimeData = new RuntimeData();
            
            
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_updateSystems);
#endif

            _updateSystems
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new PlayerRotationSystem())
                .Inject(configuration)
                .Inject(sceneData)
                .Inject(runtimeData);

            _fixedUpdateSystems
                .Add(new PlayerMoveSystem())
                .Inject(configuration)
                .Inject(sceneData)
                .Inject(runtimeData);
            
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
        }

        private void Update () {
            _updateSystems?.Run ();
        }
        
        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy () {
            if (_updateSystems != null) {
                _updateSystems.Destroy ();
                _fixedUpdateSystems?.Destroy();
                _fixedUpdateSystems = null;
                _updateSystems = null;
                _ecsWorld.Destroy ();
                _ecsWorld = null;
            }
        }
    }
}