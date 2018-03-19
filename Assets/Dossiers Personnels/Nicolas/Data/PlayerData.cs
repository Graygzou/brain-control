[System.Serializable]
public class PlayerData
{
    public string pseudo;
    public int score;

    public PlayerData(string pseudo, int score)
    {
        this.pseudo = pseudo;
        this.score = score;
    }

    public string Name
    {
        get
        {
            return this.pseudo;
        }
        set
        {
            pseudo = value;
        }
    }

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            score = value;
        }
    }
}
