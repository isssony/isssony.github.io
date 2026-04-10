using System;
using System.Drawing;

[Serializable]
public abstract class Figure
{
    public Rectangle Bounds { get; set; }
    public Stroke Stroke { get; set; } = new Stroke();

    protected Figure() { }

    protected Figure(Rectangle rect)
    {
        Bounds = rect;
    }

    public abstract void Draw(Graphics g);

    public virtual bool Contains(Point p)
    {
        return Bounds.Contains(p);
    }

    public void Move(int dx, int dy)
    {
        Bounds = new Rectangle(
            Bounds.X + dx,
            Bounds.Y + dy,
            Bounds.Width,
            Bounds.Height);
    }

    public void MoveTo(Point p)
    {
        Bounds = new Rectangle(p.X, p.Y, Bounds.Width, Bounds.Height);
    }
}