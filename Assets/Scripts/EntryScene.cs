using UnityEngine;

public class EntryScene : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.LoadScene(1, false);
    }
}
