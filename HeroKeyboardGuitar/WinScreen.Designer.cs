﻿using System;

namespace HeroKeyboardGuitar
{
    partial class WinScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // WinScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.WinImage;
            ClientSize = new System.Drawing.Size(555, 305);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "WinScreen";
            Text = "Form1";
            ResumeLayout(false);
        }

        //private void WinScreen_Load(object sender, EventArgs e)
        //{
        //S
        //}

        #endregion
    }
}