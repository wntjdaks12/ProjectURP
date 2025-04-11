public class Hero : Character
{
    public int MaxExp { get; set; }
    public int CurrentExp { get; private set; }

    public virtual void SetExp(int exp)
    {
        var resCurExp = CurrentExp + exp;

        if (resCurExp >= MaxExp)
        {
            CurrentExp = resCurExp - MaxExp;
        }
        else
        {
            CurrentExp = resCurExp;
        }
    }
}
