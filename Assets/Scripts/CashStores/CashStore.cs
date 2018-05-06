using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CashStore : MonoBehaviour {
    public float m_startingCash;
    public bool hasCash{
        get{
            return m_cash > 0;
        }
    }
    protected float m_cash;

	// Use this for initialization
	void Awake () {
		m_cash = m_startingCash;
	}
    public virtual float TakeCash(float amount){
        float retAmount = amount;
        if(amount > m_cash){
            retAmount = m_cash;
        }
        m_cash -= retAmount;
        return retAmount;
    }
}
