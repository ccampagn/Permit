using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using Microsoft.IdentityModel.Protocols;
using Permit.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Configuration;



namespace Permit.DataBase
{
    public class Db
    {
        #region conn
        public SqlConnection openconn()//open db conn
        {
            SqlConnection conn = new SqlConnection(
               new SqlConnectionStringBuilder()
               {
                   DataSource = "localhost\\SQLEXPRESS",
                   InitialCatalog = "PermitDB",
                   UserID = "user",
                   Password = "password"
               }.ConnectionString
              );
            conn.Open();
            return conn;//return conn

        }

        public int sitespots(Campsite campsite)
        {
            int sites = 0;
            Db db = new Db();
            SqlConnection conn = openconn();
            String sql = "SELECT AdvanceSite FROM [PermitDB].[dbo].[Campsites]  where CampsiteId=@campsiteid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@campsiteId", campsite.CampsiteId);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                sites = Convert.ToInt32(rdr["AdvanceSite"]);
            }
            rdr.Close();
            db.closeconn(conn);
            return sites;
        }

        public void closeconn(SqlConnection conn)//function to close conn
        {
            conn.Close();//conn the connection
        }
        #endregion
        public List<Application> getapplications()
        {
            Db db = new Db();
            List<Application> application = new List<Application>();
            SqlConnection conn = openconn();
            String sql = "SELECT ApplicationId,parksid,parks.name as parkname,StartDate,entrytrail,Entry.name as entryname,exittrail,exits.name as exitname,status,GroupSize,Tents,useraccount.UserId,Password.PasswordId,Password.Email,Password.PasswordHash,name.nameid,name.firstname,name.middlename,name.lastname,name.suffix,address.addressid,address.address1,address.address2,address.city,address.state,address.country,address.zipcode,address.phonenumber,EmergencyContract.EmergencyContractID,EmergencyContract.Email as emeremail,emer.nameid as emernameid,emer.firstname as emerfirstname,emer.middlename as emermiddlename,emer.lastname as emerlastname,emer.suffix as emersuffix,emeradd.addressid as emeraddressid,emeradd.address1 as emeraddress1,emeradd.address2 as emeraddress2,emeradd.city as emercity,emeradd.state as emerstate,emeradd.country as emercountry,emeradd.zipcode as emerzipcode,emeradd.phonenumber as emerphonenumber,creditcard.creditcardid,number FROM Applications join UserAccount on Applications.UserId = UserAccount.UserId join Password on UserAccount.PasswordId=Password.PasswordId join creditcard on creditcard.creditcardid=useraccount.creditcardid join EmergencyContract on EmergencyContract.EmergencyContractID = UserAccount.emergencycontractid join name on name.nameid=useraccount.nameid join Parks on Parks.ParkId = Applications.ParksId join address on address.addressid = Name.addressid join name as emer on emer.nameid = emergencycontract.nameid join address as emeradd on emeradd.addressid=emer.addressid join entry on applications.entrytrail = entry.entryid join entry as exits on exits.entryid=applications.exittrail where Status='Pending' ORDER BY NEWID()";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                application.Add(new Application(Convert.ToInt32(rdr["ApplicationId"]),
                    new Park(Convert.ToInt32(rdr["parksid"]), rdr["parkname"].ToString()),
                    Convert.ToDateTime(rdr["StartDate"]),
                    new EntryExit(Convert.ToInt32(rdr["entrytrail"]), rdr["entryname"].ToString()),
                    new EntryExit(Convert.ToInt32(rdr["exittrail"]), rdr["exitname"].ToString()),
                    rdr["status"].ToString(),
                    Convert.ToInt32(rdr["groupsize"]),
                    Convert.ToInt32(rdr["tents"]),
                    getnights(Convert.ToInt32(rdr["ApplicationId"])),
                    new User(Convert.ToInt32(rdr["userid"]), new Password(Convert.ToInt32(rdr["passwordid"]), rdr["email"].ToString(), rdr["passwordhash"].ToString()),
                    new Name(Convert.ToInt32(rdr["nameid"]), rdr["firstname"].ToString(), rdr["middlename"].ToString(), rdr["lastname"].ToString(), rdr["suffix"].ToString(),
                    new Address(Convert.ToInt32(rdr["addressid"]), rdr["address1"].ToString(), rdr["address2"].ToString(), rdr["city"].ToString(), rdr["state"].ToString(), rdr["country"].ToString(), rdr["zipcode"].ToString(), rdr["phonenumber"].ToString())),
                    new EmergencyContract(Convert.ToInt32(rdr["emergencycontractid"]),
                    new Name(Convert.ToInt32(rdr["emernameid"]), rdr["emerfirstname"].ToString(), rdr["emermiddlename"].ToString(), rdr["emerlastname"].ToString(), rdr["emersuffix"].ToString(),
                    new Address(Convert.ToInt32(rdr["emeraddressid"]), rdr["emeraddress1"].ToString(), rdr["emeraddress2"].ToString(), rdr["emercity"].ToString(), rdr["emerstate"].ToString(), rdr["emercountry"].ToString(), rdr["emerzipcode"].ToString(), rdr["emerphonenumber"].ToString())), rdr["emeremail"].ToString()), new CreditCard(Convert.ToInt32(rdr["creditcardid"]), Convert.ToInt32(rdr["number"])))));
            }
            rdr.Close();
            db.closeconn(conn);
            return application;
        }
        public List<Night> getnights(int applicationid)
        {
            Db db = new Db();
            List<Night> night = new List<Night>();
            SqlConnection conn = openconn();
            String sql = "SELECT * FROM Nights where applicationid=@applicationid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@applicationid", applicationid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                night.Add(new Night(Convert.ToInt32(rdr["NightId"]), Convert.ToDateTime(rdr["Date"]), getcampsite(Convert.ToInt32(rdr["CampsiteId"]))));
            }
            rdr.Close();
            db.closeconn(conn);
            return night;
        }
        public Campsite getcampsite(int nightid)
        {
            Db db = new Db();
            Campsite campsite = null;
            SqlConnection conn = openconn();
            String sql = "SELECT * FROM Campsites where campsiteId=@nightid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nightid", nightid);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                campsite = new Campsite(Convert.ToInt32(rdr["CampsiteId"]), rdr["Name"].ToString(), Convert.ToDateTime(rdr["OpenDate"]), Convert.ToDateTime(rdr["CloseDate"]), Convert.ToInt32(rdr["TotalSite"]), Convert.ToInt32(rdr["AdvanceSite"]), Convert.ToInt32(rdr["Stock"]), Convert.ToInt32(rdr["Tents"]), Convert.ToInt32(rdr["GroupSize"]));
            }
            rdr.Close();
            db.closeconn(conn);
            return campsite;
        }

        public int sitetaken(Night a)
        {
            int taken = 0;
            Db db = new Db();
            SqlConnection conn = openconn();
            String sql = "SELECT isnull(sum(case when CEILING(CAST(Applications.GroupSize AS float)/CAST(Campsites.GroupSize AS float)) >=CEILING(CAST(Applications.Tents AS float)/CAST(Campsites.Tents AS float)) then CEILING(CAST(Applications.GroupSize AS float)/CAST(Campsites.GroupSize AS float)) else CEILING(CAST(Applications.Tents AS float)/CAST(Campsites.Tents AS float)) end),0) as siteneeded  FROM [PermitDB].[dbo].[Nights] join Applications on Applications.ApplicationId=Nights.ApplicationId join campsites on campsites.CampsiteId=Nights.CampsiteId where Date=@date and Status='Approve' and Campsites.CampsiteId=@campsiteid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@date", a.Date);
            cmd.Parameters.AddWithValue("@campsiteid", a.Campsite.CampsiteId);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                taken = Convert.ToInt32(rdr["siteneeded"]);
            }
            rdr.Close();
            db.closeconn(conn);
            return taken;
        }
        public bool stockleft(Application a,Night b)
        {
            Db db = new Db();
            SqlConnection conn = openconn();
            String sql = "select ((select Stock from Campsites where CampsiteId=@campsiteid)-(isnull(Sum(Applications.stock),0)) -(select stock from nights join Applications on Applications.ApplicationId = Nights.ApplicationId where applications.ApplicationId=@applicationid)) as stockleft from Campsites join Nights on Campsites.CampsiteId = Nights.CampsiteId join Applications on Applications.ApplicationId = Nights.ApplicationId where Status='Approve' and Campsites.CampsiteId=@campsiteid and Nights.Date=@date";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@campsiteid", b.Campsite.CampsiteId);
            cmd.Parameters.AddWithValue("@date", b.Date);
            cmd.Parameters.AddWithValue("@applicationid",a.ApplicationId);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                if (Convert.ToInt32(rdr["stockleft"]) >= 0)
                {
                    rdr.Close();
                    db.closeconn(conn);
                    return true;
                }
            }
            rdr.Close();
            db.closeconn(conn);
            return false;
        }

        public void updateapplication(int a, string type)
        {
            Db db = new Db();
            SqlConnection conn = openconn();
            String sql = "update Applications set Status=@type where applicationid=@applicationid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@applicationid", a);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.ExecuteNonQuery();
            db.closeconn(conn);

        }
    }
}



            
