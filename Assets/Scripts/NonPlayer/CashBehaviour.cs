using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBehaviour : MonoBehaviour {
    public float m_influenceRadius;
    public float m_collideRadius;
    public bool m_isOnGround = false;
    private CashStore m_cashStore;
    void Start()
    {
        m_cashStore = GetComponent<CashStore>();
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
}
