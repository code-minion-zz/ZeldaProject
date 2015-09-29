using UnityEngine;
using System.Collections;

public class AnimationClass : MonoBehaviour {

    enum EDirection
    {
        //Might be used as direction index to make things easier.
    };

    public Texture2D tileset;
    public int tileWidth = 10;
    public int tileHeight = 10;
    public float framesPerSecond = 6.0f;
    public AnimationGroup[] animations;

    private int currentFrame = 0;
    private float frameTime = 0;
    private SpriteClip currentClip;

    void Awake ()
    {
        if (animations.Length <= 0 || animations[0] == null)
        {
            Debug.LogWarning("The first animation of this AnimationClass has not been set. Disabling...");
            this.enabled = false;
            return;
        }
        else if (animations[0].directions.Length <= 0 || animations[0].directions == null)
        {
            Debug.LogWarning("The first SpriteClip of this AnimationClass has not been set. Disabling...");
            this.enabled = false;
            return;
        }
        //Don't forget to check for empty frames.

        currentClip = animations[1].directions[3];
    }

    void Start ()
    {
        //this is not really going to work...
        //Texture2D newTex = new Texture2D(tileWidth, tileHeight); //format has not been set (yet).
        //Color[] texPixels = tileset.GetPixels(555, 555, tileWidth, tileHeight);
        //newTex.SetPixels(texPixels);

        
    }

    void Update ()
    {
        frameTime += Time.deltaTime;

        if (framesPerSecond > 0 && frameTime > Time.deltaTime)
        {
            frameTime = 0;
            NextFrame();
        }
    }

    void SetAnimation ( int index, int direction )
    {
        currentClip = animations[index].directions[direction];
        currentFrame = 0;
        UpdateFrame();
    }

    void SetAnimation (string animationName, int direction)
    {
        //Do the index method, but select an animation by name.
        //Might be a waste of resources.
    }

    void NextFrame ()
    {
        currentFrame++;
        if (currentFrame >= currentClip.frames.Length)
            currentFrame = 0;
        
        UpdateFrame();
    }

    void UpdateFrame()
    {
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        SpriteFrame frame = currentClip.frames[currentFrame];
        Rect spriteRect = new Rect();

        spriteRect.x = frame.tileX * tileWidth;
        spriteRect.y = tileset.height - tileHeight - frame.tileY * tileHeight;
        spriteRect.width = tileWidth;
        spriteRect.height = tileHeight;

        Vector3 newScale = new Vector3(1, 1, 1);
        if (currentClip.mirror == SpriteClip.EMirrorDimensions.MirrorHorizontal || currentClip.mirror == SpriteClip.EMirrorDimensions.MirrorBoth)
        {
            //spriteRect.x += tileWidth;
            //spriteRect.width *= -1;
            newScale.x = -1;
        }
        if (currentClip.mirror == SpriteClip.EMirrorDimensions.MirrorVertical || currentClip.mirror == SpriteClip.EMirrorDimensions.MirrorBoth)
        {
            //spriteRect.y -= tileHeight;
            //spriteRect.height *= -1;
            newScale.y = -1;
        }
        transform.localScale = newScale;

        GetComponent<SpriteRenderer>().sprite = Sprite.Create(tileset, spriteRect, new Vector2(0.5f, 0.5f));
    }

}