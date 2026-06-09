using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemIndex = -1;


    void Start()
    {
        if (DataManager.Instance.NowPlayData.Items[itemIndex])
        {
            gameObject.SetActive(false);
        }
    }

    public void SetActivated()
    {
        DataManager.Instance.NowPlayData.Items[itemIndex] = true;
        DataManager.Instance.SaveData();

        gameObject.SetActive(false);
    }
}
