using UnityEngine;

public class Gun : MonoBehaviour
{
    
    [SerializeField] private Transform origin;

    private void Scope()
    {
        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        var direction = (Vector2)worldPos - (Vector2)origin.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        /*angle = Mathf.Clamp(angle, 0, 45);*/
        origin.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private void Update()
    {
        Scope();
    }
}
