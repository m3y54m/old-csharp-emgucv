namespace myColorMask
{
    partial class ColorMask
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
            this.components = new System.ComponentModel.Container();
            this.maskedFrame = new Emgu.CV.UI.ImageBox();
            this.liveCamera = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioPurple = new System.Windows.Forms.RadioButton();
            this.radioCyan = new System.Windows.Forms.RadioButton();
            this.radioYellow = new System.Windows.Forms.RadioButton();
            this.radioBlue = new System.Windows.Forms.RadioButton();
            this.radioGreen = new System.Windows.Forms.RadioButton();
            this.radioRed = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioCircle = new System.Windows.Forms.RadioButton();
            this.radioRectangle = new System.Windows.Forms.RadioButton();
            this.radioPentagon = new System.Windows.Forms.RadioButton();
            this.radioTriangle = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.maskedFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liveCamera)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // maskedFrame
            // 
            this.maskedFrame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.maskedFrame.Location = new System.Drawing.Point(11, 25);
            this.maskedFrame.Name = "maskedFrame";
            this.maskedFrame.Size = new System.Drawing.Size(400, 300);
            this.maskedFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.maskedFrame.TabIndex = 2;
            this.maskedFrame.TabStop = false;
            // 
            // liveCamera
            // 
            this.liveCamera.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.liveCamera.Location = new System.Drawing.Point(421, 25);
            this.liveCamera.Name = "liveCamera";
            this.liveCamera.Size = new System.Drawing.Size(400, 300);
            this.liveCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.liveCamera.TabIndex = 4;
            this.liveCamera.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(163, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "خروجی فیلتر شده";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.Location = new System.Drawing.Point(600, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "تصویر زنده";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioPurple);
            this.groupBox1.Controls.Add(this.radioCyan);
            this.groupBox1.Controls.Add(this.radioYellow);
            this.groupBox1.Controls.Add(this.radioBlue);
            this.groupBox1.Controls.Add(this.radioGreen);
            this.groupBox1.Controls.Add(this.radioRed);
            this.groupBox1.Location = new System.Drawing.Point(430, 339);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(158, 91);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "فیلتر رنگ";
            // 
            // radioPurple
            // 
            this.radioPurple.AutoSize = true;
            this.radioPurple.Location = new System.Drawing.Point(41, 43);
            this.radioPurple.Name = "radioPurple";
            this.radioPurple.Size = new System.Drawing.Size(52, 17);
            this.radioPurple.TabIndex = 5;
            this.radioPurple.Text = "بنفش";
            this.radioPurple.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioPurple.UseVisualStyleBackColor = true;
            // 
            // radioCyan
            // 
            this.radioCyan.AutoSize = true;
            this.radioCyan.Location = new System.Drawing.Point(10, 66);
            this.radioCyan.Name = "radioCyan";
            this.radioCyan.Size = new System.Drawing.Size(83, 17);
            this.radioCyan.TabIndex = 4;
            this.radioCyan.Text = "آبی آسمانی";
            this.radioCyan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioCyan.UseVisualStyleBackColor = true;
            // 
            // radioYellow
            // 
            this.radioYellow.AutoSize = true;
            this.radioYellow.Location = new System.Drawing.Point(55, 20);
            this.radioYellow.Name = "radioYellow";
            this.radioYellow.Size = new System.Drawing.Size(38, 17);
            this.radioYellow.TabIndex = 3;
            this.radioYellow.Text = "زرد";
            this.radioYellow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioYellow.UseVisualStyleBackColor = true;
            // 
            // radioBlue
            // 
            this.radioBlue.AutoSize = true;
            this.radioBlue.Location = new System.Drawing.Point(103, 43);
            this.radioBlue.Name = "radioBlue";
            this.radioBlue.Size = new System.Drawing.Size(42, 17);
            this.radioBlue.TabIndex = 2;
            this.radioBlue.Text = "آبی";
            this.radioBlue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioBlue.UseVisualStyleBackColor = true;
            // 
            // radioGreen
            // 
            this.radioGreen.AutoSize = true;
            this.radioGreen.Location = new System.Drawing.Point(100, 66);
            this.radioGreen.Name = "radioGreen";
            this.radioGreen.Size = new System.Drawing.Size(45, 17);
            this.radioGreen.TabIndex = 1;
            this.radioGreen.Text = "سبز";
            this.radioGreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioGreen.UseVisualStyleBackColor = true;
            // 
            // radioRed
            // 
            this.radioRed.AutoSize = true;
            this.radioRed.Checked = true;
            this.radioRed.Location = new System.Drawing.Point(99, 20);
            this.radioRed.Name = "radioRed";
            this.radioRed.Size = new System.Drawing.Size(46, 17);
            this.radioRed.TabIndex = 0;
            this.radioRed.TabStop = true;
            this.radioRed.Text = "قرمز";
            this.radioRed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioRed.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtY);
            this.groupBox2.Controls.Add(this.txtX);
            this.groupBox2.Location = new System.Drawing.Point(102, 339);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(158, 91);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "مرکز جرم";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "عرض";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "طول";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(25, 56);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(79, 21);
            this.txtY.TabIndex = 1;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(25, 29);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(79, 21);
            this.txtX.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(594, 345);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(111, 85);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "شروع";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(711, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 85);
            this.button1.TabIndex = 12;
            this.button1.Text = "پردازش تصویر";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioCircle);
            this.groupBox3.Controls.Add(this.radioRectangle);
            this.groupBox3.Controls.Add(this.radioPentagon);
            this.groupBox3.Controls.Add(this.radioTriangle);
            this.groupBox3.Location = new System.Drawing.Point(266, 339);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox3.Size = new System.Drawing.Size(158, 91);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "شکل";
            // 
            // radioCircle
            // 
            this.radioCircle.AutoSize = true;
            this.radioCircle.Location = new System.Drawing.Point(32, 56);
            this.radioCircle.Name = "radioCircle";
            this.radioCircle.Size = new System.Drawing.Size(46, 17);
            this.radioCircle.TabIndex = 3;
            this.radioCircle.Text = "دایره";
            this.radioCircle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioCircle.UseVisualStyleBackColor = true;
            // 
            // radioRectangle
            // 
            this.radioRectangle.AutoSize = true;
            this.radioRectangle.Location = new System.Drawing.Point(81, 56);
            this.radioRectangle.Name = "radioRectangle";
            this.radioRectangle.Size = new System.Drawing.Size(67, 17);
            this.radioRectangle.TabIndex = 2;
            this.radioRectangle.Text = "مستطیل";
            this.radioRectangle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioRectangle.UseVisualStyleBackColor = true;
            // 
            // radioPentagon
            // 
            this.radioPentagon.AutoSize = true;
            this.radioPentagon.Location = new System.Drawing.Point(5, 29);
            this.radioPentagon.Name = "radioPentagon";
            this.radioPentagon.Size = new System.Drawing.Size(73, 17);
            this.radioPentagon.TabIndex = 1;
            this.radioPentagon.Text = "پنج ضلعی";
            this.radioPentagon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioPentagon.UseVisualStyleBackColor = true;
            // 
            // radioTriangle
            // 
            this.radioTriangle.AutoSize = true;
            this.radioTriangle.Checked = true;
            this.radioTriangle.Location = new System.Drawing.Point(99, 29);
            this.radioTriangle.Name = "radioTriangle";
            this.radioTriangle.Size = new System.Drawing.Size(49, 17);
            this.radioTriangle.TabIndex = 0;
            this.radioTriangle.TabStop = true;
            this.radioTriangle.Text = "مثلث";
            this.radioTriangle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioTriangle.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 382);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ColorMask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 448);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.liveCamera);
            this.Controls.Add(this.maskedFrame);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorMask";
            this.Text = "فیلتر رنگ";
            ((System.ComponentModel.ISupportInitialize)(this.maskedFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liveCamera)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox maskedFrame;
        private Emgu.CV.UI.ImageBox liveCamera;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioBlue;
        private System.Windows.Forms.RadioButton radioGreen;
        private System.Windows.Forms.RadioButton radioRed;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RadioButton radioPurple;
        private System.Windows.Forms.RadioButton radioCyan;
        private System.Windows.Forms.RadioButton radioYellow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioCircle;
        private System.Windows.Forms.RadioButton radioRectangle;
        private System.Windows.Forms.RadioButton radioPentagon;
        private System.Windows.Forms.RadioButton radioTriangle;
        private System.Windows.Forms.Button button2;
    }
}

