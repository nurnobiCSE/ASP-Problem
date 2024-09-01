using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SPweb.Pages.clients
{
    public class IndexModel : PageModel
    {
        public List<ProductInfo> listProducts = new List<ProductInfo>();
        public void OnGet()
        {
            try {
                String conString = "Data Source=DESKTOP-4SK1J3Q\\SQL2019ENT;Initial Catalog=Inventory_db;User ID=sa;Password=1234;Encrypt=False";
                SqlConnection con = new SqlConnection(conString);
                
                con.Open();
                SqlCommand conCommand = new SqlCommand("ListProducts_sp", con);
                SqlDataReader reader = conCommand.ExecuteReader();
                while (reader.Read())
                {
                    ProductInfo p_info = new ProductInfo();
                    p_info.add_date = reader.GetString(0);
                    p_info.product_category = reader.GetString(1);
                    p_info.product_name = reader.GetString(2);
                    p_info.product_model = reader.GetString(3);
                    p_info.product_quantity = reader.GetString(4);
                    p_info.product_price = reader.GetString(5);

                    listProducts.Add(p_info);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured is :"+ex.ToString());
            }
        }
        public void debuf()
        {
            System.Diagnostics.Debug.WriteLine("ello");
        }
    }

    public class ProductInfo 
    {
        public string add_date;
        public string product_category;
        public string product_name;
        public string product_model;
        public string product_quantity;
        public string product_price;
    }
}
