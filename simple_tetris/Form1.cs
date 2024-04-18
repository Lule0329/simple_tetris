using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simple_tetris
{
    public partial class Form1 : Form
    {
        int boxSize = 32;// Storleken på varje fyrkant
        int playWidth = 5;// Hur många fyrkanter som ska få plats på bredden
        int playHeight = 10;// Hur många fyrkanter som ska få plats på höjden

        PictureBox current = null;

        public Form1()
        {
            InitializeComponent();
        }

        void addBox()
        {
            // Skapa en ny PictureBox och sätt variabeln current till den pictureboxen
            current = new PictureBox();
            current.Size = new Size(boxSize, boxSize);

            // Ändra den här så att lådan börjar i mitten
            current.Location = new Point(label1.Left, label1.Top);

            current.BackColor = Color.Black;
            Controls.Add(current);

            // Säg att allt i fönstret ska ligga framför label1 så att lådorna alltid syns
            label1.SendToBack();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Sätt storleken på spelplanen
            label1.Height = boxSize * playHeight;
            label1.Width = boxSize * playWidth;

            // Kolla på mitt exempel och gör så att fönstret ändrar storlek efter hur stor spelplanen är
            // se också till att flytta label2 och button1 så att dom ligger på rätt ställe
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Om current är tom, alltså att ingen fyrkant är aktiv
            if(current == null)
            {
                // Lägg till en låda
                addBox();
            }
            else
            {
                // Flytta lådan ett steg nedåt
                if(current.Top + current.Height < label1.Height)
                {
                    if (intersectsWithBoxes() == false)
                    {
                        current.Top += boxSize;
                    }
                }
                else
                {
                    addBox();
                }   
            }
        }

        bool intersectsWithBoxes()
        {
            // Skapa en ny rektangel med nuvarande lådans position och storlek
            // Genom att skapa en kopia så kan man manipulera den utan att ändra lådans Bounds
            Rectangle bounds = new Rectangle(current.Location, current.Size);
            
            // Lägg till 1 på höjden så att den kollar positionen under där lådan är
            bounds.Height++;

            // Loopa igenom alla lådor
            foreach (var box in boxes)
            {
                if (box == current)
                {
                    continue;
                }

                // Om lådan är innuti lådan definierad i variabeln "bounds"
                if (box.Bounds.IntersectsWith(bounds))
                {
                    return true;
                }
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            // Sätt fokus på fönstret, det ser till att saker så som Form_KeyPress fungerar som det ska
            Focus();
            timer1.Start();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'd')
            {
                if (current.Left + current.Width < label1.Width)
                {
                    current.Left += boxSize;
                }
            }
            else if (e.KeyChar == 'a')
            {
                if (current.Left > 0)
                {
                    current.Left -= boxSize;
                }
            }
        }

        bool CollidesWith()
        {
            if (!current.Bounds.IntersectsWith(label1.Bounds))
            {
                return true;
            }
            else if (current.Bounds.IntersectsWith(current.Bounds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
