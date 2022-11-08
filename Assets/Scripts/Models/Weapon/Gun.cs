using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    private Player _player;
    
    public event Action<Bullet> BulletCreated;

    public Gun(Player player)
    {
        _player = player;
    }

    public void Shoot(Space space)
    {
        Bullet bullet = new Bullet(_player,_player.Position,_player.Direction, space.Diagonal);
        BulletCreated?.Invoke(bullet);
    }
}
