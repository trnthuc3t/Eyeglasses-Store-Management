﻿namespace BTL_LTTQ_VIP
{
    partial class ThemCongDung
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Ma = new System.Windows.Forms.TextBox();
            this.Ten = new System.Windows.Forms.TextBox();
            this.btnsua = new System.Windows.Forms.Button();
            this.xacnhan = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnxoa = new System.Windows.Forms.Button();
            this.dgvCongDung = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongDung)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã công dụng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên công dụng";
            // 
            // Ma
            // 
            this.Ma.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Ma.Location = new System.Drawing.Point(12, 28);
            this.Ma.Name = "Ma";
            this.Ma.Size = new System.Drawing.Size(237, 15);
            this.Ma.TabIndex = 2;
            // 
            // Ten
            // 
            this.Ten.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Ten.Location = new System.Drawing.Point(12, 65);
            this.Ten.Name = "Ten";
            this.Ten.Size = new System.Drawing.Size(237, 15);
            this.Ten.TabIndex = 3;
            // 
            // btnsua
            // 
            this.btnsua.FlatAppearance.BorderSize = 0;
            this.btnsua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsua.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnsua.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnsua.Image = global::BTL_LTTQ_VIP.Properties.Resources.sua;
            this.btnsua.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsua.Location = new System.Drawing.Point(256, 3);
            this.btnsua.Name = "btnsua";
            this.btnsua.Size = new System.Drawing.Size(178, 60);
            this.btnsua.TabIndex = 5;
            this.btnsua.Text = "Sửa";
            this.btnsua.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnsua.UseVisualStyleBackColor = true;
            this.btnsua.Click += new System.EventHandler(this.btnsua_Click_1);
            // 
            // xacnhan
            // 
            this.xacnhan.FlatAppearance.BorderSize = 0;
            this.xacnhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xacnhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.xacnhan.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.xacnhan.Image = global::BTL_LTTQ_VIP.Properties.Resources.xacnhan;
            this.xacnhan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xacnhan.Location = new System.Drawing.Point(26, 3);
            this.xacnhan.Name = "xacnhan";
            this.xacnhan.Size = new System.Drawing.Size(158, 60);
            this.xacnhan.TabIndex = 6;
            this.xacnhan.Text = "Xác Nhận";
            this.xacnhan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xacnhan.UseVisualStyleBackColor = true;
            this.xacnhan.Click += new System.EventHandler(this.xacnhan_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(55)))), ((int)(((byte)(77)))));
            this.panel1.Controls.Add(this.btnxoa);
            this.panel1.Controls.Add(this.btnsua);
            this.panel1.Controls.Add(this.xacnhan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 337);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 66);
            this.panel1.TabIndex = 7;
            // 
            // btnxoa
            // 
            this.btnxoa.FlatAppearance.BorderSize = 0;
            this.btnxoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnxoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnxoa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnxoa.Image = global::BTL_LTTQ_VIP.Properties.Resources.undo;
            this.btnxoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnxoa.Location = new System.Drawing.Point(494, 3);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(178, 60);
            this.btnxoa.TabIndex = 7;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnxoa.UseVisualStyleBackColor = true;
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click_1);
            // 
            // dgvCongDung
            // 
            this.dgvCongDung.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCongDung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCongDung.Location = new System.Drawing.Point(305, 14);
            this.dgvCongDung.Name = "dgvCongDung";
            this.dgvCongDung.RowHeadersWidth = 51;
            this.dgvCongDung.RowTemplate.Height = 24;
            this.dgvCongDung.Size = new System.Drawing.Size(411, 305);
            this.dgvCongDung.TabIndex = 8;
            // 
            // ThemCongDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 403);
            this.Controls.Add(this.dgvCongDung);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Ten);
            this.Controls.Add(this.Ma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ThemCongDung";
            this.Text = "Thêm Công Dụng";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongDung)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Ma;
        private System.Windows.Forms.TextBox Ten;
        private System.Windows.Forms.Button btnsua;
        private System.Windows.Forms.Button xacnhan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.DataGridView dgvCongDung;
    }
}