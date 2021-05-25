
namespace DesktopApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.workersTextBox = new System.Windows.Forms.TextBox();
            this.tasksTextBox = new System.Windows.Forms.TextBox();
            this.createMatrixButton = new System.Windows.Forms.Button();
            this.fileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "К-сть виконавців";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "К-сть задач";
            // 
            // workersTextBox
            // 
            this.workersTextBox.Location = new System.Drawing.Point(135, 27);
            this.workersTextBox.Name = "workersTextBox";
            this.workersTextBox.Size = new System.Drawing.Size(100, 23);
            this.workersTextBox.TabIndex = 2;
            // 
            // tasksTextBox
            // 
            this.tasksTextBox.Location = new System.Drawing.Point(135, 64);
            this.tasksTextBox.Name = "tasksTextBox";
            this.tasksTextBox.Size = new System.Drawing.Size(100, 23);
            this.tasksTextBox.TabIndex = 3;
            // 
            // createMatrixButton
            // 
            this.createMatrixButton.Location = new System.Drawing.Point(272, 26);
            this.createMatrixButton.Name = "createMatrixButton";
            this.createMatrixButton.Size = new System.Drawing.Size(86, 39);
            this.createMatrixButton.TabIndex = 4;
            this.createMatrixButton.Text = "Створити матрицю";
            this.createMatrixButton.UseVisualStyleBackColor = true;
            this.createMatrixButton.Click += new System.EventHandler(this.createMatrixButton_Click);
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(390, 26);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(86, 39);
            this.fileButton.TabIndex = 5;
            this.fileButton.Text = "Матриця з файлу";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.fileButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1217, 777);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.createMatrixButton);
            this.Controls.Add(this.tasksTextBox);
            this.Controls.Add(this.workersTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tasksTextBox;
        private System.Windows.Forms.Button createMatrixButton;
        private System.Windows.Forms.TextBox workersTextBox;
        private System.Windows.Forms.Button fileButton;
    }
}

