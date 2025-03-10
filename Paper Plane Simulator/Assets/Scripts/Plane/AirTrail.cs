using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine;

public class AirTrail : MonoBehaviour
{
   public TrailRenderer[] windStreaks;

    private void Start()
    {
        startEmmiters();
    }

    //function to start emitting trails behind the paperplane
   private void startEmmiters()
   {
    foreach(TrailRenderer T in windStreaks)
    {
        T.emitting = true;
    }
   }

    //later trails might be ussd to visualize boosing/increases in speed
    //for now they are always on
    //function below is to stop emitting to account for future changes

    private void stopEmitting()
    {
        foreach(TrailRenderer T in windStreaks)
    {
        T.emitting = false;
    }
    }

}
