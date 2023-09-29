using Unity.Burst;
using Unity.Entities;

namespace TMG.Zombies
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public partial struct ApplyBrainDamageSystem : ISystem
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
            foreach (var brain in SystemAPI.Query<BrainAspect>())
            {
                brain.DamageBrain();
            }
        }
    }
}


