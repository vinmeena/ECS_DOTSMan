using Unity.Entities;

//Basically IBufferElementData is used for Storing multiple value on an entity, Whereas IComponentData bound to store single single data on Entities.
public struct CollisionBufferDataComponent : IBufferElementData
{
    public Entity entity;
}
