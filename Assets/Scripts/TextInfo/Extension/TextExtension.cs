public static class TextExtension
{
    public static string GetStatReplace(this string str, StatAbility statAbilitys, SkillSystem skillSystem)
    {
        str = str.Replace("?skillAbilityPowerMultiplier", skillSystem.StatAbility.AbilityPowerMultiplier.ToString());
        str = str.Replace("?skillPerSecond", skillSystem.StatAbility.PerSecond.ToString());
        str = str.Replace("?skillCooldownTime", skillSystem.SkillInfo.CooldwonTime.ToString());
        str = str.Replace("?casterAbilityPower", statAbilitys.AbilityPower.ToString());

        return str;
    }
}
