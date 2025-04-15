namespace Game
{
public sealed class UniqueCellsProvider
{
    private Cell[] _cells;
    private bool[] _chosenCells;

    public void SetCells(Cell[] cells)
    {
        _cells = cells;
        _chosenCells = new bool[_cells.Length];
    }

    public Cell GetRandomCell()
    {
        if (_chosenCells.All(cell => cell == true))
        {
            ResetChosen();
        }

        int index = Random.Range(0, _cells.Length - 1);
        while (_chosenCells[index] == true)
        {
            index = Random.Range(0, _cells.Length - 1);
        }

        return _cells[i];
    }

    private void ResetChosen()
    {
        Debug.Log("All cells were used. Resetting");
        for (int i = 0; i < _chosenCells.Length; i++)
        {
            _chosenCells[i] = false;
        }
    }
}
}