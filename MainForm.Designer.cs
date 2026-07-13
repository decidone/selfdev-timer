namespace SelfDevTimer;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    // 자기개발
    private System.Windows.Forms.GroupBox devGroup;
    private System.Windows.Forms.Label _devClock;
    private System.Windows.Forms.Button _devPlay;
    private System.Windows.Forms.Button _devPause;
    private System.Windows.Forms.Button _devStop;
    private System.Windows.Forms.Label _devToday;
    // 휴식
    private System.Windows.Forms.GroupBox restGroup;
    private System.Windows.Forms.Label _restClock;
    private System.Windows.Forms.Button _restPlay;
    private System.Windows.Forms.Button _restPause;
    private System.Windows.Forms.Button _restStop;
    private System.Windows.Forms.Label _restToday;
    // 시간 보정
    private System.Windows.Forms.GroupBox adjustGroup;
    private System.Windows.Forms.Label adjLabel;
    private System.Windows.Forms.NumericUpDown _adjustMinutes;
    private System.Windows.Forms.Label minLabel;
    private System.Windows.Forms.Label devAdjLabel;
    private System.Windows.Forms.Button devPlus;
    private System.Windows.Forms.Button devMinus;
    private System.Windows.Forms.Label restAdjLabel;
    private System.Windows.Forms.Button restPlus;
    private System.Windows.Forms.Button restMinus;
    // 색상 기준
    private System.Windows.Forms.GroupBox thrGroup;
    private System.Windows.Forms.Label t1;
    private System.Windows.Forms.NumericUpDown _thresholdInput;
    private System.Windows.Forms.Label t2;
    private System.Windows.Forms.NumericUpDown _highInput;
    private System.Windows.Forms.Label t3;
    // 이전 기록
    private System.Windows.Forms.Label histLabel;
    private System.Windows.Forms.ListView _history;
    private System.Windows.Forms.ColumnHeader colDate;
    private System.Windows.Forms.ColumnHeader colDev;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.devGroup = new System.Windows.Forms.GroupBox();
        this._devClock = new System.Windows.Forms.Label();
        this._devPlay = new System.Windows.Forms.Button();
        this._devPause = new System.Windows.Forms.Button();
        this._devStop = new System.Windows.Forms.Button();
        this._devToday = new System.Windows.Forms.Label();
        this.restGroup = new System.Windows.Forms.GroupBox();
        this._restClock = new System.Windows.Forms.Label();
        this._restPlay = new System.Windows.Forms.Button();
        this._restPause = new System.Windows.Forms.Button();
        this._restStop = new System.Windows.Forms.Button();
        this._restToday = new System.Windows.Forms.Label();
        this.adjustGroup = new System.Windows.Forms.GroupBox();
        this.adjLabel = new System.Windows.Forms.Label();
        this._adjustMinutes = new System.Windows.Forms.NumericUpDown();
        this.minLabel = new System.Windows.Forms.Label();
        this.devAdjLabel = new System.Windows.Forms.Label();
        this.devPlus = new System.Windows.Forms.Button();
        this.devMinus = new System.Windows.Forms.Button();
        this.restAdjLabel = new System.Windows.Forms.Label();
        this.restPlus = new System.Windows.Forms.Button();
        this.restMinus = new System.Windows.Forms.Button();
        this.thrGroup = new System.Windows.Forms.GroupBox();
        this.t1 = new System.Windows.Forms.Label();
        this._thresholdInput = new System.Windows.Forms.NumericUpDown();
        this.t2 = new System.Windows.Forms.Label();
        this._highInput = new System.Windows.Forms.NumericUpDown();
        this.t3 = new System.Windows.Forms.Label();
        this.histLabel = new System.Windows.Forms.Label();
        this._history = new System.Windows.Forms.ListView();
        this.colDate = new System.Windows.Forms.ColumnHeader();
        this.colDev = new System.Windows.Forms.ColumnHeader();
        this.devGroup.SuspendLayout();
        this.restGroup.SuspendLayout();
        this.adjustGroup.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this._adjustMinutes)).BeginInit();
        this.thrGroup.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this._thresholdInput)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this._highInput)).BeginInit();
        this.SuspendLayout();
        
        this.devGroup.Controls.Add(this._devClock);
        this.devGroup.Controls.Add(this._devPlay);
        this.devGroup.Controls.Add(this._devPause);
        this.devGroup.Controls.Add(this._devStop);
        this.devGroup.Controls.Add(this._devToday);
        this.devGroup.Location = new System.Drawing.Point(12, 12);
        this.devGroup.Size = new System.Drawing.Size(225, 170);
        this.devGroup.TabStop = false;
        this.devGroup.Text = "자기개발";
        
        this._devClock.AutoSize = false;
        this._devClock.Font = new System.Drawing.Font("Consolas", 20F);
        this._devClock.Location = new System.Drawing.Point(10, 24);
        this._devClock.Size = new System.Drawing.Size(205, 38);
        this._devClock.Text = "00:00:00";
        this._devClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        this._devPlay.Location = new System.Drawing.Point(10, 70);
        this._devPlay.Size = new System.Drawing.Size(100, 30);
        this._devPlay.Text = "시작";
        this._devPlay.Click += this.DevPlay_Click;
        
        this._devPause.Location = new System.Drawing.Point(115, 70);
        this._devPause.Size = new System.Drawing.Size(100, 30);
        this._devPause.Text = "일시정지";
        this._devPause.Click += this.DevPause_Click;
        
        this._devStop.Location = new System.Drawing.Point(10, 106);
        this._devStop.Size = new System.Drawing.Size(205, 30);
        this._devStop.Text = "정지";
        this._devStop.Click += this.DevStop_Click;
        
        this._devToday.AutoSize = false;
        this._devToday.Location = new System.Drawing.Point(10, 142);
        this._devToday.Size = new System.Drawing.Size(205, 20);
        this._devToday.Text = "오늘 누적 0시간 0분";
        this._devToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        this.restGroup.Controls.Add(this._restClock);
        this.restGroup.Controls.Add(this._restPlay);
        this.restGroup.Controls.Add(this._restPause);
        this.restGroup.Controls.Add(this._restStop);
        this.restGroup.Controls.Add(this._restToday);
        this.restGroup.Location = new System.Drawing.Point(243, 12);
        this.restGroup.Size = new System.Drawing.Size(225, 170);
        this.restGroup.TabStop = false;
        this.restGroup.Text = "휴식";
        
        this._restClock.AutoSize = false;
        this._restClock.Font = new System.Drawing.Font("Consolas", 20F);
        this._restClock.Location = new System.Drawing.Point(10, 24);
        this._restClock.Size = new System.Drawing.Size(205, 38);
        this._restClock.Text = "00:00:00";
        this._restClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        this._restPlay.Location = new System.Drawing.Point(10, 70);
        this._restPlay.Size = new System.Drawing.Size(100, 30);
        this._restPlay.Text = "시작";
        this._restPlay.Click += this.RestPlay_Click;
        
        this._restPause.Location = new System.Drawing.Point(115, 70);
        this._restPause.Size = new System.Drawing.Size(100, 30);
        this._restPause.Text = "일시정지";
        this._restPause.Click += this.RestPause_Click;
        
        this._restStop.Location = new System.Drawing.Point(10, 106);
        this._restStop.Size = new System.Drawing.Size(205, 30);
        this._restStop.Text = "정지";
        this._restStop.Click += this.RestStop_Click;
        
        this._restToday.AutoSize = false;
        this._restToday.Location = new System.Drawing.Point(10, 142);
        this._restToday.Size = new System.Drawing.Size(205, 20);
        this._restToday.Text = "오늘 쉰 시간 0시간 0분";
        this._restToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        this.adjustGroup.Controls.Add(this.adjLabel);
        this.adjustGroup.Controls.Add(this._adjustMinutes);
        this.adjustGroup.Controls.Add(this.minLabel);
        this.adjustGroup.Controls.Add(this.devAdjLabel);
        this.adjustGroup.Controls.Add(this.devPlus);
        this.adjustGroup.Controls.Add(this.devMinus);
        this.adjustGroup.Controls.Add(this.restAdjLabel);
        this.adjustGroup.Controls.Add(this.restPlus);
        this.adjustGroup.Controls.Add(this.restMinus);
        this.adjustGroup.Location = new System.Drawing.Point(12, 190);
        this.adjustGroup.Size = new System.Drawing.Size(456, 58);
        this.adjustGroup.TabStop = false;
        this.adjustGroup.Text = "오늘 시간 보정";
        
        this.adjLabel.AutoSize = false;
        this.adjLabel.Location = new System.Drawing.Point(10, 22);
        this.adjLabel.Size = new System.Drawing.Size(44, 25);
        this.adjLabel.Text = "보정값";
        this.adjLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this._adjustMinutes.Location = new System.Drawing.Point(56, 22);
        this._adjustMinutes.Size = new System.Drawing.Size(58, 25);
        this._adjustMinutes.Maximum = 6000;
        this._adjustMinutes.Minimum = 0;
        this._adjustMinutes.ValueChanged += this.AdjustMinutes_ValueChanged;
        
        this.minLabel.AutoSize = false;
        this.minLabel.Location = new System.Drawing.Point(116, 22);
        this.minLabel.Size = new System.Drawing.Size(18, 25);
        this.minLabel.Text = "분";
        this.minLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this.devAdjLabel.AutoSize = false;
        this.devAdjLabel.Location = new System.Drawing.Point(150, 22);
        this.devAdjLabel.Size = new System.Drawing.Size(56, 25);
        this.devAdjLabel.Text = "자기개발";
        this.devAdjLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this.devPlus.Location = new System.Drawing.Point(208, 21);
        this.devPlus.Size = new System.Drawing.Size(30, 27);
        this.devPlus.Text = "+";
        this.devPlus.Click += this.DevPlus_Click;
        
        this.devMinus.Location = new System.Drawing.Point(240, 21);
        this.devMinus.Size = new System.Drawing.Size(30, 27);
        this.devMinus.Text = "-";
        this.devMinus.Click += this.DevMinus_Click;
        
        this.restAdjLabel.AutoSize = false;
        this.restAdjLabel.Location = new System.Drawing.Point(286, 22);
        this.restAdjLabel.Size = new System.Drawing.Size(34, 25);
        this.restAdjLabel.Text = "휴식";
        this.restAdjLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this.restPlus.Location = new System.Drawing.Point(322, 21);
        this.restPlus.Size = new System.Drawing.Size(30, 27);
        this.restPlus.Text = "+";
        this.restPlus.Click += this.RestPlus_Click;
        
        this.restMinus.Location = new System.Drawing.Point(354, 21);
        this.restMinus.Size = new System.Drawing.Size(30, 27);
        this.restMinus.Text = "-";
        this.restMinus.Click += this.RestMinus_Click;
        
        this.thrGroup.Controls.Add(this.t1);
        this.thrGroup.Controls.Add(this._thresholdInput);
        this.thrGroup.Controls.Add(this.t2);
        this.thrGroup.Controls.Add(this._highInput);
        this.thrGroup.Controls.Add(this.t3);
        this.thrGroup.Location = new System.Drawing.Point(12, 256);
        this.thrGroup.Size = new System.Drawing.Size(456, 56);
        this.thrGroup.TabStop = false;
        this.thrGroup.Text = "색상 기준";
        
        this.t1.AutoSize = false;
        this.t1.Location = new System.Drawing.Point(10, 22);
        this.t1.Size = new System.Drawing.Size(36, 25);
        this.t1.Text = "기준";
        this.t1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this._thresholdInput.Location = new System.Drawing.Point(48, 22);
        this._thresholdInput.Size = new System.Drawing.Size(54, 25);
        this._thresholdInput.Maximum = 24;
        this._thresholdInput.Minimum = 0;
        this._thresholdInput.ValueChanged += this.ThresholdInput_ValueChanged;
        
        this.t2.AutoSize = false;
        this.t2.Location = new System.Drawing.Point(106, 22);
        this.t2.Size = new System.Drawing.Size(104, 25);
        this.t2.Text = "시간 미만 빨강 /";
        this.t2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this._highInput.Location = new System.Drawing.Point(212, 22);
        this._highInput.Size = new System.Drawing.Size(54, 25);
        this._highInput.Maximum = 24;
        this._highInput.Minimum = 0;
        this._highInput.ValueChanged += this.HighInput_ValueChanged;
        
        this.t3.AutoSize = false;
        this.t3.Location = new System.Drawing.Point(270, 22);
        this.t3.Size = new System.Drawing.Size(120, 25);
        this.t3.Text = "시간 이상 마젠타";
        this.t3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this.histLabel.AutoSize = false;
        this.histLabel.Location = new System.Drawing.Point(12, 320);
        this.histLabel.Size = new System.Drawing.Size(200, 18);
        this.histLabel.Text = "이전 기록 (최신순)";
        this.histLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        this._history.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this._history.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colDate, this.colDev });
        this._history.FullRowSelect = true;
        this._history.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
        this._history.Location = new System.Drawing.Point(12, 340);
        this._history.Size = new System.Drawing.Size(456, 294);
        this._history.UseCompatibleStateImageBehavior = false;
        this._history.View = System.Windows.Forms.View.Details;
        
        this.colDate.Text = "날짜";
        this.colDate.Width = 200;
        
        this.colDev.Text = "자기개발 시간";
        this.colDev.Width = 248;
        
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(480, 646);
        this.Controls.Add(this.devGroup);
        this.Controls.Add(this.restGroup);
        this.Controls.Add(this.adjustGroup);
        this.Controls.Add(this.thrGroup);
        this.Controls.Add(this.histLabel);
        this.Controls.Add(this._history);
        this.Font = new System.Drawing.Font("Malgun Gothic", 9F);
        this.MinimumSize = new System.Drawing.Size(496, 620);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = " 더 열 심 히 노 력 하 라";
        this.devGroup.ResumeLayout(false);
        this.restGroup.ResumeLayout(false);
        this.adjustGroup.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this._adjustMinutes)).EndInit();
        this.thrGroup.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this._thresholdInput)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this._highInput)).EndInit();
        this.ResumeLayout(false);
    }
}
