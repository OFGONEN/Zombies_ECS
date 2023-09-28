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

        public int NumberTombstonesToSpawn => _graveyardProperties.ValueRO.NumberTombstonesToSpawn;
        public Entity TombstonePrefab => _graveyardProperties.ValueRO.TombstonePrefab;

        public UniformScaleTransform GetRandomTombstoneTransform => new UniformScaleTransform
        {
            Position = GetRandomPosition,
            Rotation = quaternion.identity,
            Scale = 1f
        };

        private float3 GetRandomPosition => _graveyardRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
        
        private float3 MinCorner => _transformAspect.Position - HalfDimensions;
        private float3 MaxCorner => _transformAspect.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = _graveyardProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0f,
            z = _graveyardProperties.ValueRO.FieldDimensions.y * 0.5f
        };
    }    
}