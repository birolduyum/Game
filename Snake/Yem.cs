using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Yem
    {
        public Panel GamePanel; // Oyun alanı
        public Panel YemPanel { get; set; }
        public Point Konum { get; set; }
        public Color color { get; set; }
        public Yem(Panel gamePanel, Point yemKonum, Color renk)
        {
            Konum = yemKonum;
            YemPanel = new Panel();
            YemPanel.BackColor = renk;
            YemPanel.Size = new Size(20, 20);
            Random random = new Random();
            int x = random.Next(20, 580);
            int y = random.Next(20, 580);
            x -= x % 20;
            y -= y % 20;
            
            YemPanel.Location = Konum;
            gamePanel.Controls.Add(YemPanel);
        }
    }
}
