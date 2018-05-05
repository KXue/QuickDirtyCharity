using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBehaviour : MonoBehaviour {
    public Transform m_player;
	void Update () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = m_player.position; 
    }
}
