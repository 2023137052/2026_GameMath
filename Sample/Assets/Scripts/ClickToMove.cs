using UnityEngine;
using UnityEngine.InputSystem;
public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 mouseScreenPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;


    public void Onpoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>();
    }

    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != gameObject)
                {


                    targetPosition = hit.point;
                    targetPosition.y = transform.position.y;
                    isMoving = true;

                    break;
                }
            }
        }
    }


    void Update()
    {
        if (isMoving)
        {
            Vector3 dir = targetPosition - transform.position;
            float length = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y * dir.z * dir.z);
            Vector3 moveDir = new Vector3(dir.x / length, dir.y / length, dir.z / length);
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            if (length <= moveSpeed * Time.deltaTime)
            {
                isMoving = false;
            }
        }
    }
}