using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{   
    public List<GameObject> controllerPrefabs;
    private InputDevice targetDevice;
    private GameObject spawnedController;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }

        //StartCoroutine(GetDevices(1.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
        if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log("Pressing Primary Button");

        
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
            Debug.Log("Trigger pressed " + triggerValue);

        
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
            Debug.Log("Primary Touchpad " + primary2DAxisValue);

    }



/*     IEnumerator GetDevices(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        Debug.Log("Enumerating XR Devices...");
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        Debug.Log("Finished!");
    } */
}
