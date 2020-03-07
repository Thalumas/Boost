using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustPower = 120f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] AudioClip engine;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip levelComplete;

    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem completeParticles;

    private Transform initialTransform;
    private Vector3 thrustVector;
    [SerializeField] int level;

    enum State { ALIVE, DIEING, TRANSITIONING};
    State state = State.ALIVE;

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        initialTransform = transform;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        thrustVector = Vector3.up * thrustPower;
    }

    // Update is called once per frame
    void Update() {
        if (state == State.ALIVE) {
            HandleThrust();
            HandleRotation();
        }
    }

    private void HandleThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(thrustVector);
            if (!leftEngineParticles.isPlaying) {
                leftEngineParticles.Play();
            }
            if (!rightEngineParticles.isPlaying) {
                rightEngineParticles.Play();
            }
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(engine);
            }
        } else {
            audioSource.Stop();
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
    }

    private void HandleRotation() {

        if (Input.GetKey(KeyCode.LeftArrow) & !Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) & !Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
        }
   }

    public void OnCollisionEnter(Collision collision) {
        if (state == State.ALIVE) {
            switch (collision.gameObject.tag) {
                case "Safe":
                    print("Safe collision");
                    break;
                case "LandingPad":
                    audioSource.Stop();
                    leftEngineParticles.Stop();
                    rightEngineParticles.Stop();
                    audioSource.PlayOneShot(levelComplete);
                    completeParticles.Play();
                    state = State.TRANSITIONING;
                    Invoke("LevelComplete", 2f);
                    break;
                default:
                    state = State.DIEING;
                    audioSource.Stop();
                    audioSource.PlayOneShot(explosion);
                    deathParticles.Play();
                    Invoke("Die", 1f);
                    break;
            }
        }
    }

    private void LevelComplete() {
        SceneManager.LoadScene(level + 1);
    }

    private void Die() {
        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
        SceneManager.LoadScene(level);
    }
}
