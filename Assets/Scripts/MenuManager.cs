using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public int MainMenu { get; } = 1;
    public int SelectFile { get; } = 2;


    public void ToMain()
    {
        GameManager.Instance.LoadScene(MainMenu);
    }

    public void ToSelect()
    {
        GameManager.Instance.LoadScene(SelectFile);
    }

    public void ToPlay(int saveSlotNumber)
    {
        DataManager.Instance.SaveSlot = saveSlotNumber;
        DataManager.Instance.LoadData();
        GameManager.Instance.TimeScaleSet(false);
        GameManager.Instance.LoadScene(DataManager.Instance.NowPlayData.lastSceneIndex);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
