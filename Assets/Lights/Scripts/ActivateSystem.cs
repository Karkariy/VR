using UnityEngine;
using System.Collections;

public class ActivateSystem : MonoBehaviour {

    /// <summary>
    ///  Active or desactive an object like a pointLight in the scene
    /// </summary>
    /// <param name="bactive"></param>
    public void UpdateActivation(bool bactive)
    {
        gameObject.SetActive(bactive);
    }


}
