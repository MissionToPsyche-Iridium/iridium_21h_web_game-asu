using Unity.VisualScripting;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float scroll_speed = 0.1f;
    public float scroll_duration = 5f;
    public LineRenderer myLineRenderer;
    public int points = 100;
    public float amplitude = 1f;
    public float frequency = 1f;
    public float yStart = 10f;
    public float yEnd = -10f;
    public float width = 5f;
    public float waveScrollSpeed = 1f;
    public float colliderWidth = 0.1f;

    public GameObject sineWaveColliderObject;
    public GameOverScreen GameOverScreen;
    public WelcomeScreen WelcomeScreen;
    public CongratScreen CongratScreen;
    public Bar progressBar;

    private bool isPaused = false;
    private float pausedTimer;
    private float pausedScrollTime;

    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab;
    public float asteroidSpawnInterval = 7f;
    public float asteroidSpeed = 5f;
    public float asteroidYOffset = 0.5f;

    private MeshRenderer mesh_Render;
    private float y_Scroll;
    private float timer;
    private bool isScrolling = false;
    private EdgeCollider2D edgeCollider;
    private float nextAsteroidSpawnTime;
    private GameObject currentAsteroid;
    private float timeOffset = 0f;

    [Header("Asteroid Movement")]
    public float asteroidRotationSpeed = 15f;  // Degrees per second
    public float asteroidRotationVariation = 5f;  // Random variation
    public float asteroidDriftAmplitude = 0.15f;  // How far it drifts from center
    public float asteroidDriftFrequency = 0.5f;  // How quickly it drifts

    private float currentAsteroidRotationSpeed;
    private float asteroidTimeOffset;


    void Awake()
    {
        mesh_Render = GetComponent<MeshRenderer>();
        myLineRenderer = GetComponent<LineRenderer>();

        myLineRenderer.sortingLayerName = "SineWave";
        myLineRenderer.sortingOrder = 0;

        if (sineWaveColliderObject != null)
        {
            var existingPolygon = sineWaveColliderObject.GetComponent<PolygonCollider2D>();
            if (existingPolygon != null)
            {
                Destroy(existingPolygon);
            }

            edgeCollider = sineWaveColliderObject.GetComponent<EdgeCollider2D>();
            if (edgeCollider == null)
            {
                edgeCollider = sineWaveColliderObject.AddComponent<EdgeCollider2D>();
            }
            edgeCollider.edgeRadius = colliderWidth / 2;
        }
        else
        {
            Debug.LogError("sineWaveColliderObject is not assigned. Please set it in the Inspector.");
        }
    }

    void Update()
    {
        if (isScrolling && !isPaused)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                Scroll();
                DrawWave();
                UpdateCollider();
                UpdateAsteroidSpawning();
            }
            else
            {
                isScrolling = false;
                DestroyCurrentAsteroid();
            }
        }
    }

    void DrawWave()
    {
        float Tau = 2 * Mathf.PI;
        myLineRenderer.positionCount = points;
        float yLength = yStart - yEnd;
        float spacing = yLength / (points - 1);

        float timeValue = Time.timeSinceLevelLoad - timeOffset;

        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float y = yStart - (currentPoint * spacing);
            float x = amplitude * Mathf.Sin(
                (Tau * frequency * (y / yLength)) +
                (timeValue * waveScrollSpeed)
            );
            x = (x * width) / 2;
            myLineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    void UpdateCollider()
    {
        if (edgeCollider == null) return;

        Vector2[] colliderPoints = new Vector2[points];
        for (int i = 0; i < points; i++)
        {
            Vector3 linePoint = myLineRenderer.GetPosition(i);
            colliderPoints[i] = new Vector2(linePoint.x, linePoint.y);
        }

        edgeCollider.points = colliderPoints;
    }

    void UpdateAsteroidSpawning()
    {
        if (Time.time >= nextAsteroidSpawnTime)
        {
            SpawnAsteroid();
            nextAsteroidSpawnTime = Time.time + asteroidSpawnInterval;
        }

        if (currentAsteroid != null)
        {
            UpdateAsteroidPosition();
        }
    }

    void SpawnAsteroid()
    {
        DestroyCurrentAsteroid();

        Vector3 spawnPosition = new Vector3(0f, yStart, 0f);
        currentAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        AsteroidCollision collisionScript = currentAsteroid.GetComponent<AsteroidCollision>();
        if (collisionScript != null)
        {
            collisionScript.ResetCollisionState();
        }

        currentAsteroidRotationSpeed = asteroidRotationSpeed + Random.Range(-asteroidRotationVariation, asteroidRotationVariation); //creating random rotation speed and time offset
        asteroidTimeOffset = Random.Range(0f, 100f);
    }

    void UpdateAsteroidPosition()
    {
        if (currentAsteroid == null) return;

        //update Y position (main descent)
        float currentY = currentAsteroid.transform.position.y;
        currentY -= asteroidSpeed * Time.deltaTime;

        //calculate the base X position from the sine wave
        float Tau = 2 * Mathf.PI;
        float yLength = yStart - yEnd;
        float timeValue = Time.timeSinceLevelLoad - timeOffset;
        float baseX = amplitude * Mathf.Sin(
            (Tau * frequency * ((currentY) / yLength)) +
            (timeValue * waveScrollSpeed)
        );
        baseX = (baseX * width) / 2;

        //add the drifting motion
        float drift = Mathf.Sin((Time.time + asteroidTimeOffset) * asteroidDriftFrequency) * asteroidDriftAmplitude;
        float finalX = baseX + drift;

        //update position
        currentAsteroid.transform.position = new Vector3(finalX, currentY, 0f);

        //apply rotation
        currentAsteroid.transform.Rotate(Vector3.forward, currentAsteroidRotationSpeed * Time.deltaTime);

        //remove the asteroid when it reaches the end of the sine wave
        if (currentY <= yEnd)
        {
            DestroyCurrentAsteroid();
        }
    }

    void DestroyCurrentAsteroid()
    {
        if (currentAsteroid != null)
        {
            Destroy(currentAsteroid);
            currentAsteroid = null;
        }
    }

    void Scroll()
    {
        y_Scroll = Time.time * scroll_speed;
        Vector2 offset = new Vector2(0f, y_Scroll);
        mesh_Render.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    public void StopScrolling()
    {
        isScrolling = false;
        timer = 0;
        DestroyCurrentAsteroid();
    }

    public void PauseScrolling()
    {
        if (isScrolling && !isPaused)
        {
            isPaused = true;
            pausedTimer = timer;
            pausedScrollTime = Time.time;
        }
    }
    public void ContinueScrolling()
    {
        if (isScrolling && isPaused)
        {
            isPaused = false;
            //float timeElapsedSincePause = Time.time - pausedScrollTime;
            //timer = pausedTimer - timeElapsedSincePause;
            timeOffset += (Time.time - pausedScrollTime);
            timer = pausedTimer;
        }
    }

    public void StartBtn()
    {
        if (!isScrolling)
        {
            isScrolling = true;
            timer = scroll_duration;
            nextAsteroidSpawnTime = Time.time + asteroidSpawnInterval;
            progressBar?.ResetBar();
            GameOverScreen.Close();
            WelcomeScreen.Close();
            CongratScreen.Close();
        }
    }
}