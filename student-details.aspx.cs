using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// reference the model binding library
using System.Web.ModelBinding;

namespace Week6
{
    public partial class student_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false) {
                //check the url and see if there is an id to know if we are adding or editiing the data 
                if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    //get the id from the url of the object dont forget to convert string to number
                    Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    //connect to the db 
                    var conn = new ContosoEntities();

                    // find the selected departemnt 
                    var stud = (from d in conn.Students
                                where d.StudentID == StudentID
                                select d).FirstOrDefault();

                    //populate the form with the users data 
                    txtLastName.Text = stud.LastName;
                    txtFirstMidName.Text = stud.FirstMidName;
                    txtEnrollmentDate.Text = stud.EnrollmentDate.ToString();
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //check if we have an id to decide if we are adding or editing 
            Int32 StudentID = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
            {
                StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);
            }

            // connect
            var conn = new ContosoEntities();

            // use the Student class to create a new Student object
            Student s = new Student();

            // fill the properties of the new student object
            s.LastName = txtLastName.Text;
            s.FirstMidName = txtFirstMidName.Text;
            s.EnrollmentDate  = Convert.ToDateTime(txtEnrollmentDate.Text);

            // save the new object to the database
            if (StudentID == 0)
            {
                conn.Students.Add(s);
                conn.SaveChanges();
            }
            else
            {
                s.StudentID = StudentID;
                conn.Students.Attach(s);
                conn.Entry(s).State = System.Data.Entity.EntityState.Modified;
            }
            conn.SaveChanges();
            // redirect to the students page
            Response.Redirect("students.aspx");
        }
    }
}