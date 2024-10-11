using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

[RequireComponent(typeof(SpriteRenderer))]

public class SpriteNormalMapAnimator : MonoBehaviour
{
    SpriteRenderer sr;

    void Awake()
    {
        sr=GetComponent<SpriteRenderer>();
    }

    // ============================================================================

#if UNITY_EDITOR
    void OnEnable()
    {
        EditorApplication.update += EditorUpdate;
    }

    void OnDisable()
    {
        EditorApplication.update -= EditorUpdate;
    }

    void EditorUpdate()
    {
        if(!Application.isPlaying) UpdateNormal();
    }
#endif

    // ============================================================================
    
    void Update()
    {
        if(!Application.isPlaying) return;

        UpdateNormal();
    }
    
    // ============================================================================

    [HideInInspector]
    public Sprite normalSprite; // use animator to control this

    public float updateInterval=.1f;

    float timer=0;

    public bool enable=true;

    void UpdateNormal()
    {
        if(!enable) return;

        timer += Time.unscaledDeltaTime;

        // Switch normal maps every n seconds
        if (timer >= updateInterval)
        {
            timer = 0f;

            if(normalSprite)
            {
                Texture2D normal_tex = ConvertSpriteToTexture2D(normalSprite);

                // Destroy the previous texture to prevent memory leaks
                if (currentNormalTexture != null)
                {
                    Destroy(currentNormalTexture);
                }

                currentNormalTexture = normal_tex;

                sr.material.SetTexture("_MainNormal", currentNormalTexture);
            }
        }
    }

    // ============================================================================

    Texture2D ConvertSpriteToTexture2D(Sprite sprite)
    {
        // Create a new Texture2D with the same dimensions as the sprite's rect
        Texture2D texture = new((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);

        // Get the pixels from the sprite's texture
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height);

        // Set the pixels to the new texture
        texture.SetPixels(pixels);
        texture.Apply(); // Apply changes to the texture

        return texture;
    }

    // ============================================================================

    // Store the current normal texture
    Texture2D currentNormalTexture; 

    // Cleanup on destroy
    void OnDestroy()
    {
        if(currentNormalTexture != null)
        {
            Destroy(currentNormalTexture);
        }
    }
}
