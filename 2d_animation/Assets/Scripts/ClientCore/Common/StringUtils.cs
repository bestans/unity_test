namespace ClientCore {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using UnityEngine;

    /// <summary>
    /// 字符串处理公共类
    /// </summary>
    public static class StringUtils {
        public const string COLON = ":";
        public const string DIVIDE = "/";
        public const string PERCENT = "%";
        public const string BRACKET_SMALL_LEFT = "(";
        public const string BRACKET_SMALL_RIGHT = ")";
        public const string BRACKET_MIDDLE_LEFT = "[";
        public const string BRACKET_MIDDLE_RIGHT = "]";
        public const string BRACKET_BIG_LEFT = "{";
        public const string BRACKET_BIG_RIGHT = "}";
        public const string MINUS = "-";
        public const string ADD = "+";
        public const string COMMA = ",";
        public const string POINT = ".";
        public const string VERTICAL_LINE = "|";
        public const string UNDERLINE = "_";

        public const char VERTICAL_LINE_CHAR = '|';
        public const char PERCENT_CHAR = '%';
        public const char COMMA_CHAR = ',';

        public const string BLANK = " ";

        public const string GREEN = "00FF00";
        public const string RED = "FF0000";
        public const string YELLOW = "0000FF";
        public const string WHITE = "FFFFFF";
        public const string BLACK = "000000";

        /// <summary>
        ///  获得字符串拼接函数都是非线程安全的，多线程禁止调用
        /// </summary>
        public static StringBuilder stringBuilder = new StringBuilder();
        /// <summary>
        /// 获得带颜色的文本（非线程安全）
        /// </summary>
        /// <param name="sText">目标文本</param>
        /// <param name="sColor">颜色16进制字符串</param>

        public static string GetColor(string sText, string sColor) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(BRACKET_MIDDLE_LEFT);
            stringBuilder.Append(sColor);
            stringBuilder.Append(BRACKET_MIDDLE_RIGHT);
            stringBuilder.Append(sText);
            stringBuilder.Append(BRACKET_MIDDLE_LEFT);
            stringBuilder.Append(MINUS);
            stringBuilder.Append(BRACKET_MIDDLE_RIGHT);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获得带颜色的文本（非线程安全）<see cref="stringBuilder"/>
        /// </summary>
        /// <param name="oText">目标对象</param>
        /// <param name="sColor">颜色16进制字符串</param>
        public static string GetColor(object oText, string sColor) {
            return GetColor(oText.ToString(), sColor);
        }

        /// <summary>
        /// 获得带括弧的文本 类似 "(1)"（非线程安全）
        /// </summary>
        /// <returns></returns>
        public static string GetBraceText(string str) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(BRACKET_SMALL_LEFT);
            stringBuilder.Append(str);
            stringBuilder.Append(BRACKET_SMALL_RIGHT);
            return stringBuilder.ToString();
        }

        public static string GetBlankText(string str1, string str2) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(str1);
            stringBuilder.Append(BLANK);
            stringBuilder.Append(str2);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 得到带百分号的文本 类似 "(1%)"（非线程安全）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPerText(string str) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(BRACKET_SMALL_LEFT);
            stringBuilder.Append(str);
            stringBuilder.Append(PERCENT);
            stringBuilder.Append(BRACKET_SMALL_RIGHT);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获得带括弧的文本 类似"（1/10）"（非线程安全）
        /// </summary>
        /// <param name="iCur">当前值，（第一个值）</param>
        /// <param name="iMax">最大值，（第二个值）</param>

        public static string GetBracketsText(string sCur, string sMax) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(BRACKET_SMALL_LEFT);
            stringBuilder.Append(sCur);
            stringBuilder.Append(DIVIDE);
            stringBuilder.Append(sMax);
            stringBuilder.Append(BRACKET_SMALL_RIGHT);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获得带括弧的文本 类似"（1/10）"（非线程安全）
        /// </summary>
        /// <param name="iCur">当前值，（第一个值）</param>
        /// <param name="iMax">最大值，（第二个值）</param>
        public static string GetBracketsText(int iCur, int iMax) {
            return GetBracketsText(iCur.ToString(), iMax.ToString());
        }

        /// <summary>
        /// 获得带括弧的文本 类似"[目标文本]"（非线程安全）
        /// </summary>
        /// <param sStr="iCur">目标文本</param>
        public static string GetBracketsText(string sStr) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(BRACKET_MIDDLE_LEFT);
            stringBuilder.Append(sStr);
            stringBuilder.Append(BRACKET_MIDDLE_RIGHT);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获得带斜杠的文本 类似"1/10"（非线程安全）
        /// </summary>
        /// <param name="iCur">当前值，（第一个值）</param>
        /// <param name="iMax">最大值，（第二个值）</param>

        public static string GetDivideText(string sCur, string sMax) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(sCur);
            stringBuilder.Append(DIVIDE);
            stringBuilder.Append(sMax);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获得带斜杠的文本 类似"1/10"（非线程安全）
        /// </summary>
        /// <param name="iCur">当前值，（第一个值）</param>
        /// <param name="iMax">最大值，（第二个值）</param>

        public static string GetDivideText(int iCur, int iMax) {
            return GetDivideText(iCur.ToString(), iMax.ToString());
        }

        /// <summary>
        /// 获得带冒号的文本 类似 "等级：1"（非线程安全）
        /// </summary>
        /// <param name="sPre">冒号前部分</param>
        /// <param name="sStd">冒号后部分</param>

        public static string GetColonText(string sPre, string sStd) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(sPre);
            stringBuilder.Append(COLON);
            stringBuilder.Append(sStd);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 两个文本相加（非线程安全）
        /// </summary>
        /// <param name="str1">目标文本1</param>
        /// <param name="str2">目标文本2</param>
        public static string AddText(string str1, string str2) {
            stringBuilder.Remove(0, stringBuilder.Length);
            stringBuilder.Append(str1);
            stringBuilder.Append(str2);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 连接字符串（非线程安全）
        /// </summary>
        /// <param name="strings">需要连接的字符串</param>
        /// <returns>返回字符串相的连接</returns>
        public static string LinkText(params string[] strings) {
            stringBuilder.Remove(0, stringBuilder.Length);
            for (int i = 0, len = strings.Length; i < len; i++) {
                stringBuilder.Append(strings[i]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 连接字符串（非线程安全）
        /// </summary>
        /// <param name="objects">需要连接的对象</param>
        /// <returns>返回字符串相的连接</returns>
        public static string LinkText(params object[] objects) {
            stringBuilder.Remove(0, stringBuilder.Length);
            for (int i = 0, len = objects.Length; i < len; i++) {
                stringBuilder.Append(objects[i].ToString());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// String 转 bool
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StringToBool(string str) {
            if (str == string.Empty) {
                return false;
            }
            return Convert.ToBoolean(str);
        }
        /// <summary>
        /// String 转 int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StringToInt(string str) {
            if (str == string.Empty) {
                return Global.INVALID_INT;
            }
            int value = Global.INVALID_INT;
            if (!int.TryParse(str, out value)) {
                GlobalToolsFunction.LogError("StringToInt error from " + str);
            }
            return value;
        }
        /// <summary>
        /// String 转 uint
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static uint StringToUInt(string str) {
            if (str == string.Empty) {
                return Global.INVALID_UINT;
            }
            uint value = Global.INVALID_UINT;
            if (!uint.TryParse(str, out value)) {
                GlobalToolsFunction.LogError("StringToUInt error from " + str);
            }
            return Convert.ToUInt32(str);
        }
        /// <summary>
        /// String 转 long
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long StringToLong(string str) {
            if (str == string.Empty) {
                return Global.INVALID_LONG;
            }
            long value = Global.INVALID_LONG;
            if (!long.TryParse(str, out value)) {
                GlobalToolsFunction.LogError("StringToLong error from " + str);
            }
            return Convert.ToInt64(str);
        }
        /// <summary>
        /// String 转 ulong
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ulong StringToULong(string str) {
            if (str == string.Empty) {
                return Global.INVALID_ULONG;
            }
            ulong value = Global.INVALID_ULONG;
            if (!ulong.TryParse(str, out value)) {
                GlobalToolsFunction.LogError("StringToULong error from " + str);
            }
            return Convert.ToUInt64(str);
        }
        /// <summary>
        /// String 转 float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float StringToFloat(string str) {
            if (str == string.Empty) {
                return Global.INVALID_FLOAT;
            }
            float value = Global.INVALID_FLOAT;
            if (!float.TryParse(str, out value)) {
                GlobalToolsFunction.LogError("StringToFloat error from " + str);
            }
            return (float)Convert.ToDouble(str);
        }
        /// <summary>
        /// String 转 double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double StringToDouble(string str) {
            if (str == string.Empty) {
                return Global.INVALID_FLOAT;
            }
            double value = Global.INVALID_FLOAT;
            if (!double.TryParse(str, out value)) {
                GlobalToolsFunction.LogError("StringToDouble error from " + str);
            }
            return Convert.ToDouble(str);
        }
        /// <summary>
        /// String 转 Vector3
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector3 StringToVector3(string str) {
            string[] strvector = str.Split(',');
            if (strvector == null || strvector.Length != 3) {
                return Vector3.zero;
            }
            return new Vector3((float)Convert.ToDouble(strvector[0]), (float)Convert.ToDouble(strvector[1]), (float)Convert.ToDouble(strvector[2]));
        }
        /// <summary>
        /// String 转 GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// TODOyyh
        //public static GUID StringToGUID(string str) {
        //    if (str == string.Empty) {
        //        return null;
        //    }
        //    return new GUID(StringToULong(str));
        //}
        /// <summary>
        /// 字符串拆分
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strSplit"></param>
        /// <returns></returns>
        static public string[] StringSplit(string strSource, string strSplit) {
            string[] strtmp = new string[1];
            int index = strSource.IndexOf(strSplit, 0);
            if (index < 0) {
                strtmp[0] = strSource;
                return strtmp;
            } else {
                strtmp[0] = strSource.Substring(0, index);
                return StringSplit(strSource.Substring(index + strSplit.Length), strSplit, strtmp);
            }
        }
        /// <summary>
        /// 字符串拆分
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strSplit"></param>
        /// <param name="attachArray"></param>
        /// <returns></returns>
        static private string[] StringSplit(string strSource, string strSplit, string[] attachArray) {
            string[] strtmp = new string[attachArray.Length + 1];
            attachArray.CopyTo(strtmp, 0);

            int index = strSource.IndexOf(strSplit, 0);
            if (index < 0) {
                strtmp[attachArray.Length] = strSource;
                return strtmp;
            } else {
                strtmp[attachArray.Length] = strSource.Substring(0, index);
                return StringSplit(strSource.Substring(index + strSplit.Length), strSplit, strtmp);
            }
        }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="sTargetStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatString(string sTargetStr, params object[] param) {
            if (string.IsNullOrEmpty(sTargetStr))
                return string.Empty;

            StringBuilder sbTargetStr = new StringBuilder(sTargetStr);
            for (int i = param.Length - 1; i >= 0; i--) {
                if (param[i] == null)
                    continue;

                int j = i + 1;
                sbTargetStr.Replace(PERCENT + j.ToString(), param[i].ToString());
            }
            return sbTargetStr.ToString();
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="sTargetStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatString(string sTargetStr, List<string> param) {
            if (string.IsNullOrEmpty(sTargetStr))
                return string.Empty;

            StringBuilder sbTargetStr = new StringBuilder(sTargetStr);
            int count = param.Count;
            for (int i = count - 1; i >= 0; i--) {
                if (param[i] == null)
                    continue;

                int j = i + 1;
                sbTargetStr.Replace("%" + j.ToString(), param[i].ToString());
            }
            return sbTargetStr.ToString();
        }

        public static bool IsNumber(string key) {
            if (string.IsNullOrEmpty(key)) {
                return false;
            }

            return Regex.IsMatch(key, @"^\d+$");
        }

        /// <summary>
        /// 从表格文字中转移 (&n为换行)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TransformString(string str) {
            string strreplace = str.Replace("&n", "\n");
            return strreplace.Replace("&r", " ");
        }

        /// <summary>
        /// 是否是有效的 tab string 空 null -1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidTabString(string str) {
            return !(string.IsNullOrEmpty(str) || "-1".Equals(str));
        }
    }
}

