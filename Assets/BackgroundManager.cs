using Unity.VisualScripting;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    private static readonly float Y_DIFF = 30.0f;

    public Camera cam;
    public int minObstacles;
    public int maxObstacles;
    public float ySpawnRange;
    public Object obstacleToSpawn;
    public int firstSpawnBackgroundIndex;
    public int numLanes;
    public float laneSize;

    private bool genNext = false;
    private int backgroundCounter = 0;

    void Start()
    {
        // Copy the left boundary box collider's parameters we've set up in UI to the right boundary
        BoxCollider2D[] boxes = GetComponents<BoxCollider2D>();
        boxes[1].size = boxes[0].size;
        boxes[1].offset = new Vector2(-boxes[0].offset.x, boxes[0].offset.y);
    }

    void Update()
    {
        if (!genNext && cam.transform.position.y > this.transform.position.y - Y_DIFF)
        {
            // Generate next background
            float thisX = this.transform.position.x;
            float thisY = this.transform.position.y;
            float thisZ = this.transform.position.z;
            float thisHeight = this.GetComponent<SpriteRenderer>().bounds.size.y;
            Object newBackground = Instantiate(this, new Vector3(thisX, thisY + thisHeight, thisZ), Quaternion.identity);
            newBackground.GetComponent<BackgroundManager>().backgroundCounter = backgroundCounter + 1;

            if (newBackground.GetComponent<BackgroundManager>().backgroundCounter >= firstSpawnBackgroundIndex)
            {
                // Spawn new obstacle cars
                int numObstacles = Random.Range(minObstacles, maxObstacles + 1);
                for (int i = 0; i < numObstacles; i++)
                {
                    int lane = Random.Range(0, numLanes);
                    float lanesHalfWidth = numLanes * laneSize / 2.0f;
                    float xOff = -lanesHalfWidth + laneSize / 2.0f + lane * laneSize;
                    float yOff = Random.Range(-ySpawnRange, ySpawnRange);

                    Transform obsT = obstacleToSpawn.GetComponent<Transform>();
                    Transform newT = newBackground.GetComponent<Transform>();
                    Object obs = Instantiate(obstacleToSpawn, new Vector3(newT.position.x + xOff, newT.position.y + yOff, obsT.position.z), Quaternion.identity);
                    obs.GetComponent<ObstacleController>().isOriginal = false;
                }
            }

            genNext = true;
        }
        if (backgroundCounter != 0 && cam.transform.position.y > this.transform.position.y + Y_DIFF)
        {
            // Delete this offscreen background
            Destroy(this.gameObject);
        }
    }

}
