using System;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    private static float Y_DIFF = 30.0f;

    public Camera cam;
    public int minObstacles;
    public int maxObstacles;
    public float obstacleXHalfRange;
    public UnityEngine.Object obstacleToSpawn;

    private Boolean genNext = false;
    private int backgroundCounter = 0;

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

            // Spawn new obstacle cars
            int numObstacles = UnityEngine.Random.Range(minObstacles, maxObstacles + 1);
            for (int i = 0; i < numObstacles; i++)
            {
                Transform obstacleTransform = obstacleToSpawn.GetComponent<Transform>();
                Transform newTransform = newBackground.GetComponent<Transform>();
                Instantiate(obstacleToSpawn, new Vector3(newTransform.position.x, newTransform.position.y, obstacleTransform.position.z), Quaternion.identity); // TODO: set x, y
            }

            genNext = true;
        }
        if (cam.transform.position.y > this.transform.position.y + Y_DIFF)
        {
            // Delete this offscreen background
            Destroy(this.gameObject);
        }
    }
}
