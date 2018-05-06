using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessCalculator : MonoBehaviour {
	public Transform m_civilianContainer;
	public CashStore m_playerWealth;
	public FaceUI m_faceUI;
	public float happinessPercentage{
		get{
			return m_currentSum / m_bestCaseSum;
		}
	}
	private float m_bestCaseSum;
	private float m_expectedAverage;
	private float m_currentSum;
	private List<CashStore> m_civilianStores;
	// Use this for initialization
	void Start () {
        m_bestCaseSum = m_playerWealth.m_startingCash;
        m_expectedAverage = m_bestCaseSum / m_civilianContainer.childCount;
		PopulateCashList();
    }
	
	// Update is called once per frame
	void Update () {
		CalculateOverallHappiness();
		UpdateUI();
	}
	void PopulateCashList(){
		m_civilianStores = new List<CashStore>();
        foreach (Transform child in m_civilianContainer)
        {
			CashStore newCashStore = child.GetComponent<CashStore>();
			if(newCashStore != null){
				m_civilianStores.Add(newCashStore);
			}
        }
	}
	void CalculateOverallHappiness(){
		m_currentSum = 0;
		foreach(CashStore store in m_civilianStores){
            m_currentSum += Mathf.Min(m_expectedAverage, store.cash);
		}
	}
	void UpdateUI(){
		m_faceUI.FillFraction = happinessPercentage;
	}
}
