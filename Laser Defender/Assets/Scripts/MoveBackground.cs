using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] float bgMoveSpeed = .2f;
    void Update()
    {
        Vector2 newPos = new Vector2();
        var moveY = Time.deltaTime * bgMoveSpeed;
        newPos.y = Mathf.Clamp(transform.position.y - moveY, -10, 10);
        transform.position = newPos;
    }
}
