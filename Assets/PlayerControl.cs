using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator animator;
    public GameObject lookAtObject;
    public GameObject grabObject;
    public bool IKEnabled;
    public float lookAtWeight;
    public float grabWeight;
    public float rotateWeight;
    public float throwForce;
    public float torqueForce;
    public GameObject handPosition;
    // Rotate throwAngle to position of other players hand
    public GameObject otherThrowLeft;
    public GameObject otherThrowRight;
    public GameObject throwAngle;
    public Transform neck;

    private bool isThrowing;
    private bool grabbedBall;
    private Rigidbody rb_ball;
    private GameObject selectedOther;

    public GameObject ikTrigger;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb_ball = lookAtObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Vector3.Distance(transform.position, lookAtObject.transform.position) < 2)
        //{
        //IKEnabled = true;
        //}
        //else
        //{
        //IKEnabled = false;
        //}

        if((grabWeight > 0 || rotateWeight > 0) && IKEnabled == false)
        {
            grabWeight = Mathf.Lerp(grabWeight, 0, 10f * Time.deltaTime);
            rotateWeight = Mathf.Lerp(rotateWeight, 0, 5f * Time.deltaTime);
        }

        if ((grabWeight < 1 || rotateWeight < 1) && IKEnabled == true)
        {
            grabWeight = Mathf.Lerp(grabWeight, 1, 5f * Time.deltaTime);
            rotateWeight = Mathf.Lerp(rotateWeight, 1, 5f * Time.deltaTime);
        }



        if (Input.GetKeyDown(KeyCode.LeftArrow) && grabbedBall)
        {
            float prevAnlge = throwAngle.transform.rotation.eulerAngles.x;
            throwAngle.transform.LookAt(otherThrowLeft.transform);
            selectedOther = otherThrowLeft;
            throwAngle.transform.rotation = Quaternion.Euler(prevAnlge, throwAngle.transform.rotation.eulerAngles.y, throwAngle.transform.rotation.eulerAngles.z);
            animator.SetTrigger("Throws");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && grabbedBall)
        {
            float prevAnlge = throwAngle.transform.rotation.eulerAngles.x;
            throwAngle.transform.LookAt(otherThrowRight.transform);
            selectedOther = otherThrowRight;
            throwAngle.transform.rotation = Quaternion.Euler(prevAnlge, throwAngle.transform.rotation.eulerAngles.y, throwAngle.transform.rotation.eulerAngles.z);
            animator.SetTrigger("Throws");
        }

        if (grabbedBall)
        {
            lookAtObject.transform.position = handPosition.transform.position + handPosition.transform.forward * (lookAtObject.transform.localScale.x / 2);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(lookAtObject.transform.position);
        animator.SetLookAtWeight(lookAtWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, grabObject.transform.position);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, grabWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, grabObject.transform.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, grabWeight);

        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotateWeight);
        animator.SetIKRotation(AvatarIKGoal.RightHand, grabObject.transform.rotation);
    }

    public void ThrowBall()
    {
        if(grabbedBall) {
            animator.SetBool("HasBall", false);
            grabbedBall = false;
            lookAtWeight = 0.5f;
            rb_ball.useGravity = true;
            lookAtObject.GetComponent<Ball>().Throw(selectedOther.GetComponent<PlayerControl>().neck.transform);
            //rb_ball.AddForce(handPosition.transform.forward * throwForce, ForceMode.Impulse);
            //rb_ball.AddForce(throwAngle.transform.forward * throwForce, ForceMode.Impulse);
            //rb_ball.AddTorque(new Vector3(1, 1, 1) * torqueForce, ForceMode.Impulse);
        }

    }

    public void GrabBall()
    {
        rb_ball.useGravity = false;
        rb_ball.velocity = Vector3.zero;
        rb_ball.angularVelocity = Vector3.zero;
        IKEnabled = false;
        lookAtWeight = 0.2f;
        animator.SetBool("HasBall", true);
        grabbedBall = true;
        //lookAtObject.transform.position = handPosition.transform.position;
        //lookAtObject.transform.SetParent(handPosition.transform);
        //        lookAtObject.transform.position = Vector3.zero;
    }

    public void ReachForBall(bool inRange)
    {
        IKEnabled = inRange;
        //if(inRange)
        //{
        //    animator.SetTrigger("InRange");
        //}
        
    }
}

// SETTING KINEMATIC TRIGGERS TRIGGERS
