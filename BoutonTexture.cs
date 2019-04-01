using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

    class BoutonTexture : Bouton
    {
        
		private Sprite mSprtNormal=null;
		private Sprite mSprtHover=null;
        private Sprite mSprtAppuyer=null;
	

			/**
            * Constructeur avec parametres
            * @param x : position du bouton en x
            * @param y : position du bouton en y
            * @param up : chemin relatif de l'image du bouton non appuye
            * @param hover : chemin relatif de l'image du bouton survol
            * @param son : recuperation du son
            */
        public BoutonTexture(float posX, float posY, TypeBouton normal, TypeBouton hover, TypeBouton appuyer) : base(posX, posY)
        {
	        mSprtNormal=Ins.GetSpriteBouton(normal);
	        mSprtHover=Ins.GetSpriteBouton(hover);
            mSprtAppuyer=Ins.GetSpriteBouton(appuyer);
	        mSprtNormal.Position = new Vector2f(mPositionX, mPositionY);
	        mSprtHover.Position =new Vector2f(mPositionX, mPositionY);
            mSprtAppuyer.Position =new Vector2f(mPositionX, mPositionY);
        }

			/** 
            * Methode : test l'appuie sur un bouton
            * @param curseurX : position actuelle en x de la souris
            * @param curseurY : position actuelle en y de la souris
            * @return : renvoie vrai si on appuie sur le bouton, faux sinon
            */
        public override int Appuyer(int curseurX, int curseurY)
        {
            if (curseurX >= mPositionX && curseurX < mPositionX + mSprtAppuyer.Texture.Size.X && curseurY >= mPositionY && curseurY < mPositionY + mSprtAppuyer.Texture.Size.Y)
            {
                return (int)TypeEffetBouton.APPUYER;
            }
            else
            {
                return (int)TypeEffetBouton.NORMAL;
            }
        }

			/**
            * Methode : test le survol d'un bouton
            * @param curseurX : nouvelle position en x de la souris
            * @param curseurY : nouvelle position en y de la souris
            */
        public override void Survoler(int curseurX, int curseurY)
        {
            if (curseurX >= mPositionX && curseurX < mPositionX + mSprtHover.Texture.Size.X && curseurY >= mPositionY && curseurY < mPositionY + mSprtHover.Texture.Size.Y)
            {
                mAppuie = TypeEffetBouton.HOVER;
            }
            else
            {
                mAppuie = TypeEffetBouton.NORMAL;
            }
        }



        /**
         * **/
        public override void SurvolerAppuyer(int curseurX, int curseurY)
        {
            if (curseurX >= mPositionX && curseurX < mPositionX + mSprtAppuyer.Texture.Size.X && curseurY >= mPositionY && curseurY < mPositionY + mSprtAppuyer.Texture.Size.Y)
            {
                 mAppuie= TypeEffetBouton.APPUYER;
            }
            else
            {
                 mAppuie=TypeEffetBouton.NORMAL;
            }
        }
       
			/**
            * Methode : dessine le bouton non appuye ou survol
            * @param ecran : reference sur la fenetre de jeu
            */
        public override void Dessiner()
        {
            switch (mAppuie)
            {
                case TypeEffetBouton.NORMAL:
                    mFenetre.Draw(mSprtNormal);
                    break;
                case TypeEffetBouton.HOVER:
                    mFenetre.Draw(mSprtHover);
                    break;
                case TypeEffetBouton.APPUYER:
                    mFenetre.Draw(mSprtAppuyer);
                    break;
                default:
                    break;
            }
        }
    }
