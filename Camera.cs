using SFML.Graphics;
using SFML.Window;
using System;

class Camera : View
{
    private int m_niveauZoom;

    public Camera()
        : base(new Vector2f(10.0f * 128.0f, 10.0f * 128.0f), new Vector2f(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height))
    {
        m_niveauZoom = 0;
    }

    public void MiseAJour()
    {
        Deplacer();
        Zoom();
        Dezoom();
    }

    private void Deplacer()
    {
        Single x = 0.0f, y = 0.0f;

        if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            y -= 6.0f;
        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            y += 6.0f;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            x -= 6.0f;
        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            x += 6.0f;

        this.Move(new Vector2f(x, y));
    }

    private void Zoom()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Add))
        {
            m_niveauZoom--;
            if (m_niveauZoom < -50)
            {
                m_niveauZoom++;
                return;
            }
            this.Zoom(0.99f);

        }
    }

    private void Dezoom()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract))
        {
            m_niveauZoom++;
            if (m_niveauZoom > 50)
            {
                m_niveauZoom--;
                return;
            }
            this.Zoom(1.01f);
        }
    }
}