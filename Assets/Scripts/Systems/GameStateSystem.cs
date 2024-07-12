using Unity.Entities;
using UnityEngine;

//ENTITY QUERY NOTE: If We don't have any entity in Query than it won't run by default because of the "Nothing to return than don't run" Behaviour
// If We still want to run on "Nothing return state of query" than we have to add an Attribute "AlwaysUpdateSystem".

[AlwaysUpdateSystem]
public partial class GameStateSystem : SystemBase
{
    protected override void OnUpdate()
    {

        var PelletCollectablesQuery = GetEntityQuery(ComponentType.ReadOnly<PelletDataComponent>());
        var PlayerQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerTagComponent>());

        GameManager.instance.UpdatePellet(PelletCollectablesQuery.CalculateEntityCount());

        if (PelletCollectablesQuery.CalculateEntityCount() <= 0)
        {
            GameManager.instance.Win();
        }

        if (PlayerQuery.CalculateEntityCount() <= 0)
        {
            GameManager.instance.Lose();
        }

    }
}
