using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteContainer : MonoBehaviour {
    public Sprite m_selectedSprite;
    Renderer m_renderer;
    MaterialPropertyBlock m_propertyBlock;
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_propertyBlock = new MaterialPropertyBlock();
    }
	// Update is called once per frame
	void Update () {
        m_renderer.GetPropertyBlock(m_propertyBlock);
        transform.localScale = SpriteQuadScale(m_selectedSprite);
        TouchGround();
        m_propertyBlock.SetTexture("_MainTex", textureFromSprite(m_selectedSprite));
        m_renderer.SetPropertyBlock(m_propertyBlock);
	}
    void TouchGround(){
        Vector3 newPosition = new Vector3();
        newPosition.y = -(1 - transform.localScale.y * 0.5f * Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.x));
        transform.localPosition = newPosition;
    }
    Vector3 SpriteQuadScale(Sprite sprite){
        float baseWidth = transform.localScale.x;
        float scaledHeight = baseWidth * sprite.rect.height / sprite.rect.width;
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
