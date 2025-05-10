using UnityEngine;
using System.Collections;
public class Movement : MonoBehaviour
{

    Rigidbody rb;
    public float force = 5f;
    public Transform cameraY;
    public float maxSpeed = 5f;
    AttackingScript attackingScript;
    public bool canMakeIdle = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attackingScript = GetComponent<AttackingScript>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraY.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            if (attackingScript.IsAnimationPlaying == false && attackingScript.anim.isPlaying == false)
            {
                attackingScript.moveAnim();
                canMakeIdle = true;
            }
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
            moveDirection += transform.right;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= transform.right;

        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection.normalized * force);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        else
        {
            StartCoroutine(WaitAndPrint());
        }
    }

    IEnumerator WaitAndPrint()
    {
        
        yield return new WaitForSeconds(0.19f);
        if (canMakeIdle && !(Input.anyKey))
        {
            canMakeIdle = false;
            attackingScript.IdleAnim();

        }
        rb.linearVelocity = Vector3.zero;

    }


}
