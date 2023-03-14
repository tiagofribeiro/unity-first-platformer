using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int gems = 0;

    [SerializeField] private TextMeshProUGUI gemsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GreenGem"))
        {
            Destroy(collision.gameObject);
            gems++;
            gemsText.text = "Gems: " + gems;
        }
    }
}
