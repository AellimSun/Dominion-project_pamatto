using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace DominionApp
{
    // 마녀 카드 서서히 등장
    public partial class Attack_Witch : Form
    {
          
        public Attack_Witch()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void Form4_Load(object sender, EventArgs e)
        {

            pbCrow.Image = Properties.Resources.Crow;
            FormEffect();
        }

        private async void FormEffect()
        {
            await Task.Run(async () =>
            {
                double[] opacity = new double[] { 0.05d, 0.1d, 0.15d, 0.2d, 0.25d, 0.3d, 0.4d, 0.5d, 0.7d, 0.8d, 0.9d, 1.0d };
                int cnt = 0;
                while (true)
                {
                    await Task.Delay(300);
                    pbWitch.Image = ChangeOpacity(Properties.Resources.witch, (float)opacity[cnt++]);
                    if (cnt + 1 > opacity.Length) break;
                }
                await Task.Delay(2000);
                this.Close();
            });                
        }

        public Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();
            return bmp;
        }

        private void pbCrow_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
