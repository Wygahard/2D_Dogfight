using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Plane2 : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 1;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Vector3 inputV = new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
            Vector3 inputH = new Vector3(0, 0, Input.GetAxisRaw("Horizontal"));

            transform.Translate(inputV.normalized * speed * Time.deltaTime);
            transform.Rotate(inputH.normalized * -rotationSpeed * Time.deltaTime);
        }
        
    }
}
