using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript.hasWeapon = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
