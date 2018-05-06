using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPileStore : CashStore {
    public override float TakeCash(float amount){
        float retAmount = base.TakeCash(amount);
        if(m_cash <= 0){
            Destroy(gameObject);
        }
        return retAmount;
    }
}
