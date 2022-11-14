using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickController : MonoBehaviour
{
    Vector3 target;
    float currentSpeed;
    public float maxSpeed;
    [SerializeField] Animator animator;
    public NavMeshAgent navMeshAgent;

    [HideInInspector] public float maxDistanceFromCenter = 0.5f;

    bool isFalling;
    bool canTrip;

    void Update()
    {
        if (isFalling) return;

        if (Vector3.Distance(transform.position, target) < maxDistanceFromCenter)
        {
            currentSpeed = 0;
            if (FindObjectOfType<ARTapToPlaceObject>().isTinyMode)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            currentSpeed = maxSpeed;
            if (FindObjectOfType<ARTapToPlaceObject>().isTinyMode)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isWalking", true);
            }
        }

        //if (Input.GetMouseButtonDown(0) == true)
        //{
        //    currentSpeed = maxSpeed;
        //    animator.SetBool("isRunning", true);

        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;
        //    if (Physics.Raycast(ray.origin, ray.direction, out hitInfo) == true)
        //    {
        //        SetNewTarget(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
        //    }
        //}

        Vector3 direction = target - transform.position; //comment out when using nav mesh
        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World); //comment out when using nav mesh
    }

    public void SetNewTarget(Vector3 newTarget)
    {
        int rand = Random.Range(0, 17);
        if (rand == 1 && canTrip)
        {
            animator.SetTrigger("trip");
            StartCoroutine(FallBuffer());
        }
        else
        {
            target = newTarget;
            canTrip = true;
        }

        transform.LookAt(new Vector3 (target.x, transform.localPosition.y, target.z));  //comment out when using nav mesh
    }

    private IEnumerator FallBuffer()
    {
        isFalling = true;
        yield return new WaitForSeconds(2.5f);
        isFalling = false;
    }
}
