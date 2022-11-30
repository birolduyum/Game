using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Yilan
    {
        public Panel GamePanel; // Oyun alanı
        public Yem yem;
        public bool Yasiyor = true;
        public List<Point> Konum { get; set; }
        public List<Panel> YilanParca { get; set; }
        public string Yon { get; set; }
        public Color Renk { get; set; } = Color.Green;
        public int Hiz { get; set; } = 200;
        public int Boyut { get; set; } = 3;
        public int Skor { get; set; } = 0;
        public bool YemVarMi { get; set; }

        public Yilan(Panel gamePanel)
        {
            Konum = new List<Point>(); // Yılanın başlangıç konumu    
            YilanParca = new List<Panel>();
            Yon = "sag";
            Renk = Color.Green;
            Hiz = 100;
            Skor = 0;
            YemVarMi = false;
            GamePanel = gamePanel;
            YilanOlustur();  
        }
        private int HizArttir()
        {
            if (Skor % 5 == 0)
            {
                Hiz -= Hiz * 20 / 100;
            }

            return Hiz;
        }
        private void ParcaEkle()
        {
            Panel pnl = new Panel();
            pnl.BackColor = Renk;
            pnl.Size = new Size(20, 20);

            pnl.Location = Konum[Boyut - 1];
            YilanParca.Add(pnl);

        }
        private void YilanOlustur()
        {
            this.YilanParca.Clear();
            Random random = new Random();
            int x = random.Next(60, 540);
            int y = random.Next(60, 540);
            x -= x % 20;
            y -= y % 20;

            for (int i = 0; i < Boyut; i++)
            {
                Panel pnl = new Panel();
                pnl.BackColor = Renk;
                pnl.Size = new Size(20, 20);
                if (i == 0)
                {
                    Konum.Add(new Point(x, y));
                    pnl.Location = Konum[0];
                }
                else
                {
                    pnl.Location = new Point(Konum[0].X - (i * 20), Konum[0].Y);
                    Konum.Add(new Point(pnl.Location.X, pnl.Location.Y));

                }
                YilanParca.Add(pnl);
            }
            GamePanel.Controls.AddRange(YilanParca.ToArray());
        }
        public void YilanHareket()
        {
            switch (Yon)
            {
                case "sag":
                    if (Konum[0].X < 580)
                    {
                        Konum[0] = new Point(Konum[0].X + 20, Konum[0].Y);
                    }
                    else
                    {
                        Konum[0] = new Point(0, Konum[0].Y);
                    }
                    break;
                case "sol":
                    if (Konum[0].X > 0)
                    {
                        Konum[0] = new Point(Konum[0].X - 20, Konum[0].Y);
                    }
                    else
                    {
                        Konum[0] = new Point(580, Konum[0].Y);
                    }
                    break;
                case "yukari":
                    if (Konum[0].Y > 0)
                    {
                        Konum[0] = new Point(Konum[0].X, Konum[0].Y - 20);
                    }
                    else
                    {
                        Konum[0] = new Point(Konum[0].X, 580);
                    }
                    break;
                case "asagi":
                    if (Konum[0].Y < 580)
                    {
                        Konum[0] = new Point(Konum[0].X, Konum[0].Y + 20);
                    }
                    else
                    {
                        Konum[0] = new Point(Konum[0].X, 0);
                    }
                    break;
            }


            for (int i = 1; i < YilanParca.Count; i++)
            {
                Konum[i] = YilanParca[i - 1].Location;
            }
            for (int i = 0; i < YilanParca.Count; i++)
            {
                YilanParca[i].Location = Konum[i];
            }
            
        }
        public void YilanYem()
        {
            if (YemVarMi == false)
            {
                Random random = new Random();
                int x = random.Next(20, 580);
                int y = random.Next(20, 580);
                x -= x % 20;
                y -= y % 20;
                yem = new Yem(GamePanel, new Point(x, y), Color.Yellow);
                YemVarMi = true;
            }
        }
        public void YemKontrol()
        {
            if (yem != null)
            {
                if (YilanParca[0].Location == yem.Konum)
                {
                    YemVarMi = false;
                    YilanYem();
                    GamePanel.Controls.Remove(yem.YemPanel);
                    ParcaEkle();
                    Skor++;
                    HizArttir();
                    Konum.Add(new Point(YilanParca[YilanParca.Count - 1].Location.X, YilanParca[YilanParca.Count - 1].Location.Y));
                }
            }

        }
        public bool YilanKontrol()
        {
            for (int i = 1; i < YilanParca.Count; i++)
            {
                if (YilanParca[0].Location == YilanParca[i].Location)
                {
                    RenkAyarla();
                    Yasiyor = false;
                }
            }
            return Yasiyor;
        }
        public bool YilanSinirKontrol()
        {
            if (YilanParca[0].Location.X <= 0 || YilanParca[0].Location.X >= 580 || YilanParca[0].Location.Y <= 0 || YilanParca[0].Location.Y >= 580)
            {
                Yasiyor = false;
                RenkAyarla(); 

            }
            return Yasiyor;
        }
        private void RenkAyarla()  
        {
            if (!Yasiyor)
            {
                foreach (Panel panel in YilanParca)
                {
                    panel.BackColor = Color.Red;
                }
            }            
        }
    }
}
