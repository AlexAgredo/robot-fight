using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitControler : MonoBehaviour
{
    [SerializeField]
    private GameObject punchSlasher;
    private playerController GetPlayer;

    private void Awake()
    {
        GetPlayer = GameObject.FindGameObjectWithTag(tags.Player_Tag).GetComponent<playerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == tags.Player_Tag && !GetPlayer.isDefense)
        {
            Instantiate(punchSlasher, new Vector3(transform.position.x, transform.position.y, -4.0f), Quaternion.identity);
        }*/

        if (collision.tag == tags.Enemy_Tag)
        {
            Instantiate(punchSlasher, new Vector3(transform.position.x, transform.position.y, -4.0f), Quaternion.identity);
        }
    }
}
