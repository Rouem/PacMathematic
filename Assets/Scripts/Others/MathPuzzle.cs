using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathPuzzle : MonoBehaviour
{
    public Text termA, termB;

    public Button[] options;

    private int n1, n2, result;

    private string correctAwnser = "";

    public static System.Action correctAwnserResult, wrongAwnserResult;

    private void OnEnable()
    {

        Time.timeScale = 0f;
        foreach (var op in options)
        {
            op.onClick.RemoveAllListeners();
            op.onClick.AddListener(CloseMathPuzzle);
        }
        options[0].onClick.AddListener(CheckAwnserA);
        options[1].onClick.AddListener(CheckAwnserB);
        options[2].onClick.AddListener(CheckAwnserC);

        GeneratePuzzle();
    }

    private void CloseMathPuzzle()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }


    public void CheckAwnserA()
    {
        if (options[0].GetComponentInChildren<Text>().text.Equals(correctAwnser))
        {
            correctAwnserResult?.Invoke();
        }
        else
        {
            wrongAwnserResult?.Invoke();
        }
    }

    public void CheckAwnserB()
    {
        if (options[1].GetComponentInChildren<Text>().text.Equals(correctAwnser))
        {
            correctAwnserResult?.Invoke();
        }
        else
        {
            wrongAwnserResult?.Invoke();
        }
    }

    public void CheckAwnserC()
    {
        if (options[2].GetComponentInChildren<Text>().text.Equals(correctAwnser))
        {
            correctAwnserResult?.Invoke();
        }
        else
        {
            wrongAwnserResult?.Invoke();
        }
    }

    public void GeneratePuzzle()
    {
        n1 = Random.Range(0, 50);
        n2 = Random.Range(0, 50);
        result = n1 + n2;

        Debug.Log(result);

        termA.text = n1.ToString();
        termB.text = n2.ToString();

        foreach (var op in options)
        {
            op.GetComponentInChildren<Text>().text = (result - (Random.Range(-5, 6))).ToString();
        }

        int i = Random.Range(0, options.Length);

        correctAwnser = result.ToString();
        options[i].GetComponentInChildren<Text>().text = result.ToString();
    }


}
