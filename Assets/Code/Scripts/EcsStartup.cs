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
                .OneFrame<TryReload>()
                .Add(new PlayerInputSystem())
                .Add(new PlayerRotationSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new WeaponShootSystem())
                .Add(new SpawnProjectileSystem())
                .Add(new ProjectileMoveSystem())
                .Add(new ProjectileHitSystem())
                .Add(new ReloadingSystem())
                .Inject(configuration)
                .Inject(sceneData)
                .Inject(runtimeData);

            _fixedUpdateSystems
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
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
            _ecsWorld.Destroy();
            _updateSystems.Destroy();
        }
    }
}