using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kiwoom_AutoTrade_FF
{
    class LocalData
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
                for (int i = 0; i < nCnt; i++)
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
                    for (int i = 0; i < nCnt; i++)
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
                    for (int i = 0; i < strTemp.Length; i++)
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

            string sDirPath = System.IO.Directory.GetCurrentDirectory();
            sDirPath += "\\data\\";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sDirPath);
            if (!di.Exists)  // 경로가 존재 하지 않으면 경로를 생성하라!
                di.Create();

            string fileName = sDirPath + jongCode + categorize;

            return fileName;
        }
    }
}



        

        