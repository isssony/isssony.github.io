using System;
using System.Drawing;

[Serializable]
public class ArcFigure : Figure
{
    public ArcFigure(Rectangle rect) : base(rect) { }

    public override void Draw(Graphics g)
    {
        using (Pen pen = Stroke.GetPen())
        {
            if (Bounds.Width < 2 || Bounds.Height < 2)
                return;

            Rectangle safeBounds = Bounds;

            if (safeBounds.Width < 1 || safeBounds.Height < 1)
                return;

            g.DrawArc(pen, safeBounds, 0, 180);
        }
    }
}