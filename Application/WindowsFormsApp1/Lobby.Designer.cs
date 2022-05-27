
namespace LobbyForm
{
    partial class Lobby
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
            this.btnMatching = new System.Windows.Forms.Button();
            this.btnRanking = new System.Windows.Forms.Button();
            this.btnRuleBook = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnMatching
            // 
            this.btnMatching.Location = new System.Drawing.Point(60, 80);
            this.btnMatching.Name = "btnMatching";
            this.btnMatching.Size = new System.Drawing.Size(123, 80);
            this.btnMatching.TabIndex = 0;
            this.btnMatching.Text = "매칭시작";
            this.btnMatching.UseVisualStyleBackColor = true;
            this.btnMatching.Click += new System.EventHandler(this.btnMatching_Click);
            // 
            // btnRanking
            // 
            this.btnRanking.Location = new System.Drawing.Point(60, 180);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Size = new System.Drawing.Size(123, 80);
            this.btnRanking.TabIndex = 0;
            this.btnRanking.Text = "랭킹";
            this.btnRanking.UseVisualStyleBackColor = true;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // btnRuleBook
            // 
            this.btnRuleBook.Location = new System.Drawing.Point(60, 280);
            this.btnRuleBook.Name = "btnRuleBook";
            this.btnRuleBook.Size = new System.Drawing.Size(123, 80);
            this.btnRuleBook.TabIndex = 0;
            this.btnRuleBook.Text = "룰북";
            this.btnRuleBook.UseVisualStyleBackColor = true;
            this.btnRuleBook.Click += new System.EventHandler(this.btnRuleBook_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(60, 380);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(123, 80);
            this.btnModify.TabIndex = 0;
            this.btnModify.Text = "회원정보수정";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(60, 480);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(123, 80);
            this.btnLogOut.TabIndex = 0;
            this.btnLogOut.Text = "로그아웃";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(236, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 614);
            this.panel1.TabIndex = 1;
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 638);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnRuleBook);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.btnMatching);
            this.Name = "Lobby";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMatching;
        private System.Windows.Forms.Button btnRanking;
        private System.Windows.Forms.Button btnRuleBook;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Panel panel1;
    }
}

