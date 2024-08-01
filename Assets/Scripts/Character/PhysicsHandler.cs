using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhysicsHandler
{
    Transform character;
    CharacterController controller;

    float currentVerticalVelocity;
    Quaternion targetRotation;

    public float TURN_RATE = 10f;
    public float GRAVITY = -9.8f;
    public float FALLING_ACCELERATION_RATE = 1.2f;

    const float GROUND_CHECK_RADIUS = 0.18f;
    const float GROUND_CHECK_OFFSET = 0.1f;
    LayerMask GROUND_MASK = LayerMask.GetMask("Ground");
    const float NEAR_GROUND_FALL_TIME = 0.8f;

    public PhysicsHandler(Transform character, CharacterController controller)
    {
        this.character = character;
        this.controller = controller;
    }

    public bool IsGrounded()
    {
        Collider[] cols = Physics.OverlapSphere(character.position + Vector3.up * GROUND_CHECK_OFFSET, GROUND_CHECK_RADIUS, GROUND_MASK, QueryTriggerInteraction.Ignore);

        return cols.Length > 0;
    }

    public bool IsNearGround()
    {
        float nearGroundDIstance = currentVerticalVelocity * NEAR_GROUND_FALL_TIME + 0.5f * GRAVITY * NEAR_GROUND_FALL_TIME * NEAR_GROUND_FALL_TIME;
        if (Physics.Raycast(character.position, Vector3.down, nearGroundDIstance, GROUND_MASK, QueryTriggerInteraction.Ignore))
        {
            return true;
        }
        return false;
    }
    public void MoveHorizontal(Vector3 dir, float power)
    {
        dir.y = 0;
        controller.Move(dir.normalized * power);
    }

    public void MoveVertical()
    {
        controller.Move(Vector3.up * currentVerticalVelocity);
    }

    public void MoveVerticalForced(Vector3 dir, float power)
    {
        dir.x = 0;
        dir.z = 0;
        controller.Move(dir.normalized * power);
    }

    public void ApplyGravity()
    {
        currentVerticalVelocity += GRAVITY;
    }

    public void SetVerticalVelocity(float velocity)
    {
        currentVerticalVelocity = velocity;
    }

    public void SetTargetRotation(Quaternion targetRotation)
    {
        this.targetRotation = targetRotation;
    }

    public void Turn()
    {
        character.rotation = Quaternion.Slerp(character.rotation, targetRotation, TURN_RATE * Time.deltaTime);
    }
}
