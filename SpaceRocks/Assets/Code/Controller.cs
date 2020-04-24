using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

/*TODO
 * Set room size so we know the bounds were working with-
 * Waves of Rocks-
 * Rocks spawn at room edge and move in-
 * Score for destroying-
 * Rocks have random velocity - they should be faster when they are in smaller pieces. Three sizes, spawn with random size when hit break into two smaller sizes with some velocity? elastic collision?
 * add to score when rocks are destroyed-
 * Room Wrapping-
 * Ship is cone shaped
 * Ship lives-
 * Rocks spawn as different sizes and break into smaller versions-
 * Scoreboard-
 * The entire game
 * Get some better sprites
 * Collisions-
 * Escape causes quit-
 * Split rock destroyed into a wave function and a score function-
 * Ships handle their own lives, controller handles score and rocks handle reporting their demise when they are destroyed to the controller-
 * When a rock is created, it 
 */

//Its a class not a script
public class Controller : MonoBehaviour
{
    [SerializeField] Rock rockPrefab;//create a general rock instance that the object can use to create copies
    [SerializeField] Text scoreText;// give it access to scoreboard
    [SerializeField] Text livesText;
    [SerializeField] Ship shipPrefab;
    [SerializeField] GameObject warpMessage;

    int wave = 0;
    int lives = 3;
    int spawnCount = 0;//rocks spawned in a round
    public int rocksInPlay = 0;//current number of rocks in play
    public bool rockDestroy = false;
    int score = 0;
    bool corunning;
    Ship player;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }
    /*
    IEnumerator SpawnRock()//This is a coroutine, it allows you to add a delay without needing to make a shitty deltatime frame clock
    {
        while (true)//loop for rock spawning, probably add a cap
        {
            Rock stone = Instantiate(rockPrefab);//create an instance of the rock object
            stone.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);//spawn at screen edge
            yield return new WaitForSeconds(3);
        }
    }
    */

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        if (player == null)
        {
            warpMessage.gameObject.SetActive(true);

            if (lives == 0)
            {
                NewGame();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                lives--;
                player = Instantiate(shipPrefab);
                livesText.text = "Lives: " + lives.ToString();
                warpMessage.gameObject.SetActive(false);
            }
        }      
    }

    void NewGame()
    {
        rockDestroy = true;
        score = 0;
        wave = 0;
        lives = 3;
        player = Instantiate(shipPrefab);
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();//update score
        warpMessage.gameObject.SetActive(false);
        StartCoroutine(DelayWave(wave, 3));
    }

    public void Quit()
    {
        //Application.Quit();//for builds
        UnityEditor.EditorApplication.isPlaying = false;//for editor
    }


    //update score - maybe intake rock size for variable score
    public void scoreChange()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();//update score
    }

    public void rockDestroyed()//could use an array to keep track of rocks for wave 
    {
        rocksInPlay--;
        if (rocksInPlay <= 0)
        {
            /*
            StartCoroutine(Pause(3f));
            while (paused) { }
            */
            wave++;
            StartCoroutine(DelayWave(wave, 3));
        }
    }

    IEnumerator DelayWave(int wave, float time)
    {
        if (corunning)
        {
            yield break;
        }

        corunning = true;
        yield return new WaitForSeconds(time);

        spawnCount = wave + 3;
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRock();
        }

        corunning = false;
    }

    void SpawnRock()//create a rock with random size, location and speed
    {
        rockDestroy = false;
        Rock stone = Instantiate(rockPrefab);//create an instance of the rock object
        rocksInPlay++;
        if (Random.value <= 0.5)//random position on an edge
        {
            stone.transform.position = new Vector3(9 * Mathf.Sign((float)(Random.value - 0.5)), Random.Range(-5, 5), 0);
        }
        else
        {
            stone.transform.position = new Vector3(Random.Range(-9, 9), 5 * Mathf.Sign((float)(Random.value - 0.5)), 0);//set rock position
        }
        //randomly select rock size
        stone.size = Mathf.Ceil(Random.Range(0.01f, 3));//set as 1, 2 or 3
        stone.controller = this.gameObject;//the rock must know about the controller
    }
}
