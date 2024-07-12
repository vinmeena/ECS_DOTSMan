using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Mathematics;
using Unity.Transforms;

public class AnimationManager : MonoBehaviour
{

    [SerializeField]Animator _playerAnimator;
    [SerializeField] Transform _playerTransform;



    void Update()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var playerQuery = entityManager.CreateEntityQuery(typeof(PlayerTagComponent), typeof(PhysicsVelocity),typeof(Translation),typeof(Rotation), typeof(MoveComponent));

        

        if (playerQuery.CalculateEntityCount() <= 0)
            return;

        Reset();
        Entity playerEntity = playerQuery.GetSingletonEntity();

        _playerTransform.position = entityManager.GetComponentData<Translation>(playerEntity).Value;

        float moveAnimSpeed = math.length(entityManager.GetComponentData<PhysicsVelocity>(playerEntity).Linear);

        float3 rotationDirection = entityManager.GetComponentData<MoveComponent>(playerEntity).direction;

        if (math.length(rotationDirection)>.2f)
            _playerTransform.rotation = Quaternion.LookRotation(rotationDirection);

        _playerAnimator.SetFloat("Speed", moveAnimSpeed*10f);


    }

    public void WinAnimation()
    {
        _playerAnimator.SetBool("Win", true);
        _playerAnimator.SetFloat("Speed", 0);
    }

    public void LoseAnimation()
    {
        _playerAnimator.SetBool("Hit", true);
        _playerAnimator.SetFloat("Speed", 0);
    }

    public void Reset()
    {
        _playerAnimator.SetBool("Win", false);
        _playerAnimator.SetBool("Hit", false);
    }

}
