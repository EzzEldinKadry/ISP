using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-542A6SC\\SQLEXPRESS;Initial Catalog=ministry_of_tourism;Integrated Security=True");
        private void canc_booking(/*int tourist_id,*/ int place_id, /*DateTime d*/ int trans_id)
        {
           
            con.Open();
            SqlCommand canc = new SqlCommand("cancel_booking", con);
            canc.CommandType = CommandType.StoredProcedure;
          //  canc.Parameters.Add(new SqlParameter("@date", d));
            canc.Parameters.Add(new SqlParameter("@transport_id", trans_id));
           // canc.Parameters.Add(new SqlParameter("@tourist_id", tourist_id));
            canc.Parameters.Add(new SqlParameter("@tourPlace_id", place_id));
            SqlDataReader reader = canc.ExecuteReader();
            reader.Close();
            con.Close();
        
        }
        private void book(int userId, int placeID, /*DateTime date,*/ int trasnID)
        {
            
            con.Open();
            SqlCommand exc =new SqlCommand ("proc_Reservation",con);
            exc.CommandType = CommandType.StoredProcedure;
            exc.Parameters.Add( new SqlParameter("@userID",userId));
            //exc.Parameters.Add( new SqlParameter("@Date_res",date));
            exc.Parameters.Add( new SqlParameter("@tplaceID",placeID));
            exc.Parameters.Add(new SqlParameter("@trasnID", trasnID));

             SqlDataReader reader = exc.ExecuteReader();
            reader.Close();
            con.Close();


        }

        public Form1()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-542A6SC\\SQLEXPRESS;Initial Catalog=ministry_of_tourism;Integrated Security=True");
            con.Open();
            //CREATING PLACES TABLE
            SqlCommand cmd = new SqlCommand("Select place_id,name,location,price from tourplace", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable place_tbl = new DataTable();
            place_tbl.Columns.Add("place_id");
            place_tbl.Columns.Add("name");
            place_tbl.Columns.Add("location");
            place_tbl.Columns.Add("price");
            DataRow row;

            while (reader.Read())
            {

                row = place_tbl.NewRow();
                row["place_id"] = reader["place_id"];
                row["name"] = reader["name"];
                row["location"] = reader["location"];
                row["price"] = reader["price"];
                place_tbl.Rows.Add(row);
            }
            reader.Close();

            //CREATING TRNS. TABLE
            SqlCommand cmd2 = new SqlCommand("Select trans_ID,type,price,dep_time,arr_time from transportation", con);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader reader2 = cmd2.ExecuteReader();
            DataTable trans_tbl = new DataTable();
            trans_tbl.Columns.Add("trans_ID");
            trans_tbl.Columns.Add("type");
            trans_tbl.Columns.Add("price");
            trans_tbl.Columns.Add("dep_time");
            trans_tbl.Columns.Add("arr_time");

            DataRow row2;

            while (reader2.Read())
            {

                row2 = trans_tbl.NewRow();
                row2["trans_ID"] = reader2["trans_ID"];
                row2["type"] = reader2["type"];
                row2["price"] = reader2["price"];
                row2["dep_time"] = reader2["dep_time"];
                row2["arr_time"] = reader2["arr_time"];
                trans_tbl.Rows.Add(row2);
            }
            reader2.Close();
            con.Close();
            dataGridView2.DataSource = trans_tbl;
            dataGridView1.DataSource = place_tbl;
            //DateTime d = new DateTime(2017, 01, 01);
           
           book(1,1,3);
           //book(2,2,2);
          //book(2,69, 55);
          
    
      //     canc_booking(1,0);
            
        }


    }
}
