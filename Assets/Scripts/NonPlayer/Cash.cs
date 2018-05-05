using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour {
    public float m_startingCash;
    public float m_influenceRadius;
    public float m_collideRadius;
    public bool m_isOnGround = false;
    private float m_cash;

    void Start()
    {
        m_cash = m_startingCash;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            m_isOnGround = true;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector3();
            rigidbody.isKinematic = true;
            SphereCollider collider = GetComponent<SphereCollider>();
            collider.radius = m_collideRadius;
            collider.isTrigger = false;
        }
    }
    public float TakeCash(float amount){
        float retAmount = amount;
        if(amount > m_cash){
            retAmount = m_cash;
        }
        m_cash -= retAmount;
        if(m_cash <= 0){
            Destroy(gameObject);
        }
        return retAmount;
    }

}
