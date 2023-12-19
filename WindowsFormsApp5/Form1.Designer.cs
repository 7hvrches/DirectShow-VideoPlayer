namespace WindowsFormsApp5
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.labelNowTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblwidthheight = new System.Windows.Forms.Label();
            this.durationBar = new System.Windows.Forms.TrackBar();
            this.videoFiles = new System.Windows.Forms.ListBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSetMedia = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.재생ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.중지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.캡쳐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWebCamPlay = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.durationBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Location = new System.Drawing.Point(174, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 316);
            this.panel1.TabIndex = 2;
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.AutoSize = true;
            this.labelTotalTime.Location = new System.Drawing.Point(95, 429);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(33, 12);
            this.labelTotalTime.TabIndex = 6;
            this.labelTotalTime.Text = "00:00";
            // 
            // labelNowTime
            // 
            this.labelNowTime.AutoSize = true;
            this.labelNowTime.Location = new System.Drawing.Point(12, 429);
            this.labelNowTime.Name = "labelNowTime";
            this.labelNowTime.Size = new System.Drawing.Size(66, 12);
            this.labelNowTime.TabIndex = 5;
            this.labelNowTime.Text = "00:00      ~";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblwidthheight
            // 
            this.lblwidthheight.AutoSize = true;
            this.lblwidthheight.Location = new System.Drawing.Point(168, 429);
            this.lblwidthheight.Name = "lblwidthheight";
            this.lblwidthheight.Size = new System.Drawing.Size(97, 12);
            this.lblwidthheight.TabIndex = 8;
            this.lblwidthheight.Text = "비디오 넓이 높이";
            this.lblwidthheight.Visible = false;
            // 
            // durationBar
            // 
            this.durationBar.Location = new System.Drawing.Point(174, 369);
            this.durationBar.Name = "durationBar";
            this.durationBar.Size = new System.Drawing.Size(614, 45);
            this.durationBar.TabIndex = 10;
            this.durationBar.Visible = false;
            this.durationBar.Scroll += new System.EventHandler(this.durationBar_Scroll);
            // 
            // videoFiles
            // 
            this.videoFiles.FormattingEnabled = true;
            this.videoFiles.ItemHeight = 12;
            this.videoFiles.Location = new System.Drawing.Point(14, 41);
            this.videoFiles.Name = "videoFiles";
            this.videoFiles.Size = new System.Drawing.Size(154, 316);
            this.videoFiles.TabIndex = 11;
            this.videoFiles.SelectedIndexChanged += new System.EventHandler(this.videoListBox);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(14, 369);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(154, 21);
            this.txtPath.TabIndex = 13;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetMedia,
            this.toolStripDropDownButton1,
            this.btnWebCamPlay});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSetMedia
            // 
            this.btnSetMedia.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSetMedia.Image = ((System.Drawing.Image)(resources.GetObject("btnSetMedia.Image")));
            this.btnSetMedia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetMedia.Name = "btnSetMedia";
            this.btnSetMedia.Size = new System.Drawing.Size(59, 22);
            this.btnSetMedia.Text = "파일열기";
            this.btnSetMedia.Click += new System.EventHandler(this.btnSetMedia_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.재생ToolStripMenuItem,
            this.중지ToolStripMenuItem,
            this.종료ToolStripMenuItem,
            this.캡쳐ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripDropDownButton1.Text = "실행메뉴";
            // 
            // 재생ToolStripMenuItem
            // 
            this.재생ToolStripMenuItem.Name = "재생ToolStripMenuItem";
            this.재생ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.재생ToolStripMenuItem.Text = "재생";
            this.재생ToolStripMenuItem.Click += new System.EventHandler(this.재생ToolStripMenuItem_Click);
            // 
            // 중지ToolStripMenuItem
            // 
            this.중지ToolStripMenuItem.Name = "중지ToolStripMenuItem";
            this.중지ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.중지ToolStripMenuItem.Text = "중지";
            this.중지ToolStripMenuItem.Click += new System.EventHandler(this.중지ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // 캡쳐ToolStripMenuItem
            // 
            this.캡쳐ToolStripMenuItem.Name = "캡쳐ToolStripMenuItem";
            this.캡쳐ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.캡쳐ToolStripMenuItem.Text = "캡쳐";
            this.캡쳐ToolStripMenuItem.Click += new System.EventHandler(this.캡쳐ToolStripMenuItem_Click);
            // 
            // btnWebCamPlay
            // 
            this.btnWebCamPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnWebCamPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnWebCamPlay.Image")));
            this.btnWebCamPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWebCamPlay.Name = "btnWebCamPlay";
            this.btnWebCamPlay.Size = new System.Drawing.Size(87, 22);
            this.btnWebCamPlay.Text = "웹캠 시작하기";
            this.btnWebCamPlay.Click += new System.EventHandler(this.btnWebCamPlay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.videoFiles);
            this.Controls.Add(this.durationBar);
            this.Controls.Add(this.lblwidthheight);
            this.Controls.Add(this.labelTotalTime);
            this.Controls.Add(this.labelNowTime);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "VideoPlayer";
            ((System.ComponentModel.ISupportInitialize)(this.durationBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Label labelNowTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblwidthheight;
        private System.Windows.Forms.TrackBar durationBar;
        private System.Windows.Forms.ListBox videoFiles;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 재생ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 중지ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 캡쳐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnSetMedia;
        private System.Windows.Forms.ToolStripButton btnWebCamPlay;
    }
}

