using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform campoz;
    public Transform pivot;
    public Transform Charackters;
    public Transform mTransform;

    public float smothX;
    public float smothY;
    public charackterStatus cs;
    public cameraConfim cc;
    public bool leftPivot;
    public float delta;
    public float mouseX;
    public float mouseY;
    public float smoothXVelocity;
    public float smoothYVelocity;
    public float lookAngle;
    public float titlAngle;


    void FixedUpdate()
    {
      FixedTick();
    }

    void FixedTick()
    {
      delta = Time.deltaTime;
      HandlePoz();
      HandleRot();

      Vector3 targetPoz = Vector3.Lerp(mTransform.position,Charackters.position,1);
      mTransform.position = targetPoz;
    }

    void HandlePoz()
    {
      float tragetX = cc.normalX;
      float tragetY = cc.normalY;
      float tragetZ = cc.normalZ;

    }

    void HandleRot()
    {
      mouseY = Input.GetAxis("Mouse Y");
      mouseX = Input.GetAxis("Mouse X");

      if(cc.turnSmooth > 0)
      {
        smothX = Mathf.SmoothDamp(smothX,mouseX, ref smoothXVelocity, cc.turnSmooth);
        smothY = Mathf.SmoothDamp(smothY,mouseY, ref smoothYVelocity, cc.turnSmooth);
      }
      else
      {
        smothX = mouseX;
        smothY = mouseY;
      }


      lookAngle += smothX * cc.y_rot;
      Quaternion targetRot = Quaternion.Euler(0,lookAngle,0);
      mTransform.rotation = targetRot;

      titlAngle -= smothY * cc.y_rot;
      titlAngle = Mathf.Clamp(titlAngle,cc.minAngle,cc.maxAangle);
      pivot.localRotation = Quaternion.Euler(titlAngle,0,0);
    }
}
