using System;
using System.Drawing;

[Serializable]
public class EpilepticRingFigure : Figure
{
    public EpilepticRingFigure(Rectangle rect) : base(rect) { }

    public override void Draw(Graphics g)
    {
        using (Pen pen = Stroke.GetPen())
        {
            g.DrawEllipse(pen, Bounds);

            int cx = Bounds.X + Bounds.Width / 2;
            int cy = Bounds.Y + Bounds.Height / 2;
            int rx = Bounds.Width / 2;
            int ry = Bounds.Height / 2;

            PointF prev = PointOnEllipse(cx, cy, rx, ry, 0);

            for (int i = 1; i <= 72; i++)
            {
                double angle = i * Math.PI * 2 / 72.0;
                PointF cur = PointOnEllipse(cx, cy, rx, ry, angle);

                float wave = 4f * (float)Math.Sin(angle * 8);
                PointF p1 = new PointF(prev.X + wave, prev.Y - wave);
                PointF p2 = new PointF(cur.X - wave, cur.Y + wave);

                g.DrawLine(pen, p1, p2);
                prev = cur;
            }
        }
    }

    private PointF PointOnEllipse(int cx, int cy, int rx, int ry, double angle)
    {
        return new PointF(
            cx + rx * (float)Math.Cos(angle),
            cy + ry * (float)Math.Sin(angle));
    }
}