namespace BitTradeUpdater
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.lbProject = new System.Windows.Forms.ListBox();
            this.lbCurrent2 = new System.Windows.Forms.Label();
            this.lbNew2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDescript = new System.Windows.Forms.TextBox();
            this.lbNew = new System.Windows.Forms.Label();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.tbCmdOutput = new System.Windows.Forms.TextBox();
            this.btnBuild = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project:";
            // 
            // lbProject
            // 
            this.lbProject.FormattingEnabled = true;
            this.lbProject.ItemHeight = 16;
            this.lbProject.Location = new System.Drawing.Point(14, 29);
            this.lbProject.Margin = new System.Windows.Forms.Padding(4);
            this.lbProject.Name = "lbProject";
            this.lbProject.Size = new System.Drawing.Size(325, 148);
            this.lbProject.TabIndex = 1;
            this.lbProject.SelectedIndexChanged += new System.EventHandler(this.lbProject_SelectedIndexChanged);
            // 
            // lbCurrent2
            // 
            this.lbCurrent2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCurrent2.AutoSize = true;
            this.lbCurrent2.Location = new System.Drawing.Point(11, 185);
            this.lbCurrent2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCurrent2.Name = "lbCurrent2";
            this.lbCurrent2.Size = new System.Drawing.Size(102, 16);
            this.lbCurrent2.TabIndex = 2;
            this.lbCurrent2.Text = "Current Version:";
            // 
            // lbNew2
            // 
            this.lbNew2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbNew2.AutoSize = true;
            this.lbNew2.Location = new System.Drawing.Point(11, 203);
            this.lbNew2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNew2.Name = "lbNew2";
            this.lbNew2.Size = new System.Drawing.Size(87, 16);
            this.lbNew2.TabIndex = 3;
            this.lbNew2.Text = "New Version:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 226);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description:";
            // 
            // tbDescript
            // 
            this.tbDescript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescript.Location = new System.Drawing.Point(12, 248);
            this.tbDescript.Multiline = true;
            this.tbDescript.Name = "tbDescript";
            this.tbDescript.Size = new System.Drawing.Size(711, 113);
            this.tbDescript.TabIndex = 5;
            // 
            // lbNew
            // 
            this.lbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbNew.Location = new System.Drawing.Point(121, 203);
            this.lbNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNew.Name = "lbNew";
            this.lbNew.Size = new System.Drawing.Size(120, 16);
            this.lbNew.TabIndex = 8;
            // 
            // lbCurrent
            // 
            this.lbCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCurrent.Location = new System.Drawing.Point(121, 185);
            this.lbCurrent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCurrent.Name = "lbCurrent";
            this.lbCurrent.Size = new System.Drawing.Size(120, 16);
            this.lbCurrent.TabIndex = 7;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubmit.Location = new System.Drawing.Point(124, 367);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(90, 222);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(25, 25);
            this.btnNew.TabIndex = 10;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // tbCmdOutput
            // 
            this.tbCmdOutput.Location = new System.Drawing.Point(390, 35);
            this.tbCmdOutput.Multiline = true;
            this.tbCmdOutput.Name = "tbCmdOutput";
            this.tbCmdOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCmdOutput.Size = new System.Drawing.Size(333, 184);
            this.tbCmdOutput.TabIndex = 11;
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(290, 196);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 12;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 403);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.tbCmdOutput);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lbNew);
            this.Controls.Add(this.lbCurrent);
            this.Controls.Add(this.tbDescript);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbNew2);
            this.Controls.Add(this.lbCurrent2);
            this.Controls.Add(this.lbProject);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Update Maker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbProject;
        private System.Windows.Forms.Label lbCurrent2;
        private System.Windows.Forms.Label lbNew2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDescript;
        private System.Windows.Forms.Label lbNew;
        private System.Windows.Forms.Label lbCurrent;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox tbCmdOutput;
        private System.Windows.Forms.Button btnBuild;
    }
}

