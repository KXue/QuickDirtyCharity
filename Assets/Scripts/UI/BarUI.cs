using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum FillScheme{linear, quadratic}
public class BarUI : MonoBehaviour {
    public FillScheme m_fillScheme = FillScheme.quadratic;
    public Image m_fillImage;
    [SerializeField]
    [Range(0, 1)]
    protected float m_fillPercentage = 1;
    private void Start() {
        FillFraction = 1;
    }
    public float FillFraction{
        get{
            return m_fillPercentage;
        }
        set{
            m_fillPercentage = Mathf.Clamp(value, 0, 1);
            UpdateUI();
        }
    }
    protected virtual void UpdateUI(){
        switch(m_fillScheme){
            case FillScheme.linear:
                m_fillImage.fillAmount = m_fillPercentage;
                break;
            case FillScheme.quadratic:
                m_fillImage.fillAmount = m_fillPercentage * m_fillPercentage;
                break;
            default:
                break;
        }
    }

}
