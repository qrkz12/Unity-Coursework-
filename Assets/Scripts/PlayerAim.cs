using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);

            Vector3 direction = point - transform.position;

            direction.y = 0f;

            if (direction.magnitude > 0.1f)
            {
                transform.forward = direction.normalized;
            }
        }
    }
}