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
    public GameObject handPosition;
    public GameObject throwAngle;

    private bool isThrowing;
    private Rigidbody rb_ball;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb_ball = lookAtObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Throws");
            isThrowing = true;
            rb_ball.isKinematic = true;
        }

        if (isThrowing)
        {
            lookAtObject.transform.position = handPosition.transform.position;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (IKEnabled)
        {
            animator.SetLookAtPosition(lookAtObject.transform.position);
            animator.SetLookAtWeight(lookAtWeight);

            // animator.SetIKPosition(AvatarIKGoal.LeftHand, grabObject.transform.position);
            // animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, grabWeight);

            animator.SetIKPosition(AvatarIKGoal.RightHand, grabObject.transform.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, grabWeight);

            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotateWeight);
            animator.SetIKRotation(AvatarIKGoal.RightHand, grabObject.transform.rotation);
        }
    }

    public void ThrowBall()
    {
        isThrowing = false;
        rb_ball.isKinematic = false;
        // rb_ball.AddForce(handPosition.transform.forward * throwForce, ForceMode.Impulse);
        rb_ball.AddForce(throwAngle.transform.forward * throwForce, ForceMode.Impulse);

    }
}
