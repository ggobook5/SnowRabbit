using UnityEngine;

public class BgScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the textuer scroll?")]
    public float scrollSpeed = 0.5f; // Speed at which the background scrolls

    [Header("References")]
    public MeshRenderer meshRenderer; // Reference to the Renderer component of the background


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
