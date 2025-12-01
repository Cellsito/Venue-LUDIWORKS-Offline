using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 10f;
    private Rigidbody2D playerRigidBody;
    private Vector2 moveInput;
    private Vector2 moveVelocity;


    [Header("Footstep Settings")]
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;

    public float stepInterval = 0.35f;
    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;

    private float stepTimer = 0f;
    private bool wasWalking = false;

        
    public Transform playerPosition;
    //public Animator animator;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        // Carrega posição salva
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "gameTrello" &&
            PlayerPrefs.HasKey("PlayerX") &&
            PlayerPrefs.HasKey("PlayerY"))
        {
            StartCoroutine(ApplySavedPosition());
        }
    }

    private System.Collections.IEnumerator ApplySavedPosition()
    {
        yield return new WaitForSeconds(0.1f);

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        playerPosition.position = new Vector2(x, y);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;
        moveVelocity = playerSpeed * moveInput;

        // Atualiza animações
        //if (animator != null)
        //{
        //    animator.SetFloat("MoveX", moveX);
        //    animator.SetFloat("MoveY", moveY);
        //    animator.SetFloat("Speed", moveInput.sqrMagnitude);
        //}

        bool isWalking = moveInput.sqrMagnitude > 0.1f;

        if (isWalking)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            if (wasWalking && footstepSource.isPlaying)
                footstepSource.Stop();

            stepTimer = 0f;
        }

        wasWalking = isWalking;
    }

    private void FixedUpdate()
    {
        if (playerRigidBody == null) return;

        // Movimento correto offline
        playerRigidBody.linearVelocity = moveVelocity;
    }

    // ============================================
    //              FOOTSTEP PLAYER
    // ============================================
    private void PlayFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0)
            return;

        int index = Random.Range(0, footstepClips.Length);

        footstepSource.pitch = Random.Range(pitchMin, pitchMax);

        if (footstepSource.isPlaying)
            footstepSource.Stop();

        footstepSource.clip = footstepClips[index];
        footstepSource.Play();
    }
}