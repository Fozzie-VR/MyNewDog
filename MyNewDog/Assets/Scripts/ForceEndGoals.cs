using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceEndGoals : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<GoalManager>().ForceEndAllGoals();
    }

   
    
}
