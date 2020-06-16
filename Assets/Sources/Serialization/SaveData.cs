using System;

[Serializable]
public class SaveData
{
    public static readonly string SAVE_NAME = "Save";

    private static SaveData _current = null;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }

        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    public PlayerProfileData playerProfile = new PlayerProfileData();
}