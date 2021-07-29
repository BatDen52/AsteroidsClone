public class Space
{
    public float BrakingForce { get; private set; }
    public float Bottom { get; private set; }
    public float Top { get; private set; }
    public float Left { get; private set; }
    public float Right { get; private set; }

    public Space(float brakingForce, float bottom, float top, float left, float right)
    {
        BrakingForce = brakingForce;
        Bottom = bottom;
        Top = top;
        Left = left;
        Right = right;
    }
}
