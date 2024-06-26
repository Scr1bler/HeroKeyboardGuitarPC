using AudioAnalyzing;
using HeroKeyboardGuitar.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HeroKeyboardGuitar;

internal partial class FrmMain : Form
{
    public List<Note> notes;
    private const float noteSpeed = 0.5f;
    private Audio curSong;
    private Score score;

    // for double buffering
    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
            return cp;
        }
    }

    public FrmMain()
    {
        InitializeComponent();
    }


    public void FrmMain_Load(object sender, EventArgs e)
    {
        score = new();
        lblScore.Text = score.Amount.ToString();
        panBg.BackgroundImage = Game.GetInstance().GetBg();
        panBg.Height = (int)(Height * .5);
        panBg.Width = (int)(Width * .5);
        //button1 = new Button();
        curSong = Game.GetInstance().CurSong;
        notes = new();
        foreach (var actionTime in curSong.ActionTimes)
        {
            double x = 100; //actionTime * noteSpeed + picTarget.Left + picTarget.Width;
            const int noteSize = 50;
            if (notes.Any(note => (x + note.Pic.Top) < noteSize / 2))
            {
                continue;
            }
            PictureBox picNote = new()
            {
                BackColor = Color.Black,
                ForeColor = Color.Black,
                Width = noteSize,
                Height = noteSize,
                Top = (int)x,
                Left = picTarget.Left + picTarget.Height / 2 - noteSize / 2,
                BackgroundImage = Resources.marker,
                BackgroundImageLayout = ImageLayout.Stretch,
                Anchor = AnchorStyles.Left,
            };
            Controls.Add(picNote);
            notes.Add(new(picNote, x));
        }
        Timer tmrWaitThenPlay = new()
        {
            Interval = 1000,
            Enabled = true,
        };
        components.Add(tmrWaitThenPlay);
        tmrWaitThenPlay.Tick += (e, sender) =>
        {
            Game.GetInstance().CurSong.Play();
            tmrWaitThenPlay.Enabled = false;
            tmrPlay.Enabled = true;
        };
    }

    public int disposed = 0;

    public void tmrPlay_Tick(object sender, EventArgs e)
    {
        int index = curSong.GetPosition();
        foreach (var note in notes)
        {
            note.Move(tmrPlay.Interval * (noteSpeed * 1.3));
            if (note.CheckMiss(picTarget))
            {
                score.Miss();
            }
        }
        if (index == curSong.GetNumberOfSamples() - 1)
        {
            tmrPlay.Enabled = false;
            foreach (var note in notes)
            {
                Controls.Remove(note.Pic);
                note.Dispose();
                disposed += 1;
                //win();
            }
            //MessageBox.Show("win");
            win();
        }
    }

    //public int disposed = 0;

    //public void Disposed()
    //{
    //    disposed += 1;

    //}


    private void FrmMain_KeyPress(object sender, KeyPressEventArgs e)
    {
        foreach (var note in notes)
        {
            if (note.CheckHit(picTarget))
            {
                score.Add(1);
                lblScore.Text = score.Amount.ToString();
                lblScore.Font = new("Arial", 42);
                break;
            }
        }
    }

    private void FrmMain_KeyDown(object sender, KeyEventArgs e)
    {
        picTarget.BackgroundImage = Resources.pressed;
    }

    private void FrmMain_KeyUp(object sender, KeyEventArgs e)
    {
        picTarget.BackgroundImage = Resources._default;
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        Game.GetInstance().CurSong.Stop();
    }

    private void tmrScoreShrink_Tick(object sender, EventArgs e)
    {
        if (lblScore.Font.Size > 20)
        {
            lblScore.Font = new("Arial", lblScore.Font.Size - 1);
        }
    }

    private void picTarget_Click(object sender, EventArgs e)
    {

    }

    private void panBg_Paint(object sender, PaintEventArgs e)
    {

    }




    //private Audio curSong;
    public void win()
    {
        //FrmMain dispose = new FrmMain();
        //curSong = Game.GetInstance().CurSong;
        WinScreen winScreen = new WinScreen();
        winScreen.Show();
    }

    public void CheckFail()
    {
        curSong = Game.GetInstance().CurSong;
        if (Note.misses == 5)
        {
            Game.GetInstance().CurSong.Stop();
        }
    }

    //private void button1_Click(object sender, EventArgs e)
    //{
    //    WinScreen winScreen = new WinScreen();
    //    winScreen.Show();
    //}

    private void lblScore_Click(object sender, EventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
