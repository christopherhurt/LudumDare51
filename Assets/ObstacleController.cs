using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private readonly float DELETION_Y_OFFSET = 30.0f;
    private readonly float DELETION_X_OFFSET = 30.0f;

    public Camera cam;
    public float initSpeed;
    public Sprite[] spriteOptions;

    public bool isOriginal = true;
    public int id;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.0f, initSpeed);

        // Randomly assign a sprite
        Sprite sprite = spriteOptions[(int)Random.Range(0.0f, spriteOptions.Length - 0.00001f)];
        GetComponent<SpriteRenderer>().sprite = sprite;

        id = Random.Range(int.MinValue, int.MaxValue);
    }

    void Update()
    {
        // Destroy when off bottom or side of screen
        if (!isOriginal && (transform.position.y < cam.transform.position.y - DELETION_Y_OFFSET || transform.position.x > DELETION_X_OFFSET || transform.position.x < -DELETION_X_OFFSET))
        {
            Destroy(this.gameObject);
        }
        
        // Expensive, but check each update for new backgrounds and ignore collisions with background barriers
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Collider2D[] bgColliders = backgrounds[i].GetComponents<Collider2D>();
            for (int j = 0; j < bgColliders.Length; j++)
            {
                Physics2D.IgnoreCollision(bgColliders[j], GetComponent<Collider2D>());
            }
        }
    }

}
