using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float m_maxSpeed;
    public float m_acceleration;
    public float m_brakeForce;
    public float m_groundedEpsilon = 0.1f;
    private bool m_isGrounded = false;
    private Rigidbody m_rigidBody;
    CapsuleCollider m_capsuleCollider;
	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
	}
	
	void FixedUpdate () {
        CheckGrounded();
		if(GetMovementInput().sqrMagnitude > 0){
            Vector2 inputDirection = GetMovementInput();
            Vector3 desiredMoveDirection = new Vector3(inputDirection.x, 0, inputDirection.y) * m_acceleration;
            // Vector3 brakeVector = Vector3.Project(desiredMoveDirection, m_rigidBody.velocity); //little extra braking
            m_rigidBody.AddForce(desiredMoveDirection);
        }
        else if(m_isGrounded){
             Vector3 xZVelocity = m_rigidBody.velocity.normalized;
            // xZVelocity.y = 0;
            m_rigidBody.AddForce(m_brakeForce * -xZVelocity);
        }

        if(m_rigidBody.velocity.sqrMagnitude > m_maxSpeed * m_maxSpeed){
            m_rigidBody.velocity = m_rigidBody.velocity.normalized * m_maxSpeed;
        }
	}
    void CheckGrounded(){
        string[] desiredLayers = {"Ground"};
        int layerMask = LayerMask.GetMask(desiredLayers);
        RaycastHit hit;
        m_isGrounded = Physics.SphereCast(transform.position, m_capsuleCollider.radius, Vector3.down, out hit, m_capsuleCollider.height * 0.5f + m_groundedEpsilon, layerMask);
    }
    Vector2 GetMovementInput(){
        Vector2 retVal = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(retVal.sqrMagnitude > 1){
            retVal.Normalize();
        }
        return retVal;
    }
}
