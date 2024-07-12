using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct EnemyAIDataComponent : IComponentData
{
    public float3 previousCell;

}
