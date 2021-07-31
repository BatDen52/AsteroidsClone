using System;
using System.Timers;
using UnityEngine;

public class Spawner
{
    private System.Random _random;
    private Space _space;
    private Player _player;

    private float _delay;
    private int _minDelay;
    private int _maxDelay;

    private float _astroidChance;
    private float _fragmentChance;
    private float _ufoChance;

    public event Action<DangerousObject> Spawned;

    public Spawner(Player player,Space space, int minDelay, int maxDelay)
    {
        _player = player;
        _space = space;
        _minDelay = minDelay;
        _maxDelay = maxDelay;
        _astroidChance = 0.5f;
        _fragmentChance = 0.2f;
        _ufoChance = 0.3f;
    }

    public void Enabled()
    {
        _random = new System.Random();
        _delay = _random.Next(_minDelay, _maxDelay);
    }

    public void Disable()
    {
    }

    public void Spawn(float deltaTime)
    {
        _delay -= deltaTime;
        if (_delay > 0)
            return;

        Side side = (Side)_random.Next(Enum.GetValues(typeof(Side)).Length);
        Vector2 position = GetRandomPosition(side);
        Vector2 direction = GetRandomDirection(side);
        DangerousObject dangerousObject = GetRandomDangerousObject(position, direction);

        Spawned?.Invoke(dangerousObject);

        _delay = _random.Next(_minDelay, _maxDelay);
    }

    private DangerousObject GetRandomDangerousObject(Vector2 position, Vector2 direction)
    {
        float chance = (float)_random.NextDouble();

        if (chance < _astroidChance)
            return new Asteroid(_player, position, direction, _space.Diagonal);
        else if (chance < _astroidChance + _fragmentChance)
            return new Fragment(_player, position, direction, _space.Diagonal);
        else
            return new UFO(_player, position, direction, _space.Diagonal);
    }

    private Vector2 GetRandomPosition(Side side)
    {
        switch (side)
        {
            case Side.Top:
                return new Vector2(GetRandomWithRoundRange(_space.Left, _space.Right), _space.Top);
            case Side.Left:
                return new Vector2(_space.Left, GetRandomWithRoundRange(_space.Bottom, _space.Top));
            case Side.Bottom:
                return new Vector2(GetRandomWithRoundRange(_space.Left, _space.Right), _space.Bottom);
            case Side.Right:
                return new Vector2(_space.Right, GetRandomWithRoundRange(_space.Bottom, _space.Top));
        }
        return default;
    }

    private Vector2 GetRandomDirection(Side side)
    {
        switch (side)
        {
            case Side.Top:
                return new Vector2(GetSignFloat(), -(float)_random.NextDouble());
            case Side.Left:
                return new Vector2((float)_random.NextDouble(), GetSignFloat());
            case Side.Bottom:
                return new Vector2(GetSignFloat(), (float)_random.NextDouble());
            case Side.Right:
                return new Vector2(-(float)_random.NextDouble(), GetSignFloat());
        }
        return default;
    }

    private float GetSignFloat() => _random.Next(-100, 100) / (float)100;

    private int GetRandomWithRoundRange(float minValue, float maxValue) =>
        _random.Next((int)Math.Floor(minValue), (int)Math.Ceiling(maxValue));

    enum Side { Top, Left, Bottom, Right }
}
