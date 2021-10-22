using Microsoft.Xna.Framework;
/// <summary>
/// This is where we create the classes for each individual block
/// </summary>

class Square : Block
{
    public Square()
    {
        color = Color.Yellow;
        bGrid = new bool[2, 2];
        bGrid[0, 0] = true;
        bGrid[0, 1] = true;
        bGrid[1, 1] = true;
        bGrid[1, 0] = true;
    }
}

class Long : Block
{
    public Long()
    {
        color = Color.Blue;
        bGrid = new bool[4, 4];
        bGrid[1, 0] = true;
        bGrid[1, 1] = true;
        bGrid[1, 2] = true;
        bGrid[1, 3] = true;
    }
}

class SnakeL : Block
{
    public SnakeL()
    {
        color = Color.Green;
        bGrid = new bool[3, 3];
        bGrid[0, 0] = true;
        bGrid[1, 0] = true;
        bGrid[1, 1] = true;
        bGrid[2, 1] = true;
    }
}
class SnakeR : Block
{
    public SnakeR()
    {
        color = Color.Purple;
        bGrid = new bool[3, 3];
        bGrid[2, 0] = true;
        bGrid[1, 0] = true;
        bGrid[1, 1] = true;
        bGrid[0, 1] = true;
    }
}

class LL : Block
{
    public LL()
    {
        color = Color.Red;
        bGrid = new bool[3, 3];
        bGrid[0, 0] = true;
        bGrid[0, 1] = true;
        bGrid[1, 1] = true;
        bGrid[2, 1] = true;

    }
}
class LR : Block
{
    public LR()
    {
        color = Color.Orange;
        bGrid = new bool[3, 3];
        bGrid[2, 0] = true;
        bGrid[0, 1] = true;
        bGrid[1, 1] = true;
        bGrid[2, 1] = true;
    }
}
class T : Block
{
    public T()
    {
        color = Color.Cyan;
        bGrid = new bool[3, 3];
        bGrid[1, 0] = true;
        bGrid[1, 1] = true;
        bGrid[0, 1] = true;
        bGrid[2, 1] = true;
    }
}