using Unity.Burst;
using Unity.Entities;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateAfter(typeof(SpawnZombieSystem))]
    public partial struct ZombieRiseSystem : ISystem 
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new ZombieRiseJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    public partial struct ZombieRiseJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(ZombieRiseAspect zombie)
        {
            zombie.Rise(DeltaTime);
        }
    } 
}