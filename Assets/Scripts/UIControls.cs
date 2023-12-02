using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    [SerializeField]
    private int UIMode = 1;
    public GameObject JoyStick;
    public GameObject JumpBtn;
    public GameObject GamePad;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(Application.isMobilePlatform);
        if(UIMode == 1)
        {
            JoyStick.SetActive(false);
            GamePad.SetActive(true);
            JumpBtn.SetActive(true);
        }
        else
        {
            JoyStick.SetActive(true);
            GamePad.SetActive(false);
            JumpBtn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
