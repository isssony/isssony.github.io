using System;
using System.Drawing;

[Serializable]
public class CircleFigure : Figure
{
    public CircleFigure(Rectangle rect) : base(rect)
    {
        int size = Math.Min(rect.Width, rect.Height);
        Bounds = new Rectangle(
            rect.X + (rect.Width - size) / 2,
            rect.Y + (rect.Height - size) / 2,
            size,
            size);
    }

    public override void Draw(Graphics g)
    {
        using (Pen pen = Stroke.GetPen())
        {
            g.DrawEllipse(pen, Bounds);
        }
    }
}