using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INPUT_MANAGER : MonoBehaviour
{
    public delegate void ReadInput(INPUT_MANAGER inputManager);

    private ReadInput gameInput;
    private ReadInput inventoryInput;

    private ReadInput input;

    public void SetInputToGameInputMode()
    {
        input = gameInput;
    }

    public void SetInputToInventoryInputMode() 
    {  
        input = inventoryInput;
    }

    public void SetInputMode(ReadInput inputMode)
    {
        input = inputMode;
    }

    void Update()
    {
        input(this);
    }
}
