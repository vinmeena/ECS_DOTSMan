using Unity.Entities;

public partial class CollectionSystem : SystemBase
{
    protected override void OnUpdate()
    {

        EndSimulationEntityCommandBufferSystem bufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        EntityCommandBuffer entityCommandBuffer = bufferSystem.CreateCommandBuffer();

        Entities
            .WithAll<PlayerTagComponent>()
            .ForEach((Entity playerEntity,DynamicBuffer<TriggerBufferDataComponent> triggers) =>
            {

                for(int i=0;i<triggers.Length;i++)
                {
                    if(HasComponent<CollectablesDataComponent>(triggers[i].entity) && !HasComponent<KillDataComponent>(triggers[i].entity))
                    {
                        entityCommandBuffer.AddComponent(triggers[i].entity, new KillDataComponent { KillTimerValue = 0f });

                        GameManager.instance.AddPoints( GetComponent<CollectablesDataComponent>(triggers[i].entity).CollectablePointsValue);

                    }

                    if(HasComponent<PowerPillDataComponent>(triggers[i].entity)&&!HasComponent<KillDataComponent>(triggers[i].entity))
                    {
                        AudioManager.instance.PlayMusic("powerup");
                        entityCommandBuffer.AddComponent(playerEntity, GetComponent<PowerPillDataComponent>(triggers[i].entity));
                        entityCommandBuffer.AddComponent(triggers[i].entity, new KillDataComponent { KillTimerValue = 0f });
                    }

                }

            }).WithoutBurst().Run();

    }
}
