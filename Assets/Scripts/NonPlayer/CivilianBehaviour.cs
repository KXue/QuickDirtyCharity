using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianBehaviour : MonoBehaviour {
    public float m_collectRadius = 1;
    public float m_cashCollectRate = 10;
    public float m_maxSpeed = 10f;
    public float m_acceleration = 10f;
    public float m_noticeRange = 10f;
    private float m_cash = 0;
    private CashStore m_cashScript;
    private Rigidbody m_rigidBody;
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }
    void Update() {
        DetectCash();
        if(m_cashScript != null && m_cashScript.gameObject.activeInHierarchy){
            MoveTowardsCash();
            CollectCash();
        }
    }
    void DetectCash(){
        string[] wantedLayers = {"Cash"};
        int layerMask = LayerMask.GetMask(wantedLayers);
        RaycastHit[] hitResults = Physics.SphereCastAll(transform.position, m_noticeRange, Vector3.up, Mathf.Infinity, layerMask);
        foreach(RaycastHit hit in hitResults){
            if(hit.transform.GetComponent<CashBehaviour>().m_isOnGround && 
                (m_cashScript == null 
                    || m_cashScript != null && (hit.transform.position - transform.position).sqrMagnitude < (m_cashScript.transform.position - transform.position).sqrMagnitude)){
                m_cashScript = hit.transform.GetComponent<CashStore>();
            }
        }
    }
    void CollectCash(){
        if((m_cashScript.transform.position - transform.position).sqrMagnitude < m_collectRadius * m_collectRadius){
            m_cash += m_cashScript.TakeCash(m_cashCollectRate * Time.deltaTime);
        }
    }
    void MoveTowardsCash(){
        m_rigidBody.AddForce((m_cashScript.transform.position - transform.position).normalized * m_acceleration);
        if(m_rigidBody.velocity.sqrMagnitude > m_maxSpeed * m_maxSpeed){
            m_rigidBody.velocity.Normalize();
        }
    }
}
