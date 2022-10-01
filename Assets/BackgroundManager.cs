using System;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    private static readonly float Y_DIFF = 30.0f;

    public Camera cam;
    public int minObstacles;
    public int maxObstacles;
    public float obstacleXHalfRange;
    public UnityEngine.Object obstacleToSpawn;
    public int firstSpawnBackgroundIndex;

    private Boolean genNext = false;
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
            UnityEngine.Object newBackground = Instantiate(this, new Vector3(thisX, thisY + thisHeight, thisZ), Quaternion.identity);
            newBackground.GetComponent<BackgroundManager>().backgroundCounter = this.backgroundCounter + 1;

            if (backgroundCounter >= firstSpawnBackgroundIndex)
            {
                // Spawn new obstacle cars
                int numObstacles = UnityEngine.Random.Range(minObstacles, maxObstacles + 1);
                for (int i = 0; i < numObstacles; i++)
                {
                    Transform obsT = obstacleToSpawn.GetComponent<Transform>();
                    Transform newT = newBackground.GetComponent<Transform>();
                    UnityEngine.Object obs = Instantiate(obstacleToSpawn, new Vector3(newT.position.x, newT.position.y, obsT.position.z), Quaternion.identity); // TODO: set random x, y, spawn in lanes?
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
