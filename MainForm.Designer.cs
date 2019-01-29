namespace ShaderEditor
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
      this.m_ProjectTree = new ShaderEditor.AttributeTree();
      this.SuspendLayout();
      // 
      // m_ProjectTree
      // 
      this.m_ProjectTree.Location = new System.Drawing.Point(12, 12);
      this.m_ProjectTree.Name = "m_ProjectTree";
      this.m_ProjectTree.Size = new System.Drawing.Size(292, 384);
      this.m_ProjectTree.TabIndex = 1;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(316, 408);
      this.Controls.Add(this.m_ProjectTree);
      this.Name = "MainForm";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private AttributeTree m_ProjectTree;
  }
}

