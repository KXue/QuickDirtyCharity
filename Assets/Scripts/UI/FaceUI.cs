using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FaceUI : BarUI {
    public Image m_faceImage;
    public float m_faceChangeCutoff = 0.5f;
    public Color m_lowPercentColor;
    public Color m_highPercentColor;
    public Sprite m_lowPercentSprite;
    public Sprite m_highPercentSprite;
    protected override void UpdateUI(){
        base.UpdateUI();
        float lerpT = m_fillPercentage;

        if(m_fillScheme == FillScheme.quadratic){
            lerpT = m_fillPercentage * m_fillPercentage;
        }
        m_fillImage.color = Color.Lerp(m_lowPercentColor, m_highPercentColor, lerpT);

        if(m_fillPercentage > m_faceChangeCutoff){
            m_faceImage.sprite = m_highPercentSprite;
        }
        else{
            m_faceImage.sprite = m_lowPercentSprite;
        }
    }
}
