using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    public Transform pointer;
    public Transform pointerModel;
    public bool rotatePointer = true;

    void Update()
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Cancel model rotation
        if (!rotatePointer)
        {
            pointerModel.transform.rotation = Quaternion.Euler(pointer.transform.rotation.x, pointer.transform.rotation.y, -pointer.transform.rotation.z);
        }
    }
}
