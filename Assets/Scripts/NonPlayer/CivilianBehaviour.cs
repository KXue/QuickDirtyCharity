using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianBehaviour : MonoBehaviour {
    public float m_collectRadius = 1;
    public float m_cashCollectRate = 10;
    public float m_maxSpeed = 10f;
    public float m_acceleration = 10f;
    public float m_noticeRange = 10f;
    public SpriteContainer m_spriteContainer;
    private CashStore m_cashPileScript;
    private CashStore m_cashStore;
    private Rigidbody m_rigidBody;
    private AudioSource m_audioSource;
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_cashStore = GetComponent<CashStore>();
    }
    void Update() {
        DetectCash();
        if(m_cashPileScript != null && m_cashPileScript.gameObject.activeInHierarchy){
            MoveTowardsCash();
            CollectCash();
        }
        if (m_rigidBody.velocity.x * m_spriteContainer.spriteScale < -0.01f)
        {
            m_spriteContainer.spriteScale *= -1;
        }
    }
    void DetectCash(){
        string[] wantedLayers = {"Cash"};
        int layerMask = LayerMask.GetMask(wantedLayers);
        Collider[] hitResults = Physics.OverlapSphere(transform.position, m_noticeRange, layerMask);
        foreach(Collider hit in hitResults){
            if(hit.transform.GetComponent<CashBehaviour>().m_isOnGround && 
                (m_cashPileScript == null 
                    || m_cashPileScript != null && (hit.transform.position - transform.position).sqrMagnitude < (m_cashPileScript.transform.position - transform.position).sqrMagnitude)){
                if(m_cashPileScript == null){
                    m_audioSource.Play();
                }
                m_cashPileScript = hit.transform.GetComponent<CashStore>();
            }
        }
    }
    void CollectCash(){
        if((m_cashPileScript.transform.position - transform.position).sqrMagnitude < m_collectRadius * m_collectRadius){
            m_cashStore.GetCash(m_cashPileScript.TakeCash(m_cashCollectRate * Time.deltaTime));
        }
    }
    void MoveTowardsCash(){
        Vector3 moveDirection = (m_cashPileScript.transform.position - transform.position);
        moveDirection.y = 0;
        m_rigidBody.AddForce(moveDirection.normalized * m_acceleration);
        if(m_rigidBody.velocity.sqrMagnitude > m_maxSpeed * m_maxSpeed){
            m_rigidBody.velocity = m_rigidBody.velocity.normalized * m_maxSpeed;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if(m_cashPileScript != null){
            Gizmos.DrawLine(transform.position, m_cashPileScript.transform.position);
        }
    }
}
