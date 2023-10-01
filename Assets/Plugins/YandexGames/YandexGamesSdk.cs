using System.Runtime.InteropServices;
using UnityEngine;

public class YandexGamesSdk : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void Hello();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
