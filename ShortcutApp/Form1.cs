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
using System.Runtime.InteropServices;

namespace ShortcutApp
{
    public partial class form1 : Form
    {
        private int [,] locations;


        //path保存用リスト
        List<string> pathList = new List<string>();
        List<Button> buttonList = new List<Button>();

        // ボタン配置の初期位置
        private int x = 12; // 横座標
        private int y = 12; // 縦座標
        private int space = 40;//ボタン間の幅
        //ボタンのサイズ
        private int buttonSize = 64;
        int plusXPos = 1;//ボタンの位置変更用係数

        //pathList要素数カウント用変数
        int currentListCount;

        //ボタン識別用変数
        int buttonTag=0;

        //ファイルダイアログ
        private OpenFileDialog openFileDialog = new OpenFileDialog();

        string openFilepath;

        public form1 ()
        {
            InitializeComponent();
        }

        //ボタンクリックしてFile-PATH取得
        private void button1_Click ( object sender, EventArgs e )
        {
            //取得できるファイルの種類を指定
            openFileDialog.Filter = "All Files|*.exe;*.url*";

            //ファイルを選択してOK押したら
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //パスの要素数をcurrentListCountに追加
                currentListCount = pathList.Count;
                //選んだファイルのパスをpathListに追加
                pathList.Add(openFileDialog.FileName);

                openFilepath = openFileDialog.FileName;

                //ボタン生成メソッドを実行
                buttonCreate();
            }
        }


        //ボタン生成メソッド
        private void buttonCreate ()
        {
            //pathListがcurrentListCountより大きくなったら(pathListに要素数が増えたら)
            if(pathList.Count > currentListCount)
            {
                //ボタンを生成、リストに追加
                Button button = new Button();//ボタンを生成
                buttonList.Add(button); //リストに追加

                // 取得したいファイルのパス
                string filePath = openFilepath;

                //ボタンのプロパティ設定
                button.Text = openFilepath; //ボタンのテキストに取得したファイル名代入
                button.Size = new Size(buttonSize,buttonSize);
                button.Location = new Point(x, y + space * plusXPos);//Pos移動
                button.Click += newButton_Click; //クリックイベント指定
                button.Tag = buttonTag; //ボタン識別用のTag付け
                button.MouseDown += button2_MouseDown; //右クリックされた時の処理
                buttonTag++;//Tagに1足す
                plusXPos += 2;//次生成ボタンX軸移動用変数にx足す

                //formにボタンを追加表示
                Controls.Add(button);
            }
        }

        //生成されたボタン用クリックイベント
        private void newButton_Click( object sender, EventArgs e )
        {
            //path先のファイル起動
            try
            {
                Process.Start(openFileDialog.FileName);
            }
            catch { }
        }

        //ContextMenuStripで呼び出す削除機能
        private void deleteShortcuts ( object sender, EventArgs e)
        {
            //ダイアログ表示
            DialogResult result = MessageBox.Show(
                "このショートカットを削除しますか?",
                "確認", 
                MessageBoxButtons.YesNo
            );

            //ダイアログでYES押されたら
            if (result == DialogResult.Yes)
            {
                Controls.Remove(clickedButton);
                textBox1.Text = "delete";

                //pathList内、buttonの番号番の要素削除
                //pathList.RemoveAt((int)clickedButton.Tag);
            }
        }

        private MouseEventArgs mouseEventArgs;
        private Button clickedButton;

        //MouseDown時に呼び出す処理
        private void button2_MouseDown ( object sender, MouseEventArgs e )
        {
            //clickedButtonにクリックしたbuttonの情報を代入
            clickedButton = sender as Button;

            //マウスの情報    
            mouseEventArgs = e;

            //ボタン上で右クリックしたら
            if(mouseEventArgs.Button == MouseButtons.Right)
            {
                //contextMenuStrip1をカーソルの位置に表示
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void locationControl ()
        {
            
        }

    }
}
