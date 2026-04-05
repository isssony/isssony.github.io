namespace rabota5
{
    partial class Form1
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
            this.txtWord = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearchPrefix = new System.Windows.Forms.Button();
            this.btnDoubleConsonants = new System.Windows.Forms.Button();
            this.btnFuzzy = new System.Windows.Forms.Button();
            this.btnSaveResult = new System.Windows.Forms.Button();
            this.btnSaveDictionary = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtWord
            // 
            this.txtWord.Location = new System.Drawing.Point(71, 42);
            this.txtWord.Multiline = true;
            this.txtWord.Name = "txtWord";
            this.txtWord.Size = new System.Drawing.Size(214, 32);
            this.txtWord.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(70, 80);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(214, 35);
            this.txtSearch.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(351, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(250, 394);
            this.listBox1.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(115, 121);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(134, 37);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Загрузить словарь";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(115, 164);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(134, 37);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить слово";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(115, 289);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(134, 34);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearchPrefix
            // 
            this.btnSearchPrefix.Location = new System.Drawing.Point(115, 207);
            this.btnSearchPrefix.Name = "btnSearchPrefix";
            this.btnSearchPrefix.Size = new System.Drawing.Size(134, 36);
            this.btnSearchPrefix.TabIndex = 6;
            this.btnSearchPrefix.Text = "Поиск по префиксу";
            this.btnSearchPrefix.UseVisualStyleBackColor = true;
            this.btnSearchPrefix.Click += new System.EventHandler(this.btnSearchPrefix_Click);
            // 
            // btnDoubleConsonants
            // 
            this.btnDoubleConsonants.Location = new System.Drawing.Point(115, 249);
            this.btnDoubleConsonants.Name = "btnDoubleConsonants";
            this.btnDoubleConsonants.Size = new System.Drawing.Size(134, 34);
            this.btnDoubleConsonants.TabIndex = 7;
            this.btnDoubleConsonants.Text = "Поиск удвоенных согласных";
            this.btnDoubleConsonants.UseVisualStyleBackColor = true;
            this.btnDoubleConsonants.Click += new System.EventHandler(this.btnDoubleConsonants_Click);
            // 
            // btnFuzzy
            // 
            this.btnFuzzy.Location = new System.Drawing.Point(115, 329);
            this.btnFuzzy.Name = "btnFuzzy";
            this.btnFuzzy.Size = new System.Drawing.Size(134, 33);
            this.btnFuzzy.TabIndex = 8;
            this.btnFuzzy.Text = "Нечеткий поиск";
            this.btnFuzzy.UseVisualStyleBackColor = true;
            this.btnFuzzy.Click += new System.EventHandler(this.btnFuzzy_Click);
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.Location = new System.Drawing.Point(115, 368);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(134, 34);
            this.btnSaveResult.TabIndex = 9;
            this.btnSaveResult.Text = "Сохранить результаты";
            this.btnSaveResult.UseVisualStyleBackColor = true;
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResults_Click);
            // 
            // btnSaveDictionary
            // 
            this.btnSaveDictionary.Location = new System.Drawing.Point(115, 408);
            this.btnSaveDictionary.Name = "btnSaveDictionary";
            this.btnSaveDictionary.Size = new System.Drawing.Size(134, 36);
            this.btnSaveDictionary.TabIndex = 10;
            this.btnSaveDictionary.Text = "Сохранить словарь";
            this.btnSaveDictionary.UseVisualStyleBackColor = true;
            this.btnSaveDictionary.Click += new System.EventHandler(this.btnSaveResults_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 470);
            this.Controls.Add(this.btnSaveDictionary);
            this.Controls.Add(this.btnSaveResult);
            this.Controls.Add(this.btnFuzzy);
            this.Controls.Add(this.btnDoubleConsonants);
            this.Controls.Add(this.btnSearchPrefix);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.txtWord);
            this.Name = "Form1";
            this.Text = "Словарь";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWord;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearchPrefix;
        private System.Windows.Forms.Button btnDoubleConsonants;
        private System.Windows.Forms.Button btnFuzzy;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.Button btnSaveDictionary;
    }
}

