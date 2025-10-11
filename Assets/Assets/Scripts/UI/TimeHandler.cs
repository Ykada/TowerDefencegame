using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    private int startime = 20;

    [SerializeField]private Text timeText;
    [SerializeField] private GameObject startingobjects;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }
    private System.Collections.IEnumerator StartTimer()
    {
        int currentTime = startime;
        while (currentTime > 0)
        {
            timeText.text = "Time: " + currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        timeText.text = "Time: 0";
        startingobjects.SetActive(true);
        timeText.gameObject.SetActive(false);
    }

}