using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManger : MonoBehaviour
{
    [SerializeField] private List<SequenceInfo> sequences = new List<SequenceInfo>();
    public float times;
    BasePlayer player;
    private void Awake()
    {
        player = GetComponent<BasePlayer>();
        foreach (var sequence in sequences)
        {
            sequence.sequence = ConvertKeyStringsToKeyCodes(sequence.keyStrings);
        }
    }
    private List<KeyCode> ConvertKeyStringsToKeyCodes(List<string> keyStrings)
    {
        List<KeyCode> keyCodes = new List<KeyCode>();
        foreach (string keyString in keyStrings)
        {
            KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyString);
            keyCodes.Add(keyCode);
        }
        return keyCodes;
    }
    public void AddSequence(float timeLimit, List<string> keyStrings)
    {
        sequences.Add(new SequenceInfo(timeLimit, keyStrings));
    }

    void Update()
    {

        foreach (var sequenceInfo in sequences)
        {

            if (Input.anyKeyDown)
            {
                if (Time.time - sequenceInfo.lastKeyPressTime <= sequenceInfo.timeLimit)
                {
                    if (Input.GetKeyDown(sequenceInfo.sequence[sequenceInfo.currentIndex]))
                    {
                        sequenceInfo.currentIndex++;
                        if (sequenceInfo.currentIndex >= sequenceInfo.sequence.Count)
                        {
                            Debug.Log("You pressed the right buttons! Take a combo!");
                            player.stateMachine.ChangeState(player.backFlipTaunt);
                            sequenceInfo.currentIndex = 0;
                        }
                    }
                    else
                    {

                        sequenceInfo.currentIndex = 0;
                    }
                }

                sequenceInfo.lastKeyPressTime = Time.time;
            }
        }
    }
}
[System.Serializable]
public class SequenceInfo
{
    [Header("KeyInputs")]
    public List<string> keyStrings; // This will be assigned from the script
    [HideInInspector]
    public List<KeyCode> sequence;
    public float timeLimit;
    public float lastKeyPressTime;
    public int currentIndex;
    public SequenceInfo(float limit, List<string> keys)
    {

        timeLimit = limit;
        lastKeyPressTime = 0f;
        currentIndex = 0;

        keyStrings = keys;
    }
}

