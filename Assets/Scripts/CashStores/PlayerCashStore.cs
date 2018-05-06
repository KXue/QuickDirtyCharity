using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCashStore : CashStore {
    public BarUI m_cashBar;
    public override float TakeCash(float amount){
        float retAmount = base.TakeCash(amount);
        UpdateCashUI();
        return retAmount;
    }
    void UpdateCashUI(){
        m_cashBar.FillFraction = m_cash/ m_startingCash;
    }
}
