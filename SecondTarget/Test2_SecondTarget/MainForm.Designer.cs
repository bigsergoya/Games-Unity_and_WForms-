namespace Test2_SecondTarget
{
    partial class MainForm
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
            this.customControll1 = new Test2_SecondTarget.CustomControll();
            this.SuspendLayout();
            // 
            // customControll1
            // 
            this.customControll1.Dock = System.Windows.Forms.DockStyle.Top;
            this.customControll1.Location = new System.Drawing.Point(0, 0);
            this.customControll1.Margin = new System.Windows.Forms.Padding(0);
            this.customControll1.Name = "customControll1";
            this.customControll1.Size = new System.Drawing.Size(234, 183);
            this.customControll1.TabIndex = 0;
            this.customControll1.Text = "customControll1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 183);
            this.Controls.Add(this.customControll1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MMiner";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControll customControll1;
    }
}