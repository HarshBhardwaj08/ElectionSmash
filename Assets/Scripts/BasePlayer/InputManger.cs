using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReadyPlayerMe.Core.Analytics.Constants;

public class InputManger : MonoBehaviour
{
    public float delayBetweenAnimations = 1.0f;
    private KeyCode currentKey = KeyCode.None;
    private bool isPlayingAnimation = false;
    private Coroutine inputCoroutine;
    public List<Pair<string, string, string>> inputRecord = new List<Pair<string, string, string>>();
    private int count = 0;
    private float timeCounter = 0f;
    private bool isPlaying = true;
    ModiPlayer player;
    private void Awake()
    {
        player = GetComponent<ModiPlayer>();
    }
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        if (!Input.anyKeyDown)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 2f)
            {
                count = 0;
                timeCounter = 0f;
            }
        }
        else
        {
            timeCounter = 0f;
        }

        if (Input.anyKeyDown)
        {
            ResetInputCoroutine();
            currentKey = GetPressedKey();
            count++;
            if (count <= 1)
            {
                SwitchAnim(new Pair<string, string, string>(currentKey.ToString().ToUpper(), inputX.ToString(), inputY.ToString()));

            }
             Debug.Log(currentKey.ToString());
            if (currentKey != KeyCode.None && count > 1)
            {
                inputRecord.Add(new Pair<string, string, string>(currentKey.ToString().ToUpper(), inputX.ToString(), inputY.ToString()));

            }
        }
        if (!isPlayingAnimation && inputRecord.Count > 0 && inputCoroutine == null)
        {
            inputCoroutine = StartCoroutine(InputDelayCoroutine());
        }

    }

    void ResetInputCoroutine()
    {
        if (inputCoroutine != null)
        {
            StopCoroutine(inputCoroutine);
            inputCoroutine = null;
        }
    }

    KeyCode GetPressedKey()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

    IEnumerator InputDelayCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        inputCoroutine = null;
        StartCoroutine(PlayAnimationSequence());
    }

    IEnumerator PlayAnimationSequence()
    {
        isPlayingAnimation = true;

        List<Pair<string, string, string>> val = new List<Pair<string, string, string>>(inputRecord);

        foreach (var pair in val)
        {
            yield return new WaitForSeconds(delayBetweenAnimations);

            SwitchAnim(pair);
        }
        inputRecord.Clear();
        count = 0;
        isPlayingAnimation = false;
        isPlaying = true;
    }

    void SwitchAnim(Pair<string, string, string> pair)
    {
        switch ((pair.first, pair.second, pair.third))
        {
            case ("L", "1", "0"):
            case ("JOYSTICKBUTTON0", "1", "0"):
                player.stateMachine.ChangeState(player.dropKick);
                break;
            case ("P", "0", "0"):
              player. stateMachine.ChangeState(player.lowpunchState);
                break;
            case ("P" , "1", "1"):
                player.stateMachine.ChangeState(player.playerWindKick);
                break;
            case ("O", "0", "0"):
                player.stateMachine.ChangeState(player.playerhighpunch);
                break;
            default:
              isPlaying = false;
                return;
        }
    }
}
[System.Serializable]
public class Pair<T1, T2, T3>
{
    public T1 first;
    public T2 second;
    public T3 third;

    public Pair(T1 first, T2 second, T3 third)
    {
        this.first = first;
        this.second = second;
        this.third = third;
    }
}