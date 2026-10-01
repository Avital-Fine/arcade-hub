using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure
{
    public static class ExtensionMethods
    {
        private static readonly ArgumentOutOfRangeException sr_ArgumentOutOfRangeException = new ArgumentOutOfRangeException();

        public static bool IsInRange(this float s_TheNum, float i_Low, float i_High)
        {
            return s_TheNum <= i_High && s_TheNum >= i_Low;
        }

        public static void ThrowIfNotInRange(this float s_TheNum, float i_Low, float i_High)
        {
            if (s_TheNum > i_High || s_TheNum < i_Low)
            {
                throw sr_ArgumentOutOfRangeException;
            }
        }

        public static int Mod(this int k, int n) { return ((k %= n) < 0) ? k + n : k; }

        public static void DrawRectOutline(this SpriteBatch i_Batch, Texture2D i_Pixel, Rectangle i_Rect, Color i_Color, int i_Thickness = 1)
        {
            if (i_Pixel == null) return;
            i_Batch.Draw(i_Pixel, new Rectangle(i_Rect.X, i_Rect.Y, i_Rect.Width, i_Thickness), i_Color);
            i_Batch.Draw(i_Pixel, new Rectangle(i_Rect.X, i_Rect.Bottom - i_Thickness, i_Rect.Width, i_Thickness), i_Color);
            i_Batch.Draw(i_Pixel, new Rectangle(i_Rect.X, i_Rect.Y, i_Thickness, i_Rect.Height), i_Color);
            i_Batch.Draw(i_Pixel, new Rectangle(i_Rect.Right - i_Thickness, i_Rect.Y, i_Thickness, i_Rect.Height), i_Color);
        }

        public static void DrawFilledRect(this SpriteBatch i_Batch, Texture2D i_Pixel, Rectangle i_Rect, Color i_Color)
        {
            if (i_Pixel == null) return;
            i_Batch.Draw(i_Pixel, i_Rect, i_Color);
        }

        public static void DrawCircle(this SpriteBatch i_Batch, Texture2D i_Pixel, Vector2 i_Center, float i_Radius, Color i_Color)
        {
            if (i_Pixel == null) return;
            int r = (int)i_Radius;
            for (int y = -r; y <= r; y++)
            {
                for (int x = -r; x <= r; x++)
                {
                    if (x * x + y * y <= r * r)
                    {
                        i_Batch.Draw(i_Pixel, new Rectangle((int)(i_Center.X + x), (int)(i_Center.Y + y), 1, 1), i_Color);
                    }
                }
            }
        }
    }
}
