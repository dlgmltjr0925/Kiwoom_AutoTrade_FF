using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Kiwoom_AutoTrade_FF
{
    public partial class Kwansim : Form
    {   
        public string m_strScrNo;
        private string m_strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;  // 파일이 존재하는 폴더 위치를 저장
        private string addCode;
        private int nCnt;

        public Kwansim()
        {
            InitializeComponent();
        }

        private void Kwansim_Load(object sender, EventArgs e)
        {
            InitKwansimGrid();
            SetKwansimGrid();
        }

        private void Kwansim_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_strPath = System.IO.Path.GetDirectoryName(m_strPath);

            string sDirPath;
            sDirPath = Application.StartupPath + "\\data\\";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sDirPath);
            if (!di.Exists)
                di.Create();

            string fileName = sDirPath + "Kwansim.txt";

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);

            string strOri = sr.ReadToEnd();
            
            sr.Close();
            fs.Close();

            string strTemp = "";

            for (int i = 0; i < nCnt; i++)
            {
                for (int j = 0; j < 5; j++)
                    strTemp += grdKwansim.Rows[i].Cells[j + 1].Value.ToString() + ";";
            }

            if(strOri.Trim() != strTemp.Trim())
            {
                const string message = "변경된 내용을 저장하시겠습니까?";
                const string caption = "저장";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    File.Delete(fileName);
                    if (nCnt > 0)
                    {
                        fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(strTemp);
                        sw.Close();
                        fs.Close();
                    }
                }
            }
            CommonLib.PostMessage(((Kiwoom_AutoTrade_FF)this.Owner).Handle, Constants.UM_SCREEN_CLOSE, 0, int.Parse(m_strScrNo));
        }

        private void InitKwansimGrid()
        {
            grdKwansim.RowCount = 4;
            grdKwansim.ColumnCount = 5;
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            {
                column.CellTemplate = new DataGridViewCheckBoxCell();
            }

            grdKwansim.Columns.Insert(0, column);

            string[] arrHeader = new string[6] {"", "종목코드", "거래수량", "손절틱수" ,"틱단위", "틱가치"};

            for (int i = 0; i < grdKwansim.ColumnCount; i++)
            {
                if (i == 0)
                {
                    grdKwansim.Columns[i].Width = 30;
                }   
                else if(i == 1)
                    grdKwansim.Columns[i].Width = 98;
                else
                    grdKwansim.Columns[i].Width = 60;
                grdKwansim.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;   // 헤더 텍스트 가운데 정렬
                grdKwansim.Columns[i].HeaderText = arrHeader[i];                                                // 헤더 텍스트 입력
                grdKwansim.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;                        // 정렬 없음
                grdKwansim.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            for (int i = 0; i < grdKwansim.RowCount; i++)
            {
                grdKwansim.Rows[i].Height = 22;
            }
            grdKwansim.ClearSelection();
        }

        private void SetKwansimGrid()
        {
            m_strPath = System.IO.Path.GetDirectoryName(m_strPath);

            string sDirPath;
            sDirPath = Application.StartupPath + "\\data\\";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sDirPath);
            if (!di.Exists)
                di.Create();

            string fileName = sDirPath + "Kwansim.txt";

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);

            string strTemp = sr.ReadToEnd();
            if (strTemp == "")
            {
                return;
            }
            else
            {
                string[] arrTemp = strTemp.Split(';');
                nCnt = arrTemp.Length / 5;
                for(int i = 0; i < nCnt; i++)
                {
                    grdKwansim.Rows[i].Cells[1].ReadOnly = true;
                    for(int j = 0; j < 5; j++)
                    {                        
                        grdKwansim.Rows[i].Cells[j+1].Value = arrTemp[5*i+j];
                    }
                }
            }

            sr.Close();
            fs.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addCode = tbJongCode.Text.ToUpper();
            tbJongCode.Text = "";

            if (addCode == "") // 입력이 없는 경우
                return;

            if (nCnt == 4) // 최대 4개까지 지정
                return;

            for(int i = 0; i < nCnt; i++) // 이미 입력된 경우
            {
                if (addCode == grdKwansim.Rows[i].Cells[1].Value.ToString())
                {
                    MessageBox.Show(addCode + "는 이미 추가된 종목입니다.");
                    return;
                }
            }
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("종목코드", addCode);

            int iRet = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.CommRqData("관심종목", "opt10001", "", m_strScrNo);
            string strErr;
            if (!((Kiwoom_AutoTrade_FF)this.Owner).IsError(iRet))
            {
                strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", "opt10001", iRet);
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nCnt; i++)
            {
                if (System.Convert.ToBoolean(grdKwansim.Rows[i].Cells[0].Value))
                {   
                    for (int j = i; j < nCnt; j++)
                    {

                        for (int k = 0; k < 6; k++)
                        {
                            if (j < nCnt - 1)
                                grdKwansim.Rows[j].Cells[k].Value = grdKwansim.Rows[j + 1].Cells[k].Value;
                            else
                            {
                                if (k == 0)
                                    grdKwansim.Rows[j].Cells[k].Value = false;
                                else
                                    grdKwansim.Rows[j].Cells[k].Value = "";
                            }
                        }
                    }
                    i--;
                    nCnt--;
                }
            }
        }

        private void btnAllDelete_Click(object sender, EventArgs e)
        {
            if (nCnt > 0)
            {
                for (int i = 0; i < nCnt; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (j == 0)
                            grdKwansim.Rows[i].Cells[j].Value = false;
                        else
                            grdKwansim.Rows[i].Cells[j].Value = "";
                    }
                }
                for (int i = 0; i < 4; i++)
                    grdKwansim.Rows[i].Cells[0].Value = false;
            }
            nCnt = 0;
            
        }

        private void SetAddJong(string strData, string tickSize, string tickValue)
        {
            if (strData != "")
            {
                int iVolume = 1, iLoss = 15;

                grdKwansim.Rows[nCnt].Cells[0].Value = false;
                grdKwansim.Rows[nCnt].Cells[1].Value = addCode;
                grdKwansim.Rows[nCnt].Cells[2].Value = iVolume;
                grdKwansim.Rows[nCnt].Cells[3].Value = iLoss;
                grdKwansim.Rows[nCnt].Cells[4].Value = tickSize;
                grdKwansim.Rows[nCnt].Cells[5].Value = tickValue;
                nCnt++;
            }
            else
                MessageBox.Show("잘못된 종목코드입니다.");
            
        }

        public void axKFOpenAPI1_OnReceiveTrData(string sScrNo, string sRQName, string sTrCode, string sRecordName, string sPreNext)
        {
            string strRQName = sRQName;
            if (strRQName == "관심종목")
            {
                string strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, "현재가");
                string tickSize = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, "틱단위");
                string tickValue = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, "틱가치");
                SetAddJong(strData, tickSize.Trim(), tickValue.Trim());
                
            }
        }

        public void axKFOpenAPI1_OnReceiveRealData(string sJongmokCode, string sRealType, string sRealData)
        {
        }

        public void axKFOpenAPI1_OnReceiveMsg(string sScrNo, string sRQName, string sTrCode, string sMsg)
        {
        }

        public void axKFOpenAPI1_OnReceiveChejanData(string sGubun, int nItemCnt, string sFidList)
        {
        }
    }
}
