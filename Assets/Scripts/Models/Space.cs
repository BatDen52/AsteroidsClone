using System;

public class Space
{
    public float BrakingForce { get; private set; }
    public float Bottom { get; private set; }
    public float Top { get; private set; }
    public float Left { get; private set; }
    public float Right { get; private set; }
    public float Diagonal => (float)Math.Sqrt(Math.Pow(Top-Bottom,2)+ Math.Pow(Left-Right,2)); 

    public Space(float brakingForce, float bottom, float top, float left, float right)
    {
        BrakingForce = brakingForce;
        Bottom = bottom;
        Top = top;
        Left = left;
        Right = right;
    }
}
