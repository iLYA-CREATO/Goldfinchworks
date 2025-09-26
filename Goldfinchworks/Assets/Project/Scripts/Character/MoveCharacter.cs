using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runMoveSpeed = 30f;
    [SerializeField] private float gravityValue = -5;

    float moveX;
    float moveY;
    float moveZ;
    Vector3 velocity;

    Vector3 previousPosition;
    public static bool isWalk;

    [SerializeField]
    [Header("Блокировка управления")]
    private bool lockController;

    private void Start()
    {
        previousPosition = transform.position;
    }
    private void FixedUpdate()
    {
        if (lockController == false)
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            if (transform.position.y > 1.1f)
            {
                moveY = gravityValue;
            }

            MovePlayer(moveX, moveY, moveZ);

            velocity.y += gravityValue * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            isWalk = (transform.position - previousPosition).magnitude > 0.01f; // 0.01f - это допуск, чтобы избежать ложных срабатываний из-за небольших изменений
            previousPosition = transform.position;
        }
       
    }
    private void MovePlayer(float moveX, float moveY, float moveZ)
    {
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

       if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runMoveSpeed;
        }
       else
        {
            moveSpeed = 5f;
        }

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
    public void LockController(bool isLock)
    {
        lockController = isLock;
    }
}