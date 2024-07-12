using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public struct HealthDataComponent : IComponentData
{
    public float HealthValue;
    public float InvincibleTimer;
    public float KillTimer;
}
