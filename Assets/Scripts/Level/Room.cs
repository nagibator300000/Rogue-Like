using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public GameObject top_door;
    [SerializeField] public GameObject right_door;
    [SerializeField] public GameObject left_door;
    [SerializeField] public GameObject bottom_door;
    public bool isTopFree = true;
    public bool isLeftFree = true;
    public bool isRightFree = true;
    public bool isBottomFree = true;
}
