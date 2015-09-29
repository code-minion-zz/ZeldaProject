
[System.Serializable]
public class SpriteClip {

    public enum EMirrorDimensions
    {
        DontMirror,
        MirrorHorizontal,
        MirrorVertical,
        MirrorBoth
    };

    public string name = "Default";
    public SpriteFrame[] frames;
    public EMirrorDimensions mirror;

}
