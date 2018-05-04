using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float m_maxSpeed;
    public float m_acceleration;
    public float m_brakeForce;
    private Rigidbody m_rigidBody;
	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		if(GetMovementInput().sqrMagnitude > 0){
            Vector2 inputDirection = GetMovementInput();
            Vector3 desiredMoveDirection = new Vector3(inputDirection.x, 0, inputDirection.y) * m_acceleration;
            Vector3 brakeVector = Vector3.Project(desiredMoveDirection, m_rigidBody.velocity); //little extra braking
            m_rigidBody.AddForce(brakeVector + desiredMoveDirection);
        }
        else{
            m_rigidBody.AddForce(m_brakeForce * -m_rigidBody.velocity.normalized);
        }
        if(m_rigidBody.velocity.sqrMagnitude > m_maxSpeed * m_maxSpeed){
            m_rigidBody.velocity = m_rigidBody.velocity.normalized * m_maxSpeed;
        }
	}
    Vector2 GetMovementInput(){
        Vector2 retVal = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(retVal.sqrMagnitude > 1){
            retVal.Normalize();
        }
        return retVal;
    }
}
