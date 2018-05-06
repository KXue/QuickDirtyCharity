using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float m_maxSpeed;
    public float m_acceleration;
    public float m_brakeForce;
    public float m_groundedEpsilon = 0.1f;
    public bool isKicked{
        get{
            return m_isKicked;
        }
        set{
            m_isKicked = value;
            if(value){
                m_kickedTimer = m_kickedTime;
            }
        }
    }
    private bool m_isGrounded = false;
    private bool m_isKicked = false;
    private float m_kickedTime = 0.5f;
    private float m_kickedTimer = 0;
    private Rigidbody m_rigidBody;
    private CapsuleCollider m_capsuleCollider;
    private AudioSource m_audioSource;
    public SpriteContainer m_spriteContainer;
    private Animator m_animator;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
	}
	void Update()
    {
        if(m_isKicked){
            m_kickedTimer -= Time.deltaTime;
            if(m_kickedTimer <= 0){
                m_isKicked = false;
            }
        }

    }
	void FixedUpdate () {
        CheckGrounded();
		if(GetMovementInput().sqrMagnitude > 0 && !m_isKicked){
            if(!m_audioSource.isPlaying){
                m_audioSource.Play();
            }
            Vector2 inputDirection = GetMovementInput();
            Vector3 desiredMoveDirection = new Vector3(inputDirection.x, 0, inputDirection.y) * m_acceleration;
            // Vector3 brakeVector = Vector3.Project(desiredMoveDirection, m_rigidBody.velocity); //little extra braking
            m_rigidBody.AddForce(desiredMoveDirection);
        }
        else if(m_isGrounded && !m_isKicked){
            Vector3 xZVelocity = m_rigidBody.velocity;
            if(xZVelocity.sqrMagnitude > 0.1 * 0.1){
                m_rigidBody.AddForce(m_brakeForce * -xZVelocity);
            } 
            // xZVelocity.y = 0;
        }
        if(!m_isKicked){
            if (m_rigidBody.velocity.sqrMagnitude > m_maxSpeed * m_maxSpeed)
            {
                m_rigidBody.velocity = m_rigidBody.velocity.normalized * m_maxSpeed;
            }
        }
        if(m_isGrounded && !m_isKicked){
            m_animator.SetFloat("Speed", m_rigidBody.velocity.magnitude);
        }
        else{
            m_animator.SetFloat("Speed", 0f);
        }
        if(m_rigidBody.velocity.x * m_spriteContainer.spriteScale > 0.1f){
            m_spriteContainer.spriteScale *= -1;
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
