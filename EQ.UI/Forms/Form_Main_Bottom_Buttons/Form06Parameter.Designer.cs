namespace EQ.UI
{
    partial class Form06Parameter
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
            extruderRecipe_View1 = new EQ.UI.UserViews.Extruder.ExtruderRecipe_View();
            SuspendLayout();
            // 
            // extruderRecipe_View1
            // 
            extruderRecipe_View1.Dock = DockStyle.Fill;
            extruderRecipe_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            extruderRecipe_View1.Location = new Point(0, 0);
            extruderRecipe_View1.Margin = new Padding(3, 4, 3, 4);
            extruderRecipe_View1.Name = "extruderRecipe_View1";
            extruderRecipe_View1.Size = new Size(993, 832);
            extruderRecipe_View1.TabIndex = 0;
            // 
            // Form06Parameter
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 832);
            Controls.Add(extruderRecipe_View1);
            Name = "Form06Parameter";
            Text = "Form6";
            ResumeLayout(false);
        }

        #endregion

        private UserViews.Extruder.ExtruderRecipe_View extruderRecipe_View1;
    }
}