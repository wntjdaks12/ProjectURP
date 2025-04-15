using UnityEngine;

public static class TextExtension
{
    public static string GetStatReplace(this string str, StatAbility[] statAbilitys)
    {
        for (int i = 0; i < statAbilitys.Length; i++)
        {
            if (i == 0)
            {
                str = str.Replace("?skillAbilityPowerMultiplier", statAbilitys[i].AbilityPowerMultiplier.ToString());
                str = str.Replace("?skillPerSecond", "0.5");
            }
            else if (i == 1)
            {
                str = str.Replace("?casterAbilityPower", statAbilitys[i].AbilityPower.ToString());
            }
        }

        return str;
    }
}
