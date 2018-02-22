namespace Sample_SignIn
{
    partial class Information_Account
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
            this.button1 = new System.Windows.Forms.Button();
            this.txt_email2 = new System.Windows.Forms.TextBox();
            this.btn_changepasswd = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_changepasswd1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_repsswd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_currentpswd = new System.Windows.Forms.TextBox();
            this.txt_newpswd = new System.Windows.Forms.TextBox();
            this.txt_email1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_username3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(293, 497);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 47);
            this.button1.TabIndex = 26;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_email2
            // 
            this.txt_email2.Location = new System.Drawing.Point(109, 76);
            this.txt_email2.Margin = new System.Windows.Forms.Padding(4);
            this.txt_email2.Multiline = true;
            this.txt_email2.Name = "txt_email2";
            this.txt_email2.Size = new System.Drawing.Size(299, 24);
            this.txt_email2.TabIndex = 25;
            // 
            // btn_changepasswd
            // 
            this.btn_changepasswd.Location = new System.Drawing.Point(259, 169);
            this.btn_changepasswd.Margin = new System.Windows.Forms.Padding(4);
            this.btn_changepasswd.Name = "btn_changepasswd";
            this.btn_changepasswd.Size = new System.Drawing.Size(151, 39);
            this.btn_changepasswd.TabIndex = 23;
            this.btn_changepasswd.Text = "Change a password";
            this.btn_changepasswd.UseVisualStyleBackColor = true;
            this.btn_changepasswd.Click += new System.EventHandler(this.btn_changepasswd_Click);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(111, 169);
            this.btn_update.Margin = new System.Windows.Forms.Padding(4);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(140, 39);
            this.btn_update.TabIndex = 22;
            this.btn_update.Text = "Update Email";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_changepasswd1
            // 
            this.btn_changepasswd1.Location = new System.Drawing.Point(217, 196);
            this.btn_changepasswd1.Margin = new System.Windows.Forms.Padding(4);
            this.btn_changepasswd1.Name = "btn_changepasswd1";
            this.btn_changepasswd1.Size = new System.Drawing.Size(151, 39);
            this.btn_changepasswd1.TabIndex = 14;
            this.btn_changepasswd1.Text = "Update Password";
            this.btn_changepasswd1.UseVisualStyleBackColor = true;
            this.btn_changepasswd1.Click += new System.EventHandler(this.btn_changepasswd1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 80);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 24;
            this.label6.Text = "Email:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_changepasswd1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_repsswd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_currentpswd);
            this.groupBox1.Controls.Add(this.txt_newpswd);
            this.groupBox1.Location = new System.Drawing.Point(20, 236);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(389, 254);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Change a password";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "New Password: ";
            // 
            // txt_repsswd
            // 
            this.txt_repsswd.Location = new System.Drawing.Point(151, 146);
            this.txt_repsswd.Margin = new System.Windows.Forms.Padding(4);
            this.txt_repsswd.Multiline = true;
            this.txt_repsswd.Name = "txt_repsswd";
            this.txt_repsswd.PasswordChar = '*';
            this.txt_repsswd.Size = new System.Drawing.Size(216, 24);
            this.txt_repsswd.TabIndex = 9;
            this.txt_repsswd.TextChanged += new System.EventHandler(this.txt_repsswd_TextChanged);
            this.txt_repsswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_repsswd_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current Passwor:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 150);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Re-Type Password: ";
            // 
            // txt_currentpswd
            // 
            this.txt_currentpswd.Location = new System.Drawing.Point(151, 41);
            this.txt_currentpswd.Margin = new System.Windows.Forms.Padding(4);
            this.txt_currentpswd.Multiline = true;
            this.txt_currentpswd.Name = "txt_currentpswd";
            this.txt_currentpswd.PasswordChar = '*';
            this.txt_currentpswd.Size = new System.Drawing.Size(216, 24);
            this.txt_currentpswd.TabIndex = 5;
            this.txt_currentpswd.TextChanged += new System.EventHandler(this.txt_currentpswd_TextChanged);
            // 
            // txt_newpswd
            // 
            this.txt_newpswd.Location = new System.Drawing.Point(151, 92);
            this.txt_newpswd.Margin = new System.Windows.Forms.Padding(4);
            this.txt_newpswd.Multiline = true;
            this.txt_newpswd.Name = "txt_newpswd";
            this.txt_newpswd.PasswordChar = '*';
            this.txt_newpswd.Size = new System.Drawing.Size(216, 24);
            this.txt_newpswd.TabIndex = 7;
            // 
            // txt_email1
            // 
            this.txt_email1.Location = new System.Drawing.Point(109, 124);
            this.txt_email1.Margin = new System.Windows.Forms.Padding(4);
            this.txt_email1.Multiline = true;
            this.txt_email1.Name = "txt_email1";
            this.txt_email1.Size = new System.Drawing.Size(299, 24);
            this.txt_email1.TabIndex = 20;
            this.txt_email1.TextChanged += new System.EventHandler(this.txt_email1_TextChanged_1);
            this.txt_email1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_email1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 128);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "New Email:";
            // 
            // txt_username3
            // 
            this.txt_username3.Location = new System.Drawing.Point(111, 26);
            this.txt_username3.Margin = new System.Windows.Forms.Padding(4);
            this.txt_username3.Multiline = true;
            this.txt_username3.Name = "txt_username3";
            this.txt_username3.Size = new System.Drawing.Size(297, 24);
            this.txt_username3.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "User name: ";
            // 
            // Information_Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 551);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_email2);
            this.Controls.Add(this.btn_changepasswd);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_email1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_username3);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Information_Account";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Information";
            this.Load += new System.EventHandler(this.Information_Account_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_email2;
        private System.Windows.Forms.Button btn_changepasswd;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_changepasswd1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_repsswd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_currentpswd;
        private System.Windows.Forms.TextBox txt_newpswd;
        private System.Windows.Forms.TextBox txt_email1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_username3;
        private System.Windows.Forms.Label label1;
    }
}