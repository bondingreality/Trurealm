using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class KeybindManager : MonoBehaviour
{

    private static KeybindManager instance;

    public static KeybindManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<KeybindManager>();
            }

            return instance;
        }
    }
    public static KeyCode GetKeyCode(KeyBinderKeys key)
    {
        return MyInstance.MyKeybinds[key.ToString()];
    }

    public Dictionary<string, KeyCode> MyKeybinds { get; private set; }
    public Dictionary<string, KeyCode> MyActionbinds { get; private set; }

    private string bindName;

    // Start is called before the first frame update
    void Start()
    {
        MyKeybinds = new Dictionary<string, KeyCode>();
        MyActionbinds = new Dictionary<string, KeyCode>();

        BindKey(KeyBinderKeys.UP, KeyCode.W);
        BindKey(KeyBinderKeys.LEFT, KeyCode.A);
        BindKey(KeyBinderKeys.DOWN, KeyCode.S);
        BindKey(KeyBinderKeys.RIGHT, KeyCode.D);

        BindKey(KeyBinderKeys.ACT1, KeyCode.Alpha1);
        BindKey(KeyBinderKeys.ACT2, KeyCode.Alpha2);
        BindKey(KeyBinderKeys.ACT3, KeyCode.Alpha3);
    }

    public void BindKey(KeyBinderKeys keyName, KeyCode keyBind)
    {
        string key = keyName.ToString();
        Dictionary<string, KeyCode> currentDictionary = MyKeybinds;

        if(key.Contains("ACT"))
        {
            currentDictionary = MyActionbinds;
        }

        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
        }
        else
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            if (myKey != null)
            {
                currentDictionary[myKey] = KeyCode.None;
                UIManager.MyInstance.UpdateKeyText(myKey, KeyCode.None);
            }
            currentDictionary[key] = keyBind;
        }

        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName; 
    }

    private void OnGUI()
    {
        if(bindName != string.Empty)
        {
            Event e = Event.current;

            if(e.isKey)
            {
                var foundBind = Enum.Parse(typeof(KeyBinderKeys), bindName);
                BindKey((KeyBinderKeys)foundBind, e.keyCode);
            }
        }
    }
}
