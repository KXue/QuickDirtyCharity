using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashShooter : MonoBehaviour {
    public Transform m_cashPrefab;
    public float m_shootSpeed;
    public float m_spawnDistance;
    public float m_maxRange;
    public float m_maxCash;
    private float m_cash;
	// Use this for initialization
	void Start () {
		m_cash = m_maxCash;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
            Vector3 foundClickPoint;
            if(FindClickPoint(out foundClickPoint)){
                Vector3 pointToPlayer = foundClickPoint - transform.position;
                
                if(pointToPlayer.sqrMagnitude > m_maxRange * m_maxRange ){
                    foundClickPoint = transform.position + pointToPlayer.normalized * m_maxRange;
                    foundClickPoint.y = 0;
                    pointToPlayer = foundClickPoint - transform.position;
                }

                Vector3 normalShootDirection = (foundClickPoint - transform.position).normalized;
                Vector3 spawnPosition = normalShootDirection * m_spawnDistance;

                Transform cashProjectile = Instantiate(m_cashPrefab,transform.position + spawnPosition, Quaternion.identity);
                cashProjectile.GetComponent<Rigidbody>().velocity = normalShootDirection * m_shootSpeed;
            }
        }
	}
    bool FindClickPoint(out Vector3 foundVector){
        bool retVal = false;
        foundVector = new Vector3();
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        string[] wantedLayers = {"Ground"};
        int layerMask = LayerMask.GetMask(wantedLayers);

        if(mouseRay.direction.y != 0 ){
            foundVector = mouseRay.origin + mouseRay.direction * (mouseRay.origin.y / -mouseRay.direction.y);
            retVal = true;

        }

        if(Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask)){
            foundVector = hit.point;
            retVal = true;
        }
        return retVal;
    }
}
