using System;
using System.Drawing;

[Serializable]
public class EllipseFigure : Figure
{
    public EllipseFigure(Rectangle rect) : base(rect) { }

    public override void Draw(Graphics g)
    {
        using (Pen pen = Stroke.GetPen())
        {
            g.DrawEllipse(pen, Bounds);
        }
    }
}