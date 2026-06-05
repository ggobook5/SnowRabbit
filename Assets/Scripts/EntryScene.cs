using UnityEngine;

public class EntryScene : MonoBehaviour
{
    private void Start()
    {
        BaseSceneManager.Instance.LoadScene(1, false);
    }
}
