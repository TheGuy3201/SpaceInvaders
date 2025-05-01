using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletTraveler : MonoBehaviour
{
    public float beamSpeed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, beamSpeed);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("UFO"))
        {
            if(string.Compare(other.gameObject.name,0, "DepressedAlien",0,14) == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Scoring(10);
            }
            else if (string.Compare(other.gameObject.name, 0, "KrabAlien", 0, 9) == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Scoring(20);
            }
            else if (string.Compare(other.gameObject.name, 0, "SquidAlien", 0, 10) == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Scoring(30);
            }
            else if (other.gameObject.CompareTag("UFO"))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Scoring(Random.Range(1,7)*50);
                GameObject.Find("GameManager").GetComponent<GameManager>().ufoCount = 0;
            }
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Base"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);

    }
}
