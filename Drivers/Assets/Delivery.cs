using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public List<string> packagesHeld { get; private set; }
    public int packagesDelivered { get; private set; }

    Color32 redPackageColor;
    Color32 noPackageColor = new Color32(255,255,255,255);
    private int score = 0;

    SpriteRenderer spriteRenderer;

    private void Start() 
    {
        redPackageColor = hexTo32("C35252");
        spriteRenderer = GetComponent<SpriteRenderer>();
        packagesHeld = new List<string>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Ouch!");
        score--;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Package" && !packagesHeld.Contains("red"))
        {
            spriteRenderer.color = redPackageColor;
            Debug.Log("Package picked up");
            packagesHeld.Add("red");
            Destroy(other.gameObject);
        }
        if (other.tag == "Customer")
        {
            if (packagesHeld.Contains("red")) 
            {
                spriteRenderer.color = noPackageColor;
                Debug.Log("Delivered package");
                packagesHeld.Remove("red");
                packagesDelivered++;
                score++;
                if (packagesDelivered == 4) {
                    Debug.Log($"You're done! You scored:{score}");
                }
            }
            else
            {
                Debug.Log("You need to pick up a package first");
            }
        }
    }
    private Color32 hexTo32(string hex) {
        int hexVal = System.Convert.ToInt32(hex, 16);
        byte R = (byte)((hexVal >> 16) & 0xFF);
        byte G = (byte)((hexVal >> 8) & 0xFF);
        byte B = (byte)((hexVal) & 0xFF);
        Debug.Log(new Color32(R, G, B, 255));
        return new Color32(R, G, B, 255);
    }
}
