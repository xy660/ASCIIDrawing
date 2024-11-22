using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
public class UnsafeImageTool : IDisposable
{
    Bitmap bitmap;
    BitmapData bData;
    int PixelSize = 4;

    public UnsafeImageTool(Bitmap bmp)
    {
        bitmap = bmp;
        bData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
    }
    private bool clamp(int value,int min, int max)
    {
        if(value >= min && value < max)
        {
            return true;
        }
        return false;
    }
    static char[] table = { ' ', '`', '"', '.', ',', '-', ':', ';', '!', '+', '*', '=', '\'', '\\', '/', '|', 'L', 'A', 'G', 'F', 'X', 'H', '@', '#', '%'};

    public char GetPixelCharact(int x, int y)
    {
        unsafe
        {
            byte* p = (byte*)bData.Scan0 + y * bData.Stride + x * PixelSize;
            byte gray = (byte)((p[1] + p[2] + p[3]) / 3);
            for (int i = 0; i < 25; i++)
            {
                if(clamp(gray,i * 9,(i + 1) * 9))
                {
                    return table[i];
                }
            }
            return '+';
        }
    }
    public Color GetPixelColor(int x,int y)
    {
        unsafe
        {
            byte* p = (byte*)bData.Scan0 + y * bData.Stride + x * PixelSize;
            return Color.FromArgb(p[2], p[1], p[0]);
        }
    }
    public void Dispose()
    {
        bitmap.UnlockBits(bData);
    }
}


