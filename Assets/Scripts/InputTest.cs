using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public InputActionProperty testActionValue;
    
    void Start()
    {
        
    }

    void Update()
    {
        float value = testActionValue.action.ReadValue<float>();
        Debug.Log("VALUE : " + value);
    }
}
