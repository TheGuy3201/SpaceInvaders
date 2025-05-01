using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float playerSpeed;
     
    public GameObject laserBeam;
    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        Vector2 newVelocity = new Vector2(horizontalMove, 0);
        GetComponent<Rigidbody2D>().velocity = newVelocity * playerSpeed;
        if (Input.GetAxis("Fire1") > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (GameObject.FindGameObjectsWithTag("Laser").Length == 0)
        {
            GameObject goObj;
            goObj = GameObject.Instantiate(laserBeam, new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y + 2, GetComponent<Transform>().position.z), transform.rotation);
            goObj.transform.Rotate(0.0f, 0.0f, 90.0f);
        }

    }
}
