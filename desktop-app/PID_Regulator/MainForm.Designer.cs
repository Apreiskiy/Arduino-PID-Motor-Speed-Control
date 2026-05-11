namespace PID_Regulator
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.SelectionPortToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgramInfoToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SaveDCoeffButton = new System.Windows.Forms.Button();
            this.SaveICoeffButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.RegButton = new System.Windows.Forms.Button();
            this.SetSpeedTextBox = new System.Windows.Forms.TextBox();
            this.MeasuredSpeedTextBox = new System.Windows.Forms.TextBox();
            this.SHIMTextBox = new System.Windows.Forms.TextBox();
            this.ExchangeDataRadioButton = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SavePCoeffButton = new System.Windows.Forms.Button();
            this.PCoeffNumeric = new System.Windows.Forms.NumericUpDown();
            this.ICoeffNumeric = new System.Windows.Forms.NumericUpDown();
            this.DCoeffNumeric = new System.Windows.Forms.NumericUpDown();
            this.SavePZUButton = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCoeffNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ICoeffNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCoeffNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectionPortToolStrip,
            this.ProgramInfoToolStrip});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // SelectionPortToolStrip
            // 
            this.SelectionPortToolStrip.Name = "SelectionPortToolStrip";
            this.SelectionPortToolStrip.Size = new System.Drawing.Size(91, 20);
            this.SelectionPortToolStrip.Text = "Выбор порта";
            this.SelectionPortToolStrip.Click += new System.EventHandler(this.SelectionPortToolStrip_Click);
            // 
            // ProgramInfoToolStrip
            // 
            this.ProgramInfoToolStrip.Name = "ProgramInfoToolStrip";
            this.ProgramInfoToolStrip.Size = new System.Drawing.Size(94, 20);
            this.ProgramInfoToolStrip.Text = "О программе";
            this.ProgramInfoToolStrip.Click += new System.EventHandler(this.ProgramInfoToolStrip_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.SaveDCoeffButton, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.SaveICoeffButton, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.RegButton, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.SetSpeedTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.MeasuredSpeedTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.SHIMTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ExchangeDataRadioButton, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.SavePCoeffButton, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.PCoeffNumeric, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.ICoeffNumeric, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.DCoeffNumeric, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.SavePZUButton, 2, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 337);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // SaveDCoeffButton
            // 
            this.SaveDCoeffButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveDCoeffButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveDCoeffButton.Location = new System.Drawing.Point(391, 213);
            this.SaveDCoeffButton.Name = "SaveDCoeffButton";
            this.SaveDCoeffButton.Size = new System.Drawing.Size(190, 36);
            this.SaveDCoeffButton.TabIndex = 20;
            this.SaveDCoeffButton.Text = "загрузить в мк";
            this.SaveDCoeffButton.UseVisualStyleBackColor = true;
            this.SaveDCoeffButton.Click += new System.EventHandler(this.SaveDCoeffButton_Click);
            // 
            // SaveICoeffButton
            // 
            this.SaveICoeffButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveICoeffButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveICoeffButton.Location = new System.Drawing.Point(391, 171);
            this.SaveICoeffButton.Name = "SaveICoeffButton";
            this.SaveICoeffButton.Size = new System.Drawing.Size(190, 36);
            this.SaveICoeffButton.TabIndex = 19;
            this.SaveICoeffButton.Text = "загрузить в мк";
            this.SaveICoeffButton.UseVisualStyleBackColor = true;
            this.SaveICoeffButton.Click += new System.EventHandler(this.SaveICoeffButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Заданная скорость";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Измеренная скорость";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Значение ШИМ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(3, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Коэффициент П";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(3, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Коэффициент И";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(3, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 19);
            this.label6.TabIndex = 5;
            this.label6.Text = "Коэффициент Д";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = "Обмен данными";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RegButton
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.RegButton, 3);
            this.RegButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RegButton.Location = new System.Drawing.Point(3, 297);
            this.RegButton.Name = "RegButton";
            this.RegButton.Size = new System.Drawing.Size(578, 37);
            this.RegButton.TabIndex = 7;
            this.RegButton.Text = "Регистратор";
            this.RegButton.UseVisualStyleBackColor = true;
            this.RegButton.Click += new System.EventHandler(this.RegButton_Click);
            // 
            // SetSpeedTextBox
            // 
            this.SetSpeedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SetSpeedTextBox.BackColor = System.Drawing.Color.White;
            this.SetSpeedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SetSpeedTextBox.Location = new System.Drawing.Point(197, 11);
            this.SetSpeedTextBox.Name = "SetSpeedTextBox";
            this.SetSpeedTextBox.ReadOnly = true;
            this.SetSpeedTextBox.Size = new System.Drawing.Size(188, 20);
            this.SetSpeedTextBox.TabIndex = 8;
            this.SetSpeedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MeasuredSpeedTextBox
            // 
            this.MeasuredSpeedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MeasuredSpeedTextBox.BackColor = System.Drawing.Color.White;
            this.MeasuredSpeedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MeasuredSpeedTextBox.Location = new System.Drawing.Point(197, 53);
            this.MeasuredSpeedTextBox.Name = "MeasuredSpeedTextBox";
            this.MeasuredSpeedTextBox.ReadOnly = true;
            this.MeasuredSpeedTextBox.Size = new System.Drawing.Size(188, 20);
            this.MeasuredSpeedTextBox.TabIndex = 9;
            this.MeasuredSpeedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SHIMTextBox
            // 
            this.SHIMTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SHIMTextBox.BackColor = System.Drawing.Color.White;
            this.SHIMTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHIMTextBox.Location = new System.Drawing.Point(197, 95);
            this.SHIMTextBox.Name = "SHIMTextBox";
            this.SHIMTextBox.ReadOnly = true;
            this.SHIMTextBox.Size = new System.Drawing.Size(188, 20);
            this.SHIMTextBox.TabIndex = 10;
            this.SHIMTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExchangeDataRadioButton
            // 
            this.ExchangeDataRadioButton.AutoSize = true;
            this.ExchangeDataRadioButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ExchangeDataRadioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExchangeDataRadioButton.Enabled = false;
            this.ExchangeDataRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExchangeDataRadioButton.Location = new System.Drawing.Point(197, 255);
            this.ExchangeDataRadioButton.Name = "ExchangeDataRadioButton";
            this.ExchangeDataRadioButton.Size = new System.Drawing.Size(188, 36);
            this.ExchangeDataRadioButton.TabIndex = 14;
            this.ExchangeDataRadioButton.TabStop = true;
            this.ExchangeDataRadioButton.Text = "не запущен";
            this.ExchangeDataRadioButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(391, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 42);
            this.label8.TabIndex = 15;
            this.label8.Text = "об. / мин";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(391, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(190, 42);
            this.label9.TabIndex = 16;
            this.label9.Text = "об. / мин";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(391, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(190, 42);
            this.label10.TabIndex = 17;
            this.label10.Text = "%";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SavePCoeffButton
            // 
            this.SavePCoeffButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePCoeffButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SavePCoeffButton.Location = new System.Drawing.Point(391, 129);
            this.SavePCoeffButton.Name = "SavePCoeffButton";
            this.SavePCoeffButton.Size = new System.Drawing.Size(190, 36);
            this.SavePCoeffButton.TabIndex = 18;
            this.SavePCoeffButton.Text = "загрузить в мк";
            this.SavePCoeffButton.UseVisualStyleBackColor = true;
            this.SavePCoeffButton.Click += new System.EventHandler(this.SavePCoeffButton_Click);
            // 
            // PCoeffNumeric
            // 
            this.PCoeffNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PCoeffNumeric.DecimalPlaces = 4;
            this.PCoeffNumeric.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PCoeffNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.PCoeffNumeric.Location = new System.Drawing.Point(197, 134);
            this.PCoeffNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PCoeffNumeric.Name = "PCoeffNumeric";
            this.PCoeffNumeric.Size = new System.Drawing.Size(188, 26);
            this.PCoeffNumeric.TabIndex = 21;
            // 
            // ICoeffNumeric
            // 
            this.ICoeffNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ICoeffNumeric.DecimalPlaces = 4;
            this.ICoeffNumeric.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ICoeffNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.ICoeffNumeric.Location = new System.Drawing.Point(197, 176);
            this.ICoeffNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ICoeffNumeric.Name = "ICoeffNumeric";
            this.ICoeffNumeric.Size = new System.Drawing.Size(188, 26);
            this.ICoeffNumeric.TabIndex = 22;
            // 
            // DCoeffNumeric
            // 
            this.DCoeffNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DCoeffNumeric.DecimalPlaces = 4;
            this.DCoeffNumeric.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DCoeffNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.DCoeffNumeric.Location = new System.Drawing.Point(197, 218);
            this.DCoeffNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.DCoeffNumeric.Name = "DCoeffNumeric";
            this.DCoeffNumeric.Size = new System.Drawing.Size(188, 26);
            this.DCoeffNumeric.TabIndex = 23;
            // 
            // SavePZUButton
            // 
            this.SavePZUButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePZUButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SavePZUButton.Location = new System.Drawing.Point(391, 255);
            this.SavePZUButton.Name = "SavePZUButton";
            this.SavePZUButton.Size = new System.Drawing.Size(190, 36);
            this.SavePZUButton.TabIndex = 24;
            this.SavePZUButton.Text = "Сохранить коэфф. в ПЗУ";
            this.SavePZUButton.UseVisualStyleBackColor = true;
            this.SavePZUButton.Click += new System.EventHandler(this.SavePZUButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.Name = "MainForm";
            this.Text = "Мониторинг ПИД регулятора";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCoeffNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ICoeffNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DCoeffNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem PortSelectionToolStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem SelectionPortToolStrip;
        private System.Windows.Forms.ToolStripMenuItem ProgramInfoToolStrip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button RegButton;
        private System.Windows.Forms.TextBox SetSpeedTextBox;
        private System.Windows.Forms.TextBox MeasuredSpeedTextBox;
        private System.Windows.Forms.TextBox SHIMTextBox;
        private System.Windows.Forms.RadioButton ExchangeDataRadioButton;
        private System.Windows.Forms.Button SaveDCoeffButton;
        private System.Windows.Forms.Button SaveICoeffButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button SavePCoeffButton;
        private System.Windows.Forms.NumericUpDown PCoeffNumeric;
        private System.Windows.Forms.NumericUpDown ICoeffNumeric;
        private System.Windows.Forms.NumericUpDown DCoeffNumeric;
        private System.Windows.Forms.Button SavePZUButton;
    }
}

