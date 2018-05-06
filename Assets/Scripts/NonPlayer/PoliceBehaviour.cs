using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBehaviour : MonoBehaviour {
    public float m_stealAmount = 100;
    public Transform m_player;
    public Vector3 m_kickVector;
	void Update () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = m_player.position; 
    }
    private void OnCollisionEnter(Collision other) {
        if(other.transform == m_player){
            StealFrom(other.gameObject.GetComponent<CashStore>());
            KickOut(other.rigidbody);
        }
    }
    void StealFrom(CashStore other){
        other.TakeCash(m_stealAmount);
    }
    void KickOut(Rigidbody other){
        other.AddForce(transform.rotation * m_kickVector, ForceMode.Impulse);
    }
}
