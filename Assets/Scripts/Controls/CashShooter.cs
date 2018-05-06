using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashShooter : MonoBehaviour {
    public CashStore m_cashPrefab;
    public float m_shootSpeed;
    public float m_spawnDistance;
    public float m_cashShotValue = 20f;
    public Transform m_cashCollection;
    public Transform m_crosshair;
    private CashStore m_cash;
	// Use this for initialization
	void Start () {
		m_cash = GetComponent<CashStore>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && m_cash.hasCash){
            FireCash();
        }
	}
    void FireCash(){
        Vector3 normalShootDirection = (m_crosshair.position - transform.position).normalized;
        Vector3 spawnPosition = normalShootDirection * m_spawnDistance;

        CashStore cashProjectile = Instantiate(m_cashPrefab, transform.position + spawnPosition, Quaternion.identity, m_cashCollection);
        cashProjectile.transform.parent = m_cashCollection;
        cashProjectile.m_startingCash = m_cash.TakeCash(m_cashShotValue);
        cashProjectile.GetComponent<Rigidbody>().velocity = normalShootDirection * m_shootSpeed;
    }
}
