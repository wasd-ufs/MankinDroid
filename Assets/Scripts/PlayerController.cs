using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D playerRigidBody;
    private bool playerRightSide = false;
    private float playerDirection;
    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        playerDirection = Input.GetAxis("Horizontal");
        playerRigidBody.linearVelocity = new Vector2(playerDirection * playerSpeed, playerRigidBody.linearVelocity.y);

        if (playerDirection > 0 && !playerRightSide || playerDirection < 0 && playerRightSide) TurnPlayer();
    }
    private void TurnPlayer()
    {
        playerRightSide = !playerRightSide;
        transform.Rotate(0f, 180f, 0f);
    }
}
