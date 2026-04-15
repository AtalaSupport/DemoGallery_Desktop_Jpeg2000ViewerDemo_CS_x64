using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Atalasoft.Imaging;
using Atalasoft.Imaging.Codec.Jpeg2000;
using Atalasoft.Imaging.WinControls;

namespace Jp2Viewer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.MenuItem menuItemFileOpen;
		private Atalasoft.Imaging.WinControls.WorkspaceViewer workspaceViewer1;
		private System.Windows.Forms.MenuItem menuItemFileExit;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.MenuItem menuFileOpenUrl;

		private Jp2Decoder _jp2;
		private int _numProgress = 0;
		private bool _cancel = false;
		private System.Windows.Forms.CheckBox chkEnableProgressive;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_jp2 = new Jp2Decoder();
            //// NOTE you need a license for the Jpeg2000 Professional version for the progressive features
            //_jp2.EnableProgressiveDecompression = true; 
            //_jp2.ProgressiveImage += new ProgressiveImageEventHandler(jp2_ProgressiveImage); 

            //register JP2 Decoder
            Atalasoft.Imaging.Codec.RegisteredDecoders.Decoders.Insert(0, _jp2);

			//initialize OpenFileDialog
			openFileDialog1.CheckFileExists = true;
			openFileDialog1.CheckPathExists = true;
			openFileDialog1.Filter = "Jpeg2000 (*.j2k; *.jpc; *.jp2; *.jpf;)|*.j2k;*.jpc;*.jp2;*.jpf;|All files (*.*)|*.*";
			openFileDialog1.FilterIndex = 1;
			openFileDialog1.RestoreDirectory = true;
			openFileDialog1.ShowHelp = false; 
			openFileDialog1.ShowReadOnly = true;
			openFileDialog1.Title = "Open Image File";

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemFileOpen = new System.Windows.Forms.MenuItem();
			this.menuFileOpenUrl = new System.Windows.Forms.MenuItem();
			this.menuItemFileExit = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.workspaceViewer1 = new Atalasoft.Imaging.WinControls.WorkspaceViewer();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.chkEnableProgressive = new System.Windows.Forms.CheckBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemFileOpen,
																					  this.menuFileOpenUrl,
																					  this.menuItemFileExit});
			this.menuItem1.Text = "&File";
			// 
			// menuItemFileOpen
			// 
			this.menuItemFileOpen.Index = 0;
			this.menuItemFileOpen.Text = "Open File";
			this.menuItemFileOpen.Click += new System.EventHandler(this.menuItemFileOpen_Click);
			// 
			// menuFileOpenUrl
			// 
			this.menuFileOpenUrl.Index = 1;
			this.menuFileOpenUrl.Text = "Open URL";
			this.menuFileOpenUrl.Click += new System.EventHandler(this.menuFileOpenUrl_Click);
			// 
			// menuItemFileExit
			// 
			this.menuItemFileExit.Index = 2;
			this.menuItemFileExit.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.menuItemFileExit.Text = "&Exit";
			this.menuItemFileExit.Click += new System.EventHandler(this.menuItemFileExit_Click);
			// 
			// workspaceViewer1
			// 
			this.workspaceViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.workspaceViewer1.Asynchronous = true;
			this.workspaceViewer1.Centered = true;
			this.workspaceViewer1.DisplayProfile = null;
			this.workspaceViewer1.ForeColor = System.Drawing.Color.Transparent;
			this.workspaceViewer1.Location = new System.Drawing.Point(0, 0);
			this.workspaceViewer1.Magnifier.BackColor = System.Drawing.Color.White;
			this.workspaceViewer1.Magnifier.BorderColor = System.Drawing.Color.Black;
			this.workspaceViewer1.Magnifier.Size = new System.Drawing.Size(100, 100);
			this.workspaceViewer1.Name = "workspaceViewer1";
			this.workspaceViewer1.OutputProfile = null;
			this.workspaceViewer1.Selection = null;
			this.workspaceViewer1.Size = new System.Drawing.Size(528, 360);
			this.workspaceViewer1.TabIndex = 0;
			this.workspaceViewer1.Text = "workspaceViewer1";
			this.workspaceViewer1.ZoomRectangle = null;
			this.workspaceViewer1.Progress += new Atalasoft.Imaging.ProgressEventHandler(this.workspaceViewer1_Progress);
			this.workspaceViewer1.ProcessError += new Atalasoft.Imaging.ExceptionEventHandler(this.workspaceViewer1_ProcessError);
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(8, 360);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(512, 16);
			this.progressBar1.TabIndex = 1;
			// 
			// chkEnableProgressive
			// 
			this.chkEnableProgressive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkEnableProgressive.Checked = true;
			this.chkEnableProgressive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkEnableProgressive.Location = new System.Drawing.Point(8, 384);
			this.chkEnableProgressive.Name = "chkEnableProgressive";
			this.chkEnableProgressive.Size = new System.Drawing.Size(168, 24);
			this.chkEnableProgressive.TabIndex = 2;
			this.chkEnableProgressive.Text = "Enable Progressive Decode";
			this.chkEnableProgressive.CheckedChanged += new System.EventHandler(this.chkEnableProgressive_CheckedChanged);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.textBox1.Location = new System.Drawing.Point(320, 384);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(32, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "16";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(216, 384);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Progressive Steps:";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(448, 376);
			this.button1.Name = "button1";
			this.button1.TabIndex = 5;
			this.button1.Text = "Cancel";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3});
			this.menuItem2.Text = "&Help";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "About ...";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 417);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.chkEnableProgressive);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.workspaceViewer1);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "JP2 Viewer";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main(string[] args)
		{
			Application.Run(new Form1());
		}

		private void workspaceViewer1_ProcessError(object sender, ExceptionEventArgs e)
		{
			MessageBox.Show(this, e.ToString());
		}

		private void workspaceViewer1_Progress(object sender, Atalasoft.Imaging.ProgressEventArgs e)
		{
			if (e.Total == 0) 
				e.Total = 1;
			int progV = e.Current * 100 / e.Total;
			if (progV > 100)
			{
				progV = 100;
			}
			progressBar1.Value = progV;
			progressBar1.Refresh();
			if (_cancel)
				e.Cancel = true;
		}

        private void RefreshViewer()
        {
            workspaceViewer1.Refresh();
        }

        public delegate void InvokeDelegate();

		private void jp2_ProgressiveImage(object sender, ProgressiveImageEventArgs e)
		{
			workspaceViewer1.Image = e.Image;
            workspaceViewer1.BeginInvoke(new InvokeDelegate(RefreshViewer));
			_numProgress++;
			if (_cancel)
				e.Cancel = true;
		}

		private void menuFileOpenUrl_Click(object sender, System.EventArgs e)
		{
			OpenUrlDialog openUrl = new OpenUrlDialog();
			if (openUrl.ShowDialog(this) == DialogResult.OK)
			{
				_numProgress = 0;
				_cancel = false;
				// network stream
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(openUrl.Url);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream stream = response.GetResponseStream();
				
				ProgressEventHandler progress = new ProgressEventHandler(workspaceViewer1_Progress);
				workspaceViewer1.Image = _jp2.Read(stream, progress);
				
			}
		}

		private void chkEnableProgressive_CheckedChanged(object sender, System.EventArgs e)
		{
			_jp2.EnableProgressiveDecompression = chkEnableProgressive.Checked;
		}

		private void menuItemFileOpen_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				_numProgress = 0;
				_cancel = false;
				workspaceViewer1.Open(openFileDialog1.FileName);	
			}
		}

		private void menuItemFileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				_jp2.ProgressiveDecodeSteps = int.Parse(textBox1.Text);
			}
			catch(Exception)
			{
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			_cancel = true;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			AtalaDemos.AboutBox.About aboutBox = new AtalaDemos.AboutBox.About("About Atalasoft DotImage Jp2 Viewer Demo",
				"DotImage Jp2 Viewer Demo");
			aboutBox.Description = @"This simple demo shows how to open and view JPEG2000 image files, and demonstrates some of the functionality in the professional version of JPEG2000 such as progressive loading and display.";
			aboutBox.ShowDialog();

		}
	}
}
