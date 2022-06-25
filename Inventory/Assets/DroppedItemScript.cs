using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemScript : MonoBehaviour
{
    [SerializeField] public Item myItem;
    private void OnTriggerEnter2D(Collider2D other) {
        //call pickup
    }
}
