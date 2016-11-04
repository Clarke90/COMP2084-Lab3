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
    public partial class department_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false) { 
                // check the url for an id so we know if we are adding or editing
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    // get id from the url
                    Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    // connect
                    var conn = new ContosoEntities();

                    // look up the selected department
                    var objDep = (from d in conn.Departments
                                  where d.DepartmentID == DepartmentID
                                  select d).FirstOrDefault();

                    // populate the form
                    txtName.Text = objDep.Name;
                    txtBudget.Text = objDep.Budget.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // check if we have an id to decide if we're adding or editing
            Int32 DepartmentID = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
            {
                DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);
            }

            // connect
            var conn = new ContosoEntities();

            // use the Department class to create a new Department object
            Department d = new Department();

            // fill the properties of the new department object
            d.Name = txtName.Text;
            d.Budget = Convert.ToDecimal(txtBudget.Text);

            // save the object to the database
            if (DepartmentID == 0)
            {
                conn.Departments.Add(d);
            }
            else
            {
                d.DepartmentID = DepartmentID;
                conn.Departments.Attach(d);
                conn.Entry(d).State = System.Data.Entity.EntityState.Modified;
            }

            conn.SaveChanges();

            // redirect to the departments page
            Response.Redirect("departments.aspx");

        }
    }
}