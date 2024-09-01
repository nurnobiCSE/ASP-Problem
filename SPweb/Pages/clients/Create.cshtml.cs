using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace SPweb.Pages.clients
{
    public class CreateModel : PageModel
    {
        public ProductInfo p_info = new ProductInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            p_info.add_date = Request.Form["date"];
            p_info.product_category = Request.Form["category"];
            p_info.product_name = Request.Form["name"];
            p_info.product_model = Request.Form["model"];
            p_info.product_quantity = Request.Form["quantity"];
            p_info.product_price = Request.Form["price"];

            if (string.IsNullOrEmpty(p_info.add_date) || string.IsNullOrEmpty(p_info.product_category) ||
                string.IsNullOrEmpty(p_info.product_name) || string.IsNullOrEmpty(p_info.product_model) ||
                string.IsNullOrEmpty(p_info.product_quantity) || string.IsNullOrEmpty(p_info.product_price))
            {
                errorMessage = "All fields are required!";
                return;
            }

            try
            {
                string conString = "Data Source=DESKTOP-4SK1J3Q\\SQL2019ENT;Initial Catalog=Inventory_db;User ID=sa;Password=1234;Encrypt=False";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("InsertProduct_sp", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@add_date", p_info.add_date);
                        cmd.Parameters.AddWithValue("@product_category", p_info.product_category);
                        cmd.Parameters.AddWithValue("@product_name", p_info.product_name);
                        cmd.Parameters.AddWithValue("@product_model", p_info.product_model);
                        cmd.Parameters.AddWithValue("@product_quantity", p_info.product_quantity);
                        cmd.Parameters.AddWithValue("@product_price", p_info.product_price);

                        cmd.ExecuteNonQuery();
                    }
                }

                successMessage = "Product added successfully!";
                Response.Redirect("/clients/Index");
            }
            catch (Exception ex)
            {
                errorMessage = "Error: " + ex.Message;
            }
        }
    }
}
