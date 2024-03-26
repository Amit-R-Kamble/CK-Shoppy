using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CKShoppy.Models;

namespace CKShoppy.Controllers
{
    public class ProductController : Controller
    {
        private IConfiguration _configuration;
        string cstring;
        public ProductController(IConfiguration _configuration) {
            this._configuration = _configuration;
            cstring = _configuration.GetConnectionString("dbconn").ToString();
                }
        public IActionResult Index()
        {
            SqlConnection con = new SqlConnection(cstring); ;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from product", con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<product> products = new List<product>();
            while (dr.Read()) {
                product pobj = new product();
                pobj.pid = Convert.ToInt32(dr["pid"]);
                pobj.pname = dr["pname"].ToString();
                pobj.pimage = dr["pimage"].ToString();
                pobj.pvideo = dr["pvideo"].ToString();
                products.Add(pobj);
            }
            return View(products);

        }
    }
}
