using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace WindowsFormsApplication1
{
    public partial class Form_casa : Form
    {
        
        int lateral = 0;
        int lateral2 = 0;
        Vector3d dir = new Vector3d(0, -450, 120);        //direção da câmera
        Vector3d pos = new Vector3d(0, -550, 120);     //posição da câmera
        float camera_rotation = 0;                     //rotação no eixo Z
        float camera_rotation2 = 0;

        Paredes estrutura = new Paredes();
        
        //texturas
        int texPorta;
        int texParede;
        int texPortao;
        int texJanela;
        int texChao;
        int texQuintal;

        public Form_casa()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //limpa os buffers
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity(); //zera a matriz de projeção com a matriz identidade


            //                 Matrix4 lookat = Matrix4.LookAt(lateral, -500.0f, 1.5f,    
            //                                              1.5f, 5.0f, 1.5f,
            //                                           0.0f, 0.0f, 1.0f);
            Matrix4d lookat = Matrix4d.LookAt(pos.X, pos.Y, pos.Z, dir.X, dir.Y, dir.Z, 0, 0, 1);

            //aplica a transformacao na matriz de rotacao
            GL.LoadMatrix(ref lookat);
            //GL.Rotate(camera_rotation, 0, 0, 1);

            GL.Enable(EnableCap.DepthTest);

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0); GL.Vertex3(500, 0, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 500, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 500);
            GL.End();

            GL.Color3(Color.Gray);
            GL.Begin(PrimitiveType.Quads);

            //PAREDE COM PORTA
            GL.Vertex3(-200, 0, 200);
            GL.Vertex3(-200, 0, 400);
            GL.Vertex3(500, 0, 400);
            GL.Vertex3(500, 0, 200);

            GL.Vertex3(-200, 0, 0);
            GL.Vertex3(-200, 0, 200);
            GL.Vertex3(-50, 0, 200);
            GL.Vertex3(-50, 0, 0);

            GL.Vertex3(400, 0, 0);
            GL.Vertex3(400, 0, 200);
            GL.Vertex3(500, 0, 200);
            GL.Vertex3(500, 0, 0);

            GL.Vertex3(200, 0, 0);
            GL.Vertex3(200, 0, 200);
            GL.Vertex3(300, 0, 200);
            GL.Vertex3(300, 0, 0);

            //---

            GL.Vertex3(-200, 0, 200);
            GL.Vertex3(-200, 0, 400);
            GL.Vertex3(-200, 500, 400);
            GL.Vertex3(-200, 500, 200);

            GL.Vertex3(-200, 0, 0);
            GL.Vertex3(-200, 0, 400);
            GL.Vertex3(-200, 200, 400);
            GL.Vertex3(-200, 200, 0);

            GL.Vertex3(-200, 300, 0);
            GL.Vertex3(-200, 300, 400);
            GL.Vertex3(-200, 500, 400);
            GL.Vertex3(-200, 500, 0);
            GL.End();

            //porta de entrada pra sala
            GL.Color3(Color.Transparent);
            estrutura.paredeTextura(0, 200, -200, 0, 200, 100, texPorta);

            //estrutura.fazerParede(0, 400, -200, 0, 0, 500);
            GL.Color3(Color.SkyBlue);
            //chao da entrada da sala
            estrutura.fazerChao(0, 0, -200, 700, 0, 500);
            //teto
            estrutura.fazerChao(400, 400, -200, 700, 0, 500);

            //chao da sala
            GL.Color3(Color.ForestGreen);
            estrutura.fazerChao(0, 0, -200, 480, 0, -400);
            //teto
            estrutura.fazerChao(400, 400, -200, 480, 0, -400);

            //frente da casa
            estrutura.fazerParede(0, 50, -700, 0, -400, 800);

            GL.Begin(PrimitiveType.Quads);
            //parede de entrada do jardim de inverno
            GL.Color3(Color.Wheat);
            GL.Vertex3(-200, 500, 200);
            GL.Vertex3(-200, 500, 400);
            GL.Vertex3(500, 500, 400);
            GL.Vertex3(500, 500, 200);

            GL.Vertex3(-200, 500, 0);
            GL.Vertex3(-200, 500, 200);
            GL.Vertex3(200, 500, 200);
            GL.Vertex3(200, 500, 0);

            GL.Vertex3(400, 500, 0);
            GL.Vertex3(400, 500, 200);
            GL.Vertex3(500, 500, 200);
            GL.Vertex3(500, 500, 0);
            //---


            GL.Color3(Color.MediumAquamarine);
            GL.Vertex3(500, 500, 0);
            GL.Vertex3(500, 500, 400);
            GL.Vertex3(500, 0, 400);
            GL.Vertex3(500, 0, 0);

            GL.Vertex3(280, 0, 0);
            GL.Vertex3(280, 0, 400);
            GL.Vertex3(280, -400, 400);
            GL.Vertex3(280, -400, 0);
            GL.End();

            //Chao da cozinha
            GL.Color3(Color.BlueViolet);
            estrutura.fazerChao(0, 0, 500, 900, 100, -500);
            //teto da cozinha
            estrutura.fazerChao(400, 400, 500, 900, 100, -500);

            GL.Begin(PrimitiveType.Quads);
            ///Parede entrada da cozinha
            GL.Color3(Color.LimeGreen);
            GL.Vertex3(280, -400, 200);
            GL.Vertex3(280, -400, 400);
            GL.Vertex3(1400, -400, 400);
            GL.Vertex3(1400, -400, 200);

            GL.Vertex3(280, -400, 0);
            GL.Vertex3(280, -400, 200);
            GL.Vertex3(350, -400, 200);
            GL.Vertex3(350, -400, 0);

            GL.Vertex3(450, -400, 0);
            GL.Vertex3(450, -400, 400);
            GL.Vertex3(1200, -400, 400);
            GL.Vertex3(1200, -400, 0);

            GL.Vertex3(1300, -400, 0);
            GL.Vertex3(1300, -400, 400);
            GL.Vertex3(1400, -400, 400);
            GL.Vertex3(1400, -400, 0);
            //---

            GL.Color3(Color.CornflowerBlue);
            GL.Vertex3(280, -400, 0);
            GL.Vertex3(280, -400, 400);
            GL.Vertex3(-700, -400, 400);
            GL.Vertex3(-700, -400, 0);

            //chao da cozinha
            GL.Vertex3(700, -400, -100);
            GL.Vertex3(700, -800, -100);
            GL.Vertex3(-700, -800, -100);
            GL.Vertex3(-700, -400, -100);
            //teto
            GL.Vertex3(700, -400, 400);
            GL.Vertex3(700, -800, 400);
            GL.Vertex3(-700, -800, 400);
            GL.Vertex3(-700, -400, 400);
            GL.End();

            GL.Color3(Color.Gray);
            //parede do fogao da cozinha
            estrutura.paredeTextura(200, 400, 700, 0, -400, -400, texParede);
            estrutura.paredeTextura(-100, 100, 700, 0, -400, -400, texParede);
            estrutura.paredeTextura(-100, 400, 700, 0, -400, -100, texParede);
            estrutura.paredeTextura(-100, 400, 700, 0, -700, -100, texParede);
            //---

            GL.Begin(PrimitiveType.Quads);
            //porta do quintal
            GL.Color3(Color.LightGray);
            GL.Vertex3(700, -800, 200);
            GL.Vertex3(700, -800, 400);
            GL.Vertex3(-700, -800, 400);
            GL.Vertex3(-700, -800, 200);

            GL.Vertex3(-600, -800, -100);
            GL.Vertex3(-600, -800, 200);
            GL.Vertex3(-700, -800, 200);
            GL.Vertex3(-700, -800, -100);

            GL.Vertex3(-400, -800, -100);
            GL.Vertex3(-400, -800, 200);
            GL.Vertex3(700, -800, 200);
            GL.Vertex3(700, -800, -100);
            //---

            //parede da cozinha da frente da casa
            GL.Vertex3(-700, -400, -100);
            GL.Vertex3(-700, -400, 400);
            GL.Vertex3(-700, -800, 400);
            GL.Vertex3(-700, -800, -100);

            //muro do quintal
            GL.Color3(Color.DarkSeaGreen);
            GL.Vertex3(-700, -800, -200);
            GL.Vertex3(-700, -800, 500);
            GL.Vertex3(-700, -1700, 500);
            GL.Vertex3(-700, -1700, -200);

            GL.Vertex3(-700, -1700, -200);
            GL.Vertex3(-700, -1700, 500);
            GL.Vertex3(400, -1700, 500);
            GL.Vertex3(400, -1700, -200);
            GL.End();

            //chao do quintal dos cats
            
            GL.Color3(Color.Transparent);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texQuintal);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(3, 3); GL.Vertex3(-700, -800, -200);
            GL.TexCoord2(0, 3); GL.Vertex3(400, -800, -200);
            GL.TexCoord2(0, 0); GL.Vertex3(400, -1700, -200);
            GL.TexCoord2(3, 0); GL.Vertex3(-700, -1700, -200);
            GL.End();
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);

            //garagem
            GL.Color3(Color.Black);
            estrutura.fazerChao(-200, -200, 400, 2000, -800, -900);
            GL.Color3(Color.Blue);
            estrutura.fazerParede(-200, 400, 2400, 0, -800, -900);
            //parede de frente do quarto do meio
            GL.Color3(Color.SaddleBrown);
            estrutura.paredeTextura(-200, 400, 700, 1200, -800, texParede);
            estrutura.paredeTextura(-200, 100, 2100, 300, -800, texParede);

            


            //Portoes da garagem
            estrutura.paredeTextura(300, 500, 400, 2000, -1700, texParede);
            estrutura.paredeTextura(-200, 500, 400, 300, -1700, texParede);
            estrutura.paredeTextura(-200, 500, 1300, 350, -1700, texParede);
            estrutura.paredeTextura(-200, 500, 2250, 200, -1700, texParede);
            //textura do portao
            GL.Color3(Color.Transparent);
            estrutura.paredeTextura(-200, 300, 700, 600, -1700, texPortao);
            estrutura.paredeTextura(-200, 300, 1650, 600, -1700, texPortao);

            GL.Color3(Color.DodgerBlue);
            estrutura.paredeTextura(-200, 100, 1900, 0, -800, -200, texParede);
            estrutura.paredeTextura(-200, 100, 1900, 100, -1000, texParede);

            //teto
            GL.Color3(Color.BlueViolet);
            estrutura.fazerChao(400, 500, 400, 2000, -800, -900);
            //muro do quintal pra garagem
            GL.Color3(Color.Gray);
            estrutura.fazerParede(-200, -100, 400, 0, -800, -900);
            estrutura.fazerParede(-200, -100, 420, 0, -800, -900);
            estrutura.fazerChao(-100, -100, 400, 20, -800, -900);

            //quintal dos dogs para a lavanderia
            GL.Color3(Color.LightGray);

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texQuintal);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 5); GL.Vertex3(700,-400,-50);
            GL.TexCoord2(0, 0); GL.Vertex3(700,-800,-50);
            GL.TexCoord2(5, 0); GL.Vertex3(2500,-800,-50);
            GL.TexCoord2(5, 5); GL.Vertex3(2500, -400, -50);
            GL.End();
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
            
            //chao da escada
            estrutura.chaoTextura(-100, -100, 1900, 100, -800, -200, texQuintal);
            estrutura.chaoTextura(-150, -150, 2000, 100, -800, -200, texQuintal);
            GL.Color3(Color.DarkOrange);
            estrutura.fazerParede(-50, 0, 700, 1400, -400);

            GL.Begin(PrimitiveType.Quads);
            //Parede da cozinha
            GL.Color3(Color.Black);
            GL.Vertex3(1400, 100, 0);
            GL.Vertex3(1400, 100, 400);
            GL.Vertex3(1400, -400, 400);
            GL.Vertex3(1400, -400, 0);

            //corredor esquerdo
            GL.Color3(Color.Gray);
            GL.Vertex3(500, 300, 200);
            GL.Vertex3(500, 300, 400);
            GL.Vertex3(2000, 300, 400);
            GL.Vertex3(2000, 300, 200);

            GL.Vertex3(500, 300, 0);
            GL.Vertex3(500, 300, 200);
            GL.Vertex3(1000, 300, 200);
            GL.Vertex3(1000, 300, 0);

            GL.Vertex3(1200, 300, 0);
            GL.Vertex3(1200, 300, 200);
            GL.Vertex3(2000, 300, 200);
            GL.Vertex3(2000, 300, 0);

            //BANHEIRO DO CORREDOR
            GL.Color3(Color.BurlyWood);
            GL.Vertex3(1000, 300, 200);
            GL.Vertex3(1000, 300, 300);
            GL.Vertex3(1000, 500, 300);
            GL.Vertex3(1000, 500, 200);

            GL.Vertex3(1000, 300, 0);
            GL.Vertex3(1000, 300, 200);
            GL.Vertex3(1000, 350, 200);
            GL.Vertex3(1000, 350, 0);

            GL.Vertex3(1000, 440, 0);
            GL.Vertex3(1000, 440, 200);
            GL.Vertex3(1000, 500, 200);
            GL.Vertex3(1000, 500, 0);

            GL.Color3(Color.Black);
            //GL.Vertex3(800, 320, 0);
            //GL.Vertex3(800, 320, 400);
            //GL.Vertex3(1000, 320, 400);
            //GL.Vertex3(1000, 320, 0);

            GL.Vertex3(800, 500, 0);
            GL.Vertex3(800, 500, 300);
            GL.Vertex3(1000, 500, 300);
            GL.Vertex3(1000, 500, 0);

            GL.Color3(Color.Wheat);
            GL.Vertex3(800, 300, 0);
            GL.Vertex3(800, 300, 300);
            GL.Vertex3(800, 500, 300);
            GL.Vertex3(800, 500, 0);

            //---

            GL.Vertex3(1200, 300, 0);
            GL.Vertex3(1200, 300, 300);
            GL.Vertex3(1200, 500, 300);
            GL.Vertex3(1200, 500, 0);

            GL.Color3(Color.Firebrick);
            GL.Vertex3(1000, 500, 0);
            GL.Vertex3(1000, 500, 300);
            GL.Vertex3(1200, 500, 300);
            GL.Vertex3(1200, 500, 0);

            //corredor direito
            GL.Color3(Color.DarkGoldenrod);
            GL.Vertex3(700, 100, 200);
            GL.Vertex3(700, 100, 400);
            GL.Vertex3(2000, 100, 400);
            GL.Vertex3(2000, 100, 200);

            //porta do quarto do meio
            GL.Vertex3(700, 100, 0);
            GL.Vertex3(700, 100, 400);
            GL.Vertex3(1450, 100, 400);
            GL.Vertex3(1450, 100, 0);

            GL.Vertex3(1550, 100, 0);
            GL.Vertex3(1550, 100, 400);
            GL.Vertex3(2000, 100, 400);
            GL.Vertex3(2000, 100, 0);
            //---

            GL.Color3(Color.Blue);
            GL.Vertex3(2000, 300, 0);
            GL.Vertex3(2000, 300, 400);
            GL.Vertex3(2000, 100, 400);
            GL.Vertex3(2000, 100, 0);

            //Parede entrada do corredor
            GL.Vertex3(700, 300, 200);
            GL.Vertex3(700, 300, 400);
            GL.Vertex3(700, 100, 400);
            GL.Vertex3(700, 100, 200);

            GL.Vertex3(700, 300, 0);
            GL.Vertex3(700, 300, 200);
            GL.Vertex3(700, 270, 200);
            GL.Vertex3(700, 270, 0);

            GL.Vertex3(700, 150, 0);
            GL.Vertex3(700, 150, 200);
            GL.Vertex3(700, 100, 200);
            GL.Vertex3(700, 100, 0);
            //---

            //parede do quarto do meio c janela
            GL.Vertex3(1400, -400, 200);
            GL.Vertex3(1400, -400, 400);
            GL.Vertex3(1850, -400, 400);
            GL.Vertex3(1850, -400, 200);


            GL.Vertex3(1400, -400, 0);
            GL.Vertex3(1400, -400, 100);
            GL.Vertex3(1850, -400, 100);
            GL.Vertex3(1850, -400, 0);

            GL.Vertex3(1400, -400, 0);
            GL.Vertex3(1400, -400, 200);
            GL.Vertex3(1500, -400, 200);
            GL.Vertex3(1500, -400, 0);

            GL.Vertex3(1700, -400, 0);
            GL.Vertex3(1700, -400, 200);
            GL.Vertex3(1850, -400, 200);
            GL.Vertex3(1850, -400, 0);
            //---

            GL.End();

            //chao do corredor
            GL.Color3(Color.Transparent);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texChao);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(3, 1); GL.Vertex3(2000,300,0);
            GL.TexCoord2(0, 1); GL.Vertex3(700,300,0);
            GL.TexCoord2(0, 0); GL.Vertex3(700,100,0);
            GL.TexCoord2(3, 0); GL.Vertex3(2000,100,0);
            GL.End();
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);

            GL.Color3(Color.LightGray);
            //teto
            estrutura.fazerChao(400, 400, 700, 1300, 100, 200);

            //rodape da cozinha
            estrutura.fazerParede(-100, 0, 700, -1400, -400);
            estrutura.fazerParede(-200, -100, 700, -1400, -800);

            //parede do quarto do meio
            estrutura.paredeTextura(0, 400, 1850, 0, -400, 500, texParede);

            //chao do quarto do meio
            GL.Color3(Color.MediumVioletRed);
            estrutura.fazerChao(0, 0, 1400, 450, -400, 500);
            //teto
            estrutura.fazerChao(400, 400, 1400, 450, -400, 500);

            //chao do banheiro do corredor
            estrutura.fazerChao(0, 0, 800, 200, 300, 200);
            //teto
            estrutura.fazerChao(300, 300, 800, 200, 300, 200);

            //chao da abertura para o banheiro do corredor
            estrutura.fazerChao(0, 0, 1000, 200, 300, 200);
            //teto
            estrutura.fazerChao(300, 300, 1000, 200, 300, 200);

            //janela do quarto do meio - c textura
            GL.Color3(Color.Transparent);
            estrutura.paredeTextura(100, 200, 1500, 200, -400, 0, texJanela);

            //janela da cozinha

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texJanela);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 1); GL.Vertex3(700, -500, 100);
            GL.TexCoord2(0, 0); GL.Vertex3(700, -500, 200);
            GL.TexCoord2(1, 0); GL.Vertex3(700, -700, 200);
            GL.TexCoord2(1, 1); GL.Vertex3(700, -700, 100);
            GL.End();
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);


            //Objetos transparentes
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Color4(0.1f, 0.5f, 0.6f, 0.6f); //Último parâmetro é a porcentagem de trasparência
            
            //janela do quarto do meio
            //estrutura.fazerParede(100, 200, 1500, 200, -400, 0);

            estrutura.fazerParede(-100, 400, 400, 0, -800, -250);

            //porta do jardim de inverno
            estrutura.fazerParede(0, 200, 200, 200, 500, 0);

            //frente da casa
            estrutura.fazerParede(50, 220, -700, 0, -300, 400);

            //porta do corredor
            estrutura.fazerParede(0, 200, 700, 0, 150, 120);
            
            GL.Disable(EnableCap.Blend);

            glControl1.SwapBuffers(); //troca os buffers de frente e de fundo 

        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.GhostWhite);         // definindo a cor de limpeza do fundo da tela
            GL.Enable(EnableCap.Light0);

            texPorta = LoadTexture("../../Recursos/PortaFotografia.png");
            texParede = LoadTexture("../../Recursos/fotografia.jpg");
            texPortao = LoadTexture("../../Recursos/Portao.png");
            texJanela = LoadTexture("../../Recursos/Janela2.png");
            texChao = LoadTexture("../../Recursos/Chao.jpg");
            texQuintal = LoadTexture("../../Recursos/chaoQuintal.jpg");

            SetupViewport();                      //configura a janela de pintura
        }

        private void SetupViewport() //configura a janela de projeção 
        {
            int w = glControl1.Width; //largura da janela
            int h = glControl1.Height; //altura da janela

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1f, w / (float)h, 0.1f, 3000.0f);
            GL.LoadIdentity(); //zera a matriz de projeção com a matriz identidade

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Viewport(0, 0, w, h); // usa toda area de pintura da glcontrol
            lateral = w / 2;
            lateral2 = h / 2;
        }

        static int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id;//= GL.GenTexture(); 

            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return id;
        }
        private void calcula_direcao()
        {
            dir.X = pos.X + (Math.Sin(camera_rotation * Math.PI / 180) * 1000);
            dir.Y = pos.Y + (Math.Cos(camera_rotation * Math.PI / 180) * 1000);
            dir.Z = pos.Z + (Math.Tan(camera_rotation2 * Math.PI / 180) * 1000);
        }
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            Form_casa frmCasa = new Form_casa();
            if (e.X > lateral)
            {
                camera_rotation += 2;
            }
            if (e.X < lateral)
            {
                camera_rotation -= 2;
            }
            if (e.Y > lateral2)
            {
                camera_rotation2 -= 0.6f;
            }
            if (e.Y < lateral2)
            {
                camera_rotation2 += 0.6f;
            }

            label2.Text = e.X.ToString();
            lateral = e.X;
            lateral2 = e.Y;
            calcula_direcao();
            glControl1.Invalidate();
        }

        private void glControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            float a = camera_rotation;
            int tipoTecla = 0;
            int sinal = 1;

            if (e.KeyCode == Keys.Q)
            {
                pos.Z += 100;
                glControl1.Invalidate();
            }
            if (e.KeyCode == Keys.E)
            {
                pos.Z -= 100;
                glControl1.Invalidate();
            }
            if (e.KeyCode == Keys.A)
            {
                sinal = 0;
                a -= 90;
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.D)
            {
                sinal = 0;
                a += 90;
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.W)
            {
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.S)
            {
                sinal = -1;
                a += 180;
                tipoTecla = 1;
            }

            if (e.KeyCode == Keys.Right)
            {
                a += 3;
                tipoTecla = 2;
            }
            if (e.KeyCode == Keys.Left)
            {
                a -= 3;
                tipoTecla = 2;
            }
            if (e.KeyCode == Keys.Up)
            {
                pos.Z += 2;
            }
            if (e.KeyCode == Keys.Down)
            {
                pos.Z -= 2;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (tipoTecla == 1)
            {
                if (a < 0) a += 360;
                if (a > 360) a -= 360;
                //label2.Text = lateral.ToString();
                pos.X += (Math.Sin(a * Math.PI / 180) * 100);
                pos.Y += (Math.Cos(a * Math.PI / 180) * 100);
                pos.Z += (Math.Sin(camera_rotation2 * Math.PI / 180) * 100) * sinal;
                calcula_direcao();
                glControl1.Invalidate();
            }

            if (tipoTecla == 2)
            {
                camera_rotation = a;
                calcula_direcao();
                glControl1.Invalidate();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            glControl1.Width = Form_casa.ActiveForm.Width - 10;
            glControl1.Height = Form_casa.ActiveForm.Height - 10;
            SetupViewport();
            glControl1.Invalidate();
        }
    }
}
