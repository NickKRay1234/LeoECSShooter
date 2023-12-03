using Leopotam.Ecs;

namespace Shooter
{
    public class ProjectileHitSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile, ProjectileHit> filter;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var projectile = ref filter.Get1(i);
            
                projectile.projectileGO.SetActive(false);
                // Здесь немного пустовато. Мы добавим больше функционала в новых частях
            }
        }
    }
}