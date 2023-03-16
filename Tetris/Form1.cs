using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Tetris
{
    // 50 - �������� �� x � y
    public partial class Form1 : Form
    {
        Figurs? figurs;
        int[,] field = new int[16, 8];       // ������� ���� 
        int size;                           // ������ ����������
        int RemovedLines;                  // �������� �����
        int score;
        int interval;
        string playerName;
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet dt = null;
        SqlCommandBuilder cmd = null;
        string cs = "";
        public Form1()
        {
            InitializeComponent();
            playerName = Microsoft.VisualBasic.Interaction.InputBox("������� ��� ������", "���� ������", "����� �����");
            if (playerName == "")
            {
                playerName = "����� �����";
            }
            this.KeyDown += new KeyEventHandler(KeyFunc);
            Init();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["Records"].ConnectionString;
            conn.ConnectionString = cs;
        }
        public void Init()
        {
            score = 0;
            RemovedLines = 0;
            size = 30;
            timer1.Tick += new EventHandler(Update);
            timer1.Start();
            figurs = new Figurs(3, 0);
            labelScore.Text = "Score: " + score;
            labelLines.Text = "Lines: " + RemovedLines;
            interval = 500;
            timer1.Interval = interval;
            Invalidate();
        }
        private void KeyFunc(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    timer1.Interval = 10;
                    break;
                case Keys.Right:
                    if (!CollideHorizontal(1))
                    {
                        Reset();
                        figurs.MoveRight();
                        Merge();
                        Invalidate();
                    }
                    break;
                case Keys.Left:

                    if (!CollideHorizontal(-1))
                    {
                        Reset();
                        figurs.MoveLeft();
                        Merge();
                        Invalidate();
                    }
                    break;
                case Keys.A:
                    if (!IsIntersects())
                    {
                        Reset();
                        figurs.RotateFigurs();
                        Merge();
                        Invalidate();
                    }
                    break;
            }
        }
        private void Update(object sender, EventArgs e)                   // ���������� ���� �� �������
        {
            Reset();
            if (!Collide())
            {
                figurs.MoveDown();
            }
            else
            {
                Merge();
                FilledLine();
                timer1.Interval = interval;
                figurs.ResetFigurs(3, 0);
                if (Collide())
                {
                    ClearMap();
                    timer1.Tick -= new EventHandler(Update);
                    timer1.Stop();
                    MessageBox.Show("��� ���������: " + score);
                    AddRecords();
                    Init();
                }
            }
            Merge();
            Invalidate();
        }
        public void DrawFigurs(Graphics e)                           // ��������� ������
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (field[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (field[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (field[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Gold, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (field[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Fuchsia, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (field[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(50 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));   // ��������� ������
                    }
                }
            }
        }
        public void Merge()                                                      // ����������� ������ � �����
        {
            for (int i = figurs.y; i < figurs.y + figurs.sizeMatrix; i++)
            {
                for (int j = figurs.x; j < figurs.x + figurs.sizeMatrix; j++)
                {
                    if (figurs.matrix[i - figurs.y, j - figurs.x] != 0)             // ������ �� ������� �������
                        field[i, j] = figurs.matrix[i - figurs.y, j - figurs.x];
                }
            }
        }
        public bool Collide()                                                         // �������� �� ������������ ������������ 
        {
            for (int i = figurs.y + figurs.sizeMatrix - 1; i >= figurs.y; i--)
            {
                for (int j = figurs.x; j < figurs.x + figurs.sizeMatrix; j++)
                {
                    if (figurs.matrix[i - figurs.y, j - figurs.x] != 0)
                    {
                        //if (j-1< 0 || j+1>= 8)                                     // �������� ������ �� ������� �����
                        //{
                        //    return true;
                        //}
                        //if (field[i,j+1]!=0|| field[i, j - 1] != 0)                  // �������� �� �������������� ������������
                        //{
                        //    return true;
                        //}
                        if (i + 1 == 16)                                           //  �������� �� ������������ ������������
                        {
                            return true;
                        }
                        if (field[i + 1, j] != 0)                                     //  �������� �� ������������ ������������
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool CollideHorizontal(int a)                                      // �������� �� �������������� ������������  ( ���������� a  (-1 ���� ,  1 �����) )
        {
            for (int i = figurs.y + figurs.sizeMatrix - 1; i >= figurs.y; i--)
            {
                for (int j = figurs.x; j < figurs.x + figurs.sizeMatrix; j++)
                {
                    if (figurs.matrix[i - figurs.y, j - figurs.x] != 0)
                    {
                        if (j + 1 * a > 7 || j + 1 * a < 0)                       // �������� ������ �� �������, ���� ���� �� true
                            return true;

                        if (field[i, j + 1 * a] != 0)                                   // ���� �������� J ��  ��������� � ������  �������   
                        {
                            if (j - figurs.x + 1 * a >= figurs.sizeMatrix || j - figurs.x + 1 * a < 0)    // ���� ������������ ����� �������� ������ ��� ����� ���� ��������, ������ ������������
                            {
                                return true;
                            }
                            if (figurs.matrix[i - figurs.y, j - figurs.x + 1 * a] == 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        public void Reset()                                                  // ��������� ����
        {
            for (int i = figurs.y; i < figurs.y + figurs.sizeMatrix; i++)
            {
                for (int j = figurs.x; j < figurs.x + figurs.sizeMatrix; j++)
                {
                    if (i >= 0 && j >= 0 && i < 16 && j < 8)
                    {
                        if (figurs.matrix[i - figurs.y, j - figurs.x] != 0)
                        {
                            field[i, j] = 0;
                        }
                    }
                }
            }
        }
        public void DrawField(Graphics g)     // ��������� ���� 
        {
            for (int i = 0; i <= 16; i++)
            {
                g.DrawLine(Pens.Black, new Point(50, 50 + i * size), new Point(50 + 8 * size, 50 + i * size));   // �������������� �����
            }
            for (int i = 0; i <= 8; i++)
            {
                g.DrawLine(Pens.Black, new Point(50 + i * size, 50), new Point(50 + i * size, 50 + 16 * size));   // ������������ �����
            }
        }
        private void OnPaint(object sender, PaintEventArgs e)      // ������� �����������
        {
            DrawField(e.Graphics);
            DrawFigurs(e.Graphics);
            ShowNextShape(e.Graphics);
        }
        public void FilledLine()                           // �������� �� ����������� �� ����������� �����
        {
            int count = 0;                                // ���������� �� ������ �����
            int curRemovedLines = 0;                        // ���������� ������� �������� �����
            for (int i = 0; i < 16; i++)
            {
                count = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (field[i, j] != 0)                    // ���� �� ������
                        count++;
                }
                if (count == 8)                              // ���� ��� ������ �����
                {
                    curRemovedLines++;
                    for (int k = i; k >= 1; k--)                // �������� ���� ����� ���� �� 1 ��������
                    {
                        for (int o = 0; o < 8; o++)
                        {
                            field[k, o] = field[k - 1, o];
                        }
                    }
                }
            }
            for (int i = 0; i < curRemovedLines; i++)
            {
                score += 10 * (i + 1);
            }
            RemovedLines += curRemovedLines;
            if (RemovedLines % 5 == 0)                                    //  ���������� �������� ������� ������ 5 ����������� �����
            {
                if (interval > 70)

                    interval -= 10;
            }
            labelScore.Text = "Score: " + score;
            labelLines.Text = "Lines: " + RemovedLines;
        }
        public bool IsIntersects()                                                //  �������� �� ����� ������ (���� ������������� �� ������ �� ���)
        {
            for (int i = figurs.y; i < figurs.y + figurs.sizeMatrix; i++)
            {
                for (int j = figurs.x; j < figurs.x + figurs.sizeMatrix; j++)
                {
                    if (j >= 0 && j <= 7)
                    {
                        if (field[i, j] != 0 && figurs.matrix[i - figurs.y, j - figurs.x] == 0)
                            return true;
                    }
                }
            }
            return false;
        }
        public void ShowNextShape(Graphics e)                               //  ����� ��������� ������
        {
            for (int i = 0; i < figurs.nextSizeMatrix; i++)
            {
                for (int j = 0; j < figurs.nextSizeMatrix; j++)
                {
                    if (figurs.nextMatrix[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(300 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (figurs.nextMatrix[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(300 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (figurs.nextMatrix[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Gold, new Rectangle(300 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (figurs.nextMatrix[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Fuchsia, new Rectangle(300 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                    if (figurs.nextMatrix[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(300 + j * (size) + 1, 50 + i * (size) + 1, size - 1, size - 1));
                    }
                }
            }
        }
        private void OnPauseButtonClick(object sender, EventArgs e)             // ���������� ������ ���� �����
        {
            var pressedButton = sender as ToolStripMenuItem;
            if (timer1.Enabled)
            {
                pressedButton.Text = "����������";
                timer1.Stop();
            }
            else
            {
                pressedButton.Text = "�����";
                timer1.Start();
            }
        }
        private void OnAgainButtonClick(object sender, EventArgs e)              // ���������� ������ ���� ������
        {
            timer1.Tick -= new EventHandler(Update);
            timer1.Stop();
            ClearMap();
            Init();
        }
        public void ShowRecords(object sender, EventArgs e)               // ����� ������� ����� 
        {
            Form2 newForm = new Form2(this);
            newForm.Show();
            timer1.Stop();
            startToolStripMenuItem.Text= "����������";
        }
        public void AddRecords()                                       // ���������� � ������� �����
        {
            da = new SqlDataAdapter("select * from Records;", conn);
            cmd = new SqlCommandBuilder(da);
            dt = new DataSet();
            da.Fill(dt);
            DataTable dtt = dt.Tables[0];
            DataRow newRow = dtt.NewRow();
            newRow["name"] = playerName;
            newRow["score"] = score;
            dtt.Rows.Add(newRow);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            da.Update(dt);
            dt.Clear();
            da.Fill(dt);
        }
        public void ClearMap()                                              // ������� �������� ����
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    field[i, j] = 0;
                }
            }
        }
    }
}