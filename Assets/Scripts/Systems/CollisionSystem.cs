using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Physics.Systems;

public partial class CollisionSystem : SystemBase
{
    protected override void OnUpdate()
    {

        ISimulation _physicsStepSimulation = World.GetOrCreateSystem<StepPhysicsWorld>().Simulation;

        Entities.ForEach((DynamicBuffer<CollisionBufferDataComponent> dynamicCollisionBuffer) => {

            dynamicCollisionBuffer.Clear();

        }).Run();


        var CollisionJobHandle = new CollisionSystemJob()
        {
            _collisions = GetBufferFromEntity<CollisionBufferDataComponent>()

        }.Schedule(_physicsStepSimulation, Dependency);

        CollisionJobHandle.Complete();


        Entities.ForEach((DynamicBuffer<TriggerBufferDataComponent> dynamicTriggerBuffer) => {

            dynamicTriggerBuffer.Clear();

        }).Run();


        var TriggerJobHandle = new TriggerSystemJob()
        {
            _triggers = GetBufferFromEntity<TriggerBufferDataComponent>()

        }.Schedule(_physicsStepSimulation, Dependency);

        TriggerJobHandle.Complete();

    }

    private struct CollisionSystemJob : ICollisionEventsJob
    {
       public BufferFromEntity<CollisionBufferDataComponent> _collisions;
        public void Execute(CollisionEvent collisionEvent)
        {

            if (_collisions.HasComponent(collisionEvent.EntityA))
                _collisions[collisionEvent.EntityA].Add(new CollisionBufferDataComponent { entity = collisionEvent.EntityB });

            if (_collisions.HasComponent(collisionEvent.EntityB))
                _collisions[collisionEvent.EntityB].Add(new CollisionBufferDataComponent { entity = collisionEvent.EntityA });


        }
    }


    private struct TriggerSystemJob : ITriggerEventsJob
    {
        public BufferFromEntity<TriggerBufferDataComponent> _triggers;

        public void Execute(TriggerEvent triggerEvent)
        {

            if (_triggers.HasComponent(triggerEvent.EntityA))
                _triggers[triggerEvent.EntityA].Add(new TriggerBufferDataComponent { entity = triggerEvent.EntityB });

            if (_triggers.HasComponent(triggerEvent.EntityB))
                _triggers[triggerEvent.EntityB].Add(new TriggerBufferDataComponent { entity = triggerEvent.EntityA });
        }
    }


}
