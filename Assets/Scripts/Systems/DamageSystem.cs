using Unity.Entities;

public partial class DamageSystem : SystemBase
{
    protected override void OnUpdate()
    {

        float deltaTime = Time.DeltaTime;

        EndSimulationEntityCommandBufferSystem bufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

        EntityCommandBuffer entityCommandBuffer = bufferSystem.CreateCommandBuffer();

        Entities
            .ForEach((DynamicBuffer<CollisionBufferDataComponent> collisionBuffer, ref HealthDataComponent healthData) =>
            {

                for(int i=0;i<collisionBuffer.Length;i++)
                {
                    if(healthData.InvincibleTimer<=0 && HasComponent<DamageDataComponent>(collisionBuffer[i].entity))
                    {
                        healthData.HealthValue -= GetComponent<DamageDataComponent>(collisionBuffer[i].entity).DamageValue;
                        healthData.InvincibleTimer = 1f;
                    }
                }
            }).Schedule(Dependency).Complete();


        Entities
            .WithNone<KillDataComponent>()
            .ForEach((Entity entity, ref HealthDataComponent healthData) =>
        {
            healthData.InvincibleTimer -= deltaTime;

            if (healthData.HealthValue <= 0)
                EntityManager.AddComponentData(entity, new KillDataComponent { KillTimerValue = healthData.KillTimer });

        }).WithStructuralChanges().Run();



        Entities.ForEach((Entity entity, ref KillDataComponent killData) =>
        {
                killData.KillTimerValue -= deltaTime;

            if (killData.KillTimerValue <= 0)
            {
                if (HasComponent<OnKillDataComponent>(entity))
                {
                    OnKillDataComponent onKillDataComponent = GetComponent<OnKillDataComponent>(entity);

                    AudioManager.instance.PlaySFX(onKillDataComponent.customKillDataBlobAssetsRef.Value.customString.ToString());
                }


                entityCommandBuffer.DestroyEntity(entity);
            }


        }).WithoutBurst().Run();

        bufferSystem.AddJobHandleForProducer(this.Dependency);

    }
}
