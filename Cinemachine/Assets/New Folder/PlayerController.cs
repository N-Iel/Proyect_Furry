using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float moveSpeed,
        turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("trapCam"))
        {
            other.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 99;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("trapCam"))
        {
            other.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
    }
}
