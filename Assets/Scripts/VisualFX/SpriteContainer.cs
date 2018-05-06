using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SpriteContainer : MonoBehaviour {
    public Sprite m_selectedSprite;
    private Sprite m_previousSprite = null;
    public float spriteScale{
        get{
            return m_spriteScale;
        }
        set{
            m_spriteScale = value;
        }
    }
    public float m_spriteScale = 1;
    Renderer m_renderer;
    MaterialPropertyBlock m_propertyBlock;
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_propertyBlock = new MaterialPropertyBlock();
        UpdateSprite();
    }
	// Update is called once per frame
	void Update () {
        UpdateSprite();
	}
    void UpdateSprite(){
        if(m_previousSprite == null || m_previousSprite != m_selectedSprite){
            m_renderer.GetPropertyBlock(m_propertyBlock);
            m_propertyBlock.SetTexture("_MainTex", textureFromSprite(m_selectedSprite));
            m_renderer.SetPropertyBlock(m_propertyBlock);
            m_previousSprite = m_selectedSprite;
        }
        transform.localScale = SpriteQuadScale(m_selectedSprite);
    }

    Vector3 SpriteQuadScale(Sprite sprite){
        float baseWidth = m_spriteScale * sprite.rect.width / sprite.pixelsPerUnit;
        float scaledHeight = Mathf.Abs(baseWidth * sprite.rect.height / sprite.rect.width);
        return new Vector3(
            baseWidth,
            scaledHeight,
            1f
        );
    }
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if(sprite.rect.width != sprite.texture.width){
            Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
                                                        (int)sprite.textureRect.y, 
                                                        (int)sprite.textureRect.width, 
                                                        (int)sprite.textureRect.height );
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        } else
            return sprite.texture;
    }
}
