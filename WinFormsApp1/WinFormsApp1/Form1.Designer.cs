namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxEps;
        private System.Windows.Forms.Button buttonCalc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFunc;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.Label labelN;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            textBoxX = new TextBox();
            textBoxEps = new TextBox();
            buttonCalc = new Button();
            label1 = new Label();
            label2 = new Label();
            labelFunc = new Label();
            labelSum = new Label();
            labelN = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBoxX
            // 
            textBoxX.Location = new Point(379, 127);
            textBoxX.Name = "textBoxX";
            textBoxX.Size = new Size(104, 23);
            textBoxX.TabIndex = 0;
            // 
            // textBoxEps
            // 
            textBoxEps.Location = new Point(78, 127);
            textBoxEps.Name = "textBoxEps";
            textBoxEps.Size = new Size(100, 23);
            textBoxEps.TabIndex = 1;
            // 
            // buttonCalc
            // 
            buttonCalc.Location = new Point(226, 157);
            buttonCalc.Name = "buttonCalc";
            buttonCalc.Size = new Size(117, 49);
            buttonCalc.TabIndex = 2;
            buttonCalc.Text = "Вычислить";
            buttonCalc.UseVisualStyleBackColor = true;
            buttonCalc.Click += buttonCalc_Click;
            // 
            // label1
            // 
            label1.Location = new Point(379, 101);
            label1.Name = "label1";
            label1.Size = new Size(120, 23);
            label1.TabIndex = 3;
            label1.Text = "Введите x:";
            // 
            // label2
            // 
            label2.Location = new Point(78, 101);
            label2.Name = "label2";
            label2.Size = new Size(120, 23);
            label2.TabIndex = 4;
            label2.Text = "Точность (eps):";
            // 
            // labelFunc
            // 
            labelFunc.Location = new Point(12, 210);
            labelFunc.Name = "labelFunc";
            labelFunc.Size = new Size(350, 23);
            labelFunc.TabIndex = 5;
            labelFunc.Text = "Math.Log(1 - x) = ";
            // 
            // labelSum
            // 
            labelSum.Location = new Point(12, 240);
            labelSum.Name = "labelSum";
            labelSum.Size = new Size(350, 23);
            labelSum.TabIndex = 6;
            labelSum.Text = "Сумма ряда = ";
            // 
            // labelN
            // 
            labelN.Location = new Point(12, 270);
            labelN.Name = "labelN";
            labelN.Size = new Size(350, 23);
            labelN.TabIndex = 7;
            labelN.Text = "Количество членов = ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources._5299023164413253062;
            pictureBox1.Location = new Point(45, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(495, 75);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            ClientSize = new Size(552, 309);
            Controls.Add(pictureBox1);
            Controls.Add(textBoxX);
            Controls.Add(textBoxEps);
            Controls.Add(buttonCalc);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(labelFunc);
            Controls.Add(labelSum);
            Controls.Add(labelN);
            Name = "Form1";
            Text = "Разложение в ряд";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private PictureBox pictureBox1;
    }
}