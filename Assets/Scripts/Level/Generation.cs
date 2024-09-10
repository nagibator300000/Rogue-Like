using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generation : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private Vector2 rooms_gap;
    private List<Room> rooms = new List<Room>();
    private int rooms_num = 0 ;

    private void Awake()
    {
        rooms_num= Random.Range(10,20);
        Generate();
    }
    private void Generate()
    {
        var start_pos = Vector3.zero;
        CreateRoom(start_pos);
        for (int i = 0; i < rooms_num; i++)
        {
            var current_room = new Room();
            var room_direction = Vector2.zero;
            while (room_direction == Vector2.zero)
            {
                current_room = rooms[Random.Range(0, rooms.Count)];
                room_direction = ChooseDirection(current_room);
            }

            
            var new_room_pos = new Vector2(current_room.transform.position.x, current_room.transform.position.y) + room_direction * rooms_gap;
            CreateRoom(new_room_pos);
        }
    }

    private void CreateRoom(Vector3 pos)
    {
        Debug.Log(pos);
        var room_obj = Instantiate(room, pos, Quaternion.identity); 
        var new_room = room_obj.GetComponent<Room>();
        rooms.Add(new_room);
    }

    private Vector2 ChooseDirection(Room room)
    {
        if (room.isBottomFree)
        {
            return Vector2.down;
        }
        if (room.isTopFree)
        {
            return Vector2.up;
        }
        if (room.isRightFree)
        {
            return Vector2.right;
        }
        if (room.isLeftFree)
        {
            return Vector2.left;
        }

        return Vector2.zero;
    }
}
