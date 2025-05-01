using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float horizontalMove = 0.25f, verticalMove = 0.5f;
    private bool collided;

    public int score;
    public GameObject scoreText;

    public GameObject ufo;
    public int ufoCount = 0;
    public int timesCollided = 0;
    private float ufoHorizontal = 0.4f;
    
    float time = 0.0f;

    private void Start()
    {
        //methodName, interval, rate
        InvokeRepeating("MoveEnemy",1f,1f);
    }

    void Update()
    {

        int randTime = Random.Range(10, 20);
        if (time > randTime)
        {
            UfoSpawner();
            time = 0;
        }
        time += Time.deltaTime;
    }

    private void MoveEnemy()
    {
        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Vector3 currentPos = enemy.transform.position;
            enemy.transform.position = currentPos + new Vector3(horizontalMove,0,0);
        }
    }

    private async void UfoSpawner()
    {
        await Task.Delay(20000);
        if (ufoCount < 1)
        {
            int powerChance = Random.Range(0, 6);
            float x = -13.8f;
            float y = 7.4f;

            if (powerChance == 1)
            {
                Debug.Log("it is in if struc");
                Instantiate(ufo, new Vector3(x, y), ufo.transform.rotation);
                ufoCount++;
                InvokeRepeating("UfoMover", 1f, 0.06f);
            }
        }
    }

    private void UfoMover()
    {
        
        foreach (var ufo in GameObject.FindGameObjectsWithTag("UFO"))
        {
            Vector3 currentPos = ufo.transform.position;
            ufo.transform.position = currentPos + new Vector3(ufoHorizontal, 0, 0);
        }
    }

    public void Scoring(int scr)
    {
        score += scr;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    private async void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy" && !collided)
        {
            collided = true;
            horizontalMove *= -1;

            foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Vector3 currentPos = enemy.transform.position;
                enemy.transform.position = currentPos - new Vector3(0,verticalMove,0);
            }

            await Task.Delay(2000);
            collided = false;
        }
        else if(other.gameObject.tag == "UFO")
        {
            ufoHorizontal *= -1;
            timesCollided++;
            if(timesCollided >= Random.Range(1,6))
            {
                Destroy(other.gameObject);
                ufoCount = 0;
                timesCollided = 0;
            }
        }
    }
}
