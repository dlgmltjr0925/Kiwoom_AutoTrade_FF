using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Kiwoom_AutoTrade_FF
{
    public struct stGRID
    {
        public string strKey;				// 조회 키
        public string strRealKey;			// 리얼 키
        public int nRow;				// 그리드 행
        public int nCol;				// 그리드 열
        public int nDataType;			// 데이타 타입(0:기본문자, 1:일자, 2:시간, 3:콤파 숫자, 4:콤파 숫자(0표시), 5:대비기호)
        public bool bTextColor;			// 문자열 색 변경(상승, 하락색)
        public uint nAlign;				// 문자열 정렬(DT_LEFT, DT_CENTER, DT_RIGHT)
        public string strBeforeData;		// 문자열 앞 문자 넣기
        public string strAfterData;		// 문자열 뒤 문자 넣기

        public stGRID(string strKey, string strRealKey, int nRow, int nCol, int nDataType, bool bTextColor, uint nAlign, string strBeforeData, string strAfterData)
        {
            this.strKey = strKey;
            this.strRealKey = strRealKey;
            this.nRow = nRow;
            this.nCol = nCol;
            this.nDataType = nDataType;
            this.bTextColor = bTextColor;
            this.nAlign = nAlign;
            this.strBeforeData = strBeforeData;
            this.strAfterData = strAfterData;
        }
    }
    public partial class Kiwoom_AutoTrade_FF : Form
    {
        const int STGRIDACCOUNT_SIZE = 10;
        public stGRID[] lstACCOUNT = new stGRID[STGRIDACCOUNT_SIZE]
        {
            new stGRID("구분",        "-1",   -1, 0,  Constants.DT_NONE,           false,  Constants.DT_CENTER,     "", ""),
            new stGRID("종목코드",    "-1",   -1, 1,  Constants.DT_NONE,           false,  Constants.DT_CENTER,     "", ""),
            new stGRID("현재가",      "10",   -1, 2,  Constants.DT_PRICE,          true,   Constants.DT_CENTER,     "", ""),
            new stGRID("매도수구분",  "-1",   -1, 3,  Constants.DT_NONE,           false,  Constants.DT_CENTER,     "", ""),
            new stGRID("진입수량",    "-1",   -1, 4,  Constants.DT_NUMBER_NOCOMMA, false,  Constants.DT_CENTER,     "", ""),
            new stGRID("진입가",      "-1",   -1, 5,  Constants.DT_PRICE,          false,  Constants.DT_CENTER,     "", ""),
            new stGRID("청산수량",    "-1",   -1, 6,  Constants.DT_NUMBER_NOCOMMA, false,  Constants.DT_CENTER,     "", ""),
            new stGRID("청산가",      "-1",   -1, 7,  Constants.DT_PRICE,          false,  Constants.DT_CENTER,     "", ""),
            new stGRID("손절가",      "-1",   -1, 8,  Constants.DT_PRICE,          false,  Constants.DT_CENTER,     "", ""),
            new stGRID("평가손익",    "-1",   -1, 9,  Constants.DT_NONE,           false,  Constants.DT_CENTER,     "", "")
        };
        
        const int STGRIDRATE_SIZE = 7;
        public stGRID[] lstRATE = new stGRID[STGRIDRATE_SIZE]
        {
            new stGRID("종목코드",   "9001",   -1, 1,  Constants.DT_NONE,           false,  Constants.DT_CENTER,     "", ""),
            new stGRID("현재가",       "10",   -1, 2,  Constants.DT_PRICE,           true,  Constants.DT_CENTER,     "", ""),
            new stGRID("매도수구분",   "-1",   -1, 3,  Constants.DT_ORDGUBUN,       false,  Constants.DT_CENTER,     "", ""),
            new stGRID("청산가능",     "-1",   -1, 4,  Constants.DT_NUMBER_NOCOMMA, false,  Constants.DT_CENTER,     "", ""),
            new stGRID("평균단가",     "-1",   -1, 5,  Constants.DT_PRICE,          false,  Constants.DT_CENTER,     "", ""),
            new stGRID("평가손익",      "0",   -1, 8,  Constants.DT_DOUBLE,          true,  Constants.DT_CENTER,     "", ""),
            new stGRID("통화코드",     "-1",   -1, 9,  Constants.DT_NONE,           false,  Constants.DT_CENTER,     "", "")
        };

        const int STGRIDHOGA_SIZE = 36;
        public stGRID[] lstHOGA = new stGRID[STGRIDHOGA_SIZE]
        {
            //시간
            new stGRID("호가시간",      "21",   0,  2,  Constants.DT_TIME,          false,  Constants.DT_CENTER,    "", ""),
            //매도호가
            new stGRID("매도건수5",    "105",   1,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량5",     "65",   1,  1,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가5",     "45",   1,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도건수4",    "104",   2,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량4",     "64",   2,  1,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가4",     "44",   2,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도건수3",    "103",   3,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량3",     "63",   3,  1,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가3",     "43",   3,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도건수2",    "102",   4,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량2",     "62",   4,  1,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가2",     "42",   4,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도건수1",    "101",   5,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량1",     "61",   5,  1,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가1",     "41",   5,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
	        /////////////////////////////////////
	        // 매수호가
            new stGRID("매수호가1",     "51",   6,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량1",     "71",   6,  3,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수건수1",    "111",   6,  4,  Constants.DT_NUMBER,        false,   Constants.DT_RIGHT,    "", ""),
            new stGRID("매수호가2",     "52",   7,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량2",     "72",   7,  3,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수건수2",    "112",   7,  4,  Constants.DT_NUMBER,        false,   Constants.DT_RIGHT,    "", ""),
            new stGRID("매수호가3",     "53",   8,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량3",     "73",   8,  3,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수건수3",    "113",   8,  4,  Constants.DT_NUMBER,        false,   Constants.DT_RIGHT,    "", ""),
            new stGRID("매수호가4",     "54",   9,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량4",     "74",   9,  3,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수건수4",    "113",   9,  4,  Constants.DT_NUMBER,        false,   Constants.DT_RIGHT,    "", ""),
            new stGRID("매수호가5",     "55",  10,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량5",     "75",  10,  3,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수건수5",    "113",  10,  4,  Constants.DT_NUMBER,        false,   Constants.DT_RIGHT,    "", ""),

            new stGRID("매도호가총건수","123", 11, 0,  Constants.DT_NUMBER,         false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가총잔량","121", 11, 1,  Constants.DT_NUMBER,         false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("순매수잔량",    "128", 11, 2,  Constants.DT_NUMBER,         false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수호가총잔량","125", 11, 3,  Constants.DT_NUMBER,         false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수호가총건수","127", 11, 4,  Constants.DT_NUMBER,         false,  Constants.DT_RIGHT,     "", "")
        };

        const int STGRIDSETTLE_SIZE = 4;
        public stGRID[] lstSETTLE = new stGRID[STGRIDSETTLE_SIZE]
        {
            new stGRID("매도수량",      "-1",    0,  1,  Constants.DT_NUMBER_NOCOMMA, false,  Constants.DT_CENTER,    "", ""),
            new stGRID("매수수량",      "-1",    0,  3,  Constants.DT_NUMBER_NOCOMMA, false,  Constants.DT_CENTER,    "", ""),
            new stGRID("청산손익합계",  "-1",    0,  5,  Constants.DT_DOUBLE,          true,  Constants.DT_CENTER,    "", ""),
            new stGRID("순손익합계",    "-1",    0,  7,  Constants.DT_DOUBLE,          true,  Constants.DT_CENTER,    "", "")
        };

        const int STGRIDSETTLEDETAIL_SIZE = 9;
        public stGRID[] lstSETTLEDETAILE = new stGRID[STGRIDSETTLEDETAIL_SIZE]
        {
            new stGRID("종목코드",      "-1",   -1,  0,  Constants.DT_NONE,           false,  Constants.DT_CENTER,    "", ""),
            new stGRID("매도수구분",    "-1",   -1,  1,  Constants.DT_ORDGUBUN,       false,  Constants.DT_CENTER,    "", ""),
            new stGRID("수량",          "-1",   -1,  2,  Constants.DT_NUMBER_NOCOMMA, false,  Constants.DT_CENTER,    "", ""),
            new stGRID("청산손익",      "-1",   -1,  3,  Constants.DT_DOUBLE,          true,  Constants.DT_CENTER,    "", ""),
            new stGRID("수수료",        "-1",   -1,  4,  Constants.DT_DOUBLE,         false,  Constants.DT_CENTER,    "", ""),
            new stGRID("순손익",        "-1",   -1,  5,  Constants.DT_DOUBLE,          true,  Constants.DT_CENTER,    "", ""),
            new stGRID("매입표시가격",  "-1",   -1,  6,  Constants.DT_PRICE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("청산가격",      "-1",   -1,  7,  Constants.DT_PRICE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("청산일자",      "-1",   -1,  8,  Constants.DT_DATE,           false,  Constants.DT_CENTER,    "", "")
        };

        const int STGRIDINFO_SIZE = 4;
        public stGRID[] lstINFO = new stGRID[STGRIDINFO_SIZE]
        {
            new stGRID("시가",      "16",   1,  4,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("고가",      "17",   2,  4,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("저가",      "18",   3,  4,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("거래량",    "13",   4,  4,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", "")
        };

        const int STGRIDTICK_SIZE = 2;
        public stGRID[] lstTICK = new stGRID[STGRIDTICK_SIZE]
        {
            new stGRID("체결량n",      "15",   7, 0,  Constants.DT_ZERO_NUMBER,    true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("현재가n",      "10",   7, 1,  Constants.DT_PRICE,          true,  Constants.DT_RIGHT,     "", "")
        };

        const string m_strRealSet = "해외선물시세;해외선물호가;해외옵션시세;해외옵션호가";
        private Dictionary<string, string> m_mapScreenNum = new Dictionary<string, string>();
        private Dictionary<string, Form> m_mapScreen = new Dictionary<string, Form>();
        private int m_nScrNo;
        private string m_strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;  // 파일이 존재하는 폴더 위치를 저장
        private string m_strScrNo;
        private int jongCount = 0, jongInfoIndex = -1;
        public bool[] _bAutoTrade;
        public string m_AccNo;
        public string m_strJongCode;
        private string hogaJongCode = "";
        private string[,] arrTickData = new string[4, 2] { { "", "" }, { "", "" }, { "", "" }, { "", "" } };
        // private bool m_bNextFlag = false;
        private string strCodelist = "";
        private bool m_Online = false;
        private bool m_Settle = false;
        public int[] tradeVolume, stopCount;
        public double[] tickSize, tickValue;
        private string[]  goalPrice = new string[4] { "", "", "", "" };
        Dictionary<string, int> m_JongIndex = new Dictionary<string, int>();
        private int searchCounter = 0;
        private DateTime lastSearchTime = DateTime.Now;
         
        public Kiwoom_AutoTrade_FF()
        {
            InitializeComponent();
        }

        private void Kiwoom_AutoTrade_FF_Load(object sender, EventArgs e)
        {
            axKFOpenAPI1.CommConnect(1);
            InitGrdAccount();
            InitGrdHoga();
            InitGrdSettleList();
        }
        private void Kiwoom_AutoTrade_FF_FormClosing(object sender, FormClosingEventArgs e)
        {
            // axKFOpenAPI1.Commterminate();

            m_mapScreen.Clear();
            m_mapScreenNum.Clear();

            m_mapScreen = null;
            m_mapScreenNum = null;
        }

        // 초기화 

        private void InitGrdAccount()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "btnAutoTrade";
            grdAccount.Columns.Add(buttonColumn);

            grdAccount.RowCount = 4;
            grdAccount.ColumnCount = 10;

            string[] arrHeader = new string[10] { "구 분", "종목코드", "현재가", "포지션", "보유수량", "진입가", "청산가", "손절가", "평가손익","통화코드"};

            for (int i = 0; i < grdAccount.ColumnCount; i++)
            {
                if (i == 0)
                    grdAccount.Columns[i].Width = 62;
                else if (i == 3 || i == 4 || i == 9)
                    grdAccount.Columns[i].Width = 77;
                else
                    grdAccount.Columns[i].Width = 99;
                grdAccount.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdAccount.Columns[i].HeaderText = arrHeader[i];
                grdAccount.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < grdAccount.RowCount; i++)
            {
                grdAccount.Rows[i].Height = 22;
            }
            for (int i = 0; i < grdAccount.RowCount; i++)
            {
                for (int j = 0; j < grdAccount.ColumnCount; j++)
                {
                    grdAccount.Rows[i].Cells[j].Value = "";
                }
            }

            grdAccount.ClearSelection();
        }
        private void InitGrdHoga()
        {
            grdHoga.RowCount = 12;
            grdHoga.ColumnCount = 5;
            string[] arrTemp = new string[] { "건 수", "매도잔량", "06:00:00", "매수잔량", "건 수" };
            string[] arrTemp2 = new string[] { "시 가", "고 가", "저 가", "거래량" };
            string[] arrTemp3 = new string[] { "체결량", "체결가" };
            Color[] clrHoga = new Color[6]
            {Color.FromArgb(229,229,229), Color.FromArgb(224,238,245), Color.FromArgb(255,229,229), Color.FromArgb(241,247,251), Color.FromArgb(255,224,203), Color.FromArgb(255,239,229)};

            for (int i = 0; i < grdHoga.ColumnCount; i++)
            {
                if (i == 0 || i == 4)
                    grdHoga.Columns[i].Width = 50;
                else if (i == 1 || i == 3)
                    grdHoga.Columns[i].Width = 60;
                else
                    grdHoga.Columns[i].Width = 80;
                grdHoga.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdHoga.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < grdHoga.RowCount; i++)
            {
                grdHoga.Rows[i].Height = 22;
            }
            for (int i = 0; i < grdHoga.ColumnCount; i++)
            {
                for (int j = 0; j < grdHoga.RowCount; j++)
                {
                    grdHoga.Rows[j].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grdHoga.Rows[j].Cells[i].ReadOnly = true;
                    if (j == 0 || j == 11)
                    {
                        grdHoga.Rows[j].Cells[i].Style.BackColor = clrHoga[0];
                        if (j == 0)
                            grdHoga.Rows[j].Cells[i].Value = arrTemp[i];
                    }
                    if (i == 0 || i == 1)
                    {
                        if (j == 6)
                        {
                            grdHoga.Rows[j].Cells[i].Value = arrTemp3[i];
                            grdHoga.Rows[j].Cells[i].Style.BackColor = clrHoga[4];
                        }
                        else if (j > 6 && j < 11)
                        {
                            grdHoga.Rows[j].Cells[i].Value = "";
                            grdHoga.Rows[j].Cells[i].Style.BackColor = clrHoga[5];
                        }

                    }
                    if (i == 2)
                    {
                        if (j > 0 && j < 6)
                            grdHoga.Rows[j].Cells[i].Style.BackColor = clrHoga[1];
                        else if (j > 5 && j < 11)
                            grdHoga.Rows[j].Cells[i].Style.BackColor = clrHoga[2];
                    }
                    if (i == 3)
                    {
                        if (j > 0 && j < 5)
                        {
                            grdHoga.Rows[j].Cells[i].Value = arrTemp2[j - 1];
                            grdHoga.Rows[j].Cells[i].Style.BackColor = clrHoga[3];
                        }
                    }
                    if (i == 4)
                    {
                        if (j > 0 && j < 5)
                        {
                            grdHoga.Rows[j].Cells[i].Style.BackColor = clrHoga[3];
                        }
                    }
                }
            }
            grdAccount.ClearSelection();
        }
        private void InitGrdSettleList()
        {
            string[] arrChange = new string[] { "달러(USD)", "원화(KRW)" };
            cbChange.Items.Clear();
            cbChange.Items.AddRange(arrChange);
            cbChange.SelectedIndex = 0;

            string today = DateTime.Today.ToShortDateString();
            txtDay.Text = today;

            btnAllOne.Text = "전 체";
            cbCode.Enabled = false;

            grdSettleListSummary.RowCount = 1;
            grdSettleListSummary.ColumnCount = 8;
            string[] arrSummary = new string[] { "매도수량", "", "매수수량", "", "청산손익", "", "순손익", "" };
            Color[] clrSettle = new Color[4]
            {Color.FromArgb(224,238,245), Color.FromArgb(241,247,251), Color.FromArgb(241,241,241), Color.FromArgb(240, 240, 240)};
            for(int i = 0; i < grdSettleListSummary.ColumnCount; i++)
            {
                grdSettleListSummary.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdSettleListSummary.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                grdSettleListSummary.Rows[0].Cells[i].ReadOnly = true;
                grdSettleListSummary.Rows[0].Cells[i].Value = arrSummary[i];

                if (i == 1 || i == 3)
                    grdSettleListSummary.Columns[i].Width = 40;
                else if (i == 5 || i == 7)
                    grdSettleListSummary.Columns[i].Width = 100;
                else
                    grdSettleListSummary.Columns[i].Width = 64;

                if (i % 2 == 0)
                {
                    grdSettleListSummary.Rows[0].Cells[i].Style.BackColor = clrSettle[0];
                }
                else
                    grdSettleListSummary.Rows[0].Cells[i].Style.BackColor = clrSettle[1];
            }
            grdSettleListSummary.ClearSelection();
            
            grdSettleListDetail.ColumnCount = 9;

            string[] arrHeader = new string[] { "종 목", "구 분", "수 량", "청산손익", "수수료", "순손익", "매입가격", "청산가격", "청산일자"};

            for (int i = 0; i < grdSettleListDetail.ColumnCount; i++)
            {
                if (i == 1 || i == 2 || i == 4)
                    grdSettleListDetail.Columns[i].Width = 50;
                else if (i == 8)
                    grdSettleListDetail.Columns[i].Width = 76;
                else
                    grdSettleListDetail.Columns[i].Width = 62;

                grdSettleListDetail.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdSettleListDetail.Columns[i].HeaderText = arrHeader[i];
                grdSettleListDetail.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < grdSettleListDetail.RowCount; i++)
            {
                if (i % 2 == 0)
                    grdSettleListDetail.Rows[i].DefaultCellStyle.BackColor = clrSettle[2];
            }
            
            grdSettleListDetail.ClearSelection();
        }

        // 데이터 세팅 

        private void SetAccNo()
        {
            if (axKFOpenAPI1.GetConnectState() == 1)
            {
                string[] arrTemp = axKFOpenAPI1.GetLoginInfo("ACCNO").Split(';');
                int nCnt = arrTemp.Length;
                nCnt--;
                if (nCnt != 0)
                {
                    string[] arrAccNo = new string[nCnt];
                    for (int i = 0; i < nCnt; i++)
                        arrAccNo[i] = arrTemp[i];

                    cbAccNo.Items.Clear();
                    cbAccNo.Items.AddRange(arrAccNo);
                    cbAccNo.SelectedIndex = 0;
                }
            }
        } // 계좌번호 세팅
        private void SetDataHogaGrid(string[] arrData, string strRealType)
        {
            string strData;
            int i, nCnt = arrData.Length;
            int nStart = 0, nEnd = nCnt;
            string[] strTemp = new string[nCnt];

            for (i = nStart; i < nEnd; i++)
            {
                strData = arrData[i];
                if (strData != null)
                {
                    if (lstHOGA[i].bTextColor)
                    {
                        SetDataFgColour(grdHoga, lstHOGA[i].nRow, lstHOGA[i].nCol, strData);
                    }
                    if (lstHOGA[i].nDataType == Constants.DT_SIGN)
                    {
                        SetDataFgColour(grdHoga, lstHOGA[i].nRow, lstHOGA[i].nCol, strData);
                    }
                    else
                    {
                        grdHoga.Rows[lstHOGA[i].nRow].Cells[lstHOGA[i].nCol].Value = ConvDataFormat(lstHOGA[i].nDataType, strData.Trim(), lstHOGA[i].strBeforeData, lstHOGA[i].strAfterData); //strData.Trim();
                    }
                }
            }
        }  // 호가 정보 세팅
        private void SetDataInfoGrid(string[] arrData, string strRealType)
        {
            string strData;
            int i, nCnt = arrData.Length;

            for (i = 0; i < nCnt; i++)
            {
                strData = arrData[i];

                if (lstINFO[i].bTextColor)
                {
                    SetDataFgColour(grdHoga, lstINFO[i].nRow, lstINFO[i].nCol, strData);
                }
                if (lstINFO[i].nDataType == Constants.DT_SIGN)
                {
                    SetDataFgColour(grdHoga, lstINFO[i].nRow, lstINFO[i].nCol, strData);
                }
                else
                {
                    grdHoga.Rows[lstINFO[i].nRow].Cells[lstINFO[i].nCol].Value = ConvDataFormat(lstINFO[i].nDataType, strData.Trim(), lstINFO[i].strBeforeData, lstINFO[i].strAfterData); //strData.Trim();
                }
            }
        }  // 종목 정보 세팅
        private void SetDataTickGrid(string[] arrData)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 3)
                    {
                        arrTickData[3 - i, j] = arrData[j];
                        SetDataFgColour(grdHoga, 10 - i, j, arrTickData[3 - i, 0]);
                        grdHoga.Rows[lstTICK[j].nRow].Cells[lstTICK[j].nCol].Value = ConvDataFormat(lstTICK[j].nDataType, arrTickData[3 - i, j], lstTICK[j].strBeforeData, lstTICK[j].strAfterData);
                    }
                    else
                    {
                        if (arrTickData[2 - i, j] != "")
                        {
                            arrTickData[3 - i, j] = arrTickData[2 - i, j];
                            SetDataFgColour(grdHoga, 10 - i, j, arrTickData[3 - i, 0]);
                            grdHoga.Rows[10 - i].Cells[j].Value = ConvDataFormat(lstTICK[j].nDataType, arrTickData[3 - i, j], lstTICK[j].strBeforeData, lstTICK[j].strAfterData);
                        }
                    }
                }
            }
        }  // 체결 정보 세팅


        // 서버 데이터 요청

        private void SendSearch()
        {
            SearchTimer();
            if (jongCount > 0)
            {
                strCodelist = "";

                for (int i = 0; i < jongCount; i++)
                    strCodelist += grdAccount.Rows[i].Cells[1].FormattedValue.ToString() + ";";

                string strRQName = "잔고내역";
                string strTRCode = "opw30003";
                axKFOpenAPI1.SetInputValue("계좌번호", m_AccNo);
                axKFOpenAPI1.SetInputValue("비밀번호", "");
                axKFOpenAPI1.SetInputValue("비밀번호입력매체", "00");
                axKFOpenAPI1.SetInputValue("통화코드", "USD");
                int iRet = axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
                string strErr;
                if (!(IsError(iRet)))
                {
                    strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", strTRCode, iRet);
                }
            }
        } // 
        private void SendJongSearch(string strData)
        {
            SearchTimer();
            m_strJongCode = strData;

            string strRQName = "선물옵션현재가";
            string strTRCode = "opt10001";

            axKFOpenAPI1.SetInputValue("종목코드", m_strJongCode);
            int iRet = axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
            string strErr;
            if (IsError(iRet))
            {
                strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", strTRCode, iRet);
            }
        }
        private void txtDay_TextMouseWheel(object sender, MouseEventArgs e)
        {
            int move = e.Delta * SystemInformation.MouseWheelScrollLines / 360;

            int year = System.Convert.ToInt32(txtDay.Text.Substring(0, 4));
            int month = System.Convert.ToInt32(txtDay.Text.Substring(5, 2));
            int day = System.Convert.ToInt32(txtDay.Text.Substring(8, 2));
            if (move < 0)
            {
                if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    day++;
                    if (day == 31)
                    {
                        day = 1;
                        month++;
                    }
                }
                else if (month == 2)
                {
                    day++;
                    if (year % 4 == 0 && day == 30)
                    {
                        day = 1;
                        month++;
                    }
                    else if (year % 4 != 0 && day == 29)
                    {
                        day = 1;
                        month++;
                    }
                }
                else
                {
                    day++;
                    if (day == 32)
                    {
                        day = 1;
                        month++;
                        if (month == 13)
                        {
                            month = 1;
                            year++;
                        }
                    }

                }
            }
            else
            {
                if (month == 5 || month == 7 || month == 10 || month == 12)
                {
                    day--;
                    if (day == 0)
                    {
                        day = 30;
                        month--;
                    }
                }
                else if (month == 3)
                {
                    day--;
                    if (year % 4 == 0)
                    {
                        day = 29;
                        month--;
                    }
                    else if (year % 4 != 0)
                    {
                        day = 28;
                        month--;
                    }
                }
                else
                {
                    day--;
                    if (day == 0)
                    {
                        day = 31;
                        month--;
                        if (month == 0)
                        {
                            month = 12;
                            year--;
                        }
                    }
                }
            }
            txtDay.Text = string.Format("{0:d4}-{1:d2}-{2:d2}", year, month, day);
        }

        // 서버 이벤트

        private void axKFOpenAPI1_OnEventConnet(object sender, AxKFOpenAPILib._DKFOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                m_Online = true;
                SetAccNo();
            }
            else
            {
                //접속 비정상 처리
                m_Online = false;
                Close();
            }
        }
        private void axKFOpenAPI1_OnReceiveTrData(object sender, AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            Logger(e.sRQName);
            string strScrType;
            string strKey = e.sScrNo;

            if (!m_mapScreenNum.TryGetValue(strKey, out strScrType))
                return;

            Form ChildForm = null;
            if (m_mapScreen.TryGetValue(strKey, out ChildForm))
            {
                switch (int.Parse(strScrType))
                {
                    case 0:
                        string strRQName = e.sRQName;
                        string sTrCode = e.sTrCode;
                        string strPrevNext = e.sPreNext;
                        if (strRQName == "잔고내역")
                        {
                            Logger("잔고내역");
                            // m_bNextFlag = false;

                            string strData;
                            int i, j, nCnt;
                            string strCode = "";
                            nCnt = axKFOpenAPI1.GetRepeatCnt(sTrCode, strRQName);
                            for (i = 0; i < jongCount; i++)
                            {
                                for (j = 2; j < grdAccount.ColumnCount; j++)
                                {
                                    grdAccount.Rows[i].Cells[j].Value = "";
                                }
                            }
                            for (i = 0; i < nCnt; i++)
                            {
                                for (j = 0; j < STGRIDRATE_SIZE; j++)
                                {
                                    strData = axKFOpenAPI1.GetCommData(strCode, strRQName, i, lstRATE[j].strKey);
                                    strData = strData.Trim();

                                    if (j == 0)
                                    {
                                        strCode = strData;
                                    }

                                    if (strData != "")
                                    {
                                        grdAccount.Rows[m_JongIndex[strCode]].Cells[lstRATE[j].nCol].Style.Alignment = (DataGridViewContentAlignment)lstRATE[j].nAlign;
                                        grdAccount.Rows[m_JongIndex[strCode]].Cells[lstRATE[j].nCol].Value = ConvDataFormat(lstRATE[j].nDataType, strData.Trim(), lstRATE[j].strBeforeData, lstRATE[j].strAfterData);
                                    }
                                }
                                // 손절가 설정
                                if (grdAccount.Rows[m_JongIndex[strCode]].Cells[4].Value.ToString() != "")
                                {
                                    double inPrice = System.Convert.ToDouble(grdAccount.Rows[m_JongIndex[strCode]].Cells[5].Value.ToString());
                                    double tickSize = this.tickSize[m_JongIndex[strCode]];
                                    if (grdAccount.Rows[m_JongIndex[strCode]].Cells[3].Value.ToString() == "매수")
                                    {
                                        tickSize = -1 * tickSize;
                                    }
                                    double stopPrice = inPrice + tickSize * tickValue[m_JongIndex[strCode]];
                                    Logger(stopPrice.ToString());
                                    grdAccount.Rows[m_JongIndex[strCode]].Cells[7].Value = ConvDataFormat(Constants.DT_DOUBLE, stopPrice.ToString(), "", "");
                                }
                                else
                                    grdAccount.Rows[m_JongIndex[strCode]].Cells[7].Value = "";

                                // 목표가 설정
                                if (grdAccount.Rows[m_JongIndex[strCode]].Cells[4].Value.ToString() != "" && goalPrice[m_JongIndex[strCode]] != "")
                                {
                                    grdAccount.Rows[m_JongIndex[strCode]].Cells[6].Value = ConvDataFormat(Constants.DT_DOUBLE, goalPrice[m_JongIndex[strCode]], "", "");
                                }
                                else
                                    grdAccount.Rows[m_JongIndex[strCode]].Cells[6].Value = "";
                            }
                            /*
                            if (strPrevNext.Trim() != "") //연속조회
                            {
                                m_bNextFlag = true; //연속조회여부

                                //연속조회를 한다.
                                axKFOpenAPI1.SetInputValue("계좌번호", m_AccNo);
                                axKFOpenAPI1.SetInputValue("비밀번호", "");
                                axKFOpenAPI1.SetInputValue("비밀번호입력매체", "00");
                                axKFOpenAPI1.SetInputValue("통화코드", "USD");
                                axKFOpenAPI1.CommRqData(strRQName, "opw30003", strPrevNext, m_strScrNo);
                            }
                            */
                        }
                        if (strRQName == "선물옵션현재가")
                        {
                            string strData;
                            string[] arrData;
                            int i, j, nCnt;

                            //기본정보
                            arrData = new string[STGRIDINFO_SIZE];
                            for (j = 0; j < STGRIDINFO_SIZE; j++)
                            {
                                strData = axKFOpenAPI1.GetCommData(e.sTrCode, strRQName, 0, lstINFO[j].strKey);
                                strData.Trim();
                                arrData[j] = strData;
                            }
                            SetDataInfoGrid(arrData, "");


                            //호가

                            arrData = new string[STGRIDHOGA_SIZE];
                            for (j = 0; j < STGRIDHOGA_SIZE; j++)
                            {
                                Array.Clear(arrData, 0, STGRIDHOGA_SIZE);
                                strData = axKFOpenAPI1.GetCommData(e.sTrCode, strRQName, 0, lstHOGA[j].strKey);
                                strData.Trim();
                                arrData[j] = strData;

                                SetDataHogaGrid(arrData, "");
                            }

                            //체결데이타
                            Array.Clear(arrData, 0, STGRIDTICK_SIZE);
                            nCnt = axKFOpenAPI1.GetRepeatCnt(e.sTrCode, strRQName);
                            for (i = 0; i < nCnt; i++)
                            {
                                Array.Clear(arrData, 0, STGRIDTICK_SIZE);
                                arrData = new string[STGRIDTICK_SIZE];
                                for (j = 0; j < STGRIDTICK_SIZE; j++)
                                {
                                    strData = axKFOpenAPI1.GetCommData(e.sTrCode, strRQName, i, lstTICK[j].strKey);
                                    strData.Trim();
                                    arrData[j] = strData;
                                }
                                SetDataTickGrid(arrData);

                            }
                        }
                        if (strRQName == "청산내역")
                        {
                            m_Settle = true;
                            string strData;
                            int i, j, nCnt;

                            nCnt = 4;
                            for(i = 0; i < nCnt; i++)
                            {
                                strData = axKFOpenAPI1.GetCommData(sTrCode, strRQName, i, lstSETTLE[i].strKey);
                                strData = strData.Trim();
                                
                                if (lstSETTLE[i].bTextColor)
                                    SetDataFgColour(grdSettleListSummary, lstSETTLE[i].nRow, lstSETTLE[i].nCol, strData);
                                if (strData != "")
                                {
                                    grdSettleListSummary.Rows[lstSETTLE[i].nRow].Cells[lstSETTLE[i].nCol].Style.Alignment = (DataGridViewContentAlignment)lstSETTLE[i].nAlign;
                                    grdSettleListSummary.Rows[lstSETTLE[i].nRow].Cells[lstSETTLE[i].nCol].Value = ConvDataFormat(lstSETTLE[i].nDataType, strData, lstSETTLE[i].strBeforeData, lstSETTLE[i].strAfterData);
                                }
                            }

                            nCnt = axKFOpenAPI1.GetRepeatCnt(sTrCode, strRQName);
                            if (nCnt == 0)
                            {
                                nCnt = 1;
                                grdSettleListDetail.RowCount = nCnt;
                                for (i = 0; i < grdSettleListDetail.ColumnCount; i++)
                                    grdSettleListDetail.Rows[0].Cells[i].Value = "";
                                return;
                            }
                            grdSettleListDetail.ClearSelection();
                            grdSettleListDetail.RowCount = nCnt;
                            bool backColor = true;
                            for (i = 0; i < nCnt; i++)
                            {
                                if (!backColor)
                                    backColor = true;
                                else
                                    backColor = false;

                                for (j = 0; j < grdSettleListDetail.ColumnCount; j++)
                                {
                                    strData = axKFOpenAPI1.GetCommData(sTrCode, strRQName, i, lstSETTLEDETAILE[j].strKey);
                                    strData = strData.Trim();
                                    if (backColor)
                                        grdSettleListDetail.Rows[i].Cells[lstSETTLEDETAILE[j].nCol].Style.BackColor = Color.FromArgb(240, 240, 240);
                                    if (lstSETTLEDETAILE[j].bTextColor)
                                        SetDataFgColour(grdSettleListDetail, i, lstSETTLEDETAILE[j].nCol, strData);
                                    if (j == 1)
                                    {
                                        if (strData == "1")
                                            grdSettleListDetail.Rows[i].Cells[lstSETTLEDETAILE[j].nCol].Style.ForeColor = Color.FromArgb(0, 0, 255);    // 지정된 셀의 텍스트 색상 설정
                                        else
                                            grdSettleListDetail.Rows[i].Cells[lstSETTLEDETAILE[j].nCol].Style.ForeColor = Color.FromArgb(255, 0, 0);    // 지정된 셀의 텍스트 색상 설정
                                    }  
                                    if (strData != "")
                                    {
                                        grdSettleListDetail.Rows[i].Cells[lstSETTLEDETAILE[j].nCol].Style.Alignment = (DataGridViewContentAlignment)lstSETTLEDETAILE[j].nAlign;
                                        grdSettleListDetail.Rows[i].Cells[lstSETTLEDETAILE[j].nCol].Value = ConvDataFormat(lstSETTLEDETAILE[j].nDataType, strData, lstSETTLEDETAILE[j].strBeforeData, lstSETTLEDETAILE[j].strAfterData);
                                    }
                                    else if(strData == "")
                                    {
                                        grdSettleListDetail.Rows[i].Cells[lstSETTLEDETAILE[j].nCol].Value = "";
                                    }
                                }
                            }
                        }
                        break;
                    case 1:  //현재가
                        ((Kwansim)ChildForm).axKFOpenAPI1_OnReceiveTrData(e.sScrNo, e.sRQName, e.sTrCode, e.sRecordName, e.sPreNext);
                        break;
                    case 2:  //자동매매
                        ((CurrentAuto)ChildForm).axKFOpenAPI1_OnReceiveTrData(e.sScrNo, e.sRQName, e.sTrCode, e.sRecordName, e.sPreNext);
                        break;
                }

            }
        }
        private void axKFOpenAPI1_OnReceiveRealData(object sender, AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            string strScrType;
            string strKey;
            int nCnt = m_mapScreen.Count();
            for (int i = 0; i < nCnt; i++)
            {
                strKey = m_mapScreen.ElementAt(i).Key;
                Form ChildForm = m_mapScreen.ElementAt(i).Value;
                if (ChildForm != null)
                {
                    m_mapScreenNum.TryGetValue(strKey, out strScrType);

                    switch (int.Parse(strScrType))
                    {
                        case 0:
                            string strData;

                            if ((e.sRealType.Trim() == "해외선물시세") || (e.sRealType.Trim() == "해외옵션시세"))
                            {
                                int index = m_JongIndex[e.sJongmokCode.Trim()];
                                strData = axKFOpenAPI1.GetCommRealData(e.sJongmokCode, int.Parse(lstRATE[1].strRealKey));
                                strData = strData.Trim();
                                SetDataFgColour(grdAccount, index, lstRATE[1].nCol, strData);
                                if (strData != "")
                                {
                                    grdAccount.Rows[index].Cells[lstRATE[1].nCol].Style.Alignment = (DataGridViewContentAlignment)lstRATE[1].nAlign;
                                    grdAccount.Rows[index].Cells[lstRATE[1].nCol].Value = ConvDataFormat(lstRATE[1].nDataType, strData, lstRATE[1].strBeforeData, lstRATE[1].strAfterData);
                                }
                                if(grdAccount.Rows[index].Cells[5].Value.ToString() != "")
                                {
                                    double curPrice = System.Convert.ToDouble(grdAccount.Rows[index].Cells[2].Value);
                                    double inPrice = System.Convert.ToDouble(grdAccount.Rows[index].Cells[5].Value);
                                    double gainPrice;
                                    if (grdAccount.Rows[index].Cells[3].Value.ToString() == "매수")
                                        gainPrice = curPrice - inPrice;
                                    else
                                        gainPrice = inPrice - curPrice;
                                    gainPrice = gainPrice * tickValue[index] / tickSize[index];
                                    gainPrice = Math.Round(gainPrice, 2);
                                    strData = gainPrice.ToString();
                                    SetDataFgColour(grdAccount, index, lstRATE[5].nCol, strData);
                                    if (strData != "")
                                    {
                                        grdAccount.Rows[index].Cells[lstRATE[5].nCol].Style.Alignment = (DataGridViewContentAlignment)lstRATE[5].nAlign;
                                        grdAccount.Rows[index].Cells[lstRATE[5].nCol].Value = strData;
                                    }
                                }
                                else
                                {
                                    grdAccount.Rows[index].Cells[lstRATE[5].nCol].Value = "";
                                }
                                
                                //체결
                                if (e.sJongmokCode == hogaJongCode)
                                {
                                    string[] arrData = new string[2];
                                    for (int j = 0; j < STGRIDTICK_SIZE; j++)
                                    {
                                        if (int.Parse(lstTICK[j].strRealKey) < 0)
                                        {
                                            arrData[j] = "";
                                            continue;
                                        }
                                        strData = axKFOpenAPI1.GetCommRealData(e.sJongmokCode, int.Parse(lstTICK[j].strRealKey));
                                        strData.Trim();
                                        arrData[j] = strData;
                                    }
                                    Logger(arrData[0] + "  " + arrData[1]);
                                    SetDataTickGrid(arrData);
                                }

                            }
                            if ((e.sRealType.Trim() == "해외선물호가") || (e.sRealType.Trim() == "해외옵션호가"))
                            {
                                string[] arrData = new string[STGRIDHOGA_SIZE]; 
                                if (hogaJongCode == e.sJongmokCode)
                                {
                                    for (int j = 0; j < STGRIDHOGA_SIZE; j++)
                                    {
                                        if (int.Parse(lstHOGA[j].strRealKey) < 0)
                                        {
                                            arrData[j] = "";
                                            continue;
                                        }
                                        strData = axKFOpenAPI1.GetCommRealData(e.sJongmokCode, int.Parse(lstHOGA[j].strRealKey));
                                        strData.Trim();
                                        arrData[j] = strData;
                                    }
                                    SetDataHogaGrid(arrData, e.sRealType);
                                }
                            }
                            break;
                        case 1:  //관심종목
                            ((Kwansim)ChildForm).axKFOpenAPI1_OnReceiveRealData(e.sJongmokCode, e.sRealType, e.sRealData);
                            break;
                        case 2:  //자동매매
                            ((CurrentAuto)ChildForm).axKFOpenAPI1_OnReceiveRealData(e.sJongmokCode, e.sRealType, e.sRealData);
                            break;
                    }

                }
            }
        }
        private void axKFOpenAPI1_OnReceiveChejanData(object sender, AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            string strScrType;
            string strKey;
            int nCnt = m_mapScreen.Count();
            for (int i = 0; i < nCnt; i++)
            {
                strKey = m_mapScreen.ElementAt(i).Key;
                Form ChildForm = m_mapScreen.ElementAt(i).Value;
                if (ChildForm != null)
                {
                    m_mapScreenNum.TryGetValue(strKey, out strScrType);

                    switch (int.Parse(strScrType))
                    {
                        case 0:
                            string strGubun = e.sGubun, strAccNo;

                            strAccNo = axKFOpenAPI1.GetChejanData(9201);
                            if(strAccNo == m_AccNo)
                            {
                                if (strGubun == "1")
                                {
                                    string strRQName = "잔고내역";
                                    string strTRCode = "opw30003";
                                    axKFOpenAPI1.SetInputValue("계좌번호", m_AccNo);
                                    axKFOpenAPI1.SetInputValue("비밀번호", "");
                                    axKFOpenAPI1.SetInputValue("비밀번호입력매체", "00");
                                    axKFOpenAPI1.SetInputValue("통화코드", "USD");
                                    int iRet = axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
                                    string strErr;
                                    if (!(IsError(iRet)))
                                    {
                                        strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", strTRCode, iRet);
                                    }
                                }
                            }
                            break;
                        case 1:  //현재가
                            ((Kwansim)ChildForm).axKFOpenAPI1_OnReceiveChejanData(e.sGubun, e.nItemCnt, e.sFidList);
                            break;
                        case 2:  //자동매매
                            ((CurrentAuto)ChildForm).axKFOpenAPI1_OnReceiveChejanData(e.sGubun, e.nItemCnt, e.sFidList);
                            break;
                    }
                }
            }
        }
        private void axKFOpenAPI1_OnReceiveMsg(object sender, AxKFOpenAPILib._DKFOpenAPIEvents_OnReceiveMsgEvent e)
        {
            string strRQ = e.sRQName;
            string strScrType, strKey = e.sScrNo;
            if (!m_mapScreenNum.TryGetValue(strKey, out strScrType))
                return;

            Form ChildForm = null;
            if (m_mapScreen.TryGetValue(strKey, out ChildForm))
            {
                string strData = string.Format("[{0:s}] [{1:s}] 오류", e.sRQName, e.sTrCode);

                switch (int.Parse(strScrType))
                {
                    case 1: //현재가
                        ((Kwansim)ChildForm).axKFOpenAPI1_OnReceiveMsg(e.sScrNo, e.sRQName, e.sTrCode, e.sMsg);
                        break;
                    case 2: //자동매매
                        ((CurrentAuto)ChildForm).axKFOpenAPI1_OnReceiveMsg(e.sScrNo, e.sRQName, e.sTrCode, e.sMsg);
                        break;
                }
            }
        }


        // 버튼 이벤트

        private void 로그인ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axKFOpenAPI1.GetConnectState() == 0)
                axKFOpenAPI1.CommConnect(1);
            else
                Logger("로그아웃 후 로그인 하십시오");
        } // 로그인 버튼 이벤트
        private void 로그아웃ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axKFOpenAPI1.GetConnectState() == 0)
                Logger("로그인 정보가 없습니다.");
            else
                axKFOpenAPI1.CommTerminate();
        } // 로그아웃 버튼 이벤트
        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_mapScreen.Clear();
            m_mapScreenNum.Clear();

            m_mapScreen = null;
            m_mapScreenNum = null;
        } // 종료 버튼 이벤트
        private void 종목설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GetNextScreenNum(1))
                return;

            Kwansim Kwansim = new Kwansim();
            Kwansim.Show(this);
            Kwansim.m_strScrNo = string.Format("{0:d4}", m_nScrNo);

            m_mapScreen.Add(Kwansim.m_strScrNo, Kwansim);
        } // 관심종목 설정 이벤트
        private void btnSearch_Click(object sender, EventArgs e) // 조회 버튼 이벤트
        {
            if (cbAccNo.Text == "")
                return;

            if (!GetNextScreenNum(0))
                return;
            
            m_AccNo = cbAccNo.Text;
            m_Online = true;

            m_strScrNo = string.Format("{0:d4}", m_nScrNo);
            m_mapScreen.Add(m_strScrNo, this);

            FileStream fs = new FileStream(SetFileName("Kwansim.txt"), FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string strTemp = sr.ReadToEnd();
            sr.Close();
            fs.Close();

            if (strTemp != "")
            {
                string[] arrTemp = strTemp.Split(';');
                jongCount = arrTemp.Length / 5;
                tradeVolume = new int[jongCount];
                stopCount = new int[jongCount];
                tickSize = new double[jongCount];
                tickValue = new double[jongCount];
                for (int i = 0; i < jongCount; i++)
                {
                    grdAccount.Rows[i].Cells[1].ReadOnly = true;
                    grdAccount.Rows[i].Cells[1].Value = arrTemp[i * 5];
                    tradeVolume[i] = System.Convert.ToInt32(arrTemp[i * 5 + 1]);
                    stopCount[i] = System.Convert.ToInt32(arrTemp[i * 5 + 2]);
                    tickSize[i] = System.Convert.ToDouble(arrTemp[i * 5 + 3]);
                    tickValue[i] = System.Convert.ToDouble(arrTemp[i * 5 + 4]);
                }

                if (jongCount != 4)
                {
                    for (int i = jongCount; i < 4; i++)
                    {
                        grdAccount.Rows[i].Cells[1].ReadOnly = true;
                        grdAccount.Rows[i].Cells[1].Value = "";
                    }

                }
            }
            _bAutoTrade = new bool[jongCount];
            for (int i = 0; i < jongCount; i++)
            {
                _bAutoTrade[i] = false;
                grdAccount.Rows[i].Cells["btnAutoTrade"].Value = "수 동";
            }

            m_JongIndex.Clear();
            if (jongCount > 0)
            {
                for(int i =0; i < jongCount; i++)
                {
                    m_JongIndex.Add(grdAccount.Rows[i].Cells[1].Value.ToString(), i);
                }
            }
            SendSearch();
        }
        private void grdAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex >= jongCount)
                    return;

                if (!GetNextScreenNum(2))
                    return;

                if (!_bAutoTrade[e.RowIndex])
                {
                    if (_bAutoTrade[e.RowIndex] == false)
                    {
                        CurrentAuto CurrentAuto = new CurrentAuto();
                        CurrentAuto.m_strScrNo = string.Format("{0:d4}", m_nScrNo);
                        CurrentAuto.m_strAccNo = m_AccNo;
                        CurrentAuto.m_strJongCode = grdAccount.Rows[e.RowIndex].Cells[1].Value.ToString();
                        CurrentAuto.tradeVolume = tradeVolume[e.RowIndex].ToString();
                        CurrentAuto.tradeStoploss = stopCount[e.RowIndex].ToString();

                        CurrentAuto.Show(this);

                        m_mapScreen.Add(CurrentAuto.m_strScrNo, CurrentAuto);

                        _bAutoTrade[e.RowIndex] = true;
                        grdAccount.Rows[e.RowIndex].Cells["btnAutoTrade"].ReadOnly = true;
                        grdAccount.Rows[e.RowIndex].Cells["btnAutoTrade"].Value = "자 동";
                    }

                }
            }

            else if (e.ColumnIndex == 1)
            {
                if (e.RowIndex < jongCount && jongInfoIndex != e.RowIndex)
                {
                    jongInfoIndex = e.RowIndex;
                    hogaJongCode = grdAccount.Rows[e.RowIndex].Cells[1].Value.ToString();
                    groupBox3.Text = "종목 정보 : " + grdAccount.Rows[e.RowIndex].Cells[1].Value.ToString();
                    // SendJongSearch(hogaJongCode);
                }

            }
        }
        private void grdSettleListDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void btnAllOne_Click(object sender, EventArgs e)
        {
            if (btnAllOne.Text == "전 체")
            {
                btnAllOne.Text = "종 목";
                cbCode.Enabled = true;
                if (strCodelist != "")
                {
                    Logger(strCodelist);
                    string[] arrCode = strCodelist.Split(';');
                    string[] arrTemp = new string[arrCode.Length - 1];
                    for (int i = 0; i < arrCode.Length - 1; i++)
                        arrTemp[i] = arrCode[i];
                    cbCode.Items.Clear();
                    cbCode.Items.AddRange(arrTemp);
                    cbCode.SelectedIndex = 0;
                }
            }
            else
            {
                btnAllOne.Text = "전 체";
                cbCode.Enabled = false;
            }
        }
        private void btnSerchList_Click(object sender, EventArgs e) // 청산 내역 조회
        {
            SearchTimer();
            string strTRCode = "opw30007";
            string strDate = txtDay.Text.Substring(0, 4) + txtDay.Text.Substring(5, 2) + txtDay.Text.Substring(8, 2);
            string strChange;
            string strJongCode;
            string strRQName = "청산내역";
            if (cbChange.Text == "달러(USD)")
                strChange = "USD";
            else
                strChange = "KRW";

            if (btnAllOne.Text == "전 체")
                strJongCode = "";
            else
            {
                if (cbCode.Text == "")
                    return;

                strJongCode = cbCode.Text;
            }
            if (!m_Online)
            {
                MessageBox.Show("로그인 정보가 없습니다.");
                return;
            }
            axKFOpenAPI1.SetInputValue("조회일자", strDate);
            axKFOpenAPI1.SetInputValue("계좌번호", m_AccNo);
            axKFOpenAPI1.SetInputValue("비밀번호", "");
            axKFOpenAPI1.SetInputValue("비밀번호입력매체", "00");
            axKFOpenAPI1.SetInputValue("통화코드", strChange);
            axKFOpenAPI1.SetInputValue("종목코드", strJongCode);

            int iRet = axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
            string strErr;
            if (!(IsError(iRet)))
            {
                strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", strTRCode, iRet);
            }
            Logger("청산 내역 조회");
        }

        // 데이터 처리

        protected override void WndProc(ref Message m)
        {
            string strKey, strScrType;
            switch (m.Msg)
            {
                case Constants.UM_SCREEN_CLOSE:
                    int iVal = (int)m.LParam;
                    strKey = string.Format("{0:d4}", iVal);

                    if (m_mapScreenNum.TryGetValue(strKey, out strScrType))
                        m_mapScreenNum.Remove(strKey);

                    Form childForm = null;
                    if (m_mapScreen.TryGetValue(strKey, out childForm))
                    {
                        axKFOpenAPI1.DisconnectRealData(strKey);
                        m_mapScreen.Remove(strKey);

                    }
                    break;
            }
            base.WndProc(ref m);
        }
        public bool IsError(int iErrCode)
        {
            string strMsg = "";
            switch (iErrCode)
            {
                case Constants.OP_ERR_NO_LOGIN:
                    strMsg = "미접속상태";
                    break;
                case Constants.OP_ERR_TRCODE:
                    strMsg = "TrCode가 존재하지 않습니다.";
                    break;
                case Constants.OP_ERR_SISE_OVERFLOW:
                    strMsg = "시세조회 과부하";
                    break;
                case Constants.OP_ERR_ORDER_OVERFLOW:
                    strMsg = "주문 과부하";
                    break;
                case Constants.OP_ERR_RQ_WRONG_INPUT:
                    strMsg = "조회입력값 오류";
                    break;
                case Constants.OP_ERR_ORD_WRONG_INPUT:
                    strMsg = "주문입력값 오류";
                    break;
                case Constants.OP_ERR_ORD_WRONG_ACCPWD:
                    strMsg = "계좌비밀번호를 입력하십시오.";
                    break;
                case Constants.OP_ERR_ORD_WRONG_ACCNO:
                    strMsg = "타인 계좌를 사용할 수 없습니다.";
                    break;
                case Constants.OP_ERR_ORD_WRONG_QTY200:
                    strMsg = "경고-주문수량 200개 초과";
                    break;
                case Constants.OP_ERR_ORD_WRONG_QTY400:
                    strMsg = "제한-주문수량 400개 초과";
                    break;
            }

            // 에러 메세지 처리
            if (strMsg != "")
            {
                MessageBox.Show(strMsg);
                return false;
            }

            return true;
        }
        public string ConvDataFormat(int nType, string szData, string szBeforeData, string szAfterData)
        {
            string strReturn, strData, strTemp = strData = szData;

            switch (nType)
            {
                case Constants.DT_DATE:
                    if (strTemp.Length == 6)
                        strData = string.Format("{0:d2}/{1:d2}/{2:d2}", int.Parse(strTemp.Substring(0, 2)), int.Parse(strTemp.Substring(2, 2)), int.Parse(strTemp.Substring(4, 2)));
                    else if (strTemp.Length == 8)
                        strData = string.Format("{0:d4}/{1:d2}/{2:d2}", int.Parse(strTemp.Substring(0, 4)), int.Parse(strTemp.Substring(4, 2)), int.Parse(strTemp.Substring(6, 2)));
                    else
                        break;
                    break;
                case Constants.DT_TIME:
                    if (strTemp.Length == 6)
                        strData = string.Format("{0:d2}:{1:d2}:{2:d2}", int.Parse(strTemp.Substring(0, 2)), int.Parse(strTemp.Substring(2, 2)), int.Parse(strTemp.Substring(4, 2)));
                    //else if (strTemp.Length == 8)
                    //    strData = string.Format("{0:d4}:{1:d2}:{2:d2}:{2:d2}", int.Parse(strTemp.Substring(0, 4)), int.Parse(strTemp.Substring(4, 2)), int.Parse(strTemp.Substring(6, 2)), int.Parse(strTemp.Substring(8, 2)));
                    break;
                case Constants.DT_NUMBER:
                    break;
                case Constants.DT_ZERO_NUMBER:
                    strTemp = strTemp.Replace("+", "");
                    strTemp = strTemp.Replace("-", "");
                    if (strTemp == "")
                        break;
                    if (float.Parse(strTemp) == 0.00)
                    {
                        strData = nType == Constants.DT_ZERO_NUMBER ? strTemp : "";
                        break;
                    }
                    strData = strTemp;
                    break;
                case Constants.DT_NUMBER_NOCOMMA:
                    if (int.Parse(strTemp) == 0)
                        strData = "";
                    else
                        strData = string.Format("{0:d}", int.Parse(strTemp));
                    break;
                case Constants.DT_ORDREG_GB: //접수구분(1:접수, 2:확인, 3:체결, X:거부)
                    {
                        if (strTemp == "1")
                            strData = "접수";
                        else if (strTemp == "2")
                            strData = "확인";
                        else if (strTemp == "3")
                            strData = "체결";
                        else if (strTemp == "X")
                            strData = "거부";
                    }
                    break;
                case Constants.DT_ORDTYPE: // 주문유형(1:시장가, 2:지정가, 3:STOP, 4:STOPLIMIT)
                    {
                        if (int.Parse(strTemp) == 1)
                            strData = "시장가";
                        else if (int.Parse(strTemp) == 2)
                            strData = "지정가";
                        else if (int.Parse(strTemp) == 3)
                            strData = "STOP";
                        else if (int.Parse(strTemp) == 4)
                            strData = "STOPLIMIT";
                    }
                    break;
                case Constants.DT_DOUBLE:
                    {
                        int nFind = strTemp.IndexOf(".");
                        if (nFind < 0)
                            strTemp = string.Format("{0:f2}", float.Parse(strTemp) / 100);

                        strData = strTemp;
                    }
                    break;
                case Constants.DT_ORDGUBUN:
                    {
                        if (int.Parse(strTemp) == 2)
                            strData = "매수";
                        else if (int.Parse(strTemp) == 1)
                            strData = "매도";
                    }
                    break;
                case Constants.DT_PRICE:
                    {
                        strTemp = strTemp.Replace("+", "");
                        strTemp = strTemp.Replace("-", "");
                        strData = strTemp;
                    }
                    break;
            }

            strReturn = string.Format("{0:s}{1:s}{2:s}", szBeforeData, strData, szAfterData);

            return strReturn;
        }
        public void SetSignData(DataGridView pGrid, int nRow, int nCol, string szData)
        {
            if (szData == "") // 오류
                szData = "3";
            string strData = szData;
            switch (int.Parse(strData))
            {
                case 1:
                    strData = "↑";
                    pGrid.Rows[nRow].Cells[nCol].Style.ForeColor = Color.FromArgb(255, 0, 0);	// 지정된 셀의 텍스트 색상 설정
                    break;
                case 2:
                    strData = "▲";
                    pGrid.Rows[nRow].Cells[nCol].Style.ForeColor = Color.FromArgb(255, 0, 0);	// 지정된 셀의 텍스트 색상 설정
                    break;
                case 3: strData = ""; break;
                case 4:
                    strData = "↓";
                    pGrid.Rows[nRow].Cells[nCol].Style.ForeColor = Color.FromArgb(0, 0, 255);	// 지정된 셀의 텍스트 색상 설정
                    break;
                case 5:
                    strData = "▼";
                    pGrid.Rows[nRow].Cells[nCol].Style.ForeColor = Color.FromArgb(0, 0, 255);	// 지정된 셀의 텍스트 색상 설정
                    break;
            }
            pGrid.Rows[nRow].Cells[nCol].Value = strData;
        }
        public void SetDataFgColour(DataGridView pGrid, int nRow, int nCol, string szData)
        {
            if (szData == "")
                return;
            string strData = szData;
            if (strData.Substring(0, 1) == "-")
            {
                pGrid.Rows[nRow].Cells[nCol].Style.ForeColor = Color.FromArgb(0, 0, 255);	// 지정된 셀의 텍스트 색상 설정
            }
            else //if (strData.Substring(0, 1) == "+")
            {
                pGrid.Rows[nRow].Cells[nCol].Style.ForeColor = Color.FromArgb(255, 0, 0); // 지정된 셀의 텍스트 색상 설정               	
            }

        }
        private bool GetNextScreenNum(int nScreenType)
        {
            int nRepeat = 0;
            m_nScrNo++;
            if (m_nScrNo > 9999)
            {
                nRepeat++;
                m_nScrNo = 1;
            }

            if (nRepeat > 1)
            {
                nRepeat = 0;
                MessageBox.Show("더 이상 화면을 열 수 없습니다. 다른 화면을 닫고 실행 해 주세요!");
                return false;
            }

            string strKey = string.Format("{0:d4}", m_nScrNo);
            string strTemp;
            if (m_mapScreenNum.TryGetValue(strKey, out strTemp))
                return GetNextScreenNum(nScreenType);

            nRepeat = 0;
            strTemp = string.Format("{0:d}", nScreenType);
            m_mapScreenNum.Add(strKey, strTemp);
            return true;
        }
        public string SetFileName(string strData)
        {
            m_strPath = System.IO.Path.GetDirectoryName(m_strPath);

            string sDirPath;
            sDirPath = Application.StartupPath + "\\data\\";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sDirPath);
            if (!di.Exists)  // 경로가 존재 하지 않으면 경로를 생성하라!
                di.Create();

            string fileName = sDirPath + strData;

            return fileName;

        }
        private string TransUnit(string strCode, double dValue)
        {
            string subCode = strCode.Substring(0, 2);
            double[] tickInfo = new double[4];
            string transValue;
            if (subCode == "6E")
            {
                transValue = dValue.ToString("F5");
            }
            else if (subCode == "CL")
            {
                transValue = dValue.ToString("F2");
            }
            else if (subCode == "GC")
            {
                transValue = dValue.ToString("F1");
            }
            else if (subCode == "ES")
            {
                transValue = dValue.ToString("F2");
            }
            else
            {
                transValue = dValue.ToString();
            }
            return transValue;
        }
        public void SetAutoButton(string strCode)
        {
            for (int i = 0; i < jongCount; i++)
            {
                if (strCode == grdAccount.Rows[i].Cells[1].Value.ToString())
                {
                    _bAutoTrade[i] = false;
                    grdAccount.Rows[i].Cells["btnAutoTrade"].ReadOnly = false;
                    grdAccount.Rows[i].Cells["btnAutoTrade"].Value = "수 동";
                    Logger((i + 1).ToString() + "번째");
                }
            }
        }
        public void SetGoalPrice(string strCode, string gPrice)
        {
            goalPrice[m_JongIndex[strCode]] = gPrice;
            Logger("종목명 : "+strCode + ", 청산가 : " + goalPrice[m_JongIndex[strCode]]);
        }

        //test
        
        private void btnTest_Click(object sender, EventArgs e)
        {
            SearchTimer();
        }
        public void Logger(object strData)
        {
            lbTest.Items.Insert(0, strData);
        }

        private void txtDay_TextChanged(object sender, EventArgs e)
        {
            Logger(txtDay.Text);
            if (txtDay.TextLength == 10)
            {
                string date = txtDay.Text.Substring(0, 4) + txtDay.Text.Substring(5, 2) + txtDay.Text.Substring(8, 2);
                Logger(date);
            }
        }        

        private void cbChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Settle)
            {
                btnSearchList.PerformClick();
            }
        }

        private void TradeLog(string strData)
        {
            string today = DateTime.Today.ToShortDateString();
            string now = DateTime.Now.ToLongTimeString();

            strData = today + " " + now + " | " + strData;

            m_strPath = System.IO.Path.GetDirectoryName(m_strPath);

            string sDirPath;
            sDirPath = Application.StartupPath + "\\log\\";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sDirPath);
            if (!di.Exists)  // 경로가 존재 하지 않으면 경로를 생성하라!
                di.Create();

            string fileName = sDirPath + "test" + ".txt";
            FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs);

            sr.WriteLine(strData);

            sr.Close();
            fs.Close();
        }

        public void SearchTimer()
        {
            DateTime now = DateTime.Now;
            if (lastSearchTime.Hour == now.Hour)
            {
                if (lastSearchTime.Minute == now.Minute)
                {
                    if (now.Second - lastSearchTime.Second == 0)
                    {
                        if (now.Millisecond - lastSearchTime.Millisecond < 335)
                            searchCounter++;
                        else if (now.Millisecond - lastSearchTime.Millisecond >= 335 && now.Millisecond - lastSearchTime.Millisecond < 1000)
                        {
                            if (searchCounter > 1)
                                searchCounter--;
                        }
                        else
                            searchCounter = 1;
                    }
                    else if (now.Second - lastSearchTime.Second == 1)
                    {
                        if (now.Millisecond + 1000 - lastSearchTime.Millisecond < 335)
                            searchCounter++;
                        else if (now.Millisecond + 1000 - lastSearchTime.Millisecond >= 335 && now.Millisecond + 1000 - lastSearchTime.Millisecond < 1000)
                        {
                            if (searchCounter > 1)
                                searchCounter--;
                        }
                        else
                            searchCounter = 1;
                    }
                    else
                        searchCounter = 1;
                }
                else
                    searchCounter = 1;
            }
            else
                searchCounter = 1;
            lastSearchTime = now;

            if (searchCounter > 4)
            {
                searchCounter = 3;
                System.Threading.Thread.Sleep(1000);
            }

            Logger(searchCounter);
        }
    }

    public static class Constants
    {
        public const string TR_OPT10001 = "opt10001"; //현재가
        public const string TR_OPT10003 = "opt10005"; //관심종목
        public const string TR_OPC10001 = "opc10001"; //틱차트
        public const string TR_OPC10002 = "opc10002"; //분차트
        public const string TR_OPC10003 = "opc10003"; //일차트
        public const string TR_OPC10004 = "opc10004";//주차트
        public const string TR_OPC10005 = "opc10005"; //월차트
        public const string TR_OPW30005 = "opw30005"; //주문체결
        public const string TR_OPW30003 = "opw30003"; //잔고내역

        public const int OP_ERR_NONE = 0; //"정상처리" 
        public const int OP_ERR_NO_LOGIN = -1; //"미접속상태"	
        public const int OP_ERR_CONNECT = -101; //"서버 접속 실패" 
        public const int OP_ERR_VERSION = -102; //"버전처리가 실패하였습니다. 
        public const int OP_ERR_TRCODE = -103; //"TrCode가 존재하지 않습니다.”
        public const int OP_ERR_SISE_OVERFLOW = -200; //”시세과부하”
        public const int OP_ERR_ORDER_OVERFLOW = -201; //”주문과부하” 
        public const int OP_ERR_RQ_WRONG_INPUT = -202; //”조회입력값 오류”
        public const int OP_ERR_ORD_WRONG_INPUT = -300; //”주문입력값 오류”
        public const int OP_ERR_ORD_WRONG_ACCPWD = -301; //”계좌비밀번호를 입력하십시오.”
        public const int OP_ERR_ORD_WRONG_ACCNO = -302; //”타인 계좌를 사용할 수 없습니다.”
        public const int OP_ERR_ORD_WRONG_QTY200 = -303; //”경고-주문수량 200개 초과”
        public const int OP_ERR_ORD_WRONG_QTY400 = -304; //”제한-주문수량 400개 초과.”

        public const int DT_NONE = 0;		// 기본문자 형식
        public const int DT_DATE = 1;		// 일자 형식
        public const int DT_TIME = 2;		// 시간 형식
        public const int DT_NUMBER = 3;		// 콤마 숫자 형식
        public const int DT_ZERO_NUMBER = 4;		// 콤마 숫자(0표시) 형식
        public const int DT_SIGN = 5;		// 대비기호 형식
        public const int DT_NUMBER_NOCOMMA = 6;		// 콤마없는 숫자 형식
        public const int DT_ORDREG_GB = 7;		// 접수구분(1:접수, 2:확인, 3:체결, X:거부)
        public const int DT_ORDTYPE = 8;		// 주문유형(1:시장가, 2:지정가, 3:STOP, 4:STOPLIMIT)
        public const int DT_DOUBLE = 9;		// 실수값 처리( 소수점 2자리까지)
        public const int DT_ORDGUBUN = 10;		// 주문구분(1:매도, 2:매수)
        public const int DT_PRICE = 11;		// 가격표시

        public const int DT_LEFT = 16;
        public const int DT_RIGHT = 64;
        public const int DT_CENTER = 32;

        public const int UM_SCREEN_CLOSE = 1000;
    }

    public class CommonLib
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    }
}
