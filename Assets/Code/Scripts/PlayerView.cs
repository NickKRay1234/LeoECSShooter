using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    public class PlayerView : MonoBehaviour
    {
        public EcsEntity entity;

        public void Shoot()
        {
            entity.Get<HasWeapon>().weapon.Get<Shoot>();
        }
        
        public void Reload()
        {
            entity.Get<HasWeapon>().weapon.Get<ReloadingFinished>();
        }
    }
}