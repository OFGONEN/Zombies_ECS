using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TMG.Zombies 
{
    public readonly partial struct ZombieRiseAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<ZombieRiseRate> _zombieRiseRate;

        public void Rise(float deltaTime)
        {
            _transformAspect.Position += math.up() * _zombieRiseRate.ValueRO.Value * deltaTime;
        }

        public bool IsAboveGround => _transformAspect.Position.y > 0f;

        public void SetAtGroundLevel()
        {
            var position = _transformAspect.Position;
            position.y = 0;
            _transformAspect.Position = position;
        }
    }
}