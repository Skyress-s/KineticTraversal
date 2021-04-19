using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
    protected int height, width;
    internal int depth;
    public float GetArea()
    {
        return height * width;
    }
    protected void SetWidthAndHeight(int height, int width)
    {
        this.height = height;
        this.width = width;
    }

    public void pub()
    {

    }

    protected void prot()
    {

    }

    internal void inter()
    {

    }

    private void priv()
    {

    }
}

public class Rectangle : Shape
{
    public void DoSomething()
    {
        SetWidthAndHeight(2, 2);
        GetArea();

        pub();
        prot();
        inter();
    }

}

public class Flower
{
    void Method()
    {
        Shape shape = new Shape();

        shape.depth = 2;
    }
}
