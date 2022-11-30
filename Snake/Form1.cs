namespace Snake
{
    public partial class Form1 : Form
    {
        Yilan yilan;
        Dictionary<int, string> Yon = new Dictionary<int, string>();
        
        public Form1()
        {
            InitializeComponent();
           
        }
        //protected override bool ProcessCmdKey(ref Message msg, System.Windows.Forms.Keys keyData)
        //{
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;            
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            Yon.Add(37, "sol");
            Yon.Add(38, "yukari");
            Yon.Add(39, "sag");
            Yon.Add(40, "asagi");
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {
            pnlGame.Controls.Clear(); // Oyun alanýný temizle
            yilan = new Yilan(pnlGame);
            
            //yilan.YilanOlustur();
            yilan.Boyut = 3;
            yilan.Hiz = 200;
            yilan.Renk = Color.Green;

            //pnlGame.Controls.AddRange(yilan.YilanParca.ToArray());
            timerYilan.Start();
            yilan.YemVarMi = false;
        }

        private void timerYilan_Tick(object sender, EventArgs e)
        {
            timerYilan.Interval = yilan.Hiz;
            yilan.YemKontrol();
            yilan.YilanHareket();
            lblSkor.Text = yilan.Skor.ToString();
            pnlGame.Controls.Remove(yilan.YilanParca.ToArray()[0]);
            pnlGame.Controls.AddRange(yilan.YilanParca.ToArray());
            
            if (!yilan.YilanSinirKontrol() || !yilan.YilanKontrol())
            {
                timerYilan.Stop();
                MessageBox.Show("Oyun Bitti");
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
         {
            int key = e.KeyValue;
            if (Yon.ContainsKey(e.KeyValue))
            {
                if (e.KeyCode == Keys.Left && yilan.Yon != "sag")
                {
                    yilan.Yon = Yon[e.KeyValue];
                }
                else if (e.KeyCode == Keys.Up && yilan.Yon != "asagi")
                {
                    yilan.Yon = Yon[e.KeyValue];
                }
                else if (e.KeyCode == Keys.Right && yilan.Yon != "sol")
                {
                    yilan.Yon = Yon[e.KeyValue];
                }
                else if (e.KeyCode == Keys.Down && yilan.Yon != "yukari")
                {
                    yilan.Yon = Yon[e.KeyValue];
                }
               
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                timerYilan.Enabled = !timerYilan.Enabled;
            }
        }
    }
}