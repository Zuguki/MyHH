namespace FindWork.BL.General;

public static class Helpers
{
    public static int? StringToIntDef(string str, int? def)
    {
        return int.TryParse(str, out var value) 
            ? value 
            : def;
    }
}