﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Jeelu.Billboard.Server
{
    /// <summary>
    /// 获取软件应用的各种Path环境.(无论是目录还是文件名，名称的大小写基本上都会被转为小写)
    /// </summary>
    internal static partial class Service
    {
        internal static class Path
        {
            /// <summary>
            /// 目录。获取软件应用的各种Path环境的目录。
            /// </summary>
            internal static class Directory
            {
                /// <summary>
                /// 软件配置文件目录
                /// </summary>
                public static string Config { get { return System.IO.Path.Combine(Application.StartupPath, "config"); } }

                public static string Version { get { return System.IO.Path.Combine(Application.StartupPath, "Version"); } }
                /// <summary>
                /// 软件配置文件的语言文件目录，该目录中存放的是由应用程序开发期自动生成的资源文件
                /// </summary>
                public static string ConfigLanguage { get { return System.IO.Path.Combine(Config, "chs"); } }

                /// <summary>
                /// Jeelu.WordSeg的词库目录
                /// </summary>
                public static string SegData { get { return System.IO.Path.Combine(Application.StartupPath, @"segdata\"); } }

                /// <summary>
                /// 软件工作的临时目录
                /// </summary>
                public static string Temp { get { return System.IO.Path.Combine(Application.StartupPath, "temp"); } }

                /// <summary>
                /// 与cgi交互的文件的根目录
                /// </summary>
                public static string RelationFileBasePath { get { return  @"D:\jihui\"; } }
            }

            /// <summary>
            /// 配置文件的后缀名
            /// </summary>
            internal static string ConfigFileExt = ".appcfg";
            /// <summary>
            /// Jeelu.WordSeg用到的词库的后缀名
            /// </summary>
            internal static string WordSegDataExt = ".wseg";
            /// <summary>
            /// 文件。获取软件应用的各种Path环境中的文件。
            /// </summary>
            internal static class File
            {
                /// <summary>
                /// 主DockForm的窗体状态保存文件
                /// </summary>
                public static string WorkbenchLayoutConfig { get { return System.IO.Path.Combine(Directory.Config, "workbenchlayout" + ConfigFileExt); } }

                /// <summary>
                /// 应用程序的选项文件
                /// </summary>
                public static string OptionFile { get { return System.IO.Path.Combine(Directory.Config, "option" + ConfigFileExt); } }

                /// <summary>
                /// 应用程序中数据库访问应用到的所有Sql语句
                /// </summary>
                public static string SqlXml { get { return System.IO.Path.Combine(Directory.Config, "sqlxml.xml"); } }

                /// <summary>
                /// Jeelu.WordSeg用到的主词库
                /// </summary>
                public static string SegWordDict { get { return System.IO.Path.Combine(Directory.SegData, "worddict" + WordSegDataExt); } }
                /// <summary>
                /// Jeelu.WordSeg用到的中文停止词
                /// </summary>
                public static string SegChsStopWords { get { return System.IO.Path.Combine(Directory.SegData, "chsstopwords" + WordSegDataExt); } }
                /// <summary>
                /// Jeelu.WordSeg用到的中文标点符号
                /// </summary>
                public static string SegChsSymbol { get { return System.IO.Path.Combine(Directory.SegData, "chssymbol" + WordSegDataExt); } }
                /// <summary>
                /// Jeelu.WordSeg用到的英文停止词
                /// </summary>
                public static string SegEngStopWords { get { return System.IO.Path.Combine(Directory.SegData, "engstopwords" + WordSegDataExt); } }
                /// <summary>
                /// Jeelu.WordSeg用到的英文标点符号
                /// </summary>
                public static string SegEngSymbol { get { return System.IO.Path.Combine(Directory.SegData, "engsymbol" + WordSegDataExt); } }
                /// <summary>
                /// 
                /// </summary>
                public static string VersionFile { get { return System.IO.Path.Combine(Directory.Version, "DictVersion"); } }


            }
           
        }
    }
}
