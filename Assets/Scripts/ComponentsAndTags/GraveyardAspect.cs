using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Zombies
{
    public readonly partial struct GraveyardAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;
        private readonly RefRO<GraveyardProperties> _graveyardProperties;
        private readonly RefRW<GraveyardRandom> _graveyardRandom;
        private readonly RefRW<ZombieSpawnPoints> _zombieSpawnPoints;

        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        public NativeArray<float3> ZombieSPawnPoints
        {
            get => _zombieSpawnPoints.ValueRO.Value;
            set => _zombieSpawnPoints.ValueRW.Value = value;
        }
        public UniformScaleTransform GetRandomTombstoneTransform => new UniformScaleTransform
        {
            Position = GetRandomPosition(),
            Rotation = GetRandomRotation,
            Scale = GetRandomScale(0.5f)
        };

        // private float3 GetRandomPosition() => _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(_transformAspect.Position, randomPosition) < BRAIN_SAFETY_RADIUS_SQ);

            return randomPosition;
        }
        
        private float3 MinCorner => _transformAspect.Position - HalfDimensions;
        private float3 MaxCorner => _transformAspect.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = _graveyardProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0f,
            z = _graveyardProperties.ValueRO.FieldDimensions.y * 0.5f
        };
        private const float BRAIN_SAFETY_RADIUS_SQ = 100;

        private quaternion GetRandomRotation =>
            quaternion.RotateY(_graveyardRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
        private float GetRandomScale(float min) => _graveyardRandom.ValueRW.Value.NextFloat(min, 1f);
    }    
}