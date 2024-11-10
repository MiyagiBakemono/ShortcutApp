using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ShortcutApp
{
    public partial class form1 : Form
    {
        //path保存用リスト
        List<string> pathList = new List<string>();

        // ボタン配置の初期位置
        int x = 10; // 横座標
        int y = 10; // 縦座標
        int width = 40;//ボタン間の幅

        public form1 ()
        {
            InitializeComponent();
            

        }

        //ボタンクリックしてFile-PATH取得
        private void button1_Click ( object sender, EventArgs e )
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            //取得するファイルの種類を指定
            openFileDialog.Filter = "All Files|*.exe;*.url*";

            //ファイルを選択
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //pathListに追加
                pathList.Add(openFileDialog.FileName);

                for (int i = 0; i < pathList.Count; i++)
                {
                    //ボタンインスタンス生成
                    Button button = new Button();

                    //ボタンのプロパティ設定
                    button.Text = pathList.Count.ToString();
                    button.Location = new Point(10, 10 + width * i);

                    //formにボタンを追加表示
                    Controls.Add(button);
                }
            }
        }
    }
}
