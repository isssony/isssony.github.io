using System;
using System.Drawing;
using System.Drawing.Drawing2D;

[Serializable]
public class Stroke
{
    public Stroke()
    {
        Color = Color.Black;
        Width = 2f;
        DashStyle = DashStyle.Solid;
    }

    public Color Color { get; set; }
    public float Width { get; set; }
    public DashStyle DashStyle { get; set; }

    public Pen GetPen()
    {
        Pen pen = new Pen(Color, Width);
        pen.DashStyle = DashStyle;
        return pen;
    }
}