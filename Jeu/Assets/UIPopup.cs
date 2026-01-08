using UnityEngine;
using TMPro;
using System.Collections;

public class UIPopup : MonoBehaviour
{
    public float displayTime = 2f;
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        StopAllCoroutines();
        text.text = message;
        gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(displayTime);
        gameObject.SetActive(false);
    }
}
