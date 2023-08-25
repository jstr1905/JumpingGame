using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    private Animator _animator;
    private float gravity = -50f;
    private CharacterController _characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float horizontalInput;

    
    
    
    public GameObject objectPrefab; // Oluşturulacak obje prefabı
    private float zOffset = 0f; // Z eksenindeki ilerleme mesafesi
    public int numberofGround = 10;
    private int spawnedGround = 0;
    
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        
    }

    private void Update()
    {
        if (spawnedGround < numberofGround)
        {
            Spawner();
            spawnedGround++;
        }
        
        
        horizontalInput = 1;

        transform.forward = new Vector3(Mathf.Abs(horizontalInput) - 1, 0, horizontalInput);

        //isGrounded
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            //Add gravity
            velocity.y += gravity * Time.deltaTime;
            
        }

        _characterController.Move(new Vector3(0, 0, horizontalInput * runSpeed) * Time.deltaTime);

        if (isGrounded && Input.GetButton("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            
        }


        //Vertical velocity
        _characterController.Move(velocity * Time.deltaTime);
    }

    void Spawner()
    {
        Instantiate(objectPrefab, new Vector3(0f, 0f, zOffset), Quaternion.identity);
        zOffset += 10;
    }
}