using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickController : MonoBehaviour
{
    Vector3 target;
    float currentSpeed;
    public float maxSpeed;
    [SerializeField] Animator animator;

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            currentSpeed = 0;
            animator.SetBool("isRunning", false);
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            currentSpeed = maxSpeed;
            animator.SetBool("isRunning", true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo) == true)
            {
                SetNewTarget(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
            }
        }

        Vector3 direction = target - transform.position;
        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);
    }

    void SetNewTarget(Vector3 newTarget)
    {
        target = newTarget;
        transform.LookAt(target);
    }
}
