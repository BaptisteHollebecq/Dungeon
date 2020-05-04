using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { top, bottom, left, right};

public class RoomSpawner : MonoBehaviour
{
    public Direction OpeningDirection;

    private Manager template;
    private int rand;

    public bool spawned = false;

    private void Start()
    {
        template = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Manager>();
        Invoke("Spawn", 0.1f);
    }


    void Spawn()
    {
        if (!spawned)
        {
            switch (OpeningDirection)
            {
                case Direction.bottom:
                    {
                        if (template.rooms.Count < 15)
                        {
                            rand = Random.Range(1, template.BottomRooms.Length - 1);
                        }
                        else
                        {
                            rand = 0;
                        }
                        var inst = Instantiate(template.BottomRooms[rand], transform.position, Quaternion.identity);
                        template.rooms.Add(inst);
                        break;
                    }
                case Direction.top:
                    {
                        if (template.rooms.Count < 15)
                        {
                            rand = Random.Range(1, template.TopRooms.Length - 1);
                        }
                        else
                        {
                            rand = 0;
                        }
                        var inst = Instantiate(template.TopRooms[rand], transform.position, Quaternion.identity);
                        template.rooms.Add(inst);
                        break;
                    }
                case Direction.left:
                    {
                        if (template.rooms.Count < 15)
                        {
                            rand = Random.Range(1, template.LeftRooms.Length - 1);
                        }
                        else
                        {
                            rand = 0;
                        }
                        var inst = Instantiate(template.LeftRooms[rand], transform.position, Quaternion.identity);
                        template.rooms.Add(inst);
                        break;
                    }
                case Direction.right:
                    {
                        if (template.rooms.Count < 15)
                        {
                            rand = Random.Range(1, template.RightRooms.Length - 1);
                        }
                        else
                        {
                            rand = 0;
                        }
                        var inst = Instantiate(template.RightRooms[rand], transform.position, Quaternion.identity);
                        template.rooms.Add(inst);
                        break;
                    }
            }
            spawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Rooms")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "SpawnPoint")
        {
            if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(template.ClosedRooms, transform.position, Quaternion.identity);
                spawned = true;
            }
/*            else
                Destroy(gameObject);*/
        }
    }

}
