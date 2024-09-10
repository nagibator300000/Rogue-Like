using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiCirclePolygonColider : MonoBehaviour
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private int points = 10;
    [SerializeField] private float angle = 180;
    private void Start()
    {
        var angleStep= angle/points;
        var collider = GetComponent<PolygonCollider2D>();
        var segments = new Vector2[points];
        for (var i = 0; i <= points - 1; i++)
        {
            float radAngle = Mathf.Deg2Rad * ((i - 1) * angleStep);
            float x =radius * Mathf.Cos(radAngle);
            float y =radius * Mathf.Sin(radAngle);
            segments[i ] = new Vector2(x,y);
        }
        collider.points = segments;
    }
}
