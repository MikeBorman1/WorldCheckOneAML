namespace WindowsFormsApp1
{
    partial class FormMain
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
            this.buttonGetProfile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.radioButtonSanctioned = new System.Windows.Forms.RadioButton();
            this.caseCreate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Individual = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.import = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.getCached = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGetProfile
            // 
            this.buttonGetProfile.Location = new System.Drawing.Point(29, 27);
            this.buttonGetProfile.Name = "buttonGetProfile";
            this.buttonGetProfile.Size = new System.Drawing.Size(75, 23);
            this.buttonGetProfile.TabIndex = 0;
            this.buttonGetProfile.Text = "Get Proifile";
            this.buttonGetProfile.UseVisualStyleBackColor = true;
            this.buttonGetProfile.Click += new System.EventHandler(this.buttonGetProfile_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "e_tr_wci_1592168";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(30, 129);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(588, 307);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ID";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Gender";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "RiskFactor";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "AssociateRisk";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Age";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Sanctions";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(624, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Show Graph";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(528, 100);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Load Profiles";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // radioButtonAll
            // 
            this.radioButtonAll.AutoSize = true;
            this.radioButtonAll.Checked = true;
            this.radioButtonAll.Location = new System.Drawing.Point(6, 15);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new System.Drawing.Size(36, 17);
            this.radioButtonAll.TabIndex = 5;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.Text = "All";
            this.radioButtonAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonSanctioned
            // 
            this.radioButtonSanctioned.AutoSize = true;
            this.radioButtonSanctioned.Location = new System.Drawing.Point(48, 15);
            this.radioButtonSanctioned.Name = "radioButtonSanctioned";
            this.radioButtonSanctioned.Size = new System.Drawing.Size(103, 17);
            this.radioButtonSanctioned.TabIndex = 6;
            this.radioButtonSanctioned.Text = "Sanctioned Only";
            this.radioButtonSanctioned.UseVisualStyleBackColor = true;
            // 
            // caseCreate
            // 
            this.caseCreate.Location = new System.Drawing.Point(593, 61);
            this.caseCreate.Name = "caseCreate";
            this.caseCreate.Size = new System.Drawing.Size(108, 23);
            this.caseCreate.TabIndex = 7;
            this.caseCreate.Text = "Find Persons";
            this.caseCreate.UseVisualStyleBackColor = true;
            this.caseCreate.Click += new System.EventHandler(this.caseCreate_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(625, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 46);
            this.button2.TabIndex = 8;
            this.button2.Text = "Deleate Profile";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Individual
            // 
            this.Individual.AutoSize = true;
            this.Individual.Location = new System.Drawing.Point(157, 15);
            this.Individual.Name = "Individual";
            this.Individual.Size = new System.Drawing.Size(70, 17);
            this.Individual.TabIndex = 9;
            this.Individual.TabStop = true;
            this.Individual.Text = "Individual";
            this.Individual.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(233, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 17);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Organisation";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(364, 10);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 11;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(323, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Name";
            // 
            // import
            // 
            this.import.Location = new System.Drawing.Point(625, 432);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(76, 23);
            this.import.TabIndex = 14;
            this.import.Text = "Import";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonAll);
            this.groupBox1.Controls.Add(this.radioButtonSanctioned);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Individual);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(39, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 38);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // getCached
            // 
            this.getCached.AutoCheck = false;
            this.getCached.AutoSize = true;
            this.getCached.Location = new System.Drawing.Point(45, 67);
            this.getCached.Name = "getCached";
            this.getCached.Size = new System.Drawing.Size(145, 17);
            this.getCached.TabIndex = 16;
            this.getCached.TabStop = true;
            this.getCached.Text = "Get From Cached Profiles";
            this.getCached.UseVisualStyleBackColor = true;
            this.getCached.Click += new System.EventHandler(this.getCached_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(625, 182);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 43);
            this.button3.TabIndex = 17;
            this.button3.Text = "Add Profile To Watchlist";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(624, 232);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 37);
            this.button4.TabIndex = 18;
            this.button4.Text = "Country Influence";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 467);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.getCached);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.import);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.caseCreate);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonGetProfile);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetProfile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        public System.Windows.Forms.ColumnHeader columnHeader1;
        public System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.RadioButton radioButtonAll;
        private System.Windows.Forms.RadioButton radioButtonSanctioned;
        private System.Windows.Forms.Button caseCreate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton Individual;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton getCached;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}