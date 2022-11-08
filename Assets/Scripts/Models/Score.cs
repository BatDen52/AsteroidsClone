using System;

public class Score
{
    private int _value;

    public event Action<int> ValueCanged;

    public Score()
    {
        _value = 0;
        ValueCanged?.Invoke(_value);
    }

    public void AddValue(int reward)
    {
        _value += reward;
        ValueCanged?.Invoke(_value);
    }
}
