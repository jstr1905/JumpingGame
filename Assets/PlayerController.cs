using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f;


    private float zOffset = 0f;
    [SerializeField] private GameObject[] environmentObjects;
    private GameObject environmentObject;
    private Vector3 spawnEnvironmentLeftPosition;
    private Vector3 spawnEnvironmentRightPosition;
    private int numberofEnvironmentObjects = 40;
    private int spawnedEnvironmentObjects = 0;


    [SerializeField] private GameObject[] groundObjects;
    private Vector3 groundSpawnPosition;
    public int numberOfGroundObjects = 150;
    private int spawnedGroundObjects = 0;
    private float offset = 10f;

    private float yOffset = 1;


    [SerializeField] private GameObject[] clouds;
    private Vector3 cloudSpawnPosition;
    private Vector3 cloudLeftSpawnPosition;
    public int numberOfClouds = 20;
    private int spawnedClouds = 0;
    private int z = 0;


    private Animator _animator;
    private float gravity = -50f;
    private CharacterController _characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float horizontalInput;


    private SceneManager _sceneManager;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        groundSpawnPosition = transform.position;

        // spawnEnvironmentLeftPosition = new Vector3(-2, 2, 60); // Örneğin
        // spawnEnvironmentRightPosition = new Vector3(10, 2, 60); // Örneğin
        //
        cloudSpawnPosition = new Vector3(2, 2, 2);
        cloudLeftSpawnPosition = new Vector3(2, 2, 2);
    }

    
    void Update()
    {
        if (spawnedGroundObjects < numberOfGroundObjects)
        {
            SpawnGroundObject();
            spawnedGroundObjects++;
        }


        if (spawnedEnvironmentObjects < numberofEnvironmentObjects)
        {
            SpawnNextEnvironmentObject();
            spawnedEnvironmentObjects++;
        }

        if (spawnedClouds < numberOfClouds)
        {
            SpawnCloud();
            spawnedClouds++;
        }


        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            _animator.SetBool("isJumping", false);
            _animator.SetFloat("speed", Mathf.Abs(horizontalInput));
        }

        _characterController.Move(new Vector3(0, 0, horizontalInput * runSpeed) * Time.deltaTime);

        if (isGrounded && Input.GetButton("Jump"))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            _animator.SetBool("isJumping", true);
        }


        //Vertical velocity
        _characterController.Move(velocity * Time.deltaTime);
    }

    void SpawnCloud()
    {
        Vector3 cloudLeftSpawnPosition = new Vector3(Random.Range(-30, -15), Random.Range(8, 12), z);
        Vector3 cloudSpawnPosition = new Vector3(Random.Range(25, 40), Random.Range(8, 13), z);
        int randomCloudIndex = Random.Range(0, clouds.Length);
        Instantiate(clouds[randomCloudIndex], cloudSpawnPosition, Quaternion.identity);
        Instantiate(clouds[randomCloudIndex], cloudLeftSpawnPosition, Quaternion.identity);
        z += 25;
    }


    void SpawnNextEnvironmentObject()
    {
        Vector3 spawnEnvironmentLeftPosition = new Vector3(Random.Range(-6, -7), 0, zOffset);
        Vector3 spawnEnvironmentRightPosition = new Vector3(Random.Range(9, 12), 0, zOffset);


        int randomIndex = Random.Range(0, environmentObjects.Length);
        Instantiate(environmentObjects[randomIndex], spawnEnvironmentLeftPosition, Quaternion.identity);
        Instantiate(environmentObjects[randomIndex], spawnEnvironmentRightPosition, Quaternion.identity);
        zOffset += 3f;
    }

    void SpawnGroundObject()
    {
        Vector3 groundObjectPosition = new Vector3(0, yOffset, offset);
        int randomGroundIndex = Random.Range(0, groundObjects.Length);
        Instantiate(groundObjects[randomGroundIndex], groundObjectPosition, Quaternion.identity);
        offset += Random.Range(-1, 7);
        yOffset += Random.Range(0, 1);
    }

     
}