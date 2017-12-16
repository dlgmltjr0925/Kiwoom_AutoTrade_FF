using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Kiwoom_AutoTrade_FF
{
    public partial class CurrentAuto : Form
    {
        const string m_strRealSet = "해외선물시세;해외선물호가;해외옵션시세;해외옵션호가";
        private Dictionary<string, string> m_mapFIDName = new Dictionary<string, string>();

        const int STGRID10001_SIZE = 27;
        public stGRID[] lstOPT10001 = new stGRID[STGRID10001_SIZE]
        {
            new stGRID("종목명",         "-1",  -1, -1,  Constants.DT_NONE,            false,  Constants.DT_LEFT,      "", "" ),
            // 현재가 그리드  
            new stGRID("현재가",       "10",   0,  0,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("대비기호",      "25",   0,  1,  Constants.DT_SIGN,          true,   Constants.DT_CENTER,    "", ""),
            new stGRID("전일대비",      "11",   0,  2,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("등락율",       "12",   0,  3,  Constants.DT_ZERO_NUMBER,   true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("거래량",       "13",   0,  4,  Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("거래량대비", "30",   0,  5,  Constants.DT_ZERO_NUMBER,   true,   Constants.DT_RIGHT,     "", ""), 
	        /////////////////////////////////////
	        // 기본정보1
	        new stGRID("거래소",       "-1",   0,  1,  Constants.DT_NONE,         false,   Constants.DT_CENTER,    "", ""),
            new stGRID("시가",            "-1",   1,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("저가",            "-1",   2,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("고가",            "-1",   3,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("영업일자",      "-1",   4,  1,  Constants.DT_DATE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("잔존만기",      "-1",   5,  1,  Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("틱단위",       "-1",   6,  1,  Constants.DT_NONE,          false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("시작시간",      "-1",   7,  1,  Constants.DT_TIME,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("상장중최고가",    "-1",   8,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("상장중최저가",    "-1",   9,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
	        /////////////////////////////////////
	        // 기본정보2
	        new stGRID("레버리지",      "-1",   0,  3,  Constants.DT_ZERO_NUMBER,   false,  Constants.DT_CENTER,    "", ""),
            new stGRID("정산가",       "-1",   1,  3,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("품목구분",      "-1",   2,  3,  Constants.DT_NONE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("최종거래",      "-1",   3,  3,  Constants.DT_DATE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("결제구분",      "-1",   4,  3,  Constants.DT_NONE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("결제통화",      "-1",   5,  3,  Constants.DT_NONE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("틱가치",       "-1",   6,  3,  Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("종료시간",      "-1",   7,  3,  Constants.DT_TIME,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("상장중최고일",    "-1",   8,  3,  Constants.DT_DATE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("상장중최저일",    "-1",   9,  3,  Constants.DT_DATE,          false,  Constants.DT_CENTER,    "", "")
        };

        const int STGRID10002_SIZE = 6;
        public stGRID[] lstOPT10002 = new stGRID[STGRID10002_SIZE]
        {
            new stGRID("체결시간n",     "20",   -1, 0,  Constants.DT_TIME,          false,  Constants.DT_CENTER,    "", "" ),
            new stGRID("현재가n",      "10",   -1, 1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("대비기호n",     "25",   -1, 2,  Constants.DT_SIGN,          true,   Constants.DT_CENTER,    "", ""),
            new stGRID("전일대비n",     "11",   -1, 3,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("등락율n",      "12",   -1, 4,  Constants.DT_ZERO_NUMBER,   true,   Constants.DT_RIGHT,     "", "%"),
            new stGRID("체결량n",      "15",   -1, 5,  Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", "")
        };

        const int STGRID10003_SIZE = 23;
        public stGRID[] lstOPT10003 = new stGRID[STGRID10003_SIZE]
        {
            //매수호가
            new stGRID("매도수량5",     "65",   0,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가5",     "45",   0,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량4",     "64",   1,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가4",     "44",   1,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량3",     "63",   2,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가3",     "43",   2,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량2",     "62",   3,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가2",     "42",   3,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량1",     "61",   4,  0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매도호가1",     "41",   4,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
	        /////////////////////////////////////
	        // 매도호가
	        new stGRID("매수호가1",     "51",   5,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매도수량1",     "71",   5,  2,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수호가2",     "52",   6,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량2",     "72",   6,  2,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수호가3",     "53",   7,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량3",     "73",   7,  2,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수호가4",     "54",   8,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량4",     "74",   8,  2,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수호가5",     "55",   9,  1,  Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량5",     "75",   9,  2,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),

            new stGRID("매도호가총잔량","121", 10, 0,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수호가총잔량","125", 10, 2,  Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("호가시간",      "21",   10, 1,  Constants.DT_TIME,          false,  Constants.DT_RIGHT,     "", "")
        };

        const int lstOPW30003_OUTPUT_SIZE = 6;
        public stGRID[] lstOPW30003_OUTPUT = new stGRID[lstOPW30003_OUTPUT_SIZE]
        {
            new stGRID("매도수량",      "-1",       -1,     1,      Constants.DT_NUMBER_NOCOMMA,false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("매수수량",      "-1",       -1,     2,      Constants.DT_NUMBER_NOCOMMA,false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("총평가금액",     "-1",       -1,     3,      Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("실현수익금액",    "-1",       -1,     4,      Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("총약정금액",     "-1",       -1,     5,      Constants.DT_ZERO_NUMBER,   false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("총수익율",      "-1",       -1,     6,      Constants.DT_NONE,          false,  Constants.DT_RIGHT,     "", "%")

        };

        const int lstOPW30003_OCCURS_SIZE = 12;
        public stGRID[] lstOPW30003_OCCURS = new stGRID[lstOPW30003_OCCURS_SIZE]
        {
            new stGRID("종목코드",      "9001",     -1,     0,      Constants.DT_NONE,          false,  Constants.DT_CENTER,    "", ""),
            new stGRID("매도수구분",     "-1",       -1,     1,      Constants.DT_ORDGUBUN,      false,  Constants.DT_CENTER,    "", ""),
            new stGRID("수량",            "-1",       -1,     2,      Constants.DT_NUMBER_NOCOMMA,false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("청산가능",      "-1",       -1,     3,      Constants.DT_NUMBER_NOCOMMA,false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("평균단가",      "-1",       -1,     4,      Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("현재가격",      "10",       -1,     5,      Constants.DT_PRICE,         true,   Constants.DT_RIGHT,     "", ""),
            new stGRID("평가손익",      "-1",       -1,     6,      Constants.DT_DOUBLE,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("약정금액",      "-1",       -1,     7,      Constants.DT_DOUBLE,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("평가금액",      "-1",       -1,     8,      Constants.DT_DOUBLE,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("수익율",           "-1",       -1,     9,      Constants.DT_NONE,          false,  Constants.DT_RIGHT,     "", "%"),
            new stGRID("수수료",           "-1",       -1,     10,     Constants.DT_NUMBER,        false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("통화코드",      "-1",       -1,     11,     Constants.DT_NONE,          false,  Constants.DT_CENTER,    "", "")

        };

        const int lstFID_SIZE = 14;
        public stGRID[] lstFID = new stGRID[lstFID_SIZE]
        {
            new stGRID("주문번호",       "9203",        -1, 0,      Constants.DT_NUMBER_NOCOMMA,    false,  Constants.DT_CENTER,    "", ""),
            new stGRID("종목코드",      "9001",     -1, 1,      Constants.DT_NONE,              false,  Constants.DT_CENTER,    "", ""),
            new stGRID("구분",            "905",      -1, 2,      Constants.DT_NONE,              false,  Constants.DT_CENTER,    "", ""),
            new stGRID("주문표시가격",    "901",      -1, 3,      Constants.DT_NONE,              false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("주문수량",      "900",      -1, 4,      Constants.DT_NUMBER_NOCOMMA,    false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("미체결수량",     "902",      -1, 5,      Constants.DT_NUMBER_NOCOMMA,    false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("접수구분",       "913",     -1, 6,      Constants.DT_ORDREG_GB,         false,  Constants.DT_CENTER,    "", ""),
            new stGRID("FCM코드",     "947",      -1, 7,      Constants.DT_NONE,              false,  Constants.DT_CENTER,    "", ""),
            new stGRID("원주문번호",     "904",      -1, 8,      Constants.DT_NUMBER_NOCOMMA,    false,  Constants.DT_CENTER,    "", ""),
            new stGRID("통화코드",      "8043",     -1, 9,      Constants.DT_NONE,              false,  Constants.DT_CENTER,    "", ""),
            new stGRID("주문유형",      "906",      -1, 10,     Constants.DT_ORDTYPE,           false,  Constants.DT_CENTER,    "", ""),
            new stGRID("체결표시가격",    "910",      -1, 11,     Constants.DT_NONE,              false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("체결수량",      "911",      -1, 12,     Constants.DT_NUMBER_NOCOMMA,    false,  Constants.DT_RIGHT,     "", ""),
            new stGRID("체결시간",      "908",      -1, 13,     Constants.DT_TIME,              false,  Constants.DT_CENTER,    "", "")
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct FSCODEINFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string stk_code;          // 종목코드(자체코드)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string arti_code;          // 품목코드(자체코드)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string arti_hnm;          // 품목명(HTS)  
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string arti_tp;            // 품목구분   
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string crnc_code;          // 결제통화코드
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string tick_unit;         // TICK 단위
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string tick_value;        // TICK 가치 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string deal_unit;         // 거래단위 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string deal_mtal;         // 거래승수
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string ntt_code;           // 진법코드
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string ntt_calc_unit;     // 가격표시조정계수
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string frgn_exch_code;    // 해외거래소코드 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string expr_dt;            // 만기일자 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string fprc;              // 소숫점자리수 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string gubun;              // 최근월물구분
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string atv_code;           // 액티브월물구분    
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct FPCODEINFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string stk_code;          // 종목코드(자체코드)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string arti_code;          // 품목코드(자체코드)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string arti_hnm;          // 품목명(HTS)  
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string arti_tp;            // 품목구분   
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string crnc_code;          // 결제통화코드
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string tick_unit;         // TICK 단위
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string tick_value;        // TICK 가치 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string deal_unit;         // 거래단위 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string deal_mtal;         // 거래승수
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string ntt_code;           // 진법코드
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string ntt_calc_unit;     // 가격표시조정계수
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string frgn_exch_code;    // 해외거래소코드 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public string expr_dt;            // 만기일자 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string underlying_code; // 기초 자산 코드    
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string atmg;               // ATM 구분
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
            public string hsga;              // 행사가  
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string cpgubn;             // 콜풋 구분
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string fprc;              // frpc 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string tick_calc_unnm;    // tick check price
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string over_tick_unmn;    // over tick price
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string vtt_code;           // vtt code  
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
            public string yymm;               // yymmd 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
            public string op_type;            // 옵션 type('A','E')	
        }

        private void InitFIDName()
        {
            m_mapFIDName.Add("9201", "계좌번호");
            m_mapFIDName.Add("9203", "주문번호");
            m_mapFIDName.Add("9001", "종목코드");
            m_mapFIDName.Add("907", "매도수구분");
            m_mapFIDName.Add("905", "주문/체결구분");
            m_mapFIDName.Add("904", "원주문번호");
            m_mapFIDName.Add("302", "종목명");
            m_mapFIDName.Add("906", "주문유형");
            m_mapFIDName.Add("900", "주문수량");
            m_mapFIDName.Add("901", "주문가격");
            m_mapFIDName.Add("13333", "조건가격");
            m_mapFIDName.Add("13330", "주문표시가격");
            m_mapFIDName.Add("13332", "조건표시가격");
            m_mapFIDName.Add("902", "미체결수량");
            m_mapFIDName.Add("913", "주문상태");
            m_mapFIDName.Add("919", "반대매매여부");
            m_mapFIDName.Add("8046", "거래소코드");
            m_mapFIDName.Add("947", "FCM코드");
            m_mapFIDName.Add("8043", "통화코드");
            m_mapFIDName.Add("908", "주문/체결시간");
            m_mapFIDName.Add("909", "체결번호");
            m_mapFIDName.Add("911", "체결수량");
            m_mapFIDName.Add("910", "체결가격");
            m_mapFIDName.Add("13331", "체결표시가격");
            m_mapFIDName.Add("13329", "체결금액");
            m_mapFIDName.Add("13326", "거부수량");
            m_mapFIDName.Add("935", "체결수수료");
            m_mapFIDName.Add("13327", "신규수량");
            m_mapFIDName.Add("13328", "청산수량");
            m_mapFIDName.Add("8018", "실현손익");
            m_mapFIDName.Add("8009", "약정금액");
            m_mapFIDName.Add("930", "미결제약정합계");
            m_mapFIDName.Add("13334", "미결제약정단가표시(평균)");
        }

        string m_strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;  // 파일이 존재하는 폴더 위치를 저장

        //definition variable
        private string[] tradeMode = new string[] { "수 동", "자 동"}; // 자동주문 변수
        private string[] tradePosition = new string[] { "", "감시중", "매수", "매도" }; // 포지션
        private string[] tradeState = new string[] { "진입탐색", "진입대기", "진입중", "진입완료", "청산대기", "청산중" }; // 상태
        public string tradeStoploss;  // 손절 틱수
        public string tradeVolume; // 거래량 초기설정

        //control variable
        private string autoPosition = "", orderPosition = "", havePosition = "";
        private bool[] autoIntoJudge = new bool[6] { true, false, false, false, false, false }; //진입신호 제어 변수
        private bool orderFlag = true;  //매매 제어 변수
        private bool InitFlag = false;  //종목정보 초기 제어 변수(gain 예외 발생시 사용)
        private bool InitChFlag = true;  //체결수량 초기 제어 변수
        private bool InitPrice = true;
        private bool _bRealTrade = false;
        private int stopCancle = 0;
        

        //information variable
        public string m_strScrNo;
        public string m_strAccNo = "";
        public string m_strJongCode = "";
        private string highPrice="0", lowPrice="9999", high_LowPrice, low_HighPrice; //호가창 변수
        private string[] autoState;
        private string[,] chegyulData;
        private int sendMode = 0;
        private int hogaVolume;
        private string recentBuyOriNo, recentSellOriNo;
        private int buyHogaAver = 0, sellHogaAver = 0, buyHogaMax, sellHogaMax;
        private double[,] autoData = new double[2, 5];
        private string buyHoga, sellHoga;
        private string strLogData = "";
        private string curTrMode, curCode, curPrice, curPosition, curState, curOrVolume, curChVolume, curInVolume, curOrPrice, curChPrice, curGain, curGoPrice, curOrType, curInStop, curSend;

        // 제어 상수
        private double conTotal = 1.5, conHoga1 = 0.35, conHoga2 = 0.6, conVolume = 0.8;

        private LocalData LD = new LocalData();
        
        private Dictionary<string, string> m_mapJongCode = new Dictionary<string, string>();

        public CurrentAuto()
        {
            InitializeComponent();
        }

        // 그리드 초기화 메소드
        private void CurrentAuto_Load(object sender, EventArgs e)
        {
            InitAccNo();
            InitCode();
            InitHogaGrid();
            InitHogaHighGrid();
            InitHogaLowGrid();
            InitCurPrcGrid();
            InitJongInfoGrid();
            InitTickGrid();
        }

        private void InitAccNo()//계좌번호 초기화 메소드
        {
            txtAccNo.Text = m_strAccNo;
        }
        private void InitCode()//종목코드 초기화 메소드
        {
            txtCode.Text = m_strJongCode;
        }
        private void InitHogaGrid()//호가창 그리드 초기화 메소드
        {
            grdHoga.RowCount = 11;
            grdHoga.ColumnCount = 3;

            Color[] clrHoga = new Color[10]
            {Color.FromArgb(205,230,235), Color.FromArgb(209,234,238), Color.FromArgb(216,235,241), Color.FromArgb(222,237,242), Color.FromArgb(226,241,244),
             Color.FromArgb(253,226,219), Color.FromArgb(251,221,211), Color.FromArgb(250,216,204), Color.FromArgb(250,210,198), Color.FromArgb(252,204,192)};

            for (int i = 0; i < grdHoga.RowCount - 1; i++)
            {
                grdHoga.Rows[i].Cells[1].Style.BackColor = clrHoga[i];
            }


            for (int i = 0; i < grdHoga.ColumnCount; i++)
            {
                grdHoga.Columns[i].Width = 62;
                if (i == 1)
                {
                    grdHoga.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    grdHoga.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

            }
            grdHoga.ClearSelection();
        }
        private void InitHogaHighGrid()//호가창 High 그리드 초기화 메소드
        {
            grdHogaHigh.RowCount = 11;
            grdHogaHigh.ColumnCount = 3;

            Color[] clrHoga = new Color[10]
            {Color.FromArgb(205,230,235), Color.FromArgb(209,234,238), Color.FromArgb(216,235,241), Color.FromArgb(222,237,242), Color.FromArgb(226,241,244),
             Color.FromArgb(253,226,219), Color.FromArgb(251,221,211), Color.FromArgb(250,216,204), Color.FromArgb(250,210,198), Color.FromArgb(252,204,192)};

            grdHogaHigh.Rows[0].Cells[2].Value = "고가";
            for (int i = 0; i < grdHogaHigh.RowCount - 1; i++)
            {
                grdHogaHigh.Rows[i].Cells[1].Style.BackColor = clrHoga[i];
            }
            for (int i = 0; i < grdHogaHigh.ColumnCount; i++)
            {
                grdHogaHigh.Columns[i].Width = 62;
                if (i == 1)
                {
                    grdHogaHigh.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    grdHogaHigh.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            grdHogaHigh.ClearSelection();
        }
        private void InitHogaLowGrid()//호가창 Low 그리드 초기화 메소드
        {
            grdHogaLow.RowCount = 11;
            grdHogaLow.ColumnCount = 3;

            Color[] clrHoga = new Color[10]
            {Color.FromArgb(205,230,235), Color.FromArgb(209,234,238), Color.FromArgb(216,235,241), Color.FromArgb(222,237,242), Color.FromArgb(226,241,244),
             Color.FromArgb(253,226,219), Color.FromArgb(251,221,211), Color.FromArgb(250,216,204), Color.FromArgb(250,210,198), Color.FromArgb(252,204,192)};

            grdHogaLow.Rows[0].Cells[2].Value = "저가";
            for (int i = 0; i < grdHogaLow.RowCount - 1; i++)
            {
                grdHogaLow.Rows[i].Cells[1].Style.BackColor = clrHoga[i];
            }
            for (int i = 0; i < grdHogaLow.ColumnCount; i++)
            {
                grdHogaLow.Columns[i].Width = 62;
                if (i == 1)
                {
                    grdHogaLow.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    grdHogaLow.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

            }
            grdHogaLow.ClearSelection();
        }
        private void InitCurPrcGrid()// 현재 가격 그리드 설정 메소드
        {
            grdCurPrc.RowCount = 1;
            grdCurPrc.ColumnCount = 6;
            for (int i = 0; i < grdCurPrc.ColumnCount; i++)
            {
                if (i == 1) //전일비기호
                {
                    grdCurPrc.Columns[i].Width = 44;
                    grdCurPrc.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    grdCurPrc.Columns[i].Width = 175;
                    grdCurPrc.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            grdCurPrc.ClearSelection();
        }
        private void InitJongInfoGrid()// 종목정보 그리드 설정 메소드
        {
            grdJongInfo.RowCount = 10;
            grdJongInfo.ColumnCount = 4;

            for (int i = 0; i < grdJongInfo.ColumnCount; i++)
            {
                if (i == 0 || i == 2)
                    grdJongInfo.Columns[i].Width = 90;
                else
                    grdJongInfo.Columns[i].Width = 80;
                grdJongInfo.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            int nStartRow = 7; //종목정보 시작점
            int nRow, nCol;
            for (int i = 0; i < grdJongInfo.RowCount * 2; i++)
            {
                if (i < 10)
                {
                    nRow = i;
                    nCol = 0;
                }
                else
                {
                    nRow = i - 10;
                    nCol = 2;
                }
                grdJongInfo.Columns[nCol].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdJongInfo.Rows[nRow].Cells[nCol].Value = lstOPT10001[i + nStartRow].strKey;
                grdJongInfo.Rows[nRow].Cells[nCol].Style.BackColor = Color.FromArgb(220, 224, 233);
            }
            grdJongInfo.ClearSelection();
        }
        private void InitTickGrid()// 체결 내용 그리드 초기화 메소드
        {
            grdTick.RowCount = 1;
            grdTick.ColumnCount = 6;

            string[] arrHeader = new string[6] { "시간", "체결가", "", "전일대비", "등락율", "거래량" };

            for (int i = 0; i < grdTick.ColumnCount; i++)
            {
                if (i == 2) //전일비기호
                    grdTick.Columns[i].Width = 42;
                else
                    grdTick.Columns[i].Width = 172;
                grdTick.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grdTick.Columns[i].HeaderText = arrHeader[i];
                grdTick.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            }
            grdTick.ClearSelection();
        }
        private void InitAutoTradeGrid()// 자동매매 그리드 초기화 메소드
        {
            autoState = new string[] { tradeMode[0], txtCode.Text, "0", tradePosition[0], "", "0", "0", tradeVolume, "", "", "", "", tradeStoploss, "", "주문" };
            
            for (int i = 0; i < grdAutoTrade.ColumnCount; i++)
            {
                grdAutoTrade.Rows[0].Cells[i].Value = autoState[i];
                grdAutoTrade.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            SetAutoTrade(99);
        }

        // 화면 종료시 메소드
        public void CurrentVolume_FormClosing(object sender, FormClosedEventArgs e)
        {
            ((Kiwoom_AutoTrade_FF)this.Owner).SetAutoButton(m_strJongCode);
            lstOPW30003_OUTPUT = null;
            lstOPW30003_OCCURS = null;

            CommonLib.PostMessage(((Kiwoom_AutoTrade_FF)this.Owner).Handle, Constants.UM_SCREEN_CLOSE, 0, int.Parse(m_strScrNo));
        }

        // 서버로 정보 요청 함수
        private void SendSearch() // 종목의 실시간 시세를 불러온다.
        {
            if (m_strJongCode.ToString().Trim() == "")
                return;

            string strRQName = "선물옵션현재가";
            string strTRCode = Constants.TR_OPT10001;
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("종목코드", m_strJongCode);
            int iRet = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
            string strErr;
            if (((Kiwoom_AutoTrade_FF)this.Owner).IsError(iRet))
            {
                strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", strTRCode, iRet);
            }
        }
        private void SendRate() // 계좌의 잔고 내용을 불러온다. 
        {
            string strRQName = "잔고내역";
            string strTRCode = Constants.TR_OPW30003;
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("계좌번호", m_strAccNo);
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("비밀번호", "");
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("비밀번호입력매체", "00");
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("통화코드", "USD");

            int iRet = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
            //string strErr;
            if (((Kiwoom_AutoTrade_FF)this.Owner).IsError(iRet))
            {
                // grdAutoTrade.Rows[0].Cells[6].Value = "0";
                // grdAutoTrade.Rows[0].Cells[9].Value = "";
            }
        }
        private void SendChegyul() // 계좌의 주문 내용을 불러온다. 
        {
            string strRQName = "주문체결내역";
            string strTRCode = Constants.TR_OPW30005;
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("계좌번호", m_strAccNo);
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("비밀번호", "");
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("비밀번호입력매체", "00");
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("조회구분", "1");
            ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SetInputValue("종목코드", m_strJongCode);

            int iRet = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.CommRqData(strRQName, strTRCode, "", m_strScrNo);
            //string strErr;
            if (((Kiwoom_AutoTrade_FF)this.Owner).IsError(iRet))
            {
                //strErr = string.Format("조회요청 에러 [{0:s}][{0:d}]", strTRCode, iRet);
            }
        }

        // 버튼시 실행
        private void btnSearch_Click(object sender, EventArgs e)
        {
            InitAutoTradeGrid();
            LoadHoga();
            LoadGoal();
            SendChegyul();
        }
        private void grdAutoTrade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)  // 자동 수동 설정
            {
                if (_bRealTrade == false)
                {
                    if(InitFlag == false)
                    {
                        return;
                    }
                    SetAutoTrade(99);
                    _bRealTrade = true;
                    sellHogaAver = System.Convert.ToInt32(grdHoga.Rows[10].Cells[0].Value.ToString());
                    buyHogaAver = System.Convert.ToInt32(grdHoga.Rows[10].Cells[2].Value.ToString());
                    SetAutoTrade(0);
                    laView.Text = "자동모드 시작";
                    if (curPosition == tradePosition[0])
                    {
                        autoPosition = tradePosition[1];
                        SetAutoTrade(3);
                    }

                    if (curOrVolume != "0" && curChVolume == "0")
                    {
                        if(curPosition == "매수")
                        {
                            TradeEx("매수취소");
                        }
                        else if(curPosition == "매도")
                        {
                            TradeEx("매도취소");
                        }
                    }
                    else if (curOrVolume == "0" && curChVolume != "0" && curGoPrice == "")
                    {
                        if (curPosition == "매수")
                        {
                            TradeEx("매수청산(시장가)");
                        }
                        else if (curPosition == "매도")
                        {
                            TradeEx("매도청산(시장가)");
                        }
                    }
                }
                else
                {
                    _bRealTrade = false;
                    SetAutoTrade(0);
                    laView.Text = "자동모드 종료";
                }
            }
            if (e.ColumnIndex == 3)
            {
                string strTemp = grdAutoTrade.Rows[0].Cells[3].Value.ToString();
                if (_bRealTrade == false)
                {
                    if (strTemp == tradePosition[0] || strTemp == tradePosition[1] || strTemp == tradePosition[3])
                    {
                        curPosition = tradePosition[2];
                        SetAutoTrade(3);
                    }
                    else
                    {
                        curPosition = tradePosition[3];
                        SetAutoTrade(3);
                    }
                }
                else
                    return;

            }
            if (e.ColumnIndex == 14)
            {

                if (_bRealTrade == false)
                {
                    TradeEx("");
                }
                else
                    SetAutoTrade(99);
            }
        }

        // 데이터 리시브 메소드
        public void axKFOpenAPI1_OnReceiveTrData(string sScrNo, string sRQName, string sTrCode, string sRecordName, string sPreNext)
        {
            string strRQName = sRQName;
            if (strRQName == "선물옵션현재가")
            {
                string strData;
                string[] arrData;
                int i, j, nCnt;

                //기본정보
                arrData = new string[STGRID10001_SIZE];
                for (j = 0; j < STGRID10001_SIZE; j++)
                {
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, lstOPT10001[j].strKey);
                    strData.Trim();
                    arrData[j] = strData;
                }
                SetDataJongInfoGrid(arrData, "");

                //호가
                Array.Clear(arrData, 0, STGRID10001_SIZE);
                arrData = new string[STGRID10003_SIZE];
                for (j = 0; j < STGRID10003_SIZE; j++)
                {
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, lstOPT10003[j].strKey);
                    strData.Trim();
                    arrData[j] = strData;

                    if (InitPrice)
                    {
                        if (j == 9)
                        {
                            double temp = Convert.ToDouble(strData);
                            if(highPrice == null)
                            {
                                highPrice = ValueAbs(temp).ToString();
                            }
                        }
                        else if (j == 10)
                        {
                            double temp = Convert.ToDouble(strData);
                            if(lowPrice == null)
                            {
                                lowPrice = ValueAbs(temp).ToString();
                            }
                            
                        }
                    }
                }
                InitPrice = false;
                SetDataHogaGrid(arrData, "");

                //체결데이타
                Array.Clear(arrData, 0, STGRID10003_SIZE);
                nCnt = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetRepeatCnt(sTrCode, strRQName);
                for (i = 0; i < nCnt; i++)
                {
                    Array.Clear(arrData, 0, STGRID10002_SIZE);
                    arrData = new string[STGRID10002_SIZE];
                    for (j = 0; j < STGRID10002_SIZE; j++)
                    {
                        strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, i, lstOPT10002[j].strKey);
                        strData.Trim();
                        arrData[j] = strData;
                    }
                    SetDataTickGrid(-1, i, arrData);
                }
            }
            if (strRQName == "주문")
            {
                // string strData;

                //주문번호
                // strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, 0, "주문번호");
            }
            if (strRQName == "주문체결내역")
            {
                string strData;
                string[] arrData;
                int i, j, nCnt;

                arrData = new string[lstFID_SIZE];
                nCnt = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetRepeatCnt(sTrCode, strRQName);
                chegyulData = new string[nCnt, lstFID_SIZE];
                for (i = 0; i < nCnt; i++)
                {
                    Array.Clear(arrData, 0, lstFID_SIZE);
                    arrData = new string[lstFID_SIZE];

                    for (j = 0; j < lstFID_SIZE; j++)
                    {
                        strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, i, lstFID[j].strKey);
                        strData = strData.Trim();
                        arrData[j] = strData;
                    }
                    SetChegyulData(-1, i, arrData);
                }
                SetChegyul(chegyulData);// 주문 표시

                if (sendMode == 0)
                    SendRate();
                else
                {
                    SetAutoTrade(0);
                    orderFlag = true;
                    SendSearch();
                }
                    
            }
            if (strRQName == "잔고내역")
            {
                string strData;
                int i, j, nCnt;
                string strCode = "";
                nCnt = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetRepeatCnt(sTrCode, strRQName);
                string[] temp = new string[lstOPW30003_OCCURS_SIZE];
                temp[0] = "";
                for (i = 0; i < nCnt; i++)
                {
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, i, lstOPW30003_OCCURS[0].strKey);
                    strData = strData.Trim();
                    strCode = strData;
                    for (j = 0; j < lstOPW30003_OCCURS_SIZE; j++)
                    {
                        strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommData(sTrCode, strRQName, i, lstOPW30003_OCCURS[j].strKey);
                        strData = strData.Trim();

                        if (j == 0) //
                        {
                            strCode = strData;
                        }
                        if (txtCode.Text == strCode)
                        {
                            if (strData != "")
                            {
                                //grdRate.Rows[i + m_nNextRow].Cells[j].Style.Alignment = (DataGridViewContentAlignment)lstOPW30003_OCCURS[j].nAlign;
                                //grdRate.Rows[i + m_nNextRow].Cells[j].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPW30003_OCCURS[j].nDataType, strData.Trim(), lstOPW30003_OCCURS[j].strBeforeData, lstOPW30003_OCCURS[j].strAfterData); ;
                                temp[j] = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPW30003_OCCURS[j].nDataType, strData.Trim(), lstOPW30003_OCCURS[j].strBeforeData, lstOPW30003_OCCURS[j].strAfterData);
                            }
                        }
                    }
                }
                if (temp[0] != txtCode.Text)  // 청산되거나 체결 수량이 없을 경우
                {
                    if (InitChFlag)
                    {
                        curChVolume = "0";
                        curChPrice = "";
                        havePosition = "";
                        SetAutoTrade(6);
                        SetAutoTrade(9);
                        InitChFlag = false;
                    }
                    if (curChVolume != "0")
                    {
                        if(System.Convert.ToDouble(curGain) < 0)
                        {
                            laView.Text = "청산완료(손절)";
                        }
                        else
                        {
                            laView.Text = "청산완료(수익축하!!!)";
                        }
                        orderFlag = true;
                        curGoPrice = "";
                        LD.DeleteData(txtCode.Text, "grdGoal");
                        curChPrice = "";
                        havePosition = "";
                        orderPosition = "";
                        curChVolume = "0";
                        curOrVolume = "0";
                        curOrPrice = "";
                        curGain = "";
                        SetAutoTrade(0);
                    }
                    else
                    {
                        curChVolume = "0";
                        curChPrice = "";
                        havePosition = "";
                        SetAutoTrade(6);
                        SetAutoTrade(9);
                    }
                }
                else // 체결 수량이 있을 경우 temp[]에 기록 완료
                {
                    strData = temp[1];
                    if (strData == tradePosition[2])
                        havePosition = tradePosition[2];
                    else if (strData == tradePosition[3])
                        havePosition = tradePosition[3];
                    curPosition = havePosition;
                    SetAutoTrade(3);

                    curChPrice = temp[4];
                    SetAutoTrade(9);

                    if (havePosition != "" && curChPrice != "" && curPrice != "")
                    {
                        SetGain();
                    }

                    if (InitChFlag)
                    {
                        curChVolume = temp[2];
                        SetAutoTrade(6);
                        InitChFlag = false;
                    }
                    else
                    {
                        if (curChVolume != curInVolume && temp[2] == curInVolume)
                        {
                            orderFlag = true;
                            curChVolume = temp[2];
                            curOrVolume = "0";
                            curOrPrice = "";
                            SetAutoTrade(0);
                        }
                        else
                        {
                            orderFlag = true;
                            curOrVolume = temp[2];
                            SetAutoTrade(6);
                            sendMode = 1;
                        }
                    }
                }
                if (sendMode == 0)
                {
                    orderFlag = true;
                    SendSearch();
                }
                else
                    SendChegyul();   
            }
        }
        public void axKFOpenAPI1_OnReceiveRealData(string sJongmokCode, string sRealType, string sRealData)
        {
            if (m_strJongCode.CompareTo(sJongmokCode) != 0)
                return;

            AutoTrade();
            
            string strData;
            string[] arrData;

            if ((sRealType.Trim() == "해외선물시세") || (sRealType.Trim() == "해외옵션시세"))
            {
                //기본정보
                arrData = new string[STGRID10001_SIZE];
                for (int i = 0; i < STGRID10001_SIZE; i++)
                {
                    if (int.Parse(lstOPT10001[i].strRealKey) < 0)
                    {
                        arrData[i] = "";
                        continue;
                    }
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommRealData(sJongmokCode, int.Parse(lstOPT10001[i].strRealKey));
                    strData.Trim();
                    arrData[i] = strData;
                }
                SetDataJongInfoGrid(arrData, sRealType);

                //체결
                Array.Clear(arrData, 0, STGRID10001_SIZE);
                for (int i = 0; i < STGRID10002_SIZE; i++)
                {
                    if (int.Parse(lstOPT10002[i].strRealKey) < 0)
                    {
                        arrData[i] = "";
                        continue;
                    }
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommRealData(sJongmokCode, int.Parse(lstOPT10002[i].strRealKey));
                    strData.Trim();
                    arrData[i] = strData;
                }
                SetDataTickGrid(1, 0, arrData);
            }
            else if ((sRealType.Trim() == "해외선물호가") || (sRealType.Trim() == "해외옵션호가"))
            {
                arrData = new string[STGRID10003_SIZE];
                for (int i = 0; i < STGRID10003_SIZE; i++)
                {
                    if (int.Parse(lstOPT10003[i].strRealKey) < 0)
                    {
                        arrData[i] = "";
                        continue;
                    }
                    strData = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetCommRealData(sJongmokCode, int.Parse(lstOPT10003[i].strRealKey));
                    strData.Trim();
                    arrData[i] = strData;
                }
                SetDataHogaGrid(arrData, sRealType);
            }

        }
        public void axKFOpenAPI1_OnReceiveMsg(string sScrNo, string sRQName, string sTrCode, string sMsg)
        {
            // laView.Text = sMsg;
        }
        public void axKFOpenAPI1_OnReceiveChejanData(string sGubun, int nItemCnt, string sFidList)
        {
            // 주문 체결
            string strGubun = sGubun, strFidlist = sFidList;

            string[] strArrData = null;
            char[] cSplit = { ';' };
            strArrData = strFidlist.Split(cSplit);
            string stat1, stat2, stat3;
            stat1 = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetChejanData(int.Parse("905")).Trim();
            stat2 = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetChejanData(int.Parse("906")).Trim();
            stat3 = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.GetChejanData(int.Parse("9001")).Trim();
            if(stat3 == txtCode.Text)
            {
                if (stat1 == "1" && stat2 == "1")
                {
                    orderFlag = true;
                    return;
                }
                else if (stat1 == "1" || stat1 == "22" || stat1 == "23")
                {
                    sendMode = 1;
                    SendChegyul();
                }
                else if (stat1 == "21")
                {
                    sendMode = 0;
                    SendRate();
                }
                else
                    orderFlag = true;
            }
        }

        //종목정보 세팅 메소드
        private void SetDataJongInfoGrid(string[] arrData, string strRealType)
        {
            string strData, strTempData, strTemp;
            int i, nCnt = arrData.Length;

            InitFlag = true;

            for (i = 0; i < nCnt; i++)
            {
                strData = arrData[i];
                if (i == 0 && strRealType == "")
                    laName.Text = strData;
                else if (i < 7) //현재가그리드
                {
                    if (strRealType != "" && (i == 1 || i == 3 || i == 4))
                    {
                        strTempData = strData;
                        strTempData = strTempData.Replace("+", "");
                        strTempData = strTempData.Replace("-", "");
                        strTempData = strTempData.Replace(".", "");

                        strTemp = grdCurPrc.Rows[lstOPT10001[i].nRow].Cells[lstOPT10001[i].nCol].FormattedValue.ToString();
                        strTemp = strTemp.Replace("%", ""); strTemp = strTemp.Replace("+", ""); strTemp = strTemp.Replace("-", "");
                        strTemp = strTemp.Replace(",", ""); strTemp = strTemp.Replace(".", "");

                    }

                    if (lstOPT10001[i].bTextColor)
                    {
                        strTemp = arrData[2];
                        if (strTemp == "1" || strTemp == "2")  //부호에 따라 색상변경
                            strTemp = "1";
                        else if (strTemp == "4" || strTemp == "5") // 부호에 따라 색상변경
                            strTemp = "-1";
                        else
                            strTemp = "0";
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdCurPrc, lstOPT10001[i].nRow, lstOPT10001[i].nCol, strTemp);
                    }
                    if (lstOPT10001[i].nDataType == Constants.DT_SIGN)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetSignData(grdCurPrc, lstOPT10001[i].nRow, lstOPT10001[i].nCol, strData);
                    }
                    else
                    {
                        if (strData == "")
                            continue;
                        grdCurPrc.Rows[lstOPT10001[i].nRow].Cells[lstOPT10001[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10001[i].nDataType, strData.Trim(), lstOPT10001[i].strBeforeData, lstOPT10001[i].strAfterData);
                    }
                }
                else //선물기본정보
                {
                    if (strRealType != "")
                        break;

                    if (lstOPT10001[i].bTextColor)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdJongInfo, lstOPT10001[i].nRow, lstOPT10001[i].nCol, strData);
                    }
                    if (lstOPT10001[i].nDataType == Constants.DT_SIGN)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdJongInfo, lstOPT10001[i].nRow, lstOPT10001[i].nCol, strData);
                    }
                    else
                    {
                        grdJongInfo.Rows[lstOPT10001[i].nRow].Cells[lstOPT10001[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10001[i].nDataType, strData.Trim(), lstOPT10001[i].strBeforeData, lstOPT10001[i].strAfterData);
                    }
                }
            }
            SetGain();
        }
        private void SetDataHogaGrid(string[] arrData, string strRealType)
        {
            string strData;
            int i, nCnt = arrData.Length;
            int nStart = 0, nEnd = nCnt;
            string[] strTemp = new string[nCnt];
            LD.SetHogaData(arrData);

            for (i = nStart; i < nEnd; i++)
            {
                strData = arrData[i];

                if (lstOPT10003[i].bTextColor)
                {
                    ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHoga, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                }
                if (lstOPT10003[i].nDataType == Constants.DT_SIGN)
                {
                    ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHoga, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                }
                else
                {
                    grdHoga.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, strData.Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                }
            }
            AutoTrade();

            
            // grdHogaHigh 고가 갱신

            double temp = ValueAbs(Convert.ToDouble(arrData[9]));
            if (temp >= ValueAbs(double.Parse(highPrice)))
            {
                if (grdJongInfo.Rows[3].Cells[1].Value.ToString() == "")
                    grdJongInfo.Rows[3].Cells[1].Value = highPrice;

                if (temp >= System.Convert.ToDouble(grdJongInfo.Rows[3].Cells[1].Value.ToString()) && InitFlag)
                {
                    autoPosition = tradePosition[2];
                    SetPosition("");
                    grdJongInfo.Rows[3].Cells[1].Value = temp;
                }
                highPrice = ValueAbs(temp).ToString();
                grdHogaHigh.Rows[1].Cells[2].Value = highPrice; // 고가 표시
                nStart = 0;
                nEnd = nCnt;
                for (i = nStart; i < nEnd; i++)
                {
                    strData = arrData[i];
                    if (i == 10)
                        high_LowPrice = strData; // 고가 갱신시 고가 호가창 저가 갱신
                    if (lstOPT10003[i].bTextColor)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaHigh, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    if (lstOPT10003[i].nDataType == Constants.DT_SIGN)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaHigh, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    else
                    {
                        grdHogaHigh.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, strData.Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                        if (i == 20)
                        {
                            sellHogaMax = System.Convert.ToInt32(grdHoga.Rows[10].Cells[0].Value.ToString());
                        }
                    }
                }
                LD.SetHogaHighData(LD.GetHogaData(""));
                LD.SaveData(m_strJongCode, "grdHogaHigh", LD.GetHogaData("고가갱신"));
                
                SearchAutoGoal(curPosition, curGoPrice, curOrPrice);
            }

            // grdHogaHigh 눌림값

            temp = ValueAbs(Convert.ToDouble(arrData[10]));
            if (temp <= ValueAbs(double.Parse(high_LowPrice)))
            {
                strTemp = LD.GetHogaData("눌림");
                nCnt = strTemp.Length;
                double[] dData = new double[nCnt];
                for(i = 0; i < nCnt; i++)
                {
                    dData[i] = System.Convert.ToDouble(strTemp[i]);
                    if (i == 10 || i == 12 || i == 14 || i == 16 || i == 18)
                        strTemp[i] = TransUnit(m_strJongCode, dData[i]);
                }

                high_LowPrice = ValueAbs(temp).ToString();
                nStart = 10;
                nEnd = 20;
                for (i = nStart; i < nEnd; i++)
                {
                    strData = arrData[i];
                    strTemp[i] = arrData[i];

                    if (lstOPT10003[i].bTextColor)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaHigh, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    if (lstOPT10003[i].nDataType == Constants.DT_SIGN)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaHigh, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    else
                    {
                        grdHogaHigh.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, strData.Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                    }
                }
                i = 21;  // 저점 매수 잔량
                grdHogaHigh.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, arrData[i].Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                LD.SetHogaHighData(strTemp);
                LD.SaveData(m_strJongCode, "grdHogaHigh", LD.GetHogaData("눌림"));
            }
            i = 22;
            grdHogaHigh.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, arrData[i].Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();

            // grdHogaLow 저가 갱신

            temp = ValueAbs(Convert.ToDouble(arrData[10]));
            if (temp <= ValueAbs(double.Parse(lowPrice)))
            {
                if (grdJongInfo.Rows[2].Cells[1].Value.ToString() == "")
                    grdJongInfo.Rows[2].Cells[1].Value = lowPrice;
                
                if (temp <= System.Convert.ToDouble(grdJongInfo.Rows[2].Cells[1].Value.ToString()) && InitFlag)
                {
                    autoPosition = tradePosition[3];
                    SetPosition("");
                    grdJongInfo.Rows[2].Cells[1].Value = temp;
                }
                lowPrice = ValueAbs(temp).ToString();
                grdHogaLow.Rows[1].Cells[2].Value = lowPrice; // 저가 표시
                nStart = 0;
                nEnd = nCnt;
                for (i = nStart; i < nEnd; i++)
                {
                    strData = arrData[i];
                    if (i == 9)
                        low_HighPrice = strData; // 저가 갱신시 저가 호가창 고가 갱신
                    if (lstOPT10003[i].bTextColor)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaLow, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    if (lstOPT10003[i].nDataType == Constants.DT_SIGN)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaLow, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    else
                    {
                        grdHogaLow.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, strData.Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                    }
                }
                LD.SetHogaLowData(LD.GetHogaData(""));
                LD.SaveData(m_strJongCode, "grdHogaLow", LD.GetHogaData("저가갱신"));

                SearchAutoGoal(curPosition, curGoPrice, curOrPrice);
            }

            // grdHogaLow 반등값
            temp = ValueAbs(Convert.ToDouble(arrData[9]));
            if (temp >= ValueAbs(double.Parse(low_HighPrice)))
            {
                strTemp = LD.GetHogaData("반등");
                nCnt = strTemp.Length;
                double[] dData = new double[nCnt];
                for (i = 0; i < nCnt; i++)
                {
                    dData[i] = System.Convert.ToDouble(strTemp[i]);
                    if (i == 1 || i == 3 || i == 5 || i == 7 || i == 9)
                        strTemp[i] = TransUnit(m_strJongCode, dData[i]);
                }

                low_HighPrice = ValueAbs(temp).ToString();
                nStart = 0;
                nEnd = 10;
                for (i = nStart; i < nEnd; i++)
                {
                    strData = arrData[i];
                    strTemp[i] = arrData[i];

                    if (lstOPT10003[i].bTextColor)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaLow, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    if (lstOPT10003[i].nDataType == Constants.DT_SIGN)
                    {
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaLow, lstOPT10003[i].nRow, lstOPT10003[i].nCol, strData);
                    }
                    else
                    {
                        grdHogaLow.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, strData.Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                    }
                }
                i = 20; // 고점 매도 잔량
                grdHogaLow.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, arrData[i].Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                LD.SetHogaLowData(strTemp);
                LD.SaveData(m_strJongCode, "grdHogaLow", LD.GetHogaData("반등"));
            }
            i = 22;
            grdHogaLow.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, arrData[i].Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
        }
        private void SetDataTickGrid(int nInsertRow, int nRow, string[] arrData)
        {
            if (nInsertRow >= grdTick.RowCount)
                nInsertRow = -1;
            //행추가
            if (nInsertRow == -1)
                grdTick.Rows.Add("", "", "", "", "", "");
            else
                grdTick.Rows.Insert(0, "", "", "", "", "", "");

            string strData, strTemp;
            int i, nCnt = 6;

            for (i = 0; i < nCnt; i++)
            {
                strData = arrData[i];
                grdTick.Rows[nRow].Cells[i].Style.Alignment = (DataGridViewContentAlignment)lstOPT10002[i].nAlign;

                if (lstOPT10002[i].bTextColor)
                {
                    strTemp = arrData[2];
                    if (strTemp == "1" || strTemp == "2")   // 부호에 따라 색상변경
                    {
                        strTemp = "1";
                    }
                    else if (strTemp == "4" || strTemp == "5")  // 부호에 따라 색상변경
                    {
                        strTemp = "-1";
                    }
                    else
                    {
                        strTemp = "0";
                    }
                    ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdTick, nRow, lstOPT10002[i].nCol, strTemp);
                }
                if (lstOPT10002[i].nDataType == Constants.DT_SIGN)
                {
                    ((Kiwoom_AutoTrade_FF)this.Owner).SetSignData(grdTick, nRow, lstOPT10002[i].nCol, strData);
                }
                else
                {
                    grdTick.Rows[nRow].Cells[lstOPT10002[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10002[i].nDataType, strData.Trim(), lstOPT10002[i].strBeforeData, lstOPT10002[i].strAfterData);
                    if (i == 1)
                    {
                        curPrice = grdTick.Rows[nRow].Cells[lstOPT10002[i].nCol].Value.ToString();
                        SetAutoTrade(2);
                    }
                }
            }

        }
        private void SetPosition(string strPosition)
        {
            if (strPosition != "")
            {
                curPosition = strPosition;
                SetAutoTrade(3);
            }
            else
            {
                if (_bRealTrade == false)
                {
                    if (havePosition != "")
                    {
                        curPosition = havePosition;
                        SetAutoTrade(3);
                    }
                    else if (havePosition == "" && orderPosition != "")
                    {
                        curPosition = orderPosition;
                        SetAutoTrade(3);
                    }
                    else if(havePosition == "" && orderPosition == "")
                    {
                        curPosition = tradePosition[0];
                        SetAutoTrade(3);
                    }
                }
                else
                {
                    if (havePosition != "")
                    {
                        curPosition = havePosition;
                        SetAutoTrade(3);
                    }
                    else if(havePosition == "" && orderPosition != "")
                    {
                        curPosition = orderPosition;
                        SetAutoTrade(3);

                    }
                    else if (havePosition == "" && orderPosition == "")
                    {
                        curPosition = autoPosition;
                        SetAutoTrade(3);
                    }
                }
            }
        }
        private void SetTradeState()  // 상태(탐색중, 진입대기, 진입중, 진입완료, 청산대기, 청산중)
        {
            string strPosition = grdAutoTrade.Rows[0].Cells[3].Value.ToString();
            int stat1 = System.Convert.ToInt32(curOrVolume);
            int stat2 = System.Convert.ToInt32(curChVolume);
            int stat3 = System.Convert.ToInt32(curInVolume);
            if (_bRealTrade == false)
            {
                curState = "";
                SetAutoTrade(4);
                return;
            }
            else
            {
                if (strPosition == tradePosition[1])
                {
                    if (stat1 == 0 && stat2 == 0)
                    {
                        curState = tradeState[0];
                        SetAutoTrade(4);
                    }
                }
                else
                {
                    if (stat1 == 0 && stat2 == 0)
                    {
                        curState = tradeState[0];
                        SetAutoTrade(4);
                    }
                    else if (stat1 == stat3 && stat2 == 0)
                    {
                        curState = tradeState[1];
                        SetAutoTrade(4);
                    }
                    else if (stat1 > 0 && stat2 > 0 && (stat1 + stat2) == stat3)
                    {
                        curState = tradeState[2];
                        SetAutoTrade(4);
                    }
                    else if (stat1 == 0 && stat2 == stat3)
                    {
                        curState = tradeState[3];
                        SetAutoTrade(4);
                    }
                    else if (stat1 == stat3 && stat2 == stat3)
                    {
                        curState = tradeState[4];
                        SetAutoTrade(4);
                    }
                    else if (stat1 == stat2 && stat2 < stat3 && stat1 != 0)
                    {
                        curState = tradeState[5];
                        SetAutoTrade(4);
                    }
                    else
                    {
                        curState = "";
                        SetAutoTrade(4);
                    }
                }
            }
        }
        private void SetGain()
        {
            if (!InitFlag)
            {
                return;
            }
            if (havePosition == "" || curChPrice == "")
            {
                curGain = "";
                SetAutoTrade(10);
            }
            else
            {
                int count = System.Convert.ToInt32(curChVolume);
                double chegyul = System.Convert.ToDouble(curChPrice);
                double current = System.Convert.ToDouble(curPrice);
                double ticValue = System.Convert.ToDouble(grdJongInfo.Rows[6].Cells[3].Value.ToString());
                double tic = 1 / System.Convert.ToDouble(grdJongInfo.Rows[6].Cells[1].Value.ToString());
                double gain;
                string Gain;
                string position = grdAutoTrade.Rows[0].Cells[3].Value.ToString();
                if (chegyul > 0)
                {
                    if (position == tradePosition[2]) // 매수 포지션이다.
                    {
                        gain = (current - chegyul) * tic * ticValue * count;
                        Gain = Math.Round(gain, 2, MidpointRounding.AwayFromZero).ToString();
                        curGain = Gain;
                        SetAutoTrade(10);
                    }
                    else if (position == tradePosition[3])
                    {
                        gain = (chegyul - current) * tic * ticValue * count;
                        Gain = Math.Round(gain, 2, MidpointRounding.AwayFromZero).ToString();
                        curGain = Gain;
                        SetAutoTrade(10);
                    }
                    else
                    {
                        curGain = "";
                        SetAutoTrade(10);
                        laView.Text = "포지션 점검 바랍니다!";
                    }
                }
            }
            
        }
        private void SetChegyul(string[,] arrData) // 주문 내용을 그리드에 입력한다.
        {
            int i, nCnt;
            int buyOrderCount = 0, sellOrderCount = 0;
            double buyHogaTotal = 0, sellHogaTotal = 0, buyHogaAver, sellHogaAver;

            nCnt = arrData.Length / lstFID_SIZE;
            for (i = 0; i < nCnt; i++)
            {
                if (arrData[i, 5] != "")
                {
                    if (arrData[i, 2] == "매수" || arrData[i, 2] == "매수정정")
                    {
                        recentBuyOriNo = arrData[i, 0];
                        buyOrderCount = buyOrderCount + System.Convert.ToInt32(arrData[i, 5]);
                        buyHogaTotal = buyHogaTotal + (buyOrderCount * System.Convert.ToDouble(arrData[i, 3]));
                    }
                    else if (arrData[i, 2] == "매도" || arrData[i, 2] == "매도정정")
                    {
                        recentSellOriNo = arrData[i, 0];
                        sellOrderCount = sellOrderCount + System.Convert.ToInt32(arrData[i, 5]);
                        sellHogaTotal = sellHogaTotal + (sellOrderCount * System.Convert.ToDouble(arrData[i, 3]));
                    }
                }
            }
            laView.Text = "매수주문개수 : " + buyOrderCount + "  매도주문개수 : " + sellOrderCount;

            if (buyOrderCount == 0 && sellOrderCount == 0)
            {
                orderPosition = "";
                SetPosition("");
                curOrVolume = "0";
                curOrPrice = "";
                SetAutoTrade(5);
                SetAutoTrade(8);
            }
            else if (buyOrderCount > sellOrderCount)
            {
                orderPosition = tradePosition[2];
                buyHogaAver = buyHogaTotal / buyOrderCount;
                curOrVolume = buyOrderCount.ToString();
                curOrPrice = buyHogaAver.ToString();
                SetAutoTrade(5);
                SetAutoTrade(8);
            }
            else if (buyOrderCount < sellOrderCount)
            {
                orderPosition = tradePosition[3];
                sellHogaAver = sellHogaTotal / sellOrderCount;
                curOrVolume = sellOrderCount.ToString();
                curOrPrice = sellHogaAver.ToString();
                SetAutoTrade(5);
                SetAutoTrade(8);
            }
            else
                laView.Text = "양방향 주문이 이루어 졌습니다.";

            if (curOrVolume == curInVolume && curChVolume == curInVolume)
            {
                curGoPrice = curOrPrice;
                SetAutoTrade(11);
            }

        }
        private void SetChegyulData(int nInsertRow, int nRow, string[] arrData)
        {
            if (nInsertRow >= 30)
                nInsertRow = -1;

            int nCnt = arrData.Length;
            string strData;

            for (int i = 0; i < nCnt; i++)
            {
                strData = arrData[i];
                chegyulData[nRow, i] = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstFID[i].nDataType, strData.Trim(), lstFID[i].strBeforeData, lstFID[i].strAfterData);
            }
        }
        private void LoadHoga()
        {
            string strFileName = LD.SetFileName(txtCode.Text, "grdHogaHigh");
            FileInfo fi = new FileInfo(strFileName);
            if (!fi.Exists)
                return;
            
            const string message = "로컬정보를 불러 오겠습니까?";
            const string caption = "호가 데이터 불러오기";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int nCnt;
                LD.LoadData(m_strJongCode, "grdHogaHigh");
                LD.LoadData(m_strJongCode, "grdHogaLow");

                string[] highData = LD.GetHogaData("고가갱신");
                if (highData == null)
                    return;
                else
                {
                    nCnt = highData.Length;
                    for (int i = 0; i < nCnt; i++)
                    {
                        if (lstOPT10003[i].bTextColor)
                        {
                            ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaHigh, lstOPT10003[i].nRow, lstOPT10003[i].nCol, highData[i]);
                        }
                        if (lstOPT10003[i].nDataType == Constants.DT_SIGN)
                        {
                            ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaHigh, lstOPT10003[i].nRow, lstOPT10003[i].nCol, highData[i]);
                        }
                        else
                        {
                            grdHogaHigh.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, highData[i].Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData);
                        }
                    }
                    sellHogaMax = System.Convert.ToInt32(highData[20]);
                    highPrice = highData[9];
                    high_LowPrice = highData[10];
                    grdJongInfo.Rows[3].Cells[1].Value = highPrice;
                }

                string[] lowData = LD.GetHogaData("저가갱신");
                if (lowData == null)
                    return;
                else
                {
                    nCnt = lowData.Length;
                    for (int i = 0; i < nCnt; i++)
                    {
                        if (lstOPT10003[i].bTextColor)
                        {
                            ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaLow, lstOPT10003[i].nRow, lstOPT10003[i].nCol, lowData[i]);
                        }
                        if (lstOPT10003[i].nDataType == Constants.DT_SIGN)
                        {
                            ((Kiwoom_AutoTrade_FF)this.Owner).SetDataFgColour(grdHogaLow, lstOPT10003[i].nRow, lstOPT10003[i].nCol, lowData[i]);
                        }
                        else
                        {
                            grdHogaLow.Rows[lstOPT10003[i].nRow].Cells[lstOPT10003[i].nCol].Value = ((Kiwoom_AutoTrade_FF)this.Owner).ConvDataFormat(lstOPT10003[i].nDataType, lowData[i].Trim(), lstOPT10003[i].strBeforeData, lstOPT10003[i].strAfterData); //strData.Trim();
                        }
                    }
                    buyHogaMax = System.Convert.ToInt32(lowData[21]);
                    lowPrice = lowData[10];
                    low_HighPrice = lowData[9];
                    grdJongInfo.Rows[2].Cells[1].Value = lowPrice;
                }
            }
            else
                return;
            
            
        }
        private void LoadGoal()
        {
            string strFileName = LD.SetFileName(txtCode.Text, "grdGoal");
            FileInfo fi = new FileInfo(strFileName);
            if (!fi.Exists)
                return;

            const string message = "목표 데이터가 있습니다. 불러 오겠습니까?";
            const string caption = "목표 데이터 불러오기";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LD.LoadData(txtCode.Text, "grdGoal");
                string[] strData = LD.GetGoalData();
                autoPosition = strData[0];
                curGoPrice = strData[1];
                SetAutoTrade(3);
                SetAutoTrade(11);
                ((Kiwoom_AutoTrade_FF)this.Owner).SetGoalPrice(txtCode.Text, curGoPrice);
            }
            else
            {
                LD.DeleteData(txtCode.Text, "grdGoal");
            }
        }

        // 참고 메소드
        private double ValueAbs(double value)
        {
            if (value < 0)
            {
                value = value * -1;
            }
            return value;
        }
               

        // 자동매매 명령 메소드 
        private void StopLoss() // 자체 감시에 의해서 손절을 진행한다.
        {
            int stat1, stat2, stat3;
            stat1 = System.Convert.ToInt32(grdAutoTrade.Rows[0].Cells[5].Value.ToString());
            stat2 = System.Convert.ToInt32(grdAutoTrade.Rows[0].Cells[6].Value.ToString());
            stat3 = System.Convert.ToInt32(grdAutoTrade.Rows[0].Cells[7].Value.ToString());
            double ticValue = System.Convert.ToDouble(grdJongInfo.Rows[6].Cells[3].Value.ToString());
            int stopValue = System.Convert.ToInt32(grdAutoTrade.Rows[0].Cells[12].Value.ToString());
            double stopPrice = (-1) * ticValue * stopValue * stat2;
            double cancelPrice = (-1) * ticValue * (stopValue - 10) * stat2;

            grdHogaLow.Rows[6].Cells[0].Value = "취소";
            grdHogaLow.Rows[7].Cells[0].Value = cancelPrice;
            grdHogaLow.Rows[8].Cells[0].Value = "손절";
            grdHogaLow.Rows[9].Cells[0].Value = stopPrice;

            double gain;
            if (grdAutoTrade.Rows[0].Cells[10].Value.ToString() == "")
                gain = 0;
            else
                gain = System.Convert.ToDouble(grdAutoTrade.Rows[0].Cells[10].Value.ToString());

            if (stat2 > 0)
            {
                if (gain <= cancelPrice)
                {
                    if (stat1 > 0) // 접수 취소후 시장가 청산
                    {
                        if (grdAutoTrade.Rows[0].Cells[3].Value.ToString() == tradePosition[2] && stopCancle == 0) // 매수 상태 이면
                        {
                            stopCancle = 1;
                            grdHogaLow.Rows[5].Cells[0].Value = "손절 준비";
                            TradeEx("매도취소");
                        }
                        else if (grdAutoTrade.Rows[0].Cells[3].Value.ToString() == tradePosition[3] && stopCancle == 0) // 매도 상태 이면
                        {
                            stopCancle = 1;
                            grdHogaLow.Rows[5].Cells[0].Value = "손절 준비";
                            TradeEx("매수취소");
                        }
                    }
                    else if (gain <= stopPrice && stopCancle == 1)
                    {
                        if (grdAutoTrade.Rows[0].Cells[3].Value.ToString() == tradePosition[2]) // 매수 상태 이면
                        {
                            stopCancle = 0;
                            grdHogaLow.Rows[5].Cells[0].Value = "손절";
                            curGain = "";
                            curGoPrice = "";
                            SetAutoTrade(0);
                            TradeEx("매수청산(시장가)");
                        }
                        else if (grdAutoTrade.Rows[0].Cells[3].Value.ToString() == tradePosition[3]) // 매도 상태 이면
                        {
                            stopCancle = 0;
                            grdHogaLow.Rows[5].Cells[0].Value = "손절";
                            curGain = "";
                            curGoPrice = "";
                            SetAutoTrade(0);
                            TradeEx("매도청산(시장가)");
                        }
                    }
                }
                else if (gain > 0 && stopCancle == 1)
                {
                    if (grdAutoTrade.Rows[0].Cells[3].Value.ToString() == tradePosition[2]) // 매수 상태 이면
                    {
                        stopCancle = 0;
                        grdHogaLow.Rows[5].Cells[0].Value = "청산대기";
                    }
                    else if (grdAutoTrade.Rows[0].Cells[3].Value.ToString() == tradePosition[3]) // 매도 상태 이면
                    {
                        stopCancle = 0;
                        grdHogaLow.Rows[5].Cells[0].Value = "청산대기";
                    }
                }
            }
        }
        private void OrderTrade(string strOrderType, string strHogaGb, string strOriNo, string strVolume, string strPrice)
        {
            string strAccNo = txtAccNo.Text;
            if (strAccNo.Length != 10)
            {
                MessageBox.Show("계좌번호 10자리를 입력해 주세요.");
                txtAccNo.SelectAll();
                return;
            }
            //종목코드 
            m_strJongCode = txtCode.Text;

            string strStopPrice = "";
            int iOrderType = System.Convert.ToInt32(strOrderType);
            if (iOrderType == 5 || iOrderType == 6)  // 주문 정정시 STOP Price 1 설정
            {
                strStopPrice = "1";
            }

            //주문수량
            int iQty = System.Convert.ToInt32(strVolume);
            if (iQty < 1)
            {
                MessageBox.Show("주문수량을 0보다 큰 수로 입력해 주세요.");
                return;
            }

            if (iOrderType > 2 && strOriNo.Length < 1)
            {
                MessageBox.Show("원주문번호를 입력해 주십시요.");
                return;
            }
            string strRQName = "주문";

            int iRet = ((Kiwoom_AutoTrade_FF)this.Owner).axKFOpenAPI1.SendOrder(strRQName, m_strScrNo, strAccNo, iOrderType, m_strJongCode, iQty, strPrice, strStopPrice, strHogaGb, strOriNo);

            if (!((Kiwoom_AutoTrade_FF)this.Owner).IsError(iRet))
            {
                return;
            }
        }
        private void TradeEx(string strOrder)
        {
            string orderType;
            string OrderType, HogaGb, OriNo, Volume, Price;
            if (strOrder == "")
            {
                orderType = grdAutoTrade.Rows[0].Cells[13].Value.ToString();
            }
            else
            {
                orderType = strOrder;
                if (!(orderType == "신규매수(지정가)" || orderType == "신규매도(지정가)" || orderType == "신규매수(시장가)" || orderType == "신규매도(시장가)" || orderType == "매수정정(지정가)" || orderType == "매도정정(지정가)" || orderType == "매수청산(지정가)" || orderType == "매도청산(지정가)" || orderType == "매수청산(시장가)" || orderType == "매도청산(시장가)" || orderType == "매수취소" || orderType == "매도취소"))
                {
                    laView.Text = "잘못된 주문 형식입니다.";
                    return;
                }

            }
            if(orderType == "신규매수(지정가)" || orderType == "신규매도(지정가)" || orderType == "매수정정(지정가)" || orderType == "매도정정(지정가)")
            {
                if(grdAutoTrade.Rows[0].Cells[8].Value.ToString() == "")
                {
                    laView.Text = "입력 데이터가 부족합니다.";
                    return;
                }
            }
            else if (orderType == "매수청산(지정가)" || orderType == "매도청산(지정가)")
            {
                if ( grdAutoTrade.Rows[0].Cells[11].Value.ToString() == "")
                {
                    laView.Text = "입력 데이터가 부족합니다.";
                    return;
                }
            }
            if (!orderFlag)
            {
                return;
            }

            orderFlag = false;

            if (orderType == "신규매수(지정가)")
            {
                OrderType = "2"; // 신규매수
                HogaGb = "2"; // 지정가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[7].Value.ToString();
                Price = grdAutoTrade.Rows[0].Cells[8].Value.ToString();
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "신규매도(지정가)")
            {
                OrderType = "1"; // 신규매도
                HogaGb = "2"; // 지정가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[7].Value.ToString();
                Price = grdAutoTrade.Rows[0].Cells[8].Value.ToString();
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "신규매수(시장가)")
            {
                OrderType = "2"; // 신규매수
                HogaGb = "1"; // 시장가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[7].Value.ToString();
                Price = "";
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "신규매도(시장가)")
            {
                OrderType = "1"; // 신규매도
                HogaGb = "1"; // 시장가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[7].Value.ToString();
                Price = "";
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "매수정정(지정가)")
            {
                OrderType = "6"; // 매수정정
                HogaGb = "2"; // 지정가
                int j = 0, temp;
                for (int i = 0; i < 30; i++)
                {

                    if (chegyulData[i, 5] == "")
                        temp = 0;
                    else
                        temp = System.Convert.ToInt32(chegyulData[i, 5]);

                    j++;

                    if (((chegyulData[i, 6] == "접수" && chegyulData[i, 2] == "매수") || (chegyulData[i, 6] == "확인" && chegyulData[i, 2] == "매수정정")) && temp > 0)
                    {
                        OriNo = chegyulData[i, 0];
                        Volume = System.Convert.ToInt32(chegyulData[i, 5]).ToString();
                        if (chegyulData[i, 3] != grdAutoTrade.Rows[0].Cells[8].Value.ToString())
                        {
                            Price = grdAutoTrade.Rows[0].Cells[8].Value.ToString();
                            laView.Text = Price + "에" + Volume + "계약이 정정되었습니다.";
                            i = 30;
                            OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
                        }
                        else
                        {
                            laView.Text = "원래 주문가격과 동일합니다.";
                            i = 30;
                        }
                    }
                    if (j == 30)
                        laView.Text = "매수 접수 내용이 없습니다.";
                }
            }
            else if (orderType == "매도정정(지정가)")
            {
                OrderType = "5"; // 매도정정
                HogaGb = "2"; // 지정가
                int j = 0, temp;
                for (int i = 0; i < 30; i++)
                {
                    if (chegyulData[i, 5] == "")
                        temp = 0;
                    else
                        temp = System.Convert.ToInt32(chegyulData[i, 5]);

                    j++;
                    if (((chegyulData[i, 6] == "접수" && chegyulData[i, 2] == "매도") || (chegyulData[i, 6] == "확인" && chegyulData[i, 2] == "매도정정")) && temp > 0)
                    {
                        OriNo = chegyulData[i, 0];
                        Volume = System.Convert.ToInt32(chegyulData[i, 5]).ToString();
                        if (chegyulData[i, 3] != grdAutoTrade.Rows[0].Cells[8].Value.ToString())
                        {
                            Price = grdAutoTrade.Rows[0].Cells[8].Value.ToString();
                            laView.Text = Price + "에" + Volume + "계약이 정정되었습니다.";
                            i = 30;
                            OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
                        }
                        else
                        {
                            laView.Text = "원래 주문가격과 동일합니다.";
                            i = 30;
                        }
                    }
                    if (j == 30)
                        laView.Text = "매도 접수 내용이 없습니다.";
                }
            }
            if (orderType == "매수청산(지정가)")
            {
                OrderType = "1"; // 신규매도 = 매수청산
                HogaGb = "2"; // 지정가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[6].Value.ToString();
                Price = grdAutoTrade.Rows[0].Cells[11].Value.ToString();
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "매도청산(지정가)")
            {
                OrderType = "2"; // 신규매수 = 매도청산
                HogaGb = "2"; // 지정가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[6].Value.ToString();
                Price = grdAutoTrade.Rows[0].Cells[11].Value.ToString();
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "매수청산(시장가)")
            {
                OrderType = "1"; // 신규매도 = 매수청산
                HogaGb = "1"; // 시장가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[6].Value.ToString();
                Price = "";
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "매도청산(시장가)")
            {
                OrderType = "2"; // 신규매수 = 매도청산
                HogaGb = "1"; // 시장가
                OriNo = "";
                Volume = grdAutoTrade.Rows[0].Cells[6].Value.ToString();
                Price = "";
                OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
            }
            else if (orderType == "매수취소")
            {
                OrderType = "4"; // 매수취소
                HogaGb = "";
                Price = "";
                int j = 0, temp;
                
                for (int i = 0; i < 30; i++)
                {
                    if (chegyulData[i, 5] == "")
                        temp = 0;
                    else
                        temp = System.Convert.ToInt32(chegyulData[i, 5]);

                    j++;
                    if (((chegyulData[i, 6] == "접수" && chegyulData[i, 2] == "매수") || (chegyulData[i, 6] == "확인" && chegyulData[i, 2] == "매수정정")) && temp > 0)
                    {
                        OriNo = chegyulData[i, 0];
                        Volume = System.Convert.ToInt32(chegyulData[i, 5]).ToString();
                        i = 30;
                        OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
                    }
                    if (j == 30)
                    {
                        laView.Text = "매수 접수 내용이 없습니다.";
                    }
                    
                }
            }
            else if (orderType == "매도취소")
            {
                OrderType = "3"; // 매도취소
                HogaGb = "";
                Price = "";
                int j = 0, temp;
                
                for (int i = 0; i < 30; i++)
                {
                    if (chegyulData[i, 5] == "")
                        temp = 0;
                    else
                        temp = System.Convert.ToInt32(chegyulData[i, 5]);

                    j++;
                    if (((chegyulData[i, 6] == "접수" && chegyulData[i, 2] == "매도") || (chegyulData[i, 6] == "확인" && chegyulData[i, 2] == "매도정정")) && chegyulData[i, 5] != "" && temp > 0)
                    {
                        OriNo = chegyulData[i, 0];
                        Volume = System.Convert.ToInt32(chegyulData[i, 5]).ToString();
                        i = 30;
                        OrderTrade(OrderType, HogaGb, OriNo, Volume, Price);
                    }
                    if (j == 30)
                    {
                        laView.Text = "매도 접수 내용이 없습니다.";
                    }
                }
            }
            else
            {
                laView.Text = "주문종류를 선택하십시오.";
                return;
            }
        }
        private void SetAutoTrade(int curData)
        {
            if (curData == 0)
            {
                grdAutoTrade.Rows[0].Cells[2].Value = curPrice;
                grdAutoTrade.Rows[0].Cells[5].Value = curOrVolume;
                grdAutoTrade.Rows[0].Cells[6].Value = curChVolume;
                grdAutoTrade.Rows[0].Cells[8].Value = curOrPrice;
                grdAutoTrade.Rows[0].Cells[9].Value = curChPrice;
                grdAutoTrade.Rows[0].Cells[11].Value = curGoPrice;
                SetMode();
                SetPosition("");
                SetTradeState();
                SetGain();
            }
            else if (curData == 2)
            {
                if (grdAutoTrade.Rows[0].Cells[2].Value.ToString() != curPrice)
                    grdAutoTrade.Rows[0].Cells[2].Value = curPrice;
                else
                    return;
            }
            else if (curData == 3)
            {
                if (grdAutoTrade.Rows[0].Cells[3].Value.ToString() != curPosition)
                    grdAutoTrade.Rows[0].Cells[3].Value = curPosition;
                else
                    return;
            }
            else if (curData == 4)
            {
                if (grdAutoTrade.Rows[0].Cells[4].Value.ToString() != curState)
                    grdAutoTrade.Rows[0].Cells[4].Value = curState;
                else
                    return;
            }
            else if (curData == 5)
            {
                if (grdAutoTrade.Rows[0].Cells[5].Value.ToString() != curOrVolume)
                    grdAutoTrade.Rows[0].Cells[5].Value = curOrVolume;
                else
                    return;
            }
            else if (curData == 6)
            {
                if (grdAutoTrade.Rows[0].Cells[6].Value.ToString() != curChVolume)
                    grdAutoTrade.Rows[0].Cells[6].Value = curChVolume;
                else
                    return;
            }
            else if (curData == 8)
            {
                if (grdAutoTrade.Rows[0].Cells[8].Value.ToString() != curOrPrice)
                    grdAutoTrade.Rows[0].Cells[8].Value = curOrPrice;
                else
                    return;
            }
            else if (curData == 9)
            {
                if (grdAutoTrade.Rows[0].Cells[9].Value.ToString() != curChPrice)
                    grdAutoTrade.Rows[0].Cells[9].Value = curChPrice;
                else
                    return;
            }
            else if (curData == 10)
            {
                if (grdAutoTrade.Rows[0].Cells[10].Value.ToString() != curGain)
                    grdAutoTrade.Rows[0].Cells[10].Value = curGain;
                else
                    return;
            }
            else if (curData == 11)
            {
                if (grdAutoTrade.Rows[0].Cells[11].Value.ToString() != curGoPrice)
                    grdAutoTrade.Rows[0].Cells[11].Value = curGoPrice;
                else
                    return;
            }
            else if (curData == 99)
            {
                curTrMode = grdAutoTrade.Rows[0].Cells[0].Value.ToString();
                curCode = grdAutoTrade.Rows[0].Cells[1].Value.ToString();
                curPrice = grdAutoTrade.Rows[0].Cells[2].Value.ToString();
                curPosition = grdAutoTrade.Rows[0].Cells[3].Value.ToString();
                curState = grdAutoTrade.Rows[0].Cells[4].Value.ToString();
                curOrVolume = grdAutoTrade.Rows[0].Cells[5].Value.ToString();
                curChVolume = grdAutoTrade.Rows[0].Cells[6].Value.ToString();
                curInVolume = grdAutoTrade.Rows[0].Cells[7].Value.ToString();
                curOrPrice = grdAutoTrade.Rows[0].Cells[8].Value.ToString();
                curChPrice = grdAutoTrade.Rows[0].Cells[9].Value.ToString();
                curGain = grdAutoTrade.Rows[0].Cells[10].Value.ToString();
                curGoPrice = grdAutoTrade.Rows[0].Cells[11].Value.ToString();
                curInStop = grdAutoTrade.Rows[0].Cells[12].Value.ToString();
                curOrType = grdAutoTrade.Rows[0].Cells[13].Value.ToString();
                curSend = grdAutoTrade.Rows[0].Cells[14].Value.ToString();
            }
            else
                laView.Text = curData.ToString();
                return;
        }
        private void CalcHogaAver()
        {
            double temp;
            if (grdHoga.Rows[10].Cells[0].Value.ToString() == "")
                temp = sellHogaAver;
            else
                temp = System.Convert.ToDouble(grdHoga.Rows[10].Cells[0].Value.ToString());
            temp = sellHogaAver * 0.98 + temp * 0.02;
            sellHogaAver = System.Convert.ToInt32(temp);
            grdHoga.Rows[5].Cells[0].Value = sellHogaAver;

            if (grdHoga.Rows[10].Cells[2].Value.ToString() == "")
                temp = buyHogaAver;
            else
                temp = System.Convert.ToDouble(grdHoga.Rows[10].Cells[2].Value.ToString());
            temp = buyHogaAver * 0.98 + temp * 0.02;
            buyHogaAver = System.Convert.ToInt32(temp);
            grdHoga.Rows[4].Cells[2].Value = buyHogaAver;

        }
        private void SetMode()
        {
            if (!_bRealTrade)
                curTrMode = tradeMode[0];
            else
                curTrMode = tradeMode[1];
            grdAutoTrade.Rows[0].Cells[0].Value = curTrMode;

        }
        private double InsertHoga()
        {
            double insertHoga;
            double tickSize = System.Convert.ToDouble(grdJongInfo.Rows[6].Cells[1].Value.ToString());
            int sellVolume, buyVolume, temp, j;
            if (curPosition == tradePosition[2]) //매수일 경우
            {
                sellVolume = System.Convert.ToInt32(grdHogaHigh.Rows[10].Cells[0].Value.ToString());
                buyVolume = System.Convert.ToInt32(grdHogaHigh.Rows[10].Cells[2].Value.ToString());
                if (buyVolume >= sellVolume * 0.8)
                {
                    temp = System.Convert.ToInt32(grdHogaHigh.Rows[5].Cells[2].Value.ToString());
                    j = 5;
                    for (int i = 6; i < 10; i++)
                    {
                        if (System.Convert.ToInt32(grdHogaHigh.Rows[i].Cells[2].Value.ToString()) >= temp)
                        {
                            temp = System.Convert.ToInt32(grdHogaHigh.Rows[i].Cells[2].Value.ToString());
                            j = i;
                        }
                    }
                    insertHoga = System.Convert.ToDouble(grdHogaHigh.Rows[j].Cells[1].Value.ToString()) + 1 * tickSize;
                    return insertHoga;
                }
                else
                {
                    insertHoga = System.Convert.ToDouble(grdHogaHigh.Rows[5].Cells[1].Value.ToString()) + 3 * tickSize;
                    if (insertHoga - System.Convert.ToDouble(curPrice) >= 0)
                        insertHoga = System.Convert.ToDouble(grdHogaHigh.Rows[5].Cells[1].Value.ToString());
                    return insertHoga;

                }
            }
            else if (curPosition == tradePosition[3]) //매도일 경우 
            {
                sellVolume = System.Convert.ToInt32(grdHogaLow.Rows[10].Cells[0].Value.ToString());
                buyVolume = System.Convert.ToInt32(grdHogaLow.Rows[10].Cells[2].Value.ToString());
                if (sellVolume >= buyVolume * 0.8)
                {
                    temp = System.Convert.ToInt32(grdHogaLow.Rows[4].Cells[0].Value.ToString());
                    j = 4;
                    for (int i = 3; i >= 0; i--)
                    {
                        if (System.Convert.ToInt32(grdHogaLow.Rows[i].Cells[0].Value.ToString()) >= temp)
                        {
                            temp = System.Convert.ToInt32(grdHogaLow.Rows[i].Cells[0].Value.ToString());
                            j = i;
                        }
                    }
                    insertHoga = System.Convert.ToDouble(grdHogaLow.Rows[j].Cells[1].Value.ToString()) - 1 * tickSize;
                    return insertHoga;
                }
                else
                {
                    insertHoga = System.Convert.ToDouble(grdHogaLow.Rows[4].Cells[1].Value.ToString()) - 3 * tickSize;
                    if (System.Convert.ToDouble(curPrice) - insertHoga >= 0)
                        insertHoga = System.Convert.ToDouble(grdHogaHigh.Rows[5].Cells[1].Value.ToString());
                    return insertHoga;
                }
            }
            else
                return 0;
        }

        // 자동 매매 메소드 
        private void SearchAutoGoal(string strAutoPosition, string strGoalPrice, string strIntoPrice)//목표가 탐색 메소드
        {
            if (_bRealTrade == false)
                return;

            if (autoPosition == "")
                return;

            double temp;

            if (strAutoPosition == "매수" && strGoalPrice == "")
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        autoData[i, j] = System.Convert.ToDouble(grdHogaHigh.Rows[j].Cells[i].Value.ToString());
                    }
                }
                sellHogaMax = System.Convert.ToInt32(grdHogaHigh.Rows[10].Cells[0].Value.ToString());
                if (sellHogaMax >= conTotal * buyHogaAver)
                {
                    double[] max1 = new double[2];
                    double[] max2 = new double[2];
                    temp = autoData[0, 1];
                    if (autoData[0, 0] > temp)  // 호가 잔량의 최대값과 호가를 구한다.
                    {
                        max1[0] = autoData[0, 0];
                        max1[1] = autoData[1, 0];
                        max2[0] = autoData[0, 1];
                    }
                    else
                    {
                        max1[0] = autoData[0, 1];
                        max1[1] = autoData[1, 1];
                        max2[0] = autoData[0, 0]; 
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        temp = autoData[0, i + 1];
                        if (temp >= max1[0])
                        {
                            max2[0] = max1[0];
                            max1[0] = autoData[0, i + 1];
                            max1[1] = autoData[1, i + 1];
                        }
                        else if (temp < max1[0] && temp > max2[0])
                        {
                            max2[0] = autoData[0, i + 1];
                        }

                    }
                    if ((max1[0] > conHoga1 * sellHogaMax) || (max1[0] + max2[0]) > conHoga2 * sellHogaMax)
                    {
                        sellHoga = max1[1].ToString();
                        curGoPrice = sellHoga;
                        string[] arrData = new string[2] { autoPosition, curGoPrice };
                        LD.SetGoalData(arrData);
                        LD.SaveData(txtCode.Text, "grdGoal", LD.GetGoalData());
                        SetAutoTrade(0);
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetGoalPrice(txtCode.Text, curGoPrice);
                    }
                }
            }
            else if (strAutoPosition == "매도" && strGoalPrice == "")
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        autoData[i, j] = System.Convert.ToDouble(grdHogaLow.Rows[j + 5].Cells[2 - i].Value.ToString());
                    }
                }
                buyHogaMax = System.Convert.ToInt32(grdHogaLow.Rows[10].Cells[2].Value.ToString());
                if (buyHogaMax >= conTotal * sellHogaAver)
                {
                    double[] max1 = new double[2];
                    double[] max2 = new double[2];
                    temp = autoData[0, 1];
                    if (autoData[0, 1] > temp)  // 호가 잔량의 최대값과 호가를 구한다.
                    {
                        max1[0] = autoData[0, 0];
                        max1[1] = autoData[1, 0];
                        max2[0] = autoData[0, 1];
                    }
                    else
                    {
                        max1[0] = autoData[0, 1];
                        max1[1] = autoData[1, 1];
                        max2[0] = autoData[0, 0];
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        temp = autoData[0, i + 1];
                        if (temp >= max1[0])
                        {
                            max2[0] = max1[0];
                            max1[0] = autoData[0, i + 1];
                            max1[1] = autoData[1, i + 1];
                        }
                        else if (temp < max1[0] && temp > max2[0])
                        {
                            max2[0] = autoData[0, i + 1];
                        }
                    }
                    if ((max1[0] > conHoga1 * buyHogaMax) || (max1[0] + max2[0]) > conHoga2 * buyHogaMax)
                    {
                        buyHoga = max1[1].ToString();
                        curGoPrice = buyHoga;
                        string[] arrData = new string[2] { autoPosition, curGoPrice };
                        LD.SetGoalData(arrData);
                        LD.SaveData(txtCode.Text, "grdGoal", LD.GetGoalData());
                        SetAutoTrade(0);
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetGoalPrice(txtCode.Text, curGoPrice);
                    }
                }
            }
        }
        private void SearchAutoInto(string strAutoPosition, string strGoalPrice, string strIntoPrice)//진입가 탐색 메소드
        {
            double currentPrice = System.Convert.ToDouble(curPrice);
            double curGoalPrice = System.Convert.ToDouble(curGoPrice);
            double tickSize = System.Convert.ToDouble(grdJongInfo.Rows[6].Cells[1].Value.ToString());

            if ((strAutoPosition == tradePosition[2]) && (currentPrice > curGoalPrice))
            {
                curOrPrice = "";
                curGoPrice = "";
                LD.DeleteData(txtCode.Text, "grdGoal");
                SetAutoTrade(0);
                ((Kiwoom_AutoTrade_FF)this.Owner).SetGoalPrice(txtCode.Text, curGoPrice);
                autoIntoJudge[0] = true;
                autoIntoJudge[1] = false;
                autoIntoJudge[2] = false;
                autoIntoJudge[3] = false;
                autoIntoJudge[4] = false;
                autoIntoJudge[5] = false;
                laView.Text = "목표값 재설정 중";
            }
            else if ((strAutoPosition == tradePosition[3]) && (currentPrice < curGoalPrice))
            {
                curOrPrice = "";
                curGoPrice = "";
                LD.DeleteData(txtCode.Text, "grdGoal");
                SetAutoTrade(0);
                ((Kiwoom_AutoTrade_FF)this.Owner).SetGoalPrice(txtCode.Text, curGoPrice);
                autoIntoJudge[0] = true;
                autoIntoJudge[1] = false;
                autoIntoJudge[2] = false;
                autoIntoJudge[3] = false;
                autoIntoJudge[4] = false;
                autoIntoJudge[5] = false;
                laView.Text = "목표값 재설정 중";
            }
            else if (ValueAbs(currentPrice - curGoalPrice) <  10 * tickSize)
                return;
            else
            {
                int VolumeMax;
                double IntoHoga;
                if (strAutoPosition == "매수")
                {

                    VolumeMax = System.Convert.ToInt32(grdHogaHigh.Rows[10].Cells[0].Value.ToString());
                    if (autoIntoJudge[0])
                    {
                        if (VolumeMax * conVolume < buyHogaAver)
                        {
                            laView.Text = "진입신호 포착! 매도 잔량 : " + VolumeMax + " 매수 잔량 : " + buyHogaAver;
                            hogaVolume = buyHogaAver;
                            autoIntoJudge[0] = false;
                            autoIntoJudge[1] = true;
                        }
                        else
                        {
                            laView.Text = "진입가 탐색 중입니다.";
                        }
                    }
                    else if (autoIntoJudge[1])  // 진입 신호 포착
                    {
                        if (hogaVolume < buyHogaAver)
                        {
                            hogaVolume = buyHogaAver;
                            laView.Text = "거래량 갱신";
                        }
                        else if (VolumeMax * conVolume < buyHogaAver)
                        {
                            laView.Text = "진입신호 포착!";
                        }
                        if (sellHogaAver * 1.2 < buyHogaAver)
                        {
                            laView.Text = "진입신호 1단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                            autoIntoJudge[1] = false;
                            autoIntoJudge[2] = true;
                        }
                    }
                    else if (autoIntoJudge[2])  // 진입신호 1단계!!
                    {
                        /*
                        if (VolumeMax * conVolume < buyHogaAver)
                        {
                            laView.Text = "진입신호 포착!";
                            autoIntoJudge[2] = false;
                            autoIntoJudge[1] = true;
                        }
                        */
                        if (hogaVolume < buyHogaAver)
                        {
                            hogaVolume = buyHogaAver;
                            laView.Text = "거래량 갱신";
                            autoIntoJudge[2] = false;
                            autoIntoJudge[1] = true;
                        }
                        else if (sellHogaAver * 1.2 < buyHogaAver)
                        {
                            laView.Text = "진입신호 1단계";
                        }
                        else if (sellHogaAver * 1.05 < buyHogaAver)
                        {
                            laView.Text = "진입신호 2단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                            autoIntoJudge[2] = false;
                            autoIntoJudge[3] = true;
                        }
                    }
                    else if (autoIntoJudge[3]) // 진입신호 2단계!!
                    {
                        if (hogaVolume < buyHogaAver)
                        {
                            hogaVolume = buyHogaAver;
                            laView.Text = "거래량 갱신";
                            autoIntoJudge[3] = false;
                            autoIntoJudge[1] = true;
                        }
                        else if (sellHogaAver * 1.2 < buyHogaAver)
                        {
                            laView.Text = "진입신호 1단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                            autoIntoJudge[3] = false;
                            autoIntoJudge[2] = true;
                        }
                        else if (sellHogaAver * 1.05 < buyHogaAver)
                        {
                            laView.Text = "진입신호 2단계";
                        }
                        else if (sellHogaAver * 1.05 > buyHogaAver && buyHogaAver * 1.05 > sellHogaAver)
                        {
                            laView.Text = "진입신호 3단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                            autoIntoJudge[3] = false;
                            autoIntoJudge[4] = true;
                        }
                    }
                    else if (autoIntoJudge[4]) // 진입신호 3단
                    {
                        if (hogaVolume < buyHogaAver)
                        {
                            hogaVolume = buyHogaAver;
                            laView.Text = "거래량 갱신";
                            autoIntoJudge[2] = false;
                            autoIntoJudge[1] = true;
                        }
                        else if (sellHogaAver * 1.05 < buyHogaAver)
                        {
                            laView.Text = "진입신호 2단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                            autoIntoJudge[4] = false;
                            autoIntoJudge[3] = true;

                        }
                        else if (sellHogaAver * 1.05 > buyHogaAver && buyHogaAver * 1.05 > sellHogaAver)
                        {
                            laView.Text = "진입신호 3단계";
                        }
                        else if (buyHogaAver * 1.05 < sellHogaAver)
                        {
                            laView.Text = "진입신호 4단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                            autoIntoJudge[4] = false;
                            autoIntoJudge[5] = true;
                        }
                    }
                    else if (autoIntoJudge[5])
                    {
                        if (buyHogaAver * 1.2 < sellHogaAver)
                        {
                            IntoHoga = InsertHoga();
                            curOrPrice = IntoHoga.ToString();
                            SetAutoTrade(0);
                            laView.Text = "진입가격 포착! 진입가 : " + IntoHoga;
                            autoIntoJudge[0] = true;
                            autoIntoJudge[1] = false;
                            autoIntoJudge[2] = false;
                            autoIntoJudge[3] = false;
                            autoIntoJudge[4] = false;
                            autoIntoJudge[5] = false;
                        }
                        else if (sellHogaAver * 1.05 > buyHogaAver && buyHogaAver * 1.05 > sellHogaAver)
                        {
                            laView.Text = "진입신호 3단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                            autoIntoJudge[5] = false;
                            autoIntoJudge[4] = true;
                        }
                        else if (buyHogaAver * 1.05 < sellHogaAver)
                        {
                            laView.Text = "진입신호 4단계! 매도 잔량 : " + sellHogaAver + " 매수 잔량 : " + buyHogaAver;
                        }
                    }
                }
                else if (strAutoPosition == "매도")
                {
                    VolumeMax = System.Convert.ToInt32(grdHogaLow.Rows[10].Cells[2].Value.ToString());
                    if (autoIntoJudge[0])
                    {
                        if (VolumeMax * conVolume < sellHogaAver)
                        {
                            laView.Text = "진입신호 포착! 매수 잔량 : " + VolumeMax + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[0] = false;
                            autoIntoJudge[1] = true;
                        }
                        else
                        {
                            laView.Text = "진입가 탐색 중입니다.";
                        }
                    }
                    else if (autoIntoJudge[1])  // 진입 신호 포착
                    {
                        if (hogaVolume < sellHogaAver)
                        {
                            hogaVolume = sellHogaAver;
                            laView.Text = "거래량 갱신";
                        }
                        else if (VolumeMax * conVolume < sellHogaAver)
                        {
                            laView.Text = "진입신호 포착!";
                        }
                        if (buyHogaAver * 1.2 < sellHogaAver)
                        {
                            laView.Text = "진입신호 1단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[1] = false;
                            autoIntoJudge[2] = true;
                        }
                    }
                    else if (autoIntoJudge[2])  // 진입신호 1단계!!
                    {
                        /*
                        if (VolumeMax * conVolume < sellHogaAver)
                        {
                            laView.Text = "진입신호 포착! 매수 잔량 : " + VolumeMax + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[2] = false;
                            autoIntoJudge[1] = true;
                        }
                        */

                        if (hogaVolume < sellHogaAver)
                        {
                            hogaVolume = sellHogaAver;
                            laView.Text = "거래량 갱신";
                            autoIntoJudge[2] = false;
                            autoIntoJudge[1] = true;
                        }
                        else if (buyHogaAver * 1.2 < sellHogaAver)
                        {
                            laView.Text = "진입신호 1단계";
                        }
                        else if (buyHogaAver * 1.05 < sellHogaAver)
                        {
                            laView.Text = "진입신호 2단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[2] = false;
                            autoIntoJudge[3] = true;
                        }
                    }
                    else if (autoIntoJudge[3]) // 진입신호 2단계!!
                    {
                        if (hogaVolume < sellHogaAver)
                        {
                            hogaVolume = sellHogaAver;
                            laView.Text = "거래량 갱신";
                            autoIntoJudge[2] = false;
                            autoIntoJudge[1] = true;
                        }
                        else if (buyHogaAver * 1.2 < sellHogaAver)
                        {
                            laView.Text = "진입신호 1단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[3] = false;
                            autoIntoJudge[2] = true;
                        }
                        else if (buyHogaAver * 1.05 < sellHogaAver)
                        {
                            laView.Text = "진입신호 2단계";
                        }
                        else if (buyHogaAver * 1.05 > sellHogaAver && sellHogaAver * 1.05 > buyHogaAver)
                        {
                            laView.Text = "진입신호 3단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[3] = false;
                            autoIntoJudge[4] = true;
                        }
                    }
                    else if (autoIntoJudge[4]) // 진입신호 3단
                    {
                        if (hogaVolume < sellHogaAver)
                        {
                            hogaVolume = sellHogaAver;
                            laView.Text = "거래량 갱신";
                            autoIntoJudge[2] = false;
                            autoIntoJudge[1] = true;
                        }
                        else if (buyHogaAver * 1.05 < sellHogaAver)
                        {
                            laView.Text = "진입신호 2단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[4] = false;
                            autoIntoJudge[3] = true;

                        }
                        else if (buyHogaAver * 1.05 > sellHogaAver && sellHogaAver * 1.05 > buyHogaAver)
                        {
                            laView.Text = "진입신호 3단계";
                        }
                        else if (sellHogaAver * 1.05 < buyHogaAver)
                        {
                            laView.Text = "진입신호 4단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[4] = false;
                            autoIntoJudge[5] = true;
                        }
                    }
                    else if (autoIntoJudge[5])
                    {
                        if (sellHogaAver * 1.2 < buyHogaAver)
                        {
                            IntoHoga = InsertHoga();
                            curOrPrice = IntoHoga.ToString();
                            SetAutoTrade(0);
                            laView.Text = "진입가격 포착! 진입가 : " + IntoHoga;
                            autoIntoJudge[0] = true;
                            autoIntoJudge[1] = false;
                            autoIntoJudge[2] = false;
                            autoIntoJudge[3] = false;
                            autoIntoJudge[4] = false;
                            autoIntoJudge[5] = false;
                        }
                        else if (buyHogaAver * 1.05 > sellHogaAver && sellHogaAver * 1.05 > buyHogaAver)
                        {
                            laView.Text = "진입신호 3단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                            autoIntoJudge[5] = false;
                            autoIntoJudge[4] = true;
                        }
                        else if (sellHogaAver * 1.05 < buyHogaAver)
                        {
                            laView.Text = "진입신호 4단계! 매수 잔량 : " + buyHogaAver + " 매도 잔량 : " + sellHogaAver;
                        }
                    }
                }
            }
        }
        private void AutoTrade()//자동주문
        {
            if (_bRealTrade == false)
                return;
            
            CalcHogaAver();
            IntoTest();

            // 진입탐색 
            if (curOrVolume == "0" && curChVolume == "0" && curGoPrice == "")
            {
                laView.Text = "목표가 탐색 중입니다.";
                TradeLog(laView.Text);
            }
            else if (curOrVolume == "0" && curChVolume == "0" && curOrPrice == "" && curGoPrice != "")
            {
                SearchAutoInto(curPosition, curGoPrice, curOrPrice);
            }
            else if (curOrVolume == "0" && curChVolume == "0" && curState == tradeState[0] && curOrPrice != "" && curGoPrice != "")
            {
                if (curPosition == "매수")
                {
                    laView.Text = curOrPrice + "에 " + curOrVolume + "계약 매수 주문하였습니다.";
                    TradeLog(laView.Text);
                    TradeEx("신규매수(지정가)");
                }
                else
                {
                    laView.Text = curOrPrice + "에 " + curOrVolume + "계약 매도 주문하였습니다.";
                    TradeLog(laView.Text);
                    TradeEx("신규매도(지정가)");
                }
            }
            else if (curOrVolume != "0" && curChVolume == "0" && curState == tradeState[1] && curOrPrice != "" && curGoPrice != "")
            {   
                if (curPosition == "매수")
                {
                    laView.Text = curOrPrice + "에 " + curOrVolume + "계약 매수 진입 대기중입니다.";
                    TradeLog(laView.Text);
                    if (System.Convert.ToDouble(curPrice) > System.Convert.ToDouble(curGoPrice))
                    {   
                        curOrPrice = "";
                        curGoPrice = "";
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetGoalPrice(txtCode.Text, curGoPrice);
                        LD.DeleteData(txtCode.Text, "grdGoal");
                        SetAutoTrade(0);
                        autoIntoJudge[0] = true;
                        autoIntoJudge[1] = false;
                        autoIntoJudge[2] = false;
                        autoIntoJudge[3] = false;
                        autoIntoJudge[4] = false;
                        autoIntoJudge[5] = false;
                        laView.Text = "목표값 재설정 중";
                        TradeEx("매수취소");
                    }
                }
                else
                {
                    laView.Text = curOrPrice + "에 " + curOrVolume + "계약 매도 진입 대기중입니다.";
                    TradeLog(laView.Text);
                    if (System.Convert.ToDouble(curPrice) < System.Convert.ToDouble(curGoPrice))
                    {
                        curOrPrice = "";
                        curGoPrice = "";
                        ((Kiwoom_AutoTrade_FF)this.Owner).SetGoalPrice(txtCode.Text, curGoPrice);
                        LD.DeleteData(txtCode.Text, "grdGoal");
                        SetAutoTrade(0);
                        autoIntoJudge[0] = true;
                        autoIntoJudge[1] = false;
                        autoIntoJudge[2] = false;
                        autoIntoJudge[3] = false;
                        autoIntoJudge[4] = false;
                        autoIntoJudge[5] = false;
                        laView.Text = "목표값 재설정 중";
                        TradeEx("매도취소");
                    }

                }
            }
            else if (curOrVolume != "0" && curChVolume != "0" && curState == tradeState[2] && curOrPrice != "" && curGoPrice != "")
            {
                if (curPosition == "매수")
                {
                    laView.Text = curOrPrice + "에 " + curOrVolume + "계약 매도 진입중입니다.";
                    TradeLog(laView.Text);
                }
                else
                {
                    laView.Text = curOrPrice + "에 " + curOrVolume + "계약 매수 진입중입니다.";
                    TradeLog(laView.Text);
                }
            }
            else if (curOrVolume == "0" && curChVolume != "0" && curState == tradeState[3] && curGoPrice != "") // 진입완료
            {
                double intoPrice = System.Convert.ToDouble(grdAutoTrade.Rows[0].Cells[9].Value.ToString());
                double goalPrice = System.Convert.ToDouble(grdAutoTrade.Rows[0].Cells[11].Value.ToString());
                double tickSize = System.Convert.ToDouble(grdJongInfo.Rows[6].Cells[1].Value.ToString());
                if (Math.Abs(goalPrice - intoPrice) < 30 * tickSize)
                {
                    double temp;
                    if (curPosition == "매수")
                    {
                        temp = intoPrice + 30 * tickSize;
                        grdAutoTrade.Rows[0].Cells[11].Value = temp;
                    }
                    else
                    {
                        temp = intoPrice - 30 * tickSize;
                        grdAutoTrade.Rows[0].Cells[11].Value = temp;
                    }
                }
                if (stopCancle == 0)
                {
                    if (curPosition == "매수")
                    {
                        laView.Text = curChPrice + "에 " + curChVolume + "계약 매수 진입이 완료되었습니다.";
                        TradeLog(laView.Text);
                        TradeEx("매수청산(지정가)");
                    }
                    else
                    {
                        laView.Text = curChPrice + "에 " + curChVolume + "계약 매도 진입이 완료되었습니다.";
                        TradeLog(laView.Text);
                        TradeEx("매도청산(지정가)");
                    }
                }
                else
                    StopLoss();
            }
            else if (curOrVolume != "0" && curChVolume != "0" && curState == tradeState[4] && curGoPrice != "") // 청산대기
            {
                if (stopCancle == 0)
                {
                    StopLoss();
                }
                else
                {
                    if (curPosition == "매수")
                    {
                        laView.Text = curGoPrice + "에 " + curChVolume + "계약이 매수 청산 대기중입니다. ";
                        TradeLog(laView.Text);
                    }
                    else
                    {
                        laView.Text = curGoPrice + "에 " + curChVolume + "계약이 매도 청산 대기중입니다. ";
                        TradeLog(laView.Text);
                    }
                }
            }
            else if (curOrVolume != "0" && curChVolume != "0" && curState == tradeState[5] && curGoPrice != "") // 청산중
            {
                if (stopCancle == 0)
                {
                    StopLoss();
                }
                else
                {
                    if (curPosition == "매수")
                    {
                        laView.Text = curGoPrice + "에 " + curChVolume + "계약이 매수 청산중입니다. ";
                        TradeLog(laView.Text);
                    }
                    else
                    {
                        laView.Text = curGoPrice + "에 " + curChVolume + "계약이 매도 청산중입니다. ";
                        TradeLog(laView.Text);
                    }
                }
            }
            SetAutoTrade(0);
        }

        private void IntoTest()
        {
            if (autoIntoJudge[0])
                grdHogaLow.Rows[5].Cells[0].Value = "탐색중";
            else if (autoIntoJudge[1])
                grdHogaLow.Rows[5].Cells[0].Value = "신호포착";
            else if (autoIntoJudge[2])
                grdHogaLow.Rows[5].Cells[0].Value = "1단계";
            else if (autoIntoJudge[3])
                grdHogaLow.Rows[5].Cells[0].Value = "2단계";
            else if (autoIntoJudge[4])
                grdHogaLow.Rows[5].Cells[0].Value = "3단계";
            else if (autoIntoJudge[5])
                grdHogaLow.Rows[5].Cells[0].Value = "4단계";
            else
                grdHogaLow.Rows[5].Cells[0].Value = "오류";
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
        private void TradeLog(string strData)
        {
            if (strLogData == strData)
                return;
            strLogData = strData;
            string today = DateTime.Today.ToShortDateString();
            string now = DateTime.Now.ToLongTimeString();
            string sData;

            sData = today + " " + now + " " + strData;

            m_strPath = System.IO.Path.GetDirectoryName(m_strPath);

            string sDirPath;
            sDirPath = Application.StartupPath + "\\log\\";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sDirPath);
            if (!di.Exists)  // 경로가 존재 하지 않으면 경로를 생성하라!
                di.Create();

            string fileName = sDirPath + m_strJongCode + ".txt";
            FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs);

            sr.WriteLine(sData);

            sr.Close();
            fs.Close();
        }
        

        private class LocalData
        {
            string[] hogaData, hogaHighData, hogaLowData, curGoalData = new string[2];
            string m_strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;  // 파일이 존재하는 폴더 위치를 저장
            
            public void SetHogaData(string[] arrayData)
            {
                hogaData = arrayData;
            }
            public void SetHogaHighData(string[] arrayData)
            {
                hogaHighData = arrayData;
            }
            public void SetHogaLowData(string[] arrayData)
            {
                hogaLowData = arrayData;
            }
            public string[] GetHogaData(string strState)
            {
                if (strState == "고가갱신" || strState == "눌림")
                    return hogaHighData;
                else if (strState == "저가갱신" || strState == "반등")
                    return hogaLowData;
                else
                    return hogaData;
            }
            
            public void SetGoalData(string[] arrayData)
            {
                curGoalData = arrayData;
            }
            public string[] GetGoalData()
            {
                return curGoalData;
            }


            public void SaveData(string jongCode, string categorize, string[] arrayData)
            {
                string fileName = SetFileName(jongCode, categorize);

                if (categorize == "grdHogaHigh" || categorize == "grdHogaLow")
                {   
                    double temp;

                    FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);

                    int nCnt = 23;
                    sw.WriteLine(DateTime.Now);
                    for(int i=0; i < nCnt; i++)
                    {
                        temp = System.Convert.ToDouble(arrayData[i]);
                        arrayData[i] = temp.ToString();
                        sw.WriteLine(arrayData[i]);
                    }

                    sw.Close();
                    fs.Close();
                }
                if (categorize == "grdGoal")
                {
                    FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);

                    int nCnt = 2;
                    sw.WriteLine(DateTime.Now);
                    {
                        for(int i = 0; i < nCnt; i++)
                        {
                            sw.WriteLine(curGoalData[i]);
                        }
                    }
                    sw.Close();
                    fs.Close();
                }
            }
            public void LoadData(string jongCode, string categorize)
            {
                string fileName = SetFileName(jongCode, categorize);


                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(fileName);

                if (sr.ReadLine() != null)
                {
                    if (categorize == "grdHogaHigh")
                    {
                        string[] strTemp = new string[23];
                        for (int i = 0; i < 23; i++)
                        {
                            strTemp[i] = sr.ReadLine();
                        }
                        hogaHighData = strTemp;
                    }
                    else if (categorize == "grdHogaLow")
                    {
                        string[] strTemp = new string[23];
                        for (int i = 0; i < 23; i++)
                        {
                            strTemp[i] = sr.ReadLine();
                        }
                        hogaLowData = strTemp;
                    }
                    if (categorize == "grdGoal")
                    {
                        string[] strTemp = new string[2];
                        for(int i = 0; i < strTemp.Length; i++)
                        {
                            strTemp[i] = sr.ReadLine();
                        }
                        curGoalData = strTemp;
                    }
                }   
                else
                {   
                }
                sr.Close();
                fs.Close();
            }
            public void DeleteData(string jongCode, string categorize)
            {
                curGoalData[0] = "";
                curGoalData[1] = "";

                string fileName = SetFileName(jongCode, categorize);
                File.Delete(fileName);
            }
            public string SetFileName(string jongCode, string categorize)
            {
                m_strPath = System.IO.Path.GetDirectoryName(m_strPath);

                string sDirPath;
                sDirPath = Application.StartupPath + "\\data\\";
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sDirPath);
                if (!di.Exists)  // 경로가 존재 하지 않으면 경로를 생성하라!
                    di.Create();
                
                string fileName = sDirPath + jongCode + categorize;

                return fileName;
            }
        }  
    }
}
