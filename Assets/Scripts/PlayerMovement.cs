using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed; [SerializeField] float damageForce;
    [Space]
    [SerializeField] GameObject hole; [SerializeField] GameObject particles;
    [Space]
    [SerializeField] Animator levelTransition;
    [Space]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] dropAudios;
    [SerializeField] AudioClip[] hitAudios;


    Rigidbody2D rb;
    GameObject cmCamera;

    public static int damage;
    static System.Collections.Generic.List<Vector2> holes = new System.Collections.Generic.List<Vector2>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        cmCamera = GameObject.FindGameObjectWithTag("CM Camera");
    }

    private void Start()
    {
        switch(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                holes.Clear();
                damage = 0;
                break;
        }

        for (int i = 0; i < holes.Count; i++)
        {
            Instantiate(hole, holes[i], Quaternion.identity, transform);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0;
            transform.position = new Vector3(-8, 0);
            transform.rotation = Quaternion.identity;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > damageForce && !collision.gameObject.CompareTag("Soft"))
        {
            damage++;
            Instantiate(hole, collision.GetContact(0).point, Quaternion.identity, transform);
            Instantiate(particles, collision.GetContact(0).point, Quaternion.identity);
            source.Stop();
            source.volume = 1;
            source.clip = hitAudios[Random.Range(0, hitAudios.Length)];
            source.Play();
        } else
        {
            if (source.isPlaying) return;
            source.volume = 0.8f;
            source.clip = dropAudios[Random.Range(0, dropAudios.Length)];
            source.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Win"))
        {
            SaveHoles();
            levelTransition.SetTrigger("LevelOver");
        }
    }

    void SaveHoles()
    {
        cmCamera.SetActive(false);
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0;
        transform.position = new Vector3(-8, 0);
        transform.rotation = Quaternion.identity;

        holes.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            holes.Add(new Vector2() { x = transform.GetChild(i).position.x, y = transform.GetChild(i).position.y });
        }

        rb.isKinematic = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
}
