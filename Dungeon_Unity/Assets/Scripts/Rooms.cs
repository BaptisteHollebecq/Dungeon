using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public List<Direction> possibleMove;
    public List<Item> Items;
    public List<Enemy> Enemies;
    [Header("debug")]
    public bool init = true;
    public bool exit = false;
    public bool bed = false;

    private void Start()
    {
        if (init)
        {
            if (Random.Range(0, 101) % 2 == 0)
            {
                Enemies.Add(new Enemy(Random.Range(5, 15), Random.Range(5, 15)));
            }
            if (Random.Range(0,101) < 30)
            {
                bed = true;
            }
        }
    }
}
