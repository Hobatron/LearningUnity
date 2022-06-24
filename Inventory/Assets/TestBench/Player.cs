using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float mySpeed = 7f;
    [SerializeField] VoidEventChannelSO toggleInventory;
    private Vector2 rawInput;

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 delta = rawInput * Time.deltaTime * mySpeed;
        transform.position += delta;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnInventory(InputValue value)
    {
        if (value.isPressed)
        {
            toggleInventory.RaiseEvent();
        }
    }

}
