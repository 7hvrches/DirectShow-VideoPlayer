namespace WindowsFormsApp5
{
    partial class Form3
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
            this.btnTheDeviceOp = new System.Windows.Forms.Button();
            this.btnTheCompressorOp = new System.Windows.Forms.Button();
            this.btnTheAudDeviceOp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTheDevice = new System.Windows.Forms.ComboBox();
            this.cbTheCompressor = new System.Windows.Forms.ComboBox();
            this.cbTheAudDevice = new System.Windows.Forms.ComboBox();
            this.screen = new System.Windows.Forms.Panel();
            this.btnWebCamPlay = new System.Windows.Forms.Button();
            this.btnRecordingPlay = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnWebCamStop = new System.Windows.Forms.Button();
            this.btnRecordingStop = new System.Windows.Forms.Button();
            this.txtSizeInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTheDeviceOp
            // 
            this.btnTheDeviceOp.Location = new System.Drawing.Point(506, 17);
            this.btnTheDeviceOp.Name = "btnTheDeviceOp";
            this.btnTheDeviceOp.Size = new System.Drawing.Size(75, 23);
            this.btnTheDeviceOp.TabIndex = 0;
            this.btnTheDeviceOp.Text = "옵션설정";
            this.btnTheDeviceOp.UseVisualStyleBackColor = true;
            this.btnTheDeviceOp.Click += new System.EventHandler(this.btnTheDeviceOp_Click);
            // 
            // btnTheCompressorOp
            // 
            this.btnTheCompressorOp.Location = new System.Drawing.Point(506, 61);
            this.btnTheCompressorOp.Name = "btnTheCompressorOp";
            this.btnTheCompressorOp.Size = new System.Drawing.Size(75, 23);
            this.btnTheCompressorOp.TabIndex = 1;
            this.btnTheCompressorOp.Text = "옵션설정";
            this.btnTheCompressorOp.UseVisualStyleBackColor = true;
            this.btnTheCompressorOp.Click += new System.EventHandler(this.btnTheCompressorOp_Click);
            // 
            // btnTheAudDeviceOp
            // 
            this.btnTheAudDeviceOp.Location = new System.Drawing.Point(506, 111);
            this.btnTheAudDeviceOp.Name = "btnTheAudDeviceOp";
            this.btnTheAudDeviceOp.Size = new System.Drawing.Size(75, 23);
            this.btnTheAudDeviceOp.TabIndex = 2;
            this.btnTheAudDeviceOp.Text = "옵션설정";
            this.btnTheAudDeviceOp.UseVisualStyleBackColor = true;
            this.btnTheAudDeviceOp.Click += new System.EventHandler(this.btnTheAudDeviceOp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "영상입력장치 : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "압축코덱 : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "오디오입력장치 : ";
            // 
            // cbTheDevice
            // 
            this.cbTheDevice.FormattingEnabled = true;
            this.cbTheDevice.Location = new System.Drawing.Point(137, 19);
            this.cbTheDevice.Name = "cbTheDevice";
            this.cbTheDevice.Size = new System.Drawing.Size(347, 20);
            this.cbTheDevice.TabIndex = 7;
            this.cbTheDevice.SelectedIndexChanged += new System.EventHandler(this.cbTheDevice_SelectedIndexChanged_1);
            // 
            // cbTheCompressor
            // 
            this.cbTheCompressor.FormattingEnabled = true;
            this.cbTheCompressor.Location = new System.Drawing.Point(137, 63);
            this.cbTheCompressor.Name = "cbTheCompressor";
            this.cbTheCompressor.Size = new System.Drawing.Size(347, 20);
            this.cbTheCompressor.TabIndex = 8;
            this.cbTheCompressor.SelectedIndexChanged += new System.EventHandler(this.cbTheCompressor_SelectedIndexChanged_1);
            // 
            // cbTheAudDevice
            // 
            this.cbTheAudDevice.FormattingEnabled = true;
            this.cbTheAudDevice.Location = new System.Drawing.Point(137, 113);
            this.cbTheAudDevice.Name = "cbTheAudDevice";
            this.cbTheAudDevice.Size = new System.Drawing.Size(347, 20);
            this.cbTheAudDevice.TabIndex = 9;
            this.cbTheAudDevice.SelectedIndexChanged += new System.EventHandler(this.cbTheAudDevice_SelectedIndexChanged_1);
            // 
            // screen
            // 
            this.screen.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.screen.Location = new System.Drawing.Point(21, 153);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(463, 258);
            this.screen.TabIndex = 10;
            // 
            // btnWebCamPlay
            // 
            this.btnWebCamPlay.Location = new System.Drawing.Point(506, 153);
            this.btnWebCamPlay.Name = "btnWebCamPlay";
            this.btnWebCamPlay.Size = new System.Drawing.Size(75, 33);
            this.btnWebCamPlay.TabIndex = 11;
            this.btnWebCamPlay.Text = "시 작";
            this.btnWebCamPlay.UseVisualStyleBackColor = true;
            this.btnWebCamPlay.Click += new System.EventHandler(this.btnWebCamPlay_Click);
            // 
            // btnRecordingPlay
            // 
            this.btnRecordingPlay.Location = new System.Drawing.Point(506, 366);
            this.btnRecordingPlay.Name = "btnRecordingPlay";
            this.btnRecordingPlay.Size = new System.Drawing.Size(75, 33);
            this.btnRecordingPlay.TabIndex = 12;
            this.btnRecordingPlay.Text = "녹화시작";
            this.btnRecordingPlay.UseVisualStyleBackColor = true;
            this.btnRecordingPlay.Click += new System.EventHandler(this.btnRecordingPlay_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(506, 231);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 129);
            this.button6.TabIndex = 13;
            this.button6.Text = "캡 쳐";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnWebCamStop
            // 
            this.btnWebCamStop.Location = new System.Drawing.Point(506, 192);
            this.btnWebCamStop.Name = "btnWebCamStop";
            this.btnWebCamStop.Size = new System.Drawing.Size(75, 33);
            this.btnWebCamStop.TabIndex = 14;
            this.btnWebCamStop.Text = "종 료";
            this.btnWebCamStop.UseVisualStyleBackColor = true;
            this.btnWebCamStop.Click += new System.EventHandler(this.btnWebCamStop_Click);
            // 
            // btnRecordingStop
            // 
            this.btnRecordingStop.Location = new System.Drawing.Point(506, 405);
            this.btnRecordingStop.Name = "btnRecordingStop";
            this.btnRecordingStop.Size = new System.Drawing.Size(75, 33);
            this.btnRecordingStop.TabIndex = 15;
            this.btnRecordingStop.Text = "녹화종료";
            this.btnRecordingStop.UseVisualStyleBackColor = true;
            this.btnRecordingStop.Click += new System.EventHandler(this.btnRecordingStop_Click);
            // 
            // txtSizeInfo
            // 
            this.txtSizeInfo.Location = new System.Drawing.Point(21, 417);
            this.txtSizeInfo.Name = "txtSizeInfo";
            this.txtSizeInfo.Size = new System.Drawing.Size(463, 21);
            this.txtSizeInfo.TabIndex = 16;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 455);
            this.Controls.Add(this.txtSizeInfo);
            this.Controls.Add(this.btnRecordingStop);
            this.Controls.Add(this.btnWebCamStop);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btnRecordingPlay);
            this.Controls.Add(this.btnWebCamPlay);
            this.Controls.Add(this.screen);
            this.Controls.Add(this.cbTheAudDevice);
            this.Controls.Add(this.cbTheCompressor);
            this.Controls.Add(this.cbTheDevice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTheAudDeviceOp);
            this.Controls.Add(this.btnTheCompressorOp);
            this.Controls.Add(this.btnTheDeviceOp);
            this.Name = "Form3";
            this.Text = "Webcam";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTheDeviceOp;
        private System.Windows.Forms.Button btnTheCompressorOp;
        private System.Windows.Forms.Button btnTheAudDeviceOp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTheDevice;
        private System.Windows.Forms.ComboBox cbTheCompressor;
        private System.Windows.Forms.ComboBox cbTheAudDevice;
        private System.Windows.Forms.Panel screen;
        private System.Windows.Forms.Button btnWebCamPlay;
        private System.Windows.Forms.Button btnRecordingPlay;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnWebCamStop;
        private System.Windows.Forms.Button btnRecordingStop;
        private System.Windows.Forms.TextBox txtSizeInfo;
    }
}