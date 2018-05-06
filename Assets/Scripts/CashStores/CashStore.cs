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
    public float cash{
        get{
            return m_cash;
        }
        set{
            m_cash = value;
        }
    }
    [SerializeField]
    protected float m_cash;

	// Use this for initialization
	void Start () {
		m_cash = m_startingCash;
	}
    public virtual float TakeCash(float amount){
        float retAmount = Mathf.Min(m_cash, amount);
        m_cash -= retAmount;
        return retAmount;
    }
    public virtual void GetCash(float amount){
        m_cash += amount;
    }
}
