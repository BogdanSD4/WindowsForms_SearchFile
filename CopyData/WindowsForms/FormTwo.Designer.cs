
namespace WindowsForms
{
    partial class FormTwo
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
            this.TextBoxConsole = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TextBoxConsole
            // 
            this.TextBoxConsole.AcceptsReturn = true;
            this.TextBoxConsole.AcceptsTab = true;
            this.TextBoxConsole.AllowDrop = true;
            this.TextBoxConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxConsole.Location = new System.Drawing.Point(16, 17);
            this.TextBoxConsole.Multiline = true;
            this.TextBoxConsole.Name = "TextBoxConsole";
            this.TextBoxConsole.ReadOnly = true;
            this.TextBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxConsole.ShortcutsEnabled = false;
            this.TextBoxConsole.Size = new System.Drawing.Size(396, 421);
            this.TextBoxConsole.TabIndex = 0;
            this.TextBoxConsole.UseWaitCursor = true;
            this.TextBoxConsole.WordWrap = false;
            // 
            // FormTwo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 450);
            this.Controls.Add(this.TextBoxConsole);
            this.Name = "FormTwo";
            this.Text = "FormTwo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxConsole;
    }
}