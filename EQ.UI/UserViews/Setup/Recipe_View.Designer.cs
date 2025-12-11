// EQ.UI/UserViews/Recipe_View.Designer.cs
namespace EQ.UI.UserViews
{
    partial class Recipe_View
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._ListBoxRecipes = new EQ.UI.Controls._ListBox();
            this._ButtonSetCurrent = new EQ.UI.Controls._Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._LabelCurrent = new EQ.UI.Controls._Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._ButtonDelete = new EQ.UI.Controls._Button();
            this._ButtonCopy = new EQ.UI.Controls._Button();
            this._ButtonNew = new EQ.UI.Controls._Button();
            this._TextBoxNewName = new EQ.UI.Controls._TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._PanelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _LabelTitle
            // 
            this._LabelTitle.Size = new System.Drawing.Size(699, 59);
            this._LabelTitle.Text = "Recipe Management";
            // 
            // _ButtonSave
            // 
            this._ButtonSave.Enabled = false;
            this._ButtonSave.Location = new System.Drawing.Point(699, 0);
            this._ButtonSave.Visible = false;
            // 
            // _PanelMain
            // 
            this._PanelMain.Controls.Add(this.tableLayoutPanel1);
            this._PanelMain.Size = new System.Drawing.Size(799, 425);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(799, 425);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._ListBoxRecipes);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(393, 419);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recipe List";
            // 
            // _ListBoxRecipes
            // 
            this._ListBoxRecipes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._ListBoxRecipes.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListBoxRecipes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ListBoxRecipes.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ListBoxRecipes.ForeColor = System.Drawing.Color.Black;
            this._ListBoxRecipes.FormattingEnabled = true;
            this._ListBoxRecipes.ItemHeight = 18;
            this._ListBoxRecipes.Location = new System.Drawing.Point(3, 22);
            this._ListBoxRecipes.Name = "_ListBoxRecipes";
            this._ListBoxRecipes.Size = new System.Drawing.Size(387, 394);
            this._ListBoxRecipes.TabIndex = 0;
            this._ListBoxRecipes.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _ButtonSetCurrent
            // 
            this._ButtonSetCurrent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this._ButtonSetCurrent.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonSetCurrent.ForeColor = System.Drawing.Color.White;
            this._ButtonSetCurrent.Location = new System.Drawing.Point(10, 102);
            this._ButtonSetCurrent.Name = "_ButtonSetCurrent";
            this._ButtonSetCurrent.Size = new System.Drawing.Size(375, 55);
            this._ButtonSetCurrent.TabIndex = 1;
            this._ButtonSetCurrent.Text = "Set as Current Recipe";
            this._ButtonSetCurrent.ThemeStyle = EQ.UI.Controls.ThemeStyle.Success_Green;
            this._ButtonSetCurrent.UseVisualStyleBackColor = false;
            this._ButtonSetCurrent.Click += new System.EventHandler(this._ButtonSetCurrent_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this._LabelCurrent);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this._ButtonSetCurrent);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(402, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 206);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Recipe";
            // 
            // _LabelCurrent
            // 
            this._LabelCurrent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._LabelCurrent.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelCurrent.ForeColor = System.Drawing.Color.Black;
            this._LabelCurrent.Location = new System.Drawing.Point(145, 36);
            this._LabelCurrent.Name = "_LabelCurrent";
            this._LabelCurrent.Size = new System.Drawing.Size(240, 31);
            this._LabelCurrent.TabIndex = 3;
            this._LabelCurrent.Text = "DefaultRecipe";
            this._LabelCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelCurrent.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current Recipe:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._ButtonDelete);
            this.groupBox3.Controls.Add(this._ButtonCopy);
            this.groupBox3.Controls.Add(this._ButtonNew);
            this.groupBox3.Controls.Add(this._TextBoxNewName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(402, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(394, 207);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Create / Copy / Delete";
            // 
            // _ButtonDelete
            // 
            this._ButtonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this._ButtonDelete.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonDelete.ForeColor = System.Drawing.Color.White;
            this._ButtonDelete.Location = new System.Drawing.Point(265, 126);
            this._ButtonDelete.Name = "_ButtonDelete";
            this._ButtonDelete.Size = new System.Drawing.Size(120, 55);
            this._ButtonDelete.TabIndex = 1;
            this._ButtonDelete.Text = "Delete";
            this._ButtonDelete.ThemeStyle = EQ.UI.Controls.ThemeStyle.Danger_Red;
            this._ButtonDelete.UseVisualStyleBackColor = false;
            this._ButtonDelete.Click += new System.EventHandler(this._ButtonDelete_Click);
            // 
            // _ButtonCopy
            // 
            this._ButtonCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this._ButtonCopy.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonCopy.ForeColor = System.Drawing.Color.White;
            this._ButtonCopy.Location = new System.Drawing.Point(139, 126);
            this._ButtonCopy.Name = "_ButtonCopy";
            this._ButtonCopy.Size = new System.Drawing.Size(120, 55);
            this._ButtonCopy.TabIndex = 1;
            this._ButtonCopy.Text = "Copy";
            this._ButtonCopy.ThemeStyle = EQ.UI.Controls.ThemeStyle.Info_Sky;
            this._ButtonCopy.UseVisualStyleBackColor = false;
            this._ButtonCopy.Click += new System.EventHandler(this._ButtonCopy_Click);
            // 
            // _ButtonNew
            // 
            this._ButtonNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._ButtonNew.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonNew.ForeColor = System.Drawing.Color.White;
            this._ButtonNew.Location = new System.Drawing.Point(13, 126);
            this._ButtonNew.Name = "_ButtonNew";
            this._ButtonNew.Size = new System.Drawing.Size(120, 55);
            this._ButtonNew.TabIndex = 1;
            this._ButtonNew.Text = "New";
            this._ButtonNew.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            this._ButtonNew.UseVisualStyleBackColor = false;
            this._ButtonNew.Click += new System.EventHandler(this._ButtonNew_Click);
            // 
            // _TextBoxNewName
            // 
            this._TextBoxNewName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._TextBoxNewName.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TextBoxNewName.ForeColor = System.Drawing.Color.Black;
            this._TextBoxNewName.Location = new System.Drawing.Point(13, 70);
            this._TextBoxNewName.Name = "_TextBoxNewName";
            this._TextBoxNewName.Size = new System.Drawing.Size(372, 26);
            this._TextBoxNewName.TabIndex = 1;
            this._TextBoxNewName.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "New / Copy Target Name:";
            // 
            // Recipe_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Recipe_View";
            this.Size = new System.Drawing.Size(799, 484);
            this.Load += new System.EventHandler(this.Recipe_View_Load);
            this._PanelMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls._ListBox _ListBoxRecipes;
        private Controls._Button _ButtonSetCurrent;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls._Label _LabelCurrent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Controls._Button _ButtonDelete;
        private Controls._Button _ButtonCopy;
        private Controls._Button _ButtonNew;
        private Controls._TextBox _TextBoxNewName;
        private System.Windows.Forms.Label label1;
    }
}