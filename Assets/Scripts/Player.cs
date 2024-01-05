using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;

    public float moveSpeed = 30f ;
    public CharacterController characterController;
    public float gravity = -9.8f;
    public float jumpSpeed = 5f;

    public float timer = 0f;

    public Transform shotOrigin;

    public float health;
    public float maxHealth = 100f;

    private float vertVel = 0f;

    Vector2 _direction;

    private bool[] inputs;

    private void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        jumpSpeed *= Time.fixedDeltaTime;
        moveSpeed *= Time.fixedDeltaTime;
    }


    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;

        inputs = new bool[5];
        health = maxHealth;
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if(health <= 0f)
        {
            return;
        }



       //if (timer >= 0.2f)
       //{
       //    if (!_direction.Equals(Vector2.zero))
            //{
                _direction = Vector2.zero;
       //     }
       //
       //     timer = 0f;
       // }
        if (inputs[0])
        {
            
            _direction.y = 1;
        }
        if (inputs[1])
        {
            _direction.x = -1;
        }
        if (inputs[2])
        {
            _direction.y += -1;
        }
        if (inputs[3])
        {
            _direction.x += 1;
        }

        Move(_direction);
    }

    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move(Vector2 _inputDirection)
    {
        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        _moveDirection *= moveSpeed;

        if (characterController.isGrounded)
        {
            vertVel = 0f;
            if (inputs[4])
            {
                vertVel = jumpSpeed;
            }
        }

        vertVel += gravity;

        _moveDirection.y = vertVel;
        characterController.Move(_moveDirection);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    /// <summary>Updates the player input with newly received input.</summary>
    /// <param name="_inputs">The new key inputs.</param>
    /// <param name="_rotation">The new rotation.</param>
    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;
    }

    public void Shoot(Vector3 _shotDir)
    {
        if(Physics.Raycast(shotOrigin.position, _shotDir, out RaycastHit _hit, 25f))
        {
            if (_hit.collider.CompareTag("Player"))
            {
                _hit.collider.GetComponent<Player>().TakeDamage(100f);
            }
        }
    }

    public void TakeDamage(float _damage)
    {
            
        if(health <= 0f)
        {
            return;
        }

        health -= _damage;

        if(health <= 0f)
        {
            health = 0f;
            characterController.enabled = false;
            transform.position = new Vector3(10f, 0.5f, 10f);
            ServerSend.PlayerPosition(this);

            StartCoroutine(Respawn());
        }

        ServerSend.PlayerHealth(this);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);

        health = maxHealth;
        characterController.enabled = true;
        ServerSend.PlayerRespawned(this);

    }

}
