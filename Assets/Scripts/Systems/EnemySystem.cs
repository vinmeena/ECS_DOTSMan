using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Collections;
using Unity.Physics.Systems;

//Used for RayCasting, Whenever using raycast we need to use this attribute to make it work
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public partial class EnemySystem : SystemBase
{
    Random random = new Random(1234);
    protected override void OnUpdate()
    {

        random.NextInt();
        var randomTemp = random;


        ECSRayCastSystem raycastSystem = new ECSRayCastSystem()
        {
            physicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld
        };


           Entities
            .ForEach((ref MoveComponent moveComponent, ref EnemyAIDataComponent enemyData, in Translation translation) =>
            {

                if(math.distance(translation.Value,enemyData.previousCell)>0.9f)
                {
                    enemyData.previousCell = math.round(translation.Value);

                    var validDirectionList = new NativeList<float3>(Allocator.Temp);


                    if (!raycastSystem.Raycast(translation.Value, new float3(0, 0, -1), moveComponent.direction))
                        validDirectionList.Add(new float3(0, 0, -1));

                    if (!raycastSystem.Raycast(translation.Value, new float3(0, 0, 1), moveComponent.direction))
                        validDirectionList.Add(new float3(0, 0, 1));

                    if (!raycastSystem.Raycast(translation.Value, new float3(-1, 0, 0), moveComponent.direction))
                        validDirectionList.Add(new float3(-1, 0, 0));

                    if (!raycastSystem.Raycast(translation.Value, new float3(1, 0, 0), moveComponent.direction))
                        validDirectionList.Add(new float3(1, 0, 0));


                    moveComponent.direction = validDirectionList[randomTemp.NextInt(validDirectionList.Length)];

                    validDirectionList.Dispose();

                }

            }).Schedule(Dependency).Complete();

    }


    private struct ECSRayCastSystem
    {

       [ReadOnly]public PhysicsWorld physicsWorld;

       public bool Raycast(float3 rayOrigin,float3 rayDirection,float3 rayCurrentDirection)
        {

            if (rayDirection.Equals(-rayCurrentDirection))
                return true;

            var ray = new RaycastInput()
            {
                Start = rayOrigin,
                End = rayOrigin + (rayDirection * 0.9f),
                Filter = new CollisionFilter()
                {
                    GroupIndex = 0,
                    BelongsTo = 1u << 1,
                    CollidesWith = 1u << 2
                }
            };

            return physicsWorld.CastRay(ray);
        }
    
    }



}
