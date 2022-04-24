public enum TempResult : byte
{
    Cold,
    Normal,
    Hot
}
public static class Temperarute 
{
    public static TempResult GetTemperature(int temp) 
    {
        if (temp <= -50)
            return TempResult.Cold;
        else if (temp >= 50)
            return TempResult.Hot;

        return TempResult.Normal;
    }
}
