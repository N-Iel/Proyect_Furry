using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    public Transform pointer;
    public Transform pointerModel;
    public GameObject weapon;
    public Vector2 weaponLookingDir;
    Vector2 storedWeaponLookingDir;
    public bool rotatePointer = true;

    private void Start()
    {
        weaponLookingDir = Vector2.zero;
        storedWeaponLookingDir = Vector2.zero;
    }

    void Update()
    {
        LookAtMouse();
        if (storedWeaponLookingDir != weaponLookingDir && Mathf.Sign(storedWeaponLookingDir.x) == Mathf.Sign(weaponLookingDir.x * -1)) FlipWeapon();
    }

    void LookAtMouse()
    {
        weaponLookingDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(weaponLookingDir.y, weaponLookingDir.x) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Cancel model rotation
        if (!rotatePointer)
        {
            pointerModel.transform.rotation = Quaternion.Euler(pointer.transform.rotation.x, pointer.transform.rotation.y, -pointer.transform.rotation.z);
        }
    }

    public void FlipWeapon()
    {
        // Update storedDir
        storedWeaponLookingDir = weaponLookingDir;

        // Flip Model
        Vector3 buffer = weapon.transform.localScale;
        buffer.x = Mathf.Sign(storedWeaponLookingDir.x);
        weapon.transform.localScale = buffer;
    }
}
