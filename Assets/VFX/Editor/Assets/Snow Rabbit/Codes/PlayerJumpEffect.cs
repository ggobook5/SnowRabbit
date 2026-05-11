using UnityEngine;

public class PlayerJumpEffect : MonoBehaviour
{
    public GameObject slashEffectPrefab;

    public void PlayJumpSlash(Vector2 dir)
    {
        if (slashEffectPrefab == null) return;

        GameObject fx = Instantiate(slashEffectPrefab, transform.position, Quaternion.identity);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        fx.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}