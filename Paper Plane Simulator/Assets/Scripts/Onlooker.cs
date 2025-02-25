using UnityEngine;

public class Onlooker : MonoBehaviour 
{
  [Header("Variables")]
  [SerializeField] float headTrackingSpeed;
  [SerializeField] float headMaxTurnAngle;

  [Header("Targets")]
  [SerializeField] Transform target;
  [SerializeField] Transform lookObject;

  [Header("Bones")]
  [SerializeField] Transform headBone;
  
  // All procedural animation code goes in LateUpdate.
  // This allows other systems to update the environment first, 
  // I.e. the animation system gets to adapt to it before the frame is drawn.
  void LateUpdate()
  {
    // Store the current head rotation since we will be resetting it
    Quaternion currentLocalRotation = headBone.localRotation;
    // Reset the head rotation so our world to local space transformation will use the head's zero rotation. 
    // Quaternion.Identity is the quaternion equivalent of 0
    headBone.localRotation = Quaternion.identity;

    Vector3 targetWorldLookDir = target.position - headBone.position;
    Vector3 targetLocalLookDir = headBone.InverseTransformDirection(targetWorldLookDir);

    // Apply angle limit
    targetLocalLookDir = Vector3.RotateTowards(Vector3.forward,targetLocalLookDir, Mathf.Deg2Rad * headMaxTurnAngle, 0);

    // Get the local rotation by using LookRotation on a local directional vector
    Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

    // Apply smoothing
    headBone.localRotation = Quaternion.Slerp(currentLocalRotation, targetLocalRotation, 1 - Mathf.Exp(-headTrackingSpeed * Time.deltaTime));
  }
}

