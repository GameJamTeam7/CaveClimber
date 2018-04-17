using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        Instance = this;
        Load();

        Debug.Log(Helper.Serialize<SaveState>(state));
    }

    //Save the whole state of this SaveState.cs to the playerPref
    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    //load the previous saved state from the playerPref
    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save Found, creating new save");
        }
    }
}
