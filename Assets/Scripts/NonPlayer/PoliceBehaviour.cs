using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBehaviour : MonoBehaviour {
    public float m_stealAmount = 100;
    public Vector3 m_kickVector;
    public float m_playerNoticeRange = 5f;
    public AudioClip m_detectedClip;
    public SpriteContainer m_spriteContainer;
    private Transform m_player;
    private NavMeshAgent m_agent;
    private AudioSource m_audioSource;
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_agent = GetComponent<NavMeshAgent>();
    }
    void Update () {
        if(m_player == null){
            LookForPlayer();
        }
        else{
            m_agent.destination = m_player.position;
        }
        if(transform.forward.x * m_spriteContainer.spriteScale < -0.01f){
            m_spriteContainer.spriteScale *= -1;
        }
    }
    private void OnCollisionStay(Collision other) {
        if(other.transform == m_player){
            PlayerMovement playerMovement = m_player.GetComponent<PlayerMovement>();
            if(!playerMovement.isKicked){
                StealFrom(other.gameObject.GetComponent<CashStore>());
                KickOut(other.rigidbody);
                playerMovement.isKicked = true;
            }
        }
    }
    void StealFrom(CashStore other){
        other.TakeCash(m_stealAmount);
    }
    void KickOut(Rigidbody other){
        other.AddForce(transform.rotation * m_kickVector, ForceMode.Impulse);
        m_audioSource.Play();
    }
    void LookForPlayer(){
        string[] wantedLayers = { "Player" };
        int layerMask = LayerMask.GetMask(wantedLayers);
        Collider[] hitResults = Physics.OverlapSphere(transform.position, m_playerNoticeRange, layerMask);
        if(hitResults.Length > 0){
            Debug.Log("hit");
            m_player = hitResults[0].transform;
            m_audioSource.PlayOneShot(m_detectedClip);
        } 
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_playerNoticeRange);
    }
}
