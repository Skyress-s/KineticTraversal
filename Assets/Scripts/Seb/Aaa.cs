using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square
{
    public readonly int width;
    public readonly int height;

    public Square(int width, int height)
    {
        this.width = width;
        this.height = height;
        Debug.Log("Square was created");
    }

    public Square(int hw)
    {
        this.width = this.height = hw;
    }

    public int Area()
    {
        return width * height;
    }
}

public class BackendBook
{
    private int _hasIFrame;

    public bool HasIFrame {
        get {
            return _hasIFrame > 0;
        }
        set {
            if (value)
            {
                _hasIFrame++;
            }
            else {
                _hasIFrame--;
            }
        }
    }

    public int _canOnlyBeRead;

    public int CanOnlyBeRead
    {
        get
        {
            return _canOnlyBeRead;
        }
        private set
        {
            _canOnlyBeRead = value;
        }
    }

    private void DoSomething()
    {
        HasIFrame = true;
        HasIFrame = true;
        HasIFrame = false;

        if (HasIFrame)
        {
            return;
        }

        Square square = new Square(2, 4);
        Square equalSides = new Square(2);

        Debug.Log(square.width);
        square.Area();
    }
}
