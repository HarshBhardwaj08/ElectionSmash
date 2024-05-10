using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    public GameObject firstpersoncam;
    public GameObject thirdpersoncam;
    int count;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            count++;
            if (count > 1)
            {
                count = 0;
            }
        } 
        if (count == 0)
        {
            firstpersoncam.SetActive(false);
            thirdpersoncam.SetActive(true);
        }
        else
        {
            firstpersoncam.SetActive(true);
            thirdpersoncam.SetActive(false);
        }
    }
}
