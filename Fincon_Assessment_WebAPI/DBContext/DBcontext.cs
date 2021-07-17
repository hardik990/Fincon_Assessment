using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Fincon_Assessment_WebAPI.Models;

namespace Fincon_Assessment_WebAPI.DBContext
{
    public class DBcontext
    {
        public KeyValuePair<bool, string> Register(Register _Register)
        {
            if (checkTable())
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string sqlcommand = "select case when exists((select * from tblRegister where email = '" + _Register.Email + "')) then 1 else 0 end";
                    using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        if ((int)sqlcmd.ExecuteScalar() == 1)
                        {
                            return new KeyValuePair<bool, string>(false, "Email already exist.");
                        }
                        else
                        {
                            //sqlcommand = "INSERT INTO tblRegister(name,email,password,IsActive) VALUES (@name,@email,@password,0) ; SELECT SCOPE_IDENTITY()";
                            sqlcommand = "INSERT INTO tblRegister(name,email,password,IsActive) VALUES (@name,@email,@password,1) ; SELECT SCOPE_IDENTITY()";
                            sqlcmd.CommandText = sqlcommand;
                            sqlcmd.CommandType = CommandType.Text;
                            sqlcmd.Parameters.Clear();
                            sqlcmd.Parameters.AddWithValue("@name", _Register.Name);
                            sqlcmd.Parameters.AddWithValue("@email", _Register.Email);
                            sqlcmd.Parameters.AddWithValue("@password", _Register.Password);
                            var ID = sqlcmd.ExecuteScalar();
                            sendEmail(ID.ToString());
                        }
                    }
                }
                return new KeyValuePair<bool, string>(true, "Register Successfully, Pease check Your Email to Active your Account.");
            }
            else
            {
                return new KeyValuePair<bool, string>(false, "Database Not Found.");
            }
        }
        private void sendEmail(string Id)
        {
            try
            {

            }
            catch (Exception Ex)
            {

            }
        }
        public KeyValuePair<bool, dynamic> Login(string email, string password)
        {
            // we can also create different API for configure DB & table.
            if (checkTable())
            {
                Register lstresult = null;
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string sqlcommand = "Select * from  tblRegister where email =@email and password=@password";
                    using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.AddWithValue("@email", email);
                        sqlcmd.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader Dr = sqlcmd.ExecuteReader())
                        {
                            while (Dr.Read())
                            {
                                lstresult = new Register();
                                lstresult.Id = long.Parse(Dr["Id"].ToString());
                                lstresult.Name = Dr["Name"].ToString();
                                lstresult.IsActive = bool.Parse(Dr["IsActive"].ToString());
                            }
                        }
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                if (lstresult == null)
                {
                    return new KeyValuePair<bool, dynamic>(false, "Email & Password Incorrect.");
                }
                else if (lstresult.IsActive == false)
                {
                    sendEmail(lstresult.Id.ToString());
                    return new KeyValuePair<bool, dynamic>(false, "Pease check Your Email to Active your Account.");
                }
                else
                {
                    return new KeyValuePair<bool, dynamic>(true, lstresult);
                }
            }
            else
            {
                return new KeyValuePair<bool, dynamic>(false, "Database Not Found.");
            }
        }
        public KeyValuePair<bool, string> AddQuotation(Quotation _Quotation)
        {
            // we can also create different API for configure DB & table.
            if (checkTable())
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    if (_Quotation.Id == 0)
                    {
                        string sqlcommand = "INSERT INTO tblQuotation(date,quotationnumber,customernamer,customeraddress,status,userid,IsActive) VALUES (@date,@quotationnumber,@customernamer,@customeraddress,@status,@userid,1) ; SELECT SCOPE_IDENTITY()";
                        using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                        {
                            sqlcmd.CommandType = CommandType.Text;

                            sqlcmd.Parameters.AddWithValue("@date", _Quotation.date);
                            sqlcmd.Parameters.AddWithValue("@quotationnumber", _Quotation.QuotationNumber);
                            sqlcmd.Parameters.AddWithValue("@customernamer", _Quotation.CustomerNamer);
                            sqlcmd.Parameters.AddWithValue("@customeraddress", _Quotation.CustomerAddress);
                            sqlcmd.Parameters.AddWithValue("@status", _Quotation.Status);
                            sqlcmd.Parameters.AddWithValue("@userid", _Quotation.UserID);
                            var ID = sqlcmd.ExecuteScalar();

                            sqlcommand = "INSERT INTO tblQuotationDetails(quotationid,description,quantity,price,vat) VALUES (@quotationid,@description,@quantity,@price,@vat)";
                            sqlcmd.CommandText = sqlcommand;
                            sqlcmd.CommandType = CommandType.Text;
                            foreach (var e1 in _Quotation.lstQuotationDetails)
                            {

                                sqlcmd.Parameters.Clear();
                                sqlcmd.Parameters.AddWithValue("@quotationid", ID);
                                sqlcmd.Parameters.AddWithValue("@description", e1.Description);
                                sqlcmd.Parameters.AddWithValue("@quantity", e1.Quantity);
                                sqlcmd.Parameters.AddWithValue("@price", e1.Price);
                                sqlcmd.Parameters.AddWithValue("@vat", e1.Vat);
                                sqlcmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        string sqlcommand = "Update tblQuotation Set date = @date,customernamer =@customernamer,customeraddress=@customeraddress,status=@status Where Id =@Id";
                        using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                        {
                            sqlcmd.CommandType = CommandType.Text;

                            sqlcmd.Parameters.AddWithValue("@customernamer", _Quotation.CustomerNamer);
                            sqlcmd.Parameters.AddWithValue("@customeraddress", _Quotation.CustomerAddress);
                            sqlcmd.Parameters.AddWithValue("@status", _Quotation.Status);
                            sqlcmd.Parameters.AddWithValue("@Id", _Quotation.Id);
                            sqlcmd.ExecuteNonQuery();


                            sqlcommand = "Delete from tblQuotationDetails Where quotationid = @quotationid";
                            sqlcmd.CommandText = sqlcommand;
                            sqlcmd.CommandType = CommandType.Text;
                            sqlcmd.Parameters.Clear();
                            sqlcmd.Parameters.AddWithValue("@quotationid", _Quotation.Id);
                            sqlcmd.ExecuteNonQuery();

                            sqlcommand = "INSERT INTO tblQuotationDetails(quotationid,description,quantity,price,vat) VALUES (@quotationid,@description,@quantity,@price,@vat)";
                            sqlcmd.CommandText = sqlcommand;
                            sqlcmd.CommandType = CommandType.Text;
                            foreach (var e1 in _Quotation.lstQuotationDetails)
                            {

                                sqlcmd.Parameters.Clear();
                                sqlcmd.Parameters.AddWithValue("@quotationid", _Quotation.Id);
                                sqlcmd.Parameters.AddWithValue("@description", e1.Description);
                                sqlcmd.Parameters.AddWithValue("@quantity", e1.Quantity);
                                sqlcmd.Parameters.AddWithValue("@price", e1.Price);
                                sqlcmd.Parameters.AddWithValue("@vat", e1.Vat);
                                sqlcmd.ExecuteNonQuery();
                            }
                        }
                    }

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return new KeyValuePair<bool, string>(true, "Record Inserted Successfully.");
            }
            else
            {
                return new KeyValuePair<bool, string>(false, "Database Not Found.");
            }
        }
        public KeyValuePair<bool, dynamic> GetQuotation(string UserID)
        {
            // we can also create different API for configure DB & table.
            if (checkTable())
            {
                List<Quotation> lstresult = new List<Quotation>();
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string sqlcommand = "Select * from  tblQuotation where userid =@userid and IsActive =1";
                    using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.AddWithValue("@userid", UserID);
                        using (SqlDataReader Dr = sqlcmd.ExecuteReader())
                        {
                            while (Dr.Read())
                            {
                                Quotation objdata = new Quotation();
                                objdata.Id = long.Parse(Dr["Id"].ToString());
                                objdata.date = DateTime.Parse(Dr["date"].ToString());
                                objdata.QuotationNumber = Guid.Parse(Dr["quotationnumber"].ToString());
                                objdata.CustomerNamer = Dr["customernamer"].ToString();
                                objdata.CustomerAddress = Dr["customeraddress"].ToString();
                                objdata.UserID = long.Parse(Dr["userid"].ToString());
                                objdata.Status = long.Parse(Dr["status"].ToString());
                                if (objdata.Status == 0)
                                {
                                    //Peending
                                    objdata.ColourIndex = "#FFFF99";
                                }
                                else if (objdata.Status == 1)
                                {
                                    //Active
                                    objdata.ColourIndex = "#90ee90";
                                }
                                else if (objdata.Status == 2)
                                {
                                    //Reject
                                    objdata.ColourIndex = "#ffcccb";
                                }

                                lstresult.Add(objdata);
                            }
                        }

                        sqlcommand = "SELECT  * FROM tblQuotationDetails WHERE quotationid = @quotationid";
                        sqlcmd.CommandText = sqlcommand;
                        sqlcmd.CommandType = CommandType.Text;
                        foreach (var e1 in lstresult)
                        {
                            sqlcmd.Parameters.Clear();
                            e1.lstQuotationDetails = new List<QuotationDetails>();
                            sqlcmd.Parameters.AddWithValue("@quotationid", e1.Id);
                            using (SqlDataReader Dr = sqlcmd.ExecuteReader())
                            {
                                while (Dr.Read())
                                {
                                    QuotationDetails objlstItem = new QuotationDetails();
                                    objlstItem.Id = long.Parse(Dr["Id"].ToString());
                                    objlstItem.QuotationId = long.Parse(Dr["quotationid"].ToString());
                                    objlstItem.Description = Dr["description"].ToString();
                                    objlstItem.Quantity = decimal.Parse(Dr["quantity"].ToString());
                                    objlstItem.Price = decimal.Parse(Dr["price"].ToString());
                                    objlstItem.Vat = decimal.Parse(Dr["vat"].ToString());
                                    e1.lstQuotationDetails.Add(objlstItem);
                                }
                            }
                        }
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return new KeyValuePair<bool, dynamic>(true, lstresult);
            }
            else
            {
                return new KeyValuePair<bool, dynamic>(false, "Database Not Found.");
            }
        }
        public KeyValuePair<bool, dynamic> GetQuotationId(string Id)
        {
            // we can also create different API for configure DB & table.
            if (checkTable())
            {
                Quotation lstresult = new Quotation();
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string sqlcommand = "Select Top 1 * from  tblQuotation where Id =@Id";
                    using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader Dr = sqlcmd.ExecuteReader())
                        {
                            while (Dr.Read())
                            {
                                lstresult.Id = long.Parse(Dr["Id"].ToString());
                                lstresult.date = DateTime.Parse(Dr["date"].ToString());
                                lstresult.QuotationNumber = Guid.Parse(Dr["quotationnumber"].ToString());
                                lstresult.CustomerNamer = Dr["customernamer"].ToString();
                                lstresult.CustomerAddress = Dr["customeraddress"].ToString();
                                lstresult.UserID = long.Parse(Dr["userid"].ToString());
                                lstresult.Status = long.Parse(Dr["status"].ToString());
                                if (lstresult.Status == 0)
                                {
                                    //Peending
                                    lstresult.ColourIndex = "#FFFF99";
                                }
                                else if (lstresult.Status == 1)
                                {
                                    //Active
                                    lstresult.ColourIndex = "#90ee90";
                                }
                                else if (lstresult.Status == 2)
                                {
                                    //Reject
                                    lstresult.ColourIndex = "#ffcccb";
                                }
                            }
                        }

                        if (lstresult != null && lstresult.Id > 0)
                        {
                            sqlcommand = "SELECT  * FROM tblQuotationDetails WHERE quotationid = @quotationid";
                            sqlcmd.CommandText = sqlcommand;
                            sqlcmd.CommandType = CommandType.Text;
                            sqlcmd.Parameters.Clear();
                            lstresult.lstQuotationDetails = new List<QuotationDetails>();
                            sqlcmd.Parameters.AddWithValue("@quotationid", lstresult.Id);
                            using (SqlDataReader Dr = sqlcmd.ExecuteReader())
                            {
                                while (Dr.Read())
                                {
                                    QuotationDetails objlstItem = new QuotationDetails();
                                    objlstItem.Id = long.Parse(Dr["Id"].ToString());
                                    objlstItem.QuotationId = long.Parse(Dr["quotationid"].ToString());
                                    objlstItem.Description = Dr["description"].ToString();
                                    objlstItem.Quantity = decimal.Parse(Dr["quantity"].ToString());
                                    objlstItem.Price = decimal.Parse(Dr["price"].ToString());
                                    objlstItem.Vat = decimal.Parse(Dr["vat"].ToString());
                                    lstresult.lstQuotationDetails.Add(objlstItem);
                                }
                            }
                        }

                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                return new KeyValuePair<bool, dynamic>(true, lstresult);
            }
            else
            {
                return new KeyValuePair<bool, dynamic>(false, "Database Not Found.");
            }
        }
        public KeyValuePair<bool, dynamic> DeleteQuotation(string Id, string userid)
        {
            // we can also create different API for configure DB & table.
            if (checkTable())
            {
                //Quotation lstresult = new Quotation();
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string sqlcommand = "Update  tblQuotation  set isActive = 0  Where Id = @Id";
                    using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                    {
                        sqlcmd.CommandText = sqlcommand;
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.Clear();
                        sqlcmd.Parameters.AddWithValue("@Id", Id);
                        sqlcmd.ExecuteNonQuery();

                    }
                }
                KeyValuePair<bool, dynamic> lstresult = GetQuotation(userid);
                return new KeyValuePair<bool, dynamic>(true, lstresult.Value);
            }
            else
            {
                return new KeyValuePair<bool, dynamic>(false, "Database Not Found.");
            }
        }
        public KeyValuePair<bool, dynamic> DeleteQuotationItem(string Id)
        {
            // we can also create different API for configure DB & table.
            if (checkTable())
            {
                //Quotation lstresult = new Quotation();
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string sqlcommand = "delete  tblQuotationDetails    Where Id = @Id";
                    using (var sqlcmd = new SqlCommand(sqlcommand, conn))
                    {
                        sqlcmd.CommandText = sqlcommand;
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.Clear();
                        sqlcmd.Parameters.AddWithValue("@Id", Id);
                        sqlcmd.ExecuteNonQuery();

                    }
                }
                KeyValuePair<bool, dynamic> lstresult = GetQuotation(userid);
                return new KeyValuePair<bool, dynamic>(true, lstresult.Value);
            }
            else
            {
                return new KeyValuePair<bool, dynamic>(false, "Database Not Found.");
            }
        }
        private bool checkTable()
        {
            string checkDB = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Fincon.mdf";
            if (!File.Exists(checkDB))
            {
                //Create .mdf file 
                string ldfDB = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Fincon.ldf";
                SqlConnection myConn = new SqlConnection(@"Server=(LocalDB)\MSSQLLocalDB;Integrated security=SSPI;database=master");

                string str = "CREATE DATABASE Fincon ON PRIMARY " +
                    "(NAME = MyDatabase_Data, " +
                    "FILENAME = '" + checkDB + "', " +
                     "SIZE = 2MB, MAXSIZE = 200MB, FILEGROWTH = 10%) " +
                     "LOG ON (NAME = Fincon_Log, " +
                     "FILENAME = '" + ldfDB + "', " +
                     "SIZE = 2MB, " +
                     "MAXSIZE = 50MB, " +
                     "FILEGROWTH = 10%)";
                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    //MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Exception ex)
                {
                    //MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }
            }
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sqltest = "select case when exists((select * from information_schema.tables where table_name = '" + "tblQuotation" + "')) then 1 else 0 end";
                using (var sqlcmd = new SqlCommand(sqltest, conn))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    if ((int)sqlcmd.ExecuteScalar() == 1)
                    {
                        return true;
                    }
                    else
                    {
                        createtable();
                    }
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return true;
        }
        private void createtable()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string sqltest = string.Empty;
                string path = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\DB.txt";
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    sqltest = sqltest + line + new string(' ', 10);
                }
                using (var sqlcmd = new SqlCommand(sqltest, conn))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.ExecuteScalar();
                }
            }
        }
    }
}