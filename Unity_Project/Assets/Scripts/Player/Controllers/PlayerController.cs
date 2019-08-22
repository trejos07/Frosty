using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Engine))]
public class PlayerController : MonoBehaviour
{
    private IController controller; //The controller is an interface variable that can read a class that encloses it
    private Engine mEngine;


    private void Awake()
    {
        mEngine = GetComponent<Engine>();
    }

    private void Start()
    {
        //mPlayer = GetComponent<Player>();
#if UNITY_EDITOR
        controller = gameObject.AddComponent<PcController>(); //If using unity, create a PcController which inherits from IController to assign it
#else
        controller = gameObject.AddComponent<PhoneController>(); //If using phones, create a PhoneController which inherits from IController to assign it
#endif
    }

    
    public void ExecuteMove()
    {
        if (controller != null)
        {
            mEngine.Move(controller.MoveHorizontal()); //Detect inputs from the controller each frame
        }
    }
}
