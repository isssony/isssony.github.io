using System;
using System.Drawing;

[Serializable]
public class RingFigure : Figure
{
    public int HoleRadius { get; set; } = 20;

    public RingFigure(Rectangle rect) : base(rect) { }

    public override void Draw(Graphics g)
    {
        using (Pen pen = Stroke.GetPen())
        {
            g.DrawEllipse(pen, Bounds);

            int cx = Bounds.X + Bounds.Width / 2;
            int cy = Bounds.Y + Bounds.Height / 2;

            g.DrawEllipse(pen,
                cx - HoleRadius,
                cy - HoleRadius,
                HoleRadius * 2,
                HoleRadius * 2);

            g.FillRectangle(Brushes.White,
                cx + HoleRadius - 4,
                cy - 4,
                8,
                8);
        }
    }

    public bool OnMarker(Point p)
    {
        int cx = Bounds.X + Bounds.Width / 2;
        int cy = Bounds.Y + Bounds.Height / 2;

        Rectangle marker = new Rectangle(
            cx + HoleRadius - 4,
            cy - 4,
            8,
            8);

        return marker.Contains(p);
    }

    public void ChangeRadius(Point p)
    {
        int cx = Bounds.X + Bounds.Width / 2;
        HoleRadius = Math.Abs(p.X - cx);
    }
}