// Carbon Capture - Calvin
// Logger.cs
// Rev 1.00 - July 5, 2023

using JRO;
using System;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Calvin
{
    public class Logger
    {
        private const string strSender = "calvin@carboncapture.com";
        private const string strTitle = "Calvin System Error";

        private static readonly string c = "', '";
        private static string user;
        private static string level;
        private static DateTime date;
        private static string qry;

        private static string db;
        private static int n;
        private static readonly string[] q = new string[30];
        private static readonly string[] v = new string[30];

        public static void LogMachineError(string error)
        {
            n = 0;
            db = "ErrorLog";
            q[0] = "dbError";

            v[0] = error;

            date = DateTime.Now;
            user = App.strUser;
            level = "1";
            WriteQry(date, user, level, n);
            SendEmails(error + " " + date.ToString());
        }

        private static void WriteQry(DateTime dt, string u, string l, int cnt)
        {
            qry = "INSERT INTO [" + db + "] (dbDate, dbUser, dbUserLevel";
            for (int i = 0; i <= cnt; i++)
            {
                qry += ", " + q[i];
            }
            qry += ") VALUES ('" + dt + c + u + c + l;
            for (int i = 0; i <= cnt; i++)
            {
                qry += c + v[i];
            }
            qry += "')";
            WriteData(qry, db);
        }

        public static void CleanDB()
        {
        }

        private static bool CleanTable(string table, DateTime dt)
        {
            try
            {
                using (OleDbConnection UserDB = new OleDbConnection(App.strJet + App.strDB))
                {
                    UserDB.Open();
                    OleDbCommand cmd = new OleDbCommand(table, UserDB);
                    _ = cmd.Parameters.AddWithValue("@p1", dt);
                    _ = cmd.ExecuteNonQuery();
                    UserDB.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CompactDB()
        {
            try
            {
                JetEngine engine = new JetEngine();
                engine.CompactDatabase(string.Format(App.strJet + App.strDB), string.Format(App.strJet + App.strCompactDB));
                File.Delete(App.strDB);
                File.Copy(App.strCompactDB, App.strDB, true);
                File.Delete(App.strCompactDB);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void WriteData(string dbinfo, string db)
        {
            OleDbConnection UserDB = new OleDbConnection(App.strJet + App.strDB);
            try
            {
                UserDB.Open();
            }
            catch (Exception ex)
            {
                App.WPFMessageBoxOK(ex.Message + db, App.bgRed);
                return;
            }
            try
            {
                OleDbCommand Writer = new OleDbCommand(dbinfo, UserDB);
                _ = Writer.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                App.WPFMessageBoxOK(ex.Message + db, App.bgRed);
            }
            UserDB.Close();
        }

        private static bool DBEmpty(string dbinfo)
        {
            using (OleDbConnection UserDB = new OleDbConnection(App.strJet + App.strDB))
            {
                bool bLoad = false;
                OleDbCommand command = new OleDbCommand(dbinfo, UserDB);
                try
                {
                    UserDB.Open();
                }
                catch (Exception ex)
                {
                    App.WPFMessageBoxOK(ex.Message + " - InitLog", App.bgRed);
                    return bLoad;
                }
                try
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    _ = reader.Read();
                    string strtest = reader[0].ToString();
                    UserDB.Close();
                }
                catch
                {
                    UserDB.Close();
                    bLoad = true;
                }
                return bLoad;
            }
        }

        private static void SendEmails(string error)
        {
            for (int i = 0; i < 5; i++)
            {
                if (ADS.strEmail[i] != "")
                {
                    Send(ADS.strEmail[i], error);
                }
            }
        }

        private static void Send(string toEmailAddress, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(strSender);
                mail.To.Add(toEmailAddress);
                mail.Subject = strTitle;
                mail.Body = body;
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(strSender, ADS.strPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
