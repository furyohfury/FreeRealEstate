namespace Game{
public sealed class Level
{
    public event Subject<bool> OnGuessed;

    private Cell _desiredCell;

    public bool ChooseCell(Cell Cell)
    {
        bool guessed = cell == _desiredCell;
        OnGuessed.OnNext(guessed);
        
        return guessed;
    }
}
}