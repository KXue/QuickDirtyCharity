using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCashStore : CashStore {
    public Transform m_cashContainer;
    public GameManager m_gameManager;
    public BarUI m_cashBar;
    void Update()
    {
        CheckGameOver();
    }
    public override float TakeCash(float amount){
        float retAmount = base.TakeCash(amount);
        UpdateCashUI();
        return retAmount;
    }
    void UpdateCashUI(){
        m_cashBar.FillFraction = m_cash/ m_startingCash;
    }
    void CheckGameOver(){
        if(m_cash <= 0 && m_cashContainer.childCount == 0){
            m_gameManager.PauseGame();
            m_gameManager.GameOver();
        }
    }
}
