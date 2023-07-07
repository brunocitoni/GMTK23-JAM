using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // all the scripts with the different mechanics the player hold
    [SerializeField] Health playerHealth;
    [SerializeField] SideckickMove movementScript;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] PlayerPickup pickup;
}
