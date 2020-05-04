using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static event System.Action ExitSelected;

    [Header("Rooms")]
    public GameObject[] BottomRooms;
    public GameObject[] TopRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;
    public GameObject ClosedRooms;
    public List<GameObject> rooms;
    [Header("Items")]
    public List<Item> Items;


    public float waitTime;
    private bool _setExit;


    private void Start()
    {
        StartCoroutine(FindExit());
    }

     IEnumerator FindExit()
    {
        yield return new WaitForSeconds(waitTime);
        rooms[rooms.Count - 1].GetComponent<Rooms>().exit = true;
        ExitSelected?.Invoke();
    }
}
