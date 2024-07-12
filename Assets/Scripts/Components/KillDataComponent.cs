using Unity.Entities;

[GenerateAuthoringComponent]
public struct KillDataComponent : IComponentData
{
    public float KillTimerValue;
}
