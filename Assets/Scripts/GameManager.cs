using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void NewGameDelegate();
    public static NewGameDelegate OnNewGame; // Event to be invoked on death

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickNewGame() {

        OnNewGame?.Invoke();
    }

}
