namespace ASCIIDrawing
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        void drawImageToASCII(Bitmap simpBitmap)
        {
            richTextBox1.Clear();
            bool hasColor = checkBox1.Checked;
            bool useSingleChar = checkBox2.Checked;
            string sig = " ";
            if (useSingleChar && hasColor) sig = textBox2.Text;

            UnsafeImageTool tool = new UnsafeImageTool(simpBitmap);
            for (int i = 0; i < simpBitmap.Height; i++)
            {
                for (int j = 0; j < simpBitmap.Width; j++)
                {
                    if (hasColor)
                    {
                        richTextBox1.SelectionColor = tool.GetPixelColor(j, i);
                        if (useSingleChar)
                        {
                            richTextBox1.AppendText(sig);
                        }
                        else
                        {
                            richTextBox1.AppendText(tool.GetPixelCharact(j, i).ToString());
                        }
                    }
                    else
                    {
                        richTextBox1.AppendText(tool.GetPixelCharact(j, i).ToString());
                    }
                }
                richTextBox1.AppendText("\r\n");
            }
            richTextBox1.SelectionColor = Color.Black;
            tool.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap b = (Bitmap)Image.FromFile(op.FileName);
                    string[] sp = textBox1.Text.Split(',');
                    int width = int.Parse(sp[0]);
                    int height = (int)(((double)width / b.Width) * b.Height / 2);
                    Bitmap convert = new Bitmap(width, height);
                    Graphics.FromImage(convert).DrawImage(b, 0, 0, width, height);
                    drawImageToASCII(convert);
                }
                catch
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sv = new SaveFileDialog();
            sv.Filter = "RTF File|*.rtf";
            if(sv.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(sv.FileName);
            }
        }
    }
}
