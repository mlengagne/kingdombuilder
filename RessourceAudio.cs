using System;
using SFML.Audio;

class RessourceAudio
{
    private static RessourceAudio instance = null;
    private Music m_musiqueMenu = null;
    private Music m_musiqueJeu = null;
    private SoundBuffer m_bufferMaison = null;
    private Sound m_sonMaison = null;

    public RessourceAudio()
    {
        m_musiqueMenu = new Music("musics/music_mainmenu.ogg");
        m_musiqueMenu.Loop = true;
        m_musiqueMenu.Volume = 50.0f;

        m_musiqueJeu = new Music("musics/music_ingame.ogg");
        m_musiqueJeu.Loop = true;
        m_musiqueJeu.Volume = 50.0f;

        m_bufferMaison = new SoundBuffer("sounds/poser_maison.wav");

        m_sonMaison = new Sound(m_bufferMaison);
    }

    public Sound SonPoserMaison
    {
        get { return m_sonMaison; }
    }

    public Music MusiqueMenu
    {
        get { return m_musiqueMenu; }
    }

    public Music MusiqueJeu
    {
        get { return m_musiqueJeu; }
    }

    public static void Allouer()
    {
        if (instance == null)
            instance = new RessourceAudio();
    }

    public static RessourceAudio Instance
    {
        get { return instance; }
    }
}
