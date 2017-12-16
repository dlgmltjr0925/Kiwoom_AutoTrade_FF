using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kiwoom_AutoTrade_FF
{
    public partial class JongInfo : Form
    {
        public JongInfo()
        {
            InitializeComponent();
        }


        const string m_strRealSet = "해외선물시세;해외선물호가;해외옵션시세;해외옵션호가";

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
            new stGRID("현재가",      "10",   6, 1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("체결량",      "15",   6, 0,  Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", "")
        };

        public string m_strJongCode = "";
        public string m_strScrNo;

        
        public void JongInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonLib.PostMessage(((Kiwoom_AutoTrade_FF)this.Owner).Handle, Constants.UM_SCREEN_CLOSE, 0, int.Parse(m_strScrNo));
        }

        public void SendSearch(string strCode)
        {
            m_strJongCode = strCode;

            string strRQName = "선물옵션현재가";
            string strTRCode = "opt10001";

            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("종목코드", m_strJongCode);
            int iRet = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
            string strErr;
            if (((Kiwoom_AutoTrade_FF)this.Owner).IsError(iRet))
            {
                strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", strTRCode, iRet);
            }
        }

        public void axKFOpenAPI1_OnReceiveTrData(string sScrNo, string sRQName, string sTrCode, string sRecordName, string sPreNext)
        {
            string strRQName = sRQName;
            if (strRQName == "선물옵션현재가")
            {
                string strData;
                string[] arrData;
                int i, j, nCnt;

                //기본정보
                arrData = new string[STGRIDINFO_SIZE];
                for (j = 0; j < STGRIDINFO_SIZE; j++)
                {
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, lstINFO[j].strKey);
                    strData.Trim();
                    arrData[j] = strData;
                }
                SetDataInfoGrid(arrData, "");


                //호가

                arrData = new string[STGRIDHOGA_SIZE];
                for (j = 0; j < STGRIDHOGA_SIZE; j++)
                {
                    Array.Clear(arrData, 0, STGRIDHOGA_SIZE);
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, lstHOGA[j].strKey);
                    strData.Trim();
                    arrData[j] = strData;

                    SetDataHogaGrid(arrData, "");
                }

                //체결데이타
                Array.Clear(arrData, 0, STGRIDTICK_SIZE);
                nCnt = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetRepeatCnt(sTrCode, strRQName);
                for (i = 0; i < nCnt; i++)
                {
                    Array.Clear(arrData, 0, STGRIDTICK_SIZE);
                    arrData = new string[STGRIDTICK_SIZE];
                    for (j = 0; j < STGRIDTICK_SIZE; j++)
                    {
                        strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, i, lstTICK[j].strKey);
                        strData.Trim();
                        arrData[j] = strData;
                    }
                    SetDataTickGrid(arrData);

                }
            }
        }

        public void axKFOpenAPI1_OnReceiveRealData(string sJongmokCode, string sRealType, string sRealData)
        {
            if (m_strJongCode.CompareTo(sJongmokCode) != 0)
                return;

            string strData;
            string[] arrData;

            if ((sRealType.Trim() == "해외선물시세") || (sRealType.Trim() == "해외옵션시세"))
            {
                //기본정보
                arrData = new string[STGRIDINFO_SIZE];
                for (int i = 0; i < STGRIDINFO_SIZE; i++)
                {
                    if (int.Parse(lstINFO[i].strRealKey) < 0)
                    {
                        arrData[i] = "";
                        continue;
                    }
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommRealData(sJongmokCode, int.Parse(lstINFO[i].strRealKey));
                    strData.Trim();
                    arrData[i] = strData;
                }
                SetDataInfoGrid(arrData, sRealType);

                //체결
                Array.Clear(arrData, 0, STGRIDINFO_SIZE);
                for (int i = 0; i < STGRIDTICK_SIZE; i++)
                {
                    if (int.Parse(lstTICK[i].strRealKey) < 0)
                    {
                        arrData[i] = "";
                        continue;
                    }
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommRealData(sJongmokCode, int.Parse(lstTICK[i].strRealKey));
                    strData.Trim();
                    arrData[i] = strData;
                }
                SetDataTickGrid(arrData);
            }

            if ((sRealType.Trim() == "해외선물호가") || (sRealType.Trim() == "해외옵션호가"))
            {
                arrData = new string[STGRIDHOGA_SIZE];
                for (int i = 0; i < STGRIDHOGA_SIZE; i++)
                {
                    if (int.Parse(lstHOGA[i].strRealKey) < 0)
                    {
                        arrData[i] = "";
                        continue;
                    }
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommRealData(sJongmokCode, int.Parse(lstHOGA[i].strRealKey));
                    strData.Trim();
                    arrData[i] = strData;
                }
                SetDataHogaGrid(arrData, sRealType);
            }
        }

        public void axKFOpenAPI1_OnReceiveMsg(string sScrNo, string sRQName, string sTrCode, string sMsg)
        {
        }

        public void axKFOpenAPI1_OnReceiveChejanData(string sGubun, int nItemCnt, string sFidList)
        {
        }

        private void SetDataHogaGrid(string[] arrData, string strRealType)
        {
            string strData;
            int i, nCnt = arrData.Length;
            int nStart = 0, nEnd = nCnt;
            string[] strTemp = new string[nCnt];

            for (i = nStart; i < nEnd; i++)
            {
                strData = arrData[i];
                if(strData != null)
                {
                    if (lstHOGA[i].bTextColor)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(((Kiwoom_AutoTrade_FF)this.Owner).grdHoga, lstHOGA[i].nRow, lstHOGA[i].nCol, strData);
                    }
                    if (lstHOGA[i].nDataType == Constants.DT_SIGN)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(((Kiwoom_AutoTrade_FF)this.Owner).grdHoga, lstHOGA[i].nRow, lstHOGA[i].nCol, strData);
                    }
                    else
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).grdHoga.Rows[lstHOGA[i].nRow].Cells[lstHOGA[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstHOGA[i].nDataType, strData.Trim(), lstHOGA[i].strBeforeData, lstHOGA[i].strAfterData); //strData.Trim();
                    }
                }
            }
        }
        private void SetDataInfoGrid(string[] arrData, string strRealType)
        {
            string strData;
            int i, nCnt = arrData.Length;

            for (i = 0; i < nCnt; i++)
            {
                strData = arrData[i];

                if (lstINFO[i].bTextColor)
                {
                    ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(((Kiwoom_AutoTrade_FF)this.Owner).grdHoga, lstINFO[i].nRow, lstINFO[i].nCol, strData);
                }
                if (lstINFO[i].nDataType == Constants.DT_SIGN)
                {
                    ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(((Kiwoom_AutoTrade_FF)this.Owner).grdHoga, lstINFO[i].nRow, lstINFO[i].nCol, strData);
                }
                else
                {
                    ((Kiwoom_AutoTrade_FF)this.Owner).grdHoga.Rows[lstINFO[i].nRow].Cells[lstINFO[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstINFO[i].nDataType, strData.Trim(), lstINFO[i].strBeforeData, lstINFO[i].strAfterData); //strData.Trim();
                }
            }
        }
        private void SetDataTickGrid(string[] arrData)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; i < 2; j++)
                {
                    if (i == 4)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).grdHoga.Rows[lstTICK[j].nRow].Cells[lstTICK[j].nCol].Value = arrData[j];
                    }
                    else
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).grdHoga.Rows[10 - i].Cells[j].Value = ((Kiwoom_AutoTrade_FF)this.Owner).grdHoga.Rows[9 - i].Cells[j].Value;
                    }
                }
            }
        }
    }
}