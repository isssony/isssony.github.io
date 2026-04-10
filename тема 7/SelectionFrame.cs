using System.Drawing;
using System.Drawing.Drawing2D;

public class SelectionFrame
{
    public static void Draw(Graphics g, Rectangle rect)
    {
        using (Pen pen = new Pen(Color.Blue))
        {
            pen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(pen, rect);
        }

        int size = 6;

        g.FillRectangle(Brushes.White, rect.Left - 3, rect.Top - 3, size, size);
        g.FillRectangle(Brushes.White, rect.Right - 3, rect.Top - 3, size, size);
        g.FillRectangle(Brushes.White, rect.Left - 3, rect.Bottom - 3, size, size);
        g.FillRectangle(Brushes.White, rect.Right - 3, rect.Bottom - 3, size, size);
    }
}